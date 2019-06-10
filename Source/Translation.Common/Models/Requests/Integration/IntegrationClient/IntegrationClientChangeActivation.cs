using System;

namespace Translation.Common.Models.Requests.Integration.IntegrationClient
{
    public class IntegrationClientChangeActivationRequest : IntegrationClientBaseRequest
    {
        public IntegrationClientChangeActivationRequest(long currentUserId, Guid integrationClientUid) : base(currentUserId, integrationClientUid)
        {
        }
    }
}