using System;

namespace Translation.Common.Models.Requests.Integration
{
    public class IntegrationDeleteRequest : IntegrationBaseRequest
    {
        public IntegrationDeleteRequest(long currentUserId, Guid integrationUid) : base(currentUserId, integrationUid)
        {
        }
    }
}