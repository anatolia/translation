using System.Threading.Tasks;

using Translation.Common.Models.Requests.Label;
using Translation.Common.Models.Responses.Label;

namespace Translation.Common.Contracts
{
    public interface ITextTranslateIntegration
    {
        Task<LabelGetTranslatedTextResponse> GetTranslatedText(LabelGetTranslatedTextRequest request);
    }
}