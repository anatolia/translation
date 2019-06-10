using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using StandardRepository.Helpers;
using Translation.Common.Contracts;
using Translation.Common.Enumerations;
using Translation.Common.Helpers;
using Translation.Common.Models.Requests.Label;
using Translation.Common.Models.Requests.Label.LabelTranslation;
using Translation.Common.Models.Responses.Label;
using Translation.Common.Models.Responses.Label.LabelTranslation;
using Translation.Data.Entities.Domain;
using Translation.Data.Factories;
using Translation.Data.Repositories.Contracts;
using Translation.Data.UnitOfWorks.Contracts;
using Translation.Service.Managers;

namespace Translation.Service
{
    public class LabelService : ILabelService
    {
        private readonly CacheManager _cacheManager;
        private readonly ILabelUnitOfWork _labelUnitOfWork;
        private readonly ILabelRepository _labelRepository;
        private readonly LabelFactory _labelFactory;
        private readonly ILabelTranslationRepository _labelTranslationRepository;
        private readonly LabelTranslationFactory _labelTranslationFactory;
        private readonly IProjectRepository _projectRepository;
        private readonly ProjectFactory _projectFactory;
        private readonly IOrganizationRepository _organizationRepository;
        private readonly ILanguageRepository _languageRepository;

        public LabelService(CacheManager cacheManager,
                            ILabelUnitOfWork labelUnitOfWork,
                            ILabelRepository labelRepository, LabelFactory labelFactory,
                            ILabelTranslationRepository labelTranslationRepository, LabelTranslationFactory labelTranslationFactory,
                            IProjectRepository projectRepository, ProjectFactory projectFactory,
                            IOrganizationRepository organizationRepository,
                            ILanguageRepository languageRepository)
        {
            _cacheManager = cacheManager;
            _labelUnitOfWork = labelUnitOfWork;
            _labelRepository = labelRepository;
            _labelFactory = labelFactory;
            _labelTranslationRepository = labelTranslationRepository;
            _labelTranslationFactory = labelTranslationFactory;
            _projectRepository = projectRepository;
            _projectFactory = projectFactory;
            _organizationRepository = organizationRepository;
            _languageRepository = languageRepository;
        }

        public async Task<LabelCreateResponse> CreateLabel(LabelCreateRequest request)
        {
            var response = new LabelCreateResponse();

            var currentUser = _cacheManager.GetCachedCurrentUser(request.CurrentUserId);
            if (!currentUser.IsAdmin)
            {
                response.SetInvalid();
                return response;
            }

            var project = await _projectRepository.Select(x => x.Uid == request.ProjectUid);
            if (project.IsNotExist())
            {
                response.SetInvalidBecauseEntityNotFound();
                return response;
            }

            if (project.OrganizationId != currentUser.OrganizationId)
            {
                response.SetInvalid();
                return response;
            }

            if (await _organizationRepository.Any(x => x.Id == project.OrganizationId && !x.IsActive))
            {
                response.SetInvalid();
                return response;
            }

            if (await _labelRepository.Any(x => x.Key == request.LabelKey && x.ProjectId == project.Id))
            {
                response.ErrorMessages.Add("label_key_must_be_unique");
                response.Status = ResponseStatus.Failed;
                return response;
            }

            var label = _labelFactory.CreateEntityFromRequest(request, project);
            var uowResult = await _labelUnitOfWork.DoCreateWork(request.CurrentUserId, label);
            if (uowResult)
            {
                response.Item = _labelFactory.CreateDtoFromEntity(label);
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
                response.SetInvalidBecauseEntityNotFound();
                return response;
            }

            if (!project.IsActive)
            {
                response.SetInvalidBecauseParentNotActive();
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
                response.SetInvalidBecauseParentNotActive();
                return response;
            }

            var labels = new List<Label>();
            var warningCount = 0;
            for (var i = 0; i < request.Labels.Count; i++)
            {
                var translationInfo = request.Labels[i];
                if (await _labelRepository.Any(x => x.Key == translationInfo.LabelKey
                                                    && x.ProjectId == project.Id))
                {
                    warningCount++;
                    continue;
                }

                var label = _labelFactory.CreateEntityFromRequest(translationInfo.LabelKey, project);
                labels.Add(label);
            }

            response.CanNotAddedLabelCount = warningCount;

            var languages = await _languageRepository.SelectAll(null);

            var translations = new List<LabelTranslation>();
            var warningCountTranslation = 0;
            for (var i = 0; i < request.Labels.Count; i++)
            {
                var translationInfo = request.Labels[i];

                var label = labels.Find(x => x.Key == translationInfo.LabelKey);
                if (label == null)
                {
                    warningCountTranslation++;
                    continue;
                }

                var language = languages.Find(x => x.IsoCode2Char == translationInfo.LanguageIsoCode2);
                if (language == null)
                {
                    warningCountTranslation++;
                    continue;
                }

                if (translations.Any(x => x.Translation == translationInfo.Translation && x.LabelName == translationInfo.LabelKey))
                {
                    warningCountTranslation++;
                    continue;
                }

                var labelTranslation = _labelTranslationFactory.CreateEntityFromRequest(translationInfo.Translation, label, language);
                translations.Add(labelTranslation);
            }

            response.CanNotAddedLabelTranslationCount = warningCountTranslation;

            if (labels.Any())
            {
                var uowResult = await _labelUnitOfWork.DoCreateWorkBulk(request.CurrentUserId, labels, translations);
                if (uowResult)
                {
                    response.AddedLabelCount = labels.Count;
                    response.AddedLabelTranslationCount = translations.Count;
                    response.Status = ResponseStatus.Success;
                    return response;
                }
            }

            response.SetFailed();
            return response;
        }

        public async Task<LabelReadResponse> GetLabel(LabelReadRequest request)
        {
            var response = new LabelReadResponse();

            var currentUser = _cacheManager.GetCachedCurrentUser(request.CurrentUserId);

            var label = await _labelRepository.Select(x => x.Uid == request.LabelUid);
            if (label.IsNotExist())
            {
                response.SetInvalidBecauseEntityNotFound();
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

        public async Task<LabelReadListResponse> GetLabels(LabelReadListRequest request)
        {
            var response = new LabelReadListResponse();

            var project = await _projectRepository.Select(x => x.Uid == request.ProjectUid);
            if (project.IsNotExist())
            {
                response.SetInvalidBecauseEntityNotFound();
                return response;
            }

            var currentUser = _cacheManager.GetCachedCurrentUser(request.CurrentUserId);
            if (project.OrganizationId != currentUser.OrganizationId)
            {
                response.SetInvalid();
                return response;
            }

            Expression<Func<Label, bool>> filter = x => x.ProjectId == project.Id;
            Expression<Func<Label, object>> orderByColumn = x => x.Id;
            if (request.SearchTerm.IsNotEmpty())
            {
                filter = x => x.Name.Contains(request.SearchTerm) && x.ProjectId == project.Id;
            }

            List<Label> entities;
            if (request.PagingInfo.Skip < 1)
            {
                entities = await _labelRepository.SelectAfter(filter, request.PagingInfo.LastUid, request.PagingInfo.Take, orderByColumn, request.PagingInfo.IsAscending);
            }
            else
            {
                entities = await _labelRepository.SelectMany(filter, request.PagingInfo.Skip, request.PagingInfo.Take, orderByColumn, request.PagingInfo.IsAscending);
            }

            if (entities != null)
            {
                foreach (var entity in entities)
                {
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

        public async Task<LabelEditResponse> EditLabel(LabelEditRequest request)
        {
            var response = new LabelEditResponse();

            var label = await _labelRepository.Select(x => x.Uid == request.LabelUid);
            if (label.IsNotExist())
            {
                response.SetInvalidBecauseEntityNotFound();
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
                response.SetInvalid();
                return response;
            }

            if (await _labelRepository.Any(x => x.Key == request.LabelKey
                                                && x.ProjectId == label.ProjectId
                                                && x.Uid != request.LabelUid))
            {
                response.ErrorMessages.Add("label_key_already_exist_in_this_project");
                response.Status = ResponseStatus.Failed;
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
                response.SetInvalidBecauseEntityNotFound();
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
                response.SetInvalidBecauseParentNotActive();
                return response;
            }

            if (await _projectRepository.Any(x => x.Id == label.ProjectId && !x.IsActive))
            {
                response.SetInvalidBecauseParentNotActive();
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
                response.SetInvalidBecauseEntityNotFound();
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
                response.SetInvalidBecauseParentNotActive();
                return response;
            }

            if (await _projectRepository.Any(x => x.Id == label.ProjectId && !x.IsActive))
            {
                response.SetInvalidBecauseParentNotActive();
                return response;
            }

            if (await _labelTranslationRepository.Any(x => x.LabelId == label.Id))
            {
                response.SetInvalidForDeleteBecauseHasChildren();
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
                response.SetInvalid();
                return response;
            }

            if (await _organizationRepository.Any(x => x.Id == currentUser.OrganizationId && !x.IsActive))
            {
                response.SetInvalidBecauseParentNotActive();
                return response;
            }

            var label = await _labelRepository.Select(x => x.Uid == request.CloningLabelUid);
            if (label.IsNotExist())
            {
                response.SetInvalidBecauseEntityNotFound();
                return response;
            }

            if (label.OrganizationId != currentUser.OrganizationId)
            {
                response.SetInvalid();
                return response;
            }

            if (await _labelRepository.Any(x => x.Key == request.LabelKey && x.ProjectId == label.ProjectId))
            {
                response.ErrorMessages.Add("label_key_must_be_unique");
                response.Status = ResponseStatus.Failed;
                return response;
            }

            var newLabel = _labelFactory.CreateEntityFromRequest(request, label);
            var uowResult = await _labelUnitOfWork.DoCloneWork(request.CurrentUserId, label.Id, newLabel);
            if (uowResult)
            {
                response.Item = _labelFactory.CreateDtoFromEntity(label);
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
                response.SetInvalidBecauseEntityNotFound();
                return response;
            }

            if (!label.IsActive)
            {
                response.SetInvalidBecauseParentNotActive();
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
                response.SetInvalidBecauseParentNotActive();
                return response;
            }

            if (await _projectRepository.Any(x => x.Id == label.ProjectId && !x.IsActive))
            {
                response.SetInvalidBecauseParentNotActive();
                return response;
            }

            var language = await _languageRepository.Select(x => x.Uid == request.LanguageUid);
            if (language.IsNotExist())
            {
                response.SetInvalidBecauseEntityNotFound();
                return response;
            }

            if (await _labelTranslationRepository.Any(x => x.Translation == request.LabelTranslation && x.LanguageId == language.Id && x.LabelId == label.Id))
            {
                response.ErrorMessages.Add("label_translation_must_be_unique");
                response.Status = ResponseStatus.Failed;
                return response;
            }

            var labelTranslation = _labelTranslationFactory.CreateEntityFromRequest(request.LabelTranslation, label, language);
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
                response.SetInvalidBecauseEntityNotFound();
                return response;
            }

            if (!label.IsActive)
            {
                response.SetInvalidBecauseParentNotActive();
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
                response.SetInvalidBecauseParentNotActive();
                return response;
            }

            if (await _projectRepository.Any(x => x.Id == label.ProjectId && !x.IsActive))
            {
                response.SetInvalidBecauseParentNotActive();
                return response;
            }

            var languages = await _languageRepository.SelectAll(null);

            var translations = new List<LabelTranslation>();
            var warningCount = 0;
            for (var i = 0; i < request.LabelTranslations.Count; i++)
            {
                var translationInfo = request.LabelTranslations[i];
                var language = languages.Find(x => x.IsoCode2Char == translationInfo.LanguageIsoCode2);
                if (language == null)
                {
                    warningCount++;
                    continue;
                }

                if (await _labelTranslationRepository.Any(x => x.Translation == translationInfo.Translation
                                                                    && x.LanguageId == language.Id
                                                                    && x.LabelId == label.Id))
                {
                    warningCount++;
                    continue;
                }

                var labelTranslation = _labelTranslationFactory.CreateEntityFromRequest(translationInfo.Translation, label, language);
                translations.Add(labelTranslation);
            }

            response.CanNotAddedTranslationCount = warningCount;

            if (translations.Any())
            {
                var uowResult = await _labelUnitOfWork.DoCreateTranslationWorkBulk(request.CurrentUserId, translations);
                if (uowResult)
                {
                    response.AddedTranslationCount = translations.Count;
                    response.Status = ResponseStatus.Success;
                    return response;
                }
            }

            response.SetFailed();
            return response;
        }

        public async Task<LabelTranslationReadListResponse> GetTranslation(LabelTranslationReadRequest request)
        {
            var response = new LabelTranslationReadListResponse();

            var currentUser = _cacheManager.GetCachedCurrentUser(request.CurrentUserId);

            var labelTranslation = await _labelTranslationRepository.Select(x => x.Uid == request.LabelTranslationUid);
            if (labelTranslation.IsNotExist())
            {
                response.SetInvalidBecauseEntityNotFound();
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
                response.SetFailed();
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
                response.SetInvalidBecauseEntityNotFound();
                return response;
            }

            var currentUser = _cacheManager.GetCachedCurrentUser(request.CurrentUserId);
            if (label.OrganizationId != currentUser.OrganizationId)
            {
                response.SetInvalid();
                return response;
            }

            Expression<Func<LabelTranslation, bool>> filter = x => x.LabelId == label.Id;
            Expression<Func<LabelTranslation, object>> orderByColumn = x => x.Id;
            if (request.SearchTerm.IsNotEmpty())
            {
                filter = x => x.Name.Contains(request.SearchTerm) && x.ProjectId == label.Id;
            }

            List<LabelTranslation> entities;
            if (request.PagingInfo.Skip < 1)
            {
                entities = await _labelTranslationRepository.SelectAfter(filter, request.PagingInfo.LastUid, request.PagingInfo.Take, orderByColumn, request.PagingInfo.IsAscending);
            }
            else
            {
                entities = await _labelTranslationRepository.SelectMany(filter, request.PagingInfo.Skip, request.PagingInfo.Take, orderByColumn, request.PagingInfo.IsAscending);
            }

            if (entities != null)
            {
                var languages = await _languageRepository.SelectAll(null);

                for (var i = 0; i < entities.Count; i++)
                {
                    var entity = entities[i];
                    var language = languages.Find(x => x.Id == entity.LanguageId);
                    if (language != null)
                    {
                        var dto = _labelTranslationFactory.CreateDtoFromEntity(entity, language);
                        response.Items.Add(dto);
                    }
                    else
                    {
                        response.WarningMessages.Add(entity.LanguageName + " not found!");
                    }
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

        public async Task<LabelTranslationEditResponse> EditTranslation(LabelTranslationEditRequest request)
        {
            var response = new LabelTranslationEditResponse();

            var labelTranslation = await _labelTranslationRepository.Select(x => x.Uid == request.LabelTranslationUid);
            if (labelTranslation.IsNotExist())
            {
                response.SetInvalidBecauseEntityNotFound();
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
                response.SetInvalidBecauseParentNotActive();
                return response;
            }

            if (await _projectRepository.Any(x => x.Id == labelTranslation.ProjectId && !x.IsActive))
            {
                response.SetInvalidBecauseParentNotActive();
                return response;
            }

            if (await _labelRepository.Any(x => x.Id == labelTranslation.LabelId && !x.IsActive))
            {
                response.SetInvalidBecauseParentNotActive();
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
                response.SetInvalidBecauseEntityNotFound();
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
                response.SetInvalidBecauseParentNotActive();
                return response;
            }

            if (await _projectRepository.Any(x => x.Id == labelTranslation.ProjectId && !x.IsActive))
            {
                response.SetInvalidBecauseParentNotActive();
                return response;
            }

            if (await _labelRepository.Any(x => x.Id == labelTranslation.LabelId && !x.IsActive))
            {
                response.SetInvalidBecauseParentNotActive();
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
    }
}