using System;

using StandardRepository.Models.Entities;
using StandardRepository.Models.Entities.Schemas;

namespace Translation.Data.Entities.Main
{
    public class User : BaseEntity, ISchemaMain
    {
        public long OrganizationId { get; set; }
        public Guid OrganizationUid { get; set; }
        public string OrganizationName { get; set; }

        public string Email { get; set; }
        public Guid EmailValidationToken { get; set; }
        public DateTime? EmailValidatedAt { get; set; }
        public bool IsEmailValidated { get; set; }

        public string PasswordHash { get; set; }
        public string ObfuscationSalt { get; set; }
        public Guid? PasswordResetToken { get; set; }
        public DateTime? PasswordResetRequestedAt { get; set; }

        public DateTime? LastLoginAt { get; set; }
        public DateTime? LastLoginTryAt { get; set; }
        public int LoginTryCount { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Description { get; set; }

        public bool IsActive { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsSuperAdmin { get; set; }

        public DateTime? InvitedAt { get; set; }
        public DateTime? InvitationAcceptedAt { get; set; }
        public long? InvitedByUserId { get; set; }
        public Guid? InvitedByUserUid { get; set; }
        public string InvitedByUserName { get; set; }
        public Guid? InvitationToken { get; set; }

        public int LabelCount { get; set; }
        public int LabelTranslationCount { get; set; }

        public long LanguageId { get; set; }
        public Guid LanguageUid { get; set; }
        public string LanguageName { get; set; }
        public string LanguageIconUrl { get; set; }
    }
}