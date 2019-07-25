using System;

using Translation.Client.Web.Models.Base;
using Translation.Client.Web.Models.InputModels;
using Translation.Common.Helpers;

namespace Translation.Client.Web.Models.Integration
{
    public sealed class IntegrationCreateModel : BaseModel
    {
        public Guid OrganizationUid { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }

        public HiddenInputModel OrganizationUidInput { get; }

        public InputModel NameInput { get; }
        public LongInputModel DescriptionInput { get; }

        public IntegrationCreateModel()
        {
            Title = "integration_create_title";

            OrganizationUidInput = new HiddenInputModel("OrganizationUid");

            NameInput = new InputModel("Name", "name", true);
            DescriptionInput = new LongInputModel("Description", "description");
        }

        public override void SetInputModelValues()
        {
            OrganizationUidInput.Value = OrganizationUid.ToUidString();

            NameInput.Value = Name;
            DescriptionInput.Value = Description;
        }

        public override void SetInputErrorMessages()
        {
            if (OrganizationUid.IsEmptyGuid())
            {
                ErrorMessages.Add("organization_uid_is_not_valid");
            }

            Name = Name.TrimOrDefault();
            if (Name.IsEmpty())
            {
                NameInput.ErrorMessage.Add("integration_name_required_error_message");
                InputErrorMessages.AddRange(NameInput.ErrorMessage);
            }
        }
    }
}