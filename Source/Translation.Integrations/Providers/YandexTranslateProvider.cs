using System.Collections.Generic;
using System.Configuration;
using System.Collections.Specialized;
using System.Net;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;

using Translation.Common.Contracts;

namespace Translation.Integrations.Providers
{
    public class YandexTranslateProvider : IYandexTranslateProvider
    {
        public string YandexTranslationApiKey { get;}
        private string BaseUrl { get;}
        public WebClient Client { get;}

        public YandexTranslateProvider()
        {
            YandexTranslationApiKey = ConfigurationManager.AppSettings["YANDEX_TRANSLATE_API_KEY"];
            BaseUrl = "https://translate.yandex.net/api/v1.5/tr.json/translate";
            Client = new WebClient();
        }

        public async Task<string> TranslateText(string textToTranslate, string targetLanguageIsoCode2)
        {
            var data = new NameValueCollection();
            data["text"] = textToTranslate;
            data["lang"] = targetLanguageIsoCode2;
            data["key"] = YandexTranslationApiKey;

            var response = await Client.UploadValuesTaskAsync(BaseUrl, "POST", data);

            var responseString = Encoding.UTF8.GetString(response);

            var rootObject = JsonConvert.DeserializeObject<YandexTranslation>(responseString);

            return rootObject.Text[0];
        }

        public class YandexTranslation
        {
            public int Code { get; set; }
            public string Lang { get; set; }
            public List<string> Text { get; set; }
        }
    }
}