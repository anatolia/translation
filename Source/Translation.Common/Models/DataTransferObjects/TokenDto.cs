using System;

using NodaTime;

using Translation.Common.Models.Base;

namespace Translation.Common.Models.DataTransferObjects
{
    public class TokenDto : BaseDto
    {
        public Guid AccessToken { get; set; }
        public Instant ExpiresAt { get; set; }
        public string IP { get; set; }
    }
}