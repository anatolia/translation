using System;

using Translation.Common.Helpers;
using Translation.Common.Models.Base;

namespace Translation.Common.Models.Requests.Integration
{
    public class IntegrationBaseRequest : BaseAuthenticatedPagedRequest
    {
        public Guid IntegrationUid { get; }

        public IntegrationBaseRequest(long currentUserId, Guid integrationUid) : base(currentUserId)
        {
            if (integrationUid.IsEmptyGuid())
            {
                ThrowArgumentException(nameof(integrationUid), integrationUid);
            }

            IntegrationUid = integrationUid;
        }
    }
}