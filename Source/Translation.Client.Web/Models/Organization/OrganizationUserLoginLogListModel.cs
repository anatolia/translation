using System;

using Translation.Client.Web.Models.Base;

namespace Translation.Client.Web.Models.Organization
{
    public sealed class OrganizationUserLoginLogListModel : BaseModel
    {
        public Guid OrganizationUid { get; set; }
        public OrganizationUserLoginLogListModel()
        {
            Title = "user_login_logs_title";
        }
    }
}