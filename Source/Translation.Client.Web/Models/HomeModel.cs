using Translation.Client.Web.Models.Base;

namespace Translation.Client.Web.Models
{
    public class HomeModel : BaseModel
    {
        public bool IsSuperAdmin { get; set; }
        public bool IsAuthenticated { get; set; }

        public HomeModel()
        {
            Title = "welcome_to_translation_service";
        }
    }
}