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
