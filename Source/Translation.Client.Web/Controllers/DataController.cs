using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Translation.Client.Web.Models.Base;
using Translation.Common.Contracts;
using Translation.Common.Helpers;
using Translation.Common.Models.Requests.Integration.Token;
using Translation.Common.Models.Requests.Label;

namespace Translation.Client.Web.Controllers
{
    public class DataController : Controller
    {
        private readonly IIntegrationService _integrationService;
        private readonly ILabelService _labelService;

        public DataController(IIntegrationService integrationService, ILabelService labelService)
        {
            _integrationService = integrationService;
            _labelService = labelService;
        }

        [HttpGet,
         AllowAnonymous,
         ResponseCache(Duration = 60, VaryByQueryKeys = new[] { "projectUid" })]
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

        [HttpPost,
         AllowAnonymous,
         IgnoreAntiforgeryToken]
        public async Task<IActionResult> AddLabel(Guid token, Guid projectUid, string labelKey)
        {
            var result = new CommonResult();

            if (token.IsEmptyGuid()
                || projectUid.IsEmptyGuid()
                || labelKey.IsEmpty())
            {
                result.Messages.Add("some parameters are missing! (token, projectUid, labelKey)");
                return Json(result);
            }

            var request = new TokenValidateRequest(projectUid, token);
            var response = await _integrationService.ValidateToken(request);
            if (response.Status.IsNotSuccess)
            {
                result.Messages = response.ErrorMessages;
                result.Messages.Add("unauthorized");
                return StatusCode(401, result);
            }

            var labelCreateWithTokenRequest = new LabelCreateWithTokenRequest(token, projectUid, labelKey);
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