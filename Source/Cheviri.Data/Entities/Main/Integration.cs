using System.Collections.Generic;

using Cheviri.Data.Entities.Base;
using Cheviri.Data.Entities.Base.Schemas;
using Cheviri.Data.Entities.Project;

namespace Cheviri.Data.Entities.Main
{
    public class Integration : BaseEntity, ISchemaMain
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }

        public Organization Organization { get; set; }
        public List<Role> Roles { get; set; }

        public Integration()
        {
            Roles = new List<Role>();
        }
    }
}