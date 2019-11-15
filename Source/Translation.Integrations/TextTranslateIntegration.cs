using System.Collections.Generic;
using System.Threading.Tasks;

using StandardUtils.Enumerations;

using Translation.Common.Contracts;
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

        public TextTranslateIntegration(CacheManager cacheManager, params ITextTranslateProvider[] textTranslateProvider)
        {
            _cacheManager = cacheManager;

            TranslateProviders = new Dictionary<string, ITextTranslateProvider>();

            for (var i = 0; i < textTranslateProvider.Length; i++)
            {
                TranslateProviders.Add(textTranslateProvider[i].Name, textTranslateProvider[i]);
            }
        }

        public async Task<LabelGetTranslatedTextResponse> GetTranslatedText(LabelGetTranslatedTextRequest request)
        {
            var response = new LabelGetTranslatedTextResponse();

            ActiveTranslationProvider = _cacheManager.GetCachedActiveTranslationProvider(true);

            if (ActiveTranslationProvider == null)
            {
                response.Status = ResponseStatus.Failed;
                return response;
            }

            response.Item.Name = await TranslateProviders[ActiveTranslationProvider.Name].TranslateText(request.TextToTranslate, request.TargetLanguageIsoCode2, request.SourceLanguageIsoCode2);

            response.Status = ResponseStatus.Success;
            return response;
        }
    }
}