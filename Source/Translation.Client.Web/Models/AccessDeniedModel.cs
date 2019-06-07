using Translation.Client.Web.Helpers;

namespace Translation.Client.Web.Models
{
    public class AccessDeniedModel : BaseModel
    {
        public AccessDeniedModel()
        {
            Title = Localizer.Localize("access_denied_title");
        }
    }
}