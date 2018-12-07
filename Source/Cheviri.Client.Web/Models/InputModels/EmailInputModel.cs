    namespace Cheviri.Client.Web.Models.InputModels
{
    public class EmailInputModel : InputModel
    {
        public EmailInputModel(string name, string labelKey, bool isRequired = false, string value = "") : base(name, labelKey, isRequired, value)
        {
        }
    }
}