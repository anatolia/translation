namespace Translation.Client.Web.Models.InputModels
{
    public class FileInputModel : InputModel
    {
        public FileInputModel(string name, string labelKey, bool isRequired = false, string value = "") : base(name, labelKey, isRequired, value)
        {
        }
    }
}