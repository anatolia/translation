using Cheviri.Data.Entities.Base;
using Cheviri.Data.Entities.Base.Schemas;

namespace Cheviri.Data.Entities.Main
{
    public class PermissionLog : BaseEntity, ISchemaMain
    {
        public User User { get; set; }
        public Token Token { get; set; }
        public Permission Permission { get; set; }

        public bool IsAllowed { get; set; }
    }
}