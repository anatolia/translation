using System;
using System.IO;

using Microsoft.AspNetCore.Http;

using StandardUtils.Helpers;

using Translation.Client.Web.Models.Base;
using Translation.Client.Web.Models.InputModels;

namespace Translation.Client.Web.Models.Label
{
    public sealed class LabelUploadFromCSVModel : BaseModel
    {
        public Guid OrganizationUid { get; set; }
        public Guid ProjectUid { get; set; }
        public string ProjectName { get; set; }

        public IFormFile CSVFile { get; set; }
        public bool UpdateExistedTranslations { get; set; }

        public HiddenInputModel OrganizationInput { get; }
        public HiddenInputModel ProjectNameInput { get; }
        public HiddenInputModel ProjectInput { get; }

        public FileInputModel CSVFileInput { get; }
        public CheckboxInputModel UpdateExistedTranslationsInput { get; set; }

        public LabelUploadFromCSVModel()
        {
            Title = "upload_labels_from_csv_file_title";

            OrganizationInput = new HiddenInputModel("OrganizationUid");
            ProjectInput = new HiddenInputModel("ProjectUid");
            ProjectNameInput = new HiddenInputModel("ProjectName");

            CSVFileInput = new FileInputModel("CSVFile", "csv_file", true);
            UpdateExistedTranslationsInput = new CheckboxInputModel("UpdateExistedTranslations", "update_existed_translations");
        }

        public override void SetInputModelValues()
        {
            OrganizationInput.Value = OrganizationUid.ToUidString();
            ProjectInput.Value = ProjectUid.ToUidString();
            ProjectNameInput.Value = ProjectName;
            UpdateExistedTranslationsInput.Value = UpdateExistedTranslations;

            InfoMessages.Clear();
            InfoMessages.Add("the_file_must_be_utf8_encoded");
            InfoMessages.Add("you_cannot_add_labels_and_same_label_translations_that_have_same_label_key");
            InfoMessages.Add("you_update_label_translation_previously_added_that_have_same_label_key");
            InfoMessages.Add("if_you_add_multiple_translation_for_same_language_accepts_the_first_one");
        }

        public override void SetInputErrorMessages()
        {
            if (OrganizationUid.IsEmptyGuid())
            {
                ErrorMessages.Add("organization_uid_not_valid");
            }

            if (ProjectUid.IsEmptyGuid())
            {
                ErrorMessages.Add("project_uid_not_valid");
            }

            ProjectName = ProjectName.TrimOrDefault();
            if (ProjectName.IsEmpty())
            {
                ErrorMessages.Add("project_name_required");
            }

            if (CSVFile == null
                || CSVFile.Length == 0)
            {
                CSVFileInput.ErrorMessage.Add("csv_required_error_message");
                InputErrorMessages.AddRange(CSVFileInput.ErrorMessage);
            }

            if (CSVFile != null
                && Path.GetExtension(CSVFile.FileName) != ".csv")
            {
                CSVFileInput.ErrorMessage.Add("file_is_not_csv_error_message");
                InputErrorMessages.AddRange(CSVFileInput.ErrorMessage);
            }
        }
    }
}
