using System;

namespace Translation.Common.Models.Requests.Integration.Token
{
    public class IntegrationTokenRequestLogReadListRequest : IntegrationBaseRequest
    {
        public IntegrationTokenRequestLogReadListRequest(long currentUserId, Guid integrationUid) : base(currentUserId, integrationUid)
        {
        }
    }
}