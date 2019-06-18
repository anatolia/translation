using System;

using Translation.Client.Web.Models.Base;

namespace Translation.Client.Web.Models.Integration
{
    public class IntegrationActiveTokensModel : BaseModel
    {
        public Guid IntegrationUid { get; set; }
        public string IntegrationName { get; set; }

        public IntegrationActiveTokensModel()
        {
            Title = "active_tokens_title";
        }
    }
}