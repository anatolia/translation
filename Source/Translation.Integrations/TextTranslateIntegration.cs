using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Translation.Common.Contracts;
using Translation.Common.Enumerations;
using Translation.Common.Models.Requests.Label;
using Translation.Common.Models.Responses.Label;
using Translation.Common.Models.Shared;
using Translation.Service.Managers;

namespace Translation.Integrations
{
    public class TextTranslateIntegration : ITextTranslateIntegration
    {
        private readonly CacheManager _cacheManager;
        private Dictionary<string, ITextTranslateProvider> TranslateProviders { get; set; }
        private ActiveTranslationProvider ActiveTranslationProvider { get; set; }

        public TextTranslateIntegration(
            CacheManager cacheManager,
            params ITextTranslateProvider[] textTranslateProvider)
        {
            _cacheManager = cacheManager;
            TranslateProviders = textTranslateProvider.ToDictionary(x => x.Name, x => x);
        }

        public async Task<LabelGetTranslatedTextResponse> GetTranslatedText(LabelGetTranslatedTextRequest request)
        {
            ActiveTranslationProvider = _cacheManager.GetCachedActiveTranslationProvider(true);

            return ActiveTranslationProvider == null
                ? new LabelGetTranslatedTextResponse {Status = ResponseStatus.Failed}
                : new LabelGetTranslatedTextResponse
                {
                    Status = ResponseStatus.Success,
                    Item =
                    {
                        Name = await TranslateProviders[ActiveTranslationProvider.Name]
                            .TranslateText(
                                request.TextToTranslate, 
                                request.TargetLanguageIsoCode2,
                                request.SourceLanguageIsoCode2)
                            .ConfigureAwait(false)
                    }
                };
        }
    }
}