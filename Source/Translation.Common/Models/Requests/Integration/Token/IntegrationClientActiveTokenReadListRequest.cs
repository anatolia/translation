using System;

using Translation.Common.Models.Requests.Integration.IntegrationClient;

namespace Translation.Common.Models.Requests.Integration.Token
{
    public class IntegrationClientActiveTokenReadListRequest : IntegrationClientBaseRequest
    {
        public IntegrationClientActiveTokenReadListRequest(long currentUserId, Guid integrationClientUid) : base(currentUserId, integrationClientUid)
        {
        }
    }
}