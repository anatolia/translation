using System;

using Translation.Client.Web.Models.Base;
using Translation.Client.Web.Models.InputModels;
using Translation.Common.Helpers;

namespace Translation.Client.Web.Models.Label
{
    public sealed class CreateBulkLabelModel : BaseModel
    {
        public Guid OrganizationUid { get; set; }

        public Guid ProjectUid { get; set; }
        public string ProjectName { get; set; }
        public string BulkLabelData { get; set; }

        public HiddenInputModel OrganizationUidInput { get; }
        public HiddenInputModel ProjectNameInput { get; }

        public HiddenInputModel ProjectUidInput { get; }
        public TextareaInputModel BulkLabelInput { get; }

        public CreateBulkLabelModel()
        {
            Title = "create_bulk_label_title";

            OrganizationUidInput = new HiddenInputModel("OrganizationUid");
            ProjectUidInput = new HiddenInputModel("ProjectUid");
            ProjectNameInput = new HiddenInputModel("ProjectName");

            BulkLabelInput = new TextareaInputModel("BulkLabelData", "bulk_label_data");
            BulkLabelInput.Rows = 21;
        }

        public override void SetInputModelValues()
        {
            OrganizationUidInput.Value = OrganizationUid.ToUidString();

            ProjectUidInput.Value = ProjectUid.ToUidString();
            ProjectNameInput.Value = ProjectName;

            BulkLabelInput.Value = BulkLabelData;
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

            BulkLabelData = BulkLabelData.TrimOrDefault();
            if (BulkLabelData.IsEmpty())
            {
                BulkLabelInput.ErrorMessage.Add("bulk_label_data_required_error_message");
                InputErrorMessages.AddRange(BulkLabelInput.ErrorMessage);
            }
        }
    }
}
