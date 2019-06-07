using Translation.Data.Entities.Base;
using Translation.Data.Entities.Base.Schemas;

namespace Translation.Data.Entities.Project
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