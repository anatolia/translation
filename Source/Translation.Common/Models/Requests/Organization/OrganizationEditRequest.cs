using System;

using Translation.Common.Helpers;

namespace Translation.Common.Models.Requests.Organization
{
    public class OrganizationEditRequest : OrganizationBaseRequest
    {
        public string Name { get; }
        public string Description { get; }

        public OrganizationEditRequest(long currentUserId, Guid organizationUid, string name, 
                                       string description) : base(currentUserId, organizationUid)
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