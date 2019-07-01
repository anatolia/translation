using System;

namespace Translation.Client.Web.Models.Base
{
    public class TokenResult
    {
        public bool IsOk { get; set; }
        public Guid Token { get; set; }
        public string CreatedAt { get; set; }
        public string ExpiresAt { get; set; }
    }
}