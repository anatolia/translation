using System;

namespace Translation.Common.Models.Requests.Integration.IntegrationClient
{
    public class IntegrationClientDeleteRequest : IntegrationClientBaseRequest
    {
        public IntegrationClientDeleteRequest(long currentUserId, Guid integrationClientUid) : base(currentUserId, integrationClientUid)
        {
        }
    }
}