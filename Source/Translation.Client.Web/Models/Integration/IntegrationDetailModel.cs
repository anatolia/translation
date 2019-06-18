using System;
using Translation.Client.Web.Models.Base;

namespace Translation.Client.Web.Models.Integration
{
    public class IntegrationDetailModel : BaseModel
    {
        public Guid OrganizationUid { get; set; }
        public string OrganizationName { get; set; }

        public Guid IntegrationUid { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public IntegrationDetailModel()
        {
            Title = "integration_detail_title";
        }
    }
}