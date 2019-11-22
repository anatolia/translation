using System;

using StandardUtils.Helpers;

using Translation.Client.Web.Models.Base;
using Translation.Client.Web.Models.InputModels;

namespace Translation.Client.Web.Models.Organization
{
    public sealed class OrganizationEditModel : BaseModel
    {
        public Guid OrganizationUid { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public HiddenInputModel OrganizationInput { get; set; }
        public InputModel NameInput { get; }
        public LongInputModel DescriptionInput { get; }

        public OrganizationEditModel()
        {
            Title = "organization_edit_title";

            OrganizationInput = new HiddenInputModel("OrganizationUid");
            NameInput = new InputModel("Name", "name", true);
            DescriptionInput = new LongInputModel("Description", "description");
        }

        public override void SetInputModelValues()
        {
            OrganizationInput.Value = OrganizationUid.ToUidString();
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
                NameInput.ErrorMessage.Add("organization_name_required_error_message");
                InputErrorMessages.AddRange(NameInput.ErrorMessage);
            }
        }

    }
}