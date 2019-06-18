using System;
using Translation.Client.Web.Models.Base;

namespace Translation.Client.Web.Models.Organization
{
    public sealed class OrganizationJournalListModel : BaseModel
    {
        public Guid OrganizationUid { get; set; }

        public OrganizationJournalListModel()
        {
            Title = "journal_list_title";
        }
    }
}