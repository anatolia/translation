using System;

using StandardRepository.Models.Entities;
using StandardRepository.Models.Entities.Schemas;

namespace Translation.Data.Entities.Parameter
{
    public class Word : BaseEntity, ISchemaParameter
    {
        public long LanguageId { get; set; }
        public Guid LanguageUid { get; set; }
        public string LanguageName { get; set; }
    }
}