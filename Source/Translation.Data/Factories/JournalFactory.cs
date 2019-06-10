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

            dto.IntegrationName = entity.IntegrationName;

            dto.UserUid = entity.UserUid;
            dto.UserName = entity.UserName;

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