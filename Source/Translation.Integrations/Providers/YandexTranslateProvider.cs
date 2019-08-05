using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Threading.Tasks;

using Newtonsoft.Json;

using Translation.Common.Contracts;

namespace Translation.Integrations.Providers
{
    public class YandexTranslateProvider : ITextTranslateProvider
    {
        public string YandexTranslationApiKey { get;}
        private string RequestUrl { get;}

        public YandexTranslateProvider()
        {
            YandexTranslationApiKey = ConfigurationManager.AppSettings["YANDEX_TRANSLATE_API_KEY"];
            RequestUrl = "https://translate.yandex.net/api/v1.5/tr.json/translate";
        }

        public async Task<string> TranslateText(string textToTranslate, string sourceLanguageIsoCode2, string targetLanguageIsoCode2)
        {
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