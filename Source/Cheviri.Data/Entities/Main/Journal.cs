using Cheviri.Data.Entities.Base;
using Cheviri.Data.Entities.Base.Schemas;
using Cheviri.Data.Entities.Project;

namespace Cheviri.Data.Entities.Main
{
    public class Journal : BaseEntity, ISchemaMain
    {
        public Organization Organization { get; set; }
        public User User { get; set; }

        public string Message { get; set; }

        public string Ip { get; set; }
        public string Url { get; set; }
        public string HttpMethod { get; set; }
        public string Controller { get; set; }
        public string ActionMethod { get; set; }
        public string Body { get; set; }
    }
}