using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

using StandardRepository.Helpers;
using Translation.Common.Contracts;
using Translation.Common.Enumerations;
using Translation.Common.Helpers;
using Translation.Common.Models.DataTransferObjects;
using Translation.Common.Models.Requests.Integration;
using Translation.Common.Models.Requests.Integration.IntegrationClient;
using Translation.Common.Models.Requests.Integration.Token;
using Translation.Common.Models.Responses.Integration;
using Translation.Common.Models.Responses.Integration.IntegrationClient;
using Translation.Common.Models.Responses.Integration.Token;
using Translation.Common.Models.Responses.Integration.Token.RequestLog;
using Translation.Data.Entities.Main;
using Translation.Data.Factories;
using Translation.Data.Repositories.Contracts;
using Translation.Service.Managers;

namespace Translation.Service
{
    public class IntegrationService : IIntegrationService
    {
        private readonly CacheManager _cacheManager;
        private readonly IOrganizationRepository _organizationRepository;
        private readonly IIntegrationRepository _integrationRepository;
        private readonly IntegrationFactory _integrationFactory;
        private readonly IIntegrationClientRepository _integrationClientRepository;
        private readonly IntegrationClientFactory _integrationClientFactory;
        private readonly ITokenRepository _tokenRepository;
        private readonly TokenFactory _tokenFactory;
        private readonly ITokenRequestLogRepository _tokenRequestLogRepository;
        private readonly TokenRequestLogFactory _tokenRequestLogFactory;
        private readonly IProjectRepository _projectRepository;

        public IntegrationService(CacheManager cacheManager,
                                  IOrganizationRepository organizationRepository,
                                  IIntegrationRepository integrationRepository, IntegrationFactory integrationFactory,
                                  IIntegrationClientRepository integrationClientRepository, IntegrationClientFactory integrationClientFactory,
                                  ITokenRepository tokenRepository, TokenFactory tokenFactory,
                                  ITokenRequestLogRepository tokenRequestLogRepository, TokenRequestLogFactory tokenRequestLogFactory,
                                  IProjectRepository projectRepository)
        {
            _cacheManager = cacheManager;
            _organizationRepository = organizationRepository;
            _integrationRepository = integrationRepository;
            _integrationFactory = integrationFactory;
            _integrationClientRepository = integrationClientRepository;
            _integrationClientFactory = integrationClientFactory;
            _tokenRepository = tokenRepository;
            _tokenFactory = tokenFactory;
            _tokenRequestLogRepository = tokenRequestLogRepository;
            _tokenRequestLogFactory = tokenRequestLogFactory;
            _projectRepository = projectRepository;
        }

        #region Integration
        public async Task<IntegrationCreateResponse> CreateIntegration(IntegrationCreateRequest request)
        {
            var response = new IntegrationCreateResponse();

            var currentUser = _cacheManager.GetCachedCurrentUser(request.CurrentUserId);
            if (!currentUser.IsAdmin)
            {
                response.SetInvalid();
                return response;
            }

            if (await _organizationRepository.Any(x => x.Id == currentUser.OrganizationId && !x.IsActive))
            {
                response.SetInvalid();
                return response;
            }

            if (await _integrationRepository.Any(x => x.Name == request.Name && x.OrganizationId == currentUser.OrganizationId))
            {
                response.ErrorMessages.Add("integration_name_must_be_unique");
                response.Status = ResponseStatus.Failed;
                return response;
            }

            var entity = _integrationFactory.CreateEntityFromRequest(request, currentUser.Organization);
            await _integrationRepository.Insert(request.CurrentUserId, entity);

            response.Item = _integrationFactory.CreateDtoFromEntity(entity);
            response.Status = ResponseStatus.Success;
            return response;
        }

        public async Task<IntegrationReadResponse> GetIntegration(IntegrationReadRequest request)
        {
            var response = new IntegrationReadResponse();

            var currentUser = _cacheManager.GetCachedCurrentUser(request.CurrentUserId);

            var entity = await _integrationRepository.Select(x => x.Uid == request.IntegrationUid);
            if (entity.IsNotExist())
            {
                response.SetInvalidBecauseEntityNotFound();
                return response;
            }

            if (entity.OrganizationId != currentUser.OrganizationId)
            {
                response.SetFailed();
                return response;
            }

            response.Item = _integrationFactory.CreateDtoFromEntity(entity);
            response.Status = ResponseStatus.Success;
            return response;
        }

        public async Task<IntegrationReadListResponse> GetIntegrations(IntegrationReadListRequest request)
        {
            var response = new IntegrationReadListResponse();

            var currentUser = _cacheManager.GetCachedCurrentUser(request.CurrentUserId);

            Expression<Func<Integration, bool>> filter = x => x.OrganizationId == currentUser.OrganizationId;

            if (request.SearchTerm.IsNotEmpty())
            {
                filter = x => x.OrganizationId == currentUser.OrganizationId && x.Name.Contains(request.SearchTerm);
            }

            List<Integration> entities;
            if (request.PagingInfo.Skip < 1)
            {
                entities = await _integrationRepository.SelectAfter(filter, request.PagingInfo.LastUid, request.PagingInfo.Take, x => x.Uid, request.PagingInfo.IsAscending);
            }
            else
            {
                entities = await _integrationRepository.SelectMany(filter, request.PagingInfo.Skip, request.PagingInfo.Take, x => x.Id, request.PagingInfo.IsAscending);
            }

            if (entities != null)
            {
                for (var i = 0; i < entities.Count; i++)
                {
                    var entity = entities[i];
                    var dto = _integrationFactory.CreateDtoFromEntity(entity);
                    response.Items.Add(dto);
                }
            }

            response.PagingInfo.Skip = request.PagingInfo.Skip;
            response.PagingInfo.Take = request.PagingInfo.Take;
            response.PagingInfo.LastUid = request.PagingInfo.LastUid;
            response.PagingInfo.IsAscending = request.PagingInfo.IsAscending;
            response.PagingInfo.TotalItemCount = await _integrationRepository.Count(filter);

            response.Status = ResponseStatus.Success;
            return response;
        }

        public async Task<IntegrationRevisionReadListResponse> GetIntegrationRevisions(IntegrationRevisionReadListRequest request)
        {
            var response = new IntegrationRevisionReadListResponse();

            var currentUser = _cacheManager.GetCachedCurrentUser(request.CurrentUserId);

            var integration = await _integrationRepository.Select(x => x.Uid == request.IntegrationUid);
            if (integration.IsNotExist())
            {
                response.SetInvalidBecauseEntityNotFound();
                return response;
            }

            var revisions = await _integrationRepository.SelectRevisions(integration.Id);

            for (int i = 0; i < revisions.Count; i++)
            {
                var revision = revisions[i];

                var revisionDto = new RevisionDto<IntegrationDto>();
                revisionDto.Revision = revision.Revision;
                revisionDto.RevisionedAt = revision.RevisionedAt;

                var user = _cacheManager.GetCachedUser(revision.RevisionedBy);
                revisionDto.RevisionedByUid = user.Uid;
                revisionDto.RevisionedByName = user.Name;

                revisionDto.Item = _integrationFactory.CreateDtoFromEntity(revision.Entity);

                response.Items.Add(revisionDto);
            }

            response.Status = ResponseStatus.Success;
            return response;
        }

        public async Task<IntegrationEditResponse> EditIntegration(IntegrationEditRequest request)
        {
            var response = new IntegrationEditResponse();

            var currentUser = _cacheManager.GetCachedCurrentUser(request.CurrentUserId);
            if (!currentUser.IsAdmin)
            {
                response.SetInvalid();
                return response;
            }

            if (await _organizationRepository.Any(x => x.Id == currentUser.OrganizationId && !x.IsActive))
            {
                response.SetInvalid();
                return response;
            }

            var integration = await _integrationRepository.Select(x => x.Uid == request.IntegrationUid);
            if (integration.IsNotExist())
            {
                response.SetInvalidBecauseEntityNotFound();
                return response;
            }

            if (integration.OrganizationId != currentUser.OrganizationId)
            {
                response.SetInvalid();
                return response;
            }

            var trimName = request.Name.Trim();
            if (await _integrationRepository.Any(x => x.Name == trimName
                                                      && x.OrganizationId == currentUser.OrganizationId
                                                      && x.Uid != request.IntegrationUid))
            {
                response.Status = ResponseStatus.Failed;
                response.ErrorMessages.Add("integration_name_must_be_unique");
                return response;
            }

            var updatedEntity = _integrationFactory.CreateEntityFromRequest(request, integration);
            var result = await _integrationRepository.Update(request.CurrentUserId, updatedEntity);
            if (result)
            {
                response.Item = _integrationFactory.CreateDtoFromEntity(updatedEntity);
                response.Status = ResponseStatus.Success;
                return response;
            }

            response.SetFailed();
            return response;
        }

        public async Task<IntegrationDeleteResponse> DeleteIntegration(IntegrationDeleteRequest request)
        {
            var response = new IntegrationDeleteResponse();

            var currentUser = _cacheManager.GetCachedCurrentUser(request.CurrentUserId);
            if (!currentUser.IsAdmin)
            {
                response.SetInvalid();
                return response;
            }

            if (await _organizationRepository.Any(x => x.Id == currentUser.OrganizationId && !x.IsActive))
            {
                response.SetInvalid();
                return response;
            }

            var integration = await _integrationRepository.Select(x => x.Uid == request.IntegrationUid);
            if (integration.IsNotExist())
            {
                response.SetInvalidBecauseEntityNotFound();
                return response;
            }

            if (integration.OrganizationId != currentUser.OrganizationId)
            {
                response.SetInvalid();
                return response;
            }

            var integrationId = integration.Id;
            if (await _integrationClientRepository.Any(x => x.IntegrationId == integrationId))
            {
                response.SetInvalidForDeleteBecauseHasChildren();
                return response;
            }

            var result = await _integrationRepository.Delete(request.CurrentUserId, integration.Id);
            if (result)
            {
                response.Status = ResponseStatus.Success;
                return response;
            }

            response.SetFailed();
            return response;
        }

        public async Task<IntegrationChangeActivationResponse> ChangeActivationForIntegration(IntegrationChangeActivationRequest request)
        {
            var response = new IntegrationChangeActivationResponse();

            var currentUser = _cacheManager.GetCachedCurrentUser(request.CurrentUserId);
            if (!currentUser.IsAdmin)
            {
                response.SetInvalid();
                return response;
            }

            if (await _organizationRepository.Any(x => x.Id == currentUser.OrganizationId && !x.IsActive))
            {
                response.SetInvalid();
                return response;
            }

            var integration = await _integrationRepository.Select(x => x.Uid == request.IntegrationUid);
            if (integration.IsNotExist())
            {
                response.SetInvalidBecauseEntityNotFound();
                return response;
            }

            if (integration.OrganizationId != currentUser.OrganizationId)
            {
                response.SetInvalid();
                return response;
            }

            _integrationFactory.UpdateEntityForChangeActivation(integration);
            var result = await _integrationRepository.Update(request.CurrentUserId, integration);
            if (result)
            {
                response.Item = _integrationFactory.CreateDtoFromEntity(integration);
                response.Status = ResponseStatus.Success;
                return response;
            }

            response.SetFailed();
            return response;
        }

        public async Task<IntegrationRestoreResponse> RestoreIntegration(IntegrationRestoreRequest request)
        {
            var response = new IntegrationRestoreResponse();

            var currentUser = _cacheManager.GetCachedCurrentUser(request.CurrentUserId);
            if (await _organizationRepository.Any(x => x.Id == currentUser.OrganizationId && !x.IsActive))
            {
                response.SetInvalid();
                return response;
            }

            var integration = await _integrationRepository.Select(x => x.Uid == request.IntegrationUid);
            if (integration.IsNotExist())
            {
                response.SetInvalidBecauseEntityNotFound();
                response.InfoMessages.Add("integration_not_found");
                return response;
            }

            var revisions = await _integrationRepository.SelectRevisions(integration.Id);
            if (revisions.All(x => x.Revision != request.Revision))
            {
                response.SetInvalidBecauseEntityNotFound();
                response.InfoMessages.Add("revision_not_found");
                return response;
            }

            var result = await _integrationRepository.RestoreRevision(request.CurrentUserId, integration.Id, request.Revision);
            if (result)
            {
                response.Status = ResponseStatus.Success;
                return response;
            }

            response.SetFailed();
            return response;
        }
        #endregion

        #region Integration Client

        public async Task<IntegrationClientCreateResponse> CreateIntegrationClient(IntegrationClientCreateRequest request)
        {
            var response = new IntegrationClientCreateResponse();

            var currentUser = _cacheManager.GetCachedCurrentUser(request.CurrentUserId);
            if (!currentUser.IsAdmin)
            {
                response.SetInvalid();
                return response;
            }

            if (await _organizationRepository.Any(x => x.Id == currentUser.OrganizationId && !x.IsActive))
            {
                response.SetInvalid();
                return response;
            }

            var integration = await _integrationRepository.Select(x => x.Uid == request.IntegrationUid);
            if (integration.IsNotExist())
            {
                response.SetInvalidBecauseEntityNotFound();
                return response;
            }

            if (integration.OrganizationId != currentUser.OrganizationId)
            {
                response.SetInvalid();
                return response;
            }

            var entity = _integrationClientFactory.CreateEntity(integration);
            await _integrationClientRepository.Insert(request.CurrentUserId, entity);

            response.Item = _integrationClientFactory.CreateDtoFromEntity(entity);
            response.Status = ResponseStatus.Success;
            return response;
        }

        public async Task<IntegrationClientReadResponse> GetIntegrationClient(IntegrationClientReadRequest request)
        {
            var response = new IntegrationClientReadResponse();

            var integrationClient = await _integrationClientRepository.Select(x => x.Uid == request.IntegrationClientUid);
            if (integrationClient.IsNotExist())
            {
                response.SetInvalidBecauseEntityNotFound();
                return response;
            }

            response.Item = _integrationClientFactory.CreateDtoFromEntity(integrationClient);
            response.Status = ResponseStatus.Success;
            return response;
        }

        public async Task<IntegrationClientReadListResponse> GetIntegrationClients(IntegrationClientReadListRequest request)
        {
            var response = new IntegrationClientReadListResponse();

            var integration = await _integrationRepository.Select(x => x.Uid == request.IntegrationUid);
            if (integration.IsNotExist())
            {
                response.SetInvalidBecauseEntityNotFound();
                return response;
            }

            var integrationId = integration.Id;
            Expression<Func<IntegrationClient, bool>> filter = x => x.IntegrationId == integrationId;

            if (request.SearchTerm.IsNotEmpty())
            {
                filter = x => x.IntegrationId == integrationId && x.Name.Contains(request.SearchTerm);
            }

            List<IntegrationClient> entities;
            if (request.PagingInfo.Skip < 1)
            {
                entities = await _integrationClientRepository.SelectAfter(filter, request.PagingInfo.LastUid, request.PagingInfo.Take, x => x.Uid, request.PagingInfo.IsAscending);
            }
            else
            {
                entities = await _integrationClientRepository.SelectMany(filter, request.PagingInfo.Skip, request.PagingInfo.Take, x => x.Id, request.PagingInfo.IsAscending);
            }

            if (entities != null)
            {
                for (var i = 0; i < entities.Count; i++)
                {
                    var entity = entities[i];
                    var dto = _integrationClientFactory.CreateDtoFromEntity(entity);
                    response.Items.Add(dto);
                }
            }

            response.PagingInfo.Skip = request.PagingInfo.Skip;
            response.PagingInfo.Take = request.PagingInfo.Take;
            response.PagingInfo.LastUid = request.PagingInfo.LastUid;
            response.PagingInfo.IsAscending = request.PagingInfo.IsAscending;
            response.PagingInfo.TotalItemCount = await _integrationClientRepository.Count(filter);

            response.Status = ResponseStatus.Success;
            return response;
        }

        public async Task<IntegrationClientRefreshResponse> RefreshIntegrationClient(IntegrationClientRefreshRequest request)
        {
            var response = new IntegrationClientRefreshResponse();

            var currentUser = _cacheManager.GetCachedCurrentUser(request.CurrentUserId);
            if (!currentUser.IsAdmin)
            {
                response.SetInvalid();
                return response;
            }

            if (await _organizationRepository.Any(x => x.Id == currentUser.OrganizationId && !x.IsActive))
            {
                response.SetInvalid();
                return response;
            }

            var integrationClient = await _integrationClientRepository.Select(x => x.Uid == request.IntegrationClientUid);
            if (integrationClient.IsNotExist())
            {
                response.SetInvalidBecauseEntityNotFound();
                return response;
            }

            if (integrationClient.OrganizationId != currentUser.OrganizationId)
            {
                response.SetInvalid();
                return response;
            }

            var integration = await _integrationRepository.SelectById(integrationClient.IntegrationId);
            if (integration.IsNotExist())
            {
                response.SetInvalidBecauseEntityNotFound();
                return response;
            }

            if (!integration.IsActive
                || !integrationClient.IsActive)
            {
                response.SetInvalid();
                return response;
            }

            _integrationClientFactory.UpdateEntityForRefresh(integrationClient);
            await _integrationClientRepository.Update(request.CurrentUserId, integrationClient);

            response.Item = _integrationClientFactory.CreateDtoFromEntity(integrationClient);
            response.Status = ResponseStatus.Success;
            return response;
        }

        public async Task<IntegrationClientDeleteResponse> DeleteIntegrationClient(IntegrationClientDeleteRequest request)
        {
            var response = new IntegrationClientDeleteResponse();

            var currentUser = _cacheManager.GetCachedCurrentUser(request.CurrentUserId);
            if (!currentUser.IsAdmin)
            {
                response.SetInvalid();
                return response;
            }

            if (await _organizationRepository.Any(x => x.Id == currentUser.OrganizationId && !x.IsActive))
            {
                response.SetInvalid();
                return response;
            }

            var integrationClient = await _integrationClientRepository.Select(x => x.Uid == request.IntegrationClientUid);
            if (integrationClient.IsNotExist())
            {
                response.SetInvalidBecauseEntityNotFound();
                return response;
            }

            if (integrationClient.OrganizationId != currentUser.OrganizationId)
            {
                response.SetInvalid();
                return response;
            }

            var integration = await _integrationRepository.SelectById(integrationClient.IntegrationId);
            if (integration.IsNotExist())
            {
                response.SetInvalidBecauseEntityNotFound();
                return response;
            }

            var result = await _integrationClientRepository.Delete(request.CurrentUserId, integrationClient.Id);
            if (result)
            {
                response.Status = ResponseStatus.Success;
                return response;
            }

            response.SetFailed();
            return response;
        }

        public async Task<IntegrationClientChangeActivationResponse> ChangeActivationForIntegrationClient(IntegrationClientChangeActivationRequest request)
        {
            var response = new IntegrationClientChangeActivationResponse();

            var currentUser = _cacheManager.GetCachedCurrentUser(request.CurrentUserId);
            if (!currentUser.IsAdmin)
            {
                response.SetInvalid();
                return response;
            }

            if (await _organizationRepository.Any(x => x.Id == currentUser.OrganizationId && !x.IsActive))
            {
                response.SetInvalidBecauseParentNotActive();
                return response;
            }

            var integrationClient = await _integrationClientRepository.Select(x => x.Uid == request.IntegrationClientUid);
            if (integrationClient.IsNotExist())
            {
                response.SetInvalidBecauseEntityNotFound();
                return response;
            }

            if (integrationClient.OrganizationId != currentUser.OrganizationId)
            {
                response.SetInvalid();
                return response;
            }

            var integration = await _integrationRepository.SelectById(integrationClient.IntegrationId);
            if (integration.IsNotExist())
            {
                response.SetInvalidBecauseEntityNotFound();
                return response;
            }

            if (!integration.IsActive)
            {
                response.SetInvalidBecauseParentNotActive();
                return response;
            }

            _integrationClientFactory.UpdateEntityForChangeActivation(integrationClient);
            var result = await _integrationClientRepository.Update(request.CurrentUserId, integrationClient);
            if (result)
            {
                response.Item = _integrationClientFactory.CreateDtoFromEntity(integrationClient);
                response.Status = ResponseStatus.Success;
                return response;
            }

            response.SetFailed();
            return response;
        }

        #endregion

        #region Token

        public async Task<TokenCreateResponse> CreateToken(TokenCreateRequest request)
        {
            var response = new TokenCreateResponse();
            
            var integrationClient = await _integrationClientRepository.Select(x => x.ClientId == request.ClientId && x.ClientSecret == request.ClientSecret);
            if (integrationClient.IsNotExist())
            {
                response.SetInvalidBecauseEntityNotFound();
                return response;
            }

            if (await _organizationRepository.Any(x => x.Id == integrationClient.OrganizationId && !x.IsActive)
                || await _integrationRepository.Any(x => x.Id == integrationClient.IntegrationId && !x.IsActive))
            {
                response.SetInvalidBecauseParentNotActive();
                return response;
            }

            var token = _tokenFactory.CreateEntityFromRequest(request, integrationClient);
            var id = await _tokenRepository.Insert(integrationClient.Id, token);
            if (id > 0)
            {
                response.Item = _tokenFactory.CreateDtoFromEntity(token);
                response.Status = ResponseStatus.Success;
                return response;
            }

            response.SetFailed();
            return response;
        }

        public async Task<TokenCreateResponse> CreateToken(TokenGetRequest request)
        {
            var response = new TokenCreateResponse();

            var currentUser = _cacheManager.GetCachedCurrentUser(request.CurrentUserId);
            if (currentUser == null)
            {
                response.SetInvalid();
                return response;
            }
            
            var integrationClient = await _integrationClientRepository.Select(x => x.OrganizationId == currentUser.OrganizationId && x.IsActive);
            if (integrationClient.IsNotExist())
            {
                response.SetInvalidBecauseEntityNotFound();
                return response;
            }

            if (await _organizationRepository.Any(x => x.Id == integrationClient.OrganizationId && !x.IsActive)
                || await _integrationRepository.Any(x => x.Id == integrationClient.IntegrationId && !x.IsActive))
            {
                response.SetInvalidBecauseParentNotActive();
                return response;
            }

            var token = _tokenFactory.CreateEntity(integrationClient);
            var id = await _tokenRepository.Insert(integrationClient.Id, token);
            if (id > 0)
            {
                response.Item = _tokenFactory.CreateDtoFromEntity(token);
                response.Status = ResponseStatus.Success;
                return response;
            }

            response.SetFailed();
            return response;
        }

        public async Task<TokenRevokeResponse> RevokeToken(TokenRevokeRequest request)
        {
            var response = new TokenRevokeResponse();

            var currentUser = _cacheManager.GetCachedCurrentUser(request.CurrentUserId);
            if (!currentUser.IsAdmin)
            {
                response.SetInvalid();
                return response;
            }

            var integrationClient = await _integrationClientRepository.Select(x => x.Uid == request.IntegrationClientUid);
            if (integrationClient.IsNotExist())
            {
                response.SetInvalidBecauseEntityNotFound();
                return response;
            }

            if (integrationClient.OrganizationId != currentUser.OrganizationId)
            {
                response.SetInvalid();
                return response;
            }

            var token = await _tokenRepository.Select(x => x.AccessToken == request.Token);
            if (token.IsNotExist())
            {
                response.SetInvalidBecauseEntityNotFound();
                return response;
            }

            var result = await _tokenRepository.Delete(request.CurrentUserId, token.Id);
            if (result)
            {
                response.Status = ResponseStatus.Success;
                return response;
            }

            response.SetFailed();
            return response;
        }

        public async Task<TokenValidateResponse> ValidateToken(TokenValidateRequest request)
        {
            var response = new TokenValidateResponse();

            var project = await _projectRepository.Select(x => x.Uid == request.ProjectUid && x.IsActive);
            if (project.IsNotExist())
            {
                response.SetInvalidBecauseEntityNotFound();
                return response;
            }

            var now = DateTime.UtcNow;
            var token = await _tokenRepository.Select(x => x.AccessToken == request.Token && x.ExpiresAt > now);
            if (token.IsNotExist())
            {
                response.SetInvalid();
                return response;
            }

            if (token.OrganizationId != project.OrganizationId)
            {
                response.SetInvalid();
                return response;
            }

            if (await _organizationRepository.Any(x => x.Id == project.OrganizationId && !x.IsActive))
            {
                response.SetInvalid();
                return response;
            }

            response.Status = ResponseStatus.Success;
            return response;
        }

        public async Task<OrganizationActiveTokenReadListResponse> GetActiveTokensOfOrganization(OrganizationActiveTokenReadListRequest request)
        {
            var response = new OrganizationActiveTokenReadListResponse();

            var currentUser = _cacheManager.GetCachedCurrentUser(request.CurrentUserId);

            var now = DateTime.UtcNow;
            var entities = await _tokenRepository.SelectMany(x => x.OrganizationId == currentUser.OrganizationId && x.ExpiresAt > now, request.PagingInfo.Skip, request.PagingInfo.Take, x => x.Id, request.PagingInfo.IsAscending);
            if (entities != null)
            {
                for (var i = 0; i < entities.Count; i++)
                {
                    var entity = entities[i];
                    var dto = _tokenFactory.CreateDtoFromEntity(entity);
                    response.Items.Add(dto);
                }
            }

            response.Status = ResponseStatus.Success;
            return response;
        }

        public async Task<IntegrationActiveTokenReadListResponse> GetActiveTokensOfIntegration(IntegrationActiveTokenReadListRequest request)
        {
            var response = new IntegrationActiveTokenReadListResponse();

            var currentUser = _cacheManager.GetCachedCurrentUser(request.CurrentUserId);

            var now = DateTime.UtcNow;
            var entities = await _tokenRepository.SelectMany(x => x.IntegrationUid == request.IntegrationUid 
                                                                       && x.OrganizationId == currentUser.OrganizationId
                                                                       && x.ExpiresAt > now, request.PagingInfo.Skip, request.PagingInfo.Take, x => x.Id, request.PagingInfo.IsAscending);
            if (entities != null)
            {
                for (var i = 0; i < entities.Count; i++)
                {
                    var entity = entities[i];
                    var dto = _tokenFactory.CreateDtoFromEntity(entity);
                    response.Items.Add(dto);
                }
            }

            response.Status = ResponseStatus.Success;
            return response;
        }

        public async Task<IntegrationClientActiveTokenReadListResponse> GetActiveTokensOfIntegrationClient(IntegrationClientActiveTokenReadListRequest request)
        {
            var response = new IntegrationClientActiveTokenReadListResponse();

            var currentUser = _cacheManager.GetCachedCurrentUser(request.CurrentUserId);

            var now = DateTime.UtcNow;
            var entities = await _tokenRepository.SelectMany(x => x.IntegrationClientUid == request.IntegrationClientUid 
                                                                       && x.OrganizationId == currentUser.OrganizationId
                                                                       && x.ExpiresAt > now, request.PagingInfo.Skip, request.PagingInfo.Take, x => x.Id, request.PagingInfo.IsAscending);
            if (entities != null)
            {
                for (var i = 0; i < entities.Count; i++)
                {
                    var entity = entities[i];
                    var dto = _tokenFactory.CreateDtoFromEntity(entity);
                    response.Items.Add(dto);
                }
            }

            response.Status = ResponseStatus.Success;
            return response;
        }

        public async Task<OrganizationTokenRequestLogReadListResponse> GetTokenRequestLogsOfOrganization(OrganizationTokenRequestLogReadListRequest request)
        {
            var response = new OrganizationTokenRequestLogReadListResponse();

            var currentUser = _cacheManager.GetCachedCurrentUser(request.CurrentUserId);

            var entities = await _tokenRequestLogRepository.SelectMany(x => x.OrganizationId == currentUser.OrganizationId, request.PagingInfo.Skip, request.PagingInfo.Take, x => x.Id, request.PagingInfo.IsAscending);
            if (entities != null)
            {
                for (var i = 0; i < entities.Count; i++)
                {
                    var entity = entities[i];
                    var dto = _tokenRequestLogFactory.CreateDtoFromEntity(entity);
                    response.Items.Add(dto);
                }
            }

            response.Status = ResponseStatus.Success;
            return response;
        }

        public async Task<IntegrationTokenRequestLogReadListResponse> GetTokenRequestLogsOfIntegration(IntegrationTokenRequestLogReadListRequest request)
        {
            var response = new IntegrationTokenRequestLogReadListResponse();

            var integration = await _integrationRepository.Select(x => x.Uid == request.IntegrationUid);
            if (integration.IsNotExist())
            {
                response.SetInvalidBecauseEntityNotFound();
                return response;
            }

            var entities = await _tokenRequestLogRepository.SelectMany(x => x.IntegrationUid == request.IntegrationUid, request.PagingInfo.Skip, request.PagingInfo.Take, x => x.Id, request.PagingInfo.IsAscending);
            if (entities != null)
            {
                for (var i = 0; i < entities.Count; i++)
                {
                    var entity = entities[i];
                    var dto = _tokenRequestLogFactory.CreateDtoFromEntity(entity);
                    response.Items.Add(dto);
                }
            }

            response.Status = ResponseStatus.Success;
            return response;
        }

        public async Task<IntegrationClientTokenRequestLogReadListResponse> GetTokenRequestLogsOfIntegrationClient(IntegrationClientTokenRequestLogReadListRequest request)
        {
            var response = new IntegrationClientTokenRequestLogReadListResponse();

            var entities = await _tokenRequestLogRepository.SelectMany(x => x.IntegrationClientUid == request.IntegrationClientUid, request.PagingInfo.Skip, request.PagingInfo.Take, x => x.Id, request.PagingInfo.IsAscending);
            if (entities != null)
            {
                for (var i = 0; i < entities.Count; i++)
                {
                    var entity = entities[i];
                    var dto = _tokenRequestLogFactory.CreateDtoFromEntity(entity);
                    response.Items.Add(dto);
                }
            }

            response.Status = ResponseStatus.Success;
            return response;
        }

        public async Task<AllActiveTokenReadListResponse> GetAllActiveTokens(AllActiveTokenReadListRequest request)
        {
            var response = new AllActiveTokenReadListResponse();

            var entities = await _tokenRepository.SelectMany(null, request.PagingInfo.Skip, request.PagingInfo.Take, x => x.Id, request.PagingInfo.IsAscending);
            if (entities != null)
            {
                for (var i = 0; i < entities.Count; i++)
                {
                    var entity = entities[i];
                    var dto = _tokenFactory.CreateDtoFromEntity(entity);
                    response.Items.Add(dto);
                }
            }

            response.Status = ResponseStatus.Success;
            return response;
        }

        public async Task<AllTokenRequestLogReadListResponse> GetAllTokenRequestLogs(AllTokenRequestLogReadListRequest request)
        {
            var response = new AllTokenRequestLogReadListResponse();

            var entities = await _tokenRequestLogRepository.SelectMany(null, request.PagingInfo.Skip, request.PagingInfo.Take, x => x.Id, request.PagingInfo.IsAscending);
            if (entities != null)
            {
                for (var i = 0; i < entities.Count; i++)
                {
                    var entity = entities[i];
                    var dto = _tokenRequestLogFactory.CreateDtoFromEntity(entity);
                    response.Items.Add(dto);
                }
            }

            response.Status = ResponseStatus.Success;
            return response;
        }
        #endregion
    }
}