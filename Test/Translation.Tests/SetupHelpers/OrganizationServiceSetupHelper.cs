using System.Collections.Generic;
using System.Threading.Tasks;

using Moq;

using Translation.Common.Contracts;
using Translation.Common.Enumerations;
using Translation.Common.Models.DataTransferObjects;
using Translation.Common.Models.Requests.Organization;
using Translation.Common.Models.Requests.User;
using Translation.Common.Models.Requests.User.LoginLog;
using Translation.Common.Models.Responses.Organization;
using Translation.Common.Models.Responses.User;
using Translation.Common.Models.Responses.User.LoginLog;
using Translation.Common.Models.Shared;
using static Translation.Tests.TestHelpers.FakeConstantTestHelper;

namespace Translation.Tests.SetupHelpers
{
    public static class OrganizationServiceSetupHelper
    {
        public static void Setup_RestoreUser_Returns_UserRestoreResponse_Success(this Mock<IOrganizationService> service)
        {
            service.Setup(x => x.RestoreUser(It.IsAny<UserRestoreRequest>()))
                   .Returns(Task.FromResult(new UserRestoreResponse { Status = ResponseStatus.Success }));
        }

        public static void Setup_RestoreUser_Returns_UserRestoreResponse_Failed(this Mock<IOrganizationService> service)
        {
            service.Setup(x => x.RestoreUser(It.IsAny<UserRestoreRequest>()))
                   .Returns(Task.FromResult(new UserRestoreResponse { Status = ResponseStatus.Failed, ErrorMessages = new List<string> { StringOne } }));
        }

        public static void Setup_RestoreUser_Returns_UserRestoreResponse_Invalid(this Mock<IOrganizationService> service)
        {
            service.Setup(x => x.RestoreUser(It.IsAny<UserRestoreRequest>()))
                   .Returns(Task.FromResult(new UserRestoreResponse { Status = ResponseStatus.Invalid, ErrorMessages = new List<string> { StringOne } }));
        }

        public static void Verify_RestoreUser(this Mock<IOrganizationService> service)
        {
            service.Verify(x => x.RestoreUser(It.IsAny<UserRestoreRequest>()));
        }

        public static void Setup_GetUserRevisions_Returns_UserRevisionReadListResponse_Success(this Mock<IOrganizationService> service)
        {
            var items = new List<RevisionDto<UserDto>>();
            items.Add(new RevisionDto<UserDto>() { RevisionedByUid = UidOne, Revision = One, Item = new UserDto() { Uid = UidOne } });

            service.Setup(x => x.GetUserRevisions(It.IsAny<UserRevisionReadListRequest>()))
                   .Returns(Task.FromResult(new UserRevisionReadListResponse() { Status = ResponseStatus.Success, Items = items }));
        }

        public static void Setup_GetUserRevisions_Returns_UserRevisionReadListResponse_Failed(this Mock<IOrganizationService> service)
        {
            service.Setup(x => x.GetUserRevisions(It.IsAny<UserRevisionReadListRequest>()))
                   .Returns(Task.FromResult(new UserRevisionReadListResponse() { Status = ResponseStatus.Failed, ErrorMessages = new List<string> { StringOne } }));
        }

        public static void Setup_GetUserRevisions_Returns_UserRevisionReadListResponse_Invalid(this Mock<IOrganizationService> service)
        {
            service.Setup(x => x.GetUserRevisions(It.IsAny<UserRevisionReadListRequest>()))
                   .Returns(Task.FromResult(new UserRevisionReadListResponse() { Status = ResponseStatus.Invalid, ErrorMessages = new List<string> { StringOne } }));
        }

        public static void Verify_GetUserRevisions(this Mock<IOrganizationService> service)
        {
            service.Verify(x => x.GetUserRevisions(It.IsAny<UserRevisionReadListRequest>()));
        }

        public static void Setup_GetOrganizationRevisions_Returns_OrganizationRevisionReadListResponse_Success(this Mock<IOrganizationService> service)
        {
            var items = new List<RevisionDto<OrganizationDto>>();
            items.Add(new RevisionDto<OrganizationDto>() { RevisionedByUid = UidOne, Revision = One, Item = new OrganizationDto() { Uid = UidOne } });

            service.Setup(x => x.GetOrganizationRevisions(It.IsAny<OrganizationRevisionReadListRequest>()))
                   .Returns(Task.FromResult(new OrganizationRevisionReadListResponse() { Status = ResponseStatus.Success, Items = items }));
        }

        public static void Setup_GetOrganizationRevisions_Returns_OrganizationRevisionReadListResponse_Failed(this Mock<IOrganizationService> service)
        {
            service.Setup(x => x.GetOrganizationRevisions(It.IsAny<OrganizationRevisionReadListRequest>()))
                   .Returns(Task.FromResult(new OrganizationRevisionReadListResponse() { Status = ResponseStatus.Failed, ErrorMessages = new List<string> { StringOne } }));
        }

        public static void Setup_GetOrganizationRevisions_Returns_OrganizationRevisionReadListResponse_Invalid(this Mock<IOrganizationService> service)
        {
            service.Setup(x => x.GetOrganizationRevisions(It.IsAny<OrganizationRevisionReadListRequest>()))
                   .Returns(Task.FromResult(new OrganizationRevisionReadListResponse() { Status = ResponseStatus.Invalid, ErrorMessages = new List<string> { StringOne } }));
        }

        public static void Verify_GetOrganizationRevisions(this Mock<IOrganizationService> service)
        {
            service.Verify(x => x.GetOrganizationRevisions(It.IsAny<OrganizationRevisionReadListRequest>()));
        }

        public static void Setup_RestoreOrganization_Returns_OrganizationRestoreResponse_Success(this Mock<IOrganizationService> service)
        {
            service.Setup(x => x.RestoreOrganization(It.IsAny<OrganizationRestoreRequest>()))
                   .Returns(Task.FromResult(new OrganizationRestoreResponse { Status = ResponseStatus.Success }));
        }

        public static void Setup_RestoreOrganization_Returns_OrganizationRestoreResponse_Invalid(this Mock<IOrganizationService> service)
        {
            service.Setup(x => x.RestoreOrganization(It.IsAny<OrganizationRestoreRequest>()))
                   .Returns(Task.FromResult(new OrganizationRestoreResponse { Status = ResponseStatus.Invalid, ErrorMessages = new List<string> { StringOne } }));
        }

        public static void Setup_RestoreOrganization_Returns_OrganizationRestoreResponse_Failed(this Mock<IOrganizationService> service)
        {
            service.Setup(x => x.RestoreOrganization(It.IsAny<OrganizationRestoreRequest>()))
                   .Returns(Task.FromResult(new OrganizationRestoreResponse { Status = ResponseStatus.Failed, ErrorMessages = new List<string> { StringOne } }));
        }

        public static void Verify_RestoreOrganization(this Mock<IOrganizationService> service)
        {
            service.Verify(x => x.RestoreOrganization(It.IsAny<OrganizationRestoreRequest>()));
        }

        public static void Setup_GetOrganizations_Returns_OrganizationReadListResponse_Success(this Mock<IOrganizationService> service)
        {
            service.Setup(x => x.GetOrganizations(It.IsAny<OrganizationReadListRequest>()))
                   .Returns(Task.FromResult(new OrganizationReadListResponse { Status = ResponseStatus.Success }));
        }

        public static void Verify_GetOrganizations(this Mock<IOrganizationService> service)
        {
            service.Verify(x => x.GetOrganizations(It.IsAny<OrganizationReadListRequest>()));
        }

        public static void Setup_GetUsers_Returns_UserReadListResponse_Success(this Mock<IOrganizationService> service)
        {
            var items = new List<UserDto>();
            items.Add(new UserDto() { Uid = UidOne });

            service.Setup(x => x.GetUsers(It.IsAny<UserReadListRequest>()))
                   .Returns(Task.FromResult(new UserReadListResponse() { Status = ResponseStatus.Success, Items = items }));
        }

        public static void Setup_GetUserLoginLogsOfOrganization_Returns_OrganizationLoginLogReadListResponse_Success(this Mock<IOrganizationService> service)
        {
            var items = new List<UserLoginLogDto>();
            items.Add(new UserLoginLogDto() { Uid = UidOne });

            service.Setup(x => x.GetUserLoginLogsOfOrganization(It.IsAny<OrganizationLoginLogReadListRequest>()))
                   .Returns(Task.FromResult(new OrganizationLoginLogReadListResponse() { Status = ResponseStatus.Success, Items = items }));
        }

        public static void Setup_GetPendingTranslations_Returns_OrganizationPendingTranslationReadListResponse_Success(this Mock<IOrganizationService> service)
        {
            var items = new List<LabelDto>();
            items.Add(new LabelDto() { Uid = UidOne });

            service.Setup(x => x.GetPendingTranslations(It.IsAny<OrganizationPendingTranslationReadListRequest>()))
                   .Returns(Task.FromResult(new OrganizationPendingTranslationReadListResponse { Status = ResponseStatus.Success, Items = items }));
        }

        public static void Setup_GetCurrentUser_Returns_CurrentUserResponse_Null_Success(this Mock<IOrganizationService> service)
        {
            service.Setup(x => x.GetCurrentUser(It.IsAny<CurrentUserRequest>()))
                .Returns(new CurrentUser());
        }

        public static void Verify_GetCurrentUser(this Mock<IOrganizationService> service)
        {
            service.Verify(x => x.GetCurrentUser(It.IsAny<CurrentUserRequest>()));
               
        }

        public static void Setup_GetOrganization_Returns_OrganizationReadResponse_Success(this Mock<IOrganizationService> service)
        {
            service.Setup(x => x.GetOrganization(It.IsAny<OrganizationReadRequest>()))
                .Returns(new OrganizationReadResponse { Status = ResponseStatus.Success });
        }

        public static void Verify_GetOrganization(this Mock<IOrganizationService> service)
        {
            service.Verify(x => x.GetOrganization(It.IsAny<OrganizationReadRequest>()));
        }

        public static void Setup_CreateOrganizationWithAdmin_Returns_SignUpResponse_Success(this Mock<IOrganizationService> service)
        {
            service.Setup(x => x.CreateOrganizationWithAdmin(It.IsAny<SignUpRequest>()))
                   .Returns(Task.FromResult(new SignUpResponse { Status = ResponseStatus.Success }));
        }

        public static void Setup_CreateOrganizationWithAdmin_Returns_SignUpResponse_Failed(this Mock<IOrganizationService> service)
        {
            service.Setup(x => x.CreateOrganizationWithAdmin(It.IsAny<SignUpRequest>()))
                   .Returns(Task.FromResult(new SignUpResponse { Status = ResponseStatus.Failed, ErrorMessages = new List<string> { StringOne } }));
        }

        public static void Setup_CreateOrganizationWithAdmin_Returns_SignUpResponse_Invalid(this Mock<IOrganizationService> service)
        {
            service.Setup(x => x.CreateOrganizationWithAdmin(It.IsAny<SignUpRequest>()))
                   .Returns(Task.FromResult(new SignUpResponse { Status = ResponseStatus.Invalid, ErrorMessages = new List<string> { StringOne } }));
        }

        public static void Setup_ValidateEmail_Returns_ValidateEmailResponse_Success(this Mock<IOrganizationService> service)
        {
            service.Setup(x => x.ValidateEmail(It.IsAny<ValidateEmailRequest>()))
                   .Returns(Task.FromResult(new ValidateEmailResponse { Status = ResponseStatus.Success }));
        }

        public static void Setup_GetUserLoginLogsOfOrganization_Returns_OrganizationLoginLogReadListResponse_Failed(this Mock<IOrganizationService> service)
        {
            service.Setup(x => x.GetUserLoginLogsOfOrganization(It.IsAny<OrganizationLoginLogReadListRequest>()))
                   .Returns(Task.FromResult(new OrganizationLoginLogReadListResponse() { Status = ResponseStatus.Failed, ErrorMessages = new List<string> { StringOne } }));
        }

        public static void Setup_GetUsers_Returns_UserReadListResponse_Failed(this Mock<IOrganizationService> service)
        {
            service.Setup(x => x.GetUsers(It.IsAny<UserReadListRequest>()))
                   .Returns(Task.FromResult(new UserReadListResponse() { Status = ResponseStatus.Failed, ErrorMessages = new List<string> { StringOne } }));
        }

        public static void Setup_GetPendingTranslations_Returns_OrganizationPendingTranslationReadListResponse_Failed(this Mock<IOrganizationService> service)
        {
            service.Setup(x => x.GetPendingTranslations(It.IsAny<OrganizationPendingTranslationReadListRequest>()))
                   .Returns(Task.FromResult(new OrganizationPendingTranslationReadListResponse { Status = ResponseStatus.Failed, ErrorMessages = new List<string> { StringOne } }));
        }

        public static void Setup_ValidateEmail_Returns_ValidateEmailResponse_Failed(this Mock<IOrganizationService> service)
        {
            service.Setup(x => x.ValidateEmail(It.IsAny<ValidateEmailRequest>()))
                   .Returns(Task.FromResult(new ValidateEmailResponse { Status = ResponseStatus.Failed, ErrorMessages = new List<string> { StringOne } }));
        }

        public static void Setup_ValidateEmail_Returns_ValidateEmailResponse_Invalid(this Mock<IOrganizationService> service)
        {
            service.Setup(x => x.ValidateEmail(It.IsAny<ValidateEmailRequest>()))
                   .Returns(Task.FromResult(new ValidateEmailResponse { Status = ResponseStatus.Invalid, ErrorMessages = new List<string> { StringOne } }));
        }

        public static void Setup_LogOn_Returns_LogOnResponse_Success(this Mock<IOrganizationService> service)
        {
            service.Setup(x => x.LogOn(It.IsAny<LogOnRequest>()))
                   .Returns(Task.FromResult(new LogOnResponse { Status = ResponseStatus.Success, Item = new UserDto { Name = OrganizationOneUserOneName, Email = OrganizationOneUserOneEmail} }));
        }

        public static void Setup_LogOn_Returns_LogOnResponse_Success_SuperAdmin(this Mock<IOrganizationService> service)
        {
            service.Setup(x => x.LogOn(It.IsAny<LogOnRequest>()))
                   .Returns(Task.FromResult(new LogOnResponse { Status = ResponseStatus.Success, Item = new UserDto { Name = OrganizationOneSuperAdminUserOneName, Email = OrganizationOneSuperAdminUserOneEmail, IsSuperAdmin = true } }));
        }

        public static void Setup_LogOn_Returns_LogOnResponse_Invalid(this Mock<IOrganizationService> service)
        {
            service.Setup(x => x.LogOn(It.IsAny<LogOnRequest>()))
                   .Returns(Task.FromResult(new LogOnResponse { Status = ResponseStatus.Invalid, ErrorMessages = new List<string> { StringOne } }));
        }

        public static void Setup_GetUserLoginLogsOfOrganization_Returns_OrganizationLoginLogReadListResponse_Invalid(this Mock<IOrganizationService> service)
        {
            service.Setup(x => x.GetUserLoginLogsOfOrganization(It.IsAny<OrganizationLoginLogReadListRequest>()))
                   .Returns(Task.FromResult(new OrganizationLoginLogReadListResponse() { Status = ResponseStatus.Invalid, ErrorMessages = new List<string> { StringOne } }));
        }

        public static void Setup_LogOn_Returns_LogOnResponse_Failed(this Mock<IOrganizationService> service)
        {
            service.Setup(x => x.LogOn(It.IsAny<LogOnRequest>()))
                   .Returns(Task.FromResult(new LogOnResponse { Status = ResponseStatus.Failed, ErrorMessages = new List<string> { StringOne } }));
        }

        public static void Setup_DemandPasswordReset_Returns_DemandPasswordResetResponse_Success(this Mock<IOrganizationService> service)
        {
            service.Setup(x => x.DemandPasswordReset(It.IsAny<DemandPasswordResetRequest>()))
                   .Returns(Task.FromResult(new DemandPasswordResetResponse { Status = ResponseStatus.Success }));
        }

        public static void Setup_ValidatePasswordReset_Returns_PasswordResetValidateResponse_Success(this Mock<IOrganizationService> service)
        {
            service.Setup(x => x.ValidatePasswordReset(It.IsAny<PasswordResetValidateRequest>()))
                   .Returns(Task.FromResult(new PasswordResetValidateResponse { Status = ResponseStatus.Success }));
        }

        public static void Setup_PasswordReset_Returns_PasswordResetResponse_Success(this Mock<IOrganizationService> service)
        {
            service.Setup(x => x.PasswordReset(It.IsAny<PasswordResetRequest>()))
                   .Returns(Task.FromResult(new PasswordResetResponse { Status = ResponseStatus.Success }));
        }

        public static void Setup_GetUser_Returns_UserReadResponse_Success(this Mock<IOrganizationService> service)
        {
            service.Setup(x => x.GetUser(It.IsAny<UserReadRequest>()))
                   .Returns(new UserReadResponse { Status = ResponseStatus.Success });
        }

        public static void Setup_ChangePassword_Returns_PasswordChangeResponse_Success(this Mock<IOrganizationService> service)
        {
            service.Setup(x => x.ChangePassword(It.IsAny<PasswordChangeRequest>()))
                   .Returns(Task.FromResult(new PasswordChangeResponse { Status = ResponseStatus.Success }));
        }

        public static void Setup_InviteUser_Returns_UserInviteResponse_Success(this Mock<IOrganizationService> service)
        {
            service.Setup(x => x.InviteUser(It.IsAny<UserInviteRequest>()))
                   .Returns(Task.FromResult(new UserInviteResponse { Status = ResponseStatus.Success }));
        }

        public static void Setup_ValidateUserInvitation_Returns_UserInviteValidateResponse_Success(this Mock<IOrganizationService> service)
        {
            service.Setup(x => x.ValidateUserInvitation(It.IsAny<UserInviteValidateRequest>()))
                   .Returns(Task.FromResult(new UserInviteValidateResponse { Status = ResponseStatus.Success }));
        }

        public static void Setup_AcceptInvitation_Returns_UserAcceptInviteResponse_Success(this Mock<IOrganizationService> service)
        {
            service.Setup(x => x.AcceptInvitation(It.IsAny<UserAcceptInviteRequest>()))
                   .Returns(Task.FromResult(new UserAcceptInviteResponse { Status = ResponseStatus.Success }));
        }

        public static void Setup_ChangeActivationForUser_Returns_UserChangeActivationResponse_Success(this Mock<IOrganizationService> service)
        {
            service.Setup(x => x.ChangeActivationForUser(It.IsAny<UserChangeActivationRequest>()))
                   .Returns(Task.FromResult(new UserChangeActivationResponse { Status = ResponseStatus.Success }));
        }

        public static void Setup_EditOrganization_Returns_OrganizationEditResponse_Success(this Mock<IOrganizationService> service)
        {
            service.Setup(x => x.EditOrganization(It.IsAny<OrganizationEditRequest>()))
                   .Returns(Task.FromResult(new OrganizationEditResponse { Status = ResponseStatus.Success }));
        }

        public static void Setup_EditUser_Returns_UserEditResponse_Success(this Mock<IOrganizationService> service)
        {
            service.Setup(x => x.EditUser(It.IsAny<UserEditRequest>()))
                   .Returns(Task.FromResult(new UserEditResponse { Status = ResponseStatus.Success }));
        }

        public static void Setup_DeleteUser_Returns_UserDeleteResponse_Success(this Mock<IOrganizationService> service)
        {
            service.Setup(x => x.DeleteUser(It.IsAny<UserDeleteRequest>()))
                   .Returns(Task.FromResult(new UserDeleteResponse { Status = ResponseStatus.Success }));
        }

        public static void Setup_DemandPasswordReset_Returns_DemandPasswordResetResponse_Failed(this Mock<IOrganizationService> service)
        {
            service.Setup(x => x.DemandPasswordReset(It.IsAny<DemandPasswordResetRequest>()))
                   .Returns(Task.FromResult(new DemandPasswordResetResponse { Status = ResponseStatus.Failed, ErrorMessages = new List<string> { StringOne } }));
        }

        public static void Setup_ValidatePasswordReset_Returns_PasswordResetValidateResponse_Failed(this Mock<IOrganizationService> service)
        {
            service.Setup(x => x.ValidatePasswordReset(It.IsAny<PasswordResetValidateRequest>()))
                   .Returns(Task.FromResult(new PasswordResetValidateResponse { Status = ResponseStatus.Failed, ErrorMessages = new List<string> { StringOne } }));
        }

        public static void Setup_PasswordReset_Returns_PasswordResetResponse_Failed(this Mock<IOrganizationService> service)
        {
            service.Setup(x => x.PasswordReset(It.IsAny<PasswordResetRequest>()))
                   .Returns(Task.FromResult(new PasswordResetResponse { Status = ResponseStatus.Failed, ErrorMessages = new List<string> { StringOne } }));
        }

        public static void Setup_ChangePassword_Returns_PasswordChangeResponse_Failed(this Mock<IOrganizationService> service)
        {
            service.Setup(x => x.ChangePassword(It.IsAny<PasswordChangeRequest>()))
                   .Returns(Task.FromResult(new PasswordChangeResponse { Status = ResponseStatus.Failed, ErrorMessages = new List<string> { StringOne } }));
        }

        public static void Setup_ChangeActivationForUser_Returns_UserChangeActivationResponse_Failed(this Mock<IOrganizationService> service)
        {
            service.Setup(x => x.ChangeActivationForUser(It.IsAny<UserChangeActivationRequest>()))
                   .Returns(Task.FromResult(new UserChangeActivationResponse { Status = ResponseStatus.Failed, ErrorMessages = new List<string> { StringOne } }));
        }

        public static void Setup_GetOrganization_Returns_OrganizationReadResponse_Failed(this Mock<IOrganizationService> service)
        {
            service.Setup(x => x.GetOrganization(It.IsAny<OrganizationReadRequest>()))
                   .Returns(new OrganizationReadResponse { Status = ResponseStatus.Failed, ErrorMessages = new List<string> { StringOne } });
        }

        public static void Setup_EditOrganization_Returns_OrganizationEditResponse_Failed(this Mock<IOrganizationService> service)
        {
            service.Setup(x => x.EditOrganization(It.IsAny<OrganizationEditRequest>()))
                   .Returns(Task.FromResult(new OrganizationEditResponse { Status = ResponseStatus.Failed, ErrorMessages = new List<string> { StringOne } }));
        }

        public static void Setup_EditUser_Returns_UserEditResponse_Failed(this Mock<IOrganizationService> service)
        {
            service.Setup(x => x.EditUser(It.IsAny<UserEditRequest>()))
                   .Returns(Task.FromResult(new UserEditResponse { Status = ResponseStatus.Failed, ErrorMessages = new List<string> { StringOne } }));
        }

        public static void Setup_DeleteUser_Returns_UserDeleteResponse_Failed(this Mock<IOrganizationService> service)
        {
            service.Setup(x => x.DeleteUser(It.IsAny<UserDeleteRequest>()))
                   .Returns(Task.FromResult(new UserDeleteResponse { Status = ResponseStatus.Failed, ErrorMessages = new List<string> { StringOne } }));
        }

        public static void Setup_InviteUser_Returns_UserInviteResponse_Failed(this Mock<IOrganizationService> service)
        {
            service.Setup(x => x.InviteUser(It.IsAny<UserInviteRequest>()))
                   .Returns(Task.FromResult(new UserInviteResponse { Status = ResponseStatus.Failed, ErrorMessages = new List<string> { StringOne } }));
        }

        public static void Setup_ValidateUserInvitation_Returns_UserInviteValidateResponse_Failed(this Mock<IOrganizationService> service)
        {
            service.Setup(x => x.ValidateUserInvitation(It.IsAny<UserInviteValidateRequest>()))
                   .Returns(Task.FromResult(new UserInviteValidateResponse { Status = ResponseStatus.Failed, ErrorMessages = new List<string> { StringOne } }));
        }

        public static void Setup_AcceptInvitation_Returns_UserAcceptInviteResponse_Failed(this Mock<IOrganizationService> service)
        {
            service.Setup(x => x.AcceptInvitation(It.IsAny<UserAcceptInviteRequest>()))
                   .Returns(Task.FromResult(new UserAcceptInviteResponse { Status = ResponseStatus.Failed, ErrorMessages = new List<string> { StringOne } }));
        }

        public static void Setup_GetUser_Returns_UserReadResponse_Failed(this Mock<IOrganizationService> service)
        {
            service.Setup(x => x.GetUser(It.IsAny<UserReadRequest>()))
                   .Returns(new UserReadResponse { Status = ResponseStatus.Failed, ErrorMessages = new List<string> { StringOne } });
        }

        public static void Setup_DemandPasswordReset_Returns_DemandPasswordResetResponse_Invalid(this Mock<IOrganizationService> service)
        {
            service.Setup(x => x.DemandPasswordReset(It.IsAny<DemandPasswordResetRequest>()))
                   .Returns(Task.FromResult(new DemandPasswordResetResponse { Status = ResponseStatus.Invalid, ErrorMessages = new List<string> { StringOne } }));
        }

        public static void Setup_GetPendingTranslations_Returns_OrganizationPendingTranslationReadListResponse_Invalid(this Mock<IOrganizationService> service)
        {
            service.Setup(x => x.GetPendingTranslations(It.IsAny<OrganizationPendingTranslationReadListRequest>()))
                   .Returns(Task.FromResult(new OrganizationPendingTranslationReadListResponse { Status = ResponseStatus.Invalid, ErrorMessages = new List<string> { StringOne } }));
        }

        public static void Setup_GetUsers_Returns_UserReadListResponse_Invalid(this Mock<IOrganizationService> service)
        {
            service.Setup(x => x.GetUsers(It.IsAny<UserReadListRequest>()))
                   .Returns(Task.FromResult(new UserReadListResponse() { Status = ResponseStatus.Invalid, ErrorMessages = new List<string> { StringOne } }));
        }

        public static void Setup_ValidatePasswordReset_Returns_PasswordResetValidateResponse_Invalid(this Mock<IOrganizationService> service)
        {
            service.Setup(x => x.ValidatePasswordReset(It.IsAny<PasswordResetValidateRequest>()))
                   .Returns(Task.FromResult(new PasswordResetValidateResponse { Status = ResponseStatus.Invalid, ErrorMessages = new List<string> { StringOne } }));
        }

        public static void Setup_PasswordReset_Returns_PasswordResetResponse_Invalid(this Mock<IOrganizationService> service)
        {
            service.Setup(x => x.PasswordReset(It.IsAny<PasswordResetRequest>()))
                   .Returns(Task.FromResult(new PasswordResetResponse { Status = ResponseStatus.Invalid, ErrorMessages = new List<string> { StringOne } }));
        }

        public static void Setup_ChangePassword_Returns_PasswordChangeResponse_Invalid(this Mock<IOrganizationService> service)
        {
            service.Setup(x => x.ChangePassword(It.IsAny<PasswordChangeRequest>()))
                   .Returns(Task.FromResult(new PasswordChangeResponse { Status = ResponseStatus.Invalid, ErrorMessages = new List<string> { StringOne } }));
        }

        public static void Setup_ChangeActivationForUser_Returns_UserChangeActivationResponse_Invalid(this Mock<IOrganizationService> service)
        {
            service.Setup(x => x.ChangeActivationForUser(It.IsAny<UserChangeActivationRequest>()))
                   .Returns(Task.FromResult(new UserChangeActivationResponse { Status = ResponseStatus.Invalid, ErrorMessages = new List<string> { StringOne } }));
        }

        public static void Setup_GetOrganization_Returns_OrganizationReadResponse_Invalid(this Mock<IOrganizationService> service)
        {
            service.Setup(x => x.GetOrganization(It.IsAny<OrganizationReadRequest>()))
                   .Returns(new OrganizationReadResponse { Status = ResponseStatus.Invalid, ErrorMessages = new List<string> { StringOne } });
        }

        public static void Setup_EditOrganization_Returns_OrganizationEditResponse_Invalid(this Mock<IOrganizationService> service)
        {
            service.Setup(x => x.EditOrganization(It.IsAny<OrganizationEditRequest>()))
                   .Returns(Task.FromResult(new OrganizationEditResponse { Status = ResponseStatus.Invalid, ErrorMessages = new List<string> { StringOne } }));
        }

        public static void Setup_EditUser_Returns_UserEditResponse_Invalid(this Mock<IOrganizationService> service)
        {
            service.Setup(x => x.EditUser(It.IsAny<UserEditRequest>()))
                   .Returns(Task.FromResult(new UserEditResponse { Status = ResponseStatus.Invalid, ErrorMessages = new List<string> { StringOne } }));
        }

        public static void Setup_DeleteUser_Returns_UserDeleteResponse_Invalid(this Mock<IOrganizationService> service)
        {
            service.Setup(x => x.DeleteUser(It.IsAny<UserDeleteRequest>()))
                   .Returns(Task.FromResult(new UserDeleteResponse { Status = ResponseStatus.Invalid, ErrorMessages = new List<string> { StringOne } }));
        }

        public static void Setup_InviteUser_Returns_UserInviteResponse_Invalid(this Mock<IOrganizationService> service)
        {
            service.Setup(x => x.InviteUser(It.IsAny<UserInviteRequest>()))
                   .Returns(Task.FromResult(new UserInviteResponse { Status = ResponseStatus.Invalid, ErrorMessages = new List<string> { StringOne } }));
        }

        public static void Setup_ValidateUserInvitation_Returns_UserInviteValidateResponse_Invalid(this Mock<IOrganizationService> service)
        {
            service.Setup(x => x.ValidateUserInvitation(It.IsAny<UserInviteValidateRequest>()))
                   .Returns(Task.FromResult(new UserInviteValidateResponse { Status = ResponseStatus.Invalid, ErrorMessages = new List<string> { StringOne } }));
        }

        public static void Setup_AcceptInvitation_Returns_UserAcceptInviteResponse_Invalid(this Mock<IOrganizationService> service)
        {
            service.Setup(x => x.AcceptInvitation(It.IsAny<UserAcceptInviteRequest>()))
                   .Returns(Task.FromResult(new UserAcceptInviteResponse { Status = ResponseStatus.Invalid, ErrorMessages = new List<string> { StringOne } }));
        }

        public static void Setup_GetUser_Returns_UserReadResponse_Invalid(this Mock<IOrganizationService> service)
        {
            service.Setup(x => x.GetUser(It.IsAny<UserReadRequest>()))
                   .Returns(new UserReadResponse { Status = ResponseStatus.Invalid, ErrorMessages = new List<string> { StringOne } });
        }

        public static void Verify_GetUsers(this Mock<IOrganizationService> service)
        {
            service.Verify(x => x.GetUsers(It.IsAny<UserReadListRequest>()));
        }

        public static void Verify_CreateOrganizationWithAdmin(this Mock<IOrganizationService> service)
        {
            service.Verify(x => x.CreateOrganizationWithAdmin(It.IsAny<SignUpRequest>()));
        }

        public static void Verify_GetPendingTranslations(this Mock<IOrganizationService> service)
        {
            service.Verify(x => x.GetPendingTranslations(It.IsAny<OrganizationPendingTranslationReadListRequest>()));
        }

        public static void Verify_GetUserLoginLogsOfOrganization(this Mock<IOrganizationService> service)
        {
            service.Verify(x => x.GetUserLoginLogsOfOrganization(It.IsAny<OrganizationLoginLogReadListRequest>()));
        }

        public static void Verify_ValidateEmail(this Mock<IOrganizationService> service)
        {
            service.Verify(x => x.ValidateEmail(It.IsAny<ValidateEmailRequest>()));
        }

        public static void Verify_LogOn(this Mock<IOrganizationService> service)
        {
            service.Verify(x => x.LogOn(It.IsAny<LogOnRequest>()));
        }

        public static void Verify_DemandPasswordReset(this Mock<IOrganizationService> service)
        {
            service.Verify(x => x.DemandPasswordReset(It.IsAny<DemandPasswordResetRequest>()));
        }

        public static void Verify_ValidatePasswordReset(this Mock<IOrganizationService> service)
        {
            service.Verify(x => x.ValidatePasswordReset(It.IsAny<PasswordResetValidateRequest>()));
        }

        public static void Verify_PasswordReset(this Mock<IOrganizationService> service)
        {
            service.Verify(x => x.PasswordReset(It.IsAny<PasswordResetRequest>()));
        }

        public static void Verify_ChangePassword(this Mock<IOrganizationService> service)
        {
            service.Verify(x => x.ChangePassword(It.IsAny<PasswordChangeRequest>()));
        }

        public static void Verify_ChangeActivationForUser(this Mock<IOrganizationService> service)
        {
            service.Verify(x => x.ChangeActivationForUser(It.IsAny<UserChangeActivationRequest>()));
        }

        public static void Verify_EditOrganization(this Mock<IOrganizationService> service)
        {
            service.Verify(x => x.EditOrganization(It.IsAny<OrganizationEditRequest>()));
        }

        public static void Verify_EditUser(this Mock<IOrganizationService> service)
        {
            service.Verify(x => x.EditUser(It.IsAny<UserEditRequest>()));
        }

        public static void Verify_DeleteUser(this Mock<IOrganizationService> service)
        {
            service.Verify(x => x.DeleteUser(It.IsAny<UserDeleteRequest>()));
        }

        public static void Verify_InviteUser(this Mock<IOrganizationService> service)
        {
            service.Verify(x => x.InviteUser(It.IsAny<UserInviteRequest>()));
        }

        public static void Verify_ValidateUserInvitation(this Mock<IOrganizationService> service)
        {
            service.Verify(x => x.ValidateUserInvitation(It.IsAny<UserInviteValidateRequest>()));
        }

        public static void Verify_AcceptInvitation(this Mock<IOrganizationService> service)
        {
            service.Verify(x => x.AcceptInvitation(It.IsAny<UserAcceptInviteRequest>()));
        }

        public static void Verify_GetUser(this Mock<IOrganizationService> service)
        {
            service.Verify(x => x.GetUser(It.IsAny<UserReadRequest>()));
        }
    }
}