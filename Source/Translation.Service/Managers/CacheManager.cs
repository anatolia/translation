using System;
using System.Threading.Tasks;
using StandardCache;
using StandardRepository.Helpers;
using Translation.Common.Helpers;
using Translation.Common.Models.Shared;
using Translation.Data.Entities.Domain;
using Translation.Data.Entities.Main;
using Translation.Data.Entities.Parameter;
using Translation.Data.Factories;
using Translation.Data.Repositories.Contracts;

namespace Translation.Service.Managers
{
    public class CacheManager : StandardCache.StandardCache
    {
        private const string CACHE_NAME_USER = nameof(User);
        private const string CACHE_NAME_CURRENT_USER = nameof(CurrentUser);
        private const string CACHE_NAME_ORGANIZATION = nameof(Organization);
        private const string CACHE_NAME_CURRENT_ORGANIZATION = nameof(CurrentOrganization);
        private const string CACHE_NAME_ACTIVE_PROVIDER = nameof(TranslationProvider);
        private const string CACHE_NAME_CURRENT_USER_LANGUAGE_ISO_CODE2_CHAR = nameof(CurrentUser.LanguageIsoCode2Char);

        private readonly ILanguageRepository _languageRepository;
        private readonly IUserRepository _userRepository;
        private readonly UserFactory _userFactory;
        private readonly IOrganizationRepository _organizationRepository;
        private readonly OrganizationFactory _organizationFactory;
        private readonly TranslationProviderFactory _translationProviderFactory;
        private readonly ITranslationProviderRepository _translationProviderRepository;

        public CacheManager(ILanguageRepository languageRepository,
                            IUserRepository userRepository,
                            UserFactory userFactory,
                            IOrganizationRepository organizationRepository,
                            OrganizationFactory organizationFactory,
                            TranslationProviderFactory translationProviderFactory,
                            ITranslationProviderRepository translationProviderRepository)
        {
            _languageRepository = languageRepository;
            _userRepository = userRepository;
            _userFactory = userFactory;
            _organizationRepository = organizationRepository;
            _organizationFactory = organizationFactory;
            _translationProviderFactory = translationProviderFactory;
            _translationProviderRepository = translationProviderRepository;
        }

        public User GetCachedUser(long userId)
        {
            var item = Get(CACHE_NAME_USER, userId.ToString());
            if (item == null)
            {
                var user = _userRepository.SelectById(userId).Result;
                if (user.IsNotExist())
                {
                    return null;
                }

                UpsertUserCache(user, _userFactory.MapCurrentUser(user, GetLanguageIsoCode2Char(user.LanguageId)));

                return user;
            }

            return (User)item.Item;
        }

        public User GetCachedUser(Guid userUid)
        {
            var item = Get(CACHE_NAME_USER, userUid.ToUidString());
            if (item == null)
            {
                var user = _userRepository.Select(x => x.Uid == userUid).Result;
                if (user.IsNotExist())
                {
                    return null;
                }

                UpsertUserCache(user, _userFactory.MapCurrentUser(user, GetLanguageIsoCode2Char(user.LanguageId)));

                return user;
            }

            return (User)item.Item;
        }

        public User GetCachedUser(string email)
        {
            var item = Get(CACHE_NAME_USER, email);
            if (item == null)
            {
                var user = _userRepository.Select(x => x.Email == email).Result;
                if (user.IsNotExist())
                {
                    return null;
                }

                UpsertUserCache(user, _userFactory.MapCurrentUser(user, GetLanguageIsoCode2Char(user.LanguageId)));

                return user;
            }

            return (User)item.Item;
        }
        
        public CurrentUser GetCachedCurrentUser(long currentUserId)
        {
            var item = Get(CACHE_NAME_CURRENT_USER, currentUserId.ToString());
            if (item == null)
            {
                var user = _userRepository.SelectById(currentUserId).Result;
                if (user.IsNotExist())
                {
                    return null;
                }

                var currentUser = _userFactory.MapCurrentUser(user, GetLanguageIsoCode2Char(user.LanguageId));
                UpsertUserCache(user, currentUser);

                return currentUser;
            }

            return (CurrentUser)item.Item;
        }

        public CurrentUser GetCachedCurrentUser(string email)
        {
            var item = Get(CACHE_NAME_CURRENT_USER, email);
            if (item == null)
            {
                var user = _userRepository.Select(x => x.Email == email).Result;
                if (user.IsNotExist())
                {
                    return null;
                }
                
                var currentUser = _userFactory.MapCurrentUser(user, GetLanguageIsoCode2Char(user.LanguageId));
                UpsertUserCache(user, currentUser);

                return currentUser;
            }

            return (CurrentUser)item.Item;
        }

        public void UpsertUserCache(User user, CurrentUser currentUser)
        {
            AddOrUpdate(CACHE_NAME_USER, new CacheItem(user.Uid.ToUidString(), user));
            AddOrUpdate(CACHE_NAME_USER, new CacheItem(user.Id.ToString(), user));
            AddOrUpdate(CACHE_NAME_USER, new CacheItem(user.Email, user));
            AddOrUpdate(CACHE_NAME_CURRENT_USER, new CacheItem(user.Id.ToString(), currentUser));
        }

        public Organization GetCachedOrganization(long organizationId)
        {
            var item = Get(CACHE_NAME_ORGANIZATION, organizationId.ToString());
            if (item == null)
            {
                var organization = _organizationRepository.SelectById(organizationId).Result;
                if (organization.IsNotExist())
                {
                    return null;
                }

                UpsertOrganizationCache(organization, _organizationFactory.MapCurrentOrganization(organization));

                return organization;
            }

            return (Organization)item.Item;
        }

        public Organization GetCachedOrganization(Guid organizationUid)
        {
            var item = Get(CACHE_NAME_ORGANIZATION, organizationUid.ToUidString());
            if (item == null)
            {
                var organization = _organizationRepository.Select(x => x.Uid == organizationUid).Result;
                if (organization.IsNotExist())
                {
                    return null;
                }

                UpsertOrganizationCache(organization, _organizationFactory.MapCurrentOrganization(organization));

                return organization;
            }

            return (Organization)item.Item;
        }

        public CurrentOrganization GetCachedCurrentOrganization(Guid organizationUid)
        {
            var item = Get(CACHE_NAME_CURRENT_ORGANIZATION, organizationUid.ToUidString());
            if (item == null)
            {
                var organization = _organizationRepository.Select(x => x.Uid == organizationUid).Result;
                if (organization.IsNotExist())
                {
                    return null;
                }

                var currentOrganization = _organizationFactory.MapCurrentOrganization(organization);
                UpsertOrganizationCache(organization, currentOrganization);

                return currentOrganization;
            }

            return (CurrentOrganization)item.Item;
        }

        public void UpsertOrganizationCache(Organization organization, CurrentOrganization currentOrganization)
        {
            AddOrUpdate(CACHE_NAME_ORGANIZATION, new CacheItem(organization.Uid.ToUidString(), organization));
            AddOrUpdate(CACHE_NAME_CURRENT_ORGANIZATION, new CacheItem(organization.Uid.ToUidString(), currentOrganization));
        }

        public void UpsertActiveTranslationProviderCache(TranslationProvider activeTranslationProvide)
        {
            AddOrUpdate(CACHE_NAME_ACTIVE_PROVIDER, new CacheItem(activeTranslationProvide.IsActive.ToString(), activeTranslationProvide));
        }

        public ActiveTranslationProvider GetCachedActiveTranslationProvider(bool isActive)
        {
            var item = Get(CACHE_NAME_ACTIVE_PROVIDER, isActive.ToString());
            if (item == null)
            {
                var translationProvider = _translationProviderRepository.Select(x => x.IsActive == isActive).Result;
                if (translationProvider.IsNotExist())
                {
                    return null;
                }

                var activeTranslationProvider = _translationProviderFactory.MapActiveTranslationProvider(translationProvider);
                UpsertActiveTranslationProviderCache(translationProvider);

                return activeTranslationProvider;
            }

            return (ActiveTranslationProvider)item.Item;
        }
       
        public void RemoveUser(User user)
        {
            Remove(CACHE_NAME_USER, user.Id.ToString());
            Remove(CACHE_NAME_USER, user.Uid.ToUidString());
            Remove(CACHE_NAME_CURRENT_USER, user.Id.ToString());
            Remove(CACHE_NAME_CURRENT_USER, user.Uid.ToUidString());
        }

        public string GetLanguageIsoCode2Char(long languageId)
        {
            var item = Get(CACHE_NAME_CURRENT_USER_LANGUAGE_ISO_CODE2_CHAR, languageId.ToString());
            if (item == null)
            {
                if (languageId < 1)
                {
                    return "en";
                }

                var language = _languageRepository.SelectById(languageId).Result;
                if (language.IsNotExist())
                {
                    return null;
                }

                AddOrUpdate(CACHE_NAME_CURRENT_USER_LANGUAGE_ISO_CODE2_CHAR, new CacheItem(languageId.ToString(), language.IsoCode2Char));

                return language.IsoCode2Char;
            }

            return (string)item.Item;
        }
    }
}