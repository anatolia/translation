using System;
using System.Collections.Generic;
using System.Transactions;

namespace Translation.Client.Tool.PushKeysFromSource.Models
{
    public class LabelInfo
    {
        public Guid uid { get; set; }
        public string key { get; set; }
        public List<TranslationInfo> translations { get; set; }

        public LabelInfo()
        {
            translations = new List<TranslationInfo>();
        }
    }
}