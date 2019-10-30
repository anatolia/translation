using System.Collections.Generic;

using Moq;

using Translation.Common.Contracts;
using Translation.Common.Enumerations;
using Translation.Common.Models.DataTransferObjects;
using Translation.Common.Models.Requests.TranslationProvider;
using Translation.Common.Models.Responses.TranslationProvider;
using static Translation.Client.Web.Unit.Tests.TestHelpers.FakeModelTestHelper;
using static Translation.Common.Tests.TestHelpers.FakeDtoTestHelper;

namespace Translation.Client.Web.Unit.Tests.ServiceSetupHelpers
{
    public static class TranslationProviderServiceSetupHelper
    {

        public static void Setup_EditTranslationProvider_Returns_TranslationProviderEditResponse_Success(this Mock<ITranslationProviderService> service)
        {
            var item = GetTranslationProviderDto();
            service.Setup(x => x.EditTranslationProvider(It.IsAny<TranslationProviderEditRequest>()))
                .ReturnsAsync(new TranslationProviderEditResponse() { Status = ResponseStatus.Success, Item = item });
        }

        public static void Setup_EditTranslationProvider_Returns_TranslationProviderEditResponse_Failed(this Mock<ITranslationProviderService> service)
        {
            service.Setup(x => x.EditTranslationProvider(It.IsAny<TranslationProviderEditRequest>()))
                .ReturnsAsync(new TranslationProviderEditResponse() { Status = ResponseStatus.Failed });
        }

        public static void Setup_EditTranslationProvider_Returns_TranslationProviderEditResponse_Invalid(this Mock<ITranslationProviderService> service)
        {
            service.Setup(x => x.EditTranslationProvider(It.IsAny<TranslationProviderEditRequest>()))
                .ReturnsAsync(new TranslationProviderEditResponse() { Status = ResponseStatus.Invalid });
        }

        public static void Setup_GetTranslationProvider_Returns_TranslationProviderReadResponse_Success(this Mock<ITranslationProviderService> service)
        {
            service.Setup(x => x.GetTranslationProvider(It.IsAny<TranslationProviderReadRequest>()))
                .ReturnsAsync(new TranslationProviderReadResponse() { Status = ResponseStatus.Success });
        }

        public static void Setup_GetTranslationProvider_Returns_TranslationProviderReadResponse_Failed(this Mock<ITranslationProviderService> service)
        {
            service.Setup(x => x.GetTranslationProvider(It.IsAny<TranslationProviderReadRequest>()))
                .ReturnsAsync(new TranslationProviderReadResponse() { Status = ResponseStatus.Failed });
        }

        public static void Setup_GetTranslationProvider_Returns_TranslationProviderReadResponse_Invalid(this Mock<ITranslationProviderService> service)
        {
            service.Setup(x => x.GetTranslationProvider(It.IsAny<TranslationProviderReadRequest>()))
                .ReturnsAsync(new TranslationProviderReadResponse() { Status = ResponseStatus.Invalid });
        }

        public static void Setup_GetActiveTranslationProvider_Returns_ActiveTranslationProvider(this Mock<ITranslationProviderService> service)
        {
            service.Setup(x => x.GetActiveTranslationProvider(It.IsAny<ActiveTranslationProviderRequest>()))
                .Returns(GetActiveTranslationProvider());
        }

        public static void Setup_GetTranslationProviders_Returns_TranslationProviderReadListResponse_Success(this Mock<ITranslationProviderService> service)
        {
            var items = new List<TranslationProviderDto>() { GetTranslationProviderDto() };
            service.Setup(x => x.GetTranslationProviders(It.IsAny<TranslationProviderReadListRequest>()))
                .ReturnsAsync(new TranslationProviderReadListResponse() { Status = ResponseStatus.Success, Items = items });
        }

        public static void Setup_GetTranslationProviders_Returns_TranslationProviderReadListResponse_Failed(this Mock<ITranslationProviderService> service)
        {
            service.Setup(x => x.GetTranslationProviders(It.IsAny<TranslationProviderReadListRequest>()))
                .ReturnsAsync(new TranslationProviderReadListResponse() { Status = ResponseStatus.Failed });
        }

        public static void Setup_GetActiveTranslationProvider_Returns_TranslationProviderReadListResponse_Invalid(this Mock<ITranslationProviderService> service)
        {
            service.Setup(x => x.GetTranslationProviders(It.IsAny<TranslationProviderReadListRequest>()))
                .ReturnsAsync(new TranslationProviderReadListResponse() { Status = ResponseStatus.Invalid });
        }

        public static void Verify_GetActiveTranslationProvider(this Mock<ITranslationProviderService> service)
        {
            service.Verify(x => x.GetActiveTranslationProvider(It.IsAny<ActiveTranslationProviderRequest>()));
        }

        public static void Verify_GetTranslationProviders(this Mock<ITranslationProviderService> service)
        {
            service.Verify(x => x.GetTranslationProviders(It.IsAny<TranslationProviderReadListRequest>()));
        }

        public static void Verify_GetTranslationProvider(this Mock<ITranslationProviderService> service)
        {
            service.Verify(x => x.GetTranslationProvider(It.IsAny<TranslationProviderReadRequest>()));
        }

        public static void Verify_EditTranslationProvider(this Mock<ITranslationProviderService> service)
        {
            service.Verify(x => x.EditTranslationProvider(It.IsAny<TranslationProviderEditRequest>()));
        }

    }
}