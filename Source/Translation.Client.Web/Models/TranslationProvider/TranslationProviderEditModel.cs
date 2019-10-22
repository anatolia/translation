using System;

using Translation.Client.Web.Models.Base;
using Translation.Client.Web.Models.InputModels;
using Translation.Common.Helpers;

namespace Translation.Client.Web.Models.TranslationProvider
{
    public sealed class TranslationProviderEditModel : BaseModel
    {
        public Guid TranslationProviderUid { get; set; }
        public string Value { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string GoogleDescriptionLink { get; set; }

        public HiddenInputModel TranslationProviderInput { get; }
        public HiddenInputModel TranslationProviderNameInput { get; }
        public LongInputModel DescriptionInput { get; }
        public TextareaInputModel ValueInput { get; }

        public TranslationProviderEditModel()
        {
            Title = "translation_provider_edit_title";
            TranslationProviderInput = new HiddenInputModel("TranslationProviderUid");
            TranslationProviderNameInput = new HiddenInputModel("Name");
            DescriptionInput = new LongInputModel("Description", "description");
            ValueInput = new TextareaInputModel("Value", "value");
            GoogleDescriptionLink = "https://medium.com/@yeksancansu/how-to-use-google-translate-api-in-android-studio-projects-7f09cae320c7";
        }

        public override void SetInputModelValues()
        {
            TranslationProviderInput.Value = TranslationProviderUid.ToUidString();
            TranslationProviderNameInput.Value = Name;
            DescriptionInput.Value = Description;
            ValueInput.Value = Value;

            InfoMessages.Clear();
            InfoMessages.Add("you_paste_required_informations_from_json_file_for_google_provider");
            InfoMessages.Add("you_paste_required_api_key_for_yandex_provider");
        }

        public override void SetInputErrorMessages()
        {
            if (TranslationProviderUid.IsEmptyGuid())
            {
                ErrorMessages.Add("translation_provider_uid_is_not_valid");
            }

            Name = Name.TrimOrDefault();
            if (Name.IsEmpty())
            {
                TranslationProviderNameInput.ErrorMessage.Add("translation_provider_name_required_error_message");
                InputErrorMessages.AddRange(TranslationProviderNameInput.ErrorMessage);
            }

            Value = Value.TrimOrDefault();
            if (Value.IsEmpty())
            {
                ValueInput.ErrorMessage.Add("translation_provider_api_key_not_valid");
                InputErrorMessages.AddRange(ValueInput.ErrorMessage);
            }

            if (Name == "google" && (!Value.StartsWith("{") || !Value.EndsWith("}")))
            {
                ValueInput.ErrorMessage.Add("google_api_must_place_between_{_}");
                InputErrorMessages.AddRange(ValueInput.ErrorMessage);
            }

            if (Name == "google" && (!Value.Contains("type") || !Value.Contains("project_id") || !Value.Contains("private_key_id") ||
                            !Value.Contains("private_key") || !Value.Contains("client_email") || !Value.Contains("client_id") ||
                            !Value.Contains("auth_uri") || !Value.Contains("token_uri") || !Value.Contains("auth_uri")))
            {
                ValueInput.ErrorMessage.Add("google_api_Informations_format_not_valid");
                InputErrorMessages.AddRange(ValueInput.ErrorMessage);
            }

            if (Name == "yandex" && !Value.StartsWith("trns"))
            {
                ValueInput.ErrorMessage.Add("yandex_api_key_must_start_with_trns");
                InputErrorMessages.AddRange(ValueInput.ErrorMessage);
            }        
        }
    }
}
