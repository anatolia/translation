using System.Collections.Generic;

using Translation.Data.Entities.Base;
using Translation.Data.Entities.Base.Schemas;
using Translation.Data.Entities.Project;

namespace Translation.Data.Entities.Main
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