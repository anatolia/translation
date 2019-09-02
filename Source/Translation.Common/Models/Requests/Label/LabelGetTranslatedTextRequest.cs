using Translation.Common.Helpers;
using Translation.Common.Models.Base;

namespace Translation.Common.Models.Requests.Label
{
    public sealed class LabelGetTranslatedTextRequest : BaseAuthenticatedRequest
    {
        public string TextToTranslate { get; set; }
        public string TargetLanguageIsoCode2 { get; set; }
        public string SourceLanguageIsoCode2 { get; set; }
        public bool IsGettingTranslationFromOtherProject { get; set; }

        public LabelGetTranslatedTextRequest(long currentUserId, string textToTranslate, string targetLanguageIsoCode2,
                                             string sourceLanguageIsoCode2, bool isGettingTranslationFromOtherProject = false) : base(currentUserId)
        {
            if (targetLanguageIsoCode2.IsEmpty())
            {
                ThrowArgumentException(nameof(targetLanguageIsoCode2), targetLanguageIsoCode2);
            }

            if (sourceLanguageIsoCode2.IsEmpty())
            {
                ThrowArgumentException(nameof(sourceLanguageIsoCode2), sourceLanguageIsoCode2);
            }

            TextToTranslate = textToTranslate.Replace("_", " ");
            TargetLanguageIsoCode2 = targetLanguageIsoCode2;
            SourceLanguageIsoCode2 = sourceLanguageIsoCode2;
            IsGettingTranslationFromOtherProject = isGettingTranslationFromOtherProject;
        }
    }
}