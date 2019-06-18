using System;
using Translation.Client.Web.Models.Base;
using Translation.Client.Web.Models.InputModels;
using Translation.Common.Helpers;

namespace Translation.Client.Web.Models.Integration
{
    public class IntegrationEditModel : BaseModel
    {
        public Guid IntegrationUid { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }

        public HiddenInputModel IntegrationUidInput { get; }

        public InputModel IntegrationNameInput { get; }
        public LongInputModel DescriptionInput { get; }

        public IntegrationEditModel()
        {
            Title = "integration_edit_title";

            IntegrationUidInput = new HiddenInputModel("IntegrationUid");

            IntegrationNameInput = new InputModel("Name", "name", true);
            DescriptionInput = new LongInputModel("Description", "description");
        }

        public override void SetInputModelValues()
        {
            IntegrationUidInput.Value = IntegrationUid.ToUidString();

            IntegrationNameInput.Value = Name;
            DescriptionInput.Value = Description;
        }

        public override void SetInputErrorMessages()
        {
            if (IntegrationUid.IsEmptyGuid())
            {
                ErrorMessages.Add("integration_uid_is_not_valid");
            }

            Name = Name.TrimOrDefault();
            if (Name.IsEmpty())
            {
                IntegrationNameInput.ErrorMessage.Add("integration_name_required_error_message");
                InputErrorMessages.AddRange(IntegrationNameInput.ErrorMessage);
            }
        }
    }
}