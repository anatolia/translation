using System;

using StandardUtils.Models.DataTransferObjects;

namespace Translation.Common.Models.DataTransferObjects
{
    public class UserLoginLogDto : BaseDto
    {
        public Guid OrganizationUid { get; set; }
        public string OrganizationName { get; set; }

        public Guid UserUid { get; set; }
        public string UserName { get; set; }

        public string UserAgent { get; set; }
        public string Ip { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Browser { get; set; }
        public string BrowserVersion { get; set; }
        public string Platform { get; set; }
        public string PlatformVersion { get; set; }
    }
}