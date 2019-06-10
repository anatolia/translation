using System;

using Translation.Common.Helpers;
using Translation.Common.Models.Base;

namespace Translation.Common.Models.Requests.Integration
{
    public class IntegrationCreateRequest : BaseAuthenticatedRequest
    {
        public Guid OrganizationUid { get; }
        public string Name { get; }
        public string Description { get; }

        public IntegrationCreateRequest(long currentUserId, Guid organizationUid, string name,
                                        string description) : base(currentUserId)
        {
            if (name.IsEmpty())
            {
                ThrowArgumentException(nameof(name), name);
            }

            if (organizationUid.IsEmptyGuid())
            {
                ThrowArgumentException(nameof(organizationUid), organizationUid);
            }

            OrganizationUid = organizationUid;
            Name = name;
            Description = description;
        }
    }
}