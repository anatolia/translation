using System;
using System.IO;
using Microsoft.AspNetCore.Http;

using Translation.Client.Web.Models.Base;
using Translation.Client.Web.Models.InputModels;
using Translation.Common.Helpers;

namespace Translation.Client.Web.Models.Label
{
    public sealed class LabelUploadFromCSVModel : BaseModel
    {
        public Guid OrganizationUid { get; set; }

        public Guid ProjectUid { get; set; }
        public string ProjectName { get; set; }
        public IFormFile CSVFile { get; set; }

        public HiddenInputModel OrganizationInput { get; }
        public HiddenInputModel ProjectNameInput { get; }

        public HiddenInputModel ProjectInput { get; }
        public FileInputModel CSVFileInput { get; }

        public LabelUploadFromCSVModel()
        {
            Title = "upload_labels_from_csv_file_title";

            OrganizationInput = new HiddenInputModel("OrganizationUid");
            ProjectInput = new HiddenInputModel("ProjectUid");
            ProjectNameInput = new HiddenInputModel("ProjectName");

            CSVFileInput = new FileInputModel("CSVFile", "csv_file", true);
        }

        public override void SetInputModelValues()
        {
            OrganizationInput.Value = OrganizationUid.ToUidString();
            ProjectInput.Value = ProjectUid.ToUidString();
            ProjectNameInput.Value = ProjectName;
            InfoMessages.Clear();
            InfoMessages.Add("this_page_imports_your_label_file_in_csv_format");
            InfoMessages.Add("the_file_must_be_UTF-8_encoded");
            InfoMessages.Add("you_can_download_the_sample_csv_file_below");
            InfoMessages.Add("added_if_there_is_no_label_during_import");
            InfoMessages.Add("only_the_label_translation_is_added_if_there_is_a_label_but_no_translation_in_the_import_phase");
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
