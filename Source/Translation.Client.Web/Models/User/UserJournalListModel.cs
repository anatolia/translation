using System;

using Translation.Client.Web.Models.Base;

namespace Translation.Client.Web.Models.User
{
    public class UserJournalListModel : BaseModel
    {
        public Guid UserUid { get; set; }

        public UserJournalListModel()
        {
            Title = "user_journal_list_title";
        }
    }
}