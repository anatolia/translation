using System;

using StandardRepository.Models.Entities;
using StandardRepository.Models.Entities.Schemas;

namespace Translation.Data.Entities.Domain
{
    public class Label : BaseEntity, ISchemaDomain
    {
        public long OrganizationId { get; set; }
        public Guid OrganizationUid { get; set; }
        public string OrganizationName { get; set; }

        public long ProjectId { get; set; }
        public Guid ProjectUid { get; set; }
        public string ProjectName { get; set; }

        public string LabelKey { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }

        public int LabelTranslationCount { get; set; }
    }
}