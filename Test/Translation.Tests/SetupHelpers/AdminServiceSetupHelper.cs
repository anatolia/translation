using System.Collections.Generic;
using System.Threading.Tasks;

using Moq;

using Translation.Common.Contracts;
using Translation.Common.Enumerations;
using Translation.Common.Models.Requests.Organization;
using Translation.Common.Models.Requests.User;
using Translation.Common.Models.Responses.Admin;
using Translation.Common.Models.Responses.Organization;
using Translation.Common.Models.Responses.User;
using static Translation.Tests.TestHelpers.FakeConstantTestHelper;

namespace Translation.Tests.SetupHelpers
{
    public static class AdminServiceSetupHelper
    {
        public static void Setup_GetAllUsers_Returns_AllUserReadListResponse_Success(this Mock<IAdminService> service)
        {
            service.Setup(x => x.GetAllUsers(It.IsAny<AllUserReadListRequest>()))
                   .Returns(Task.FromResult(new AllUserReadListResponse { Status = ResponseStatus.Success }));
        }

        public static void Setup_GetSuperAdmins_Returns_SuperAdminUserReadListResponse_Success(this Mock<IAdminService> service)
        {
            service.Setup(x => x.GetSuperAdmins(It.IsAny<SuperAdminUserReadListRequest>()))
                   .Returns(Task.FromResult(new SuperAdminUserReadListResponse() { Status = ResponseStatus.Success }));
        }

        public static void Setup_GetOrganizations_Returns_OrganizationReadListResponse_Success(this Mock<IAdminService> service)
        {
            service.Setup(x => x.GetOrganizations(It.IsAny<OrganizationReadListRequest>()))
                   .Returns(Task.FromResult(new OrganizationReadListResponse { Status = ResponseStatus.Success }));
        }

        public static void Setup_GetSuperAdmins_Returns_SuperAdminUserReadListResponse_Failed(this Mock<IAdminService> service)
        {
            service.Setup(x => x.GetSuperAdmins(It.IsAny<SuperAdminUserReadListRequest>()))
                   .Returns(Task.FromResult(new SuperAdminUserReadListResponse() { Status = ResponseStatus.Failed, ErrorMessages = new List<string> { StringOne } }));
        }

        public static void Setup_GetAllUsers_Returns_AllUserReadListResponse_Failed(this Mock<IAdminService> service)
        {
            service.Setup(x => x.GetAllUsers(It.IsAny<AllUserReadListRequest>()))
                   .Returns(Task.FromResult(new AllUserReadListResponse { Status = ResponseStatus.Failed, ErrorMessages = new List<string> { StringOne } }));
        }

        public static void Setup_GetOrganizations_Returns_OrganizationReadListResponse_Failed(this Mock<IAdminService> service)
        {
            service.Setup(x => x.GetOrganizations(It.IsAny<OrganizationReadListRequest>()))
                   .Returns(Task.FromResult(new OrganizationReadListResponse { Status = ResponseStatus.Failed, ErrorMessages = new List<string> { StringOne } }));
        }

        public static void Setup_GetSuperAdmins_Returns_SuperAdminUserReadListResponse_Invalid(this Mock<IAdminService> service)
        {
            service.Setup(x => x.GetSuperAdmins(It.IsAny<SuperAdminUserReadListRequest>()))
                   .Returns(Task.FromResult(new SuperAdminUserReadListResponse() { Status = ResponseStatus.Invalid, ErrorMessages = new List<string> { StringOne } }));
        }

        public static void Setup_GetAllUsers_Returns_AllUserReadListResponse_Invalid(this Mock<IAdminService> service)
        {
            service.Setup(x => x.GetAllUsers(It.IsAny<AllUserReadListRequest>()))
                   .Returns(Task.FromResult(new AllUserReadListResponse { Status = ResponseStatus.Invalid, ErrorMessages = new List<string> { StringOne } }));
        }

        public static void Setup_GetOrganizations_Returns_OrganizationReadListResponse_Invalid(this Mock<IAdminService> service)
        {
            service.Setup(x => x.GetOrganizations(It.IsAny<OrganizationReadListRequest>()))
                   .Returns(Task.FromResult(new OrganizationReadListResponse { Status = ResponseStatus.Invalid, ErrorMessages = new List<string> { StringOne } }));
        }

        public static void Verify_GetAllUsers(this Mock<IAdminService> service)
        {
            service.Setup(x => x.GetAllUsers(It.IsAny<AllUserReadListRequest>()));
        }

        public static void Verify_GetSuperAdmins(this Mock<IAdminService> service)
        {
            service.Setup(x => x.GetSuperAdmins(It.IsAny<SuperAdminUserReadListRequest>()));
        }

        public static void Verify_GetOrganizations(this Mock<IAdminService> service)
        {
            service.Setup(x => x.GetOrganizations(It.IsAny<OrganizationReadListRequest>()));
        }
    }
}