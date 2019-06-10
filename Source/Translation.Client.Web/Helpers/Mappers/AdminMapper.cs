using System;

using Translation.Client.Web.Models.Admin;
using Translation.Common.Models.DataTransferObjects;

namespace Translation.Client.Web.Helpers.Mappers
{
    public class AdminMapper
    {
        public static AdminAcceptInviteModel MapAdminAcceptInviteModel(UserDto userDto, Guid tokenUid, string email)
        {
            var model = new AdminAcceptInviteModel();
            model.FirstName = userDto.FirstName;
            model.LastName = userDto.LastName;
            model.Token = tokenUid;
            model.Email = email;
            model.SetInputModelValues();

            return model;
        }

        public static AdminInviteModel MapAdminInviteModel(Guid organizationUid)
        {
            var model = new AdminInviteModel();
            model.OrganizationUid = organizationUid;

            model.SetInputModelValues();

            return model;
        }
    }
}