﻿using System.Configuration;
using System.Threading.Tasks;

using Translation.Common.Contracts;
using Translation.Common.Enumerations;
using Translation.Common.Models.Requests.Label;
using Translation.Common.Models.Responses.Label;
using Translation.Integrations.Providers;

namespace Translation.Integrations
{
    public class TextTranslateIntegration : ITextTranslateIntegration
    {
        private readonly IGoogleTranslateProvider _googleTranslateProvider;
        private readonly IYandexTranslateProvider _yandexTranslateProvider;
        public string ProviderType { get; set; }

        public TextTranslateIntegration(IGoogleTranslateProvider googleTranslateProvider,
                                        IYandexTranslateProvider yandexTranslateProvider)
        {
            ProviderType = ConfigurationManager.AppSettings["TRANSLATE_PROVIDER_TYPE"];
            _googleTranslateProvider = googleTranslateProvider;
            _yandexTranslateProvider = yandexTranslateProvider;
        }

        public async Task<LabelGetTranslatedTextResponse> GetTranslatedText(LabelGetTranslatedTextRequest request)
        {
            var response = new LabelGetTranslatedTextResponse();

            if (ProviderType == TranslateProviderType.Google)
            {
                response.Item.Name = await _googleTranslateProvider.TranslateText(request.TextToTranslate, request.TargetLanguageIsoCode2, request.SourceLanguageIsoCode2);

            }else if (ProviderType == TranslateProviderType.Yandex)
            {
                response.Item.Name = await _yandexTranslateProvider.TranslateText(request.TextToTranslate, request.TargetLanguageIsoCode2);
            }

            response.Status = ResponseStatus.Success;
            return response;
        }
    }
}