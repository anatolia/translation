using System;

using StandardUtils.Models.DataTransferObjects;

namespace Translation.Common.Models.DataTransferObjects
{
    public class LabelTranslationDto : BaseDto
    {
        public Guid OrganizationUid { get; set; }
        public string OrganizationName { get; set; }

        public Guid ProjectUid { get; set; }
        public string ProjectName { get; set; }

        public Guid LabelUid { get; set; }
        public string LabelKey { get; set; }

        public Guid LanguageUid { get; set; }
        public string LanguageName { get; set; }
        public string LanguageIsoCode2 { get; set; }
        public string LanguageIconUrl { get; set; }

        public string Translation { get; set; }

    }
}