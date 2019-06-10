using System;

namespace Translation.Common.Models.Requests.Integration
{
    public class IntegrationReadRequest : IntegrationBaseRequest
    {
        public IntegrationReadRequest(long currentUserId, Guid integrationUid) : base(currentUserId, integrationUid)
        {
        }
    }
}