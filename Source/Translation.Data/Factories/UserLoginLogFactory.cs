using Translation.Common.Models.DataTransferObjects;
using Translation.Common.Models.Requests.Organization;
using Translation.Common.Models.Requests.User;
using Translation.Common.Models.Shared;
using Translation.Data.Entities.Main;

namespace Translation.Data.Factories
{
    public class UserLoginLogFactory
    {
        public UserLoginLog CreateEntityFromRequest(LogOnRequest request, User user)
        {
            var entity = new UserLoginLog();
            MapClientLogInfo(request.ClientLogInfo, entity);

            entity.OrganizationId = user.OrganizationId;
            entity.OrganizationName = user.OrganizationName;
            entity.UserId = user.Id;
            entity.UserName = user.Name;

            return entity;
        }

        public UserLoginLog CreateEntityFromRequest(SignUpRequest request)
        {
            var entity = new UserLoginLog();
            MapClientLogInfo(request.ClientLogInfo, entity);

            entity.OrganizationName = request.OrganizationName;
            entity.UserName = request.FirstName + " " + request.LastName;

            return entity;
        }

        private void MapClientLogInfo(ClientLogInfo clientLogInfo, UserLoginLog entity)
        {
            entity.UserAgent = clientLogInfo.UserAgent;
            entity.Platform = clientLogInfo.Platform;
            entity.PlatformVersion = clientLogInfo.PlatformVersion;
            entity.Browser = clientLogInfo.Browser;
            entity.BrowserVersion = clientLogInfo.BrowserVersion;
            entity.Ip = clientLogInfo.Ip;
            entity.Country = clientLogInfo.Country;
            entity.City = clientLogInfo.City;
        }

        public UserLoginLogDto CreateDtoFromEntity(UserLoginLog entity)
        {
            var dto = new UserLoginLogDto();
            dto.Uid = entity.Uid;
            dto.OrganizationUid = entity.OrganizationUid;
            dto.OrganizationName = entity.OrganizationName;
            dto.UserUid = entity.UserUid;
            dto.UserName = entity.UserName;
            dto.CreatedAt = entity.CreatedAt;
            dto.UpdatedAt = entity.UpdatedAt;

            dto.UserAgent = entity.UserAgent;
            dto.Platform = entity.Platform;
            dto.PlatformVersion = entity.PlatformVersion;
            dto.Browser = entity.Browser;
            dto.BrowserVersion = entity.BrowserVersion;
            dto.Ip = entity.Ip;
            dto.Country = entity.Country;
            dto.City = entity.City;
            return dto;
        }
    }
}