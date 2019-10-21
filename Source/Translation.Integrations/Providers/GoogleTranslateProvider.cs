/* Creating a GCP(Google Cloud Platform) Console project.
 *********************************************************
 1.Sign in Google API Console from the link: https://console.cloud.google.com/apis/.
 2.Click on the “Select a project” on the Google Cloud Platform tab.
 3.Click on the “New Project”.
 4.Enter your project name and click on the “Create” button.
 5.Your project will be displayed on the Google Cloud Platform dashboard. Select “APIs and Services” on the navigation drawer at the left side.
 6.Click on the “Enable APIs and Services” button.
 7.Type “Cloud Translation API” in the search box and select the API
 8.You need to enable Cloud Translation API for your project by clicking on the “Enable” button You need to enable Cloud Translation API for your project by clicking on the “Enable” button
 9.You are ready to manage Cloud Translation API for your project now. Click on “Manage”
 10.Click on “Create Credentials”
 11.Select “Cloud Translation API”
 12.Enter your personal information, select “JSON” as the key type and click on “Continue”
 13.The credentials file of your project will be downloaded to your computer automatically in JSON format. Keep this file.
 14.You(Super Admin) will use this JSON file as TranslationProvider value editing Super admin dashboard translation_providers link
  */

using System.Threading.Tasks;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Translation.V2;
using Translation.Common.Contracts;
using Translation.Data.Repositories.Contracts;

namespace Translation.Integrations.Providers
{
    public class GoogleTranslateProvider : ITextTranslateProvider
    {
        private readonly ITranslationProviderRepository _translationProviderRepository;
        public string GoogleApplicationCredentialsFile { get; set; }
        public TranslationClient Client { get; set; }
        public string Name { get; set; }
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