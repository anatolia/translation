using System;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

using StandardUtils.Helpers;
using StandardUtils.Models.Shared;

using Translation.Client.Web.Helpers;
using Translation.Client.Web.Helpers.ActionFilters;
using Translation.Client.Web.Helpers.Mappers;
using Translation.Client.Web.Models.Base;
using Translation.Client.Web.Models.User;
using Translation.Common.Contracts;
using Translation.Common.Models.Requests.Journal;
using Translation.Common.Models.Requests.Organization;
using Translation.Common.Models.Requests.User;

namespace Translation.Client.Web.Controllers
{
    public class UserController : BaseController
    {
        private readonly UserMapper _userMapper;

        public UserController(IOrganizationService organizationService, 
                              IJournalService journalService, 
                              ILanguageService languageService, 
                              ITranslationProviderService translationProviderService,
                              UserMapper userMapper) : base(organizationService, journalService, languageService, translationProviderService)
        {
            _userMapper = userMapper;
        }

        [HttpGet, AllowAnonymous]
        public IActionResult SignUp()
        {
            if (User.Identity.IsAuthenticated)
            {
                return Redirect("/User/Detail");
            }

            var model = new SignUpModel();
            return View(model);
        }

        [HttpPost, AllowAnonymous]
        public async Task<IActionResult> SignUp(SignUpModel model)
        {
            if (model.IsNotValid())
            {
                model.SetInputModelValues();
                return View(model);
            }

            var clientLog = GetClientInfoLog();
            var request = new SignUpRequest(model.OrganizationName, model.FirstName, model.LastName,
                                            model.Email, model.Password, clientLog, model.LanguageUid);
            var response = await OrganizationService.CreateOrganizationWithAdmin(request);
            if (response.Status.IsNotSuccess)
            {
                model.MapMessages(response);
                model.SetInputModelValues();
                return View(model);
            }

            await HttpContext.SignInWithClaims(model.FirstName + " " + model.LastName, model.Email);

            return RedirectToHome();
        }

        [HttpGet, AllowAnonymous]
        public async Task<IActionResult> ValidateEmailDone(string email, Guid token)
        {
            if (email.IsNotEmail()
                || token.IsEmptyGuid())
            {
                return RedirectToAccessDenied();
            }

            var request = new ValidateEmailRequest(token, email);
            var response = await OrganizationService.ValidateEmail(request);
            if (response.Status.IsNotSuccess)
            {
                return RedirectToAccessDenied();
            }

            var model = new ValidateEmailDoneModel();

            return View(model);
        }

        [HttpGet, AllowAnonymous]
        public ViewResult LogOn(string returnUrl = null)
        {
            var model = new LogOnModel();
            model.RedirectUrl = returnUrl;

            return View(model);
        }

        [HttpPost,
         AllowAnonymous]
        public async Task<IActionResult> LogOn(LogOnModel model)
        {
            if (model.IsNotValid())
            {
                model.SetInputModelValues();
                return View(model);
            }

            var clientLog = GetClientInfoLog();
            var request = new LogOnRequest(model.Email, model.Password, clientLog);
            var response = await OrganizationService.LogOn(request);
            if (response.Status.IsNotSuccess)
            {
                model.MapMessages(response);
                model.SetInputModelValues();
                return View(model);
            }

            await HttpContext.SignInWithClaims(response.Item.Name, response.Item.Email);

            if (model.RedirectUrl != null)
            {
                return Redirect(model.RedirectUrl);
            }

            if (response.Item.IsSuperAdmin)
            {
                return Redirect("/Admin/Dashboard");
            }

            return RedirectToHome();
        }

        [HttpGet, AllowAnonymous]
        public ViewResult DemandPasswordReset()
        {
            var model = new DemandPasswordResetModel();

            return View(model);
        }

        [HttpPost, AllowAnonymous]
        public async Task<IActionResult> DemandPasswordReset(DemandPasswordResetModel model)
        {
            if (model.IsNotValid())
            {
                model.SetInputModelValues();
                return View(model);
            }

            var request = new DemandPasswordResetRequest(model.Email);
            var response = await OrganizationService.DemandPasswordReset(request);
            if (response.Status.IsNotSuccess)
            {
                return RedirectToAccessDenied();
            }

            return Redirect("/User/DemandPasswordResetDone");
        }

        [HttpGet, AllowAnonymous]
        public ViewResult DemandPasswordResetDone()
        {
            var model = new DemandPasswordResetDoneModel();

            return View(model);
        }

        [HttpGet, AllowAnonymous]
        public async Task<IActionResult> ResetPassword(string email, Guid token)
        {
            if (email.IsNotEmail()
                || token.IsEmptyGuid())
            {
                return RedirectToAccessDenied();
            }

            var request = new PasswordResetValidateRequest(token, email);
            var response = await OrganizationService.ValidatePasswordReset(request);
            if (response.Status.IsNotSuccess)
            {
                return RedirectToAccessDenied();
            }

            var model = new ResetPasswordModel
            {
                Token = token,
                Email = email
            };

            return View(model);
        }

        [HttpPost, AllowAnonymous]
        public async Task<IActionResult> ResetPassword(ResetPasswordModel model)
        {
            if (model.IsNotValid())
            {
                model.SetInputModelValues();
                return View(model);
            }

            var request = new PasswordResetRequest(model.Token, model.Email, model.Password);

            var response = await OrganizationService.PasswordReset(request);
            if (response.Status.IsNotSuccess)
            {
                return RedirectToAccessDenied();
            }

            return Redirect("/User/ResetPasswordDone");
        }

        [HttpGet, AllowAnonymous]
        public ViewResult ResetPasswordDone()
        {
            var model = new ResetPasswordDoneModel();

            return View(model);
        }

        [HttpGet]
        public IActionResult Detail(Guid id)
        {
            var userUid = id;
            if (userUid.IsEmptyGuid())
            {
                return RedirectToAccessDenied();
            }

            var request = new UserReadRequest(CurrentUser.Id, userUid);
            var response = OrganizationService.GetUser(request);
            if (response.Status.IsNotSuccess)
            {
                return RedirectToAccessDenied();
            }

            var model = _userMapper.MapUserDetailModel(response.Item);

            return View(model);
        }

        [HttpGet]
        public ViewResult ChangePassword()
        {
            var model = new ChangePasswordModel();
            model.UserUid = CurrentUser.Uid;

            return View(model);
        }

        [HttpPost,
         JournalFilter(Message = "journal_change_password")]
        public async Task<IActionResult> ChangePassword(ChangePasswordModel model)
        {
            if (model.IsNotValid())
            {
                model.SetInputModelValues();
                return View(model);
            }

            var request = new PasswordChangeRequest(CurrentUser.Id, model.OldPassword, model.NewPassword);
            var response = await OrganizationService.ChangePassword(request);
            if (response.Status.IsNotSuccess)
            {
                model.MapMessages(response);
                return View(model);
            }

            CurrentUser.IsActionSucceed = true;
            return Redirect("/User/ChangePasswordDone");
        }

        [HttpGet]
        public ViewResult ChangePasswordDone()
        {
            var model = new ChangePasswordDoneModel();

            return View(model);
        }

        [HttpGet]
        public IActionResult Edit(Guid id)
        {
            var userUid = id;
            if (userUid.IsEmptyGuid())
            {
                userUid = CurrentUser.Uid;
            }

            var request = new UserReadRequest(CurrentUser.Id, userUid);
            var response = OrganizationService.GetUser(request);
            if (response.Status.IsNotSuccess)
            {
                return RedirectToAccessDenied();
            }

            var model = new UserEditModel();
            model.UserUid = userUid;
            model.FirstName = response.Item.FirstName;
            model.LastName = response.Item.LastName;
            model.LanguageUid = response.Item.LanguageUid;
            model.LanguageName = response.Item.LanguageName;
            model.SetInputModelValues();

            return View(model);
        }

        [HttpPost,
         JournalFilter(Message = "journal_user_edit")]
        public async Task<IActionResult> Edit(UserEditModel model)
        {
            if (model.IsNotValid())
            {
                model.SetInputModelValues();
                return View(model);
            }

            var request = new UserEditRequest(CurrentUser.Id, model.UserUid, model.FirstName,
                                              model.LastName, model.LanguageUid);
            var response = await OrganizationService.EditUser(request);
            if (response.Status.IsNotSuccess)
            {
                model.MapMessages(response);
                model.SetInputModelValues();
                return View(model);
            }

            CurrentUser.IsActionSucceed = true;
            return Redirect($"/User/Detail/{response.Item.Uid}");
        }

        [HttpGet]
        public IActionResult Invite(Guid id)
        {
            var organizationUid = id;
            if (organizationUid.IsEmptyGuid())
            {
                organizationUid = CurrentUser.OrganizationUid;
            }

            var model = new InviteModel();
            model.OrganizationUid = organizationUid;
            model.SetInputModelValues();

            return View(model);
        }

        [HttpPost,
         JournalFilter(Message = "journal_user_invite")]
        public async Task<IActionResult> Invite(InviteModel model)
        {
            if (model.IsNotValid())
            {
                model.SetInputModelValues();
                return View(model);
            }

            var request = new UserInviteRequest(CurrentUser.Id, model.OrganizationUid, model.Email, model.FirstName, model.LastName);
            var response = await OrganizationService.InviteUser(request);
            if (response.Status.IsNotSuccess)
            {
                model.MapMessages(response);
                model.SetInputModelValues();
                return View(model);
            }

            CurrentUser.IsActionSucceed = true;
            return Redirect("/User/InviteDone");
        }

        [HttpGet]
        public ViewResult InviteDone()
        {
            var model = new InviteDoneModel();

            return View(model);
        }

        [HttpGet, AllowAnonymous]
        public async Task<IActionResult> AcceptInvite(string email, Guid token)
        {
            if (email.IsNotEmail()
                || token.IsEmptyGuid())
            {
                return RedirectToAccessDenied();
            }

            var request = new UserInviteValidateRequest(token, email);

            var response = await OrganizationService.ValidateUserInvitation(request);
            if (response.Status.IsNotSuccess)
            {
                return RedirectToAccessDenied();
            }

            var model = new InviteAcceptModel();
            model.FirstName = response.Item.FirstName;
            model.LastName = response.Item.LastName;
            model.Token = token;
            model.Email = email;
            model.SetInputModelValues();
            return View(model);
        }

        [HttpPost, AllowAnonymous]
        public async Task<IActionResult> AcceptInvite(InviteAcceptModel model)
        {
            if (model.IsNotValid())
            {
                model.SetInputModelValues();
                return View(model);
            }

            var request = new UserAcceptInviteRequest(model.Token, model.Email, model.FirstName, model.LastName, model.Password, model.LanguageName, model.LanguageUid);

            var response = await OrganizationService.AcceptInvitation(request);
            if (response.Status.IsNotSuccess)
            {
                model.MapMessages(response);
                model.SetInputModelValues();
                return View(model);
            }

            return Redirect("/User/AcceptInviteDone");
        }

        [HttpGet, AllowAnonymous]
        public ViewResult AcceptInviteDone()
        {
            var model = new InviteAcceptDoneModel();

            return View(model);
        }

        [HttpPost,
         JournalFilter(Message = "journal_change_user_activation")]
        public async Task<IActionResult> ChangeActivation(Guid id)
        {
            var model = new CommonResult { IsOk = false };

            var userUid = id;
            if (userUid.IsEmptyGuid())
            {
                return Json(model);
            }

            var request = new UserChangeActivationRequest(CurrentUser.Id, userUid);
            var response = await OrganizationService.ChangeActivationForUser(request);
            if (response.Status.IsNotSuccess)
            {
                return Json(model);
            }

            model.IsOk = true;
            CurrentUser.IsActionSucceed = true;
            return Json(model);
        }

        [HttpPost,
         JournalFilter(Message = "journal_log_off")]
        public async Task<RedirectResult> LogOff()
        {
            await HttpContext.SignOutAsync();
            CurrentUser.IsActionSucceed = true;
            return RedirectToHome();
        }

        [HttpGet]
        public IActionResult JournalList(Guid id)
        {
            var userUid = id;
            if (userUid.IsEmptyGuid())
            {
                userUid = CurrentUser.Uid;
            }

            var model = new UserJournalListModel();
            model.UserUid = userUid;

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> JournalListData(Guid id, int skip, int take)
        {
            var userUid = id;
            if (userUid.IsEmptyGuid())
            {
                userUid = CurrentUser.Uid;
            }

            var request = new UserJournalReadListRequest(CurrentUser.Id, userUid);
            SetPaging(skip, take, request);

            var response = await JournalService.GetJournalsOfUser(request);
            if (response.Status.IsNotSuccess)
            {
                return NotFound();
            }

            var result = new DataResult();
            result.AddHeaders("message", "created_at");

            for (var i = 0; i < response.Items.Count; i++)
            {
                var item = response.Items[i];
                var stringBuilder = new StringBuilder();
                stringBuilder.Append($"{item.Uid}{DataResult.SEPARATOR}");
                stringBuilder.Append($"{item.Message}{DataResult.SEPARATOR}");
                stringBuilder.Append($"{GetDateTimeAsString(item.CreatedAt)}{DataResult.SEPARATOR}");

                result.Data.Add(stringBuilder.ToString());
            }

            result.PagingInfo = response.PagingInfo;
             result.PagingInfo.PagingType = PagingInfo.PAGE_NUMBERS;

            return Json(result);
        }

        [HttpGet]
        public IActionResult Revisions(Guid id)
        {
            var userUid = id;
            if (userUid.IsEmptyGuid())
            {
                return RedirectToHome();
            }

            var model = new UserRevisionReadListModel();
            if (userUid.IsNotEmptyGuid())
            {
                var request = new UserReadRequest(CurrentUser.Id, userUid);
                var response = OrganizationService.GetUser(request);
                if (response.Status.IsNotSuccess)
                {
                    return NotFound();
                }

                model.UserUid = userUid;
                model.UserName = response.Item.Name;
                model.SetInputModelValues();
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> RevisionsData(Guid id)
        {
            var userUid = id;
            if (userUid.IsEmptyGuid())
            {
                return NotFound();
            }

            var request = new UserRevisionReadListRequest(CurrentUser.Id, userUid);

            var response = await OrganizationService.GetUserRevisions(request);
            if (response.Status.IsNotSuccess)
            {
                return NotFound();
            }

            var result = new DataResult();
            result.AddHeaders("revision", "revisioned_by", "revisioned_at", "user_name", "email", "is_active", "created_at", "");

            for (var i = 0; i < response.Items.Count; i++)
            {
                var revisionItem = response.Items[i];
                var item = revisionItem.Item;
                var stringBuilder = new StringBuilder();
                stringBuilder.Append($"{item.Uid}{DataResult.SEPARATOR}");
                stringBuilder.Append($"{revisionItem.Revision}{DataResult.SEPARATOR}");
                stringBuilder.Append($"{revisionItem.RevisionedByName}{DataResult.SEPARATOR}");
                stringBuilder.Append($"{GetDateTimeAsString(revisionItem.RevisionedAt)}{DataResult.SEPARATOR}");
                stringBuilder.Append($"{result.PrepareLink($"/User/Detail/{item.Uid}", item.Name)}{DataResult.SEPARATOR}");
                stringBuilder.Append($"{item.Email}{DataResult.SEPARATOR}");
                stringBuilder.Append($"{item.IsActive}{DataResult.SEPARATOR}");
                stringBuilder.Append($"{GetDateTimeAsString(item.CreatedAt)}{DataResult.SEPARATOR}");
                stringBuilder.Append($"{result.PrepareRestoreButton("restore", "/User/Restore/", "/User/Detail")}{DataResult.SEPARATOR}");

                result.Data.Add(stringBuilder.ToString());
            }

            return Json(result);
        }

        [HttpPost,
         JournalFilter(Message = "journal_user_restore")]
        public async Task<IActionResult> Restore(Guid id, int revision)
        {
            var model = new CommonResult { IsOk = false };

            var userUid = id;
            if (userUid.IsEmptyGuid())
            {
                return Json(model);
            }

            if (revision < 1)
            {
                return Json(model);
            }

            var request = new UserRestoreRequest(CurrentUser.Id, userUid, revision);
            var response = await OrganizationService.RestoreUser(request);
            if (response.Status.IsNotSuccess)
            {
                model.Messages = response.ErrorMessages;
                return Json(model);
            }

            model.IsOk = true;
            CurrentUser.IsActionSucceed = true;
            return Json(model);
        }

      
    }
}
