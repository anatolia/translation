using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using StandardRepository.Helpers;
using StandardRepository.Models;
using StandardUtils.Enumerations;
using StandardUtils.Helpers;
using StandardUtils.Models.DataTransferObjects;

using Translation.Common.Contracts;
using Translation.Common.Models.DataTransferObjects;
using Translation.Common.Models.Requests.Label;
using Translation.Common.Models.Requests.Label.LabelTranslation;
using Translation.Common.Models.Responses.Label;
using Translation.Common.Models.Responses.Label.LabelTranslation;
using Translation.Data.Entities.Domain;
using Translation.Data.Entities.Main;
using Translation.Data.Factories;
using Translation.Data.Repositories.Contracts;
using Translation.Data.UnitOfWorks.Contracts;
using Translation.Service.Managers;
using Language = Translation.Data.Entities.Parameter.Language;

namespace Translation.Service
{
    public class
        LabelService : ILabelService
    {
        private readonly CacheManager _cacheManager;
        private readonly ILabelUnitOfWork _labelUnitOfWork;
        private readonly ILabelRepository _labelRepository;
        private readonly LabelFactory _labelFactory;
        private readonly ILabelTranslationRepository _labelTranslationRepository;
        private readonly LabelTranslationFactory _labelTranslationFactory;
        private readonly IProjectRepository _projectRepository;
        private readonly IOrganizationRepository _organizationRepository;
        private readonly ILanguageRepository _languageRepository;
        private readonly ITokenRepository _tokenRepository;
        private readonly ITextTranslateIntegration _textTranslateIntegration;

        public LabelService(CacheManager cacheManager,
            ILabelUnitOfWork labelUnitOfWork,
            ILabelRepository labelRepository,
            LabelFactory labelFactory,
            ILabelTranslationRepository labelTranslationRepository,
            LabelTranslationFactory labelTranslationFactory,
            IProjectRepository projectRepository,
            IOrganizationRepository organizationRepository,
            ILanguageRepository languageRepository,
            ITokenRepository tokenRepository,
            ITextTranslateIntegration textTranslateIntegration)

        {
            _cacheManager = cacheManager;
            _labelUnitOfWork = labelUnitOfWork;
            _labelRepository = labelRepository;
            _labelFactory = labelFactory;
            _labelTranslationRepository = labelTranslationRepository;
            _labelTranslationFactory = labelTranslationFactory;
            _projectRepository = projectRepository;
            _organizationRepository = organizationRepository;
            _languageRepository = languageRepository;
            _tokenRepository = tokenRepository;
            _textTranslateIntegration = textTranslateIntegration;
        }

        public async Task<LabelCreateResponse> CreateLabel(LabelCreateRequest request)
        {
            var response = new LabelCreateResponse();

            var currentUser = _cacheManager.GetCachedCurrentUser(request.CurrentUserId);
            if (!currentUser.IsAdmin)
            {
                response.SetInvalidBecauseNotAdmin(nameof(User));
                return response;
            }

            var project = await _projectRepository.Select(x => x.Uid == request.ProjectUid);
            if (project.IsNotExist())
            {
                response.SetFailedBecauseNotFound(nameof(Project));
                return response;
            }

            if (project.OrganizationId != currentUser.OrganizationId)
            {
                response.SetInvalid();
                return response;
            }

            if (await _organizationRepository.Any(x => x.Id == project.OrganizationId && !x.IsActive))
            {
                response.SetInvalidBecauseNotActive(nameof(Organization));
                return response;
            }

            if (await _labelRepository.Any(x => x.LabelKey == request.LabelKey && x.ProjectId == project.Id))
            {
                response.SetInvalidBecauseLabelKeyMustBeUnique(nameof(Label));
                return response;
            }

            var label = _labelFactory.CreateEntityFromRequest(request, project);
            var uowResult = await _labelUnitOfWork.DoCreateWork(request.CurrentUserId, label);

            if (!uowResult)
            {
                response.SetFailed();
                return response;
            }

            var uowResultLabelTranslation = true;

            var labelTranslation = "";
            if (request.LanguageUids.Length != 0)
            {
                var projectLanguage = await _languageRepository.SelectById(project.LanguageId);
                var addedLabel = await _labelRepository.Select(x => x.Uid == label.Uid);
                var languages = new List<Language>();

                for (var i = 0; i < request.LanguageUids.Length; i++)
                {
                    var languageUid = request.LanguageUids[i];
                    var language = await _languageRepository.Select(x => x.Uid == languageUid);

                    if (language != null)
                    {
                        languages.Add(language);
                    }
                }

                if (request.IsGettingTranslationFromOtherProject)
                {
                    var allExistingTranslations = await _labelTranslationRepository.SelectAll(x => x.LabelName == label.Name);
                    if (allExistingTranslations.Count != 0)
                    {
                        var demandedTranslations = new Dictionary<string, string>();

                        for (var i = 0; i < allExistingTranslations.Count; i++)
                        {
                            if (!demandedTranslations.ContainsKey(allExistingTranslations[i].LanguageName)
                                && languages.Any(x => x.Name == allExistingTranslations[i].LanguageName))
                            {
                                demandedTranslations.Add(allExistingTranslations[i].LanguageName, allExistingTranslations[i].TranslationText);
                            }
                        }

                        foreach (var item in demandedTranslations.Keys)
                        {
                            var language = languages.Find(x => x.Name == item);
                            var labelTranslationEntity = _labelTranslationFactory.CreateEntity(demandedTranslations[item], addedLabel, language);
                            uowResultLabelTranslation = await _labelUnitOfWork.DoCreateTranslationWork(currentUser.Id, labelTranslationEntity);
                            languages.Remove(language);

                            if (!uowResultLabelTranslation)
                            {
                                continue;
                            }
                        }
                    }
                }

                for (var i = 0; i < languages.Count; i++)
                {
                    if (projectLanguage.IsoCode2Char == languages[i].IsoCode2Char)
                    {
                        labelTranslation = addedLabel.Name.Replace("_", " ");
                    }
                    else
                    {
                        var labelGetTranslatedTextRequest = new LabelGetTranslatedTextRequest(currentUser.Id, addedLabel.Name, languages[i].IsoCode2Char, projectLanguage.IsoCode2Char);
                        var labelGetTranslatedTextResponse = await _textTranslateIntegration.GetTranslatedText(labelGetTranslatedTextRequest);
                        if (labelGetTranslatedTextResponse.Status.IsNotSuccess)
                        {
                            response.SetInvalidBecauseNotActive(nameof(TranslationProvider));
                            return response;
                        }

                        labelTranslation = labelGetTranslatedTextResponse.Item.Name.Replace(",", string.Empty);
                    }

                    var labelTranslationEntity = _labelTranslationFactory.CreateEntity(labelTranslation, addedLabel, languages[i]);
                    uowResultLabelTranslation = await _labelUnitOfWork.DoCreateTranslationWork(currentUser.Id, labelTranslationEntity);

                    if (!uowResultLabelTranslation)
                    {
                        continue;
                    }
                }
            }

            if (uowResultLabelTranslation)
            {
                response.Item = _labelFactory.CreateDtoFromEntity(label);
                response.Status = ResponseStatus.Success;
                return response;
            }

            response.SetFailed();
            return response;
        }

        public async Task<LabelCreateResponse> CreateLabel(LabelCreateWithTokenRequest request)
        {
            var response = new LabelCreateResponse();

            var project = await _projectRepository.Select(x => x.Uid == request.ProjectUid && x.IsActive);
            if (project.IsNotExist())
            {
                response.SetFailedBecauseNotFound(nameof(Project));
                return response;
            }

            var now = DateTime.UtcNow;
            var token = await _tokenRepository.Select(x => x.AccessToken == request.Token && x.ExpiresAt > now);
            if (token.IsNotExist())
            {
                response.SetFailedBecauseNotFound(nameof(Token));
                return response;
            }

            if (token.OrganizationId != project.OrganizationId)
            {
                response.SetInvalid();
                return response;
            }

            if (await _organizationRepository.Any(x => x.Id == project.OrganizationId && !x.IsActive))
            {
                response.SetInvalidBecauseNotActive(nameof(Organization));
                return response;
            }

            if (await _labelRepository.Any(x => x.LabelKey == request.LabelKey && x.ProjectId == project.Id))
            {
                response.SetInvalidBecauseLabelKeyMustBeUnique(nameof(Label));
                return response;
            }

            var label = _labelFactory.CreateEntity(request.LabelKey, project);
            var uowResult = await _labelUnitOfWork.DoCreateWork(token.CreatedBy, label);

            if (!uowResult)
            {
                response.SetFailed();
                return response;
            }

            var uowResultLabelTranslation = true;

            if (request.LanguageIsoCode2s.Length != 0)
            {
                var projectLanguage = await _languageRepository.SelectById(project.LanguageId);
                var addedLabel =
                    await _labelRepository.Select(x => x.LabelKey == request.LabelKey && x.ProjectId == project.Id);

                var languages = new List<Language>();
                for (var i = 0; i < request.LanguageIsoCode2s.Length; i++)
                {
                    var languageIsoCode2s = request.LanguageIsoCode2s[i];
                    var language = await _languageRepository.Select(x => x.IsoCode2Char == languageIsoCode2s);
                    if (language != null)
                    {
                        languages.Add(language);
                    }
                }

                for (var i = 0; i < languages.Count; i++)
                {
                    var labelTranslation = "";
                    if (projectLanguage.IsoCode2Char == languages[i].IsoCode2Char)
                    {
                        labelTranslation = addedLabel.Name.Replace("_", " ");
                    }
                    else
                    {
                        var labelGetTranslatedTextRequest = new LabelGetTranslatedTextRequest(token.CreatedBy, addedLabel.Name, languages[i].IsoCode2Char, projectLanguage.IsoCode2Char);
                        var labelGetTranslatedTextResponse = await _textTranslateIntegration.GetTranslatedText(labelGetTranslatedTextRequest);
                        if (labelGetTranslatedTextResponse.Status.IsNotSuccess)
                        {
                            response.SetInvalidBecauseNotActive(nameof(TranslationProvider));
                            return response;
                        }

                        labelTranslation = labelGetTranslatedTextResponse.Item.Name.Replace(",", string.Empty);
                    }

                    var labelTranslationEntity = _labelTranslationFactory.CreateEntity(labelTranslation, addedLabel, languages[i]);

                    uowResultLabelTranslation = await _labelUnitOfWork.DoCreateTranslationWork(token.CreatedBy, labelTranslationEntity);

                    if (!uowResultLabelTranslation)
                    {
                        continue;
                    }
                }
            }

            if (uowResultLabelTranslation)
            {
                response.Status = ResponseStatus.Success;
                return response;
            }

            response.SetFailed();
            return response;
        }

        public async Task<LabelCreateListResponse> CreateLabelFromList(LabelCreateListRequest request)
        {
            var response = new LabelCreateListResponse();

            var project = await _projectRepository.Select(x => x.Uid == request.ProjectUid);
            if (project.IsNotExist())
            {
                response.SetFailedBecauseNotFound(nameof(Project));
                return response;
            }

            if (!project.IsActive)
            {
                response.SetInvalidBecauseNotActive(nameof(Project));
                return response;
            }

            var currentUser = _cacheManager.GetCachedCurrentUser(request.CurrentUserId);
            if (project.OrganizationId != currentUser.OrganizationId)
            {
                response.SetInvalid();
                return response;
            }

            if (await _organizationRepository.Any(x => x.Id == project.OrganizationId && !x.IsActive))
            {
                response.SetInvalidBecauseNotActive(nameof(Organization));
                return response;
            }

            var labelsToAdd = await AddLabels(project, request.Labels, response);

            response.TotalLabelCount = response.AddedLabelCount + response.CanNotAddedLabelCount;

            var translationsToInsert = new List<LabelTranslation>();
            var translationsToUpdate = new List<LabelTranslation>();
            if (request.UpdateExistedTranslations)
            {
                var translations =
                    await AddedLabelUpdateAndAddLabelTranslation(project, request.Labels, labelsToAdd, response);

                translationsToInsert = translations.Item1;
                translationsToUpdate = translations.Item2;
            }
            else
            {
                translationsToInsert =
                    await AddedLabelAddLabelTranslation(project, request.Labels, labelsToAdd, response);
            }

            var uowResult = await _labelUnitOfWork.DoCreateWorkBulk(request.CurrentUserId, labelsToAdd,
                translationsToInsert, translationsToUpdate);
            if (uowResult)
            {
                response.Status = ResponseStatus.Success;
                return response;
            }

            response.SetFailed();
            return response;
        }

        public async Task<List<Label>> AddLabels(Project project, List<LabelListInfo> labels,
            LabelCreateListResponse response)
        {
            var oldLabels = await _labelRepository.SelectAll(x => x.ProjectId == project.Id, false);

            var isAlphabetical = new Regex("^[A-Za-z0-9_]+$", RegexOptions.Compiled);
            var labelsToInsert = new List<Label>();

            var canOldLabels = new List<string>();

            for (var i = 0; i < labels.Count; i++)
            {
                var translationInfo = labels[i];

                if (labelsToInsert.Any(x => x.LabelKey == translationInfo.LabelKey))
                {
                    continue;
                }

                if (canOldLabels.Any(x => x == translationInfo.LabelKey))
                {
                    continue;
                }

                if (oldLabels.Any(x => x.LabelKey == translationInfo.LabelKey))
                {
                    response.CanNotAddedLabelCount++;
                    canOldLabels.Add(translationInfo.LabelKey);
                    continue;
                }

                if (!isAlphabetical.IsMatch(translationInfo.LabelKey))
                {
                    response.CanNotAddedLabelCount++;
                    response.CanNotAddedLabels.Add(translationInfo.LabelKey);
                    continue;
                }

                var label = _labelFactory.CreateEntity(translationInfo.LabelKey, project);
                labelsToInsert.Add(label);
                response.AddedLabelCount++;
            }

            return labelsToInsert;
        }

        public async Task<List<LabelTranslation>> AddedLabelAddLabelTranslation(Project project,
                                                                                List<LabelListInfo> labels, 
                                                                                List<Label> labelsToAdd,
                                                                                LabelCreateListResponse response)
        {
            var languages = await _languageRepository.SelectAll(null, false);

            var oldTranslations = await _labelTranslationRepository.SelectAll(x => x.ProjectId == project.Id, false);

            var translationsToInsert = new List<LabelTranslation>();

            for (var i = 0; i < labels.Count; i++)
            {
                var translationInfo = labels[i];

                var language = languages.Find(x => x.IsoCode2Char == translationInfo.LanguageIsoCode2);
                if (language == null)
                {
                    response.CanNotAddedLabelTranslationCount++;
                    continue;
                }

                var label = labelsToAdd.Find(x => x.LabelKey == translationInfo.LabelKey);

                if (label != null)
                {
                    if (translationsToInsert.Any(x => x.LanguageId == language.Id
                                                      && x.LabelUid == label.Uid))
                    {
                        response.CanNotAddedLabelTranslationCount++;
                        continue;
                    }

                    var translationForExistingLabel =
                        oldTranslations.FirstOrDefault(x => x.LabelId == label.Id && x.LanguageId == language.Id);

                    if (translationForExistingLabel == null)
                    {
                        var translationToInsert =
                            _labelTranslationFactory.CreateEntity(translationInfo.Translation, label, language);
                        translationsToInsert.Add(translationToInsert);
                        response.AddedLabelTranslationCount++;
                    }
                    else
                    {
                        response.CanNotAddedLabelTranslationCount++;
                    }
                }
            }

            return translationsToInsert;
        }

        public async Task<Tuple<List<LabelTranslation>, List<LabelTranslation>>> AddedLabelUpdateAndAddLabelTranslation(Project project,
                                                                                                                        List<LabelListInfo> labels,
                                                                                                                        List<Label> labelsToAdd,
                                                                                                                        LabelCreateListResponse response)
        {
            var languages = await _languageRepository.SelectAll(null, false);

            var oldLabels = await _labelRepository.SelectAll(x => x.ProjectId == project.Id, false);

            var oldTranslations = await _labelTranslationRepository.SelectAll(x => x.ProjectId == project.Id, false);

            var translationsToInsert = new List<LabelTranslation>();
            var translationsToUpdate = new List<LabelTranslation>();

            for (var i = 0; i < labels.Count; i++)
            {
                var translationInfo = labels[i];

                var language = languages.Find(x => x.IsoCode2Char == translationInfo.LanguageIsoCode2);
                if (language == null)
                {
                    response.CanNotAddedLabelTranslationCount++;
                    continue;
                }

                var label = labelsToAdd.Find(x => x.LabelKey == translationInfo.LabelKey);

                var oldLabel = oldLabels.FirstOrDefault(x => x.LabelKey == translationInfo.LabelKey);

                if (label != null)
                {
                    if (translationsToInsert.Any(x => x.LanguageId == language.Id
                                                      && x.LabelUid == label.Uid))
                    {
                        response.CanNotAddedLabelTranslationCount++;
                        continue;
                    }

                    var translationToInsert =
                        _labelTranslationFactory.CreateEntity(translationInfo.Translation, label, language);
                    translationsToInsert.Add(translationToInsert);
                    response.AddedLabelTranslationCount++;
                }
                else
                {
                    if (await _labelTranslationRepository.Any(x => x.TranslationText == translationInfo.Translation
                                                                   && x.LanguageId == language.Id
                                                                   && x.LabelId == oldLabel.Id))
                    {
                        response.CanNotAddedLabelTranslationCount++;
                        continue;
                    }
                    else
                    {
                        var translationForExistingLabel = oldTranslations.FirstOrDefault(x =>
                            x.LabelId == oldLabel.Id && x.LanguageId == language.Id);
                        if (translationForExistingLabel == null)
                        {
                            var translationToInsert =
                                _labelTranslationFactory.CreateEntity(translationInfo.Translation, oldLabel, language);
                            translationsToInsert.Add(translationToInsert);
                            response.AddedLabelTranslationCount++;
                            continue;
                        }

                        if (translationsToUpdate.Any(x => x.LanguageId == language.Id
                                                          && x.LabelUid == oldLabel.Uid))
                        {
                            response.CanNotAddedLabelTranslationCount++;
                            continue;
                        }

                        translationForExistingLabel.TranslationText = translationInfo.Translation;
                        translationsToUpdate.Add(translationForExistingLabel);
                        response.UpdatedLabelTranslationCount++;
                    }
                }
            }

            return Tuple.Create(translationsToInsert, translationsToUpdate);
        }

        public async Task<LabelReadResponse> GetLabel(LabelReadRequest request)
        {
            var response = new LabelReadResponse();

            var currentUser = _cacheManager.GetCachedCurrentUser(request.CurrentUserId);

            var label = await _labelRepository.Select(x => x.Uid == request.LabelUid);
            if (label.IsNotExist())
            {
                response.SetFailedBecauseNotFound(nameof(Label));
                return response;
            }

            if (label.OrganizationId != currentUser.OrganizationId)
            {
                response.SetFailed();
                return response;
            }

            response.Item = _labelFactory.CreateDtoFromEntity(label);
            response.Status = ResponseStatus.Success;
            return response;
        }

        public async Task<LabelReadByKeyResponse> GetLabelByKey(LabelReadByKeyRequest request)
        {
            var response = new LabelReadByKeyResponse();

            var currentUser = _cacheManager.GetCachedCurrentUser(request.CurrentUserId);

            var label = await _labelRepository.Select(x => x.OrganizationId == currentUser.OrganizationId
                                                           && x.ProjectName == request.ProjectName
                                                           && x.LabelKey == request.LabelKey);
            if (label.IsNotExist())
            {
                response.SetFailedBecauseNotFound(nameof(Label));
                return response;
            }

            response.Item = _labelFactory.CreateDtoFromEntity(label);
            response.Status = ResponseStatus.Success;
            return response;
        }

        public async Task<LabelReadListResponse> GetLabels(LabelReadListRequest request)
        {
            var response = new LabelReadListResponse();

            var project = await _projectRepository.Select(x => x.Uid == request.ProjectUid);
            if (project.IsNotExist())
            {
                response.SetFailedBecauseNotFound(nameof(Project));
                return response;
            }

            var currentUser = _cacheManager.GetCachedCurrentUser(request.CurrentUserId);
            if (project.OrganizationId != currentUser.OrganizationId)
            {
                response.SetInvalid();
                return response;
            }

            Expression<Func<Label, bool>> filter = x => x.ProjectId == project.Id;

            if (request.PagingInfo.SearchTerm.IsNotEmpty())
            {
                filter = x => x.Name.Contains(request.PagingInfo.SearchTerm) && x.ProjectId == project.Id;
            }

            List<Label> entities;
            if (request.PagingInfo.Skip < 1)
            {
                entities = await _labelRepository.SelectAfter(filter, request.PagingInfo.LastUid,
                    request.PagingInfo.Take, false,
                    new List<OrderByInfo<Label>> { new OrderByInfo<Label>(x => x.Uid, request.PagingInfo.IsAscending) });
            }
            else
            {
                entities = await _labelRepository.SelectMany(filter, request.PagingInfo.Skip, request.PagingInfo.Take,
                    false,
                    new List<OrderByInfo<Label>> { new OrderByInfo<Label>(x => x.Id, request.PagingInfo.IsAscending) });
            }

            if (entities != null)
            {
                for (var i = 0; i < entities.Count; i++)
                {
                    var entity = entities[i];
                    var dto = _labelFactory.CreateDtoFromEntity(entity);
                    response.Items.Add(dto);
                }
            }

            response.PagingInfo.Skip = request.PagingInfo.Skip;
            response.PagingInfo.Take = request.PagingInfo.Take;
            response.PagingInfo.LastUid = request.PagingInfo.LastUid;
            response.PagingInfo.IsAscending = request.PagingInfo.IsAscending;
            response.PagingInfo.TotalItemCount = await _labelRepository.Count(filter);

            response.Status = ResponseStatus.Success;
            return response;
        }

        public async Task<LabelSearchListResponse> GetLabels(LabelSearchListRequest request)
        {
            var response = new LabelSearchListResponse();

            var currentUser = _cacheManager.GetCachedCurrentUser(request.CurrentUserId);

            Expression<Func<Label, bool>> filter = x =>
                x.Name.Contains(request.PagingInfo.SearchTerm) && x.OrganizationId == currentUser.OrganizationId;

            List<Label> entities = await _labelRepository.SelectMany(filter, request.PagingInfo.Skip,
                request.PagingInfo.Take, false,
                new List<OrderByInfo<Label>> { new OrderByInfo<Label>(x => x.Id, request.PagingInfo.IsAscending) });

            if (entities != null)
            {
                for (var i = 0; i < entities.Count; i++)
                {
                    var entity = entities[i];
                    var dto = _labelFactory.CreateDtoFromEntity(entity);
                    response.Items.Add(dto);
                }
            }

            response.Status = ResponseStatus.Success;
            return response;
        }

        public async Task<LabelRevisionReadListResponse> GetLabelRevisions(LabelRevisionReadListRequest request)
        {
            var response = new LabelRevisionReadListResponse();

            var currentUser = _cacheManager.GetCachedCurrentUser(request.CurrentUserId);

            var label = await _labelRepository.Select(x => x.Uid == request.LabelUid);
            if (label.IsNotExist())
            {
                response.SetFailedBecauseNotFound(nameof(Label));
                return response;
            }

            var revisions = await _labelRepository.SelectRevisions(label.Id);

            for (var i = 0; i < revisions.Count; i++)
            {
                var revision = revisions[i];

                var revisionDto = new RevisionDto<LabelDto>();
                revisionDto.Revision = revision.Revision;
                revisionDto.RevisionedAt = revision.RevisionedAt;

                var user = _cacheManager.GetCachedUser(revision.RevisionedBy);
                revisionDto.RevisionedByUid = user.Uid;
                revisionDto.RevisionedByName = user.Name;

                revisionDto.Item = _labelFactory.CreateDtoFromEntity(revision.Entity);

                response.Items.Add(revisionDto);
            }

            response.Status = ResponseStatus.Success;
            return response;
        }

        public async Task<AllLabelReadListResponse> GetLabelsWithTranslations(AllLabelReadListRequest request)
        {
            var response = new AllLabelReadListResponse();

            Project project;

            if (request.IsDefaultProject)
            {
                project = await _projectRepository.Select(x => x.IsSuperProject);
            }
            else
            {
                if (request.CurrentUserId > 0)
                {
                    project = await _projectRepository.Select(x => x.Uid == request.ProjectUid && x.IsActive);
                    if (project.IsNotExist())
                    {
                        response.SetFailedBecauseNotFound(nameof(Project));
                        return response;
                    }

                    var currentUser = _cacheManager.GetCachedCurrentUser(request.CurrentUserId);
                    if (currentUser != null)
                    {
                        if (currentUser.OrganizationId != project.OrganizationId)
                        {
                            response.SetInvalid();
                            return response;
                        }
                    }
                }
                else
                {
                    var now = DateTime.UtcNow;
                    var token = await _tokenRepository.Select(x => x.AccessToken == request.Token && x.ExpiresAt > now);
                    if (token.IsNotExist())
                    {
                        response.SetFailedBecauseNotFound(nameof(Token));
                        return response;
                    }

                    project = await _projectRepository.Select(x => x.Uid == request.ProjectUid && x.IsActive);
                    if (project.IsNotExist())
                    {
                        response.SetFailedBecauseNotFound(nameof(Project));
                        return response;
                    }

                    if (token.OrganizationId != project.OrganizationId)
                    {
                        response.SetInvalid();
                        return response;
                    }
                }

                if (await _organizationRepository.Any(x => x.Id == project.OrganizationId && !x.IsActive))
                {
                    response.SetInvalidBecauseNotActive(nameof(Organization));
                    return response;
                }
            }

            var translations =
                await _labelTranslationRepository.SelectAll(x => x.ProjectId == project.Id && x.IsActive, false);
            var languages = await _languageRepository.SelectAll(null, false);

            var entities = await _labelRepository.SelectAll(x => x.ProjectId == project.Id && x.IsActive, false);
            if (translations != null)
            {

                if (entities.Count<=0)
                {
                   response.SetInvalid();
                   response.ErrorMessages.Add("no_labels_to_download");
                   return response;
                }

                for (var i = 0; i < entities.Count; i++)
                {
                    var entity = entities[i];
                    var labelTranslations = translations.Where(x => x.LabelId == entity.Id).ToList();
                    if (!labelTranslations.Any()
                        && !request.IsAddLabelsNotTranslated)
                    {
                        continue;
                    }

                    var dto = new LabelFatDto
                    {
                        Uid = entity.Uid,
                        Key = entity.LabelKey
                    };

                    for (var j = 0; j < labelTranslations.Count; j++)
                    {
                        var labelTranslation = labelTranslations[j];
                        var language = languages.Find(x => x.Id == labelTranslation.LanguageId);
                        if (language == null)
                        {
                            continue;
                        }

                        dto.Translations.Add(new LabelTranslationSlimDto
                        {
                            Translation = labelTranslation.TranslationText,
                            LanguageIsoCode2 = language.IsoCode2Char
                        });
                    }

                    response.Labels.Add(dto);
                }
            }

            response.Status = ResponseStatus.Success;
            return response;
        }

        public async Task<LabelEditResponse> EditLabel(LabelEditRequest request)
        {
            var response = new LabelEditResponse();

            var label = await _labelRepository.Select(x => x.Uid == request.LabelUid);
            if (label.IsNotExist())
            {
                response.SetFailedBecauseNotFound(nameof(Label));
                return response;
            }

            var currentUser = _cacheManager.GetCachedCurrentUser(request.CurrentUserId);
            if (label.OrganizationId != currentUser.OrganizationId)
            {
                response.SetInvalid();
                return response;
            }

            if (await _organizationRepository.Any(x => x.Id == label.OrganizationId && !x.IsActive))
            {
                response.Item = _labelFactory.CreateDtoFromEntity(label);
                response.SetInvalidBecauseNotActive(nameof(Organization));
                return response;
            }

            if (label.LabelKey == request.LabelKey && label.Description == request.Description)
            {
                response.Item = _labelFactory.CreateDtoFromEntity(label);
                response.Status = ResponseStatus.Success;
                return response;
            }

            if (await _labelRepository.Any(x => x.LabelKey == request.LabelKey
                                                && x.ProjectId == label.ProjectId
                                                && x.Uid != request.LabelUid))
            {
                response.SetFailed();
                response.ErrorMessages.Add("label_key_already_exist_in_this_project");
                return response;
            }

            var updatedEntity = _labelFactory.CreateEntityFromRequest(request, label);
            var result = await _labelRepository.Update(request.CurrentUserId, updatedEntity);
            if (result)
            {
                response.Item = _labelFactory.CreateDtoFromEntity(updatedEntity);
                response.Status = ResponseStatus.Success;
                return response;
            }

            response.SetFailed();
            return response;
        }

        public async Task<LabelChangeActivationResponse> ChangeActivation(LabelChangeActivationRequest request)
        {
            var response = new LabelChangeActivationResponse();

            var label = await _labelRepository.Select(x => x.Uid == request.LabelUid);
            if (label.IsNotExist())
            {
                response.SetFailedBecauseNotFound(nameof(Label));
                return response;
            }

            var currentUser = _cacheManager.GetCachedCurrentUser(request.CurrentUserId);
            if (label.OrganizationId != currentUser.OrganizationId)
            {
                response.SetInvalid();
                return response;
            }

            if (await _organizationRepository.Any(x => x.Id == label.OrganizationId && !x.IsActive))
            {
                response.SetInvalidBecauseNotActive(nameof(Organization));
                return response;
            }

            if (await _projectRepository.Any(x => x.Id == label.ProjectId && !x.IsActive))
            {
                response.SetFailedBecauseNotFound(nameof(Project));
                return response;
            }

            var updatedEntity = _labelFactory.UpdateEntityForChangeActivation(label);
            var result = await _labelRepository.Update(request.CurrentUserId, updatedEntity);
            if (result)
            {
                response.Item = _labelFactory.CreateDtoFromEntity(updatedEntity);
                response.Status = ResponseStatus.Success;
                return response;
            }

            response.SetFailed();
            return response;
        }

        public async Task<LabelDeleteResponse> DeleteLabel(LabelDeleteRequest request)
        {
            var response = new LabelDeleteResponse();

            var label = await _labelRepository.Select(x => x.Uid == request.LabelUid);
            if (label.IsNotExist())
            {
                response.SetFailedBecauseNotFound(nameof(Label));
                return response;
            }

            var currentUser = _cacheManager.GetCachedCurrentUser(request.CurrentUserId);
            if (label.OrganizationId != currentUser.OrganizationId)
            {
                response.SetInvalid();
                return response;
            }

            if (await _organizationRepository.Any(x => x.Id == label.OrganizationId && !x.IsActive))
            {
                response.SetInvalidBecauseNotActive(nameof(Organization));
                return response;
            }

            if (await _projectRepository.Any(x => x.Id == label.ProjectId && !x.IsActive))
            {
                response.SetInvalidBecauseNotActive(nameof(Project));
                return response;
            }

            if (await _labelTranslationRepository.Any(x => x.LabelId == label.Id))
            {
                response.SetInvalidBecauseHasChildren(nameof(Label));
                return response;
            }

            var uowResult = await _labelUnitOfWork.DoDeleteWork(request.CurrentUserId, label);
            if (uowResult)
            {
                response.Status = ResponseStatus.Success;
                return response;
            }

            response.SetFailed();
            return response;
        }

        public async Task<LabelCloneResponse> CloneLabel(LabelCloneRequest request)
        {
            var response = new LabelCloneResponse();

            var currentUser = _cacheManager.GetCachedCurrentUser(request.CurrentUserId);
            if (!currentUser.IsAdmin)
            {
                response.SetInvalidBecauseNotAdmin(nameof(User));
                return response;
            }

            if (await _organizationRepository.Any(x => x.Id == currentUser.OrganizationId && !x.IsActive))
            {
                response.SetInvalidBecauseNotActive(nameof(Organization));
                return response;
            }

            var project = await _projectRepository.Select(x => x.Uid == request.ProjectUid);
            if (project.IsNotExist())
            {
                response.SetFailedBecauseNotFound(nameof(Project));
                return response;
            }

            var label = await _labelRepository.Select(x => x.Uid == request.CloningLabelUid);
            if (label.IsNotExist())
            {
                response.SetFailedBecauseNotFound(nameof(Label));
                return response;
            }

            if (label.OrganizationId != currentUser.OrganizationId)
            {
                response.SetInvalid();
                return response;
            }

            if (await _labelRepository.Any(x => x.LabelKey == request.LabelKey && x.ProjectUid == request.ProjectUid))
            {
                response.SetInvalidBecauseLabelKeyMustBeUnique(nameof(Label));
                return response;
            }

            var newLabel = _labelFactory.CreateEntityFromRequest(request, label, project);
            var uowResult = await _labelUnitOfWork.DoCloneWork(request.CurrentUserId, label.Id, newLabel);
            if (uowResult)
            {
                response.Item = _labelFactory.CreateDtoFromEntity(newLabel);
                response.Status = ResponseStatus.Success;
                return response;
            }

            response.SetFailed();
            return response;
        }

        public async Task<LabelRestoreResponse> RestoreLabel(LabelRestoreRequest request)
        {
            var response = new LabelRestoreResponse();

            var currentUser = _cacheManager.GetCachedCurrentUser(request.CurrentUserId);
            if (await _organizationRepository.Any(x => x.Id == currentUser.OrganizationId && !x.IsActive))
            {
                response.SetInvalidBecauseNotActive(nameof(Organization));
                return response;
            }

            var label = await _labelRepository.Select(x => x.Uid == request.LabelUid);
            if (label.IsNotExist())
            {
                response.SetFailedBecauseNotFound(nameof(Label));
                return response;
            }

            var revisions = await _labelRepository.SelectRevisions(label.Id);
            if (revisions.All(x => x.Revision != request.Revision))
            {
                response.SetFailedBecauseRevisionNotFound(nameof(Label));
                return response;
            }

            var result = await _labelRepository.RestoreRevision(request.CurrentUserId, label.Id, request.Revision);
            if (result)
            {
                response.Status = ResponseStatus.Success;
                return response;
            }

            response.SetFailed();
            return response;
        }

        public async Task<LabelTranslationCreateResponse> CreateTranslation(LabelTranslationCreateRequest request)
        {
            var response = new LabelTranslationCreateResponse();

            var label = await _labelRepository.Select(x => x.Uid == request.LabelUid);
            if (label.IsNotExist())
            {
                response.SetFailedBecauseNotFound(nameof(Label));
                return response;
            }

            if (!label.IsActive)
            {
                response.SetInvalidBecauseNotActive(nameof(Label));
                return response;
            }

            var currentUser = _cacheManager.GetCachedCurrentUser(request.CurrentUserId);
            if (label.OrganizationId != currentUser.OrganizationId)
            {
                response.SetInvalid();
                return response;
            }

            if (await _organizationRepository.Any(x => x.Id == label.OrganizationId && !x.IsActive))
            {
                response.SetInvalidBecauseNotActive(nameof(Organization));
                return response;
            }

            if (await _projectRepository.Any(x => x.Id == label.ProjectId && !x.IsActive))
            {
                response.SetInvalidBecauseNotActive(nameof(Project));
                return response;
            }

            var language = await _languageRepository.Select(x => x.Uid == request.LanguageUid);
            if (language.IsNotExist())
            {
                response.SetFailedBecauseNotFound(nameof(Language));
                return response;
            }

            if (await _labelTranslationRepository.Any(x => x.LanguageId == language.Id && x.LabelId == label.Id))
            {
                response.ErrorMessages.Add("edit_old_label_translation");
                response.Status = ResponseStatus.Failed;
                return response;
            }

            var labelTranslation = _labelTranslationFactory.CreateEntity(request.LabelTranslation, label, language);
            var uowResult = await _labelUnitOfWork.DoCreateTranslationWork(request.CurrentUserId, labelTranslation);
            if (uowResult)
            {
                response.Item = _labelTranslationFactory.CreateDtoFromEntity(labelTranslation, language);
                response.Status = ResponseStatus.Success;
                return response;
            }

            response.SetFailed();
            return response;
        }

        public async Task<LabelTranslationCreateListResponse> CreateTranslationFromList(LabelTranslationCreateListRequest request)
        {
            var response = new LabelTranslationCreateListResponse();

            var label = await _labelRepository.Select(x => x.Uid == request.LabelUid);
            if (label.IsNotExist())
            {
                response.SetFailedBecauseNotFound(nameof(Label));
                return response;
            }

            if (!label.IsActive)
            {
                response.SetInvalidBecauseNotActive(nameof(Label));
                return response;
            }

            var currentUser = _cacheManager.GetCachedCurrentUser(request.CurrentUserId);
            if (label.OrganizationId != currentUser.OrganizationId)
            {
                response.SetInvalid();
                return response;
            }

            if (await _organizationRepository.Any(x => x.Id == label.OrganizationId && !x.IsActive))
            {
                response.SetInvalidBecauseNotActive(nameof(Organization));
                return response;
            }

            if (await _projectRepository.Any(x => x.Id == label.ProjectId && !x.IsActive))
            {
                response.SetInvalidBecauseNotActive(nameof(Project));
                return response;
            }

            var translationsToInsert = new List<LabelTranslation>();
            var translationsToUpdate = new List<LabelTranslation>();

            if (request.UpdateExistedTranslations)
            {
                var translations = await UpdateAndAddLabelTranslation(label, request.LabelTranslations, response);

                translationsToInsert = translations.Item1;
                translationsToUpdate = translations.Item2;
            }
            else
            {
                translationsToInsert = await AddLabelTranslation(label, request.LabelTranslations, response);
            }

            var uowResult = await _labelUnitOfWork.DoCreateTranslationWorkBulk(request.CurrentUserId,
                translationsToInsert, translationsToUpdate);
            if (uowResult)
            {
                response.Status = ResponseStatus.Success;
                return response;
            }

            response.SetFailed();
            return response;
        }

        public async Task<List<LabelTranslation>> AddLabelTranslation(Label label, List<TranslationListInfo> LabelTranslations, LabelTranslationCreateListResponse response)
        {
            var languages = await _languageRepository.SelectAll(null, false);

            var oldTranslations =
                await _labelTranslationRepository.SelectAll(x => x.ProjectId == label.ProjectId, false);

            var translationsToInsert = new List<LabelTranslation>();

            for (var i = 0; i < LabelTranslations.Count; i++)
            {
                var translationInfo = LabelTranslations[i];

                var language = languages.Find(x => x.IsoCode2Char == translationInfo.LanguageIsoCode2);
                if (language == null)
                {
                    response.CanNotAddedTranslationCount++;
                    continue;
                }

                if (translationsToInsert.Any(x => x.LanguageId == language.Id
                                                  && x.LabelId == label.Id))
                {
                    response.CanNotAddedTranslationCount++;
                    continue;
                }

                var translationForExistingLabel =
                    oldTranslations.FirstOrDefault(x => x.LabelId == label.Id && x.LanguageId == language.Id);

                if (translationForExistingLabel == null)
                {
                    var translationToInsert =
                        _labelTranslationFactory.CreateEntity(translationInfo.Translation, label, language);
                    translationsToInsert.Add(translationToInsert);
                    response.AddedTranslationCount++;
                }
                else
                {
                    response.CanNotAddedTranslationCount++;
                }
            }

            return translationsToInsert;
        }

        public async Task<Tuple<List<LabelTranslation>, List<LabelTranslation>>> UpdateAndAddLabelTranslation(Label label, List<TranslationListInfo> LabelTranslations, LabelTranslationCreateListResponse response)
        {
            var languages = await _languageRepository.SelectAll(null, false);

            var oldTranslations =
                await _labelTranslationRepository.SelectAll(x => x.ProjectId == label.ProjectId, false);

            var translationsToInsert = new List<LabelTranslation>();
            var translationsToUpdate = new List<LabelTranslation>();

            for (var i = 0; i < LabelTranslations.Count; i++)
            {
                var translationInfo = LabelTranslations[i];

                var language = languages.Find(x => x.IsoCode2Char == translationInfo.LanguageIsoCode2);
                if (language == null)
                {
                    response.CanNotAddedTranslationCount++;
                    continue;
                }

                if (translationsToInsert.Any(x => x.LanguageId == language.Id && x.LabelId == label.Id))
                {
                    response.CanNotAddedTranslationCount++;
                    continue;
                }

                if (translationsToUpdate.Any(x => x.LanguageId == language.Id && x.LabelId == label.Id))
                {
                    response.CanNotAddedTranslationCount++;
                    continue;
                }

                if (await _labelTranslationRepository.Any(x => x.TranslationText == translationInfo.Translation
                                                               && x.LanguageId == language.Id
                                                               && x.LabelId == label.Id))
                {
                    response.CanNotAddedTranslationCount++;
                    continue;
                }
                else
                {
                    var translationForExistingLabel =
                        oldTranslations.FirstOrDefault(x => x.LabelId == label.Id && x.LanguageId == language.Id);

                    if (translationForExistingLabel == null)
                    {
                        var translationToInsert =
                            _labelTranslationFactory.CreateEntity(translationInfo.Translation, label, language);
                        translationsToInsert.Add(translationToInsert);
                        response.AddedTranslationCount++;
                        continue;
                    }

                    translationForExistingLabel.TranslationText = translationInfo.Translation;
                    translationsToUpdate.Add(translationForExistingLabel);
                    response.UpdatedTranslationCount++;
                }
            }

            return Tuple.Create(translationsToInsert, translationsToUpdate);
        }

        public async Task<LabelTranslationReadResponse> GetTranslation(LabelTranslationReadRequest request)
        {
            var response = new LabelTranslationReadResponse();

            var currentUser = _cacheManager.GetCachedCurrentUser(request.CurrentUserId);

            var labelTranslation = await _labelTranslationRepository.Select(x => x.Uid == request.LabelTranslationUid);
            if (labelTranslation.IsNotExist())
            {
                response.SetFailedBecauseNotFound(nameof(LabelTranslation));
                return response;
            }

            if (labelTranslation.OrganizationId != currentUser.OrganizationId)
            {
                response.SetFailed();
                return response;
            }

            var language = await _languageRepository.SelectById(labelTranslation.LanguageId);
            if (language.IsNotExist())
            {
                response.SetFailedBecauseNotFound(nameof(Language));
                return response;
            }

            response.Item = _labelTranslationFactory.CreateDtoFromEntity(labelTranslation, language);
            response.Status = ResponseStatus.Success;
            return response;
        }

        public async Task<LabelTranslationReadListResponse> GetTranslations(LabelTranslationReadListRequest request)
        {
            var response = new LabelTranslationReadListResponse();

            var label = await _labelRepository.Select(x => x.Uid == request.LabelUid);
            if (label.IsNotExist())
            {
                response.SetFailedBecauseNotFound(nameof(Label));
                return response;
            }

            var currentUser = _cacheManager.GetCachedCurrentUser(request.CurrentUserId);
            if (label.OrganizationId != currentUser.OrganizationId)
            {
                response.SetInvalid();
                return response;
            }

            Expression<Func<LabelTranslation, bool>> filter = x => x.LabelId == label.Id;

            if (request.PagingInfo.SearchTerm.IsNotEmpty())
            {
                filter = x => x.Name.Contains(request.PagingInfo.SearchTerm) && x.ProjectId == label.Id;
            }

            List<LabelTranslation> entities;
            if (request.PagingInfo.Skip < 1)
            {
                entities = await _labelTranslationRepository.SelectAfter(filter, request.PagingInfo.LastUid, request.PagingInfo.Take, false,
                                                                         new List<OrderByInfo<LabelTranslation>> { new OrderByInfo<LabelTranslation>(x => x.Uid, request.PagingInfo.IsAscending) });
            }
            else
            {
                entities = await _labelTranslationRepository.SelectMany(filter, request.PagingInfo.Skip, request.PagingInfo.Take, false,
                                                                         new List<OrderByInfo<LabelTranslation>> { new OrderByInfo<LabelTranslation>(x => x.Id, request.PagingInfo.IsAscending) });
            }

            if (entities != null)
            {
                var languages = await _languageRepository.SelectAll(null, false);

                for (var i = 0; i < entities.Count; i++)
                {
                    var entity = entities[i];
                    var language = languages.Find(x => x.Id == entity.LanguageId);
                    if (language != null)
                    {
                        var dto = _labelTranslationFactory.CreateDtoFromEntity(entity, language);
                        response.Items.Add(dto);
                    }
                    //todo added else info messages
                }
            }

            response.PagingInfo.Skip = request.PagingInfo.Skip;
            response.PagingInfo.Take = request.PagingInfo.Take;
            response.PagingInfo.LastUid = request.PagingInfo.LastUid;
            response.PagingInfo.IsAscending = request.PagingInfo.IsAscending;
            response.PagingInfo.TotalItemCount = await _labelTranslationRepository.Count(filter);

            response.Status = ResponseStatus.Success;
            return response;
        }

        public async Task<LabelTranslationRevisionReadListResponse> GetLabelTranslationRevisions(LabelTranslationRevisionReadListRequest request)
        {
            var response = new LabelTranslationRevisionReadListResponse();

            var currentUser = _cacheManager.GetCachedCurrentUser(request.CurrentUserId);

            var labelTranslation = await _labelTranslationRepository.Select(x => x.Uid == request.LabelTranslationUid);
            if (labelTranslation.IsNotExist())
            {
                response.SetFailedBecauseNotFound(nameof(LabelTranslation));
                return response;
            }

            var revisions = await _labelTranslationRepository.SelectRevisions(labelTranslation.Id);

            for (var i = 0; i < revisions.Count; i++)
            {
                var revision = revisions[i];

                var revisionDto = new RevisionDto<LabelTranslationDto>();
                revisionDto.Revision = revision.Revision;
                revisionDto.RevisionedAt = revision.RevisionedAt;

                var user = _cacheManager.GetCachedUser(revision.RevisionedBy);
                revisionDto.RevisionedByUid = user.Uid;
                revisionDto.RevisionedByName = user.Name;

                revisionDto.Item = _labelTranslationFactory.CreateDtoFromEntity(revision.Entity);

                response.Items.Add(revisionDto);
            }

            response.Status = ResponseStatus.Success;
            return response;
        }

        public async Task<LabelTranslationEditResponse> EditTranslation(LabelTranslationEditRequest request)
        {
            var response = new LabelTranslationEditResponse();

            var labelTranslation = await _labelTranslationRepository.Select(x => x.Uid == request.LabelTranslationUid);
            if (labelTranslation.IsNotExist())
            {
                response.SetFailedBecauseNotFound(nameof(LabelTranslation));
                return response;
            }

            if (labelTranslation.TranslationText == request.NewTranslation)
            {
                response.Item = _labelTranslationFactory.CreateDtoFromEntity(labelTranslation);
                response.Status = ResponseStatus.Success;
                return response;
            }

            var currentUser = _cacheManager.GetCachedCurrentUser(request.CurrentUserId);
            if (labelTranslation.OrganizationId != currentUser.OrganizationId)
            {
                response.SetInvalid();
                return response;
            }

            if (await _organizationRepository.Any(x => x.Id == labelTranslation.OrganizationId && !x.IsActive))
            {
                response.SetInvalidBecauseNotActive(nameof(Organization));
                return response;
            }

            if (await _projectRepository.Any(x => x.Id == labelTranslation.ProjectId && !x.IsActive))
            {
                response.SetInvalidBecauseNotActive(nameof(Project));
                return response;
            }

            if (await _labelRepository.Any(x => x.Id == labelTranslation.LabelId && !x.IsActive))
            {
                response.SetInvalidBecauseNotActive(nameof(Label));
                return response;
            }

            var updatedLabelTranslation = _labelTranslationFactory.CreateEntityFromRequest(request, labelTranslation);
            var result = await _labelTranslationRepository.Update(request.CurrentUserId, updatedLabelTranslation);
            if (result)
            {
                response.Item = _labelTranslationFactory.CreateDtoFromEntity(updatedLabelTranslation);
                response.Status = ResponseStatus.Success;
                return response;
            }

            response.SetFailed();
            return response;
        }

        public async Task<LabelTranslationDeleteResponse> DeleteTranslation(LabelTranslationDeleteRequest request)
        {
            var response = new LabelTranslationDeleteResponse();

            var labelTranslation = await _labelTranslationRepository.Select(x => x.Uid == request.LabelTranslationUid);
            if (labelTranslation.IsNotExist())
            {
                response.SetFailedBecauseNotFound(nameof(LabelTranslation));
                return response;
            }

            var currentUser = _cacheManager.GetCachedCurrentUser(request.CurrentUserId);
            if (labelTranslation.OrganizationId != currentUser.OrganizationId)
            {
                response.SetInvalid();
                return response;
            }

            if (await _organizationRepository.Any(x => x.Id == labelTranslation.OrganizationId && !x.IsActive))
            {
                response.SetInvalidBecauseNotActive(nameof(Organization));
                return response;
            }

            if (await _projectRepository.Any(x => x.Id == labelTranslation.ProjectId && !x.IsActive))
            {
                response.SetInvalidBecauseNotActive(nameof(Project));
                return response;
            }

            if (await _labelRepository.Any(x => x.Id == labelTranslation.LabelId && !x.IsActive))
            {
                response.SetInvalidBecauseNotActive(nameof(Label));
                return response;
            }

            var uowResult = await _labelUnitOfWork.DoDeleteTranslationWork(request.CurrentUserId, labelTranslation);
            if (uowResult)
            {
                response.Status = ResponseStatus.Success;
                return response;
            }

            response.SetFailed();
            return response;
        }

        public async Task<LabelTranslationRestoreResponse> RestoreLabelTranslation(LabelTranslationRestoreRequest request)
        {
            var response = new LabelTranslationRestoreResponse();

            var currentUser = _cacheManager.GetCachedCurrentUser(request.CurrentUserId);
            if (await _organizationRepository.Any(x => x.Id == currentUser.OrganizationId && !x.IsActive))
            {
                response.SetInvalidBecauseNotActive(nameof(Organization));
                return response;
            }

            var labelTranslation = await _labelTranslationRepository.Select(x => x.Uid == request.LabelTranslationUid);
            if (labelTranslation.IsNotExist())
            {
                response.SetFailedBecauseNotFound(nameof(LabelTranslation));
                return response;
            }

            var revisions = await _labelTranslationRepository.SelectRevisions(labelTranslation.Id);
            if (revisions.All(x => x.Revision != request.Revision))
            {
                response.SetFailedBecauseRevisionNotFound(nameof(LabelTranslation));
                return response;
            }

            var result =
                await _labelTranslationRepository.RestoreRevision(request.CurrentUserId, labelTranslation.Id,
                    request.Revision);
            if (result)
            {
                response.Status = ResponseStatus.Success;
                return response;
            }

            response.SetFailed();
            return response;
        }
    }
}