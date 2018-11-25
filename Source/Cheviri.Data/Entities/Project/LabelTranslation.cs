using Cheviri.Data.Entities.Base;
using Cheviri.Data.Entities.Base.Schemas;
using Cheviri.Data.Entities.Parameter;

namespace Cheviri.Data.Entities.Project
{
    public class LabelTranslation : BaseEntity, ISchemaProject
    {
        public string Text { get; set; }
        public string Description { get; set; }

        public Organization Organization { get; set; }
        public Project Project { get; set; }
        public Language Language { get; set; }
        public Label Label { get; set; }
    }
}