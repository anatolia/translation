using System;

using StandardUtils.Helpers;

namespace Translation.Common.Models.Requests.Integration.IntegrationClient
{
    public sealed class IntegrationClientEditRequest : IntegrationClientBaseRequest
    {
        public string Name { get; }
        public string Description { get; }

        public IntegrationClientEditRequest(long currentUserId, Guid integrationClientUid, string name,
                                            string description) : base(currentUserId, integrationClientUid)
        {
            if (name.IsEmpty())
            {
                ThrowArgumentException(nameof(name), name);
            }

            Name = name;
            Description = description;
        }
    }
}
