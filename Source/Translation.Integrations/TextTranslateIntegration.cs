using System.Threading.Tasks;

using Translation.Common.Contracts;
using Translation.Common.Enumerations;
using Translation.Common.Models.Requests.Label;
using Translation.Common.Models.Responses.Label;

namespace Translation.Integrations
{
    public class TextTranslateIntegration : ITextTranslateIntegration
    {
        private readonly IGoogleTranslateProvider _googleTranslateProvider;

        public TextTranslateIntegration(IGoogleTranslateProvider googleTranslateProvider)
        {
            _googleTranslateProvider = googleTranslateProvider;
        }

        public async Task<LabelGetTranslatedTextResponse> GetTranslatedText(LabelGetTranslatedTextRequest request)
        {
            var response = new LabelGetTranslatedTextResponse();

            response.Item.Name = await _googleTranslateProvider.TranslateText(request.TextToTranslate, request.TargetLanguageIsoCode2, request.SourceLanguageIsoCode2);

            response.Status = ResponseStatus.Success;
            return response;
        }
    }
}