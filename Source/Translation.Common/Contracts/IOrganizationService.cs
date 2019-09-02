using System.Threading.Tasks;

using Translation.Common.Models.Requests.Organization;
using Translation.Common.Models.Requests.User;
using Translation.Common.Models.Requests.User.LoginLog;
using Translation.Common.Models.Responses.Organization;
using Translation.Common.Models.Responses.User;
using Translation.Common.Models.Responses.User.LoginLog;
using Translation.Common.Models.Shared;

namespace Translation.Common.Contracts
{
    public interface IOrganizationService
    {
        Task<SignUpResponse> CreateOrganizationWithAdmin(SignUpRequest request);

        OrganizationReadResponse GetOrganization(OrganizationReadRequest request);
        Task<OrganizationReadListResponse> GetOrganizations(OrganizationReadListRequest request);
        Task<OrganizationRevisionReadListResponse>
            GetOrganizationRevisions(OrganizationRevisionReadListRequest request);
        Task<OrganizationEditResponse> EditOrganization(OrganizationEditRequest request);
        Task<OrganizationRestoreResponse> RestoreOrganization(OrganizationRestoreRequest request);
        Task<OrganizationPendingTranslationReadListResponse> GetPendingTranslations(OrganizationPendingTranslationReadListRequest request);
        Task<ValidateEmailResponse> ValidateEmail(ValidateEmailRequest request);
        Task<LogOnResponse> LogOn(LogOnRequest request);
        Task<DemandPasswordResetResponse> DemandPasswordReset(DemandPasswordResetRequest request);
        Task<PasswordResetValidateResponse> ValidatePasswordReset(PasswordResetValidateRequest request);
        Task<PasswordResetResponse> PasswordReset(PasswordResetRequest request);
        Task<PasswordChangeResponse> ChangePassword(PasswordChangeRequest request);

        Task<UserChangeActivationResponse> ChangeActivationForUser(UserChangeActivationRequest request);
        Task<UserEditResponse> EditUser(UserEditRequest request);
        Task<UserDeleteResponse> DeleteUser(UserDeleteRequest request);
        
        Task<UserInviteResponse> InviteUser(UserInviteRequest request);
        Task<UserInviteValidateResponse> ValidateUserInvitation(UserInviteValidateRequest request);
        Task<UserAcceptInviteResponse> AcceptInvitation(UserAcceptInviteRequest request);

        UserReadResponse GetUser(UserReadRequest request);
        Task<UserReadListResponse> GetUsers(UserReadListRequest request);
        Task<UserRevisionReadListResponse> GetUserRevisions(UserRevisionReadListRequest request);
        Task<UserLoginLogReadListResponse> GetUserLoginLogs(UserLoginLogReadListRequest request);
        Task<OrganizationLoginLogReadListResponse> GetUserLoginLogsOfOrganization(OrganizationLoginLogReadListRequest request);
        
        CurrentUser GetCurrentUser(CurrentUserRequest request);

        Task<bool> LoadOrganizationsToCache();
        Task<bool> LoadUsersToCache();
        Task<UserRestoreResponse> RestoreUser(UserRestoreRequest request);
    }
}