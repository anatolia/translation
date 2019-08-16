using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Threading.Tasks;

using Newtonsoft.Json;

using Translation.Common.Contracts;
using Translation.Data.Repositories.Contracts;

namespace Translation.Integrations.Providers
{
    public class YandexTranslateProvider : ITextTranslateProvider
    {
        private readonly ITranslationProviderRepository _translationProviderRepository;
        public string Name { get; }
        public string YandexTranslationApiKey { get; set; }
        private string RequestUrl { get; set; }

        public YandexTranslateProvider(ITranslationProviderRepository translationProviderRepository)
        {
            _translationProviderRepository = translationProviderRepository;
            Name = "yandex";
        }

        public void CreateClient()
        {
            var provider = _translationProviderRepository.Select(x => x.Name == Name).Result;
            YandexTranslationApiKey = provider.Value;
            RequestUrl = "https://translate.yandex.net/api/v1.5/tr.json/translate";
        }

        public async Task<string> TranslateText(string textToTranslate, string sourceLanguageIsoCode2, string targetLanguageIsoCode2)
        {
            if (YandexTranslationApiKey==null || RequestUrl==null)
            {
                CreateClient();
            }

            var requestBody = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("text", textToTranslate),
                new KeyValuePair<string, string>("lang", sourceLanguageIsoCode2 + "-" + targetLanguageIsoCode2),
                new KeyValuePair<string, string>("key", YandexTranslationApiKey),
            });

            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.PostAsync(RequestUrl, requestBody);
                var responseBody = await response.Content.ReadAsStringAsync();
                var yandexTranslateResponse = JsonConvert.DeserializeObject<YandexTranslateResponse>(responseBody);

                return yandexTranslateResponse.Text[0];
            }
        }
    }
}