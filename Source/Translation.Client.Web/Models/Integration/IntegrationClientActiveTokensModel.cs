using System;

using Translation.Client.Web.Models.Base;

namespace Translation.Client.Web.Models.Integration
{
    public class IntegrationClientActiveTokensModel : BaseModel
    {
        public Guid IntegrationUid { get; set; }
        public string IntegrationName { get; set; }

        public Guid ClientUid { get; set; }

        public IntegrationClientActiveTokensModel()
        {
            Title = "active_tokens_title";
        }
    }
}