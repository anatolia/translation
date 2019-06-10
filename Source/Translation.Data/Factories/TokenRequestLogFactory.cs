using Translation.Common.Models.DataTransferObjects;
using Translation.Data.Entities.Main;

namespace Translation.Data.Factories
{
    public class TokenRequestLogFactory
    {
        public TokenRequestLogDto CreateDtoFromEntity(TokenRequestLog entity)
        {
            var dto = new TokenRequestLogDto();
            dto.Uid = entity.Uid;
            dto.TokenUid = entity.TokenUid;
            dto.IntegrationClientUid = entity.IntegrationClientUid;
            dto.HttpMethod = entity.HttpMethod;

            dto.Ip = entity.Ip;
            dto.Country = entity.Country;
            dto.City = entity.City;
            dto.ResponseCode = entity.ResponseCode;

            dto.OrganizationUid = entity.OrganizationUid;
            dto.OrganizationName = entity.OrganizationName;

            dto.IntegrationUid = entity.IntegrationUid;
            dto.IntegrationName = entity.IntegrationName;

            return dto;
        }
    }
}