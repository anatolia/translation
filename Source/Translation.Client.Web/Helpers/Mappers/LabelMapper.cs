using System;

using Translation.Client.Web.Models.Label;
using Translation.Client.Web.Models.LabelTranslation;
using Translation.Common.Models.DataTransferObjects;

namespace Translation.Client.Web.Helpers.Mappers
{
    public class LabelMapper
    {
        public static LabelCreateModel MapLabelCreateModel(Guid organizationUid, Guid projectUid)
        {
            var model = new LabelCreateModel();
            model.OrganizationUid = organizationUid;
            model.ProjectUid = projectUid;

            model.SetInputModelValues();
            return model;
        }

        public static LabelDetailModel MapLabelDetailModel(LabelDto dto)
        {
            var model = new LabelDetailModel();
            model.OrganizationUid = dto.OrganizationUid;
            model.OrganizationName = dto.OrganizationName;

            model.ProjectUid = dto.ProjectUid;
            model.ProjectName = dto.ProjectName;

            model.LabelUid = dto.Uid;
            model.Key = dto.Key;
            model.Description = dto.Description;
            model.IsActive = dto.IsActive;
            model.IsActiveInput.Value = dto.IsActive;

            model.SetInputModelValues();
            return model;
        }

        public static LabelEditModel MapLabelEditModel(LabelDto dto)
        {
            var model = new LabelEditModel();
            model.OrganizationUid = dto.OrganizationUid;

            model.LabelUid = dto.Uid;
            model.Key = dto.Key;
            model.Description = dto.Description;

            model.SetInputModelValues();
            return model;
        }

        public static LabelCloneModel MapLabelCloneModel(LabelDto dto)
        {
            var model = new LabelCloneModel();
            model.OrganizationUid = dto.OrganizationUid;
            model.CloningLabelUid = dto.Uid;
            model.CloningLabelKey = dto.Key;
            model.CloningLabelDescription = dto.Description;

            model.SetInputModelValues();
            return model;
        }

        public static LabelUploadFromCSVModel MapLabelUploadFromCSVModel(ProjectDto project)
        {
            var model = new LabelUploadFromCSVModel();
            model.OrganizationUid = project.OrganizationUid;
            model.ProjectUid = project.Uid;
            model.ProjectName = project.Name;

            model.SetInputModelValues();
            return model;
        }

        public static UploadLabelTranslationFromCSVFileModel MapUploadLabelTranslationFromCSVFileModel(LabelDto label)
        {
            var model = new UploadLabelTranslationFromCSVFileModel();
            model.OrganizationUid = label.OrganizationUid;
            model.LabelUid = label.Uid;
            model.LabelKey= label.Key;

            model.SetInputModelValues();
            return model;
        }

        public static LabelTranslationCreateModel MapLabelTranslationCreateModel(LabelDto label)
        {
            var model = new LabelTranslationCreateModel();
            model.OrganizationUid = label.OrganizationUid;

            model.ProjectUid = label.ProjectUid;
            model.ProjectName = label.ProjectName;

            model.LabelUid = label.Uid;
            model.LabelKey= label.Key;

            model.SetInputModelValues();
            return model;
        }

        public static LabelTranslationEditModel MapLabelTranslationEditModel(LabelTranslationDto dto)
        {
            var model = new LabelTranslationEditModel();
            model.LabelTranslationUid = dto.Uid;
            model.Translation = dto.Translation;
            model.TranslationInput.Value = dto.Translation;
            model.LabelKey = dto.LabelKey;
            model.LanguageName = dto.LanguageName;
            model.LanguageIconUrl = dto.LanguageIconUrl;

            model.OrganizationUid = dto.OrganizationUid;
            model.SetInputModelValues();

            return model;
        }
    }
}