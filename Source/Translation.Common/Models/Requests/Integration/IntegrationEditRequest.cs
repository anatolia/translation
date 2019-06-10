using System;

using Translation.Common.Helpers;

namespace Translation.Common.Models.Requests.Integration
{
    public class IntegrationEditRequest : IntegrationBaseRequest
    {
        public string Name { get; }
        public string Description { get; }

        public IntegrationEditRequest(long currentUserId, Guid integrationUid, string name,
                                      string description) : base(currentUserId, integrationUid)
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