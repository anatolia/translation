using System;

namespace Translation.Common.Models.Requests.Integration.IntegrationClient
{
    public class IntegrationClientRefreshRequest : IntegrationClientBaseRequest
    {
        public IntegrationClientRefreshRequest(long currentUserId, Guid integrationClientUid) : base(currentUserId, integrationClientUid)
        {
        }
    }
}