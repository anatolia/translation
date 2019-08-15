using System;
using System.Collections.Generic;
using Translation.Client.Web.Models.Base;
using Translation.Client.Web.Models.InputModels;
using Translation.Common.Helpers;

namespace Translation.Client.Web.Models.TranslationProvider
{
    public sealed class TranslationProviderCreateModel : BaseModel
    {
        public Guid TranslationProviderUid { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public HiddenInputModel TranslationProviderInput { get; }
        public LongInputModel DescriptionInput { get; }

        public TranslationProviderCreateModel()
        {
            Title = "translation_provider_create_title";
            TranslationProviderInput = new HiddenInputModel("TranslationProviderUid");
            DescriptionInput = new LongInputModel("Description", "description");

        }

        public override void SetInputModelValues()
        {
            TranslationProviderInput.Value = TranslationProviderUid.ToUidString();

            DescriptionInput.Value = Description;
        }

        public override void SetInputErrorMessages()
        {

            if (TranslationProviderUid.IsEmptyGuid())
            {
                ErrorMessages.Add("translation_uid_not_valid");
            }

        }
    }
}
