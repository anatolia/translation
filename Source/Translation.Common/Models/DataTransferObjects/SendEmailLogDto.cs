using System;

using Translation.Common.Models.Base;

namespace Translation.Common.Models.DataTransferObjects
{
    public class SendEmailLogDto : BaseDto
    {
        public Guid OrganizationUid { get; set; }
        public string OrganizationName { get; set; }

        public Guid MailUid { get; set; }

        public string Subject { get; set; }
        public string EmailFrom { get; set; }
        public string EmailTo { get; set; }

        public bool IsOpened { get; set; }
    }
}