using System;

using StandardRepository.Models.Entities;
using StandardRepository.Models.Entities.Schemas;

namespace Translation.Data.Entities.Main
{
    public class Token : BaseEntity, ISchemaMain
    {
        public long OrganizationId { get; set; }
        public Guid OrganizationUid { get; set; }
        public string OrganizationName { get; set; }

        public long IntegrationId { get; set; }
        public Guid IntegrationUid { get; set; }
        public string IntegrationName { get; set; }

        public long IntegrationClientId { get; set; }
        public Guid IntegrationClientUid { get; set; }
        public string IntegrationClientName { get; set; }

        public Guid AccessToken { get; set; }
        public DateTime ExpiresAt { get; set; }
        public string Ip { get; set; }
        public bool IsActive { get; set; }
    }
}