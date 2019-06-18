using System;

using Translation.Client.Web.Models.Base;
using Translation.Client.Web.Models.InputModels;

namespace Translation.Client.Web.Models.User
{
    public class UserDetailModel : BaseModel
    {
        public Guid UserUid { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Description { get; set; }
        public bool IsAdmin { get; set; }
        public int LabelCount { get; set; }
        public int LabelTranslationCount { get; set; }

        public string LabelKey { get; set; }
        public string LanguageName { get; set; }
        public string LanguageIconUrl { get; set; }

        public Guid OrganizationUid { get; set; }
        public string OrganizationName { get; set; }

        public DateTime? InvitedAt { get; set; }
        public Guid? InvitedByUserUid { get; set; }
        public string InvitedByUserName { get; set; }

        private bool _isActive;
        public bool IsActive
        {
            get => _isActive;
            set
            {
                _isActive = value;
                IsActiveInput.Value = _isActive;
            }
        }

        public CheckboxInputModel IsActiveInput { get; set; }

        public UserDetailModel()
        {
            Title = "user_detail_title";

            IsActiveInput = new CheckboxInputModel("IsActive", "is_active", false, true, false);
        }
    }
}