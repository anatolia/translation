using Translation.Data.Entities.Base;
using Translation.Data.Entities.Base.Schemas;
using Translation.Data.Entities.Project;

namespace Translation.Data.Entities.Main
{
    public class Journal : BaseEntity, ISchemaMain
    {
        public Organization Organization { get; set; }
        public Token Token { get; set; }
        public User User { get; set; }

        public string Message { get; set; }
    }
}