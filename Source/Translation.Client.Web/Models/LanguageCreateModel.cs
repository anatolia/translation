using Microsoft.AspNetCore.Http;

using Translation.Client.Web.Helpers;
using Translation.Client.Web.Models.InputModels;
using Translation.Common.Helpers;

namespace Translation.Client.Web.Models
{
    public class LanguageCreateModel : BaseModel
    {
        public string Name { get; set; }
        public string IsoCode2 { get; set; }
        public string IsoCode3 { get; set; }
        public IFormFile Icon { get; set; }
        public string Description { get; set; }

        public InputModel NameInput { get; set; }
        public ShortInputModel IsoCode2Input { get; set; }
        public ShortInputModel IsoCode3Input { get; set; }
        public FileInputModel IconInput { get; set; }
        public LongInputModel DescriptionInput { get; set; }

        public LanguageCreateModel()
        {
            Title = Localizer.Localize("language_create_title");

            NameInput = new InputModel("Name", "name", true);
            IsoCode2Input = new ShortInputModel("IsoCode2", "iso_code_2_character", true);
            IsoCode3Input = new ShortInputModel("IsoCode3", "iso_code_3_character", true);
            IconInput = new FileInputModel("Icon", "icon", true);
            DescriptionInput = new LongInputModel("Description", "description");
        }

        public override void SetInputModelValues()
        {
            NameInput.Value = Name;
            IsoCode2Input.Value = IsoCode2;
            IsoCode3Input.Value = IsoCode3;
            DescriptionInput.Value = Description;
        }

        public override void SetInputErrorMessages()
        {
            if (Name.IsEmpty())
            {
                NameInput.ErrorMessage.Add("name_required_error_message");
                ErrorMessages.AddRange(NameInput.ErrorMessage);
            }

            if (IsoCode2.IsEmpty())
            {
                IsoCode2Input.ErrorMessage.Add("iso_code_2_required_error_message");
                ErrorMessages.AddRange(IsoCode2Input.ErrorMessage);
            }

            if (IsoCode3.IsEmpty())
            {
                IsoCode3Input.ErrorMessage.Add("iso_code_3_required_error_message");
                ErrorMessages.AddRange(IsoCode3Input.ErrorMessage);
            }
        }
    }
}