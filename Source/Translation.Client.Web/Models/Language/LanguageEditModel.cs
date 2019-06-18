using System;
using Microsoft.AspNetCore.Http;
using Translation.Client.Web.Models.Base;
using Translation.Client.Web.Models.InputModels;
using Translation.Common.Helpers;

namespace Translation.Client.Web.Models.Language
{
    public sealed class LanguageEditModel : BaseModel
    {
        public Guid LanguageUid { get; set; }

        public string Name { get; set; }
        public string OriginalName { get; set; }
        public string IsoCode2 { get; set; }
        public string IsoCode3 { get; set; }
        public IFormFile Icon { get; set; }
        public string Description { get; set; }

        public HiddenInputModel LanguageUidInput { get; }

        public InputModel NameInput { get; }
        public InputModel OriginalNameInput { get; }
        public ShortInputModel IsoCode2Input { get; }
        public ShortInputModel IsoCode3Input { get; }
        public FileInputModel IconInput { get; }
        public LongInputModel DescriptionInput { get; }

        public LanguageEditModel()
        {
            Title = "language_edit_title";

            LanguageUidInput = new HiddenInputModel("LanguageUid");

            NameInput = new InputModel("Name", "name", true);
            OriginalNameInput = new InputModel("OriginalName", "original_name", true);
            IsoCode2Input = new ShortInputModel("IsoCode2", "iso_code_2_character", true);
            IsoCode3Input = new ShortInputModel("IsoCode3", "iso_code_3_character", true);
            IconInput = new FileInputModel("Icon", "icon", false);
            DescriptionInput = new LongInputModel("Description", "description");
        }

        public override void SetInputModelValues()
        {
            LanguageUidInput.Value = LanguageUid.ToUidString();

            NameInput.Value = Name;
            OriginalNameInput.Value = OriginalName;
            IsoCode2Input.Value = IsoCode2;
            IsoCode3Input.Value = IsoCode3;
            DescriptionInput.Value = Description;
        }

        public override void SetInputErrorMessages()
        {
            if (LanguageUid.IsEmptyGuid())
            {
                ErrorMessages.Add("language_uid_not_valid");
            }

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

            if (IsoCode2.IsEmpty())
            {
                IsoCode2Input.ErrorMessage.Add("iso_code_2_required_error_message");
                InputErrorMessages.AddRange(IsoCode2Input.ErrorMessage);
            }

            if (IsoCode3.IsEmpty())
            {
                IsoCode3Input.ErrorMessage.Add("iso_code_3_required_error_message");
                InputErrorMessages.AddRange(IsoCode3Input.ErrorMessage);
            }
        }
    }
}
