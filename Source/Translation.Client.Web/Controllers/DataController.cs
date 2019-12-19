using System;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using StandardUtils.Helpers;

using Translation.Client.Web.Models.Base;
using Translation.Client.Web.Models.Data;
using Translation.Common.Contracts;
using Translation.Common.Models.Requests.Integration.Token;
using Translation.Common.Models.Requests.Label;

namespace Translation.Client.Web.Controllers
{
    public class DataController : BaseController
    {
        private readonly IIntegrationService _integrationService;
        private readonly ILabelService _labelService;

        public DataController(IOrganizationService organizationService,
                              IJournalService journalService,
                              ILanguageService languageService,
                              ITranslationProviderService translationProviderService,
                              IIntegrationService integrationService,
                              ILabelService labelService) : base(organizationService, journalService, languageService, translationProviderService)
        {
            _integrationService = integrationService;
            _labelService = labelService;
        }

        [HttpGet,
         AllowAnonymous]
        public async Task<IActionResult> GetLabels(Guid token, Guid projectUid)
        {
            var result = new CommonResult();

            if (token.IsEmptyGuid()
                || projectUid.IsEmptyGuid())
            {
                result.Messages.Add("token or projectUid is missing!");
                return StatusCode(401, result);
            }

            var labelReadListRequest = new AllLabelReadListRequest(token, projectUid);
            var request = new TokenValidateRequest(projectUid, token);
            var response = await _integrationService.ValidateToken(request);
            if (response.Status.IsNotSuccess)
            {
                result.Messages = response.ErrorMessages;
                result.Messages.Add("unauthorized");
                return StatusCode(401, result);
            }

            var labelsResponse = await _labelService.GetLabelsWithTranslations(labelReadListRequest);
            if (labelsResponse.Status.IsNotSuccess)
            {
                result.Messages = labelsResponse.ErrorMessages;
                return Json(result);
            }

            return Json(labelsResponse.Labels);
        }

        [HttpGet,
         AllowAnonymous,
         ResponseCache(Duration = 60)]
        public async Task<IActionResult> GetMainLabels()
        {
            var result = new CommonResult();

            var labelReadListRequest = new AllLabelReadListRequest();
            var labelsResponse = await _labelService.GetLabelsWithTranslations(labelReadListRequest);
            if (labelsResponse.Status.IsNotSuccess)
            {
                result.Messages = labelsResponse.ErrorMessages;
                return Json(result);
            }

            return Json(labelsResponse.Labels);
        }

        [HttpGet]
        public IActionResult GetCurrentUser()
        {
            if (CurrentUser == null)
            {
                return Json(null);
            }

            return Json(new { CurrentUser.Name, CurrentUser.LanguageCode });
        }

        [HttpPost,
         AllowAnonymous,
         IgnoreAntiforgeryToken]
        public async Task<IActionResult> AddLabel(DataAddLabelModel model)
        {
            var result = new CommonResult();

            if (model.Token.IsEmptyGuid()
                || model.ProjectUid.IsEmptyGuid()
                || model.LabelKey.IsEmpty()
                || model.LanguageIsoCode2s.IsEmpty())
            {
                result.Messages.Add("some parameters are missing! (token, projectUid, labelKey, languageIsoCode2s)");
                return Json(result);
            }

            var request = new TokenValidateRequest(model.ProjectUid, model.Token);
            var response = await _integrationService.ValidateToken(request);
            if (response.Status.IsNotSuccess)
            {
                result.Messages = response.ErrorMessages;
                result.Messages.Add("unauthorized");
                return StatusCode(401, result);
            }

            var languageIsoCode2s = model.LanguageIsoCode2s.Split(",", StringSplitOptions.RemoveEmptyEntries);

            var labelCreateWithTokenRequest = new LabelCreateWithTokenRequest(model.Token, model.ProjectUid, model.LabelKey, languageIsoCode2s);
            var labelsResponse = await _labelService.CreateLabel(labelCreateWithTokenRequest);

            if (labelsResponse.Status.IsNotSuccess)
            {
                response.ErrorMessages = labelsResponse.ErrorMessages;
                return Json(result);
            }

            result.IsOk = true;
            return Json(result);
        }
    }
}