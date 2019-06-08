using System;

using StandardRepository.Models.Entities;
using StandardRepository.Models.Entities.Schemas;

namespace Translation.Data.Entities.Domain
{
    public class LabelTranslation : BaseEntity, ISchemaDomain
    {
        public long OrganizationId { get; set; }
        public Guid OrganizationUid { get; set; }
        public string OrganizationName { get; set; }

        public long ProjectId { get; set; }
        public Guid ProjectUid { get; set; }
        public string ProjectName { get; set; }

        public long LabelId { get; set; }
        public Guid LabelUid { get; set; }
        public string LabelName { get; set; }

        public long LanguageId { get; set; }
        public Guid LanguageUid { get; set; }
        public string LanguageName { get; set; }

        public string Translation { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
    }
}