using Microsoft.AspNetCore.Http;

using Cheviri.Client.Web.Helpers;
using Cheviri.Client.Web.Models.InputModels;
using Cheviri.Common.Helpers;

namespace Cheviri.Client.Web.Models
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
                NameInput.ErrorMessage = "name_required_error_message";
                ErrorMessages.Add(NameInput.ErrorMessage);
            }

            if (IsoCode2.IsEmpty())
            {
                IsoCode2Input.ErrorMessage = "iso_code_2_required_error_message";
                ErrorMessages.Add(IsoCode2Input.ErrorMessage);
            }

            if (IsoCode3.IsEmpty())
            {
                IsoCode3Input.ErrorMessage = "iso_code_3_required_error_message";
                ErrorMessages.Add(IsoCode3Input.ErrorMessage);
            }
        }
    }
}