using System;

using Translation.Common.Helpers;
using Translation.Common.Models.Base;

namespace Translation.Common.Models.Requests.Integration.IntegrationClient
{
    public sealed class IntegrationClientCreateRequest : BaseAuthenticatedRequest
    {
        public Guid IntegrationUid { get; }

        public IntegrationClientCreateRequest(long currentUserId, Guid integrationUid) : base(currentUserId)
        {
            if (integrationUid.IsEmptyGuid())
            {
                ThrowArgumentException(nameof(integrationUid), integrationUid);
            }

            IntegrationUid = integrationUid;
        }
    }
}
