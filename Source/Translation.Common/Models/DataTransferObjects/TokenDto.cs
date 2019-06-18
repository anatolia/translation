using System;

using Translation.Common.Models.Base;

namespace Translation.Common.Models.DataTransferObjects
{
    public class TokenDto : BaseDto
    {
        public Guid IntegrationClientUid { get; set; }
        public Guid AccessToken { get; set; }
        public DateTime ExpiresAt { get; set; }
        public string IP { get; set; }
    }
}