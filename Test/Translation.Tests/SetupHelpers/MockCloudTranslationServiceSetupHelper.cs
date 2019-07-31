
using System.Threading.Tasks;
using Moq;

using Translation.Common.Contracts;
using Translation.Common.Enumerations;
using Translation.Common.Models.Base;
using Translation.Common.Models.Requests.Label;
using Translation.Common.Models.Responses.Label;
using static Translation.Tests.TestHelpers.FakeConstantTestHelper;

namespace Translation.Tests.SetupHelpers
{
    public static class MockCloudTranslationServiceSetupHelper
    {
        public static void Setup_GetTranslatedText_Returns_LabelGetTranslatedTextResponse_Success(this Mock<ICloudTranslationService> service)
        {
            var item = new BaseDto() { Name = StringTwo };
            service.Setup(x => x.GetTranslatedText(It.IsAny<LabelGetTranslatedTextRequest>()))
                   .Returns(new LabelGetTranslatedTextResponse { Status = ResponseStatus.Success, Item =item});
        }

        public static void Setup_GetTranslatedText_Returns_LabelGetTranslatedTextResponse_Failed(this Mock<ICloudTranslationService> service)
        {
           
            service.Setup(x => x.GetTranslatedText(It.IsAny<LabelGetTranslatedTextRequest>()))
                .Returns(new LabelGetTranslatedTextResponse { Status = ResponseStatus.Failed});
        }

        public static void Setup_GetTranslatedText_Returns_LabelGetTranslatedTextResponse_Invalid(this Mock<ICloudTranslationService> service)
        {
            service.Setup(x => x.GetTranslatedText(It.IsAny<LabelGetTranslatedTextRequest>()))
                .Returns(new LabelGetTranslatedTextResponse { Status = ResponseStatus.Invalid });
        }

        public static void Verify_GetTranslatedText(this Mock<ICloudTranslationService> service)
        {
            service.Verify(x => x.GetTranslatedText(It.IsAny<LabelGetTranslatedTextRequest>()));
        }

    }
}
