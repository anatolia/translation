using System;
using StandardRepository.Models.Entities;
using StandardRepository.Models.Entities.Schemas;

namespace Translation.Data.Entities.Main
{
    public class IntegrationClient : BaseEntity, ISchemaMain
    {
        public long OrganizationId { get; set; }
        public Guid OrganizationUid { get; set; }
        public string OrganizationName { get; set; }

        public long IntegrationId { get; set; }
        public Guid IntegrationUid { get; set; }
        public string IntegrationName { get; set; }

        public Guid ClientId { get; set; }
        public Guid ClientSecret { get; set; }
        public bool IsActive { get; set; }
        public string Description { get; set; }
    }
}