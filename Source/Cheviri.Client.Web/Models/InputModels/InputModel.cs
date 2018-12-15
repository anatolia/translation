using System.Collections.Generic;

namespace Cheviri.Client.Web.Models.InputModels
{
    public class InputModel
    {
        public string Name { get; set; }
        public string LabelKey { get; set; }

        public List<string> ErrorMessage { get; set; }

        public bool IsRequired { get; set; }
        public string Value { get; set; }

        public InputModel(string name, string labelKey, bool isRequired = false, string value = "")
        {
            Name = name;
            LabelKey = labelKey;
            IsRequired = isRequired;
            Value = value;
            ErrorMessage = new List<string>();
        }
    }
}