using System;

using Translation.Client.Web.Models.Base;

namespace Translation.Client.Web.Models.Organization
{
    public class OrganizationRevisionReadListModel : BaseModel
    {
        public Guid OrganizationUid { get; set; }

        public OrganizationRevisionReadListModel()
        {
            Title = "organization_revision_list_title";
        }
    }
}