using System.Configuration;
using System.IO;
using System.Threading.Tasks;

using Google.Apis.Auth.OAuth2;
using Google.Cloud.Translation.V2;

using Translation.Common.Contracts;

namespace Translation.Integrations.Providers
{
    public class GoogleTranslateProvider : IGoogleTranslateProvider
    {
        public string GoogleApplicationCredentialsFile { get;}
        public TranslationClient Client { get;}

        public GoogleTranslateProvider()
        {
            GoogleApplicationCredentialsFile = ConfigurationManager.AppSettings["GOOGLE_APPLICATION_CREDENTIALS_FILE"];

            GoogleCredential googleCredential;
            using (var stream = new FileStream(GoogleApplicationCredentialsFile, FileMode.Open, FileAccess.Read))
            {
                googleCredential = GoogleCredential.FromStream(stream).CreateScoped();
            }

            Client = TranslationClient.Create(googleCredential);
        }

        public async Task<string> TranslateText(string textToTranslate, string targetLanguageIsoCode2, string sourceLanguageIsoCode2)
        {
            var response = await Client.TranslateTextAsync(textToTranslate, targetLanguageIsoCode2, sourceLanguageIsoCode2);

            return response.TranslatedText;
        }
    }
}