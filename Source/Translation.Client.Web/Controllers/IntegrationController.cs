using System;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using StandardUtils.Helpers;
using StandardUtils.Models.Shared;

using Translation.Client.Web.Helpers;
using Translation.Client.Web.Helpers.ActionFilters;
using Translation.Client.Web.Helpers.Mappers;
using Translation.Client.Web.Models.Base;
using Translation.Client.Web.Models.Integration;
using Translation.Common.Contracts;
using Translation.Common.Models.DataTransferObjects;
using Translation.Common.Models.Requests.Integration;
using Translation.Common.Models.Requests.Integration.IntegrationClient;
using Translation.Common.Models.Requests.Integration.Token;
using Translation.Common.Models.Requests.Organization;

namespace Translation.Client.Web.Controllers
{
    public class IntegrationController : BaseController
    {
        private readonly IIntegrationService _integrationService;
        private readonly IntegrationMapper _integrationMapper;

        public IntegrationController(IOrganizationService organizationService,
                                     IJournalService journalService,
                                     ILanguageService languageService,
                                     ITranslationProviderService translationProviderService,
                                     IIntegrationService integrationService,
                                     IntegrationMapper integrationMapper) : base(organizationService, journalService, languageService, translationProviderService)
        {
            _integrationService = integrationService;
            _integrationMapper = integrationMapper;
        }

        [HttpGet]
        public IActionResult Create(Guid id)
        {
            var organizationUid = id;
            if (organizationUid.IsEmptyGuid())
            {
                organizationUid = CurrentUser.OrganizationUid;
            }

            var request = new OrganizationReadRequest(CurrentUser.Id, organizationUid);
            var response = OrganizationService.GetOrganization(request);
            if (response.Status.IsNotSuccess)
            {
                return RedirectToAccessDenied();
            }

            var model = _integrationMapper.MapIntegrationCreateModel(organizationUid);
            return View(model);
        }

        [HttpPost,
         JournalFilter(Message = "journal_integration_create")]
        public async Task<IActionResult> Create(IntegrationCreateModel model)
        {
            if (model.IsNotValid())
            {
                model.SetInputModelValues();
                return View(model);
            }

            var request = new IntegrationCreateRequest(CurrentUser.Id, model.OrganizationUid, model.Name, model.Description);
            var response = await _integrationService.CreateIntegration(request);
            if (response.Status.IsNotSuccess)
            {
                model.MapMessages(
                    response);
                model.SetInputModelValues();
                return View(model);
            }

            CurrentUser.IsActionSucceed = true;
            return Redirect($"/Integration/Detail/{response.Item.Uid}");
        }

        [HttpGet]
        public async Task<IActionResult> Detail(Guid id)
        {
            var integrationUid = id;
            if (integrationUid.IsEmptyGuid())
            {
                return RedirectToAccessDenied();
            }

            var request = new IntegrationReadRequest(CurrentUser.Id, integrationUid);
            var response = await _integrationService.GetIntegration(request);
            if (response.Status.IsNotSuccess)
            {
                return RedirectToAccessDenied();
            }

            var model = _integrationMapper.MapIntegrationDetailModel(response.Item);
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var integrationUid = id;
            if (integrationUid.IsEmptyGuid())
            {
                return RedirectToAccessDenied();
            }

            var request = new IntegrationReadRequest(CurrentUser.Id, integrationUid);
            var response = await _integrationService.GetIntegration(request);
            if (response.Status.IsNotSuccess)
            {
                return RedirectToAccessDenied();
            }

            var model = _integrationMapper.MapIntegrationEditModel(response.Item);
            return View(model);
        }

        [HttpPost,
         JournalFilter(Message = "journal_integration_edit")]
        public async Task<IActionResult> Edit(IntegrationEditModel model)
        {
            if (model.IsNotValid())
            {
                model.SetInputModelValues();
                return View(model);
            }

            var request = new IntegrationEditRequest(CurrentUser.Id, model.IntegrationUid, model.Name, model.Description);
            var response = await _integrationService.EditIntegration(request);
            if (response.Status.IsNotSuccess)
            {
                model.MapMessages(
                    response);
                model.SetInputModelValues();
                return View(model);
            }

            CurrentUser.IsActionSucceed = true;
            return Redirect($"/Integration/Detail/{response.Item.Uid}");
        }

        [HttpPost,
         JournalFilter(Message = "journal_integration_delete")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var projectUid = id;
            if (projectUid.IsEmptyGuid())
            {
                return Json(new CommonResult { IsOk = false });
            }

            var request = new IntegrationDeleteRequest(CurrentUser.Id, projectUid);
            var response = await _integrationService.DeleteIntegration(request);
            if (response.Status.IsNotSuccess)
            {
                return Json(new CommonResult { IsOk = false, Messages = response.ErrorMessages });
            }

            CurrentUser.IsActionSucceed = true;
            return Json(new CommonResult { IsOk = true });
        }

        [HttpGet]
        public async Task<IActionResult> Revisions(Guid id)
        {
            var integrationUid = id;
            if (integrationUid.IsEmptyGuid())
            {
                return RedirectToHome();
            }

            var model = new IntegrationRevisionReadListModel();
            if (integrationUid.IsNotEmptyGuid())
            {
                var request = new IntegrationReadRequest(CurrentUser.Id, integrationUid);
                var response = await _integrationService.GetIntegration(request);
                if (response.Status.IsNotSuccess)
                {
                    return NotFound();
                }

                model.IntegrationUid = integrationUid;
                model.IntegrationName = response.Item.Name;
                model.SetInputModelValues();
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> RevisionsData(Guid id)
        {
            var integrationUid = id;
            if (integrationUid.IsEmptyGuid())
            {
                return NotFound();
            }

            var request = new IntegrationRevisionReadListRequest(CurrentUser.Id, integrationUid);

            var response = await _integrationService.GetIntegrationRevisions(request);
            if (response.Status.IsNotSuccess)
            {
                return NotFound();
            }

            var result = new DataResult();
            result.AddHeaders("revision", "revisioned_by", "revisioned_at", "integration_name", "is_active", "created_at", "");

            for (var i = 0; i < response.Items.Count; i++)
            {
                var revisionItem = response.Items[i];
                var item = revisionItem.Item;
                var stringBuilder = new StringBuilder();
                stringBuilder.Append($"{item.Uid}{DataResult.SEPARATOR}");
                stringBuilder.Append($"{revisionItem.Revision}{DataResult.SEPARATOR}");
                stringBuilder.Append($"{revisionItem.RevisionedByName}{DataResult.SEPARATOR}");
                stringBuilder.Append($"{GetDateTimeAsString(revisionItem.RevisionedAt)}{DataResult.SEPARATOR}");
                stringBuilder.Append($"{result.PrepareLink($"/Integration/Detail/{item.Uid}", item.Name)}{DataResult.SEPARATOR}");
                stringBuilder.Append($"{item.IsActive}{DataResult.SEPARATOR}");
                stringBuilder.Append($"{GetDateTimeAsString(item.CreatedAt)}{DataResult.SEPARATOR}");
                stringBuilder.Append($"{result.PrepareRestoreButton("restore", "/Integration/Restore/", "/Integration/Detail")}{DataResult.SEPARATOR}");

                result.Data.Add(stringBuilder.ToString());
            }

            return Json(result);
        }

        [HttpPost,
         JournalFilter(Message = "journal_integration_restore")]
        public async Task<IActionResult> Restore(Guid id, int revision)
        {
            var model = new CommonResult { IsOk = false };

            var integrationUid = id;
            if (integrationUid.IsEmptyGuid())
            {
                return Json(model);
            }

            if (revision < 1)
            {
                return Json(model);
            }

            var request = new IntegrationRestoreRequest(CurrentUser.Id, integrationUid, revision);
            var response = await _integrationService.RestoreIntegration(request);
            if (response.Status.IsNotSuccess)
            {
                model.Messages = response.ErrorMessages;
                return Json(model);
            }

            model.IsOk = true;
            CurrentUser.IsActionSucceed = true;
            return Json(model);
        }

        [HttpGet]
        public async Task<IActionResult> ClientListData(Guid id, int skip, int take)
        {
            var integrationUid = id;
            if (integrationUid.IsEmptyGuid())
            {
                return Forbid();
            }

            var request = new IntegrationClientReadListRequest(CurrentUser.Id, integrationUid);
            SetPaging(skip, take, request);
            var response = await _integrationService.GetIntegrationClients(request);
            if (response.Status.IsNotSuccess)
            {
                return NotFound();
            }

            var result = new DataResult();
            result.AddHeaders("client_id", "client_secret", "is_active", "");

            for (var i = 0; i < response.Items.Count; i++)
            {
                var item = response.Items[i];
                var integrationClientRow = GetIntegrationClientRow(item, result);
                result.Data.Add(integrationClientRow);
            }

            result.PagingInfo = response.PagingInfo;
            result.PagingInfo.PagingType = PagingInfo.PAGE_NUMBERS;

            return Json(result);
        }

        private static string GetIntegrationClientRow(IntegrationClientDto item, DataResult result = null)
        {
            if (result == null)
            {
                result = new DataResult();
            }

            var stringBuilder = new StringBuilder();
            stringBuilder.Append($"{item.Uid}{DataResult.SEPARATOR}");
            stringBuilder.Append($"{item.ClientId}{DataResult.SEPARATOR}");
            stringBuilder.Append($"{item.ClientSecret}{DataResult.SEPARATOR}");
            stringBuilder.Append($"{item.IsActive.ToString().ToLower()}{DataResult.SEPARATOR}");

            stringBuilder.Append($"{result.PrepareLink($"/Integration/ClientActiveTokens/{item.Uid}", "active_tokens")}");
            stringBuilder.Append($"{result.PrepareChangeActivationButton("/Integration/ClientChangeActivation")}");
            stringBuilder.Append($"{result.PrepareDeleteButton("/Integration/ClientDelete")}{DataResult.SEPARATOR}");
            return stringBuilder.ToString();
        }

        [HttpPost,
         JournalFilter(Message = "journal_integration_client_create")]
        public async Task<IActionResult> ClientCreate(Guid id)
        {
            var integrationUid = id;
            if (integrationUid.IsEmptyGuid())
            {
                return Json(new CommonResult { IsOk = false });
            }

            var request = new IntegrationClientCreateRequest(CurrentUser.Id, integrationUid);
            var response = await _integrationService.CreateIntegrationClient(request);
            if (response.Status.IsNotSuccess)
            {
                return Json(new CommonResult { IsOk = false, Messages = response.ErrorMessages });
            }

            var integrationClientRow = GetIntegrationClientRow(response.Item);
            CurrentUser.IsActionSucceed = true;
            return Json(new CommonResult { IsOk = true, Item = integrationClientRow });
        }

        [HttpPost,
         JournalFilter(Message = "journal_integration_client_delete")]
        public async Task<IActionResult> ClientDelete(Guid id)
        {
            var integrationClientUid = id;
            if (integrationClientUid.IsEmptyGuid())
            {
                return RedirectToAccessDenied();
            }

            var request = new IntegrationClientDeleteRequest(CurrentUser.Id, integrationClientUid);
            var response = await _integrationService.DeleteIntegrationClient(request);
            if (response.Status.IsNotSuccess)
            {
                return Json(new CommonResult { IsOk = false, Messages = response.ErrorMessages });
            }

            CurrentUser.IsActionSucceed = true;
            return Json(new CommonResult { IsOk = true });
        }

        [HttpPost,
         JournalFilter(Message = "journal_integration_client_change_activation")]
        public async Task<IActionResult> ClientChangeActivation(Guid id)
        {
            var integrationClientUid = id;
            if (integrationClientUid.IsEmptyGuid())
            {
                return RedirectToAccessDenied();
            }

            var request = new IntegrationClientChangeActivationRequest(CurrentUser.Id, integrationClientUid);
            var response = await _integrationService.ChangeActivationForIntegrationClient(request);
            if (response.Status.IsNotSuccess)
            {
                return Json(new CommonResult { IsOk = false, Messages = response.ErrorMessages });
            }

            CurrentUser.IsActionSucceed = true;
            return Json(new CommonResult { IsOk = true });
        }

        [HttpPost,
         JournalFilter(Message = "journal_integration_client_refresh")]
        public async Task<IActionResult> ClientRefresh(Guid id)
        {
            var model = new CommonResult { IsOk = false };

            var integrationClientUid = id;
            if (integrationClientUid.IsEmptyGuid())
            {
                return Json(model);
            }

            var request = new IntegrationClientRefreshRequest(CurrentUser.Id, integrationClientUid);
            var response = await _integrationService.RefreshIntegrationClient(request);
            if (response.Status.IsNotSuccess)
            {
                return Json(model);
            }

            model.IsOk = true;
            CurrentUser.IsActionSucceed = true;
            return Json(model);
        }

        [HttpGet]
        public async Task<IActionResult> ClientActiveTokens(Guid id)
        {
            var integrationClientUid = id;
            if (integrationClientUid.IsEmptyGuid())
            {
                return RedirectToAccessDenied();
            }

            var request = new IntegrationClientReadRequest(CurrentUser.Id, integrationClientUid);
            var response = await _integrationService.GetIntegrationClient(request);
            if (response.Status.IsNotSuccess)
            {
                return RedirectToAccessDenied();
            }

            var model = _integrationMapper.MapIntegrationClientActiveTokensModel(response.Item);
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> ClientActiveTokensData(Guid id, int skip, int take)
        {
            var integrationUid = id;
            if (integrationUid.IsEmptyGuid())
            {
                return Forbid();
            }

            var request = new IntegrationClientActiveTokenReadListRequest(CurrentUser.Id, integrationUid);
            SetPaging(skip, take, request);

            var response = await _integrationService.GetActiveTokensOfIntegrationClient(request);
            if (response.Status.IsNotSuccess)
            {
                return NotFound();
            }

            var result = new DataResult();
            result.AddHeaders("access_token", "ip", "created_at", "expires_at", "");

            for (var i = 0; i < response.Items.Count; i++)
            {
                var item = response.Items[i];
                var stringBuilder = new StringBuilder();
                stringBuilder.Append($"{item.Uid}{DataResult.SEPARATOR}");
                stringBuilder.Append($"{item.AccessToken}{DataResult.SEPARATOR}");
                stringBuilder.Append($"{item.IP}{DataResult.SEPARATOR}");
                stringBuilder.Append($"{GetDateTimeAsString(item.ExpiresAt)}{DataResult.SEPARATOR}");
                stringBuilder.Append($"{GetDateTimeAsString(item.CreatedAt)}{DataResult.SEPARATOR}");

                result.Data.Add(stringBuilder.ToString());
            }

            result.PagingInfo = response.PagingInfo;
            result.PagingInfo.PagingType = PagingInfo.PAGE_NUMBERS;

            return Json(result);
        }

        [HttpGet]
        public async Task<IActionResult> ActiveTokens(Guid id)
        {
            var integrationUid = id;
            if (integrationUid.IsEmptyGuid())
            {
                return RedirectToAccessDenied();
            }

            var request = new IntegrationReadRequest(CurrentUser.Id, integrationUid);
            var response = await _integrationService.GetIntegration(request);
            if (response.Status.IsNotSuccess)
            {
                return RedirectToAccessDenied();
            }

            var model = _integrationMapper.MapIntegrationActiveTokensModel(response.Item);

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> ActiveTokensData(Guid id, int skip, int take)
        {
            var integrationUid = id;
            if (integrationUid.IsEmptyGuid())
            {
                return Forbid();
            }

            var request = new IntegrationActiveTokenReadListRequest(CurrentUser.Id, integrationUid);
            SetPaging(skip, take, request);

            var response = await _integrationService.GetActiveTokensOfIntegration(request);
            if (response.Status.IsNotSuccess)
            {
                return NotFound();
            }

            var result = new DataResult();
            result.AddHeaders("integration_client_uid", "access_token", "ip", "created_at", "expires_at", "");

            for (var i = 0; i < response.Items.Count; i++)
            {
                var item = response.Items[i];
                var stringBuilder = new StringBuilder();
                stringBuilder.Append($"{item.Uid}{DataResult.SEPARATOR}");
                stringBuilder.Append($"{item.IntegrationClientUid}{DataResult.SEPARATOR}");
                stringBuilder.Append($"{item.AccessToken}{DataResult.SEPARATOR}");
                stringBuilder.Append($"{item.IP}{DataResult.SEPARATOR}");
                stringBuilder.Append($"{GetDateTimeAsString(item.ExpiresAt)}{DataResult.SEPARATOR}");
                stringBuilder.Append($"{GetDateTimeAsString(item.CreatedAt)}{DataResult.SEPARATOR}");

                result.Data.Add(stringBuilder.ToString());
            }

            result.PagingInfo = response.PagingInfo;
            result.PagingInfo.PagingType = PagingInfo.PAGE_NUMBERS;

            return Json(result);
        }
    }
}
