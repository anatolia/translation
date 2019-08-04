using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Translation.Client.Web.Helpers.ActionFilters;
using Translation.Client.Web.Models.Base;
using Translation.Common.Contracts;
using Translation.Common.Helpers;
using Translation.Common.Models.Requests.Integration.Token;

namespace Translation.Client.Web.Controllers
{
    public class TokenController : BaseController
    {
        private readonly IIntegrationService _integrationService;

        public TokenController(IIntegrationService integrationService,
                               IOrganizationService organizationService,
                               IJournalService journalService) : base(organizationService, journalService)
        {
            _integrationService = integrationService;
        }

        [HttpPost,
         JournalFilter(Message = "journal_token_revoke")]
        public async Task<IActionResult> Revoke(Guid tokenUid, Guid clientUid)
        {
            if (tokenUid.IsEmptyGuid()
                || clientUid.IsEmptyGuid())
            {
                return RedirectToAccessDenied();
            }

            var request = new TokenRevokeRequest(CurrentUser.Id, tokenUid, clientUid);
            var response = await _integrationService.RevokeToken(request);
            if (response.Status.IsNotSuccess)
            {
                return RedirectToAccessDenied();
            }

            CurrentUser.IsActionSucceed = true;
            return Json(response.Status);
        }

        [HttpPost,
         AllowAnonymous, 
         IgnoreAntiforgeryToken]
        public async Task<IActionResult> Create(Guid clientId, Guid clientSecret)
        {
            var result = new CommonResult();

            if (clientId.IsEmptyGuid()
                || clientSecret.IsEmptyGuid())
            {
                result.Messages.Add("some parameters are missing! (token, projectUid, labelKey)");
                return Json(result);
            }

            var request = new TokenCreateRequest(clientId, clientSecret, HttpContext.Connection.RemoteIpAddress);
            var response = await _integrationService.CreateToken(request);
            if (response.Status.IsNotSuccess)
            {
                result.Messages = response.ErrorMessages;
                return StatusCode(401, result);
            }

            var model = new TokenResult
            {
                IsOk = true,
                Token = response.Item.AccessToken,
                CreatedAt = GetDateTimeAsString(response.Item.CreatedAt),
                ExpiresAt = GetDateTimeAsString(response.Item.ExpiresAt)
            };

            return Json(model);
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = new CommonResult();

            if (CurrentUser == null)
            {
                return StatusCode(401, result);
            }
            
            var request = new TokenGetRequest(CurrentUser.Id);
            var response = await _integrationService.CreateTokenWhenUserAuthenticated(request);
            if (response.Status.IsNotSuccess)
            {
                result.Messages = response.ErrorMessages;
                return StatusCode(401, result);
            }

            var model = new TokenResult
            {
                Token = response.Item.AccessToken,
                CreatedAt = GetDateTimeAsString(response.Item.CreatedAt),
                ExpiresAt = GetDateTimeAsString(response.Item.ExpiresAt)
            };

            return Json(model);
        }
    }
}