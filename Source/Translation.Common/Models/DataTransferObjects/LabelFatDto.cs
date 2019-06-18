using System;
using System.Collections.Generic;

namespace Translation.Common.Models.DataTransferObjects
{
    public class LabelFatDto
    {
        public Guid Uid { get; set; }
        public string Key { get; set; }

        public List<LabelTranslationSlimDto> Translations { get; set; }

        public LabelFatDto()
        {
            Translations = new List<LabelTranslationSlimDto>();
        }
    }
}