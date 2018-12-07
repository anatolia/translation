using Cheviri.Client.Web.Helpers;

namespace Cheviri.Client.Web.Models
{
    public class AccessDeniedModel : BaseModel
    {
        public AccessDeniedModel()
        {
            Title = Localizer.Localize("access_denied_title");
        }
    }
}