using System;

using StandardUtils.Models.DataTransferObjects;

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