using System.Collections.Generic;

using Cheviri.Data.Entities.Base;
using Cheviri.Data.Entities.Base.Schemas;

namespace Cheviri.Data.Entities.Project
{
    public class Label : BaseEntity, ISchemaProject
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public Organization Organization { get; set; }
        public Project Project { get; set; }
        public List<LabelTranslation> Translations { get; set; }

        public Label()
        {
            Translations = new List<LabelTranslation>();
        }
    }
}