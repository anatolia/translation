using Translation.Common.Models.DataTransferObjects;
using Translation.Common.Models.Requests.Project;
using Translation.Common.Models.Shared;
using Translation.Data.Entities.Domain;
using Translation.Data.Entities.Main;

namespace Translation.Data.Factories
{
    public class ProjectFactory
    {
        public Project CreateEntityFromRequest(ProjectEditRequest request, Project entity)
        {
            entity.Name = request.ProjectName;

            entity.Description = request.Description;
            entity.Url = request.Url;

            return entity;
        }

        public Project CreateEntityFromRequest(ProjectCreateRequest request, Organization organization)
        {
            var entity = new Project();
            entity.OrganizationId = organization.Id;
            entity.OrganizationUid = organization.Uid;
            entity.OrganizationName = organization.Name;

            entity.Name = request.ProjectName;

            entity.Description = request.Description;
            entity.Url = request.Url;
            entity.IsActive = true;

            return entity;
        }

        public Project CreateEntityFromRequest(ProjectCreateRequest request, CurrentOrganization organization)
        {
            var entity = new Project();
            entity.OrganizationId = organization.Id;
            entity.OrganizationUid = organization.Uid;
            entity.OrganizationName = organization.Name;

            entity.Name = request.ProjectName;

            entity.Description = request.Description;
            entity.Url = request.Url;
            entity.IsActive = true;

            return entity;
        }

        public Project CreateEntityFromRequest(ProjectCloneRequest request, Project cloningProject)
        {
            var entity = new Project();
            entity.OrganizationId = cloningProject.OrganizationId;
            entity.OrganizationUid = cloningProject.OrganizationUid;
            entity.OrganizationName = cloningProject.OrganizationName;

            entity.Name = request.Name;

            entity.LabelCount = cloningProject.LabelCount;
            entity.LabelTranslationCount = cloningProject.LabelTranslationCount;
            entity.IsSuperProject = cloningProject.IsSuperProject;

            entity.Description = request.Description;
            entity.Url = request.Url;
            entity.IsActive = true;

            return entity;
        }

        public ProjectDto CreateDtoFromEntity(Project entity)
        {
            var dto = new ProjectDto();
            dto.OrganizationUid = entity.OrganizationUid;
            dto.OrganizationName = entity.OrganizationName;

            dto.Uid = entity.Uid;
            dto.Name = entity.Name;

            dto.LabelCount = entity.LabelCount;

            dto.Url = entity.Url;
            dto.Description = entity.Description;
            dto.IsActive = entity.IsActive;

            dto.CreatedAt = entity.CreatedAt;
            dto.UpdatedAt = entity.UpdatedAt;

            return dto;
        }

        public Project UpdateEntityForChangeActivation(Project entity)
        {
            entity.IsActive = !entity.IsActive;
            return entity;
        }

        public Project CreateDefault(Organization organization)
        {
            var entity = new Project();
            entity.OrganizationId = organization.Id;
            entity.OrganizationUid = organization.Uid;
            entity.OrganizationName = organization.Name;

            entity.Name = "Default";

            entity.IsActive = true;

            return entity;
        }
    }
}