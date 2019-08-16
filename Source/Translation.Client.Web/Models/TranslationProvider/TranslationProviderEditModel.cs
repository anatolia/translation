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
        }

        public override void SetInputModelValues()
        {
            TranslationProviderInput.Value = TranslationProviderUid.ToUidString();
            TranslationProviderNameInput.Value = Name;
            DescriptionInput.Value = Description;
            ValueInput.Value = Value;

            InfoMessages.Clear();
            InfoMessages.Add("you_add_required_json_file_for_google_provider");
            InfoMessages.Add("you_add_required_api_key_for_yandex_provider");
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
                ValueInput.ErrorMessage.Add("translation_provider_data_required_error_message");
                InputErrorMessages.AddRange(ValueInput.ErrorMessage);
            }
        }
    }
}
