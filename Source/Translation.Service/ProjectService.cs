using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using StandardRepository.Helpers;
using Translation.Common.Contracts;
using Translation.Common.Enumerations;
using Translation.Common.Helpers;
using Translation.Common.Models.Requests.Project;
using Translation.Common.Models.Responses.Project;
using Translation.Data.Entities.Domain;
using Translation.Data.Factories;
using Translation.Data.Repositories.Contracts;
using Translation.Data.UnitOfWorks.Contracts;
using Translation.Service.Managers;

namespace Translation.Service
{
    public class ProjectService : IProjectService
    {
        private readonly CacheManager _cacheManager;
        private readonly IProjectUnitOfWork _projectUnitOfWork;
        private readonly IProjectRepository _projectRepository;
        private readonly ProjectFactory _projectFactory;
        private readonly IOrganizationRepository _organizationRepository;
        private readonly ILabelRepository _labelRepository;

        public ProjectService(CacheManager cacheManager,
                              IProjectUnitOfWork projectUnitOfWork,
                              IProjectRepository projectRepository, ProjectFactory projectFactory,
                              IOrganizationRepository organizationRepository,
                              ILabelRepository labelRepository)
        {
            _cacheManager = cacheManager;
            _projectUnitOfWork = projectUnitOfWork;
            _projectRepository = projectRepository;
            _projectFactory = projectFactory;
            _organizationRepository = organizationRepository;
            _labelRepository = labelRepository;
        }

        public async Task<ProjectReadListResponse> GetProjects(ProjectReadListRequest request)
        {
            var response = new ProjectReadListResponse();

            var currentUser = _cacheManager.GetCachedCurrentUser(request.CurrentUserId);
            if (request.OrganizationUid != currentUser.OrganizationUid)
            {
                response.SetInvalid();
                return response;
            }

            Expression<Func<Project, bool>> filter = x => x.OrganizationId == currentUser.OrganizationId;
            Expression<Func<Project, object>> orderByColumn = x => x.Id;
            if (request.SearchTerm.IsNotEmpty())
            {
                filter = x => x.Name.Contains(request.SearchTerm) && x.OrganizationId == currentUser.OrganizationId;
            }

            List<Project> entities;
            if (request.PagingInfo.Skip < 1)
            {
                entities = await _projectRepository.SelectAfter(filter, request.PagingInfo.LastUid, request.PagingInfo.Take, orderByColumn, request.PagingInfo.IsAscending);
            }
            else
            {
                entities = await _projectRepository.SelectMany(filter, request.PagingInfo.Skip, request.PagingInfo.Take, orderByColumn, request.PagingInfo.IsAscending);
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

        public async Task<ProjectReadResponse> GetProject(ProjectReadRequest request)
        {
            var response = new ProjectReadResponse();

            var currentUser = _cacheManager.GetCachedCurrentUser(request.CurrentUserId);

            var project = await _projectRepository.Select(x => x.Uid == request.ProjectUid);
            if (project.IsNotExist())
            {
                response.SetInvalidBecauseEntityNotFound();
                return response;
            }

            if (project.OrganizationId != currentUser.OrganizationId)
            {
                response.SetFailed();
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
                response.SetInvalid();
                return response;
            }

            if (await _organizationRepository.Any(x => x.Id == currentUser.OrganizationId && !x.IsActive))
            {
                response.SetInvalidBecauseParentNotActive();
                return response;
            }
            
            if (await _projectRepository.Any(x => x.Name == request.ProjectName
                                                       && x.OrganizationId == currentUser.OrganizationId))
            {
                response.ErrorMessages.Add("project_name_must_be_unique");
                response.Status = ResponseStatus.Failed;
                return response;
            }

            var entity = _projectFactory.CreateEntityFromRequest(request, currentUser.Organization);
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
                response.SetInvalid();
                return response;
            }

            if (await _organizationRepository.Any(x => x.Id == currentUser.OrganizationId && !x.IsActive))
            {
                response.SetInvalidBecauseParentNotActive();
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
                response.SetFailed();
                return response;
            }

            if (await _projectRepository.Any(x => x.Name == request.ProjectName
                                                       && x.OrganizationId == project.OrganizationId
                                                       && x.Id != project.Id))
            {
                response.ErrorMessages.Add("project_name_must_be_unique");
                response.Status = ResponseStatus.Failed;
                return response;
            }

            var updatedEntity = _projectFactory.CreateEntityFromRequest(request, project);
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
                response.SetFailed();
                return response;
            }

            if (await _organizationRepository.Any(x => x.Id == project.OrganizationId && !x.IsActive))
            {
                response.SetInvalid();
                return response;
            }

            if (await _labelRepository.Any(x => x.ProjectId == project.Id))
            {
                response.SetInvalidForDeleteBecauseHasChildren();
                return response;
            }

            var result = await _projectRepository.Delete(request.CurrentUserId, project.Id);
            if (result)
            {
                response.Status = ResponseStatus.Success;
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
                response.SetInvalid();
                return response;
            }
            
            var project = await _projectRepository.Select(x => x.Uid == request.CloningProjectUid);
            if (project.IsNotExist())
            {
                response.SetInvalidBecauseEntityNotFound();
                return response;
            }

            if (project.OrganizationId != currentUser.OrganizationId)
            {
                response.SetFailed();
                return response;
            }

            if (await _organizationRepository.Any(x => x.Id == project.OrganizationId && !x.IsActive))
            {
                response.SetInvalid();
                return response;
            }

            var cloningEntity = _projectFactory.CreateEntityFromRequest(request, project);
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
                response.SetFailed();
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
                response.SetInvalidBecauseEntityNotFound();
                response.InfoMessages.Add("project_not_found");
                return response;
            }

            var revisions = await _projectRepository.SelectRevisions(project.Id);
            if (revisions.All(x => x.Revision != request.Revision))
            {
                response.SetInvalidBecauseEntityNotFound();
                response.InfoMessages.Add("revision_not_found");
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
    }
}