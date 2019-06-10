using System;

using Translation.Common.Models.DataTransferObjects;
using Translation.Common.Models.Requests.Integration.IntegrationClient;
using Translation.Data.Entities.Main;

namespace Translation.Data.Factories
{
    public class IntegrationClientFactory
    {
        public IntegrationClient CreateEntityFromRequest(IntegrationClientCreateRequest request,
                                                                Integration integrationEntity)
        {
            var entity = new IntegrationClient();
            entity.OrganizationId = integrationEntity.OrganizationId;
            entity.OrganizationUid = integrationEntity.OrganizationUid;
            entity.OrganizationName = integrationEntity.OrganizationName;
            entity.IntegrationId = integrationEntity.Id;
            entity.IntegrationUid = integrationEntity.Uid;
            entity.IntegrationName = integrationEntity.Name;
            entity.ClientId = Guid.NewGuid();
            entity.ClientSecret = Guid.NewGuid();
            entity.CreatedBy = request.CurrentUserId;
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

        public void UpdateEntityForRefresh(IntegrationClient entity)
        {
            entity.ClientId = Guid.NewGuid();
            entity.ClientSecret = Guid.NewGuid();
        }
    }
}