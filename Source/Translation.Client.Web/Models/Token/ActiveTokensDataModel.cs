using Translation.Client.Web.Models.Base;

namespace Translation.Client.Web.Models.Token
{
    public class ActiveTokensDataModel : BaseModel
    {
        public string TokenUid { get; set; }
        public string Ip { get; set; }
        public string CreatedAt { get; set; }
        public string ExpiresAt { get; set; }

        public ActiveTokensDataModel()
        {
            Title = "active_tokens_table_title";
        }
    }
}