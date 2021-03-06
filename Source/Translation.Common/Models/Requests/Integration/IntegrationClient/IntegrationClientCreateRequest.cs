﻿using System;

using StandardUtils.Helpers;
using StandardUtils.Models.Requests;

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
