using System;

using Translation.Client.Web.Models.Project;
using Translation.Common.Models.DataTransferObjects;

namespace Translation.Client.Web.Helpers.Mappers
{
    public static class ProjectMapper
    {
        public static ProjectCreateModel MapProjectCreateModel(Guid organizationUid)
        {
            var model = new ProjectCreateModel();
            model.OrganizationUid = organizationUid;

            model.SetInputModelValues();

            return model;
        }

        public static ProjectDetailModel MapProjectDetailModel(ProjectDto dto)
        {
            var model = new ProjectDetailModel();
            model.OrganizationUid = dto.OrganizationUid;
            model.OrganizationName = dto.OrganizationName;

            model.ProjectUid = dto.Uid;
            model.Name = dto.Name;
            model.Slug = dto.Slug;
            model.Description = dto.Description;
            model.Url = dto.Url;
            model.LabelCount = dto.LabelCount;
            model.IsActive = dto.IsActive;
            model.IsActiveInput.Value = dto.IsActive;

            model.SetInputModelValues();
            return model;
        }

        public static ProjectEditModel MapProjectEditModel(ProjectDto dto)
        {
            var model = new ProjectEditModel();

            model.OrganizationUid = dto.OrganizationUid;

            model.ProjectUid = dto.Uid;
            model.Name = dto.Name;
            model.Slug = dto.Slug;
            model.Description = dto.Description;
            model.Url = dto.Url;

            model.SetInputModelValues();
            return model;
        }

        public static ProjectCloneModel MapProjectCloneModel(ProjectDto dto)
        {
            var model = new ProjectCloneModel();
            model.OrganizationUid = dto.OrganizationUid;
            model.CloningProjectUid = dto.Uid;
            model.Name = dto.Name;

            model.Url = dto.Url;
            model.Description = dto.Description;

            model.LabelCount = dto.LabelCount;
            model.LabelTranslationCount = dto.LabelTranslationCount;
            model.IsSuperProject = dto.IsSuperProject;

            model.SetInputModelValues();
            return model;
        }
    }
}