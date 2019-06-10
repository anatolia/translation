using System;

using Translation.Common.Models.Requests.Organization;

namespace Translation.Common.Models.Requests.SendEmailLog
{
    public class OrganizationSendEmailLogReadListRequest : OrganizationBaseRequest
    {
        public OrganizationSendEmailLogReadListRequest(long currentUserId, Guid organizationUid) : base(currentUserId, organizationUid)
        {
        }
    }
}