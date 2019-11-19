using System;

using StandardUtils.Helpers;
using StandardUtils.Models.Requests;

namespace Translation.Common.Models.Requests.Integration.Token
{
    public class TokenValidateRequest : BaseRequest
    {
        public Guid ProjectUid { get; }
        public Guid Token { get; }

        public TokenValidateRequest(Guid projectUid, Guid token)
        {
            if (projectUid.IsEmptyGuid())
            {
                ThrowArgumentException(nameof(projectUid), projectUid);
            }

            if (token.IsEmptyGuid())
            {
                ThrowArgumentException(nameof(token), token);
            }

            ProjectUid = projectUid;
            Token = token;
        }
    }
}