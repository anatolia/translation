using System;

namespace Translation.Client.Web.Models.InputModels
{
    public class DateInputModel : InputModel
    {
        public new DateTime Value { get; set; }

        public DateInputModel(string name, string labelKey, bool isRequired = false) : base(name, labelKey, isRequired)
        {

        }
    }
}