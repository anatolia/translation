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
using Translation.Common.Models.Requests.Organization;
using Translation.Common.Models.Requests.User;
using Translation.Common.Models.Requests.User.LoginLog;
using Translation.Common.Models.Responses.Organization;
using Translation.Common.Models.Responses.User;
using Translation.Common.Models.Responses.User.LoginLog;
using Translation.Common.Models.Shared;
using Translation.Data.Entities.Domain;
using Translation.Data.Entities.Main;
using Translation.Data.Factories;
using Translation.Data.Repositories.Contracts;
using Translation.Data.UnitOfWorks.Contracts;
using Translation.Service.Managers;
using Language = Translation.Data.Entities.Parameter.Language;

namespace Translation.Service
{
    public class OrganizationService : IOrganizationService
    {
        private readonly CacheManager _cacheManager;
        private readonly CryptoHelper _cryptoHelper;

        private readonly ISignUpUnitOfWork _signUpUnitOfWork;
        private readonly ILogOnUnitOfWork _logOnUnitOfWork;
        private readonly IUserRepository _userRepository;
        private readonly UserFactory _userFactory;
        private readonly IOrganizationRepository _organizationRepository;
        private readonly LabelFactory _labelFactory;
        private readonly OrganizationFactory _organizationFactory;
        private readonly IUserLoginLogRepository _userLoginLogRepository;
        private readonly UserLoginLogFactory _userLoginLogFactory;
        private readonly IntegrationFactory _integrationFactory;
        private readonly ILabelRepository _labelRepository;
        private readonly IntegrationClientFactory _integrationClientFactory;
        private readonly ProjectFactory _projectFactory;
        private readonly ILanguageRepository _languageRepository;

        public OrganizationService(CacheManager cacheManager, CryptoHelper cryptoHelper,
                                   ISignUpUnitOfWork signUpUnitOfWork,
                                   ILogOnUnitOfWork logOnUnitOfWork,
                                   IUserRepository userRepository, UserFactory userFactory,
                                   IOrganizationRepository organizationRepository,
                                   LabelFactory labelFactory,
                                   OrganizationFactory organizationFactory,
                                   IUserLoginLogRepository userLoginLogRepository, UserLoginLogFactory userLoginLogFactory,
                                   IntegrationFactory integrationFactory,
                                   ILabelRepository labelRepository,
                                   IntegrationClientFactory integrationClientFactory,
                                   ProjectFactory projectFactory,
                                   ILanguageRepository languageRepository)
        {
            _cacheManager = cacheManager;
            _cryptoHelper = cryptoHelper;

            _signUpUnitOfWork = signUpUnitOfWork;
            _logOnUnitOfWork = logOnUnitOfWork;
            _userRepository = userRepository;
            _userFactory = userFactory;
            _organizationRepository = organizationRepository;
            _labelFactory = labelFactory;
            _organizationFactory = organizationFactory;
            _userLoginLogRepository = userLoginLogRepository;
            _userLoginLogFactory = userLoginLogFactory;
            _integrationFactory = integrationFactory;
            _labelRepository = labelRepository;
            _integrationClientFactory = integrationClientFactory;
            _projectFactory = projectFactory;
            _languageRepository = languageRepository;
        }

        public async Task<SignUpResponse> CreateOrganizationWithAdmin(SignUpRequest request)
        {
            var response = new SignUpResponse();

            var user = await _userRepository.Select(x => x.Email == request.Email);
            if (user.IsExist())
            {
                response.ErrorMessages.Add("email_already_exist");
                response.SetInvalid();
                return response;
            }

            var organization = await _organizationRepository.Select(x => x.Name == request.OrganizationName);
            if (organization.IsExist())
            {
                response.SetInvalidBecauseMustBeUnique(nameof(Organization));
                return response;
            }

            organization = _organizationFactory.CreateEntityFromRequest(request, _cryptoHelper.GetKeyAsString(), _cryptoHelper.GetIVAsString());

            var salt = _cryptoHelper.GetSaltAsString();
            var passwordHash = _cryptoHelper.Hash(request.Password, salt);
            user = _userFactory.CreateEntityFromRequest(request, organization, salt, passwordHash);

            var language = await _languageRepository.Select(x => x.Uid == request.LanguageUid);
            user.LanguageId = language.Id;
            user.LanguageUid = language.Uid;
            user.LanguageName = language.Name;
            user.LanguageIconUrl = language.IconUrl;

            var loginLog = _userLoginLogFactory.CreateEntityFromRequest(request, user);
            var Integration = _integrationFactory.CreateDefault(organization);
            var IntegrationClient = _integrationClientFactory.CreateEntity(Integration);
            var project = _projectFactory.CreateDefault(organization, language);

            var (uowResult,
                 insertedOrganization,
                 insertedUser) = await _signUpUnitOfWork.DoWork(organization, user, loginLog,
                                                                Integration, IntegrationClient, project);
            if (uowResult)
            {
                // todo:send welcome email

                // todo:send email log

                _cacheManager.UpsertUserCache(insertedUser, _userFactory.MapCurrentUser(insertedUser, _cacheManager.GetLanguageIsoCode2Char(insertedUser.LanguageId)));
                _cacheManager.UpsertOrganizationCache(insertedOrganization, _organizationFactory.MapCurrentOrganization(insertedOrganization));

                response.Status = ResponseStatus.Success;
                return response;
            }

            response.SetFailed();
            return response;
        }

        public OrganizationReadResponse GetOrganization(OrganizationReadRequest request)
        {
            var response = new OrganizationReadResponse();

            var entity = _cacheManager.GetCachedOrganization(request.OrganizationUid);

            response.Item = _organizationFactory.CreateDtoFromEntity(entity);
            response.Status = ResponseStatus.Success;
            return response;
        }

        public async Task<OrganizationReadListResponse> GetOrganizations(OrganizationReadListRequest request)
        {
            var response = new OrganizationReadListResponse();

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

        public async Task<OrganizationRevisionReadListResponse> GetOrganizationRevisions(OrganizationRevisionReadListRequest request)
        {
            var response = new OrganizationRevisionReadListResponse();

            var organization = await _organizationRepository.Select(x => x.Uid == request.OrganizationUid);
            if (organization.IsNotExist())
            {
                response.SetFailedBecauseNotFound(nameof(Organization));
                return response;
            }

            var revisions = await _organizationRepository.SelectRevisions(organization.Id);

            for (var i = 0; i < revisions.Count; i++)
            {
                var revision = revisions[i];

                var revisionDto = new RevisionDto<OrganizationDto>();
                revisionDto.Revision = revision.Revision;
                revisionDto.RevisionedAt = revision.RevisionedAt;

                var user = _cacheManager.GetCachedUser(revision.RevisionedBy);
                revisionDto.RevisionedByUid = user.Uid;
                revisionDto.RevisionedByName = user.Name;

                revisionDto.Item = _organizationFactory.CreateDtoFromEntity(revision.Entity);

                response.Items.Add(revisionDto);
            }

            response.Status = ResponseStatus.Success;
            return response;
        }

        public async Task<OrganizationEditResponse> EditOrganization(OrganizationEditRequest request)
        {
            var response = new OrganizationEditResponse();

            var currentUser = _cacheManager.GetCachedCurrentUser(request.CurrentUserId);
            if (!currentUser.IsAdmin)
            {
                response.SetInvalidBecauseNotAdmin(nameof(User));
                return response;
            }

            if (await _organizationRepository.IsOrganizationActive(currentUser.OrganizationId))
            {
                response.SetInvalidBecauseNotActive(nameof(Organization));
                return response;
            }

            var entity = _cacheManager.GetCachedOrganization(currentUser.Organization.Uid);
            if (entity.Id != currentUser.OrganizationId)
            {
                response.SetInvalid();
                return response;
            }

            if (entity.Name == request.Name && entity.Description == request.Description)
            {
                response.Item = _organizationFactory.CreateDtoFromEntity(entity);
                response.Status = ResponseStatus.Success;
                return response;
            }

            if (await _organizationRepository.Any(x => x.Name == request.Name && x.Id != currentUser.OrganizationId))
            {
                response.SetInvalidBecauseMustBeUnique(nameof(Organization));
                return response;
            }

            var updatedEntity = _organizationFactory.CreateEntityFromRequest(request, entity);
            var result = await _organizationRepository.Update(request.CurrentUserId, updatedEntity);
            if (result)
            {
                _cacheManager.UpsertOrganizationCache(updatedEntity, _organizationFactory.MapCurrentOrganization(updatedEntity));

                response.Item = _organizationFactory.CreateDtoFromEntity(updatedEntity);
                response.Status = ResponseStatus.Success;
                return response;
            }

            response.SetFailed();
            return response;
        }

        public async Task<OrganizationRestoreResponse> RestoreOrganization(OrganizationRestoreRequest request)
        {
            var response = new OrganizationRestoreResponse();

            var currentUser = _cacheManager.GetCachedCurrentUser(request.CurrentUserId);
            if (await _organizationRepository.Any(x => x.Id == currentUser.OrganizationId && !x.IsActive))
            {
                response.SetInvalidBecauseNotActive(nameof(Organization));
                return response;
            }

            var organization = await _organizationRepository.Select(x => x.Uid == request.OrganizationUid);
            if (organization.IsNotExist())
            {
                response.SetFailedBecauseNotFound(nameof(Organization));
                return response;
            }

            var revisions = await _organizationRepository.SelectRevisions(organization.Id);
            if (revisions.All(x => x.Revision != request.Revision))
            {
                response.SetFailedBecauseRevisionNotFound(nameof(Organization));
                return response;
            }

            var result = await _organizationRepository.RestoreRevision(request.CurrentUserId, organization.Id, request.Revision);
            if (result)
            {
                response.Status = ResponseStatus.Success;
                return response;
            }

            response.SetFailed();
            return response;
        }

        public async Task<OrganizationPendingTranslationReadListResponse> GetPendingTranslations(OrganizationPendingTranslationReadListRequest request)
        {
            var response = new OrganizationPendingTranslationReadListResponse();

            var currentUser = _cacheManager.GetCachedCurrentUser(request.CurrentUserId);

            var organization = await _organizationRepository.Select(x => x.Uid == currentUser.OrganizationUid);
            if (organization.IsNotExist())
            {
                response.SetFailedBecauseNotFound(nameof(Organization));
                return response;
            }

            Expression<Func<Label, bool>> filter = x => x.OrganizationId == organization.Id
                                                        && x.LabelTranslationCount < 2;

            if (request.PagingInfo.SearchTerm.IsNotEmpty())
            {
                filter = x => x.Name.Contains(request.PagingInfo.SearchTerm)
                              && x.OrganizationId == organization.Id
                              && x.LabelTranslationCount < 2;
            }

            List<Label> entities;
            if (request.PagingInfo.Skip < 1)
            {
                entities = await _labelRepository.SelectAfter(filter, request.PagingInfo.LastUid, request.PagingInfo.Take, false,
                                                              new List<OrderByInfo<Label>>() { new OrderByInfo<Label>(x => x.Uid, request.PagingInfo.IsAscending) });
            }
            else
            {
                entities = await _labelRepository.SelectMany(filter, request.PagingInfo.Skip, request.PagingInfo.Take, false,
                                                             new List<OrderByInfo<Label>>() { new OrderByInfo<Label>(x => x.Id, request.PagingInfo.IsAscending) });
            }

            if (entities != null)
            {
                for (var i = 0; i < entities.Count; i++)
                {
                    var entity = entities[i];
                    var dto = _labelFactory.CreateDtoFromEntity(entity);
                    response.Items.Add(dto);
                }
            }

            response.PagingInfo.Skip = request.PagingInfo.Skip;
            response.PagingInfo.Take = request.PagingInfo.Take;
            response.PagingInfo.LastUid = request.PagingInfo.LastUid;
            response.PagingInfo.IsAscending = request.PagingInfo.IsAscending;
            response.PagingInfo.TotalItemCount = await _labelRepository.Count(filter);

            response.Status = ResponseStatus.Success;
            return response;

        }

        public async Task<ValidateEmailResponse> ValidateEmail(ValidateEmailRequest request)
        {
            var response = new ValidateEmailResponse();

            var user = await _userRepository.Select(x => x.EmailValidationToken == request.Token && x.Email == request.Email);


            if (await _organizationRepository.Any(x => x.Id == user.OrganizationId && !x.IsActive))
            {
                response.SetInvalidBecauseNotActive(nameof(Organization));
                return response;
            }

            if (user != null
                && user.Id > 0)
            {
                user.EmailValidationToken = Guid.Empty;
                user.EmailValidatedAt = DateTime.UtcNow;
                user.IsEmailValidated = true;

                var result = await _userRepository.Update(user.Id, user);
                if (result)
                {
                    response.Status = ResponseStatus.Success;
                    return response;
                }
            }

            response.SetFailed();
            return response;
        }

        public async Task<LogOnResponse> LogOn(LogOnRequest request)
        {
            var response = new LogOnResponse();

            var user = await _userRepository.Select(x => x.Email == request.Email);
            if (user.IsNotExist())
            {
                response.SetFailedBecauseNotFound(nameof(User));
                return response;
            }

            if (!user.IsActive)
            {
                response.SetInvalidBecauseNotActive(nameof(User));
                return response;
            }

            if (_cryptoHelper.Hash(request.Password, user.ObfuscationSalt) == user.PasswordHash)
            {
                if (user.LoginTryCount < 6
                    || user.LastLoginTryAt.HasValue && user.LastLoginTryAt.Value.AddHours(1) < DateTime.UtcNow)
                {
                    user.LastLoginAt = DateTime.UtcNow;
                    user.LoginTryCount = 0;

                    var loginLog = _userLoginLogFactory.CreateEntityFromRequest(request, user);
                    var uowResult = await _logOnUnitOfWork.DoWork(user, loginLog);
                    if (uowResult)
                    {
                        _cacheManager.UpsertUserCache(user, _userFactory.MapCurrentUser(user, _cacheManager.GetLanguageIsoCode2Char(user.LanguageId)));

                        response.Status = ResponseStatus.Success;
                        response.Item.OrganizationUid = user.OrganizationUid;
                        response.Item.Name = user.Name;
                        response.Item.Email = user.Email;

                        return response;
                    }
                }
            }

            user.LastLoginAt = null;
            user.LastLoginTryAt = DateTime.UtcNow;
            user.LoginTryCount++;

            await _userRepository.Update(user.Id, user);

            response.ErrorMessages.Add("password_invalid");
            response.Status = ResponseStatus.Failed;
            return response;
        }

        public async Task<DemandPasswordResetResponse> DemandPasswordReset(DemandPasswordResetRequest request)
        {
            var response = new DemandPasswordResetResponse();

            var user = await _userRepository.Select(x => x.Email == request.Email);
            if (!user.IsExist())
            {
                response.SetFailedBecauseNotFound(nameof(User));
                return response;
            }

            if (!user.IsActive)
            {
                response.SetInvalidBecauseNotActive(nameof(User));
                return response;
            }

            if (user.PasswordResetRequestedAt.HasValue
                && user.PasswordResetRequestedAt.Value.AddMinutes(2) > DateTime.UtcNow)
            {
                response.ErrorMessages.Add("already_requested_password_reset_in_last_two_minutes");
                response.Status = ResponseStatus.Invalid;
                return response;
            }

            user.PasswordResetRequestedAt = DateTime.UtcNow;
            user.PasswordResetToken = Guid.NewGuid();

            var result = await _userRepository.Update(user.Id, user);
            if (result)
            {
                //todo:send email

                response.Status = ResponseStatus.Success;
                return response;
            }

            response.SetFailed();
            return response;
        }

        public async Task<PasswordResetValidateResponse> ValidatePasswordReset(PasswordResetValidateRequest request)
        {
            var response = new PasswordResetValidateResponse();

            var user = await _userRepository.Select(x => x.PasswordResetToken == request.Token && x.Email == request.Email);
            if (user.IsExist()
                && user.PasswordResetRequestedAt.HasValue
                && user.PasswordResetRequestedAt.Value.AddDays(1) > DateTime.UtcNow)
            {
                response.Status = ResponseStatus.Success;
                return response;
            }

            response.SetFailed();
            return response;
        }

        public async Task<PasswordResetResponse> PasswordReset(PasswordResetRequest request)
        {
            var response = new PasswordResetResponse();

            var user = await _userRepository.Select(x => x.PasswordResetToken == request.Token && x.Email == request.Email);
            if (user.IsExist()
                && user.IsActive
                && user.PasswordResetRequestedAt.HasValue
                && user.PasswordResetRequestedAt.Value.AddDays(1) > DateTime.UtcNow)
            {
                user.PasswordHash = _cryptoHelper.Hash(request.Password, user.ObfuscationSalt);
                user.LoginTryCount = 0;
                user.PasswordResetRequestedAt = null;
                user.PasswordResetToken = null;

                var result = await _userRepository.Update(user.Id, user);
                if (result)
                {
                    //todo:send email

                    response.Status = ResponseStatus.Success;
                    return response;
                }
            }

            response.SetFailed();
            return response;
        }

        public async Task<PasswordChangeResponse> ChangePassword(PasswordChangeRequest request)
        {
            var response = new PasswordChangeResponse();

            var user = await _userRepository.Select(x => x.Id == request.CurrentUserId);
            if (user.IsNotExist())
            {
                response.SetFailedBecauseNotFound(nameof(User));
                return response;
            }

            if (!user.IsActive)
            {
                response.SetInvalidBecauseNotActive(nameof(User));
                return response;
            }

            if (await _organizationRepository.Any(x => x.Id == user.OrganizationId && !x.IsActive))
            {
                response.SetInvalidBecauseNotActive(nameof(Organization));

                return response;
            }


            if (user.PasswordHash != _cryptoHelper.Hash(request.OldPassword, user.ObfuscationSalt))
            {
                response.ErrorMessages.Add("old_password_is_not_right");
                response.Status = ResponseStatus.Failed;
                return response;
            }

            var passwordHash = _cryptoHelper.Hash(request.NewPassword, user.ObfuscationSalt);

            var revisions = await _userRepository.SelectRevisions(user.Id);
            var last2Password = revisions.ToList().Select(x => x.Entity.PasswordHash).Distinct().Take(2);
            if (last2Password.Contains(passwordHash))
            {
                response.ErrorMessages.Add("choose_other_password_different_then_last_2");
                response.Status = ResponseStatus.Failed;
                return response;
            }

            user.PasswordHash = passwordHash;
            user.LoginTryCount = 0;
            user.PasswordResetRequestedAt = null;
            user.PasswordResetToken = null;

            var result = await _userRepository.Update(user.Id, user);
            if (result)
            {
                //todo:send email

                response.Status = ResponseStatus.Success;
                return response;
            }

            response.SetFailed();
            return response;
        }

        public async Task<UserChangeActivationResponse> ChangeActivationForUser(UserChangeActivationRequest request)
        {
            var response = new UserChangeActivationResponse();

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

            var entity = _cacheManager.GetCachedUser(request.UserUid);
            if (entity.OrganizationId != currentUser.OrganizationId)
            {
                response.SetInvalid();
                return response;
            }

            entity.IsActive = !entity.IsActive;
            entity.UpdatedBy = request.CurrentUserId;

            var result = await _userRepository.Update(request.CurrentUserId, entity);
            if (result)
            {
                _cacheManager.UpsertUserCache(entity, currentUser);

                response.Status = ResponseStatus.Success;
                return response;
            }

            response.SetFailed();
            return response;
        }

        public async Task<UserEditResponse> EditUser(UserEditRequest request)
        {
            var response = new UserEditResponse();

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

            var entity = _cacheManager.GetCachedUser(request.UserUid);
            if (entity.OrganizationId != currentUser.OrganizationId)
            {
                response.SetInvalid();
                return response;
            }

            if (entity.FirstName == request.FirstName && entity.LastName == request.LastName
                                                  && entity.LanguageUid == request.LanguageUid)
            {
                response.Item = _userFactory.CreateDtoFromEntity(entity);
                response.Status = ResponseStatus.Success;
                return response;
            }

            var language = await _languageRepository.Select(x => x.Uid == request.LanguageUid);
            if (language.IsNotExist())
            {
                response.SetFailedBecauseNotFound(nameof(Language));
                return response;
            }

            var updatedEntity = _userFactory.CreateEntityFromRequest(request, entity, language);
            var result = await _userRepository.Update(request.CurrentUserId, updatedEntity);
            if (result)
            {
                _cacheManager.UpsertUserCache(entity, _userFactory.MapCurrentUser(entity, _cacheManager.GetLanguageIsoCode2Char(entity.LanguageId)));

                response.Item = _userFactory.CreateDtoFromEntity(entity);
                response.Status = ResponseStatus.Success;
                return response;
            }

            response.SetFailed();
            return response;
        }

        public async Task<UserDeleteResponse> DeleteUser(UserDeleteRequest request)
        {
            var response = new UserDeleteResponse();

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

            var entity = _cacheManager.GetCachedUser(request.UserUid);
            if (entity.OrganizationId != currentUser.OrganizationId)
            {
                response.SetInvalid();
                return response;
            }

            var result = await _userRepository.Delete(request.CurrentUserId, entity.Id);
            if (result)
            {
                _cacheManager.RemoveUser(entity);

                response.Status = ResponseStatus.Success;
                return response;
            }

            response.SetFailed();
            return response;
        }

        public async Task<UserInviteResponse> InviteUser(UserInviteRequest request)
        {
            var response = new UserInviteResponse();

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

            var user = await _userRepository.Select(x => x.Email == request.Email);
            if (user.IsExist())
            {
                response.ErrorMessages.Add("email_already_invited");
                response.Status = ResponseStatus.Invalid;
                return response;
            }

            var invitedUser = _userFactory.CreateEntityFromRequest(request, currentUser.Organization, _cryptoHelper.GetSaltAsString());
            invitedUser.InvitationToken = Guid.NewGuid();
            invitedUser.InvitedAt = DateTime.UtcNow;
            invitedUser.InvitedByUserId = currentUser.Id;
            invitedUser.InvitedByUserUid = currentUser.Uid;
            invitedUser.InvitedByUserName = currentUser.Name;

            var id = await _userRepository.Insert(request.CurrentUserId, invitedUser);
            if (id > 0)
            {
                //todo:send invite email

                response.Status = ResponseStatus.Success;
                return response;
            }

            response.SetFailed();
            return response;
        }

        public async Task<UserInviteValidateResponse> ValidateUserInvitation(UserInviteValidateRequest request)
        {
            var response = new UserInviteValidateResponse();

            var user = await _userRepository.Select(x => x.InvitationToken == request.Token && x.Email == request.Email);
            if (user.IsExist()
                && user.InvitedAt.HasValue
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

        public async Task<UserAcceptInviteResponse> AcceptInvitation(UserAcceptInviteRequest request)
        {
            var response = new UserAcceptInviteResponse();

            var user = await _userRepository.Select(x => x.InvitationToken == request.Token && x.Email == request.Email);
            if (user.IsExist()
                && user.InvitedAt.HasValue
                && user.InvitedAt.Value.AddDays(2) > DateTime.UtcNow)
            {
                user.FirstName = request.FirstName;
                user.LastName = request.LastName;
                user.InvitationAcceptedAt = DateTime.UtcNow;
                user.PasswordHash = _cryptoHelper.Hash(request.Password, user.ObfuscationSalt);
                user.LanguageUid = request.LanguageUid;
                user.LanguageName = request.LanguageName;
                var language = await _languageRepository.Select(x => x.Uid == request.LanguageUid);
                user.LanguageId = language.Id;
                user.LanguageIconUrl = language.IconUrl;

                //todo:send welcome email

                //todo uow

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

        public UserReadResponse GetUser(UserReadRequest request)
        {
            var response = new UserReadResponse();

            var entity = _cacheManager.GetCachedUser(request.UserUid);
            var currentUser = _cacheManager.GetCachedCurrentUser(request.CurrentUserId);

            if (entity.OrganizationId != currentUser.OrganizationId)
            {
                response.SetInvalid();
                return response;
            }

            response.Item = _userFactory.CreateDtoFromEntity(entity);
            response.Status = ResponseStatus.Success;
            return response;
        }

        public async Task<UserReadListResponse> GetUsers(UserReadListRequest request)
        {
            var response = new UserReadListResponse();

            var currentUser = _cacheManager.GetCachedCurrentUser(request.CurrentUserId);

            Expression<Func<User, bool>> filter = x => x.OrganizationId == currentUser.OrganizationId;

            if (request.PagingInfo.SearchTerm.IsNotEmpty())
            {
                filter = x => x.OrganizationId == currentUser.OrganizationId && x.Name.Contains(request.PagingInfo.SearchTerm);
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

        public async Task<UserRevisionReadListResponse> GetUserRevisions(UserRevisionReadListRequest request)
        {
            var response = new UserRevisionReadListResponse();

            var user = await _userRepository.Select(x => x.Uid == request.UserUid);
            if (user.IsNotExist())
            {
                response.SetFailedBecauseNotFound(nameof(User));
                return response;
            }

            var revisions = await _userRepository.SelectRevisions(user.Id);

            for (var i = 0; i < revisions.Count; i++)
            {
                var revision = revisions[i];

                var revisionDto = new RevisionDto<UserDto>();
                revisionDto.Revision = revision.Revision;
                revisionDto.RevisionedAt = revision.RevisionedAt;

                var revisionedByUser = _cacheManager.GetCachedUser(revision.RevisionedBy);
                revisionDto.RevisionedByUid = revisionedByUser.Uid;
                revisionDto.RevisionedByName = revisionedByUser.Name;

                revisionDto.Item = _userFactory.CreateDtoFromEntity(revision.Entity);

                response.Items.Add(revisionDto);
            }

            response.Status = ResponseStatus.Success;
            return response;
        }

        public async Task<UserLoginLogReadListResponse> GetUserLoginLogs(UserLoginLogReadListRequest request)
        {
            var response = new UserLoginLogReadListResponse();

            var user = _cacheManager.GetCachedUser(request.UserUid);
            var currentUser = _cacheManager.GetCachedCurrentUser(request.CurrentUserId);

            if (!currentUser.IsAdmin)
            {
                response.SetInvalidBecauseNotAdmin(nameof(CurrentUser));
                return response;
            }

            if (user.OrganizationId != currentUser.OrganizationId)
            {
                response.SetInvalid();
                return response;
            }

            Expression<Func<UserLoginLog, bool>> filter = x => x.OrganizationId == user.OrganizationId && x.UserId == user.Id;

            if (request.PagingInfo.SearchTerm.IsNotEmpty())
            {
                filter = x => x.Name.Contains(request.PagingInfo.SearchTerm) && x.OrganizationId == user.OrganizationId && x.UserId == user.Id;
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

        public async Task<OrganizationLoginLogReadListResponse> GetUserLoginLogsOfOrganization(OrganizationLoginLogReadListRequest request)
        {
            var response = new OrganizationLoginLogReadListResponse();

            var currentUser = _cacheManager.GetCachedCurrentUser(request.CurrentUserId);
            var organization = _cacheManager.GetCachedOrganization(request.OrganizationUid);

            if (currentUser.OrganizationId != organization.Id)
            {
                response.SetInvalid();
                return response;
            }

            Expression<Func<UserLoginLog, bool>> filter = x => x.OrganizationId == currentUser.OrganizationId;

            if (request.PagingInfo.SearchTerm.IsNotEmpty())
            {
                filter = x => x.Name.Contains(request.PagingInfo.SearchTerm) && x.OrganizationId == currentUser.OrganizationId;
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

        public async Task<UserRestoreResponse> RestoreUser(UserRestoreRequest request)
        {
            var response = new UserRestoreResponse();

            var currentUser = _cacheManager.GetCachedCurrentUser(request.CurrentUserId);
            if (await _organizationRepository.Any(x => x.Id == currentUser.OrganizationId && !x.IsActive))
            {
                response.SetInvalidBecauseNotActive(nameof(Organization));
                return response;
            }

            var user = await _userRepository.Select(x => x.Uid == request.UserUid);
            if (user.IsNotExist())
            {
                response.SetFailedBecauseNotFound(nameof(User));
                return response;
            }

            var revisions = await _userRepository.SelectRevisions(user.Id);
            if (revisions.All(x => x.Revision != request.Revision))
            {
                response.SetFailedBecauseRevisionNotFound(nameof(User));
                return response;
            }

            var result = await _userRepository.RestoreRevision(request.CurrentUserId, user.Id, request.Revision);
            if (result)
            {
                response.Status = ResponseStatus.Success;
                return response;
            }

            response.SetFailed();
            return response;
        }

        public CurrentUser GetCurrentUser(CurrentUserRequest request)
        {
            var currentUser = _cacheManager.GetCachedCurrentUser(request.Email);
            if (currentUser == null)
            {
                throw new ApplicationException("Current user not mapped!");
            }

            return currentUser;
        }

        public async Task<bool> LoadOrganizationsToCache()
        {
            var organizations = await _organizationRepository.SelectAll(x => x.IsActive, false,
                                                                       new List<OrderByInfo<Organization>>() { new OrderByInfo<Organization>(x => x.Id) });

            for (var i = 0; i < organizations.Count; i++)
            {
                var organization = organizations[i];
                _cacheManager.UpsertOrganizationCache(organization, _organizationFactory.MapCurrentOrganization(organization));
            }

            return true;
        }

        public async Task<bool> LoadUsersToCache()
        {
            var users = await _userRepository.SelectAll(x => x.IsActive, false,
                                                       new List<OrderByInfo<User>>() { new OrderByInfo<User>(x => x.Id) });

            for (var i = 0; i < users.Count; i++)
            {
                var user = users[i];
                _cacheManager.UpsertUserCache(user, _userFactory.MapCurrentUser(user, _cacheManager.GetLanguageIsoCode2Char(user.LanguageId)));
            }

            return true;
        }
    }
}
