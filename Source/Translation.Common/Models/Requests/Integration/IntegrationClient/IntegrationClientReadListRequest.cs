using System;

using StandardUtils.Helpers;
using StandardUtils.Models.Requests;

namespace Translation.Common.Models.Requests.Integration.IntegrationClient
{
    public class IntegrationClientReadListRequest : BaseAuthenticatedPagedRequest
    {
        public Guid IntegrationUid { get; }

        public IntegrationClientReadListRequest(long currentUserId, Guid integrationUid) : base(currentUserId)
        {
            if (integrationUid.IsEmptyGuid())
            {
                ThrowArgumentException(nameof(integrationUid), integrationUid);
            }

            IntegrationUid = integrationUid;
        }
    }
}