using Translation.Data.Entities.Base;
using Translation.Data.Entities.Base.Schemas;

namespace Translation.Data.Entities.Main
{
    public class PermissionLog : BaseEntity, ISchemaMain
    {
        public User User { get; set; }
        public Token Token { get; set; }
        public Permission Permission { get; set; }

        public bool IsAllowed { get; set; }
    }
}