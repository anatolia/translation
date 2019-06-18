using System;
using Translation.Client.Web.Models.Base;
using Translation.Client.Web.Models.InputModels;
using Translation.Common.Helpers;

namespace Translation.Client.Web.Models.Project
{
    public class ProjectCloneModel : BaseModel
    {
        public Guid OrganizationUid { get; set; }
        public Guid CloningProjectUid { get; set; }
        public string CloningProjectName { get; set; }

        public string Name { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }

        public HiddenInputModel OrganizationUidInput { get; }
        public HiddenInputModel CloningProjectUidInput { get; }
        public HiddenInputModel CloningProjectNameInput { get; }

        public InputModel NameInput { get; }
        public UrlInputModel UrlInput { get; }
        public LongInputModel DescriptionInput { get; }

        public ProjectCloneModel()
        {
            Title = "project_clone_title";

            OrganizationUidInput = new HiddenInputModel("OrganizationUid");
            CloningProjectUidInput = new HiddenInputModel("CloningProjectUid");
            CloningProjectNameInput = new HiddenInputModel("CloningProjectName");

            NameInput = new InputModel("Name", "name", true);
            UrlInput = new UrlInputModel("Url", "url");
            DescriptionInput = new LongInputModel("Description", "description");
        }

        public override void SetInputModelValues()
        {
            OrganizationUidInput.Value = OrganizationUid.ToUidString();
            CloningProjectUidInput.Value = CloningProjectUid.ToUidString();
            CloningProjectNameInput.Value = CloningProjectName;

            NameInput.Value = Name;
            UrlInput.Value = Url;
            DescriptionInput.Value = Description;
        }

        public override void SetInputErrorMessages()
        {
            if (OrganizationUid.IsEmptyGuid())
            {
                ErrorMessages.Add("organization_uid_is_not_valid");
            }

            if (CloningProjectUid.IsEmptyGuid())
            {
                ErrorMessages.Add("cloning_project_uid_is_not_valid");
            }

            Name = Name.TrimOrDefault();
            if (Name.IsEmpty())
            {
                NameInput.ErrorMessage.Add("project_name_required_error_message");
                InputErrorMessages.AddRange(NameInput.ErrorMessage);
            }

            Url = Url.TrimOrDefault();
            if (Url.IsNotEmpty()
                && Url.IsNotUrl())
            {
                UrlInput.ErrorMessage.Add("url_is_not_valid_error_message");
                InputErrorMessages.AddRange(UrlInput.ErrorMessage);
            }
        }
    }
}