using System;
using StandardRepository.Models.Entities;
using StandardRepository.Models.Entities.Schemas;

namespace Translation.Data.Entities.Main
{
    public class TokenRequestLog : BaseEntity, ISchemaMain
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

        public long TokenId { get; set; }
        public Guid TokenUid { get; set; }
        public string TokenName { get; set; }

        public string Ip { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string HttpMethod { get; set; }
        public string Request { get; set; }
        public string Response { get; set; }
        public string ResponseCode { get; set; }
    }
}