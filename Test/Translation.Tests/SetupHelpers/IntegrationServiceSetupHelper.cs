using System.Collections.Generic;
using System.Threading.Tasks;

using Moq;

using Translation.Common.Contracts;
using Translation.Common.Enumerations;
using Translation.Common.Models.DataTransferObjects;
using Translation.Common.Models.Requests.Integration;
using Translation.Common.Models.Requests.Integration.IntegrationClient;
using Translation.Common.Models.Requests.Integration.Token;
using Translation.Common.Models.Responses.Integration;
using Translation.Common.Models.Responses.Integration.IntegrationClient;
using Translation.Common.Models.Responses.Integration.Token;
using Translation.Common.Models.Responses.Integration.Token.RequestLog;
using static Translation.Tests.TestHelpers.FakeConstantTestHelper;

namespace Translation.Tests.SetupHelpers
{
    public static class IntegrationServiceSetupHelper
    {
        public static void Setup_GetIntegrationRevisions_Returns_IntegrationRevisionReadListResponse_Failed(this Mock<IIntegrationService> service)
        {
            service.Setup(x => x.GetIntegrationRevisions(It.IsAny<IntegrationRevisionReadListRequest>()))
                .Returns(Task.FromResult(new IntegrationRevisionReadListResponse() { Status = ResponseStatus.Failed, ErrorMessages = new List<string> { StringOne } }));
        }

        public static void Setup_GetIntegrations_Returns_IntegrationReadListResponse_Success(this Mock<IIntegrationService> service)
        {
            service.Setup(x => x.GetIntegrations(It.IsAny<IntegrationReadListRequest>()))
                   .Returns(Task.FromResult(new IntegrationReadListResponse { Status = ResponseStatus.Success }));
        }

        public static void Setup_GetIntegration_Returns_IntegrationReadResponse_Success(this Mock<IIntegrationService> service)
        {
            service.Setup(x => x.GetIntegration(It.IsAny<IntegrationReadRequest>()))
                   .Returns(Task.FromResult(new IntegrationReadResponse { Status = ResponseStatus.Success }));
        }

        public static void Setup_CreateIntegration_Returns_IntegrationCreateResponse_Success(this Mock<IIntegrationService> service)
        {
            service.Setup(x => x.CreateIntegration(It.IsAny<IntegrationCreateRequest>()))
                   .Returns(Task.FromResult(new IntegrationCreateResponse { Status = ResponseStatus.Success }));
        }

        public static void Setup_EditIntegration_Returns_IntegrationEditResponse_Success(this Mock<IIntegrationService> service)
        {
            service.Setup(x => x.EditIntegration(It.IsAny<IntegrationEditRequest>()))
                   .Returns(Task.FromResult(new IntegrationEditResponse { Status = ResponseStatus.Success, Item = new IntegrationDto() { Uid = UidOne } }));
        }
        
        public static void Setup_DeleteIntegration_Returns_IntegrationDeleteResponse_Success(this Mock<IIntegrationService> service)
        {
            service.Setup(x => x.DeleteIntegration(It.IsAny<IntegrationDeleteRequest>()))
                   .Returns(Task.FromResult(new IntegrationDeleteResponse { Status = ResponseStatus.Success }));
        }

        public static void Setup_RestoreIntegration_Returns_IntegrationRestoreResponse_Success(this Mock<IIntegrationService> service)
        {
            service.Setup(x => x.RestoreIntegration(It.IsAny<IntegrationRestoreRequest>()))
                .Returns(Task.FromResult(new IntegrationRestoreResponse { Status = ResponseStatus.Success }));
        }

        public static void Setup_RestoreIntegration_Returns_IntegrationRestoreResponse_Failed(this Mock<IIntegrationService> service)
        {
            service.Setup(x => x.RestoreIntegration(It.IsAny<IntegrationRestoreRequest>()))
                .Returns(Task.FromResult(new IntegrationRestoreResponse { Status = ResponseStatus.Failed, ErrorMessages = new List<string> { StringOne } }));
        }

        public static void Setup_ChangeActivationForIntegration_Returns_IntegrationChangeActivationResponse_Success(this Mock<IIntegrationService> service)
        {
            service.Setup(x => x.ChangeActivationForIntegration(It.IsAny<IntegrationChangeActivationRequest>()))
                   .Returns(Task.FromResult(new IntegrationChangeActivationResponse { Status = ResponseStatus.Success }));
        }

        public static void Setup_CreateIntegrationClient_Returns_IntegrationClientCreateResponse_Success(this Mock<IIntegrationService> service)
        {
            service.Setup(x => x.CreateIntegrationClient(It.IsAny<IntegrationClientCreateRequest>()))
                   .Returns(Task.FromResult(new IntegrationClientCreateResponse { Status = ResponseStatus.Success }));
        }

        public static void Setup_RefreshIntegrationClient_Returns_IntegrationClientRefreshResponse_Success(this Mock<IIntegrationService> service)
        {
            service.Setup(x => x.RefreshIntegrationClient(It.IsAny<IntegrationClientRefreshRequest>()))
                   .Returns(Task.FromResult(new IntegrationClientRefreshResponse { Status = ResponseStatus.Success }));
        }

        public static void Setup_DeleteIntegrationClient_Returns_IntegrationClientDeleteResponse_Success(this Mock<IIntegrationService> service)
        {
            service.Setup(x => x.DeleteIntegrationClient(It.IsAny<IntegrationClientDeleteRequest>()))
                   .Returns(Task.FromResult(new IntegrationClientDeleteResponse { Status = ResponseStatus.Success }));
        }

        public static void Setup_ChangeActivationForIntegrationClient_Returns_IntegrationClientChangeActivationResponse_Success(this Mock<IIntegrationService> service)
        {
            service.Setup(x => x.ChangeActivationForIntegrationClient(It.IsAny<IntegrationClientChangeActivationRequest>()))
                   .Returns(Task.FromResult(new IntegrationClientChangeActivationResponse { Status = ResponseStatus.Success }));
        }

        public static void Setup_GetIntegrationClient_Returns_IntegrationClientReadResponse_Success(this Mock<IIntegrationService> service)
        {
            service.Setup(x => x.GetIntegrationClient(It.IsAny<IntegrationClientReadRequest>()))
                   .Returns(Task.FromResult(new IntegrationClientReadResponse { Status = ResponseStatus.Success }));
        }

        public static void Setup_CreateToken_Returns_TokenCreateResponse_Success(this Mock<IIntegrationService> service)
        {
            service.Setup(x => x.CreateToken(It.IsAny<TokenCreateRequest>()))
                   .Returns(Task.FromResult(new TokenCreateResponse { Status = ResponseStatus.Success }));
        }

        public static void Setup_RevokeToken_Returns_TokenRevokeResponse_Success(this Mock<IIntegrationService> service)
        {
            service.Setup(x => x.RevokeToken(It.IsAny<TokenRevokeRequest>()))
                   .Returns(Task.FromResult(new TokenRevokeResponse { Status = ResponseStatus.Success }));
        }

        public static void Setup_GetActiveTokensOfOrganization_Returns_OrganizationActiveTokenReadListResponse_Success(this Mock<IIntegrationService> service)
        {
            service.Setup(x => x.GetActiveTokensOfOrganization(It.IsAny<OrganizationActiveTokenReadListRequest>()))
                   .Returns(Task.FromResult(new OrganizationActiveTokenReadListResponse { Status = ResponseStatus.Success }));
        }

        public static void Setup_GetActiveTokensOfIntegration_Returns_IntegrationActiveTokenReadListResponse_Success(this Mock<IIntegrationService> service)
        {
            service.Setup(x => x.GetActiveTokensOfIntegration(It.IsAny<IntegrationActiveTokenReadListRequest>()))
                   .Returns(Task.FromResult(new IntegrationActiveTokenReadListResponse { Status = ResponseStatus.Success }));
        }

        public static void Setup_GetActiveTokensOfIntegrationClient_Returns_IntegrationClientActiveTokenReadListResponse_Success(this Mock<IIntegrationService> service)
        {
            service.Setup(x => x.GetActiveTokensOfIntegrationClient(It.IsAny<IntegrationClientActiveTokenReadListRequest>()))
                   .Returns(Task.FromResult(new IntegrationClientActiveTokenReadListResponse { Status = ResponseStatus.Success }));
        }

        public static void Setup_GetAllTokenRequestLogs_Returns_AllTokenRequestLogReadListResponse_Success(this Mock<IIntegrationService> service)
        {
            service.Setup(x => x.GetAllTokenRequestLogs(It.IsAny<AllTokenRequestLogReadListRequest>()))
                   .Returns(Task.FromResult(new AllTokenRequestLogReadListResponse { Status = ResponseStatus.Success }));
        }

        public static void Setup_GetTokenRequestLogsOfOrganization_Returns_OrganizationTokenRequestLogReadListResponse_Success(this Mock<IIntegrationService> service)
        {
            service.Setup(x => x.GetTokenRequestLogsOfOrganization(It.IsAny<OrganizationTokenRequestLogReadListRequest>()))
                   .Returns(Task.FromResult(new OrganizationTokenRequestLogReadListResponse { Status = ResponseStatus.Success }));
        }

        public static void Setup_GetTokenRequestLogsOfIntegration_Returns_IntegrationTokenRequestLogReadListResponse_Success(this Mock<IIntegrationService> service)
        {
            service.Setup(x => x.GetTokenRequestLogsOfIntegration(It.IsAny<IntegrationTokenRequestLogReadListRequest>()))
                   .Returns(Task.FromResult(new IntegrationTokenRequestLogReadListResponse { Status = ResponseStatus.Success }));
        }

        public static void Setup_GetTokenRequestLogsOfIntegrationClient_Returns_IntegrationClientTokenRequestLogReadListResponse_Success(this Mock<IIntegrationService> service)
        {
            service.Setup(x => x.GetTokenRequestLogsOfIntegrationClient(It.IsAny<IntegrationClientTokenRequestLogReadListRequest>()))
                   .Returns(Task.FromResult(new IntegrationClientTokenRequestLogReadListResponse { Status = ResponseStatus.Success }));
        }

        public static void Setup_GetIntegrations_Returns_IntegrationReadListResponse_Failed(this Mock<IIntegrationService> service)
        {
            service.Setup(x => x.GetIntegrations(It.IsAny<IntegrationReadListRequest>()))
                .Returns(Task.FromResult(new IntegrationReadListResponse { Status = ResponseStatus.Failed, ErrorMessages = new List<string> { StringOne } }));
        }

        public static void Setup_GetIntegration_Returns_IntegrationReadResponse_Failed(this Mock<IIntegrationService> service)
        {
            service.Setup(x => x.GetIntegration(It.IsAny<IntegrationReadRequest>()))
                   .Returns(Task.FromResult(new IntegrationReadResponse { Status = ResponseStatus.Failed, ErrorMessages = new List<string> { StringOne } }));
        }

        public static void Setup_CreateIntegration_Returns_IntegrationCreateResponse_Failed(this Mock<IIntegrationService> service)
        {
            service.Setup(x => x.CreateIntegration(It.IsAny<IntegrationCreateRequest>()))
                   .Returns(Task.FromResult(new IntegrationCreateResponse { Status = ResponseStatus.Failed, ErrorMessages = new List<string> { StringOne } }));
        }

        public static void Setup_EditIntegration_Returns_IntegrationEditResponse_Failed(this Mock<IIntegrationService> service)
        {
            service.Setup(x => x.EditIntegration(It.IsAny<IntegrationEditRequest>()))
                   .Returns(Task.FromResult(new IntegrationEditResponse { Status = ResponseStatus.Failed, ErrorMessages = new List<string> { StringOne } }));
        }

        public static void Setup_DeleteIntegration_Returns_IntegrationDeleteResponse_Failed(this Mock<IIntegrationService> service)
        {
            service.Setup(x => x.DeleteIntegration(It.IsAny<IntegrationDeleteRequest>()))
                   .Returns(Task.FromResult(new IntegrationDeleteResponse { Status = ResponseStatus.Failed, ErrorMessages = new List<string> { StringOne } }));
        }

        public static void Setup_ChangeActivationForIntegration_Returns_IntegrationChangeActivationResponse_Failed(this Mock<IIntegrationService> service)
        {
            service.Setup(x => x.ChangeActivationForIntegration(It.IsAny<IntegrationChangeActivationRequest>()))
                   .Returns(Task.FromResult(new IntegrationChangeActivationResponse { Status = ResponseStatus.Failed, ErrorMessages = new List<string> { StringOne } }));
        }

        public static void Setup_CreateIntegrationClient_Returns_IntegrationClientCreateResponse_Failed(this Mock<IIntegrationService> service)
        {
            service.Setup(x => x.CreateIntegrationClient(It.IsAny<IntegrationClientCreateRequest>()))
                   .Returns(Task.FromResult(new IntegrationClientCreateResponse { Status = ResponseStatus.Failed, ErrorMessages = new List<string> { StringOne } }));
        }

        public static void Setup_RefreshIntegrationClient_Returns_IntegrationClientRefreshResponse_Failed(this Mock<IIntegrationService> service)
        {
            service.Setup(x => x.RefreshIntegrationClient(It.IsAny<IntegrationClientRefreshRequest>()))
                   .Returns(Task.FromResult(new IntegrationClientRefreshResponse { Status = ResponseStatus.Failed, ErrorMessages = new List<string> { StringOne } }));
        }

        public static void Setup_DeleteIntegrationClient_Returns_IntegrationClientDeleteResponse_Failed(this Mock<IIntegrationService> service)
        {
            service.Setup(x => x.DeleteIntegrationClient(It.IsAny<IntegrationClientDeleteRequest>()))
                   .Returns(Task.FromResult(new IntegrationClientDeleteResponse { Status = ResponseStatus.Failed, ErrorMessages = new List<string> { StringOne } }));
        }

        public static void Setup_ChangeActivationForIntegrationClient_Returns_IntegrationClientChangeActivationResponse_Failed(this Mock<IIntegrationService> service)
        {
            service.Setup(x => x.ChangeActivationForIntegrationClient(It.IsAny<IntegrationClientChangeActivationRequest>()))
                .Returns(Task.FromResult(new IntegrationClientChangeActivationResponse { Status = ResponseStatus.Failed, ErrorMessages = new List<string> { StringOne } }));
        }

        public static void Setup_GetIntegrationClient_Returns_IntegrationClientReadResponse_Failed(this Mock<IIntegrationService> service)
        {
            service.Setup(x => x.GetIntegrationClient(It.IsAny<IntegrationClientReadRequest>()))
                   .Returns(Task.FromResult(new IntegrationClientReadResponse { Status = ResponseStatus.Failed, ErrorMessages = new List<string> { StringOne } }));
        }

        public static void Setup_CreateToken_Returns_TokenCreateResponse_Failed(this Mock<IIntegrationService> service)
        {
            service.Setup(x => x.CreateToken(It.IsAny<TokenCreateRequest>()))
                   .Returns(Task.FromResult(new TokenCreateResponse { Status = ResponseStatus.Failed, ErrorMessages = new List<string> { StringOne } }));
        }

        public static void Setup_RevokeToken_Returns_TokenRevokeResponse_Failed(this Mock<IIntegrationService> service)
        {
            service.Setup(x => x.RevokeToken(It.IsAny<TokenRevokeRequest>()))
                   .Returns(Task.FromResult(new TokenRevokeResponse { Status = ResponseStatus.Failed, ErrorMessages = new List<string> { StringOne } }));
        }

        public static void Setup_GetActiveTokensOfOrganization_Returns_OrganizationActiveTokenReadListResponse_Failed(this Mock<IIntegrationService> service)
        {
            service.Setup(x => x.GetActiveTokensOfOrganization(It.IsAny<OrganizationActiveTokenReadListRequest>()))
                   .Returns(Task.FromResult(new OrganizationActiveTokenReadListResponse { Status = ResponseStatus.Failed, ErrorMessages = new List<string> { StringOne } }));
        }

        public static void Setup_GetActiveTokensOfIntegration_Returns_IntegrationActiveTokenReadListResponse_Failed(this Mock<IIntegrationService> service)
        {
            service.Setup(x => x.GetActiveTokensOfIntegration(It.IsAny<IntegrationActiveTokenReadListRequest>()))
                   .Returns(Task.FromResult(new IntegrationActiveTokenReadListResponse { Status = ResponseStatus.Failed, ErrorMessages = new List<string> { StringOne } }));
        }

        public static void Setup_GetActiveTokensOfIntegrationClient_Returns_IntegrationClientActiveTokenReadListResponse_Failed(this Mock<IIntegrationService> service)
        {
            service.Setup(x => x.GetActiveTokensOfIntegrationClient(It.IsAny<IntegrationClientActiveTokenReadListRequest>()))
                   .Returns(Task.FromResult(new IntegrationClientActiveTokenReadListResponse { Status = ResponseStatus.Failed, ErrorMessages = new List<string> { StringOne } }));
        }

        public static void Setup_GetAllTokenRequestLogs_Returns_AllTokenRequestLogReadListResponse_Failed(this Mock<IIntegrationService> service)
        {
            service.Setup(x => x.GetAllTokenRequestLogs(It.IsAny<AllTokenRequestLogReadListRequest>()))
                   .Returns(Task.FromResult(new AllTokenRequestLogReadListResponse { Status = ResponseStatus.Failed, ErrorMessages = new List<string> { StringOne } }));
        }

        public static void Setup_GetTokenRequestLogsOfOrganization_Returns_OrganizationTokenRequestLogReadListResponse_Failed(this Mock<IIntegrationService> service)
        {
            service.Setup(x => x.GetTokenRequestLogsOfOrganization(It.IsAny<OrganizationTokenRequestLogReadListRequest>()))
                   .Returns(Task.FromResult(new OrganizationTokenRequestLogReadListResponse { Status = ResponseStatus.Failed, ErrorMessages = new List<string> { StringOne } }));
        }

        public static void Setup_GetTokenRequestLogsOfIntegration_Returns_IntegrationTokenRequestLogReadListResponse_Failed(this Mock<IIntegrationService> service)
        {
            service.Setup(x => x.GetTokenRequestLogsOfIntegration(It.IsAny<IntegrationTokenRequestLogReadListRequest>()))
                   .Returns(Task.FromResult(new IntegrationTokenRequestLogReadListResponse { Status = ResponseStatus.Failed, ErrorMessages = new List<string> { StringOne } }));
        }

        public static void Setup_GetTokenRequestLogsOfIntegrationClient_Returns_IntegrationClientTokenRequestLogReadListResponse_Failed(this Mock<IIntegrationService> service)
        {
            service.Setup(x => x.GetTokenRequestLogsOfIntegrationClient(It.IsAny<IntegrationClientTokenRequestLogReadListRequest>()))
                   .Returns(Task.FromResult(new IntegrationClientTokenRequestLogReadListResponse { Status = ResponseStatus.Failed, ErrorMessages = new List<string> { StringOne } }));
        }

        public static void Setup_GetIntegrations_Returns_IntegrationReadListResponse_Invalid(this Mock<IIntegrationService> service)
        {
            service.Setup(x => x.GetIntegrations(It.IsAny<IntegrationReadListRequest>()))
                   .Returns(Task.FromResult(new IntegrationReadListResponse { Status = ResponseStatus.Invalid, ErrorMessages = new List<string> { StringOne } }));
        }

        public static void Setup_GetIntegration_Returns_IntegrationReadResponse_Invalid(this Mock<IIntegrationService> service)
        {
            service.Setup(x => x.GetIntegration(It.IsAny<IntegrationReadRequest>()))
                   .Returns(Task.FromResult(new IntegrationReadResponse { Status = ResponseStatus.Invalid, ErrorMessages = new List<string> { StringOne } }));
        }

        public static void Setup_CreateIntegration_Returns_IntegrationCreateResponse_Invalid(this Mock<IIntegrationService> service)
        {
            service.Setup(x => x.CreateIntegration(It.IsAny<IntegrationCreateRequest>()))
                   .Returns(Task.FromResult(new IntegrationCreateResponse { Status = ResponseStatus.Invalid, ErrorMessages = new List<string> { StringOne } }));
        }

        public static void Setup_EditIntegration_Returns_IntegrationEditResponse_Invalid(this Mock<IIntegrationService> service)
        {
            service.Setup(x => x.EditIntegration(It.IsAny<IntegrationEditRequest>()))
                   .Returns(Task.FromResult(new IntegrationEditResponse { Status = ResponseStatus.Invalid, ErrorMessages = new List<string> { StringOne } }));
        }

        public static void Setup_GetIntegrationRevisions_Returns_IntegrationRevisionReadListResponse_Success(this Mock<IIntegrationService> service)
        {
            var items = new List<RevisionDto<IntegrationDto>>();
            items.Add(new RevisionDto<IntegrationDto>() { RevisionedByUid = UidOne, Revision = One, Item = new IntegrationDto() { Uid = UidOne } });

            service.Setup(x => x.GetIntegrationRevisions(It.IsAny<IntegrationRevisionReadListRequest>()))
                .Returns(Task.FromResult(new IntegrationRevisionReadListResponse() { Status = ResponseStatus.Success, Items = items }));
        }

        public static void Setup_GetIntegrationClients_Returns_IntegrationClientReadListResponse_Success(this Mock<IIntegrationService> service)
        {
           var items= new  List<IntegrationClientDto>();
           items.Add(new  IntegrationClientDto(){Uid = UidOne});
           service.Setup(x => x.GetIntegrationClients(It.IsAny<IntegrationClientReadListRequest>()))
               .ReturnsAsync(new IntegrationClientReadListResponse() {Status = ResponseStatus.Success, Items = items});
        }

        public static void Setup_GetIntegrationClients_Returns_IntegrationClientReadListResponse_Failed(this Mock<IIntegrationService> service)
        {
            var items = new List<IntegrationClientDto>();
            items.Add(new IntegrationClientDto() { Uid = UidOne });
            service.Setup(x => x.GetIntegrationClients(It.IsAny<IntegrationClientReadListRequest>()))
                .ReturnsAsync(new IntegrationClientReadListResponse() { Status = ResponseStatus.Failed, Items = items });
        }

        public static void Setup_GetIntegrationClients_Returns_IntegrationClientReadListResponse_Invalid(this Mock<IIntegrationService> service)
        {
            var items = new List<IntegrationClientDto>();
            items.Add(new IntegrationClientDto() { Uid = UidOne });
            service.Setup(x => x.GetIntegrationClients(It.IsAny<IntegrationClientReadListRequest>()))
                .ReturnsAsync(new IntegrationClientReadListResponse() { Status = ResponseStatus.Invalid, Items = items });
        }

        public static void Verify_GetIntegrationClients(this Mock<IIntegrationService> service)
        {
            service.Verify(x => x.GetIntegrationClients(It.IsAny<IntegrationClientReadListRequest>()));
        }

        public static void Setup_GetIntegrationRevisions_Returns_IntegrationRevisionReadListResponse_Invalid(this Mock<IIntegrationService> service)
        {
            service.Setup(x => x.GetIntegrations(It.IsAny<IntegrationReadListRequest>()))
                .Returns(Task.FromResult(new IntegrationReadListResponse { Status = ResponseStatus.Invalid, ErrorMessages = new List<string> { StringOne } }));
        }

        public static void Verify_GetIntegrationRevisions(this Mock<IIntegrationService> service)
        {
            var items = new List<RevisionDto<IntegrationDto>>();
            items.Add(new RevisionDto<IntegrationDto>() { RevisionedByUid = UidOne, Revision = One, Item = new IntegrationDto() { Uid = UidOne } });

            service.Verify(x => x.GetIntegrationRevisions(It.IsAny<IntegrationRevisionReadListRequest>()));
        }

        public static void Setup_DeleteIntegration_Returns_IntegrationDeleteResponse_Invalid(this Mock<IIntegrationService> service)
        {
            service.Setup(x => x.DeleteIntegration(It.IsAny<IntegrationDeleteRequest>()))
                   .Returns(Task.FromResult(new IntegrationDeleteResponse { Status = ResponseStatus.Invalid, ErrorMessages = new List<string> { StringOne } }));
        }

        public static void Setup_RestoreIntegration_Returns_IntegrationRestoreResponse_Invalid(this Mock<IIntegrationService> service)
        {
            service.Setup(x => x.RestoreIntegration(It.IsAny<IntegrationRestoreRequest>()))
                .Returns(Task.FromResult(new IntegrationRestoreResponse { Status = ResponseStatus.Invalid, ErrorMessages = new List<string> { StringOne } }));
        }

        public static void Setup_ChangeActivationForIntegration_Returns_IntegrationChangeActivationResponse_Invalid(this Mock<IIntegrationService> service)
        {
            service.Setup(x => x.ChangeActivationForIntegration(It.IsAny<IntegrationChangeActivationRequest>()))
                   .Returns(Task.FromResult(new IntegrationChangeActivationResponse { Status = ResponseStatus.Invalid, ErrorMessages = new List<string> { StringOne } }));
        }

        public static void Setup_CreateIntegrationClient_Returns_IntegrationClientCreateResponse_Invalid(this Mock<IIntegrationService> service)
        {
            service.Setup(x => x.CreateIntegrationClient(It.IsAny<IntegrationClientCreateRequest>()))
                   .Returns(Task.FromResult(new IntegrationClientCreateResponse { Status = ResponseStatus.Invalid, ErrorMessages = new List<string> { StringOne } }));
        }

        public static void Setup_RefreshIntegrationClient_Returns_IntegrationClientRefreshResponse_Invalid(this Mock<IIntegrationService> service)
        {
            service.Setup(x => x.RefreshIntegrationClient(It.IsAny<IntegrationClientRefreshRequest>()))
                   .Returns(Task.FromResult(new IntegrationClientRefreshResponse { Status = ResponseStatus.Invalid, ErrorMessages = new List<string> { StringOne } }));
        }

        public static void Setup_DeleteIntegrationClient_Returns_IntegrationClientDeleteResponse_Invalid(this Mock<IIntegrationService> service)
        {
            service.Setup(x => x.DeleteIntegrationClient(It.IsAny<IntegrationClientDeleteRequest>()))
                   .Returns(Task.FromResult(new IntegrationClientDeleteResponse { Status = ResponseStatus.Invalid, ErrorMessages = new List<string> { StringOne } }));
        }

        public static void Setup_ChangeActivationForIntegrationClient_Returns_IntegrationClientChangeActivationResponse_Invalid(this Mock<IIntegrationService> service)
        {
            service.Setup(x =>
                    x.ChangeActivationForIntegrationClient(It.IsAny<IntegrationClientChangeActivationRequest>()))
                .Returns(Task.FromResult(
                    new IntegrationClientChangeActivationResponse { Status = ResponseStatus.Invalid, ErrorMessages = new List<string> { StringOne } }));
        }

        public static void Setup_GetIntegrationClient_Returns_IntegrationClientReadResponse_Invalid(this Mock<IIntegrationService> service)
        {
            service.Setup(x => x.GetIntegrationClient(It.IsAny<IntegrationClientReadRequest>()))
                   .Returns(Task.FromResult(new IntegrationClientReadResponse { Status = ResponseStatus.Invalid, ErrorMessages = new List<string> { StringOne } }));
        }

        public static void Setup_CreateToken_Returns_TokenCreateResponse_Invalid(this Mock<IIntegrationService> service)
        {
            service.Setup(x => x.CreateToken(It.IsAny<TokenCreateRequest>()))
                   .Returns(Task.FromResult(new TokenCreateResponse { Status = ResponseStatus.Invalid, ErrorMessages = new List<string> { StringOne } }));
        }

        public static void Setup_RevokeToken_Returns_TokenRevokeResponse_Invalid(this Mock<IIntegrationService> service)
        {
            service.Setup(x => x.RevokeToken(It.IsAny<TokenRevokeRequest>()))
                   .Returns(Task.FromResult(new TokenRevokeResponse { Status = ResponseStatus.Invalid, ErrorMessages = new List<string> { StringOne } }));
        }

        public static void Setup_GetActiveTokensOfOrganization_Returns_OrganizationActiveTokenReadListResponse_Invalid(this Mock<IIntegrationService> service)
        {
            service.Setup(x => x.GetActiveTokensOfOrganization(It.IsAny<OrganizationActiveTokenReadListRequest>()))
                .Returns(Task.FromResult(
                    new OrganizationActiveTokenReadListResponse { Status = ResponseStatus.Invalid, ErrorMessages = new List<string> { StringOne } }));
        }

        public static void Setup_GetActiveTokensOfIntegration_Returns_IntegrationActiveTokenReadListResponse_Invalid(this Mock<IIntegrationService> service)
        {
            service.Setup(x => x.GetActiveTokensOfIntegration(It.IsAny<IntegrationActiveTokenReadListRequest>()))
                   .Returns(Task.FromResult(new IntegrationActiveTokenReadListResponse { Status = ResponseStatus.Invalid, ErrorMessages = new List<string> { StringOne } }));
        }

        public static void Setup_GetActiveTokensOfIntegrationClient_Returns_IntegrationClientActiveTokenReadListResponse_Invalid(this Mock<IIntegrationService> service)
        {
            service.Setup(x =>
                    x.GetActiveTokensOfIntegrationClient(It.IsAny<IntegrationClientActiveTokenReadListRequest>()))
                .Returns(
                    Task.FromResult(
                        new IntegrationClientActiveTokenReadListResponse { Status = ResponseStatus.Invalid, ErrorMessages = new List<string> { StringOne } }));
        }

        public static void Setup_GetAllTokenRequestLogs_Returns_AllTokenRequestLogReadListResponse_Invalid(this Mock<IIntegrationService> service)
        {
            service.Setup(x => x.GetAllTokenRequestLogs(It.IsAny<AllTokenRequestLogReadListRequest>()))
                   .Returns(Task.FromResult(new AllTokenRequestLogReadListResponse { Status = ResponseStatus.Invalid, ErrorMessages = new List<string> { StringOne } }));
        }

        public static void Setup_GetTokenRequestLogsOfOrganization_Returns_OrganizationTokenRequestLogReadListResponse_Invalid(this Mock<IIntegrationService> service)
        {
            service.Setup(x => x.GetTokenRequestLogsOfOrganization(It.IsAny<OrganizationTokenRequestLogReadListRequest>()))
                   .Returns(Task.FromResult(new OrganizationTokenRequestLogReadListResponse { Status = ResponseStatus.Invalid, ErrorMessages = new List<string> { StringOne } }));
        }

        public static void Setup_GetTokenRequestLogsOfIntegration_Returns_IntegrationTokenRequestLogReadListResponse_Invalid(this Mock<IIntegrationService> service)
        {
            service.Setup(x => x.GetTokenRequestLogsOfIntegration(It.IsAny<IntegrationTokenRequestLogReadListRequest>()))
                   .Returns(Task.FromResult(new IntegrationTokenRequestLogReadListResponse { Status = ResponseStatus.Invalid, ErrorMessages = new List<string> { StringOne } }));
        }

        public static void Setup_GetTokenRequestLogsOfIntegrationClient_Returns_IntegrationClientTokenRequestLogReadListResponse_Invalid(this Mock<IIntegrationService> service)
        {
            service.Setup(x => x.GetTokenRequestLogsOfIntegrationClient(It.IsAny<IntegrationClientTokenRequestLogReadListRequest>()))
                   .Returns(Task.FromResult(new IntegrationClientTokenRequestLogReadListResponse { Status = ResponseStatus.Invalid, ErrorMessages = new List<string> { StringOne } }));
        }

        public static void Verify_RestoreIntegration(this Mock<IIntegrationService> service)
        {
            service.Verify(x => x.RestoreIntegration(It.IsAny<IntegrationRestoreRequest>()));
        }

        public static void Verify_GetIntegrations(this Mock<IIntegrationService> service)
        {
            service.Verify(x => x.GetIntegrations(It.IsAny<IntegrationReadListRequest>()));
        }

        public static void Verify_GetIntegration(this Mock<IIntegrationService> service)
        {
            service.Setup(x => x.GetIntegration(It.IsAny<IntegrationReadRequest>()));
        }

        public static void Verify_CreateIntegration(this Mock<IIntegrationService> service)
        {
            service.Setup(x => x.CreateIntegration(It.IsAny<IntegrationCreateRequest>()));
        }

        public static void Verify_EditIntegration(this Mock<IIntegrationService> service)
        {
            service.Setup(x => x.EditIntegration(It.IsAny<IntegrationEditRequest>()));
        }

        public static void Verify_DeleteIntegration(this Mock<IIntegrationService> service)
        {
            service.Setup(x => x.DeleteIntegration(It.IsAny<IntegrationDeleteRequest>()));
        }

        public static void Verify_ChangeActivationForIntegration(this Mock<IIntegrationService> service)
        {
            service.Setup(x => x.ChangeActivationForIntegration(It.IsAny<IntegrationChangeActivationRequest>()));
        }

        public static void Verify_CreateIntegrationClient(this Mock<IIntegrationService> service)
        {
            service.Setup(x => x.CreateIntegrationClient(It.IsAny<IntegrationClientCreateRequest>()));
        }

        public static void Verify_RefreshIntegrationClient(this Mock<IIntegrationService> service)
        {
            service.Setup(x => x.RefreshIntegrationClient(It.IsAny<IntegrationClientRefreshRequest>()));
        }

        public static void Verify_DeleteIntegrationClient(this Mock<IIntegrationService> service)
        {
            service.Setup(x => x.DeleteIntegrationClient(It.IsAny<IntegrationClientDeleteRequest>()));
        }

        public static void Verify_ChangeActivationForIntegrationClient(this Mock<IIntegrationService> service)
        {
            service.Setup(x =>
                x.ChangeActivationForIntegrationClient(It.IsAny<IntegrationClientChangeActivationRequest>()));
        }

        public static void Verify_GetIntegrationClient(this Mock<IIntegrationService> service)
        {
            service.Setup(x => x.GetIntegrationClient(It.IsAny<IntegrationClientReadRequest>()));
        }

        public static void Verify_CreateToken(this Mock<IIntegrationService> service)
        {
            service.Setup(x => x.CreateToken(It.IsAny<TokenCreateRequest>()));
        }

        public static void Verify_RevokeToken(this Mock<IIntegrationService> service)
        {
            service.Setup(x => x.RevokeToken(It.IsAny<TokenRevokeRequest>()));
        }

        public static void Verify_GetActiveTokensOfOrganization(this Mock<IIntegrationService> service)
        {
            service.Setup(x => x.GetActiveTokensOfOrganization(It.IsAny<OrganizationActiveTokenReadListRequest>()));
        }

        public static void Verify_GetActiveTokensOfIntegration(this Mock<IIntegrationService> service)
        {
            service.Setup(x => x.GetActiveTokensOfIntegration(It.IsAny<IntegrationActiveTokenReadListRequest>()));
        }

        public static void Verify_GetActiveTokensOfIntegrationClient(this Mock<IIntegrationService> service)
        {
            service.Setup(x =>
                x.GetActiveTokensOfIntegrationClient(It.IsAny<IntegrationClientActiveTokenReadListRequest>()));
        }

        public static void Verify_GetAllTokenRequestLogs(this Mock<IIntegrationService> service)
        {
            service.Setup(x => x.GetAllTokenRequestLogs(It.IsAny<AllTokenRequestLogReadListRequest>()));
        }

        public static void Verify_GetTokenRequestLogsOfOrganization(this Mock<IIntegrationService> service)
        {
            service.Setup(x =>
                x.GetTokenRequestLogsOfOrganization(It.IsAny<OrganizationTokenRequestLogReadListRequest>()));
        }

        public static void Verify_GetTokenRequestLogsOfIntegration(this Mock<IIntegrationService> service)
        {
            service.Setup(
                x => x.GetTokenRequestLogsOfIntegration(It.IsAny<IntegrationTokenRequestLogReadListRequest>()));
        }

        public static void Verify_GetTokenRequestLogsOfIntegrationClient(this Mock<IIntegrationService> service)
        {
            service.Setup(x =>
                x.GetTokenRequestLogsOfIntegrationClient(It.IsAny<IntegrationClientTokenRequestLogReadListRequest>()));
        }
    }
}