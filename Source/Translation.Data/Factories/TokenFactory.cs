using System;

using Translation.Common.Models.DataTransferObjects;
using Translation.Common.Models.Requests.Integration.Token;
using Translation.Data.Entities.Main;

namespace Translation.Data.Factories
{
    public class TokenFactory
    {
        public Token CreateEntityFromRequest(TokenCreateRequest request, IntegrationClient integrationClient)
        {
            var entity = CreateEntity(integrationClient);
            entity.Ip = request.IP.ToString();

            return entity;
        }

        public Token CreateEntity(IntegrationClient integrationClient)
        {
            var entity = new Token();

            entity.AccessToken = Guid.NewGuid();
            entity.ExpiresAt = entity.CreatedAt.AddMinutes(30);
            entity.IsActive = true;

            entity.IntegrationClientUid = integrationClient.Uid;
            entity.IntegrationClientId = integrationClient.Id;
            entity.IntegrationClientName = integrationClient.Name;

            entity.IntegrationUid = integrationClient.IntegrationUid;
            entity.IntegrationId = integrationClient.IntegrationId;
            entity.IntegrationName = integrationClient.IntegrationName;

            entity.OrganizationUid = integrationClient.OrganizationUid;
            entity.OrganizationId = integrationClient.OrganizationId;
            entity.OrganizationName = integrationClient.OrganizationName;

            return entity;
        }

        public TokenDto CreateDtoFromEntity(Token entity)
        {
            var dto = new TokenDto();
            dto.Uid = entity.Uid;
            dto.IntegrationClientUid = entity.IntegrationClientUid;
            dto.AccessToken = entity.AccessToken;
            dto.CreatedAt = entity.CreatedAt;
            dto.ExpiresAt = entity.ExpiresAt;

            return dto;
        }
    }
}