using Translation.Common.Contracts;

namespace Translation.Client.Web.Controllers
{
    public class WordController : BaseController
    {
        public WordController(IOrganizationService organizationService, IJournalService journalService, ILanguageService languageService, ITranslationProviderService translationProviderService) : base(organizationService, journalService, languageService, translationProviderService)
        {
        }
    }
}
