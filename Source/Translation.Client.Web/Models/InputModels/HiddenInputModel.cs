namespace Translation.Client.Web.Models.InputModels
{
    public class HiddenInputModel : InputModel
    {
        public HiddenInputModel(string name, string value = "") : base(name, "", false, value)
        {
        }
    }
}