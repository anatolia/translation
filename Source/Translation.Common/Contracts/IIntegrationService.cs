using System.Threading.Tasks;

using Translation.Common.Models.Requests.Integration;
using Translation.Common.Models.Requests.Integration.IntegrationClient;
using Translation.Common.Models.Requests.Integration.Token;
using Translation.Common.Models.Responses.Integration;
using Translation.Common.Models.Responses.Integration.IntegrationClient;
using Translation.Common.Models.Responses.Integration.Token;
using Translation.Common.Models.Responses.Integration.Token.RequestLog;

namespace Translation.Common.Contracts
{
    public interface IIntegrationService
    {
        Task<IntegrationCreateResponse> CreateIntegration(IntegrationCreateRequest request);
        Task<IntegrationReadResponse> GetIntegration(IntegrationReadRequest request);
        Task<IntegrationRevisionReadListResponse> GetIntegrationRevisions(IntegrationRevisionReadListRequest request);
        Task<IntegrationReadListResponse> GetIntegrations(IntegrationReadListRequest request);
        Task<IntegrationEditResponse> EditIntegration(IntegrationEditRequest request);
        Task<IntegrationDeleteResponse> DeleteIntegration(IntegrationDeleteRequest request);
        Task<IntegrationChangeActivationResponse> ChangeActivationForIntegration(IntegrationChangeActivationRequest request);


        Task<IntegrationClientCreateResponse> CreateIntegrationClient(IntegrationClientCreateRequest request);
        Task<IntegrationClientReadResponse> GetIntegrationClient(IntegrationClientReadRequest request);
        Task<IntegrationClientReadListResponse> GetIntegrationClients(IntegrationClientReadListRequest request);
        Task<IntegrationClientRefreshResponse> RefreshIntegrationClient(IntegrationClientRefreshRequest request);
        Task<IntegrationClientDeleteResponse> DeleteIntegrationClient(IntegrationClientDeleteRequest request);
        Task<IntegrationClientChangeActivationResponse> ChangeActivationForIntegrationClient(IntegrationClientChangeActivationRequest request);
        Task<IntegrationRestoreResponse> RestoreIntegration(IntegrationRestoreRequest request);
        Task<TokenCreateResponse> CreateToken(TokenCreateRequest request);
        Task<TokenCreateResponse> CreateTokenWhenUserAuthenticated(TokenGetRequest request);
        Task<TokenRevokeResponse> RevokeToken(TokenRevokeRequest request);
        Task<TokenValidateResponse> ValidateToken(TokenValidateRequest request);
        Task<OrganizationActiveTokenReadListResponse> GetActiveTokensOfOrganization(OrganizationActiveTokenReadListRequest request);
        Task<IntegrationActiveTokenReadListResponse> GetActiveTokensOfIntegration(IntegrationActiveTokenReadListRequest request);
        Task<IntegrationClientActiveTokenReadListResponse> GetActiveTokensOfIntegrationClient(IntegrationClientActiveTokenReadListRequest request);

        Task<OrganizationTokenRequestLogReadListResponse> GetTokenRequestLogsOfOrganization(OrganizationTokenRequestLogReadListRequest request);
        Task<IntegrationTokenRequestLogReadListResponse> GetTokenRequestLogsOfIntegration(IntegrationTokenRequestLogReadListRequest request);
        Task<IntegrationClientTokenRequestLogReadListResponse> GetTokenRequestLogsOfIntegrationClient(IntegrationClientTokenRequestLogReadListRequest request);

        Task<AllActiveTokenReadListResponse> GetAllActiveTokens(AllActiveTokenReadListRequest request);
        Task<AllTokenRequestLogReadListResponse> GetAllTokenRequestLogs(AllTokenRequestLogReadListRequest request);
    }
}