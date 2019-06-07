namespace Translation.Client.Web.Models.InputModels
{
    public class FileInputModel : InputModel
    {
        public bool IsMultiple { get; set; }

        public FileInputModel(string name, string labelKey, bool isRequired = false, bool isMultiple = false) : base(name, labelKey, isRequired)
        {
            IsMultiple = isMultiple;
        }
    }
}