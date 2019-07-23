using System;

using Translation.Common.Models.DataTransferObjects;
using Translation.Data.Entities.Main;

namespace Translation.Data.Factories
{
    public class IntegrationClientFactory
    {
        public IntegrationClient CreateEntity(Integration integration)
        {
            var entity = new IntegrationClient();
            entity.OrganizationId = integration.OrganizationId;
            entity.OrganizationUid = integration.OrganizationUid;
            entity.OrganizationName = integration.OrganizationName;
            entity.IntegrationId = integration.Id;
            entity.IntegrationUid = integration.Uid;
            entity.IntegrationName = integration.Name;
            entity.ClientId = Guid.NewGuid();
            entity.ClientSecret = Guid.NewGuid();
            
            entity.IsActive = true;

            return entity;
        }

        public IntegrationClient UpdateEntityForChangeActivation(IntegrationClient entity)
        {
            entity.IsActive = !entity.IsActive;
            return entity;
        }

        public IntegrationClientDto CreateDtoFromEntity(IntegrationClient entity)
        {
            var dto = new IntegrationClientDto();
            dto.OrganizationUid = entity.OrganizationUid;
            dto.OrganizationName = entity.OrganizationName;
            dto.IntegrationUid = entity.IntegrationUid;
            dto.IntegrationName = entity.IntegrationName;
            dto.Uid = entity.Uid;
            dto.Name = entity.Name;
            dto.IsActive = entity.IsActive;
            dto.ClientSecret = entity.ClientSecret;
            dto.ClientId = entity.ClientId;

            return dto;
        }

        public IntegrationClient UpdateEntityForRefresh(IntegrationClient entity)
        {
            entity.ClientId = Guid.NewGuid();
            entity.ClientSecret = Guid.NewGuid();

            return entity;
        }
    }
}