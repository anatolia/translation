using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;

using Translation.Common.Contracts;
using Translation.Common.Enumerations;
using Translation.Common.Models.Requests.Admin;
using Translation.Common.Models.Requests.Integration.Token;
using Translation.Common.Models.Requests.Journal;
using Translation.Common.Models.Requests.Organization;
using Translation.Common.Models.Requests.User;
using Translation.Common.Models.Responses.Admin;
using Translation.Common.Models.Responses.Integration.Token.RequestLog;
using Translation.Common.Models.Responses.Journal;
using Translation.Common.Models.Responses.Organization;
using Translation.Common.Models.Responses.User;
using static Translation.Tests.TestHelpers.FakeConstantTestHelper;

namespace Translation.Tests.SetupHelpers
{
    public static class AdminServiceSetupHelper
    {
        //public static void Setup_GetAllUserList_Returns_AllUserReadListResponse_Success(this Mock<IAdminService> service)
        //{
        //    service.Setup(x => x.GetAllUserList(It.IsAny<AllUserReadListRequest>()))
        //        .Returns(Task.FromResult(new AllUserReadListResponse { Status = ResponseStatus.Success }));
        //}

        //public static void Setup_InviteAdminUser_Returns_AdminInviteResponse_Success(this Mock<IAdminService> service)
        //{
        //    service.Setup(x => x.InviteAdminUser(It.IsAny<AdminInviteRequest>()))
        //        .Returns(Task.FromResult(new AdminInviteResponse { Status = ResponseStatus.Success }));
        //}

        //public static void Setup_ValidateAdminUserInvitation_Returns_AdminInviteValidateResponse_Success(this Mock<IAdminService> service)
        //{
        //    service.Setup(x => x.ValidateAdminUserInvitation(It.IsAny<AdminInviteValidateRequest>()))
        //        .Returns(Task.FromResult(new AdminInviteValidateResponse { Status = ResponseStatus.Success }));
        //}

        //public static void Setup_AcceptAdminUserInvite_Returns_AdminAcceptInviteResponse_Success(this Mock<IAdminService> service)
        //{
        //    service.Setup(x => x.AcceptAdminUserInvite(It.IsAny<AdminAcceptInviteRequest>()))
        //        .Returns(Task.FromResult(new AdminAcceptInviteResponse { Status = ResponseStatus.Success }));
        //}

        //public static void Setup_GetJournalList_Returns_JournalReadListResponse_Success(this Mock<IAdminService> service)
        //{
        //    service.Setup(x => x.GetJournalList(It.IsAny<AllJournalReadListRequest>()))
        //        .Returns(Task.FromResult(new JournalReadListResponse { Status = ResponseStatus.Success }));
        //}

        //public static void Setup_GetTokenRequestLogList_Returns_AllTokenRequestLogReadListResponse_Success(this Mock<IAdminService> service)
        //{
        //    service.Setup(x => x.GetTokenRequestLogList(It.IsAny<AllTokenRequestLogReadListRequest>()))
        //        .Returns(Task.FromResult(new AllTokenRequestLogReadListResponse { Status = ResponseStatus.Success }));
        //}

        //public static void Setup_GetPermissionLogList_Returns_PermissionLogReadListResponse_Success(this Mock<IAdminService> service)
        //{
        //    service.Setup(x => x.GetPermissionLogList(It.IsAny<AllPermissionLogReadListRequest>()))
        //        .Returns(Task.FromResult(new PermissionLogReadListResponse { Status = ResponseStatus.Success }));
        //}

        //public static void Setup_GetSendEmailLogList_Returns_SendEmailReadListResponse_Success(this Mock<IAdminService> service)
        //{
        //    service.Setup(x => x.GetSendEmailLogList(It.IsAny<SendEmailReadListRequest>()))
        //        .Returns(Task.FromResult(new SendEmailReadListResponse { Status = ResponseStatus.Success }));
        //}

        //public static void Setup_GetUserLoginList_Returns_AllUserLoginLogReadListResponse_Success(this Mock<IAdminService> service)
        //{
        //    service.Setup(x => x.GetUserLoginList(It.IsAny<AllUserLoginLogReadListRequest>()))
        //        .Returns(Task.FromResult(new AllUserLoginLogReadListResponse { Status = ResponseStatus.Success }));
        //}

        //public static void Setup_GetAdminList_Returns_AdminUserReadListResponse_Success(this Mock<IAdminService> service)
        //{
        //    service.Setup(x => x.GetAdminList(It.IsAny<AdminUserReadListRequest>()))
        //        .Returns(Task.FromResult(new AdminUserReadListResponse { Status = ResponseStatus.Success }));
        //}

        //public static void Setup_GetOrganizationList_Returns_OrganizationReadListResponse_Success(this Mock<IAdminService> service)
        //{
        //    service.Setup(x => x.GetOrganizationList(It.IsAny<OrganizationReadListRequest>()))
        //        .Returns(Task.FromResult(new OrganizationReadListResponse { Status = ResponseStatus.Success }));
        //}

        public static void Setup_ChangeActivation_Returns_UserChangeActivationResponse_Success(this Mock<IAdminService> service)
        {
            service.Setup(x => x.ChangeActivation(It.IsAny<UserChangeActivationRequest>()))
                .Returns(Task.FromResult(new UserChangeActivationResponse { Status = ResponseStatus.Success }));
        }

        public static void Setup_OrganizationChangeActivation_Returns_OrganizationChangeActivationResponse_Success(this Mock<IAdminService> service)
        {
            service.Setup(x => x.OrganizationChangeActivation(It.IsAny<OrganizationChangeActivationRequest>()))
                .Returns(Task.FromResult(new OrganizationChangeActivationResponse { Status = ResponseStatus.Success }));
        }

        //public static void Setup_DegradeToUser_Returns_AdminDegradeResponse_Success(this Mock<IAdminService> service)
        //{
        //    service.Setup(x => x.DegradeToUser(It.IsAny<AdminDegradeRequest>()))
        //        .Returns(Task.FromResult(new AdminDegradeResponse { Status = ResponseStatus.Success }));
        //}

        public static void Setup_UpgradeToAdmin_Returns_AdminUpgradeResponse_Success(this Mock<IAdminService> service)
        {
            service.Setup(x => x.UpgradeToAdmin(It.IsAny<AdminUpgradeRequest>()))
                .Returns(Task.FromResult(new AdminUpgradeResponse { Status = ResponseStatus.Success }));
        }

        //public static void Setup_GetAllUserList_Returns_AllUserReadListResponse_Failed(this Mock<IAdminService> service)
        //{
        //    service.Setup(x => x.GetAllUserList(It.IsAny<AllUserReadListRequest>()))
        //        .Returns(Task.FromResult(new AllUserReadListResponse { Status = ResponseStatus.Failed, ErrorMessages = new List<string> { StringOne } }));
        //}

        //public static void Setup_InviteAdminUser_Returns_AdminInviteResponse_Failed(this Mock<IAdminService> service)
        //{
        //    service.Setup(x => x.InviteAdminUser(It.IsAny<AdminInviteRequest>()))
        //        .Returns(Task.FromResult(new AdminInviteResponse { Status = ResponseStatus.Failed, ErrorMessages = new List<string> { StringOne } }));
        //}

        //public static void Setup_ValidateAdminUserInvitation_Returns_AdminInviteValidateResponse_Failed(this Mock<IAdminService> service)
        //{
        //    service.Setup(x => x.ValidateAdminUserInvitation(It.IsAny<AdminInviteValidateRequest>()))
        //        .Returns(Task.FromResult(new AdminInviteValidateResponse { Status = ResponseStatus.Failed, ErrorMessages = new List<string> { StringOne } }));
        //}

        //public static void Setup_AcceptAdminUserInvite_Returns_AdminAcceptInviteResponse_Failed(this Mock<IAdminService> service)
        //{
        //    service.Setup(x => x.AcceptAdminUserInvite(It.IsAny<AdminAcceptInviteRequest>()))
        //        .Returns(Task.FromResult(new AdminAcceptInviteResponse { Status = ResponseStatus.Failed, ErrorMessages = new List<string> { StringOne } }));
        //}

        //public static void Setup_GetJournalList_Returns_JournalReadListResponse_Failed(this Mock<IAdminService> service)
        //{
        //    service.Setup(x => x.GetJournalList(It.IsAny<AllJournalReadListRequest>()))
        //        .Returns(Task.FromResult(new JournalReadListResponse { Status = ResponseStatus.Failed, ErrorMessages = new List<string> { StringOne } }));
        //}

        //public static void Setup_GetTokenRequestLogList_Returns_AllTokenRequestLogReadListResponse_Failed(this Mock<IAdminService> service)
        //{
        //    service.Setup(x => x.GetTokenRequestLogList(It.IsAny<AllTokenRequestLogReadListRequest>()))
        //        .Returns(Task.FromResult(new AllTokenRequestLogReadListResponse { Status = ResponseStatus.Failed, ErrorMessages = new List<string> { StringOne } }));
        //}

        //public static void Setup_GetPermissionLogList_Returns_PermissionLogReadListResponse_Failed(this Mock<IAdminService> service)
        //{
        //    service.Setup(x => x.GetPermissionLogList(It.IsAny<AllPermissionLogReadListRequest>()))
        //        .Returns(Task.FromResult(new PermissionLogReadListResponse { Status = ResponseStatus.Failed, ErrorMessages = new List<string> { StringOne } }));
        //}

        //public static void Setup_GetSendEmailLogList_Returns_SendEmailReadListResponse_Failed(this Mock<IAdminService> service)
        //{
        //    service.Setup(x => x.GetSendEmailLogList(It.IsAny<SendEmailReadListRequest>()))
        //        .Returns(Task.FromResult(new SendEmailReadListResponse { Status = ResponseStatus.Failed, ErrorMessages = new List<string> { StringOne } }));
        //}

        //public static void Setup_GetUserLoginList_Returns_AllUserLoginLogReadListResponse_Failed(this Mock<IAdminService> service)
        //{
        //    service.Setup(x => x.GetUserLoginList(It.IsAny<AllUserLoginLogReadListRequest>()))
        //        .Returns(Task.FromResult(new AllUserLoginLogReadListResponse { Status = ResponseStatus.Failed, ErrorMessages = new List<string> { StringOne } }));
        //}

        //public static void Setup_GetAdminList_Returns_AdminUserReadListResponse_Failed(this Mock<IAdminService> service)
        //{
        //    service.Setup(x => x.GetAdminList(It.IsAny<AdminUserReadListRequest>()))
        //        .Returns(Task.FromResult(new AdminUserReadListResponse { Status = ResponseStatus.Failed, ErrorMessages = new List<string> { StringOne } }));
        //}

        //public static void Setup_GetOrganizationList_Returns_OrganizationReadListResponse_Failed(this Mock<IAdminService> service)
        //{
        //    service.Setup(x => x.GetOrganizationList(It.IsAny<OrganizationReadListRequest>()))
        //        .Returns(Task.FromResult(new OrganizationReadListResponse { Status = ResponseStatus.Failed, ErrorMessages = new List<string> { StringOne } }));
        //}

        public static void Setup_ChangeActivation_Returns_UserChangeActivationResponse_Failed(this Mock<IAdminService> service)
        {
            service.Setup(x => x.ChangeActivation(It.IsAny<UserChangeActivationRequest>()))
                .Returns(Task.FromResult(new UserChangeActivationResponse { Status = ResponseStatus.Failed, ErrorMessages = new List<string> { StringOne } }));
        }

        public static void Setup_OrganizationChangeActivation_Returns_OrganizationChangeActivationResponse_Failed(this Mock<IAdminService> service)
        {
            service.Setup(x => x.OrganizationChangeActivation(It.IsAny<OrganizationChangeActivationRequest>()))
                .Returns(Task.FromResult(new OrganizationChangeActivationResponse { Status = ResponseStatus.Failed, ErrorMessages = new List<string> { StringOne } }));
        }

        //public static void Setup_DegradeToUser_Returns_AdminDegradeResponse_Failed(this Mock<IAdminService> service)
        //{
        //    service.Setup(x => x.DegradeToUser(It.IsAny<AdminDegradeRequest>()))
        //        .Returns(Task.FromResult(new AdminDegradeResponse { Status = ResponseStatus.Failed, ErrorMessages = new List<string> { StringOne } }));
        //}

        public static void Setup_UpgradeToAdmin_Returns_AdminUpgradeResponse_Failed(this Mock<IAdminService> service)
        {
            service.Setup(x => x.UpgradeToAdmin(It.IsAny<AdminUpgradeRequest>()))
                .Returns(Task.FromResult(new AdminUpgradeResponse { Status = ResponseStatus.Failed, ErrorMessages = new List<string> { StringOne } }));
        }

        //public static void Setup_GetAllUserList_Returns_AllUserReadListResponse_Invalid(this Mock<IAdminService> service)
        //{
        //    service.Setup(x => x.GetAllUserList(It.IsAny<AllUserReadListRequest>()))
        //        .Returns(Task.FromResult(new AllUserReadListResponse { Status = ResponseStatus.Invalid, ErrorMessages = new List<string> { StringOne } }));
        //}

        //public static void Setup_InviteAdminUser_Returns_AdminInviteResponse_Invalid(this Mock<IAdminService> service)
        //{
        //    service.Setup(x => x.InviteAdminUser(It.IsAny<AdminInviteRequest>()))
        //        .Returns(Task.FromResult(new AdminInviteResponse { Status = ResponseStatus.Invalid, ErrorMessages = new List<string> { StringOne } }));
        //}

        //public static void Setup_ValidateAdminUserInvitation_Returns_AdminInviteValidateResponse_Invalid(this Mock<IAdminService> service)
        //{
        //    service.Setup(x => x.ValidateAdminUserInvitation(It.IsAny<AdminInviteValidateRequest>()))
        //        .Returns(Task.FromResult(new AdminInviteValidateResponse { Status = ResponseStatus.Invalid, ErrorMessages = new List<string> { StringOne } }));
        //}

        //public static void Setup_AcceptAdminUserInvite_Returns_AdminAcceptInviteResponse_Invalid(this Mock<IAdminService> service)
        //{
        //    service.Setup(x => x.AcceptAdminUserInvite(It.IsAny<AdminAcceptInviteRequest>()))
        //        .Returns(Task.FromResult(new AdminAcceptInviteResponse { Status = ResponseStatus.Invalid, ErrorMessages = new List<string> { StringOne } }));
        //}

        //public static void Setup_GetJournalList_Returns_JournalReadListResponse_Invalid(this Mock<IAdminService> service)
        //{
        //    service.Setup(x => x.GetJournalList(It.IsAny<AllJournalReadListRequest>()))
        //        .Returns(Task.FromResult(new JournalReadListResponse { Status = ResponseStatus.Invalid, ErrorMessages = new List<string> { StringOne } }));
        //}

        //public static void Setup_GetTokenRequestLogList_Returns_AllTokenRequestLogReadListResponse_Invalid(this Mock<IAdminService> service)
        //{
        //    service.Setup(x => x.GetTokenRequestLogList(It.IsAny<AllTokenRequestLogReadListRequest>()))
        //        .Returns(Task.FromResult(new AllTokenRequestLogReadListResponse { Status = ResponseStatus.Invalid, ErrorMessages = new List<string> { StringOne } }));
        //}

        //public static void Setup_GetPermissionLogList_Returns_PermissionLogReadListResponse_Invalid(this Mock<IAdminService> service)
        //{
        //    service.Setup(x => x.GetPermissionLogList(It.IsAny<AllPermissionLogReadListRequest>()))
        //        .Returns(Task.FromResult(new PermissionLogReadListResponse { Status = ResponseStatus.Invalid, ErrorMessages = new List<string> { StringOne } }));
        //}

        //public static void Setup_GetSendEmailLogList_Returns_SendEmailReadListResponse_Invalid(this Mock<IAdminService> service)
        //{
        //    service.Setup(x => x.GetSendEmailLogList(It.IsAny<SendEmailReadListRequest>()))
        //        .Returns(Task.FromResult(new SendEmailReadListResponse { Status = ResponseStatus.Invalid, ErrorMessages = new List<string> { StringOne } }));
        //}

        //public static void Setup_GetUserLoginList_Returns_AllUserLoginLogReadListResponse_Invalid(this Mock<IAdminService> service)
        //{
        //    service.Setup(x => x.GetUserLoginList(It.IsAny<AllUserLoginLogReadListRequest>()))
        //        .Returns(Task.FromResult(new AllUserLoginLogReadListResponse { Status = ResponseStatus.Invalid, ErrorMessages = new List<string> { StringOne } }));
        //}

        //public static void Setup_GetAdminList_Returns_AdminUserReadListResponse_Invalid(this Mock<IAdminService> service)
        //{
        //    service.Setup(x => x.GetAdminList(It.IsAny<AdminUserReadListRequest>()))
        //        .Returns(Task.FromResult(new AdminUserReadListResponse { Status = ResponseStatus.Invalid, ErrorMessages = new List<string> { StringOne } }));
        //}

        //public static void Setup_GetOrganizationList_Returns_OrganizationReadListResponse_Invalid(this Mock<IAdminService> service)
        //{
        //    service.Setup(x => x.GetOrganizationList(It.IsAny<OrganizationReadListRequest>()))
        //        .Returns(Task.FromResult(new OrganizationReadListResponse { Status = ResponseStatus.Invalid, ErrorMessages = new List<string> { StringOne } }));
        //}

        public static void Setup_ChangeActivation_Returns_UserChangeActivationResponse_Invalid(this Mock<IAdminService> service)
        {
            service.Setup(x => x.ChangeActivation(It.IsAny<UserChangeActivationRequest>()))
                .Returns(Task.FromResult(new UserChangeActivationResponse { Status = ResponseStatus.Invalid, ErrorMessages = new List<string> { StringOne } }));
        }

        public static void Setup_OrganizationChangeActivation_Returns_OrganizationChangeActivationResponse_Invalid(this Mock<IAdminService> service)
        {
            service.Setup(x => x.OrganizationChangeActivation(It.IsAny<OrganizationChangeActivationRequest>()))
                .Returns(Task.FromResult(new OrganizationChangeActivationResponse { Status = ResponseStatus.Invalid, ErrorMessages = new List<string> { StringOne } }));
        }

        //public static void Setup_DegradeToUser_Returns_AdminDegradeResponse_Invalid(this Mock<IAdminService> service)
        //{
        //    service.Setup(x => x.DegradeToUser(It.IsAny<AdminDegradeRequest>()))
        //        .Returns(Task.FromResult(new AdminDegradeResponse { Status = ResponseStatus.Invalid, ErrorMessages = new List<string> { StringOne } }));
        //}

        public static void Setup_UpgradeToAdmin_Returns_AdminUpgradeResponse_Invalid(this Mock<IAdminService> service)
        {
            service.Setup(x => x.UpgradeToAdmin(It.IsAny<AdminUpgradeRequest>()))
                .Returns(Task.FromResult(new AdminUpgradeResponse { Status = ResponseStatus.Invalid, ErrorMessages = new List<string> { StringOne } }));
        }

        //public static void Verify_GetAllUserList(this Mock<IAdminService> service)
        //{
        //    service.Setup(x => x.GetAllUserList(It.IsAny<AllUserReadListRequest>()));
        //}

        //public static void Verify_InviteAdminUser(this Mock<IAdminService> service)
        //{
        //    service.Setup(x => x.InviteAdminUser(It.IsAny<AdminInviteRequest>()));
        //}

        //public static void Verify_ValidateAdminUserInvitation(this Mock<IAdminService> service)
        //{
        //    service.Setup(x => x.ValidateAdminUserInvitation(It.IsAny<AdminInviteValidateRequest>()));
        //}

        //public static void Verify_AcceptAdminUserInvite(this Mock<IAdminService> service)
        //{
        //    service.Setup(x => x.AcceptAdminUserInvite(It.IsAny<AdminAcceptInviteRequest>()));
        //}

        //public static void Verify_GetJournalList(this Mock<IAdminService> service)
        //{
        //    service.Setup(x => x.GetJournalList(It.IsAny<AllJournalReadListRequest>()));
        //}

        //public static void Verify_GetTokenRequestLogList(this Mock<IAdminService> service)
        //{
        //    service.Setup(x => x.GetTokenRequestLogList(It.IsAny<AllTokenRequestLogReadListRequest>()));
        //}

        //public static void Verify_GetPermissionLogList(this Mock<IAdminService> service)
        //{
        //    service.Setup(x => x.GetPermissionLogList(It.IsAny<AllPermissionLogReadListRequest>()));
        //}

        //public static void Verify_GetSendEmailLogList(this Mock<IAdminService> service)
        //{
        //    service.Setup(x => x.GetSendEmailLogList(It.IsAny<SendEmailReadListRequest>()));
        //}

        //public static void Verify_GetUserLoginList(this Mock<IAdminService> service)
        //{
        //    service.Setup(x => x.GetUserLoginList(It.IsAny<AllUserLoginLogReadListRequest>()));
        //}

        //public static void Verify_GetAdminList(this Mock<IAdminService> service)
        //{
        //    service.Setup(x => x.GetAdminList(It.IsAny<AdminUserReadListRequest>()));
        //}

        //public static void Verify_GetOrganizationList(this Mock<IAdminService> service)
        //{
        //    service.Setup(x => x.GetOrganizationList(It.IsAny<OrganizationReadListRequest>()));
        //}

        public static void Verify_ChangeActivation(this Mock<IAdminService> service)
        {
            service.Setup(x => x.ChangeActivation(It.IsAny<UserChangeActivationRequest>()));
        }

        public static void Verify_OrganizationChangeActivation(this Mock<IAdminService> service)
        {
            service.Setup(x => x.OrganizationChangeActivation(It.IsAny<OrganizationChangeActivationRequest>()));
        }

        //public static void Verify_DegradeToUser(this Mock<IAdminService> service)
        //{
        //    service.Setup(x => x.DegradeToUser(It.IsAny<AdminDegradeRequest>()));
        //}

        public static void Verify_UpgradeToAdmin(this Mock<IAdminService> service)
        {
            service.Setup(x => x.UpgradeToAdmin(It.IsAny<AdminUpgradeRequest>()));
        }
    }
}