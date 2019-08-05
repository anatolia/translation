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
        public bool UpdateExistedTranslations { get; set; }

        public HiddenInputModel OrganizationInput { get; }
        public HiddenInputModel ProjectNameInput { get; }

        public HiddenInputModel ProjectInput { get; }
        public TextareaInputModel BulkLabelInput { get; }
        public CheckboxInputModel UpdateExistedTranslationsInput { get; set; }

        public CreateBulkLabelModel()
        {
            Title = "create_bulk_label_title";

            OrganizationInput = new HiddenInputModel("OrganizationUid");
            ProjectInput = new HiddenInputModel("ProjectUid");
            ProjectNameInput = new HiddenInputModel("ProjectName");

            BulkLabelInput = new TextareaInputModel("BulkLabelData", "bulk_label_data");
            UpdateExistedTranslationsInput = new CheckboxInputModel("UpdateExistedTranslations", "update_existed_translations");
        }

        public override void SetInputModelValues()
        {
            OrganizationInput.Value = OrganizationUid.ToUidString();

            ProjectInput.Value = ProjectUid.ToUidString();
            ProjectNameInput.Value = ProjectName;

            BulkLabelInput.Value = BulkLabelData;
            UpdateExistedTranslationsInput.Value = UpdateExistedTranslations;

            InfoMessages.Clear();
            InfoMessages.Add("you_update_label_translation_previously_added_that_have_same_language");
            InfoMessages.Add("if_you_add_multiple_translation_for_same_language_accepts_the_first_one");
        }

        public override void SetInputErrorMessages()
        {
            if (OrganizationUid.IsEmptyGuid())
            {
                ErrorMessages.Add("organization_uid_is_not_valid");
            }

            if (ProjectUid.IsEmptyGuid())
            {
                ErrorMessages.Add("project_uid_is_not_valid");
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
