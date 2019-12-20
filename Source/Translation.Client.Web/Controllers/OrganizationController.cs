using System;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using StandardUtils.Helpers;
using StandardUtils.Models.Shared;

using Translation.Client.Web.Helpers;
using Translation.Client.Web.Helpers.ActionFilters;
using Translation.Client.Web.Helpers.DataResultHelpers;
using Translation.Client.Web.Helpers.Mappers;
using Translation.Client.Web.Models.Base;
using Translation.Client.Web.Models.Organization;
using Translation.Common.Contracts;
using Translation.Common.Models.Requests.Integration;
using Translation.Common.Models.Requests.Integration.Token;
using Translation.Common.Models.Requests.Journal;
using Translation.Common.Models.Requests.Organization;
using Translation.Common.Models.Requests.Project;
using Translation.Common.Models.Requests.User;
using Translation.Common.Models.Requests.User.LoginLog;

namespace Translation.Client.Web.Controllers
{
    public class OrganizationController : BaseController
    {
        private readonly OrganizationMapper _organizationMapper;
        private readonly IIntegrationService _integrationService;
        private readonly IProjectService _projectService;

        public OrganizationController(IOrganizationService organizationService,
                                      OrganizationMapper organizationMapper,
                                      IJournalService journalService,
                                      ILanguageService languageService,
                                      ITranslationProviderService translationProviderService,
                                      IIntegrationService integrationService,
                                      IProjectService projectService) : base(organizationService, journalService, languageService, translationProviderService)
        {
            _organizationMapper = organizationMapper;
            _integrationService = integrationService;
            _projectService = projectService;
        }

        [HttpGet]
        public IActionResult Detail()
        {
            var request = new OrganizationReadRequest(CurrentUser.Id, CurrentUser.OrganizationUid);

            var response = OrganizationService.GetOrganization(request);
            if (response.Status.IsNotSuccess)
            {
                return RedirectToAccessDenied();
            }

            var model = _organizationMapper.MapOrganizationDetailModel(response.Item);

            return View(model);
        }

        [HttpGet]
        public IActionResult Edit()
        {
            var request = new OrganizationReadRequest(CurrentUser.Id, CurrentUser.OrganizationUid);
            var response = OrganizationService.GetOrganization(request);
            if (response.Status.IsNotSuccess)
            {
                return RedirectToAccessDenied();
            }

            var model = _organizationMapper.MapOrganizationEditModel(response.Item);

            return View(model);
        }

        [HttpPost,
         JournalFilter(Message = "journal_organization_edit")]
        public async Task<IActionResult> Edit(OrganizationEditModel model)
        {
            if (model.IsNotValid())
            {
                model.SetInputModelValues();
                return View(model);
            }

            var request = new OrganizationEditRequest(CurrentUser.Id, model.OrganizationUid, model.Name, model.Description);

            var response = await OrganizationService.EditOrganization(request);
            if (response.Status.IsNotSuccess)
            {
                model.MapMessages(response);
                model.SetInputModelValues();
                return View(model);
            }

            CurrentUser.IsActionSucceed = true;
            return Redirect($"/Organization/Detail/{response.Item.Uid}");
        }

        [HttpGet]
        public IActionResult Revisions(Guid id)
        {
            var organizationUid = id;
            if (organizationUid.IsEmptyGuid())
            {
                return RedirectToHome();
            }

            var model = new OrganizationRevisionReadListModel();
            if (organizationUid.IsNotEmptyGuid())
            {
                var request = new OrganizationReadRequest(CurrentUser.Id, organizationUid);
                var response = OrganizationService.GetOrganization(request);
                if (response.Status.IsNotSuccess)
                {
                    return NotFound();
                }

                model.OrganizationUid = organizationUid;
                model.OrganizationName = response.Item.Name;
                model.SetInputModelValues();
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> RevisionsData(Guid id)
        {
            var organizationUid = id;
            if (organizationUid.IsEmptyGuid())
            {
                return NotFound();
            }

            var request = new OrganizationRevisionReadListRequest(CurrentUser.Id, organizationUid);

            var response = await OrganizationService.GetOrganizationRevisions(request);
            if (response.Status.IsNotSuccess)
            {
                return NotFound();
            }

            var result = new DataResult();
            result.AddHeaders("revision", "revisioned_by", "revisioned_at", "organization_name", "is_active", "created_at", "");

            for (var i = 0; i < response.Items.Count; i++)
            {
                var revisionItem = response.Items[i];
                var item = revisionItem.Item;
                var stringBuilder = new StringBuilder();
                stringBuilder.Append($"{item.Uid}{DataResult.SEPARATOR}");
                stringBuilder.Append($"{revisionItem.Revision}{DataResult.SEPARATOR}");
                stringBuilder.Append($"{revisionItem.RevisionedByName}{DataResult.SEPARATOR}");
                stringBuilder.Append($"{GetDateTimeAsString(revisionItem.RevisionedAt)}{DataResult.SEPARATOR}");
                stringBuilder.Append($"{result.PrepareLink($"/Organization/Detail/{item.Uid}", item.Name)}{DataResult.SEPARATOR}");
                stringBuilder.Append($"{item.IsActive}{DataResult.SEPARATOR}");
                stringBuilder.Append($"{GetDateTimeAsString(item.CreatedAt)}{DataResult.SEPARATOR}");
                stringBuilder.Append($"{result.PrepareRestoreButton("restore", "/Organization/Restore/", "/Organization/Detail")}{DataResult.SEPARATOR}");

                result.Data.Add(stringBuilder.ToString());
            }

            return Json(result);
        }

        [HttpPost,
         JournalFilter(Message = "journal_organization_restore")]
        public async Task<IActionResult> Restore(Guid id, int revision)
        {
            var model = new CommonResult { IsOk = false };

            var organizationUid = id;
            if (organizationUid.IsEmptyGuid())
            {
                return Json(model);
            }

            if (revision < 1)
            {
                return Json(model);
            }

            var request = new OrganizationRestoreRequest(CurrentUser.Id, organizationUid, revision);
            var response = await OrganizationService.RestoreOrganization(request);
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
        public IActionResult PendingTranslations()
        {
            var model = new OrganizationPendingTranslationReadListModel();
            model.OrganizationUid = CurrentUser.OrganizationUid;
            model.OrganizationName = CurrentUser.Organization.Name;

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> PendingTranslationsData( int skip, int take)
        {
            var request = new OrganizationPendingTranslationReadListRequest(CurrentUser.Id);
            SetPaging(skip, take, request);

            var response = await OrganizationService.GetPendingTranslations(request);
            if (response.Status.IsNotSuccess)
            {
                return NotFound();
            }

            var result = DataResultHelper.GetLabelTranslationRevisionsData(response.Items);
            result.PagingInfo = response.PagingInfo;
            result.PagingInfo.PagingType = PagingInfo.PAGE_NUMBERS;

            return Json(result);
        }

        [HttpGet]
        public IActionResult UserLoginLogList()
        {
            var model = new OrganizationUserLoginLogListModel();
            model.OrganizationUid = CurrentUser.OrganizationUid;
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> UserLoginLogListData( int skip, int take)
        {
            var request = new OrganizationLoginLogReadListRequest(CurrentUser.Id, CurrentUser.OrganizationUid);
            SetPaging(skip, take, request);

            var response = await OrganizationService.GetUserLoginLogsOfOrganization(request);
            if (response.Status.IsNotSuccess)
            {
                return NotFound();
            }

            var result = DataResultHelper.GetUserLoginLogDataResult(response.Items);
            result.PagingInfo = response.PagingInfo;
            result.PagingInfo.PagingType = PagingInfo.PAGE_NUMBERS;

            return Json(result);
        }

        [HttpGet]
        public async Task<IActionResult> UserListData(int skip, int take)
        {
            var request = new UserReadListRequest(CurrentUser.Id, CurrentUser.OrganizationUid);
            SetPaging(skip, take, request);
            var response = await OrganizationService.GetUsers(request);
            if (response.Status.IsNotSuccess)
            {
                return NotFound();
            }

            var result = DataResultHelper.GetOrganizationUserListDataResult(response.Items);
            result.PagingInfo = response.PagingInfo;
            result.PagingInfo.PagingType = PagingInfo.PAGE_NUMBERS;

            return Json(result);
        }

        [HttpGet]
        public async Task<IActionResult> IntegrationListData( int skip, int take)
        {
            var request = new IntegrationReadListRequest(CurrentUser.Id);
            SetPaging(skip, take, request);

            var response = await _integrationService.GetIntegrations(request);
            if (response.Status.IsNotSuccess)
            {
                return NotFound();
            }

            var result = DataResultHelper.GetIntegrationListData(response.Items);
            result.PagingInfo = response.PagingInfo;
            result.PagingInfo.PagingType = PagingInfo.PAGE_NUMBERS;

            return Json(result);
        }

        [HttpGet]
        public async Task<IActionResult> ProjectListData(int skip, int take)
        {
            var request = new ProjectReadListRequest(CurrentUser.Id);
            SetPaging(skip, take, request);

            var response = await _projectService.GetProjects(request);
            if (response.Status.IsNotSuccess)
            {
                return NotFound();
            }

            var result = DataResultHelper.GetProjectListData(response.Items);
            result.PagingInfo = response.PagingInfo;
            result.PagingInfo.PagingType = PagingInfo.PAGE_NUMBERS;

            return Json(result);
        }

        [HttpGet]
        public IActionResult TokenRequestLogList()
        {
           
            var model = new OrganizationTokenRequestLogListModel();
            model.OrganizationUid = CurrentUser.OrganizationUid;

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> TokenRequestLogListData(int skip, int take)
        {
            var request = new OrganizationTokenRequestLogReadListRequest(CurrentUser.Id, CurrentUser.OrganizationUid);
            SetPaging(skip, take, request);

            var response = await _integrationService.GetTokenRequestLogsOfOrganization(request);
            if (response.Status.IsNotSuccess)
            {
                return NotFound();
            }

            var result = DataResultHelper.GetTokenRequestLogListData(response.Items);
            result.PagingInfo = response.PagingInfo;
            result.PagingInfo.PagingType = PagingInfo.PAGE_NUMBERS;

            return Json(result);
        }

        [HttpGet]
        public IActionResult JournalList()
        {
            var model = new OrganizationJournalListModel();
            model.OrganizationUid = CurrentUser.OrganizationUid;

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> JournalListData( int skip, int take)
        {
          
            var request = new OrganizationJournalReadListRequest(CurrentUser.Id);
            SetPaging(skip, take, request);

            var response = await JournalService.GetJournalsOfOrganization(request);
            if (response.Status.IsNotSuccess)
            {
                return NotFound();
            }

            var result = DataResultHelper.GetJournalListDataResult(response.Items);
            result.PagingInfo = response.PagingInfo;
            result.PagingInfo.PagingType = PagingInfo.PAGE_NUMBERS;

            return Json(result);
        }
    }
}
