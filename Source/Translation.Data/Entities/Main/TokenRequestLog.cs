using Translation.Data.Entities.Base;
using Translation.Data.Entities.Base.Schemas;

namespace Translation.Data.Entities.Main
{
    public class TokenRequestLog : BaseEntity, ISchemaMain
    {
        public Token Token { get; set; }

        public string Ip { get; set; }
        public string IpLocation { get; set; }
        public string HttpMethod { get; set; }
        public string Request { get; set; }
        public string Response { get; set; }
        public string ResponseCode { get; set; }
    }
}