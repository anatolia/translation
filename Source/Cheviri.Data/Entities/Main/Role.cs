using System.Collections.Generic;

using Cheviri.Data.Entities.Base;
using Cheviri.Data.Entities.Base.Schemas;

namespace Cheviri.Data.Entities.Main
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