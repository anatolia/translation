using System;

using Translation.Client.Web.Models.Base;
using Translation.Client.Web.Models.InputModels;
using Translation.Common.Helpers;

namespace Translation.Client.Web.Models.Project
{
    public sealed class ProjectCreateModel : BaseModel
    {
        public Guid OrganizationUid { get; set; }

        public string Name { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public string Slug { get; set; }
        public Guid LanguageUid { get; set; }
        public string LanguageName { get; set; }

        public HiddenInputModel OrganizationInput { get; }

        public InputModel NameInput { get; }
        public InputModel SlugInput { get; }
        public UrlInputModel UrlInput { get; }
        public LongInputModel DescriptionInput { get; }
        public SelectInputModel LanguageInput { get; }

        public ProjectCreateModel()
        {
            Title = "project_create_title";

            OrganizationInput = new HiddenInputModel("OrganizationUid");

            NameInput = new InputModel("Name", "name", true);
            SlugInput = new InputModel("Slug", "slug", true);
            UrlInput = new UrlInputModel("Url", "url");
            DescriptionInput = new LongInputModel("Description", "description");
            LanguageInput = new SelectInputModel("LanguageUid", "LanguageName", "language", "/Language/SelectData");
            LanguageInput.IsOptionTypeContent = true;
        }

        public override void SetInputModelValues()
        {
            OrganizationInput.Value = OrganizationUid.ToUidString();

            NameInput.Value = Name;
            SlugInput.Value = Slug;
            UrlInput.Value = Url;
            DescriptionInput.Value = Description;

            if (LanguageUid.IsNotEmptyGuid())
            {
                LanguageInput.Value = LanguageUid.ToUidString();
                LanguageInput.Text = LanguageName;
            }

            InfoMessages.Clear();
            InfoMessages.Add("the_project_language_will_use_as_the_source_language_during_the_automatic_translation_of_the_labels");
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

            if (LanguageUid.IsEmptyGuid())
            {
                LanguageInput.ErrorMessage.Add("language_uid_not_valid");
                InputErrorMessages.AddRange(LanguageInput.ErrorMessage);
            }
        }
    }
}
