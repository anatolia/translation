using Translation.Common.Models.DataTransferObjects;
using Translation.Common.Models.Requests.Integration;
using Translation.Common.Models.Shared;
using Translation.Data.Entities.Main;

namespace Translation.Data.Factories
{
    public class IntegrationFactory
    {
        public Integration CreateEntityFromRequest(IntegrationCreateRequest request, Organization organizationEntity)
        {
            var entity = new Integration();
            entity.OrganizationId = organizationEntity.Id;
            entity.OrganizationUid = organizationEntity.Uid;
            entity.OrganizationName = organizationEntity.Name;
            entity.Name = request.Name;
            entity.Description = request.Description;
            entity.IsActive = true;

            return entity;
        }

        public Integration CreateEntityFromRequest(IntegrationCreateRequest request, CurrentOrganization organizationEntity)
        {
            var entity = new Integration();
            entity.OrganizationId = organizationEntity.Id;
            entity.OrganizationUid = organizationEntity.Uid;
            entity.OrganizationName = organizationEntity.Name;
            entity.Name = request.Name;
            entity.Description = request.Description;
            entity.IsActive = true;

            return entity;
        }

        public Integration UpdateEntityForChangeActivation(Integration entity)
        {
            entity.IsActive = !entity.IsActive;
            return entity;
        }

        public Integration CreateEntityFromRequest(IntegrationEditRequest request, Integration entity)
        {
            entity.Name = request.Name;
            entity.Description = request.Description;

            return entity;
        }

        public IntegrationDto CreateDtoFromEntity(Integration entity)
        {
            var dto = new IntegrationDto();
            dto.OrganizationUid = entity.OrganizationUid;
            dto.OrganizationName = entity.OrganizationName;
            dto.Uid = entity.Uid;
            dto.Name = entity.Name;
            dto.Description = entity.Description;
            dto.IsActive = entity.IsActive;
            dto.CreatedAt = entity.CreatedAt;

            return dto;
        }

        public Integration CreateDefault(Organization organization)
        {
            var entity = new Integration();
            entity.OrganizationId = organization.Id;
            entity.OrganizationUid = organization.Uid;
            entity.OrganizationName = organization.Name;
            entity.Name = "Default";
            entity.IsActive = true;

            return entity;
        }
    }
}