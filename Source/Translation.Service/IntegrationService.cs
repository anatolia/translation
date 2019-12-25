using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

using StandardRepository.Helpers;
using StandardRepository.Models;
using StandardUtils.Enumerations;
using StandardUtils.Helpers;
using StandardUtils.Models.DataTransferObjects;

using Translation.Common.Contracts;
using Translation.Common.Models.DataTransferObjects;
using Translation.Common.Models.Requests.Integration;
using Translation.Common.Models.Requests.Integration.IntegrationClient;
using Translation.Common.Models.Requests.Integration.Token;
using Translation.Common.Models.Responses.Integration;
using Translation.Common.Models.Responses.Integration.IntegrationClient;
using Translation.Common.Models.Responses.Integration.Token;
using Translation.Common.Models.Responses.Integration.Token.RequestLog;
using Translation.Data.Entities.Domain;
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
                                  IIntegrationRepository integrationRepository,
                                  IntegrationFactory integrationFactory,
                                  IIntegrationClientRepository integrationClientRepository,
                                  IntegrationClientFactory integrationClientFactory,
                                  ITokenRepository tokenRepository, 
                                  TokenFactory tokenFactory,
                                  ITokenRequestLogRepository tokenRequestLogRepository, 
                                  TokenRequestLogFactory tokenRequestLogFactory,
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
                response.SetInvalidBecauseNotAdmin(nameof(User));
                return response;
            }

            if (await _organizationRepository.Any(x => x.Id == currentUser.OrganizationId && !x.IsActive))
            {
                response.SetInvalidBecauseNotActive(nameof(Organization));
                return response;
            }

            if (await _integrationRepository.Any(x => x.Name == request.Name && x.OrganizationId == currentUser.OrganizationId))
            {
                response.SetInvalidBecauseMustBeUnique(nameof(Integration));
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
                response.SetFailedBecauseNotFound(nameof(Integration));
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

            if (request.PagingInfo.SearchTerm.IsNotEmpty())
            {
                filter = x => x.OrganizationId == currentUser.OrganizationId && x.Name.Contains(request.PagingInfo.SearchTerm);
            }

            List<Integration> entities;
            if (request.PagingInfo.Skip < 1)
            {
                entities = await _integrationRepository.SelectAfter(filter, request.PagingInfo.LastUid, request.PagingInfo.Take, false,
                                                                    new List<OrderByInfo<Integration>>() { new OrderByInfo<Integration>(x => x.Uid, request.PagingInfo.IsAscending) });
            }
            else
            {
                entities = await _integrationRepository.SelectMany(filter, request.PagingInfo.Skip, request.PagingInfo.Take, false,
                                                                   new List<OrderByInfo<Integration>>() { new OrderByInfo<Integration>(x => x.Id, request.PagingInfo.IsAscending) });
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

            var Integration = await _integrationRepository.Select(x => x.Uid == request.IntegrationUid);
            if (Integration.IsNotExist())
            {
                response.SetFailedBecauseNotFound(nameof(Integration));
                return response;
            }

            var revisions = await _integrationRepository.SelectRevisions(Integration.Id);

            for (var i = 0; i < revisions.Count; i++)
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
                response.SetInvalidBecauseNotAdmin(nameof(User));
                return response;
            }

            if (await _organizationRepository.Any(x => x.Id == currentUser.OrganizationId && !x.IsActive))
            {
                response.SetInvalidBecauseNotActive(nameof(Organization));
                return response;
            }

            var Integration = await _integrationRepository.Select(x => x.Uid == request.IntegrationUid);
            if (Integration.IsNotExist())
            {
                response.SetFailedBecauseNotFound(nameof(Integration));
                return response;
            }

            var trimName = request.Name.Trim();
            if (Integration.Name == trimName && Integration.Description == request.Description)
            {
                response.Item = _integrationFactory.CreateDtoFromEntity(Integration);
                response.Status = ResponseStatus.Success;
                return response;
            }

            if (Integration.OrganizationId != currentUser.OrganizationId)
            {
                response.SetInvalid();
                return response;
            }


            if (await _integrationRepository.Any(x => x.Name == trimName
                                                      && x.OrganizationId == currentUser.OrganizationId
                                                      && x.Uid != request.IntegrationUid))
            {
                response.SetInvalidBecauseMustBeUnique(nameof(Integration));
                return response;
            }

            var updatedEntity = _integrationFactory.CreateEntityFromRequest(request, Integration);
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
                response.SetInvalidBecauseNotAdmin(nameof(User));
                return response;
            }

            if (await _organizationRepository.Any(x => x.Id == currentUser.OrganizationId && !x.IsActive))
            {
                response.SetInvalidBecauseNotActive(nameof(Organization));
                return response;
            }

            var Integration = await _integrationRepository.Select(x => x.Uid == request.IntegrationUid);
            if (Integration.IsNotExist())
            {
                response.SetFailedBecauseNotFound(nameof(Integration));
                return response;
            }

            if (Integration.OrganizationId != currentUser.OrganizationId)
            {
                response.SetInvalid();
                return response;
            }

            var IntegrationId = Integration.Id;
            if (await _integrationClientRepository.Any(x => x.IntegrationId == IntegrationId))
            {
                response.SetInvalidBecauseHasChildren(nameof(Integration));
                return response;
            }

            var result = await _integrationRepository.Delete(request.CurrentUserId, Integration.Id);
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
                response.SetInvalidBecauseNotAdmin(nameof(User));
                return response;
            }

            if (await _organizationRepository.Any(x => x.Id == currentUser.OrganizationId && !x.IsActive))
            {
                response.SetInvalidBecauseNotActive(nameof(Organization));
                return response;
            }

            var Integration = await _integrationRepository.Select(x => x.Uid == request.IntegrationUid);
            if (Integration.IsNotExist())
            {
                response.SetFailedBecauseNotFound(nameof(Integration));
                return response;
            }

            if (Integration.OrganizationId != currentUser.OrganizationId)
            {
                response.SetInvalid();
                return response;
            }

            _integrationFactory.UpdateEntityForChangeActivation(Integration);
            var result = await _integrationRepository.Update(request.CurrentUserId, Integration);
            if (result)
            {
                response.Item = _integrationFactory.CreateDtoFromEntity(Integration);
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
                response.SetInvalidBecauseNotActive(nameof(Organization));
                return response;
            }

            var Integration = await _integrationRepository.Select(x => x.Uid == request.IntegrationUid);
            if (Integration.IsNotExist())
            {
                response.SetFailedBecauseNotFound(nameof(Integration));
                return response;
            }

            var revisions = await _integrationRepository.SelectRevisions(Integration.Id);
            if (revisions.All(x => x.Revision != request.Revision))
            {
                response.SetFailedBecauseRevisionNotFound(nameof(Integration));
                return response;
            }

            var result = await _integrationRepository.RestoreRevision(request.CurrentUserId, Integration.Id, request.Revision);
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
                response.SetInvalidBecauseNotAdmin(nameof(User));
                return response;
            }

            if (await _organizationRepository.Any(x => x.Id == currentUser.OrganizationId && !x.IsActive))
            {
                response.SetInvalidBecauseNotActive(nameof(Organization));
                return response;
            }

            var Integration = await _integrationRepository.Select(x => x.Uid == request.IntegrationUid);
            if (Integration.IsNotExist())
            {
                response.SetFailedBecauseNotFound(nameof(Integration));
                return response;
            }

            if (Integration.OrganizationId != currentUser.OrganizationId)
            {
                response.SetInvalid();
                return response;
            }

            var entity = _integrationClientFactory.CreateEntity(Integration);
            await _integrationClientRepository.Insert(request.CurrentUserId, entity);

            response.Item = _integrationClientFactory.CreateDtoFromEntity(entity);
            response.Status = ResponseStatus.Success;
            return response;
        }

        public async Task<IntegrationClientReadResponse> GetIntegrationClient(IntegrationClientReadRequest request)
        {
            var response = new IntegrationClientReadResponse();

            var IntegrationClient = await _integrationClientRepository.Select(x => x.Uid == request.IntegrationClientUid);
            if (IntegrationClient.IsNotExist())
            {
                response.SetFailedBecauseNotFound(nameof(IntegrationClient));
                return response;
            }

            response.Item = _integrationClientFactory.CreateDtoFromEntity(IntegrationClient);
            response.Status = ResponseStatus.Success;
            return response;
        }

        public async Task<IntegrationClientReadListResponse> GetIntegrationClients(IntegrationClientReadListRequest request)
        {
            var response = new IntegrationClientReadListResponse();

            var Integration = await _integrationRepository.Select(x => x.Uid == request.IntegrationUid);
            if (Integration.IsNotExist())
            {
                response.SetFailedBecauseNotFound(nameof(Integration));
                return response;
            }

            var IntegrationId = Integration.Id;
            Expression<Func<IntegrationClient, bool>> filter = x => x.IntegrationId == IntegrationId;

            if (request.PagingInfo.SearchTerm.IsNotEmpty())
            {
                filter = x => x.IntegrationId == IntegrationId && x.Name.Contains(request.PagingInfo.SearchTerm);
            }

            List<IntegrationClient> entities;
            if (request.PagingInfo.Skip < 1)
            {
                entities = await _integrationClientRepository.SelectAfter(filter, request.PagingInfo.LastUid, request.PagingInfo.Take, false,
                                                                          new List<OrderByInfo<IntegrationClient>>() { new OrderByInfo<IntegrationClient>(x => x.Uid, request.PagingInfo.IsAscending) });
            }
            else
            {
                entities = await _integrationClientRepository.SelectMany(filter, request.PagingInfo.Skip, request.PagingInfo.Take, false,
                                                                         new List<OrderByInfo<IntegrationClient>>() { new OrderByInfo<IntegrationClient>(x => x.Id, request.PagingInfo.IsAscending) });

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
                response.SetInvalidBecauseNotAdmin(nameof(User));
                return response;
            }

            if (await _organizationRepository.Any(x => x.Id == currentUser.OrganizationId && !x.IsActive))
            {
                response.SetInvalidBecauseNotActive(nameof(Organization));
                return response;
            }

            var IntegrationClient = await _integrationClientRepository.Select(x => x.Uid == request.IntegrationClientUid);
            if (IntegrationClient.IsNotExist())
            {
                response.SetFailedBecauseNotFound(nameof(IntegrationClient));
                return response;
            }

            if (IntegrationClient.OrganizationId != currentUser.OrganizationId)
            {
                response.SetInvalid();
                return response;
            }

            var Integration = await _integrationRepository.SelectById(IntegrationClient.IntegrationId);
            if (Integration.IsNotExist())
            {
                response.SetFailedBecauseNotFound(nameof(Integration));
                return response;
            }

            if (!Integration.IsActive
                || !IntegrationClient.IsActive)
            {
                response.SetInvalidBecauseNotActive(nameof(Integration));
                return response;
            }

            _integrationClientFactory.UpdateEntityForRefresh(IntegrationClient);
            await _integrationClientRepository.Update(request.CurrentUserId, IntegrationClient);

            response.Item = _integrationClientFactory.CreateDtoFromEntity(IntegrationClient);
            response.Status = ResponseStatus.Success;
            return response;
        }

        public async Task<IntegrationClientDeleteResponse> DeleteIntegrationClient(IntegrationClientDeleteRequest request)
        {
            var response = new IntegrationClientDeleteResponse();

            var currentUser = _cacheManager.GetCachedCurrentUser(request.CurrentUserId);
            if (!currentUser.IsAdmin)
            {
                response.SetInvalidBecauseNotAdmin(nameof(User));
                return response;
            }

            if (await _organizationRepository.Any(x => x.Id == currentUser.OrganizationId && !x.IsActive))
            {
                response.SetInvalidBecauseNotActive(nameof(Organization));
                return response;
            }

            var IntegrationClient = await _integrationClientRepository.Select(x => x.Uid == request.IntegrationClientUid);
            if (IntegrationClient.IsNotExist())
            {
                response.SetFailedBecauseNotFound(nameof(IntegrationClient));
                return response;
            }

            if (IntegrationClient.OrganizationId != currentUser.OrganizationId)
            {
                response.SetInvalid();
                return response;
            }

            var Integration = await _integrationRepository.SelectById(IntegrationClient.IntegrationId);
            if (Integration.IsNotExist())
            {
                response.SetFailedBecauseNotFound(nameof(Integration));
                return response;
            }

            var result = await _integrationClientRepository.Delete(request.CurrentUserId, IntegrationClient.Id);
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
                response.SetInvalidBecauseNotAdmin(nameof(User));
                return response;
            }

            if (await _organizationRepository.Any(x => x.Id == currentUser.OrganizationId && !x.IsActive))
            {
                response.SetInvalidBecauseNotActive(nameof(Organization));
                return response;
            }

            var IntegrationClient = await _integrationClientRepository.Select(x => x.Uid == request.IntegrationClientUid);
            if (IntegrationClient.IsNotExist())
            {
                response.SetFailedBecauseNotFound(nameof(IntegrationClient));
                return response;
            }

            if (IntegrationClient.OrganizationId != currentUser.OrganizationId)
            {
                response.SetInvalid();
                return response;
            }

            var Integration = await _integrationRepository.SelectById(IntegrationClient.IntegrationId);
            if (Integration.IsNotExist())
            {
                response.SetFailedBecauseNotFound(nameof(Integration));
                return response;
            }

            if (!Integration.IsActive)
            {
                response.SetInvalidBecauseNotActive(nameof(Integration));
                return response;
            }

            _integrationClientFactory.UpdateEntityForChangeActivation(IntegrationClient);
            var result = await _integrationClientRepository.Update(request.CurrentUserId, IntegrationClient);
            if (result)
            {
                response.Item = _integrationClientFactory.CreateDtoFromEntity(IntegrationClient);
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

            var IntegrationClient = await _integrationClientRepository.Select(x => x.ClientId == request.ClientId && x.ClientSecret == request.ClientSecret);
            if (IntegrationClient.IsNotExist())
            {
                response.SetFailedBecauseNotFound(nameof(IntegrationClient));
                return response;
            }

            if (await _organizationRepository.Any(x => x.Id == IntegrationClient.OrganizationId && !x.IsActive)
                || await _integrationRepository.Any(x => x.Id == IntegrationClient.IntegrationId && !x.IsActive))
            {
                response.SetInvalidBecauseNotActive(nameof(IntegrationClient));
                return response;
            }

            var token = _tokenFactory.CreateEntityFromRequest(request, IntegrationClient);
            var id = await _tokenRepository.Insert(IntegrationClient.Id, token);
            if (id > 0)
            {
                response.Item = _tokenFactory.CreateDtoFromEntity(token);
                response.Status = ResponseStatus.Success;
                return response;
            }

            response.SetFailed();
            return response;
        }

        public async Task<TokenCreateResponse> CreateTokenWhenUserAuthenticated(TokenGetRequest request)
        {
            var response = new TokenCreateResponse();

            var currentUser = _cacheManager.GetCachedCurrentUser(request.CurrentUserId);
            if (currentUser == null)
            {
                response.SetInvalid();
                return response;
            }

            var IntegrationClient = await _integrationClientRepository.Select(x => x.OrganizationId == currentUser.OrganizationId && x.IsActive);
            if (IntegrationClient.IsNotExist())
            {
                response.SetFailedBecauseNotFound(nameof(IntegrationClient));
                return response;
            }

            if (await _organizationRepository.Any(x => x.Id == IntegrationClient.OrganizationId && !x.IsActive)
                || await _integrationRepository.Any(x => x.Id == IntegrationClient.IntegrationId && !x.IsActive))
            {
                response.SetInvalidBecauseNotActive(nameof(Integration));
                return response;
            }

            var token = _tokenFactory.CreateEntity(IntegrationClient);
            var id = await _tokenRepository.Insert(IntegrationClient.Id, token);
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
                response.SetInvalidBecauseNotAdmin(nameof(User));
                return response;
            }

            var IntegrationClient = await _integrationClientRepository.Select(x => x.Uid == request.IntegrationClientUid);
            if (IntegrationClient.IsNotExist())
            {
                response.SetFailedBecauseNotFound(nameof(IntegrationClient));
                return response;
            }

            if (IntegrationClient.OrganizationId != currentUser.OrganizationId)
            {
                response.SetInvalid();
                return response;
            }

            var token = await _tokenRepository.Select(x => x.AccessToken == request.Token);
            if (token.IsNotExist())
            {
                response.SetFailedBecauseNotFound("token");
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
                response.SetFailedBecauseNotFound(nameof(Project));
                return response;
            }

            var now = DateTime.UtcNow;
            var token = await _tokenRepository.Select(x => x.AccessToken == request.Token && x.ExpiresAt > now);
            if (token.IsNotExist())
            {
                response.SetFailedBecauseNotFound("token");
                return response;
            }

            if (token.OrganizationId != project.OrganizationId)
            {
                response.SetInvalid();
                return response;
            }

            if (await _organizationRepository.Any(x => x.Id == project.OrganizationId && !x.IsActive))
            {
                response.SetInvalidBecauseNotActive(nameof(Organization));
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
            var entities = await _tokenRepository.SelectMany(x => x.OrganizationId == currentUser.OrganizationId && x.ExpiresAt > now,
                                                             request.PagingInfo.Skip, request.PagingInfo.Take, false,
                                                             new List<OrderByInfo<Token>>() { new OrderByInfo<Token>(x => x.Id, request.PagingInfo.IsAscending) });

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
                                                                  && x.ExpiresAt > now, request.PagingInfo.Skip, request.PagingInfo.Take, false,
                                                                  new List<OrderByInfo<Token>>() { new OrderByInfo<Token>(x => x.Id, request.PagingInfo.IsAscending) });

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
                                                                       && x.ExpiresAt > now, request.PagingInfo.Skip, request.PagingInfo.Take, false,
                                                                       new List<OrderByInfo<Token>>() { new OrderByInfo<Token>(x => x.Id, request.PagingInfo.IsAscending) });
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

            var entities = await _tokenRequestLogRepository.SelectMany(x => x.OrganizationId == currentUser.OrganizationId,
                                                                       request.PagingInfo.Skip, request.PagingInfo.Take, false,
                                                                       new List<OrderByInfo<TokenRequestLog>>() { new OrderByInfo<TokenRequestLog>(x => x.Id, request.PagingInfo.IsAscending) });
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

            var Integration = await _integrationRepository.Select(x => x.Uid == request.IntegrationUid);
            if (Integration.IsNotExist())
            {
                response.SetFailedBecauseNotFound(nameof(Integration));
                return response;
            }

            var entities = await _tokenRequestLogRepository.SelectMany(x => x.IntegrationUid == request.IntegrationUid, request.PagingInfo.Skip,
                                                                       request.PagingInfo.Take, false,
                                                                       new List<OrderByInfo<TokenRequestLog>>() { new OrderByInfo<TokenRequestLog>(x => x.Id, request.PagingInfo.IsAscending) });

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

            var entities = await _tokenRequestLogRepository.SelectMany(x => x.IntegrationClientUid == request.IntegrationClientUid, request.PagingInfo.Skip,
                                                                        request.PagingInfo.Take, false,
                                                                        new List<OrderByInfo<TokenRequestLog>>() { new OrderByInfo<TokenRequestLog>(x => x.Id, request.PagingInfo.IsAscending) });
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

            var entities = await _tokenRepository.SelectMany(null, request.PagingInfo.Skip, request.PagingInfo.Take, false,
                                                             new List<OrderByInfo<Token>>() { new OrderByInfo<Token>(x => x.Id, request.PagingInfo.IsAscending) });
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

            var entities = await _tokenRequestLogRepository.SelectMany(null, request.PagingInfo.Skip, request.PagingInfo.Take, false,
                                                                       new List<OrderByInfo<TokenRequestLog>>() { new OrderByInfo<TokenRequestLog>(x => x.Id, request.PagingInfo.IsAscending) });
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