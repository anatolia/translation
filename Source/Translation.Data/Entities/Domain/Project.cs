using System;

using StandardRepository.Models.Entities;
using StandardRepository.Models.Entities.Schemas;

namespace Translation.Data.Entities.Domain
{
    public class Project : BaseEntity, ISchemaDomain
    {
        public long OrganizationId { get; set; }
        public Guid OrganizationUid { get; set; }
        public string OrganizationName { get; set; }

        public string Description { get; set; }
        public string Url { get; set; }
        public bool IsActive { get; set; }
        public int LabelCount { get; set; }
        public int LabelTranslationCount { get; set; }
        public bool IsSuperProject { get; set; }

        public string Slug { get; set; }

        public long LanguageId { get; set; }
        public Guid LanguageUid { get; set; }
        public string LanguageName { get; set; }
        public string LanguageIconUrl { get; set; }
    }
}