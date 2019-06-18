using Translation.Common.Contracts;

namespace Translation.Client.Web.Controllers
{
    public class WordController : BaseController
    {
        public WordController(IOrganizationService organizationService, IJournalService journalService) : base(organizationService, journalService)
        {
        }
    }
}
