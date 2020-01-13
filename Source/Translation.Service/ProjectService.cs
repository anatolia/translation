using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

using StandardRepository.Helpers;
using StandardRepository.Models;
using StandardUtils.Enumerations;
using StandardUtils.Helpers;
using StandardUtils.Models.DataTransferObjects;

using Translation.Common.Contracts;
using Translation.Common.Models.DataTransferObjects;
using Translation.Common.Models.Requests.Project;
using Translation.Common.Models.Responses.Project;
using Translation.Data.Entities.Domain;
using Translation.Data.Entities.Main;
using Translation.Data.Factories;
using Translation.Data.Repositories.Contracts;
using Translation.Data.UnitOfWorks.Contracts;
using Translation.Service.Managers;
using Language = Translation.Data.Entities.Parameter.Language;

namespace Translation.Service
{
    public class ProjectService : IProjectService
    {
        private readonly CacheManager _cacheManager;
        private readonly IProjectUnitOfWork _projectUnitOfWork;
        private readonly IProjectRepository _projectRepository;
        private readonly ProjectFactory _projectFactory;
        private readonly IOrganizationRepository _organizationRepository;
        private readonly ILanguageRepository _languageRepository;
        private readonly ILabelRepository _labelRepository;
        private readonly LabelFactory _labelFactory;

        public ProjectService(CacheManager cacheManager,
                              IProjectUnitOfWork projectUnitOfWork,
                              IProjectRepository projectRepository, ProjectFactory projectFactory,
                              IOrganizationRepository organizationRepository,
                              ILanguageRepository languageRepository,
                              ILabelRepository labelRepository,
                              LabelFactory labelFactory)
        {
            _cacheManager = cacheManager;
            _projectUnitOfWork = projectUnitOfWork;
            _projectRepository = projectRepository;
            _projectFactory = projectFactory;
            _organizationRepository = organizationRepository;
            _languageRepository = languageRepository;
            _labelRepository = labelRepository;
            _labelFactory = labelFactory;
        }

        public async Task<ProjectReadListResponse> GetProjects(ProjectReadListRequest request)
        {
            var response = new ProjectReadListResponse();

            var currentUser = _cacheManager.GetCachedCurrentUser(request.CurrentUserId);
            
            Expression<Func<Project, bool>> filter = x => x.OrganizationId == currentUser.OrganizationId;

            if (request.PagingInfo.SearchTerm.IsNotEmpty())
            {
                filter = x => x.Name.Contains(request.PagingInfo.SearchTerm) && x.OrganizationId == currentUser.OrganizationId;
            }

            List<Project> entities;
            if (request.PagingInfo.Skip < 1)
            {
                entities = await _projectRepository.SelectAfter(filter, request.PagingInfo.LastUid, request.PagingInfo.Take, false,
                                                                new List<OrderByInfo<Project>>() { new OrderByInfo<Project>(x => x.Uid, request.PagingInfo.IsAscending) });
            }
            else
            {
                entities = await _projectRepository.SelectMany(filter, request.PagingInfo.Skip, request.PagingInfo.Take, false,
                                                               new List<OrderByInfo<Project>>() { new OrderByInfo<Project>(x => x.Id, request.PagingInfo.IsAscending) });
            }

            if (entities != null)
            {
                for (var i = 0; i < entities.Count; i++)
                {
                    var entity = entities[i];
                    var dto = _projectFactory.CreateDtoFromEntity(entity);
                    response.Items.Add(dto);
                }
            }

            response.PagingInfo.Skip = request.PagingInfo.Skip;
            response.PagingInfo.Take = request.PagingInfo.Take;
            response.PagingInfo.LastUid = request.PagingInfo.LastUid;
            response.PagingInfo.IsAscending = request.PagingInfo.IsAscending;
            response.PagingInfo.TotalItemCount = await _projectRepository.Count(filter);

            response.Status = ResponseStatus.Success;
            return response;
        }

        public async Task<ProjectRevisionReadListResponse> GetProjectRevisions(ProjectRevisionReadListRequest request)
        {
            var response = new ProjectRevisionReadListResponse();

            var project = await _projectRepository.Select(x => x.Uid == request.ProjectUid);
            if (project.IsNotExist())
            {
                response.SetFailedBecauseNotFound(nameof(Project));
                return response;
            }

            var revisions = await _projectRepository.SelectRevisions(project.Id);

            for (var i = 0; i < revisions.Count; i++)
            {
                var revision = revisions[i];

                var revisionDto = new RevisionDto<ProjectDto>();
                revisionDto.Revision = revision.Revision;
                revisionDto.RevisionedAt = revision.RevisionedAt;

                var user = _cacheManager.GetCachedUser(revision.RevisionedBy);
                revisionDto.RevisionedByUid = user.Uid;
                revisionDto.RevisionedByName = user.Name;

                revisionDto.Item = _projectFactory.CreateDtoFromEntity(revision.Entity);

                response.Items.Add(revisionDto);
            }

            response.Status = ResponseStatus.Success;
            return response;
        }

        public async Task<ProjectReadResponse> GetProject(ProjectReadRequest request)
        {
            var response = new ProjectReadResponse();

            var currentUser = _cacheManager.GetCachedCurrentUser(request.CurrentUserId);

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

            response.Item = _projectFactory.CreateDtoFromEntity(project);
            response.Status = ResponseStatus.Success;
            return response;
        }

        public async Task<ProjectReadBySlugResponse> GetProjectBySlug(ProjectReadBySlugRequest request)
        {
            var response = new ProjectReadBySlugResponse();

            var currentUser = _cacheManager.GetCachedCurrentUser(request.CurrentUserId);

            var project = await _projectRepository.Select(x => x.Slug == request.ProjectSlug);
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

            response.Item = _projectFactory.CreateDtoFromEntity(project);
            response.Status = ResponseStatus.Success;
            return response;
        }

        public async Task<ProjectCreateResponse> CreateProject(ProjectCreateRequest request)
        {
            var response = new ProjectCreateResponse();

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

            if (await _projectRepository.Any(x => (x.Name == request.ProjectName
                                                  || x.Slug == request.ProjectSlug)
                                                  && x.OrganizationId == currentUser.OrganizationId))
            {
                response.SetFailed();
                response.ErrorMessages.Add("project_name_and_slug_must_be_unique");
                return response;
            }

            var language = await _languageRepository.Select(x => x.Uid == request.LanguageUid);
            if (language.IsNotExist())
            {
                response.SetFailedBecauseNotFound(nameof(Language));
                return response;
            }

            var entity = _projectFactory.CreateEntityFromRequest(request, currentUser.Organization, language);
            var uowResult = await _projectUnitOfWork.DoCreateWork(request.CurrentUserId, entity);
            if (uowResult)
            {
                response.Item = _projectFactory.CreateDtoFromEntity(entity);
                response.Status = ResponseStatus.Success;
                return response;
            }

            response.SetFailed();
            return response;
        }

        public async Task<ProjectEditResponse> EditProject(ProjectEditRequest request)
        {
            var response = new ProjectEditResponse();

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

            if (project.OrganizationId != currentUser.OrganizationId)
            {
                response.SetInvalid();
                return response;
            }

            if (project.Name == request.ProjectName && project.Slug == request.ProjectSlug
                                                    && project.Description == request.Description
                                                    && project.LanguageUid == request.LanguageUid)
            {
                response.Item = _projectFactory.CreateDtoFromEntity(project);
                response.Status = ResponseStatus.Success;
                return response;
            }


            if (await _projectRepository.Any(x => x.Name == request.ProjectName
                                                       && x.OrganizationId == project.OrganizationId
                                                       && x.Id != project.Id))
            {
                response.SetInvalidBecauseMustBeUnique(nameof(Project));
                return response;
            }

            var language = await _languageRepository.Select(x => x.Uid == request.LanguageUid);
            if (language.IsNotExist())
            {
                response.SetFailedBecauseNotFound(nameof(Language));
                return response;
            }

            var updatedEntity = _projectFactory.CreateEntityFromRequest(request, project, language);
            var result = await _projectRepository.Update(request.CurrentUserId, updatedEntity);
            if (result)
            {
                response.Item = _projectFactory.CreateDtoFromEntity(updatedEntity);
                response.Status = ResponseStatus.Success;
                return response;
            }

            response.SetFailed();
            return response;
        }

        public async Task<ProjectDeleteResponse> DeleteProject(ProjectDeleteRequest request)
        {
            var response = new ProjectDeleteResponse();

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

            if (await _labelRepository.Any(x => x.ProjectId == project.Id))
            {
                response.SetInvalidBecauseHasChildren(nameof(Project));
                return response;
            }

            var uowResult = await _projectUnitOfWork.DoDeleteWork(request.CurrentUserId, project);
            if (uowResult)
            {
                response.Status = ResponseStatus.Success;
                return response;
            }

            response.SetFailed();
            return response;
        }

        public async Task<ProjectCloneResponse> CloneProject(ProjectCloneRequest request)
        {
            var response = new ProjectCloneResponse();

            var currentUser = _cacheManager.GetCachedCurrentUser(request.CurrentUserId);
            if (!currentUser.IsAdmin)
            {
                response.SetInvalidBecauseNotAdmin(nameof(User));
                return response;
            }

            var project = await _projectRepository.Select(x => x.Uid == request.CloningProjectUid);
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

            if (await _projectRepository.IsProjectNameMustBeUnique(request.Name, currentUser.OrganizationId))
            {
                response.SetInvalidBecauseMustBeUnique(nameof(Project));
                return response;
            }

            if (await _projectRepository.Any(x => x.Slug == request.Slug
                                                  && x.OrganizationId == currentUser.OrganizationId))
            {
                response.SetInvalidBecauseSlugMustBeUnique(nameof(Project));
                return response;
            }

            var language = await _languageRepository.Select(x => x.Uid == request.LanguageUid);
            if (language.IsNotExist())
            {
                response.SetFailedBecauseNotFound(nameof(Language));
                return response;
            }

            var cloningEntity = _projectFactory.CreateEntityFromRequest(request, project, language);
            var uowResult = await _projectUnitOfWork.DoCloneWork(request.CurrentUserId, project.Id, cloningEntity);
            if (uowResult)
            {
                response.Item = _projectFactory.CreateDtoFromEntity(cloningEntity);
                response.Status = ResponseStatus.Success;
                return response;
            }

            response.SetFailed();
            return response;
        }

        public async Task<ProjectChangeActivationResponse> ChangeActivationForProject(ProjectChangeActivationRequest request)
        {
            var response = new ProjectChangeActivationResponse();

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
                response.SetInvalid();
                return response;
            }

            var updatedEntity = _projectFactory.UpdateEntityForChangeActivation(project);
            var result = await _projectRepository.Update(request.CurrentUserId, updatedEntity);
            if (result)
            {
                response.Item = _projectFactory.CreateDtoFromEntity(updatedEntity);
                response.Status = ResponseStatus.Success;
                return response;
            }

            response.SetFailed();
            return response;
        }

        public async Task<ProjectRestoreResponse> RestoreProject(ProjectRestoreRequest request)
        {
            var response = new ProjectRestoreResponse();

            var currentUser = _cacheManager.GetCachedCurrentUser(request.CurrentUserId);
            if (await _organizationRepository.Any(x => x.Id == currentUser.OrganizationId && !x.IsActive))
            {
                response.SetInvalid();
                return response;
            }

            var project = await _projectRepository.Select(x => x.Uid == request.ProjectUid);
            if (project.IsNotExist())
            {
                response.SetFailedBecauseNotFound(nameof(Project));
                return response;
            }

            var revisions = await _projectRepository.SelectRevisions(project.Id);
            if (revisions.All(x => x.Revision != request.Revision))
            {
                response.SetFailedBecauseRevisionNotFound(nameof(Project));
                return response;
            }

            var result = await _projectRepository.RestoreRevision(request.CurrentUserId, project.Id, request.Revision);
            if (result)
            {
                response.Status = ResponseStatus.Success;
                return response;
            }

            response.SetFailed();
            return response;
        }

        public async Task<ProjectPendingTranslationReadListResponse> GetPendingTranslations(ProjectPendingTranslationReadListRequest request)
        {
            var response = new ProjectPendingTranslationReadListResponse();

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

            Expression<Func<Label, bool>> filter = x => x.ProjectId == project.Id
                                                        && x.LabelTranslationCount == 0;

            if (request.PagingInfo.SearchTerm.IsNotEmpty())
            {
                filter = x => x.Name.Contains(request.PagingInfo.SearchTerm)
                              && x.ProjectId == project.Id
                              && x.LabelTranslationCount == 0;
            }

            List<Label> entities;
            if (request.PagingInfo.Skip < 1)
            {
                entities = await _labelRepository.SelectAfter(filter, request.PagingInfo.LastUid, request.PagingInfo.Take, false,
                                                              new List<OrderByInfo<Label>>() { new OrderByInfo<Label>(x => x.Uid, request.PagingInfo.IsAscending) });
            }
            else
            {
                entities = await _labelRepository.SelectMany(filter, request.PagingInfo.Skip, request.PagingInfo.Take, false,
                                                             new List<OrderByInfo<Label>>() { new OrderByInfo<Label>(x => x.Id, request.PagingInfo.IsAscending) });
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
    }
}