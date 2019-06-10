using System;

namespace Translation.Common.Models.Requests.Integration
{
    public class IntegrationChangeActivationRequest : IntegrationBaseRequest
    {
        public IntegrationChangeActivationRequest(long currentUserId, Guid integrationUid) : base(currentUserId, integrationUid)
        {
        }
    }
}