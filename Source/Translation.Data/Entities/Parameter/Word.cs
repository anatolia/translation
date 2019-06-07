using Translation.Data.Entities.Base;
using Translation.Data.Entities.Base.Schemas;

namespace Translation.Data.Entities.Parameter
{
    public class Word : BaseEntity, ISchemaParameter
    {
        public string Text { get; set; }

        public Language Language { get; set; }
    }
}