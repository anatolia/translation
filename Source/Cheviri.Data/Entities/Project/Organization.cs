using System.Collections.Generic;

using Cheviri.Data.Entities.Base;
using Cheviri.Data.Entities.Base.Schemas;
using Cheviri.Data.Entities.Main;

namespace Cheviri.Data.Entities.Project
{
    public class Organization : BaseEntity, ISchemaProject
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public string InsecureKey { get; set; }
        public string InsecureIV { get; set; }

        public List<Project> Projects { get; set; }
        public List<User> Users { get; set; }
        public List<Integration> Integrations { get; set; }

        public Organization()
        {
            Projects = new List<Project>();
            Users = new List<User>();
            Integrations = new List<Integration>();
        }
    }
}