using System;

using Translation.Client.Web.Models.Base;

namespace Translation.Client.Web.Models.Organization
{
    public class OrganizationPendingTranslationReadListModel : BaseModel
    {
        public Guid OrganizationUid { get; set; }
        public string OrganizationName { get; set; }

        public OrganizationPendingTranslationReadListModel()
        {
            Title = "organization_pending_translations_title";
        }
    }
}