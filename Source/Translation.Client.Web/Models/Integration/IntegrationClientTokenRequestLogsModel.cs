using System;
using Translation.Client.Web.Models.Base;

namespace Translation.Client.Web.Models.Integration
{
    public class IntegrationClientTokenRequestLogsModel : BaseModel
    {
        public Guid IntegrationClientUid { get; set; }

        public IntegrationClientTokenRequestLogsModel()
        {
            Title = "token_request_logs_title";
        }
    }
}