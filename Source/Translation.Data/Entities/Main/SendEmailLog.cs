using System;
using StandardRepository.Models.Entities;
using StandardRepository.Models.Entities.Schemas;

namespace Translation.Data.Entities.Main
{
    public class SendEmailLog : BaseEntity, ISchemaMain
    {
        public long OrganizationId { get; set; }
        public Guid OrganizationUid { get; set; }
        public string OrganizationName { get; set; }

        public Guid MailUid { get; set; }

        public string Subject { get; set; }
        public string EmailFrom { get; set; }
        public string EmailTo { get; set; }

        public bool IsOpened { get; set; }
    }
}