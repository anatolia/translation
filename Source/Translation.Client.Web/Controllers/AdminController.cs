using System;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StandardUtils.Helpers;
using StandardUtils.Models.Shared;
using Translation.Client.Web.Helpers;
using Translation.Client.Web.Helpers.ActionFilters;
using Translation.Client.Web.Helpers.Mappers;
using Translation.Client.Web.Models.Admin;
using Translation.Client.Web.Models.Base;
using Translation.Client.Web.Models.Organization;
using Translation.Client.Web.Models.User;
using Translation.Common.Contracts;
using Translation.Common.Models.Requests.Admin;
using Translation.Common.Models.Requests.Integration.Token;
using Translation.Common.Models.Requests.Journal;
using Translation.Common.Models.Requests.Organization;
using Translation.Common.Models.Requests.SendEmailLog;
using Translation.Common.Models.Requests.User;
using Translation.Common.Models.Requests.User.LoginLog;
using Translation.Common.Models.Responses.Admin;

namespace Translation.Client.Web.Controllers
{
    public class AdminController : BaseController
    {
        private readonly IAdminService _adminService;
        private readonly AdminMapper _adminMapper;

        public AdminController(IOrganizationService organizationService,
                               IJournalService journalService,
                               ILanguageService languageService,
                               ITranslationProviderService translationProviderService,
                               IAdminService adminService,
                               AdminMapper adminMapper) : base(organizationService, journalService, languageService, translationProviderService)
        {
            _adminService = adminService;
            _adminMapper = adminMapper;
        }

        [HttpGet]
        public IActionResult Dashboard()
        {
            var model = new AdminDashboardBaseModel();
            return View(model);
        }

        [HttpGet]
        public IActionResult List()
        {
            var model = new AdminListBaseModel();
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> ListData(int skip, int take)
        {
            var request = new SuperAdminUserReadListRequest(CurrentUser.Id);
            SetPaging(skip, take, request);

            var response = await _adminService.GetSuperAdmins(request);
            if (response.Status.IsNotSuccess)
            {
                return NotFound();
            }

            var result = new DataResult();
            result.AddHeaders("user_name", "is_active", "");

            for (var i = 0; i < response.Items.Count; i++)
            {
                var item = response.Items[i];
                var stringBuilder = new StringBuilder();
                stringBuilder.Append($"{item.Uid}{DataResult.SEPARATOR}");
                stringBuilder.Append($"{result.PrepareLink($"/User/Detail/{item.Uid}", item.FirstName)}{DataResult.SEPARATOR}");
                stringBuilder.Append($"{item.IsActive}{DataResult.SEPARATOR}");
                stringBuilder.Append($"{result.PrepareChangeActivationButton("/Admin/ChangeActivation/")}");
                stringBuilder.Append($"{result.PrepareButton("degrade_to_user", "handleDegradeToUser(this, \"/Admin/DegradeToUser/\")", "btn-secondary", "are_you_sure_you_want_to_degrade_to_user_title", "are_you_sure_you_want_to_degrade_to_user_content")}{DataResult.SEPARATOR}");

                result.Data.Add(stringBuilder.ToString());
            }

            result.PagingInfo = response.PagingInfo;
            result.PagingInfo.PagingType = PagingInfo.PAGE_NUMBERS;

            return Json(result);
        }

        [HttpGet]
        public IActionResult OrganizationList()
        {
            var model = new OrganizationListModel();
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> OrganizationListData(int skip, int take)
        {
            var request = new OrganizationReadListRequest(CurrentUser.Id);
            SetPaging(skip, take, request);

            var response = await OrganizationService.GetOrganizations(request);
            if (response.Status.IsNotSuccess)
            {
                return NotFound();
            }

            var result = new DataResult();
            result.AddHeaders("organization_name", "user_count", "project_count", "label_count", "label_translation_count", "is_active", "");

            for (var i = 0; i < response.Items.Count; i++)
            {
                var item = response.Items[i];
                var stringBuilder = new StringBuilder();
                stringBuilder.Append($"{item.Uid}{DataResult.SEPARATOR}");
                stringBuilder.Append($"{result.PrepareLink($"/Organization/Detail/{item.Uid}", item.Name, true)}{DataResult.SEPARATOR}");
                stringBuilder.Append($"{item.UserCount}{DataResult.SEPARATOR}");
                stringBuilder.Append($"{item.ProjectCount}{DataResult.SEPARATOR}");
                stringBuilder.Append($"{item.LabelCount}{DataResult.SEPARATOR}");
                stringBuilder.Append($"{item.LabelTranslationCount}{DataResult.SEPARATOR}");
                stringBuilder.Append($"{item.IsActive}{DataResult.SEPARATOR}");
                stringBuilder.Append($"{result.PrepareChangeActivationButton("/Admin/OrganizationChangeActivation/")}{DataResult.SEPARATOR}");

                result.Data.Add(stringBuilder.ToString());
            }

            result.PagingInfo = response.PagingInfo;
            result.PagingInfo.PagingType = PagingInfo.PAGE_NUMBERS;

            return Json(result);
        }

        [HttpGet]
        public IActionResult UserList()
        {
            var model = new AllUserListModel();
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> UserListData(int skip, int take)
        {
            var request = new AllUserReadListRequest(CurrentUser.Id);
            SetPaging(skip, take, request);

            var response = await _adminService.GetAllUsers(request);
            if (response.Status.IsNotSuccess)
            {
                return NotFound();
            }

            var result = new DataResult();
            result.AddHeaders("organization_name", "user_name", "is_active", "");

            for (var i = 0; i < response.Items.Count; i++)
            {
                var item = response.Items[i];
                var stringBuilder = new StringBuilder();
                stringBuilder.Append($"{item.Uid}{DataResult.SEPARATOR}");
                stringBuilder.Append($"{result.PrepareLink($"/Organization/Detail/{item.OrganizationUid}", item.OrganizationName)}{DataResult.SEPARATOR}");
                stringBuilder.Append($"{result.PrepareLink($"/User/Detail/{item.Uid}", item.Name)}{DataResult.SEPARATOR}");
                stringBuilder.Append($"{item.IsActive}{DataResult.SEPARATOR}");
                stringBuilder.Append($"{result.PrepareChangeActivationButton("/Admin/ChangeActivation/")}");
                stringBuilder.Append($"{result.PrepareButton("upgrade_to_admin", "handleUpgradeToAdmin(this, \"/Admin/UserUpgradeToAdmin/\")", "btn-secondary", "are_you_sure_you_want_to_upgrade_to_admin_title", "are_you_sure_you_want_to_upgrade_to_admin_content")}{DataResult.SEPARATOR}");

                result.Data.Add(stringBuilder.ToString());
            }

            result.PagingInfo = response.PagingInfo;
            result.PagingInfo.PagingType = PagingInfo.PAGE_NUMBERS;

            return Json(result);
        }

        [HttpGet]
        public IActionResult UserLoginLogList()
        {
            var model = new UserLoginLogListModel();
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> UserLoginLogListData(int skip, int take)
        {
            var request = new AllLoginLogReadListRequest(CurrentUser.Id);
            SetPaging(skip, take, request);

            var response = await _adminService.GetAllUserLoginLogs(request);
            if (response.Status.IsNotSuccess)
            {
                return NotFound();
            }

            var result = new DataResult();
            result.AddHeaders("organization", "user", "ip", "country", "city", "browser", "browser_version", "platform", "platform_version", "created_at");

            for (var i = 0; i < response.Items.Count; i++)
            {
                var item = response.Items[i];
                var stringBuilder = new StringBuilder();
                stringBuilder.Append($"{item.Uid}{DataResult.SEPARATOR}");
                stringBuilder.Append($"{result.PrepareLink($"/Organization/Detail/{item.OrganizationUid}", item.OrganizationName)}{DataResult.SEPARATOR}");
                stringBuilder.Append($"{result.PrepareLink($"/User/Detail/{item.Uid}", item.UserName)}{DataResult.SEPARATOR}");
                stringBuilder.Append($"{item.Ip}{DataResult.SEPARATOR}");
                stringBuilder.Append($"{item.Country}{DataResult.SEPARATOR}");
                stringBuilder.Append($"{item.City}{DataResult.SEPARATOR}");
                stringBuilder.Append($"{item.Browser}{DataResult.SEPARATOR}");
                stringBuilder.Append($"{item.BrowserVersion}{DataResult.SEPARATOR}");
                stringBuilder.Append($"{item.Platform}{DataResult.SEPARATOR}");
                stringBuilder.Append($"{item.PlatformVersion}{DataResult.SEPARATOR}");
                stringBuilder.Append($"{GetDateTimeAsString(item.CreatedAt)}{DataResult.SEPARATOR}");

                result.Data.Add(stringBuilder.ToString());
            }

            result.PagingInfo = response.PagingInfo;
            result.PagingInfo.PagingType = PagingInfo.PAGE_NUMBERS;

            return Json(result);
        }

        [HttpGet]
        public IActionResult JournalList()
        {
            var model = new JournalListModel();
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> JournalListData(int skip, int take)
        {
            var request = new AllJournalReadListRequest(CurrentUser.Id);
            SetPaging(skip, take, request);

            var response = await _adminService.GetJournals(request);
            if (response.Status.IsNotSuccess)
            {
                return NotFound();
            }

            var result = new DataResult();
            result.AddHeaders("organization_name", "user_name", "integration_name", "message", "created_at");

            for (var i = 0; i < response.Items.Count; i++)
            {
                var item = response.Items[i];
                var stringBuilder = new StringBuilder();
                stringBuilder.Append($"{item.Uid}{DataResult.SEPARATOR}");
                stringBuilder.Append($"{result.PrepareLink($"/Organization/Detail/{item.OrganizationUid}", item.OrganizationName)}{DataResult.SEPARATOR}");
                stringBuilder.Append($"{result.PrepareLink($"/User/Detail/{item.UserUid}", item.UserName)}{DataResult.SEPARATOR}");
                stringBuilder.Append($"{result.PrepareLink($"/Integration/Detail/{item.IntegrationUid}", item.IntegrationName)}{DataResult.SEPARATOR}");
                stringBuilder.Append($"{item.Message}{DataResult.SEPARATOR}");
                stringBuilder.Append($"{GetDateTimeAsString(item.CreatedAt)}{DataResult.SEPARATOR}");

                result.Data.Add(stringBuilder.ToString());
            }

            result.PagingInfo = response.PagingInfo;
            result.PagingInfo.PagingType = PagingInfo.PAGE_NUMBERS;

            return Json(result);
        }

        [HttpGet]
        public IActionResult TokenRequestLogList()
        {
            var model = new TokenRequestLogListModel();
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> TokenRequestLogListData(int skip, int take)
        {
            var request = new AllTokenRequestLogReadListRequest(CurrentUser.Id);
            SetPaging(skip, take, request);

            var response = await _adminService.GetTokenRequestLogs(request);
            if (response.Status.IsNotSuccess)
            {
                return NotFound();
            }

            var result = new DataResult();
            result.AddHeaders("organization_name", "user_name", "ip", "country", "city", "http_method", "response_code", "created_at");

            for (var i = 0; i < response.Items.Count; i++)
            {
                var item = response.Items[i];
                var stringBuilder = new StringBuilder();
                stringBuilder.Append($"{item.Uid}{DataResult.SEPARATOR}");
                stringBuilder.Append($"{result.PrepareLink($"/Organization/Detail/{item.OrganizationUid}", item.OrganizationName)}{DataResult.SEPARATOR}");
                stringBuilder.Append($"{result.PrepareLink($"/Integration/Detail/{item.IntegrationUid}", item.IntegrationName)}{DataResult.SEPARATOR}");
                stringBuilder.Append($"{item.Ip}{DataResult.SEPARATOR}");
                stringBuilder.Append($"{item.Country}{DataResult.SEPARATOR}");
                stringBuilder.Append($"{item.City}{DataResult.SEPARATOR}");
                stringBuilder.Append($"{item.HttpMethod}{DataResult.SEPARATOR}");
                stringBuilder.Append($"{item.ResponseCode}{DataResult.SEPARATOR}");
                stringBuilder.Append($"{GetDateTimeAsString(item.CreatedAt)}{DataResult.SEPARATOR}");

                result.Data.Add(stringBuilder.ToString());
            }

            result.PagingInfo = response.PagingInfo;
            result.PagingInfo.PagingType = PagingInfo.PAGE_NUMBERS;

            return Json(result);
        }

        [HttpGet]
        public IActionResult SendEmailLogList()
        {
            var model = new SendEmailLogListModel();
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> SendEmailLogListData(int skip, int take)
        {
            var request = new AllSendEmailLogReadListRequest(CurrentUser.Id);
            SetPaging(skip, take, request);

            var response = await _adminService.GetSendEmailLogs(request);
            if (response.Status.IsNotSuccess)
            {
                return NotFound();
            }

            var result = new DataResult();
            result.AddHeaders("organization_name", "mail_uid", "send_to", "subject", "send_at", "is_opened");

            for (var i = 0; i < response.Items.Count; i++)
            {
                var item = response.Items[i];
                var stringBuilder = new StringBuilder();
                stringBuilder.Append($"{item.Uid}{DataResult.SEPARATOR}");
                stringBuilder.Append($"{result.PrepareLink($"/Organization/Detail/{item.OrganizationUid}", item.OrganizationName)}{DataResult.SEPARATOR}");
                stringBuilder.Append($"{item.MailUid}{DataResult.SEPARATOR}");
                stringBuilder.Append($"{item.EmailTo}{DataResult.SEPARATOR}");
                stringBuilder.Append($"{item.Subject}{DataResult.SEPARATOR}");
                stringBuilder.Append($"{GetDateTimeAsString(item.CreatedAt)}{DataResult.SEPARATOR}");
                stringBuilder.Append($"{item.IsOpened}{DataResult.SEPARATOR}");

                result.Data.Add(stringBuilder.ToString());
            }

            result.PagingInfo = response.PagingInfo;
            result.PagingInfo.PagingType = PagingInfo.PAGE_NUMBERS;

            return Json(result);
        }

        [HttpGet]
        public IActionResult Invite()
        {
            var organizationUid = CurrentUser.OrganizationUid;
            var model = _adminMapper.MapAdminInviteModel(organizationUid);
            return View(model);
        }

        [HttpPost,
         JournalFilter(Message = "journal_invite_admin")]
        public async Task<IActionResult> Invite(AdminInviteModel model)
        {
            if (model.IsNotValid())
            {
                model.SetInputModelValues();
                return View(model);
            }

            var request = new AdminInviteRequest(CurrentUser.Id, model.OrganizationUid, model.Email, model.FirstName, model.LastName);
            var response = await _adminService.InviteSuperAdminUser(request);
            if (response.Status.IsNotSuccess)
            {
                model.MapMessages(response);
                model.SetInputModelValues();
                return View(model);
            }

            // todo : email gönderme senaryosu
            CurrentUser.IsActionSucceed = true;
            return Redirect("/Admin/InviteDone/");
        }

        [HttpGet]
        public ViewResult InviteDone()
        {
            var model = new AdminInviteDoneModel();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ChangeActivation(Guid id)
        {
            var adminUid = id;
            if (adminUid.IsEmptyGuid())
            {
                return Forbid();
            }

            var request = new UserChangeActivationRequest(CurrentUser.Id, adminUid);
            var response = await _adminService.ChangeActivation(request);
            if (response.Status.IsNotSuccess)
            {
                return Json(new CommonResult { IsOk = false, Messages = response.ErrorMessages });
            }

            return Json(new CommonResult { IsOk = true });
        }

        [HttpPost]
        public async Task<IActionResult> OrganizationChangeActivation(Guid id)
        {
            var organizationUid = id;
            if (organizationUid.IsEmptyGuid())
            {
                return Forbid();
            }

            var request = new OrganizationChangeActivationRequest(CurrentUser.Id, organizationUid);
            var response = await _adminService.OrganizationChangeActivation(request);
            if (response.Status.IsNotSuccess)
            {
                return Json(new CommonResult { IsOk = false, Messages = response.ErrorMessages });
            }

            return Json(new CommonResult { IsOk = true });
        }

        [HttpPost]
        public async Task<IActionResult> TranslationProviderChangeActivation(Guid id)
        {
            var translationproviderUid = id;
            if (translationproviderUid.IsEmptyGuid())
            {
                return Forbid();
            }

            var request = new TranslationProviderChangeActivationRequest(CurrentUser.Id, translationproviderUid);
            var response = await _adminService.TranslationProviderChangeActivation(request);
            if (response.Status.IsNotSuccess)
            {
                return Json(new CommonResult { IsOk = false, Messages = response.ErrorMessages });
            }

            return Json(new CommonResult { IsOk = true });
        }

        [HttpPost,
         JournalFilter(Message = "demote_to_user")]
        public async Task<IActionResult> DegradeToUser(Guid id)
        {
            var commonResult = new CommonResult { IsOk = false };

            var adminUid = id;
            if (adminUid.IsEmptyGuid())
            {
                return Json(commonResult);
            }

            var request = new AdminDemoteRequest(CurrentUser.Id, adminUid);
            var response = await _adminService.DemoteToUser(request);
            if (response.Status.IsNotSuccess)
            {
                commonResult.Messages = response.ErrorMessages;
                return Json(commonResult);
            }

            commonResult.IsOk = true;
            return Json(commonResult);
        }

        [HttpPost]
        public async Task<IActionResult> UserUpgradeToAdmin(Guid id)
        {
            var userUid = id;
            if (userUid.IsEmptyGuid())
            {
                return Forbid();
            }

            var request = new AdminUpgradeRequest(CurrentUser.Id, userUid);
            var response = await _adminService.UpgradeToAdmin(request);
            if (response.Status.IsNotSuccess)
            {
                return Json(new CommonResult { IsOk = false, Messages = response.ErrorMessages });
            }

            return Json(new CommonResult { IsOk = true });
        }

        [HttpGet, AllowAnonymous]
        public async Task<IActionResult> AcceptInvite(Guid token, string email)
        {
            if (email.IsNotEmail()
                || token.
                    IsEmptyGuid())
            {
                return RedirectToAccessDenied();
            }

            var request = new AdminInviteValidateRequest(token, email);
            var response = await _adminService.ValidateSuperAdminUserInvitation(request);
            if (response.Status.IsNotSuccess)
            {
                return RedirectToAccessDenied();
            }

            var model = _adminMapper.MapAdminAcceptInviteModel(response.Item, token, email);
            return View(model);
        }

        [HttpPost, AllowAnonymous]
        public async Task<IActionResult> AcceptInvite(AdminAcceptInviteModel model)
        {
            if (model.IsNotValid())
            {
                model.SetInputModelValues();
                return View(model);
            }

            var request = new AdminAcceptInviteRequest(model.Token, model.Email, model.FirstName, model.LastName, model.Password);
            var response = await _adminService.AcceptSuperAdminUserInvite(request);
            if (response.Status.IsNotSuccess)
            {
                model.MapMessages(response);
                model.SetInputModelValues();
                return View(model);
            }

            return Redirect("/Admin/AcceptInviteDone/");
        }

        [HttpGet, AllowAnonymous]
        public ViewResult AcceptInviteDone()
        {
            var model = new AdminAcceptInviteDoneModel();
            return View(model);
        }
    }
}
