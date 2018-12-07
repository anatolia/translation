namespace Cheviri.Client.Web.Models.InputModels
{
    public class ShortInputModel : InputModel
    {
        public ShortInputModel(string name, string labelKey, bool isRequired = false, string value = "") : base(name, labelKey, isRequired, value)
        {
        }
    }
}