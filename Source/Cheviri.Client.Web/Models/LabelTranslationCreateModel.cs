using Cheviri.Client.Web.Helpers;
using Cheviri.Client.Web.Models.InputModels;
using Cheviri.Common.Helpers;

namespace Cheviri.Client.Web.Models
{
    public class LabelTranslationCreateModel : BaseModel
    {
        public string Label { get; set; }
        public string Language { get; set; }
        public string LabelTranslation { get; set; }

        public SelectInputModel LabelInput { get; set; }
        public SelectInputModel LanguageInput { get; set; }
        public TextareaInputModel LabelTranslationInput { get; set; }

        public LabelTranslationCreateModel()
        {
            Title = Localizer.Localize("label_translation_create_title");

            LabelInput = new SelectInputModel("Label", "label", true);
            LanguageInput = new SelectInputModel("Language", "language", true);
            LabelTranslationInput = new TextareaInputModel("LabelTranslation", "label_translation", true);
        }

        public override void SetInputModelValues()
        {
            LabelInput.Value = Label;
            LanguageInput.Value = Language;
            LabelTranslationInput.Value = LabelTranslation;
        }

        public override void SetInputErrorMessages()
        {
            if (Label.IsEmpty())
            {
                LabelInput.ErrorMessage.Add("name_required_error_message");
                ErrorMessages.AddRange(LabelInput.ErrorMessage);
            }

            if (Language.IsEmpty())
            {
                LanguageInput.ErrorMessage.Add("language_required_error_message");
                ErrorMessages.AddRange(LanguageInput.ErrorMessage);
            }

            if (LabelTranslation.IsEmpty())
            {
                LabelTranslationInput.ErrorMessage.Add("label_translation_required_error_message");
                ErrorMessages.AddRange(LabelTranslationInput.ErrorMessage);
            }
        }
    }
}