using System;

using StandardUtils.Models.DataTransferObjects;

namespace Translation.Common.Models.DataTransferObjects
{
    public class TokenRequestLogDto : BaseDto
    {
        public Guid OrganizationUid { get; set; }
        public string OrganizationName { get; set; }

        public Guid IntegrationUid { get; set; }
        public string IntegrationName { get; set; }

        public Guid IntegrationClientUid { get; set; }
        public string IntegrationClientName { get; set; }

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