namespace Translation.Client.Web.Models.InputModels
{
    public class ReadOnlyInputModel : InputModel
    {
        public ReadOnlyInputModel(string labelKey, string value = "") : base(labelKey, labelKey, false, value)
        {
        }
    }
}