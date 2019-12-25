using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

using StandardRepository.Helpers;
using StandardRepository.Models;
using StandardUtils.Enumerations;
using StandardUtils.Helpers;

using Translation.Common.Contracts;
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
using Translation.Common.Models.Responses.TranslationProvider;
using Translation.Common.Models.Responses.User;
using Translation.Common.Models.Responses.User.LoginLog;
using Translation.Common.Models.Shared;
using Translation.Data.Entities.Domain;
using Translation.Data.Entities.Main;
using Translation.Data.Factories;
using Translation.Data.Repositories.Contracts;
using Translation.Service.Managers;

namespace Translation.Service
{
    public class AdminService : IAdminService
    {
        private readonly CacheManager _cacheManager;
        private readonly CryptoHelper _cryptoHelper;
        private readonly IOrganizationRepository _organizationRepository;
        private readonly OrganizationFactory _organizationFactory;
        private readonly IUserRepository _userRepository;
        private readonly UserFactory _userFactory;
        private readonly IJournalRepository _journalRepository;
        private readonly JournalFactory _journalFactory;
        private readonly ITranslationProviderRepository _translationProviderRepository;
        private readonly TranslationProviderFactory _translationProviderFactory;
        private readonly ITokenRequestLogRepository _tokenRequestLogRepository;
        private readonly TokenRequestLogFactory _tokenRequestLogFactory;
        private readonly ISendEmailLogRepository _sendEmailLogRepository;
        private readonly SendEmailLogFactory _sendEmailLogFactory;
        private readonly IUserLoginLogRepository _userLoginLogRepository;
        private readonly UserLoginLogFactory _userLoginLogFactory;

        public AdminService(CacheManager cacheManager,
                            CryptoHelper cryptoHelper,
                            IOrganizationRepository organizationRepository,
                            OrganizationFactory organizationFactory,
                            IUserRepository userRepository,
                            UserFactory userFactory,
                            IJournalRepository journalRepository,
                            JournalFactory journalFactory,
                            ITranslationProviderRepository translationProviderRepository,
                            TranslationProviderFactory translationProviderFactory,
                            ITokenRequestLogRepository tokenRequestLogRepository,
                            TokenRequestLogFactory tokenRequestLogFactory,
                            ISendEmailLogRepository sendEmailLogRepository,
                            SendEmailLogFactory sendEmailLogFactory,
                            IUserLoginLogRepository userLoginLogRepository,
                            UserLoginLogFactory userLoginLogFactory)
        {
            _cacheManager = cacheManager;
            _cryptoHelper = cryptoHelper;
            _organizationRepository = organizationRepository;
            _organizationFactory = organizationFactory;
            _userRepository = userRepository;
            _userFactory = userFactory;
            _journalRepository = journalRepository;
            _journalFactory = journalFactory;
            _translationProviderRepository = translationProviderRepository;
            _translationProviderFactory = translationProviderFactory;
            _tokenRequestLogRepository = tokenRequestLogRepository;
            _tokenRequestLogFactory = tokenRequestLogFactory;
            _sendEmailLogRepository = sendEmailLogRepository;
            _sendEmailLogFactory = sendEmailLogFactory;
            _userLoginLogRepository = userLoginLogRepository;
            _userLoginLogFactory = userLoginLogFactory;
        }

        public async Task<OrganizationReadListResponse> GetOrganizations(OrganizationReadListRequest request)
        {
            var response = new OrganizationReadListResponse();

            var currentUser = _cacheManager.GetCachedCurrentUser(request.CurrentUserId);
            if (!currentUser.IsSuperAdmin)
            {
                response.SetInvalid();
                return response;
            }

            Expression<Func<Organization, bool>> filter = null;
            if (request.PagingInfo.SearchTerm.IsNotEmpty())
            {
                filter = x => x.Name.Contains(request.PagingInfo.SearchTerm);
            }

            List<Organization> entities;
            if (request.PagingInfo.Skip < 1)
            {
                entities = await _organizationRepository.SelectAfter(filter, request.PagingInfo.LastUid, request.PagingInfo.Take, false,
                                                                     new List<OrderByInfo<Organization>>() { new OrderByInfo<Organization>(x => x.Uid, request.PagingInfo.IsAscending) });
            }
            else
            {
                entities = await _organizationRepository.SelectMany(filter, request.PagingInfo.Skip, request.PagingInfo.Take, false,
                                                                    new List<OrderByInfo<Organization>>() { new OrderByInfo<Organization>(x => x.Id, request.PagingInfo.IsAscending) });
            }

            if (entities != null)
            {
                for (var i = 0; i < entities.Count; i++)
                {
                    var entity = entities[i];
                    var dto = _organizationFactory.CreateDtoFromEntity(entity);
                    response.Items.Add(dto);
                }
            }

            response.PagingInfo.Skip = request.PagingInfo.Skip;
            response.PagingInfo.Take = request.PagingInfo.Take;
            response.PagingInfo.LastUid = request.PagingInfo.LastUid;
            response.PagingInfo.IsAscending = request.PagingInfo.IsAscending;
            response.PagingInfo.TotalItemCount = await _organizationRepository.Count(filter);

            response.Status = ResponseStatus.Success;
            return response;
        }

        public async Task<AllUserReadListResponse> GetAllUsers(AllUserReadListRequest request)
        {
            var response = new AllUserReadListResponse();

            var currentUser = _cacheManager.GetCachedCurrentUser(request.CurrentUserId);
            if (!currentUser.IsSuperAdmin)
            {
                response.SetInvalid();
                return response;
            }

            Expression<Func<User, bool>> filter = null;
            if (request.PagingInfo.SearchTerm.IsNotEmpty())
            {
                filter = x => x.Name.Contains(request.PagingInfo.SearchTerm);
            }

            List<User> entities;
            if (request.PagingInfo.Skip < 1)
            {
                entities = await _userRepository.SelectAfter(filter, request.PagingInfo.LastUid, request.PagingInfo.Take, false,
                                                             new List<OrderByInfo<User>>() { new OrderByInfo<User>(x => x.Uid, request.PagingInfo.IsAscending) });
            }
            else
            {
                entities = await _userRepository.SelectMany(filter, request.PagingInfo.Skip, request.PagingInfo.Take, false,
                                                            new List<OrderByInfo<User>>() { new OrderByInfo<User>(x => x.Id, request.PagingInfo.IsAscending) });
            }

            if (entities != null)
            {
                for (var i = 0; i < entities.Count; i++)
                {
                    var entity = entities[i];
                    var dto = _userFactory.CreateDtoFromEntity(entity);
                    response.Items.Add(dto);
                }
            }

            response.PagingInfo.Skip = request.PagingInfo.Skip;
            response.PagingInfo.Take = request.PagingInfo.Take;
            response.PagingInfo.LastUid = request.PagingInfo.LastUid;
            response.PagingInfo.IsAscending = request.PagingInfo.IsAscending;
            response.PagingInfo.TotalItemCount = await _userRepository.Count(filter);

            response.Status = ResponseStatus.Success;
            return response;
        }

        public async Task<SuperAdminUserReadListResponse> GetSuperAdmins(SuperAdminUserReadListRequest request)
        {
            var response = new SuperAdminUserReadListResponse();

            var currentUser = _cacheManager.GetCachedCurrentUser(request.CurrentUserId);
            if (!currentUser.IsSuperAdmin)
            {
                response.SetInvalid();
                return response;
            }

            Expression<Func<User, bool>> filter = x => x.IsAdmin;

            if (request.PagingInfo.SearchTerm.IsNotEmpty())
            {
                filter = x => x.Name.Contains(request.PagingInfo.SearchTerm) && x.IsAdmin;
            }

            List<User> entities;
            if (request.PagingInfo.Skip < 1)
            {
                entities = await _userRepository.SelectAfter(filter, request.PagingInfo.LastUid, request.PagingInfo.Take, false,
                                                             new List<OrderByInfo<User>>() { new OrderByInfo<User>(x => x.Uid, request.PagingInfo.IsAscending) });
            }
            else
            {
                entities = await _userRepository.SelectMany(filter, request.PagingInfo.Skip, request.PagingInfo.Take, false,
                                                            new List<OrderByInfo<User>>() { new OrderByInfo<User>(x => x.Id, request.PagingInfo.IsAscending) });
            }

            if (entities != null)
            {
                for (var i = 0; i < entities.Count; i++)
                {
                    var entity = entities[i];
                    var dto = _userFactory.CreateDtoFromEntity(entity);
                    response.Items.Add(dto);
                }
            }

            response.PagingInfo.Skip = request.PagingInfo.Skip;
            response.PagingInfo.Take = request.PagingInfo.Take;
            response.PagingInfo.LastUid = request.PagingInfo.LastUid;
            response.PagingInfo.IsAscending = request.PagingInfo.IsAscending;
            response.PagingInfo.TotalItemCount = await _userRepository.Count(filter);

            response.Status = ResponseStatus.Success;
            return response;
        }

        public async Task<AdminInviteResponse> InviteSuperAdminUser(AdminInviteRequest request)
        {
            var response = new AdminInviteResponse();

            var currentUser = _cacheManager.GetCachedCurrentUser(request.CurrentUserId);
            if (!currentUser.IsSuperAdmin)
            {
                response.SetInvalid();
                return response;
            }

            var user = await _userRepository.Select(x => x.Email == request.Email);
            if (user.IsExist())
            {
                response.ErrorMessages.Add("user_already_invited");
                response.Status = ResponseStatus.Invalid;
                return response;
            }

            var invitedUser = _userFactory.CreateEntityFromRequest(request, currentUser, _cryptoHelper.GetSaltAsString());
            var id = await _userRepository.Insert(request.CurrentUserId, invitedUser);
            if (id > 0)
            {
                // todo:send invite email

                response.Status = ResponseStatus.Success;
                return response;
            }

            response.SetFailed();
            return response;
        }

        public async Task<AdminInviteValidateResponse> ValidateSuperAdminUserInvitation(AdminInviteValidateRequest request)
        {
            var response = new AdminInviteValidateResponse();

            var user = await _userRepository.Select(x => x.InvitationToken == request.Token && x.Email == request.Email);
            if (user.IsNotExist())
            {
                response.SetFailedBecauseNotFound(nameof(User));
                return response;
            }

            if (await _organizationRepository.Any(x => x.Id == user.OrganizationId && !x.IsActive))
            {
                response.SetInvalidBecauseNotActive(nameof(Organization));
                return response;
            }

            if (user.InvitedAt.HasValue
                && user.InvitedAt.Value.AddDays(2) > DateTime.UtcNow)
            {
                response.Item.FirstName = user.FirstName;
                response.Item.LastName = user.LastName;
                response.Item.Email = user.Email;
                response.Status = ResponseStatus.Success;
                return response;
            }

            response.SetFailed();
            return response;
        }

        public async Task<AdminAcceptInviteResponse> AcceptSuperAdminUserInvite(AdminAcceptInviteRequest request)
        {
            var response = new AdminAcceptInviteResponse();

            var user = await _userRepository.Select(x => x.InvitationToken == request.Token && x.Email == request.Email);

            if (user.IsNotExist())
            {
                response.SetFailedBecauseNotFound(nameof(User));
                return response;
            }

            if (await _organizationRepository.Any(x => x.Id == user.OrganizationId && !x.IsActive))
            {
                response.SetInvalidBecauseNotActive(nameof(Organization));
                return response;
            }

            if (user.InvitedAt.HasValue
                && user.InvitedAt.Value.AddDays(2) > DateTime.UtcNow)
            {
                user.FirstName = request.FirstName;
                user.LastName = request.LastName;
                user.PasswordHash = _cryptoHelper.Hash(request.Password, user.ObfuscationSalt);

                // todo:send welcome email


                //todo:uow
                var result = await _userRepository.Update(user.Id, user);
                if (result)
                {
                    var organization = _cacheManager.GetCachedOrganization(user.OrganizationUid);
                    organization.UserCount++;
                    result = await _organizationRepository.Update(user.Id, organization);

                    if (result)
                    {
                        _cacheManager.UpsertOrganizationCache(organization, _organizationFactory.MapCurrentOrganization(organization));

                        response.Status = ResponseStatus.Success;
                        return response;
                    }
                }
            }

            response.SetFailed();
            return response;
        }

        public async Task<UserChangeActivationResponse> ChangeActivation(UserChangeActivationRequest request)
        {
            var response = new UserChangeActivationResponse();

            var currentUser = _cacheManager.GetCachedCurrentUser(request.CurrentUserId);
            if (!currentUser.IsSuperAdmin)
            {
                response.SetInvalid();
                return response;
            }

            var user = await _userRepository.Select(x => x.Uid == request.UserUid);
            if (user.IsNotExist())
            {
                response.SetFailedBecauseNotFound(nameof(User));
                return response;
            }

            user.IsActive = !user.IsActive;
            var result = await _userRepository.Update(request.CurrentUserId, user);
            if (result)
            {
                _cacheManager.UpsertUserCache(user, _userFactory.MapCurrentUser(user, _cacheManager.GetLanguageIsoCode2Char(user.LanguageId)));

                response.Status = ResponseStatus.Success;
                return response;
            }

            response.SetFailed();
            return response;
        }

        public async Task<OrganizationChangeActivationResponse> OrganizationChangeActivation(OrganizationChangeActivationRequest request)
        {
            var response = new OrganizationChangeActivationResponse();

            var currentUser = _cacheManager.GetCachedCurrentUser(request.CurrentUserId);
            if (!currentUser.IsSuperAdmin)
            {
                response.SetInvalid();
                return response;
            }

            var organization = await _organizationRepository.Select(x => x.Uid == request.OrganizationUid);
            if (organization.IsNotExist())
            {
                response.SetFailedBecauseNotFound(nameof(Organization));
                return response;
            }

            organization.IsActive = !organization.IsActive;
            var result = await _organizationRepository.Update(request.CurrentUserId, organization);
            if (result)
            {
                _cacheManager.UpsertOrganizationCache(organization, _organizationFactory.MapCurrentOrganization(organization));

                response.Status = ResponseStatus.Success;
                return response;
            }

            response.SetFailed();
            return response;
        }

        public async Task<TranslationProviderChangeActivationResponse> TranslationProviderChangeActivation(TranslationProviderChangeActivationRequest request)
        {
            var response = new TranslationProviderChangeActivationResponse();

            var currentUser = _cacheManager.GetCachedCurrentUser(request.CurrentUserId);
            if (!currentUser.IsSuperAdmin)
            {
                response.SetInvalid();
                return response;
            }

            var allTranslationProviders = await _translationProviderRepository.SelectAll(x => x.Id != 0, false);
            if (allTranslationProviders == null)
            {
                response.SetFailedBecauseNotFound(nameof(TranslationProvider));
                return response;
            }

            var selectedTranslationProvider = await _translationProviderRepository.Select(x => x.Uid == request.TranslationProviderUid);
            if (selectedTranslationProvider.CredentialValue == "" )
            {
                response.Status = ResponseStatus.Invalid;
                response.ErrorMessages.Add("please_edit_translation_api_value");
                return response;
            }

            for (var i = 0; i < allTranslationProviders.Count; i++)
            {
                var translationProvider = allTranslationProviders[i];

                if (translationProvider.CredentialValue == selectedTranslationProvider.CredentialValue)
                {
                    selectedTranslationProvider.IsActive = !selectedTranslationProvider.IsActive;
                }
                else
                {
                    translationProvider.IsActive = false;
                    await _translationProviderRepository.Update(request.CurrentUserId, translationProvider);
                }
            }

            var result = await _translationProviderRepository.Update(request.CurrentUserId, selectedTranslationProvider);
            if (result)
            {
                if (selectedTranslationProvider.IsActive)
                {
                    var activeTranslationProvider = _translationProviderFactory.MapActiveTranslationProvider(selectedTranslationProvider);

                    _cacheManager.UpsertActiveTranslationProviderCache(activeTranslationProvider);
                }
                response.Status = ResponseStatus.Success;
                return response;
            }

            response.SetFailed();
            return response;
        }

        public async Task<AdminDemoteResponse> DemoteToUser(AdminDemoteRequest request)
        {
            var response = new AdminDemoteResponse();

            var currentUser = _cacheManager.GetCachedCurrentUser(request.CurrentUserId);
            if (!currentUser.IsSuperAdmin)
            {
                response.SetInvalid();
                return response;
            }

            var user = await _userRepository.Select(x => x.Uid == request.UserUid);
            if (user.IsNotExist())
            {
                response.SetFailedBecauseNotFound(nameof(User));
                return response;
            }

            if (!user.IsAdmin)
            {
                response.SetInvalidBecauseNotAdmin(nameof(User));
                return response;
            }

            user = _userFactory.CreateEntityFromRequest(request, user);
            var result = await _userRepository.Update(request.CurrentUserId, user);
            if (result)
            {
                _cacheManager.UpsertUserCache(user, _userFactory.MapCurrentUser(user, _cacheManager.GetLanguageIsoCode2Char(user.LanguageId)));

                response.Item = _userFactory.CreateDtoFromEntity(user);
                response.Status = ResponseStatus.Success;
                return response;
            }

            response.SetFailed();
            return response;
        }

        public async Task<AdminUpgradeResponse> UpgradeToAdmin(AdminUpgradeRequest request)
        {
            var response = new AdminUpgradeResponse();

            var currentUser = _cacheManager.GetCachedCurrentUser(request.CurrentUserId);
            if (!currentUser.IsSuperAdmin)
            {
                response.SetInvalidBecauseNotSuperAdmin(nameof(CurrentUser));
                return response;
            }

            var user = await _userRepository.Select(x => x.Uid == request.UserUid);
            if (user.IsNotExist())
            {
                response.SetFailedBecauseNotFound(nameof(User));
                return response;
            }

            if (user.IsAdmin)
            {
                response.SetInvalidBecauseAdmin(nameof(User));
                return response;
            }

            if (await _organizationRepository.Any(x => x.Id == user.OrganizationId && !x.IsActive))
            {
                response.SetInvalidBecauseNotActive(nameof(Organization));
                return response;
            }

            user = _userFactory.CreateEntityFromRequest(request, user);
            var result = await _userRepository.Update(request.CurrentUserId, user);
            if (result)
            {
                _cacheManager.UpsertUserCache(user, _userFactory.MapCurrentUser(user, _cacheManager.GetLanguageIsoCode2Char(user.LanguageId)));

                response.Item = _userFactory.CreateDtoFromEntity(user);
                response.Status = ResponseStatus.Success;
                return response;
            }

            response.SetFailed();
            return response;
        }

        public async Task<JournalReadListResponse> GetJournals(AllJournalReadListRequest request)
        {
            var response = new JournalReadListResponse();

            var currentUser = _cacheManager.GetCachedCurrentUser(request.CurrentUserId);
            if (!currentUser.IsSuperAdmin)
            {
                response.SetInvalid();
                return response;
            }

            Expression<Func<Journal, bool>> filter = null;
            if (request.PagingInfo.SearchTerm.IsNotEmpty())
            {
                filter = x => x.Name.Contains(request.PagingInfo.SearchTerm);
            }

            List<Journal> entities;
            if (request.PagingInfo.Skip < 1)
            {
                entities = await _journalRepository.SelectAfter(filter, request.PagingInfo.LastUid, request.PagingInfo.Take, false,
                                                                new List<OrderByInfo<Journal>>() { new OrderByInfo<Journal>(x => x.Uid, request.PagingInfo.IsAscending) });
            }
            else
            {
                entities = await _journalRepository.SelectMany(filter, request.PagingInfo.Skip, request.PagingInfo.Take, false,
                                                               new List<OrderByInfo<Journal>>() { new OrderByInfo<Journal>(x => x.Id, request.PagingInfo.IsAscending) });
            }

            if (entities != null)
            {
                for (var i = 0; i < entities.Count; i++)
                {
                    var entity = entities[i];
                    var dto = _journalFactory.CreateDtoFromEntity(entity);
                    response.Items.Add(dto);
                }
            }

            response.PagingInfo.Skip = request.PagingInfo.Skip;
            response.PagingInfo.Take = request.PagingInfo.Take;
            response.PagingInfo.LastUid = request.PagingInfo.LastUid;
            response.PagingInfo.IsAscending = request.PagingInfo.IsAscending;
            response.PagingInfo.TotalItemCount = await _journalRepository.Count(filter);

            response.Status = ResponseStatus.Success;
            return response;
        }

        public async Task<AllTokenRequestLogReadListResponse> GetTokenRequestLogs(AllTokenRequestLogReadListRequest request)
        {
            var response = new AllTokenRequestLogReadListResponse();

            var currentUser = _cacheManager.GetCachedCurrentUser(request.CurrentUserId);
            if (!currentUser.IsSuperAdmin)
            {
                response.SetInvalid();
                return response;
            }

            Expression<Func<TokenRequestLog, bool>> filter = null;
            if (request.PagingInfo.SearchTerm.IsNotEmpty())
            {
                filter = x => x.Name.Contains(request.PagingInfo.SearchTerm);
            }

            List<TokenRequestLog> entities;
            if (request.PagingInfo.Skip < 1)
            {
                entities = await _tokenRequestLogRepository.SelectAfter(filter, request.PagingInfo.LastUid, request.PagingInfo.Take, false,
                                                                        new List<OrderByInfo<TokenRequestLog>>() { new OrderByInfo<TokenRequestLog>(x => x.Uid, request.PagingInfo.IsAscending) });
            }
            else
            {
                entities = await _tokenRequestLogRepository.SelectMany(filter, request.PagingInfo.Skip, request.PagingInfo.Take, false,
                                                                       new List<OrderByInfo<TokenRequestLog>>() { new OrderByInfo<TokenRequestLog>(x => x.Id, request.PagingInfo.IsAscending) });
            }

            if (entities != null)
            {
                for (var i = 0; i < entities.Count; i++)
                {
                    var entity = entities[i];
                    var dto = _tokenRequestLogFactory.CreateDtoFromEntity(entity);
                    response.Items.Add(dto);
                }
            }

            response.PagingInfo.Skip = request.PagingInfo.Skip;
            response.PagingInfo.Take = request.PagingInfo.Take;
            response.PagingInfo.LastUid = request.PagingInfo.LastUid;
            response.PagingInfo.IsAscending = request.PagingInfo.IsAscending;
            response.PagingInfo.TotalItemCount = await _tokenRequestLogRepository.Count(filter);

            response.Status = ResponseStatus.Success;
            return response;
        }

        public async Task<AllSendEmailReadListResponse> GetSendEmailLogs(AllSendEmailLogReadListRequest request)
        {
            var response = new AllSendEmailReadListResponse();

            var currentUser = _cacheManager.GetCachedCurrentUser(request.CurrentUserId);
            if (!currentUser.IsSuperAdmin)
            {
                response.SetInvalid();
                return response;
            }

            Expression<Func<SendEmailLog, bool>> filter = null;
            if (request.PagingInfo.SearchTerm.IsNotEmpty())
            {
                filter = x => x.Name.Contains(request.PagingInfo.SearchTerm);
            }

            List<SendEmailLog> entities;
            if (request.PagingInfo.Skip < 1)
            {
                entities = await _sendEmailLogRepository.SelectAfter(filter, request.PagingInfo.LastUid, request.PagingInfo.Take, false,
                                                                     new List<OrderByInfo<SendEmailLog>>() { new OrderByInfo<SendEmailLog>(x => x.Uid, request.PagingInfo.IsAscending) });
            }
            else
            {
                entities = await _sendEmailLogRepository.SelectMany(filter, request.PagingInfo.Skip, request.PagingInfo.Take, false,
                                                                   new List<OrderByInfo<SendEmailLog>>() { new OrderByInfo<SendEmailLog>(x => x.Id, request.PagingInfo.IsAscending) });
            }

            if (entities != null)
            {
                for (var i = 0; i < entities.Count; i++)
                {
                    var entity = entities[i];
                    var dto = _sendEmailLogFactory.CreateDtoFromEntity(entity);
                    response.Items.Add(dto);
                }
            }

            response.PagingInfo.Skip = request.PagingInfo.Skip;
            response.PagingInfo.Take = request.PagingInfo.Take;
            response.PagingInfo.LastUid = request.PagingInfo.LastUid;
            response.PagingInfo.IsAscending = request.PagingInfo.IsAscending;
            response.PagingInfo.TotalItemCount = await _sendEmailLogRepository.Count(filter);

            response.Status = ResponseStatus.Success;
            return response;
        }

        public async Task<AllLoginLogReadListResponse> GetAllUserLoginLogs(AllLoginLogReadListRequest request)
        {
            var response = new AllLoginLogReadListResponse();

            var currentUser = _cacheManager.GetCachedCurrentUser(request.CurrentUserId);
            if (!currentUser.IsSuperAdmin)
            {
                response.SetInvalid();
                return response;
            }

            Expression<Func<UserLoginLog, bool>> filter = null;
            if (request.PagingInfo.SearchTerm.IsNotEmpty())
            {
                filter = x => x.Name.Contains(request.PagingInfo.SearchTerm);
            }

            List<UserLoginLog> entities;
            if (request.PagingInfo.Skip < 1)
            {
                entities = await _userLoginLogRepository.SelectAfter(filter, request.PagingInfo.LastUid, request.PagingInfo.Take, false,
                                                                     new List<OrderByInfo<UserLoginLog>>() { new OrderByInfo<UserLoginLog>(x => x.Uid, request.PagingInfo.IsAscending) });
            }
            else
            {
                entities = await _userLoginLogRepository.SelectMany(filter, request.PagingInfo.Skip, request.PagingInfo.Take, false,
                                                                    new List<OrderByInfo<UserLoginLog>>() { new OrderByInfo<UserLoginLog>(x => x.Id, request.PagingInfo.IsAscending) });
            }

            if (entities != null)
            {
                for (var i = 0; i < entities.Count; i++)
                {
                    var entity = entities[i];
                    var dto = _userLoginLogFactory.CreateDtoFromEntity(entity);
                    response.Items.Add(dto);
                }
            }

            response.PagingInfo.Skip = request.PagingInfo.Skip;
            response.PagingInfo.Take = request.PagingInfo.Take;
            response.PagingInfo.LastUid = request.PagingInfo.LastUid;
            response.PagingInfo.IsAscending = request.PagingInfo.IsAscending;
            response.PagingInfo.TotalItemCount = await _userLoginLogRepository.Count(filter);

            response.Status = ResponseStatus.Success;
            return response;
        }
    }
}