using StandardUtils.Models.DataTransferObjects;

namespace Translation.Common.Models.DataTransferObjects
{
    public class OrganizationDto : BaseDto
    {
        public string Description { get; set; }
        public bool IsActive { get; set; }

        public int UserCount { get; set; }
        public int ProjectCount { get; set; }
        public int LabelCount { get; set; }
        public int LabelTranslationCount { get; set; }
    }
}