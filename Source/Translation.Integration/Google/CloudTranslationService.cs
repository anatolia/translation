using Microsoft.AspNetCore.Hosting;

using System.IO;

using Google.Cloud.Translation.V2;

using Translation.Common.Contracts;
using Translation.Common.Enumerations;
using Translation.Common.Models.Requests.Label;
using Translation.Common.Models.Responses.Label;

namespace Translation.Integration.Google
{
    public class CloudTranslationService : ICloudTranslationService
    {
        public CloudTranslationService(IHostingEnvironment environment)
        {
            var credential_path = Path.Combine(environment.WebRootPath, @"google_gloud_api_credentials\translation\Translation-99b9688ce4dd.json");
            System.Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", credential_path);
        }

        public LabelGetTranslatedTextResponse GetTranslatedText(LabelGetTranslatedTextRequest request)
        {
            var response = new LabelGetTranslatedTextResponse();

            var client = TranslationClient.Create();
            var googleTranslationCloudResponse = client.TranslateText(request.TextToTranslate, request.TargetLanguageIsoCode2, request.SourceLanguageIsoCode2);

            response.Item.Name = googleTranslationCloudResponse.TranslatedText;

            response.Status = ResponseStatus.Success;
            return response;
        }
    }
}