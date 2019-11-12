using System;

using Translation.Common.Models.Base;

namespace Translation.Common.Models.DataTransferObjects
{
    public class TranslationProviderDto : BaseDto
    {
        public string CredentialValue { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
    }
}