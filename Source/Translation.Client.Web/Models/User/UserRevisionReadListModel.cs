using System;

using Translation.Client.Web.Models.Base;

namespace Translation.Client.Web.Models.User
{
    public class UserRevisionReadListModel : BaseModel
    {
        public string UserName { get; set; }
        public Guid UserUid { get; set; }

        public UserRevisionReadListModel()
        {
            Title = "user_revision_list_title";
        }
    }
}