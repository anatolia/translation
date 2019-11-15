using System;

using StandardUtils.Helpers;
using StandardUtils.Models.Requests;

namespace Translation.Common.Models.Requests.Integration.IntegrationClient
{
    public class IntegrationClientBaseRequest : BaseAuthenticatedPagedRequest
    {
        public Guid IntegrationClientUid { get; }

        public IntegrationClientBaseRequest(long currentUserId, Guid integrationClientUid) : base(currentUserId)
        {
            if (integrationClientUid.IsEmptyGuid())
            {
                ThrowArgumentException(nameof(integrationClientUid), integrationClientUid);
            }

            IntegrationClientUid = integrationClientUid;
        }
    }
}