using System.Collections.Generic;

namespace Cheviri.Client.Web.Models.InputModels
{
    public class SelectInputModel : InputModel
    {
        public List<string> Items { get; set; }

        public SelectInputModel(string name, string labelKey, bool isRequired = false, string value = "") : base(name, labelKey, isRequired, value)
        {
            Items = new List<string>();
        }
    }
}