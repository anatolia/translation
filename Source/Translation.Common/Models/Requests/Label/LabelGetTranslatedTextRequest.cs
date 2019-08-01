using Translation.Common.Models.Base;

namespace Translation.Common.Models.Requests.Label
{
    public sealed class LabelGetTranslatedTextRequest : BaseAuthenticatedRequest
    {
        public string TextToTranslate { get; set; }
        public string TargetLanguageIsoCode2 { get; set; }
        public string SourceLanguageIsoCode2 { get; set; }
        public string TranslateProviderType { get; set; }

        public LabelGetTranslatedTextRequest(long currentUserId, string textToTranslate, string targetLanguageIsoCode2,
                                             string sourceLanguageIsoCode2, string translateProviderType) : base(currentUserId)
        {
            TextToTranslate = textToTranslate.Replace("_", " ");
            TargetLanguageIsoCode2 = targetLanguageIsoCode2;
            SourceLanguageIsoCode2 = sourceLanguageIsoCode2;
            TranslateProviderType = translateProviderType;
        }
    }
}