using Cheviri.Data.Entities.Base;
using Cheviri.Data.Entities.Base.Schemas;

namespace Cheviri.Data.Entities.Main
{
    public class UserLoginLog : BaseEntity, ISchemaMain
    {
        public User User { get; set; }

        public string UserAgent { get; set; }
        public string Ip { get; set; }
        public string IpLocation { get; set; }
        public string Browser { get; set; }
        public string Platform { get; set; }
    }
}