using System;

using Translation.Client.Web.Models.Base;

namespace Translation.Client.Web.Models.Integration
{
    public class IntegrationRevisionReadListModel : BaseModel
    {
        public string IntegrationName { get; set; }
        public Guid IntegrationUid { get; set; }

        public IntegrationRevisionReadListModel()
        {
            Title = "integration_revision_list_title";
        }
    }
}