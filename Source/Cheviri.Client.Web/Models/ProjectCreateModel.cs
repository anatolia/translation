using Cheviri.Client.Web.Helpers;
using Cheviri.Client.Web.Models.InputModels;
using Cheviri.Common.Helpers;

namespace Cheviri.Client.Web.Models
{
    public class ProjectCreateModel : BaseModel
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }

        public InputModel NameInput { get; set; }
        public UrlInputModel UrlInput { get; set; }
        public LongInputModel DescriptionInput { get; set; }

        public ProjectCreateModel()
        {
            Title = Localizer.Localize("project_create_title");

            NameInput = new InputModel("Name", "name", true);
            UrlInput = new UrlInputModel("Url", "url");
            DescriptionInput = new LongInputModel("Description", "description");
        }

        public override void SetInputModelValues()
        {
            NameInput.Value = Name;
            UrlInput.Value = Url;
            DescriptionInput.Value = Description;
        }

        public override void SetInputErrorMessages()
        {
            if (Name.IsEmpty())
            {
                NameInput.ErrorMessage.Add("name_required_error_message");
                ErrorMessages.AddRange(NameInput.ErrorMessage);
            }

            if (Url.IsNotEmpty()
                && Url.IsNotUrl())
            {
                UrlInput.ErrorMessage.Add("url_is_not_valid_error_message");
                ErrorMessages.AddRange(UrlInput.ErrorMessage);
            }
        }
    }
}