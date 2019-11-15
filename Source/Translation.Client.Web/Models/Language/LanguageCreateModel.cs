using Microsoft.AspNetCore.Http;

using StandardUtils.Helpers;

using Translation.Client.Web.Models.Base;
using Translation.Client.Web.Models.InputModels;

namespace Translation.Client.Web.Models.Language
{
    public sealed class LanguageCreateModel : BaseModel
    {
        public string Name { get; set; }
        public string OriginalName { get; set; }
        public string IsoCode2 { get; set; }
        public string IsoCode3 { get; set; }
        public IFormFile Icon { get; set; }
        public string Description { get; set; }

        public InputModel NameInput { get; }
        public InputModel OriginalNameInput { get; }
        public ShortInputModel IsoCode2Input { get; }
        public ShortInputModel IsoCode3Input { get; }
        public FileInputModel IconInput { get; }
        public LongInputModel DescriptionInput { get; }

        public LanguageCreateModel()
        {
            Title = "language_create_title";

            NameInput = new InputModel("Name", "name", true);
            OriginalNameInput = new InputModel("OriginalName", "original_name", true);
            IsoCode2Input = new ShortInputModel("IsoCode2", "iso_code_2_character", true);
            IsoCode3Input = new ShortInputModel("IsoCode3", "iso_code_3_character", true);
            IconInput = new FileInputModel("Icon", "icon", true);
            DescriptionInput = new LongInputModel("Description", "description");
        }

        public override void SetInputModelValues()
        {
            NameInput.Value = Name;
            OriginalNameInput.Value = OriginalName;
            IsoCode2Input.Value = IsoCode2;
            IsoCode3Input.Value = IsoCode3;
            DescriptionInput.Value = Description;
        }

        public override void SetInputErrorMessages()
        {
            Name = Name.TrimOrDefault();
            if (Name.IsEmpty())
            {
                NameInput.ErrorMessage.Add("language_name_required_error_message");
                InputErrorMessages.AddRange(NameInput.ErrorMessage);
            }

            OriginalName = OriginalName.TrimOrDefault();
            if (OriginalName.IsEmpty())
            {
                OriginalNameInput.ErrorMessage.Add("language_original_name_required_error_message");
                InputErrorMessages.AddRange(OriginalNameInput.ErrorMessage);
            }

            IsoCode2 = IsoCode2.TrimOrDefault();
            if (IsoCode2.IsEmpty())
            {
                IsoCode2Input.ErrorMessage.Add("iso_code_2_required_error_message");
                InputErrorMessages.AddRange(IsoCode2Input.ErrorMessage);
            }

            IsoCode3 = IsoCode3.TrimOrDefault();
            if (IsoCode3.IsEmpty())
            {
                IsoCode2Input.ErrorMessage.Add("iso_code_3_required_error_message");
                InputErrorMessages.AddRange(IsoCode2Input.ErrorMessage);
            }
            
            if (IsoCode2.IsNotEmpty()
                && IsoCode2.Length != 2)
            {
                IsoCode2Input.ErrorMessage.Add("iso_code_2_must_be_2_character");
                InputErrorMessages.AddRange(IsoCode2Input.ErrorMessage);
            }
            
            if (IsoCode3.IsNotEmpty()
                && IsoCode3.Length != 3)
            {
                IsoCode3Input.ErrorMessage.Add("iso_code_3_must_be_3_character");
                InputErrorMessages.AddRange(IsoCode3Input.ErrorMessage);
            }

            if (Icon == null)
            {
                IconInput.ErrorMessage.Add("icon_required_error_message");
                InputErrorMessages.AddRange(IconInput.ErrorMessage);
            }
            else if(Icon.ContentType != "image/png")
            {
                IconInput.ErrorMessage.Add("icon_file_type_error_message");
                InputErrorMessages.AddRange(IconInput.ErrorMessage);
            }
        }
    }
}
