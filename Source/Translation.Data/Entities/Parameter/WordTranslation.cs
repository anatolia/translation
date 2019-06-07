using Translation.Data.Entities.Base;
using Translation.Data.Entities.Base.Schemas;

namespace Translation.Data.Entities.Parameter
{
    public class WordTranslation : BaseEntity, ISchemaParameter
    {
        public string Text { get; set; }
        public string Description { get; set; }

        public Word Word { get; set; }
        public Language Language { get; set; }
    }
}