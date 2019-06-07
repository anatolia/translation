using System.Collections.Generic;

using Translation.Data.Entities.Base;
using Translation.Data.Entities.Base.Schemas;

namespace Translation.Data.Entities.Main
{
    public class Role : BaseEntity, ISchemaMain
    {
        public string Name { get; set; }
        public bool IsActive { get; set; }

        public List<Permission> Permissions { get; set; }

        public Role()
        {
            Permissions = new List<Permission>();
        }
    }
}