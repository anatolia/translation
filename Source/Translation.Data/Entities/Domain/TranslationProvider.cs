using System;
using System.Reflection.Emit;
using StandardRepository.Models.Entities;
using StandardRepository.Models.Entities.Schemas;

namespace Translation.Data.Entities.Domain
{
    public class TranslationProvider : BaseEntity, ISchemaDomain
    {
        public string Value { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
    }
}