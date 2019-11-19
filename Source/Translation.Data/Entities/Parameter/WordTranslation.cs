using System;

using StandardRepository.Models.Entities;
using StandardRepository.Models.Entities.Schemas;

namespace Translation.Data.Entities.Parameter
{
    public class WordTranslation : BaseEntity, ISchemaParameter
    {
        public long WordId { get; set; }
        public Guid WordUid { get; set; }
        public string WordName { get; set; }

        public long LanguageId { get; set; }
        public Guid LanguageUid { get; set; }
        public string LanguageName { get; set; }

        public string TranslationText { get; set; }
        public string Description { get; set; }
    }
}