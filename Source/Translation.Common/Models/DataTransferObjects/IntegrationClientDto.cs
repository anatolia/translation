using System;
using Translation.Common.Models.Base;

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