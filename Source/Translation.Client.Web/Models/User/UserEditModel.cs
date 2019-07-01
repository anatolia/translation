using System;

using Translation.Client.Web.Models.Base;
using Translation.Client.Web.Models.InputModels;
using Translation.Common.Helpers;

namespace Translation.Client.Web.Models.User
{
    public sealed class UserEditModel : BaseModel
    {
        public Guid UserUid { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Guid LanguageUid { get; set; }
        public string LanguageName { get; set; }

        public HiddenInputModel UserUidInput { get; }
        public InputModel FirstNameInput { get; }
        public InputModel LastNameInput { get; }
        public SelectInputModel LanguageInput { get; }

        public UserEditModel()
        {
            Title = "user_edit_title";

            UserUidInput = new HiddenInputModel("UserUid");

            FirstNameInput = new InputModel("FirstName", "first_name", true);
            LastNameInput = new InputModel("LastName", "last_name", true);
            LanguageInput = new SelectInputModel("LanguageUid", "LanguageName", "language", "/Language/SelectData");
            LanguageInput.IsOptionTypeContent = true;
        }

        public override void SetInputModelValues()
        {
            UserUidInput.Value = UserUid.ToUidString();

            FirstNameInput.Value = FirstName;
            LastNameInput.Value = LastName;

            if (LanguageUid.IsNotEmptyGuid())
            {
                LanguageInput.Value = LanguageUid.ToUidString();
                LanguageInput.Text = LanguageName;
            }
        }

        public override void SetInputErrorMessages()
        {
            if (UserUid.IsEmptyGuid())
            {
                ErrorMessages.Add("organization_uid_not_valid");
            }

            FirstName = FirstName.TrimOrDefault();
            if (FirstName.IsEmpty())
            {
                FirstNameInput.ErrorMessage.Add("first_name_required_error_massage");
                InputErrorMessages.AddRange(FirstNameInput.ErrorMessage);
            }

            LastName = LastName.TrimOrDefault();
            if (LastName.IsEmpty())
            {
                LastNameInput.ErrorMessage.Add("last_name_required_error_massage");
                InputErrorMessages.AddRange(LastNameInput.ErrorMessage);
            }

            if (LanguageUid.IsEmptyGuid())
            {
                LanguageInput.ErrorMessage.Add("language_uid_not_valid");
                InputErrorMessages.AddRange(LanguageInput.ErrorMessage);
            }
        }
    }
}