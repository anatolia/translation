using Translation.Common.Models.Requests.Label;
using Translation.Common.Models.Responses.Label;

namespace Translation.Common.Contracts
{
    public interface ICloudTranslationService
    {
        LabelGetTranslatedTextResponse GetTranslatedText(LabelGetTranslatedTextRequest request);
    }
}