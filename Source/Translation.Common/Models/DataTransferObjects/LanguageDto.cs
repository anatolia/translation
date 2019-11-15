using StandardUtils.Models.DataTransferObjects;

namespace Translation.Common.Models.DataTransferObjects
{
    public class LanguageDto : BaseDto
    {
        public string IsoCode2 { get; set; }
        public string IsoCode3 { get; set; }
        public string IconPath { get; set; }
        public string Description { get; set; }
        public string OriginalName { get; set; }
        public string IconUrl { get; set; }
    }
}