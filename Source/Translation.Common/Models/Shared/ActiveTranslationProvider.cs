using System;

namespace Translation.Common.Models.Shared
{
    public class ActiveTranslationProvider
    {
        public long Id { get; set; }
        public Guid Uid { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public string Info
        {
            get { return "selected_provider_is_" + Name; }
        }

        public bool IsActive { get; set; }
    }
}