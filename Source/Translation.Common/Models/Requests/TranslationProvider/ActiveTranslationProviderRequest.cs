using Translation.Common.Models.Base;

namespace Translation.Common.Models.Requests.TranslationProvider
{
    public sealed class ActiveTranslationProviderRequest : BaseRequest
    {
        public bool Isactive { get; set; }

        public ActiveTranslationProviderRequest()
        {
            Isactive = true;
        }
    }
}