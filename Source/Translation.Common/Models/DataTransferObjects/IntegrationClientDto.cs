using System;

using StandardUtils.Models.DataTransferObjects;

namespace Translation.Common.Models.DataTransferObjects
{
    public class IntegrationClientDto : BaseDto
    {
        public Guid OrganizationUid { get; set; }
        public string OrganizationName { get; set; }

        public Guid IntegrationUid { get; set; }
        public string IntegrationName { get; set; }
        public Guid ClientId { get; set; }
        public Guid ClientSecret { get; set; }
        public bool IsActive { get; set; }
    }
}