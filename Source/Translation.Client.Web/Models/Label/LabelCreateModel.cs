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
        public Guid ProjectLanguageUid { get; set; }
        public string ProjectName { get; set; }
        public string ProjectLanguageName { get; set; }
        public string ProjectLanguageIconUrl { get; set; }
        public string Key { get; set; }
        public string Description { get; set; }
        public string LabelTranslationLanguageName { get; set; }
        public string LabelTranslationLanguageUid { get; set; }
        public string ActiveTranslationProviderInfo { get; set; }
        public bool IsGettingTranslationFromOtherProject { get; set; }

        public HiddenInputModel OrganizationInput { get; }
        public HiddenInputModel ProjectInput { get; }
        public HiddenInputModel ProjectNameInput { get; }
        public HiddenInputModel ProjectLanguageInput { get; }
        public HiddenInputModel ProjectLanguageNameInput { get; }
        public HiddenInputModel ProjectLanguageIconUrlInput { get; }
        public HiddenInputModel ActiveTranslationProviderInfoInput { get; }
        public InputModel KeyInput { get; }
        public LongInputModel DescriptionInput { get; }
        public SelectInputModel LabelTranslationLanguagesInput { get; }
        public CheckboxInputModel IsGettingTranslationFromOtherProjectInput { get; set; }
        public LabelCreateModel()
        {
            Title = "label_create_title";

            OrganizationInput = new HiddenInputModel("OrganizationUid");
            ProjectInput = new HiddenInputModel("ProjectUid");
            ProjectNameInput= new HiddenInputModel("ProjectName");
            ProjectLanguageInput = new HiddenInputModel("ProjectLanguageUid");
            ProjectLanguageNameInput = new HiddenInputModel("ProjectLanguageName");
            ProjectLanguageIconUrlInput = new HiddenInputModel("ProjectLanguageIconUrl");
            ActiveTranslationProviderInfoInput= new HiddenInputModel("ActiveTranslationProviderInfo");
            KeyInput = new InputModel("Key", "key", true);
            DescriptionInput = new LongInputModel("Description", "description");
            LabelTranslationLanguagesInput = new SelectInputModel("LabelTranslationLanguage", "labelTranslationLanguage", "/Language/SelectData");
            LabelTranslationLanguagesInput.IsMultiple = true;
            LabelTranslationLanguagesInput.InfoText = "selected_languages_will_have_translated_by_provider_automatically";
            ActiveTranslationProviderInfo = "translation_provider_is_not_active";
            IsGettingTranslationFromOtherProjectInput = new CheckboxInputModel("IsGettingTranslationFromOtherProject", "is_getting_translation_from_other_project");
        }

        public override void SetInputModelValues()
        {
            OrganizationInput.Value = OrganizationUid.ToUidString();
            ProjectInput.Value = ProjectUid.ToUidString();
            ProjectLanguageInput.Value = ProjectLanguageUid.ToUidString();
            ProjectNameInput.Value = ProjectName;
            ProjectLanguageNameInput.Value = ProjectLanguageName;
            ProjectLanguageIconUrlInput.Value = ProjectLanguageIconUrl;
            ActiveTranslationProviderInfoInput.Value = ActiveTranslationProviderInfo;
            KeyInput.Value = Key;
            DescriptionInput.Value = Description;
            if (ErrorMessages.Count==0)
            {
                LabelTranslationLanguagesInput.Value = LabelTranslationLanguageUid;
            }
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
