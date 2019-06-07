namespace Translation.Client.Web.Models.InputModels
{
    public class EmailInputModel : InputModel
    {
        public EmailInputModel(string name, string labelKey, bool isRequired = false) : base(name, labelKey, isRequired)
        {
        }
    }
}