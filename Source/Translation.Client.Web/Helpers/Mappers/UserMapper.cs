using Translation.Client.Web.Models.User;
using Translation.Common.Models.DataTransferObjects;

namespace Translation.Client.Web.Helpers.Mappers
{
    public class UserMapper
    {
        public UserDetailModel MapUserDetailModel(UserDto dto)
        {
            var model = new UserDetailModel();
            model.OrganizationUid = dto.OrganizationUid;
            model.OrganizationName = dto.OrganizationName;

            model.UserUid = dto.Uid;
            model.Username = dto.Name;
            model.FirstName = dto.FirstName;
            model.LastName = dto.LastName;
            model.Email = dto.Email;
            model.Description = dto.Description;
            model.IsActive = dto.IsActive;

            model.InvitedAt = dto.InvitedAt;
            model.InvitedByUserUid = dto.InvitedByUserUid;
            model.InvitedByUserName = dto.InvitedByUserName;

            model.LabelCount = dto.LabelCount;
            model.LabelTranslationCount = dto.LabelTranslationCount;

            model.LanguageName = dto.LanguageName;
            model.LanguageIconUrl = dto.LanguageIconUrl;

            return model;
        }
    }
}