using System;
using Microsoft.AspNetCore.Http;

using Translation.Client.Web.Models.Base;
using Translation.Client.Web.Models.InputModels;
using Translation.Common.Helpers;

namespace Translation.Client.Web.Models.LabelTranslation
{
    public sealed class UploadLabelTranslationFromCSVFileModel : BaseModel
    {
        public Guid OrganizationUid { get; set; }
        public Guid LabelUid { get; set; }
        public string LabelKey { get; set; }

        public IFormFile CSVFile { get; set; }

        public HiddenInputModel OrganizationInput { get; }
        public HiddenInputModel LabelInput { get; }
        public HiddenInputModel LabelKeyInput { get; }

        public FileInputModel CSVFileInput { get; }

        public UploadLabelTranslationFromCSVFileModel()
        {
            Title = "upload_labels_from_csv_file_title";

            OrganizationInput = new HiddenInputModel("OrganizationUid");
            LabelInput = new HiddenInputModel("LabelUid");
            LabelKeyInput = new HiddenInputModel("LabelKey");

            CSVFileInput = new FileInputModel("CSVFile", "csv_file", true);
        }

        public override void SetInputModelValues()
        {
            OrganizationInput.Value = OrganizationUid.ToUidString();

            LabelInput.Value = LabelUid.ToUidString();
            LabelKeyInput.Value = LabelKey;
        }

        public override void SetInputErrorMessages()
        {
            if (OrganizationUid.IsEmptyGuid())
            {
                ErrorMessages.Add("organization_uid_not_valid");
            }

            if (LabelUid.IsEmptyGuid())
            {
                ErrorMessages.Add("label_uid_not_valid");
            }

            LabelKey = LabelKey.TrimOrDefault();
            if (LabelKey.IsEmpty())
            {
                ErrorMessages.Add("label_key_not_valid");
            }

            if (CSVFile == null
                || CSVFile.Length < 10)
            {
                CSVFileInput.ErrorMessage.Add("csv_required_error_message");
                InputErrorMessages.AddRange(CSVFileInput.ErrorMessage);
            }
        }
    }
}
