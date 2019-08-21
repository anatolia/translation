using System;
using System.Collections.Generic;
using Translation.Client.Web.Models.Base;
using Translation.Client.Web.Models.InputModels;
using Translation.Common.Helpers;

namespace Translation.Client.Web.Models.Label
{
    public sealed class LabelCreateModel : BaseModel
    {
        public Guid OrganizationUid { get; set; }

        public Guid ProjectUid { get; set; }
        public string ProjectName { get; set; }
        public string Key { get; set; }
        public string Description { get; set; }
        public string LanguageUid { get; set; }
        public string LanguageName { get; set; }
        public string LanguageIconUrl { get; set; }
        public string ActiveTranslationProviderInfo { get; set; }
        public bool IsGettingTranslationFromOtherProject { get; set; }

        public CheckboxInputModel IsGettingTranslationFromOtherProjectInput { get; set; }
        public HiddenInputModel OrganizationInput { get; }
        public HiddenInputModel ProjectInput { get; }

        public InputModel KeyInput { get; }
        public LongInputModel DescriptionInput { get; }
        public SelectInputModel LanguagesInput { get; }
        public LabelCreateModel()
        {
            Title = "label_create_title";

            OrganizationInput = new HiddenInputModel("OrganizationUid");
            ProjectInput = new HiddenInputModel("ProjectUid");
            KeyInput = new InputModel("Key", "key", true);
            DescriptionInput = new LongInputModel("Description", "description");
            LanguagesInput = new SelectInputModel("Language", "language", "/Language/SelectData");
            LanguagesInput.IsMultiple = true;
            LanguagesInput.InfoText = "selected_languages_will_have_translated_by_provider_automatically";
            ActiveTranslationProviderInfo = "translation_provider_not_active";
            IsGettingTranslationFromOtherProjectInput = new CheckboxInputModel("IsGettingTranslationFromOtherProject", "is_getting_translation_from_other_project");
        }

        public override void SetInputModelValues()
        {
            OrganizationInput.Value = OrganizationUid.ToUidString();
            ProjectInput.Value = ProjectUid.ToUidString();
            KeyInput.Value = Key;
            DescriptionInput.Value = Description;
            LanguagesInput.Value = LanguageUid;
            IsGettingTranslationFromOtherProjectInput.Value = IsGettingTranslationFromOtherProject;
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

            Key = Key.TrimOrDefault();
            if (Key.IsEmpty())
            {
                KeyInput.ErrorMessage.Add("key_required_error_message");
                InputErrorMessages.AddRange(KeyInput.ErrorMessage);
            }
        }
    }
}
