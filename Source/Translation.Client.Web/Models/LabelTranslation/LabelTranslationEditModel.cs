using System;

using Translation.Client.Web.Models.Base;
using Translation.Client.Web.Models.InputModels;
using Translation.Common.Helpers;

namespace Translation.Client.Web.Models.LabelTranslation
{
    public sealed class LabelTranslationEditModel : BaseModel
    {
        public Guid OrganizationUid { get; set; }
        public Guid LabelTranslationUid { get; set; }

        public string LabelKey { get; set; }
        public string LanguageName { get; set; }
        public string LanguageIconUrl { get; set; }

        public string Translation { get; set; }

        public HiddenInputModel OrganizationUidInput { get; }
        public HiddenInputModel LabelTranslationUidInput { get; set; }

        public InputModel TranslationInput { get; set; }

        public LabelTranslationEditModel()
        {
            Title = "label_translation_edit_title";

            OrganizationUidInput = new HiddenInputModel("OrganizationUid");
            LabelTranslationUidInput = new HiddenInputModel("LabelTranslationUid");

            TranslationInput = new InputModel("Translation", "translation", true);
        }

        public override void SetInputModelValues()
        {
            OrganizationUidInput.Value = OrganizationUid.ToUidString();
            LabelTranslationUidInput.Value = LabelTranslationUid.ToUidString();

            TranslationInput.Value = Translation;
        }

        public override void SetInputErrorMessages()
        {
            if (OrganizationUid.IsEmptyGuid())
            {
                ErrorMessages.Add("organization_uid_not_valid");
            }

            if (LabelTranslationUid.IsEmptyGuid())
            {
                ErrorMessages.Add("label_translation_uid_not_valid");
            }

            Translation = Translation.TrimOrDefault();
            if (Translation.IsEmpty())
            {
                TranslationInput.ErrorMessage.Add("translation_required_error_message");
                InputErrorMessages.AddRange(TranslationInput.ErrorMessage);
            }
        }
    }
}
