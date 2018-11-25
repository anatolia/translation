using Cheviri.Data.Entities.Base;
using Cheviri.Data.Entities.Base.Schemas;

namespace Cheviri.Data.Entities.Main
{
    public class Permission : BaseEntity, ISchemaMain
    {
        public string Name { get; set; }
        public bool IsAllowed { get; set; }
        public bool IsDenied { get; set; }
        public bool IsActive { get; set; }
    }
}