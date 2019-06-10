using System;

using Translation.Common.Helpers;
using Translation.Common.Models.Base;

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