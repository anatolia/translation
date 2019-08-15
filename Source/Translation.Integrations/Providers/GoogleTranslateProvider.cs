using System.Configuration;
using System.IO;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Translation.V2;
using Newtonsoft.Json.Linq;
using StandardRepository.Helpers;
using Translation.Common.Contracts;
using Translation.Data.Repositories.Contracts;

namespace Translation.Integrations.Providers
{
    public class GoogleTranslateProvider : ITextTranslateProvider
    {
        private readonly ITranslationProviderRepository _translationProviderRepository;

        public string GoogleApplicationCredentialsFile { get; set; }
        public string Name { get; }
        public TranslationClient Client { get; set; }

        public GoogleTranslateProvider(ITranslationProviderRepository translationProviderRepository)
        {
            _translationProviderRepository = translationProviderRepository;

            Name = "google";
        }

        public void CreateClient()
        {
            var provider = _translationProviderRepository.Select(x => x.Name == Name).Result;
            GoogleApplicationCredentialsFile = provider.Value;

            GoogleCredential googleCredential;

            googleCredential = GoogleCredential.FromJson(GoogleApplicationCredentialsFile).CreateScoped();

            Client = TranslationClient.Create(googleCredential);
        }

        public async Task<string> TranslateText(string textToTranslate, string targetLanguageIsoCode2, string sourceLanguageIsoCode2)
        {
            if (Client == null)
            {
                CreateClient();
            }

            var response = await Client.TranslateTextAsync(textToTranslate, targetLanguageIsoCode2, sourceLanguageIsoCode2);

            return response.TranslatedText;
        }
    }
}