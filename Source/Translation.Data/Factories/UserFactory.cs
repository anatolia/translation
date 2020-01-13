using System;

using Translation.Common.Models.DataTransferObjects;
using Translation.Common.Models.Requests.Admin;
using Translation.Common.Models.Requests.Organization;
using Translation.Common.Models.Requests.User;
using Translation.Common.Models.Shared;
using Translation.Data.Entities.Main;
using Translation.Data.Entities.Parameter;

namespace Translation.Data.Factories
{
    public class UserFactory
    {
        public User CreateEntityFromRequest(AdminDemoteRequest request, User entity)
        {
            entity.UpdatedBy = request.CurrentUserId;
            entity.IsAdmin = false;
            entity.IsSuperAdmin = false;

            return entity;
        }

        public User CreateEntityFromRequest(AdminUpgradeRequest request, User entity)
        {
            entity.UpdatedBy = request.CurrentUserId;
            entity.IsAdmin = true;

            return entity;
        }

        public User CreateEntityFromRequest(SignUpRequest request, Organization organization, string salt, string passwordHash)
        {
            var entity = new User();

            entity.Email = request.Email;
            entity.FirstName = request.FirstName;
            entity.LastName = request.LastName;
            entity.Name = MapName(request.FirstName, request.LastName);

            entity.IsActive = true;
            entity.IsAdmin = true;
            entity.ObfuscationSalt = salt;
            entity.PasswordHash = passwordHash;

            entity.OrganizationUid = organization.Uid;
            entity.OrganizationName = request.OrganizationName;

            return entity;
        }

        public User CreateEntityFromRequest(UserEditRequest request, User entity, Language language)
        {
            entity.UpdatedBy = request.CurrentUserId;

            entity.FirstName = request.FirstName;
            entity.LastName = request.LastName;
            entity.Name = MapName(request.FirstName, request.LastName);

            entity.LanguageId = language.Id;
            entity.LanguageUid = language.Uid;
            entity.LanguageName = language.Name;
            entity.LanguageIconUrl = language.IconUrl;

            return entity;
        }

        public User CreateEntityFromRequest(UserInviteRequest request, Organization organization, string salt)
        {
            var entity = new User();

            entity.Email = request.Email;
            entity.FirstName = request.FirstName;
            entity.LastName = request.LastName;
            entity.Name = MapName(request.FirstName, request.LastName);
            entity.ObfuscationSalt = salt;
            entity.IsActive = true;

            entity.OrganizationId = organization.Id;
            entity.OrganizationUid = organization.Uid;
            entity.OrganizationName = organization.Name;

            return entity;
        }

        public User CreateEntityFromRequest(UserInviteRequest request, CurrentOrganization organization, string salt)
        {
            var entity = new User();
            entity.FirstName = request.FirstName;
            entity.LastName = request.LastName;
            entity.Name = MapName(request.FirstName, request.LastName);
            entity.Email = request.Email;
            entity.ObfuscationSalt = salt;
            entity.IsActive = true;

            entity.OrganizationId = organization.Id;
            entity.OrganizationUid = organization.Uid;
            entity.OrganizationName = organization.Name;

            return entity;
        }

        public User CreateEntityFromRequest(AdminInviteRequest request, CurrentUser currentUser, string salt)
        {
            var entity = new User();

            entity.IsAdmin = true;
            entity.Email = request.Email;
            entity.FirstName = request.FirstName;
            entity.LastName = request.LastName;
            entity.Name = MapName(request.FirstName, request.LastName);
            entity.ObfuscationSalt = salt;
            entity.IsActive = true;

            entity.OrganizationId = currentUser.Organization.Id;
            entity.OrganizationUid = currentUser.Organization.Uid;
            entity.OrganizationName = currentUser.Organization.Name;

            entity.InvitedByUserId = currentUser.Id;
            entity.InvitedByUserUid = currentUser.Uid;
            entity.InvitedByUserName = currentUser.Name;
            entity.InvitedAt = DateTime.UtcNow;
            entity.InvitationToken = Guid.NewGuid();

            return entity;
        }

        public UserDto CreateDtoFromEntity(User entity)
        {
            var dto = new UserDto();
            dto.Uid = entity.Uid;
            dto.Name = entity.Name;
            dto.FirstName = entity.FirstName;
            dto.LastName = entity.LastName;
            dto.Description = entity.Description;
            dto.Email = entity.Email;
            dto.IsActive = entity.IsActive;
            dto.IsAdmin = entity.IsAdmin;
            dto.CreatedAt = entity.CreatedAt;
            dto.LastLoggedInAt = entity.LastLoginAt;

            dto.LabelCount = entity.LabelCount;
            dto.LabelTranslationCount = entity.LabelTranslationCount;
            dto.InvitedAt = entity.InvitedAt;
            dto.InvitationAcceptedAt = entity.InvitationAcceptedAt;
            dto.InvitedByUserUid = entity.InvitedByUserUid;
            dto.InvitedByUserName = entity.InvitedByUserName;

            dto.OrganizationUid = entity.OrganizationUid;
            dto.OrganizationName = entity.OrganizationName;

            dto.LanguageUid = entity.LanguageUid;
            dto.LanguageName = entity.LanguageName;
            dto.LanguageIconUrl = entity.LanguageIconUrl;

            return dto;
        }

        public string MapName(string firstName, string lastName)
        {
            return firstName + " " + lastName;
        }

        public CurrentUser MapCurrentUser(User user, string isoCode2Char)
        {
            var currentUser = new CurrentUser();
            currentUser.Id = user.Id;
            currentUser.Uid = user.Uid;
            currentUser.Name = user.Name;
            currentUser.Email = user.Email;
            currentUser.IsAdmin = user.IsAdmin;
            currentUser.IsSuperAdmin = user.IsSuperAdmin;
            currentUser.IsActive = user.IsActive;

            var currentOrganization = new CurrentOrganization
            {
                Id = user.OrganizationId,
                Uid = user.OrganizationUid,
                Name = user.OrganizationName
            };
            currentUser.Organization = currentOrganization;

            currentUser.LanguageCode = isoCode2Char;

            return currentUser;
        }
    }
}