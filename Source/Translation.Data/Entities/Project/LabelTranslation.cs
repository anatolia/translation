using Translation.Data.Entities.Base;
using Translation.Data.Entities.Base.Schemas;
using Translation.Data.Entities.Parameter;

namespace Translation.Data.Entities.Project
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