using System;

namespace Translation.Common.Models.Shared
{
    public class ActiveTranslationProvider
    {
        public long Id { get; set; }
        public Guid Uid { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
    }
}