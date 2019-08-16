using System;
using Translation.Client.Web.Models.Base;

namespace Translation.Client.Web.Models.TranslationProvider
{
    public class TranslationProviderDetailModel : BaseModel
    {
        public Guid TranslationProviderUid { get; set; }
        public string Value { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public TranslationProviderDetailModel()
        {
            Title = "translation_provider_detail_title";
        }
    }
}