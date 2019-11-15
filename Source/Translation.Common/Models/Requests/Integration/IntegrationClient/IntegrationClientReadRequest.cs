using System;

using StandardUtils.Helpers;
using StandardUtils.Models.Requests;

namespace Translation.Common.Models.Requests.Integration.IntegrationClient
{
    public class IntegrationClientReadRequest : BaseAuthenticatedRequest
    {
        public Guid IntegrationClientUid { get; }

        public IntegrationClientReadRequest(long currentUserId, Guid integrationClientUid) : base(currentUserId)
        {
            if (integrationClientUid.IsEmptyGuid())
            {
                ThrowArgumentException(nameof(integrationClientUid), integrationClientUid);
            }

            IntegrationClientUid = integrationClientUid;
        }
    }
}