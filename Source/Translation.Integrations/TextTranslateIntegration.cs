using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO.Compression;
using System.Threading.Tasks;

using Translation.Common.Contracts;
using Translation.Common.Enumerations;
using Translation.Common.Models.Requests.Label;
using Translation.Common.Models.Responses.Label;
using Translation.Common.Models.Shared;
using Translation.Data.Entities.Domain;
using Translation.Data.Repositories.Contracts;
using Translation.Integrations.Providers;
using Translation.Service.Managers;

namespace Translation.Integrations
{
    public class TextTranslateIntegration : ITextTranslateIntegration
    {
        private readonly ITextTranslateProvider[] _textTranslateProvider;
        private Dictionary<string, ITextTranslateProvider> TranslateProviders { get; set; }
        private TranslationProvider ActiveTranslationProvider { get; set; }
        private readonly CacheManager _cacheManager;

        public TextTranslateIntegration(CacheManager cacheManager, ITextTranslateProvider[] textTranslateProvider)
        {
            _cacheManager = cacheManager;
            _textTranslateProvider = textTranslateProvider;
            TranslateProviders = new Dictionary<string, ITextTranslateProvider>();
            for (int i = 0; i < _textTranslateProvider.Length; i++)
            {
                TranslateProviders.Add(_textTranslateProvider[i].Name, _textTranslateProvider[i]);
            }
        }

        public async Task<LabelGetTranslatedTextResponse> GetTranslatedText(LabelGetTranslatedTextRequest request)
        {
            var response = new LabelGetTranslatedTextResponse();
            ActiveTranslationProvider = _cacheManager.GetCachedActiveTranslationProvider(true);
            if (ActiveTranslationProvider==null)
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