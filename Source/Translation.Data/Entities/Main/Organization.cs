using StandardRepository.Models.Entities;
using StandardRepository.Models.Entities.Schemas;

namespace Translation.Data.Entities.Main
{
    public class Organization : BaseEntity, ISchemaMain
    {
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public string ObfuscationKey { get; set; }
        public string ObfuscationIv { get; set; }

        public int UserCount { get; set; }
        public int ProjectCount { get; set; }
        public int LabelCount { get; set; }
        public int LabelTranslationCount { get; set; }

        public bool IsSuperOrganization { get; set; }
    }
}