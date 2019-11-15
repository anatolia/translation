using Moq;

using StandardUtils.Enumerations;

using Translation.Common.Contracts;
using Translation.Common.Models.DataTransferObjects;
using Translation.Common.Models.Requests.Label;
using Translation.Common.Models.Responses.Label;

using static Translation.Common.Tests.TestHelpers.FakeConstantTestHelper;

namespace Translation.Common.Tests.CommonForServiceAndController
{
    public static class MockCloudTranslationServiceSetupHelper
    {
        public static void Setup_GetTranslatedText_Returns_LabelGetTranslatedTextResponse_Success(this Mock<ITextTranslateIntegration> service)
        {
            var item = new LabelGetTranslated { Name = StringTwo };
            service.Setup(x => x.GetTranslatedText(It.IsAny<LabelGetTranslatedTextRequest>()))
                   .ReturnsAsync(new LabelGetTranslatedTextResponse { Status = ResponseStatus.Success, Item =item});
        }

        public static void Setup_GetTranslatedText_Returns_LabelGetTranslatedTextResponse_Failed(this Mock<ITextTranslateIntegration> service)
        {
           
            service.Setup(x => x.GetTranslatedText(It.IsAny<LabelGetTranslatedTextRequest>()))
                   .ReturnsAsync(new LabelGetTranslatedTextResponse { Status = ResponseStatus.Failed});
        }

        public static void Setup_GetTranslatedText_Returns_LabelGetTranslatedTextResponse_Invalid(this Mock<ITextTranslateIntegration> service)
        {
            service.Setup(x => x.GetTranslatedText(It.IsAny<LabelGetTranslatedTextRequest>()))
                   .ReturnsAsync(new LabelGetTranslatedTextResponse { Status = ResponseStatus.Invalid });
        }

        public static void Verify_GetTranslatedText(this Mock<ITextTranslateIntegration> service)
        {
            service.Verify(x => x.GetTranslatedText(It.IsAny<LabelGetTranslatedTextRequest>()));
        }

    }
}
