using System;

namespace Translation.Common.Models.Requests.Integration.IntegrationClient
{
    public class IntegrationClientTokenRequestLogReadListRequest : IntegrationClientBaseRequest
    {
        public IntegrationClientTokenRequestLogReadListRequest(long currentUserId, Guid integrationClientUid) : base(currentUserId, integrationClientUid)
        {
        }
    }
}