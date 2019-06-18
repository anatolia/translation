using Translation.Client.Web.Helpers;
using Translation.Client.Web.Models.Base;

namespace Translation.Client.Web.Models.Admin
{
    public sealed class AdminAcceptInviteDoneModel : BaseModel
    {
        public AdminAcceptInviteDoneModel()
        {
            Title = Localizer.Localize("admin_accept_invite_done_tittle");
        }
    }
}