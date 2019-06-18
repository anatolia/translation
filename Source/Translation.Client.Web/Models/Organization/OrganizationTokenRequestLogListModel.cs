using System;

using Translation.Client.Web.Models.Base;

namespace Translation.Client.Web.Models.Organization
{
    public sealed class OrganizationTokenRequestLogListModel : BaseModel
    {
        public Guid OrganizationUid { get; set; }

        public OrganizationTokenRequestLogListModel()
        {
            Title = "token_request_logs_title";
        }
    }
}