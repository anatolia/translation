using Translation.Client.Web.Models.Base;

namespace Translation.Client.Web.Models.Token
{
    public class ActiveTokensModel : BaseModel
    {
        public string IntegrationUid { get; set; }
        public string IntegrationName { get; set; }
        public string ClientUid { get; set; }

        public ActiveTokensModel()
        {
            Title = "active_tokens_title";
        }
    }
}