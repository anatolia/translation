using System.Collections.Generic;
using System.Threading.Tasks;

using Moq;

using Translation.Common.Contracts;
using Translation.Common.Enumerations;
using Translation.Common.Models.Requests.Admin;
using Translation.Common.Models.Requests.Integration.Token;
using Translation.Common.Models.Requests.Journal;
using Translation.Common.Models.Requests.Organization;
using Translation.Common.Models.Requests.SendEmailLog;
using Translation.Common.Models.Requests.User;
using Translation.Common.Models.Requests.User.LoginLog;
using Translation.Common.Models.Responses.Admin;
using Translation.Common.Models.Responses.Integration.Token.RequestLog;
using Translation.Common.Models.Responses.Journal;
using Translation.Common.Models.Responses.Organization;
using Translation.Common.Models.Responses.SendEmailLog;
using Translation.Common.Models.Responses.User;
using Translation.Common.Models.Responses.User.LoginLog;
using static Translation.Tests.TestHelpers.FakeConstantTestHelper;

namespace Translation.Tests.SetupHelpers
{
    public static class AdminServiceSetupHelper
    {

        public static void Setup_ChangeActivation_Returns_UserChangeActivationResponse_Success(this Mock<IAdminService> service)
        {
            service.Setup(x => x.ChangeActivation(It.IsAny<UserChangeActivationRequest>()))
                .ReturnsAsync(new UserChangeActivationResponse { Status = ResponseStatus.Success });
        }

        public static void Setup_ChangeActivation_Returns_UserChangeActivationResponse_Failed(this Mock<IAdminService> service)
        {
            service.Setup(x => x.ChangeActivation(It.IsAny<UserChangeActivationRequest>()))
                .ReturnsAsync(new UserChangeActivationResponse { Status = ResponseStatus.Failed, ErrorMessages = new List<string> { StringOne } });
        }

        public static void Setup_ChangeActivation_Returns_UserChangeActivationResponse_Invalid(this Mock<IAdminService> service)
        {
            service.Setup(x => x.ChangeActivation(It.IsAny<UserChangeActivationRequest>()))
                .ReturnsAsync(new UserChangeActivationResponse { Status = ResponseStatus.Invalid, ErrorMessages = new List<string> { StringOne } });
        }

        public static void Setup_InviteSuperAdminUser_Returns_AdminInviteResponse_Success(this Mock<IAdminService> service)
        {
            service.Setup(x => x.InviteSuperAdminUser(It.IsAny<AdminInviteRequest>()))
                .ReturnsAsync(new AdminInviteResponse { Status = ResponseStatus.Success });
        }

        public static void Setup_InviteSuperAdminUser_Returns_AdminInviteResponse_Failed(this Mock<IAdminService> service)
        {
            service.Setup(x => x.InviteSuperAdminUser(It.IsAny<AdminInviteRequest>()))
                .ReturnsAsync(new AdminInviteResponse { Status = ResponseStatus.Failed, ErrorMessages = new List<string> { StringOne } });
        }

        public static void Setup_InviteSuperAdminUser_Returns_AdminInviteResponse_Invalid(this Mock<IAdminService> service)
        {
            service.Setup(x => x.InviteSuperAdminUser(It.IsAny<AdminInviteRequest>()))
                .ReturnsAsync(new AdminInviteResponse { Status = ResponseStatus.Invalid, ErrorMessages = new List<string> { StringOne } });
        }

        public static void Setup_GetSendEmailLogs_Returns_AllSendEmailReadListResponse_Success(this Mock<IAdminService> service)
        {
            service.Setup(x => x.GetSendEmailLogs(It.IsAny<AllSendEmailLogReadListRequest>()))
                .ReturnsAsync(new AllSendEmailReadListResponse { Status = ResponseStatus.Success });
        }

        public static void Setup_GetSendEmailLogs_Returns_AllSendEmailReadListResponse_Failed(this Mock<IAdminService> service)
        {
            service.Setup(x => x.GetSendEmailLogs(It.IsAny<AllSendEmailLogReadListRequest>()))
                .ReturnsAsync(new AllSendEmailReadListResponse { Status = ResponseStatus.Failed });
        }

        public static void Setup_GetSendEmailLogs_Returns_AllSendEmailReadListResponse_Invalid(this Mock<IAdminService> service)
        {
            service.Setup(x => x.GetSendEmailLogs(It.IsAny<AllSendEmailLogReadListRequest>()))
                .ReturnsAsync(new AllSendEmailReadListResponse { Status = ResponseStatus.Invalid });
        }

        public static void Setup_GetTokenRequestLogs_Returns_AllJournalReadListResponse_Success(this Mock<IAdminService> service)
        {
            service.Setup(x => x.GetTokenRequestLogs(It.IsAny<AllTokenRequestLogReadListRequest>()))
                .ReturnsAsync(new AllTokenRequestLogReadListResponse { Status = ResponseStatus.Success });
        }

        public static void Setup_GetTokenRequestLogs_Returns_AllJournalReadListResponse_Failed(this Mock<IAdminService> service)
        {
            service.Setup(x => x.GetTokenRequestLogs(It.IsAny<AllTokenRequestLogReadListRequest>()))
                .ReturnsAsync(new AllTokenRequestLogReadListResponse { Status = ResponseStatus.Failed });
        }

        public static void Setup_GetTokenRequestLogs_Returns_AllJournalReadListResponse_Invalid(this Mock<IAdminService> service)
        {
            service.Setup(x => x.GetTokenRequestLogs(It.IsAny<AllTokenRequestLogReadListRequest>()))
                .ReturnsAsync(new AllTokenRequestLogReadListResponse { Status = ResponseStatus.Invalid });
        }

        public static void Setup_GetJournals_Returns_JournalReadListResponse_Success(this Mock<IAdminService> service)
        {
            service.Setup(x => x.GetJournals(It.IsAny<AllJournalReadListRequest>()))
                .ReturnsAsync(new JournalReadListResponse { Status = ResponseStatus.Success });
        }

        public static void Setup_GetJournals_Returns_JournalReadListResponse_Failed(this Mock<IAdminService> service)
        {
            service.Setup(x => x.GetJournals(It.IsAny<AllJournalReadListRequest>()))
                .ReturnsAsync(new JournalReadListResponse { Status = ResponseStatus.Failed });
        }

        public static void Setup_GetJournals_Returns_JournalReadListResponse_Invalid(this Mock<IAdminService> service)
        {
            service.Setup(x => x.GetJournals(It.IsAny<AllJournalReadListRequest>()))
                .ReturnsAsync(new JournalReadListResponse { Status = ResponseStatus.Invalid });
        }

        public static void Setup_GetAllUsers_Returns_AllUserReadListResponse_Success(this Mock<IAdminService> service)
        {
            service.Setup(x => x.GetAllUsers(It.IsAny<AllUserReadListRequest>()))
                .ReturnsAsync(new AllUserReadListResponse { Status = ResponseStatus.Success });
        }

        public static void Setup_GetAllUserLoginLogs_Returns_AllLoginLogReadListResponse_Success(this Mock<IAdminService> service)
        {
            service.Setup(x => x.GetAllUserLoginLogs(It.IsAny<AllLoginLogReadListRequest>()))
                .ReturnsAsync(new AllLoginLogReadListResponse() { Status = ResponseStatus.Success });
        }

        public static void Setup_GetAllUserLoginLogs_Returns_AllLoginLogReadListResponse_Failed(this Mock<IAdminService> service)
        {
            service.Setup(x => x.GetAllUserLoginLogs(It.IsAny<AllLoginLogReadListRequest>()))
                .ReturnsAsync(new AllLoginLogReadListResponse() { Status = ResponseStatus.Failed });
        }

        public static void Setup_GetAllUserLoginLogs_Returns_AllLoginLogReadListResponse_Invalid(this Mock<IAdminService> service)
        {
            service.Setup(x => x.GetAllUserLoginLogs(It.IsAny<AllLoginLogReadListRequest>()))
                .ReturnsAsync(new AllLoginLogReadListResponse() { Status = ResponseStatus.Invalid });
        }

        public static void Setup_GetSuperAdmins_Returns_SuperAdminUserReadListResponse_Success(this Mock<IAdminService> service)
        {
            service.Setup(x => x.GetSuperAdmins(It.IsAny<SuperAdminUserReadListRequest>()))
                    .ReturnsAsync(new SuperAdminUserReadListResponse() { Status = ResponseStatus.Success });
        }

        public static void Setup_GetOrganizations_Returns_OrganizationReadListResponse_Success(this Mock<IAdminService> service)
        {
            service.Setup(x => x.GetOrganizations(It.IsAny<OrganizationReadListRequest>()))
                    .ReturnsAsync(new OrganizationReadListResponse { Status = ResponseStatus.Success });
        }

        public static void Setup_GetSuperAdmins_Returns_SuperAdminUserReadListResponse_Failed(this Mock<IAdminService> service)
        {
            service.Setup(x => x.GetSuperAdmins(It.IsAny<SuperAdminUserReadListRequest>()))
                    .ReturnsAsync(new SuperAdminUserReadListResponse() { Status = ResponseStatus.Failed, ErrorMessages = new List<string> { StringOne } });
        }

        public static void Setup_GetAllUsers_Returns_AllUserReadListResponse_Failed(this Mock<IAdminService> service)
        {
            service.Setup(x => x.GetAllUsers(It.IsAny<AllUserReadListRequest>()))
                    .ReturnsAsync(new AllUserReadListResponse { Status = ResponseStatus.Failed, ErrorMessages = new List<string> { StringOne } });
        }

        public static void Setup_GetOrganizations_Returns_OrganizationReadListResponse_Failed(this Mock<IAdminService> service)
        {
            service.Setup(x => x.GetOrganizations(It.IsAny<OrganizationReadListRequest>()))
                    .ReturnsAsync(new OrganizationReadListResponse { Status = ResponseStatus.Failed });
        }

        public static void Setup_GetSuperAdmins_Returns_SuperAdminUserReadListResponse_Invalid(this Mock<IAdminService> service)
        {
            service.Setup(x => x.GetSuperAdmins(It.IsAny<SuperAdminUserReadListRequest>()))
                    .ReturnsAsync(new SuperAdminUserReadListResponse() { Status = ResponseStatus.Invalid, ErrorMessages = new List<string> { StringOne } });
        }

        public static void Setup_GetAllUsers_Returns_AllUserReadListResponse_Invalid(this Mock<IAdminService> service)
        {
            service.Setup(x => x.GetAllUsers(It.IsAny<AllUserReadListRequest>()))
                    .ReturnsAsync(new AllUserReadListResponse { Status = ResponseStatus.Invalid, ErrorMessages = new List<string> { StringOne } });
        }

        public static void Setup_GetOrganizations_Returns_OrganizationReadListResponse_Invalid(this Mock<IAdminService> service)
        {
            service.Setup(x => x.GetOrganizations(It.IsAny<OrganizationReadListRequest>()))
                    .ReturnsAsync(new OrganizationReadListResponse { Status = ResponseStatus.Invalid, ErrorMessages = new List<string> { StringOne } });
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

        public static void Verify_GetAllUserLoginLogs(this Mock<IAdminService> service)
        {
            service.Verify(x => x.GetAllUserLoginLogs(It.IsAny<AllLoginLogReadListRequest>()));
        }

        public static void Verify_GetJournals(this Mock<IAdminService> service)
        {
            service.Verify(x => x.GetJournals(It.IsAny<AllJournalReadListRequest>()));
        }

        public static void Verify_GetTokenRequestLogs(this Mock<IAdminService> service)
        {
            service.Verify(x => x.GetTokenRequestLogs(It.IsAny<AllTokenRequestLogReadListRequest>()));
        }

        public static void Verify_GetSendEmailLogs(this Mock<IAdminService> service)
        {
            service.Verify(x => x.GetSendEmailLogs(It.IsAny<AllSendEmailLogReadListRequest>()));
        }

        public static void Verify_InviteSuperAdminUser(this Mock<IAdminService> service)
        {
            service.Verify(x => x.InviteSuperAdminUser(It.IsAny<AdminInviteRequest>()));
        }

        public static void Verify_ChangeActivation(this Mock<IAdminService> service)
        {
            service.Verify(x => x.ChangeActivation(It.IsAny<UserChangeActivationRequest>()));
        }

    }
}