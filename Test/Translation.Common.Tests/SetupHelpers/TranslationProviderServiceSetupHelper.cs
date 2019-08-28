using Moq;

using Translation.Common.Contracts;
using Translation.Common.Models.Requests.TranslationProvider;
using static Translation.Common.Tests.TestHelpers.FakeModelTestHelper;

namespace Translation.Common.Tests.SetupHelpers
{
    public static class TranslationProviderServiceSetupHelper
    {

        public static void Setup_GetActiveTranslationProvider_Returns_ActiveTranslationProvider(this Mock<ITranslationProviderService> service)
        {
            service.Setup(x => x.GetActiveTranslationProvider(It.IsAny<ActiveTranslationProviderRequest>()))
                .Returns(GetActiveTranslationProvider());
        }

        public static void Verify_GetActiveTranslationProvider(this Mock<ITranslationProviderService> service)
        {
            service.Verify(x => x.GetActiveTranslationProvider(It.IsAny<ActiveTranslationProviderRequest>()));
        }

    }
}