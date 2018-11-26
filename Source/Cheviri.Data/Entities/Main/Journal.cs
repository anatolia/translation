using Cheviri.Data.Entities.Base;
using Cheviri.Data.Entities.Base.Schemas;
using Cheviri.Data.Entities.Project;

namespace Cheviri.Data.Entities.Main
{
    public class Journal : BaseEntity, ISchemaMain
    {
        public Organization Organization { get; set; }
        public Token Token { get; set; }
        public User User { get; set; }

        public string Message { get; set; }
    }
}