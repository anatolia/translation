using Translation.Common.Models.DataTransferObjects;
using Translation.Common.Models.Requests.Journal;
using Translation.Common.Models.Shared;
using Translation.Data.Entities.Main;

namespace Translation.Data.Factories
{
    public class JournalFactory
    {
        public JournalDto CreateDtoFromEntity(Journal entity)
        {
            var dto = new JournalDto();
            dto.OrganizationUid = entity.OrganizationUid;
            dto.OrganizationName = entity.OrganizationName;

            if (entity.IntegrationUid.HasValue)
            {
                dto.IntegrationUid = entity.IntegrationUid.Value;
                dto.IntegrationName = entity.IntegrationName;
            }

            if (entity.UserUid.HasValue)
            {
                dto.UserUid = entity.UserUid.Value;
                dto.UserName = entity.UserName;
            }

            dto.Message = entity.Message;
            dto.CreatedAt = entity.CreatedAt;
            return dto;
        }

        public Journal CreateEntityFromRequest(JournalCreateRequest request, CurrentUser currentUser)
        {
            var entity = new Journal();

            entity.UserId = currentUser.Id;
            entity.UserUid = currentUser.Uid;
            entity.UserName = currentUser.Name;

            entity.OrganizationId = currentUser.Organization.Id;
            entity.OrganizationUid = currentUser.Organization.Uid;
            entity.OrganizationName = currentUser.Organization.Name;

            entity.Message = request.Message;

            return entity;
        }
    }
}