using Cheviri.Data.Entities.Base;
using Cheviri.Data.Entities.Base.Schemas;

namespace Cheviri.Data.Entities.Parameter
{
    public class Word : BaseEntity, ISchemaParameter
    {
        public string Text { get; set; }

        public Language Language { get; set; }
    }
}