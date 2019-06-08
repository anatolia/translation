using System;
using System.Collections.Generic;
using NodaTime;
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
        public Instant? EmailValidatedAt { get; set; }
        public bool IsEmailValidated { get; set; }

        public string PasswordHash { get; set; }
        public string ObfuscationSalt { get; set; }
        public Guid? PasswordResetToken { get; set; }
        public Instant? PasswordResetRequestedAt { get; set; }

        public Instant? LastLoginAt { get; set; }
        public Instant? LastLoginTryAt { get; set; }
        public int LoginTryCount { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Description { get; set; }

        public bool IsActive { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsSuperAdmin { get; set; }

        public Instant? InvitedAt { get; set; }
        public long? InvitedByUserId { get; set; }
        public Guid? InvitedByUserUid { get; set; }
        public string InvitedByUserName { get; set; }
        public Guid? InvitationToken { get; set; }

        public int LabelCount { get; set; }
        public int LabelTranslationCount { get; set; }
    }
}