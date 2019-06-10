using Translation.Common.Models.DataTransferObjects;
using Translation.Data.Entities.Main;

namespace Translation.Data.Factories
{
    public class SendEmailLogFactory
    {
        public SendEmailLogDto CreateDtoFromEntity(SendEmailLog entity)
        {
            var dto = new SendEmailLogDto();
            dto.OrganizationUid = entity.OrganizationUid;
            dto.OrganizationName = entity.OrganizationName;

            dto.MailUid = entity.MailUid;

            dto.Subject = entity.Subject;
            dto.EmailFrom = entity.EmailFrom;
            dto.EmailTo = entity.EmailTo;

            dto.IsOpened = entity.IsOpened;

            return dto;
        }
    }
}