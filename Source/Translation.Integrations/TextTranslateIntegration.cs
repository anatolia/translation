using System.Collections.Generic;
using System.Configuration;
using System.IO.Compression;
using System.Threading.Tasks;

using Translation.Common.Contracts;
using Translation.Common.Enumerations;
using Translation.Common.Models.Requests.Label;
using Translation.Common.Models.Responses.Label;
using Translation.Data.Repositories.Contracts;
using Translation.Integrations.Providers;

namespace Translation.Integrations
{
    public class TextTranslateIntegration : ITextTranslateIntegration
    {
        private readonly ITranslationProviderRepository _translationProviderRepository;
        private  Dictionary<string, ITextTranslateProvider> TranslateProviders { get; set; }

        public TextTranslateIntegration( ITranslationProviderRepository translationProviderRepository)
        {
            _translationProviderRepository = translationProviderRepository;
            TranslateProviders=new Dictionary<string, ITextTranslateProvider>();
            TranslateProviders.Add("google",new GoogleTranslateProvider(translationProviderRepository));
            TranslateProviders.Add("yandex", new YandexTranslateProvider(translationProviderRepository));
        }

        public async Task<LabelGetTranslatedTextResponse> GetTranslatedText(LabelGetTranslatedTextRequest request)
        {
            var response = new LabelGetTranslatedTextResponse();

            var provider = await _translationProviderRepository.Select(x => x.IsActive == true);

            response.Item.Name = await TranslateProviders[provider.Name].TranslateText(request.TextToTranslate, request.TargetLanguageIsoCode2, request.SourceLanguageIsoCode2);

            response.Status = ResponseStatus.Success;
            return response;
        }
    }
}