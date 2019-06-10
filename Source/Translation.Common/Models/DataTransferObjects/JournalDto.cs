using System;

using Translation.Common.Models.Base;

namespace Translation.Common.Models.DataTransferObjects
{
    public class JournalDto : BaseDto
    {
        public Guid OrganizationUid { get; set; }
        public string OrganizationName { get; set; }

        public Guid IntegrationUid { get; set; }
        public string IntegrationName { get; set; }

        public Guid UserUid { get; set; }
        public string UserName { get; set; }

        public string Message { get; set; }
    }
}