using StandardUtils.Models.Requests;

namespace Translation.Common.Models.Requests.TranslationProvider
{
    public sealed class ActiveTranslationProviderRequest : BaseRequest
    {
        public bool IsActive { get; set; }

        public ActiveTranslationProviderRequest()
        {
            IsActive = true;
        }
    }
}