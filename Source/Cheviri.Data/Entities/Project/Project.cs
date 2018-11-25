using Cheviri.Data.Entities.Base;
using Cheviri.Data.Entities.Base.Schemas;

namespace Cheviri.Data.Entities.Project
{
    public class Project : BaseEntity, ISchemaProject
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public bool IsActive { get; set; }
        public int LabelCount { get; set; }

        public Organization Organization { get; set; }
    }
}