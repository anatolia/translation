using System;
using Translation.Client.Web.Models.Base;
using Translation.Client.Web.Models.InputModels;
using Translation.Common.Helpers;

namespace Translation.Client.Web.Models.Label
{
    public sealed class LabelEditModel : BaseModel
    {
        public Guid OrganizationUid { get; set; }

        public Guid LabelUid { get; set; }
        public string Key { get; set; }
        public string Description { get; set; }

        public HiddenInputModel OrganizationUidInput { get; }

        public HiddenInputModel LabelUidInput { get; }
        public InputModel KeyInput { get; }
        public LongInputModel DescriptionInput { get; }

        public LabelEditModel()
        {
            Title = "label_edit_title";

            OrganizationUidInput = new HiddenInputModel("OrganizationUid");

            LabelUidInput = new HiddenInputModel("LabelUid");
            KeyInput = new InputModel("Key", "key", true);
            DescriptionInput = new LongInputModel("Description", "description");
        }

        public override void SetInputModelValues()
        {
            OrganizationUidInput.Value = OrganizationUid.ToUidString();

            LabelUidInput.Value = LabelUid.ToUidString();
            KeyInput.Value = Key;
            DescriptionInput.Value = Description;
        }

        public override void SetInputErrorMessages()
        {
            if (OrganizationUid.IsEmptyGuid())
            {
                ErrorMessages.Add("organization_uid_not_valid");
            }

            if (LabelUid.IsEmptyGuid())
            {
                ErrorMessages.Add("label_uid_is_not_valid");
            }

            Key = Key.TrimOrDefault();
            if (Key.IsEmpty())
            {
                KeyInput.ErrorMessage.Add("key_required_error_message");
                InputErrorMessages.AddRange(KeyInput.ErrorMessage);
            }
        }
    }
}
