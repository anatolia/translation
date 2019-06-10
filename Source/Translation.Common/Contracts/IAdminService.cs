using System.Threading.Tasks;

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

namespace Translation.Common.Contracts
{
    public interface IAdminService
    {
        Task<OrganizationReadListResponse> GetOrganizations(OrganizationReadListRequest request);
        
        Task<AllUserReadListResponse> GetAllUsers(AllUserReadListRequest request);
        Task<SuperAdminUserReadListResponse> GetSuperAdmins(SuperAdminUserReadListRequest request);
        
        Task<AdminInviteResponse> InviteSuperAdminUser(AdminInviteRequest request);
        Task<AdminInviteValidateResponse> ValidateSuperAdminUserInvitation(AdminInviteValidateRequest request);
        Task<AdminAcceptInviteResponse> AcceptSuperAdminUserInvite(AdminAcceptInviteRequest request);

        Task<UserChangeActivationResponse> ChangeActivation(UserChangeActivationRequest request);
        Task<OrganizationChangeActivationResponse> OrganizationChangeActivation(OrganizationChangeActivationRequest request);
        Task<AdminDemoteResponse> DemoteToUser(AdminDemoteRequest request);
        Task<AdminUpgradeResponse> UpgradeToAdmin(AdminUpgradeRequest request);

        Task<JournalReadListResponse> GetJournals(AllJournalReadListRequest request);
        Task<AllTokenRequestLogReadListResponse> GetTokenRequestLogs(AllTokenRequestLogReadListRequest request);
        Task<AllSendEmailReadListResponse> GetSendEmailLogs(AllSendEmailLogReadListRequest request);
        Task<AllLoginLogReadListResponse> GetAllUserLoginLogs(AllLoginLogReadListRequest request);
        
        
        
     
    }
}