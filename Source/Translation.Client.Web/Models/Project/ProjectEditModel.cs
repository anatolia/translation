using System;
using Translation.Client.Web.Models.Base;
using Translation.Client.Web.Models.InputModels;
using Translation.Common.Helpers;

namespace Translation.Client.Web.Models.Project
{
    public sealed class ProjectEditModel : BaseModel
    {
        public Guid OrganizationUid { get; set; }
        public Guid ProjectUid { get; set; }

        public string Name { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }
        public string Slug { get; set; }

        public HiddenInputModel OrganizationInput { get; }
        public HiddenInputModel ProjectInput { get; }

        public InputModel NameInput { get; }
        public InputModel SlugInput { get; }
        public UrlInputModel UrlInput { get; }
        public LongInputModel DescriptionInput { get; }

        public ProjectEditModel()
        {
            Title = "project_edit_title";

            OrganizationInput = new HiddenInputModel("OrganizationUid");
            ProjectInput = new HiddenInputModel("ProjectUid");

            NameInput = new InputModel("Name", "name", true);
            SlugInput = new InputModel("Slug", "slug", true);
            UrlInput = new UrlInputModel("Url", "url");
            DescriptionInput = new LongInputModel("Description", "description");
        }

        public override void SetInputModelValues()
        {
            OrganizationInput.Value = OrganizationUid.ToUidString();
            ProjectInput.Value = ProjectUid.ToUidString();

            NameInput.Value = Name;
            SlugInput.Value = Slug;
            UrlInput.Value = Url;
            DescriptionInput.Value = Description;
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

            Name = Name.TrimOrDefault();
            if (Name.IsEmpty())
            {
                NameInput.ErrorMessage.Add("project_name_required_error_message");
                InputErrorMessages.AddRange(NameInput.ErrorMessage);
            }

            Slug = Slug.TrimOrDefault();
            if (Slug.IsEmpty())
            {
                SlugInput.ErrorMessage.Add("project_slug_required_error_message");
                InputErrorMessages.AddRange(SlugInput.ErrorMessage);
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
