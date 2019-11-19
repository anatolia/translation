using System;
using System.Net;

using StandardUtils.Helpers;
using StandardUtils.Models.Requests;

namespace Translation.Common.Models.Requests.Integration.Token
{
    public class TokenCreateRequest : BaseRequest
    {
        public Guid ClientId { get; }
        public Guid ClientSecret { get; }
        public IPAddress IP { get; set; }

        public TokenCreateRequest(Guid clientId, Guid clientSecret, IPAddress ip)
        {
            if (clientId.IsEmptyGuid())
            {
                ThrowArgumentException(nameof(clientId), clientId);
            }

            if (clientSecret.IsEmptyGuid())
            {
                ThrowArgumentException(nameof(clientSecret), clientSecret);
            }

            ClientId = clientId;
            ClientSecret = clientSecret;
            IP = ip;
        }
    }
}