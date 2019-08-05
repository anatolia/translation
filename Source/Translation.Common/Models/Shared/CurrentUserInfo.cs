using System;

namespace Translation.Common.Models.Shared
{
    public class CurrentUser
    {
        public CurrentOrganization Organization { get; set; }
        public long OrganizationId => Organization.Id;
        public Guid OrganizationUid => Organization.Uid;

        public long Id { get; set; }
        public Guid Uid { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string ObfuscationSalt { get; set; }

        public bool IsAdmin { get; set; }
        public bool IsSuperAdmin { get; set; }
        public bool IsActive { get; set; }

        public bool IsActionSucceed { get; set; }

        public string LanguageIsoCode2Char { get; set; }
    }
}