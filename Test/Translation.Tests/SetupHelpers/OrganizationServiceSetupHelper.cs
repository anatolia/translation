using Moq;

using Translation.Common.Contracts;
using Translation.Common.Enumerations;
using Translation.Common.Models.Requests.Organization;
using Translation.Common.Models.Responses.Organization;

namespace Translation.Tests.SetupHelpers
{
    public static class OrganizationServiceSetupHelper
    {
        public static void Setup_GetOrganization_Returns_OrganizationReadResponse_Success(this Mock<IOrganizationService> service)
        {
            service.Setup(x => x.GetOrganization(It.IsAny<OrganizationReadRequest>())).Returns(new OrganizationReadResponse { Status = ResponseStatus.Success });
        }

        public static void Verify_GetOrganization(this Mock<IOrganizationService> service)
        {
            service.Verify(x => x.GetOrganization(It.IsAny<OrganizationReadRequest>()));
        }
    }
}