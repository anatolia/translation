using System;
using System.Collections.Generic;

using Cheviri.Data.Entities.Base;
using Cheviri.Data.Entities.Base.Schemas;
using Cheviri.Data.Entities.Parameter;
using Cheviri.Data.Entities.Project;

namespace Cheviri.Data.Entities.Main
{
    public class User : BaseEntity, ISchemaMain
    {
        public string Email { get; set; }
        public string EmailValidationToken { get; set; }
        public DateTime EmailValidatedAt { get; set; }
        public bool IsEmailValidated { get; set; }

        public string PasswordHash { get; set; }
        public string PasswordResetToken { get; set; }
        public DateTime? PasswordResetRequestedAt { get; set; }

        public DateTime? LastLoginAt { get; set; }
        public int LoginTryCount { get; set; }

        public string Name { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Description { get; set; }
        
        public bool IsActive { get; set; }
        public bool IsAdmin { get; set; }

        public int TranslatedLabelCount { get; set; }
        public int AddedLabelCount { get; set; }

        public Organization Organization { get; set; }
        public List<Role> Roles { get; set; }

        public User()
        {
            Roles = new List<Role>();
        }
    }
}