using Translation.Common.Models.DataTransferObjects;
using Translation.Common.Models.Requests.Project;
using Translation.Common.Models.Shared;
using Translation.Data.Entities.Domain;
using Translation.Data.Entities.Main;
using Translation.Data.Entities.Parameter;

namespace Translation.Data.Factories
{
    public class ProjectFactory
    {
        public Project CreateEntityFromRequest(ProjectEditRequest request, Project entity, Language language)
        {
            entity.Name = request.ProjectName;
            entity.Slug = request.ProjectSlug;

            entity.Description = request.Description;
            entity.Url = request.Url;

            entity.LanguageId = language.Id;
            entity.LanguageUid = language.Uid;
            entity.LanguageName = language.Name;
            entity.LanguageIconUrl = language.IconUrl;

            return entity;
        }

        public Project CreateEntityFromRequest(ProjectCreateRequest request, CurrentOrganization organization, Language language)
        {
            var entity = new Project();
            entity.OrganizationId = organization.Id;
            entity.OrganizationUid = organization.Uid;
            entity.OrganizationName = organization.Name;

            entity.Name = request.ProjectName;
            entity.Slug = request.ProjectSlug;

            entity.Description = request.Description;
            entity.Url = request.Url;
            entity.IsActive = true;

            entity.LanguageId = language.Id;
            entity.LanguageUid = language.Uid;
            entity.LanguageName = language.Name;
            entity.LanguageIconUrl = language.IconUrl;

            return entity;
        }

        public Project CreateEntityFromRequest(ProjectCloneRequest request, Project cloningProject, Language language)
        {
            var entity = new Project();
            entity.OrganizationId = cloningProject.OrganizationId;
            entity.OrganizationUid = cloningProject.OrganizationUid;
            entity.OrganizationName = cloningProject.OrganizationName;

            entity.Name = request.Name;
            entity.Slug = request.Slug;

            entity.LabelCount = cloningProject.LabelCount;
            entity.LabelTranslationCount = cloningProject.LabelTranslationCount;
            entity.IsSuperProject = cloningProject.IsSuperProject;

            entity.Description = request.Description;
            entity.Url = request.Url;
            entity.IsActive = true;

            entity.LanguageId = language.Id;
            entity.LanguageUid = language.Uid;
            entity.LanguageName = language.Name;
            entity.LanguageIconUrl = language.IconUrl;

            return entity;
        }

        public ProjectDto CreateDtoFromEntity(Project entity)
        {
            var dto = new ProjectDto();
            dto.OrganizationUid = entity.OrganizationUid;
            dto.OrganizationName = entity.OrganizationName;

            dto.Uid = entity.Uid;
            dto.Name = entity.Name;
            dto.Slug = entity.Slug;

            dto.LabelCount = entity.LabelCount;

            dto.Url = entity.Url;
            dto.Description = entity.Description;
            dto.IsActive = entity.IsActive;

            dto.CreatedAt = entity.CreatedAt;
            dto.UpdatedAt = entity.UpdatedAt;

            dto.LanguageId = entity.LanguageId;
            dto.LanguageUid = entity.LanguageUid;
            dto.LanguageName = entity.LanguageName;
            dto.LanguageIconUrl = entity.LanguageIconUrl;

            return dto;
        }

        public Project UpdateEntityForChangeActivation(Project entity)
        {
            entity.IsActive = !entity.IsActive;
            return entity;
        }

        public Project CreateDefault(Organization organization, Language language)
        {
            var entity = new Project();
            entity.OrganizationId = organization.Id;
            entity.OrganizationUid = organization.Uid;
            entity.OrganizationName = organization.Name;
            entity.Name = "Default";
            entity.Slug = "default";
            entity.IsActive = true;

            entity.LanguageId = language.Id;
            entity.LanguageUid = language.Uid;
            entity.LanguageName = language.Name;
            entity.LanguageIconUrl = language.IconUrl;

            return entity;
        }
    }
}