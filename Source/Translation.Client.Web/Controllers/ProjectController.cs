using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using StandardUtils.Helpers;
using StandardUtils.Models.Shared;

using Translation.Client.Web.Helpers;
using Translation.Client.Web.Helpers.ActionFilters;
using Translation.Client.Web.Helpers.Mappers;
using Translation.Client.Web.Models.Base;
using Translation.Client.Web.Models.Project;
using Translation.Common.Contracts;
using Translation.Common.Models.Requests.Label;
using Translation.Common.Models.Requests.Organization;
using Translation.Common.Models.Requests.Project;

namespace Translation.Client.Web.Controllers
{
    public class ProjectController : BaseController
    {
        private readonly IProjectService _projectService;
        private readonly ProjectMapper _projectMapper;
        private readonly ILabelService _labelService;

        public ProjectController(IOrganizationService organizationService,
                                 IJournalService journalService,
                                 ILanguageService languageService,
                                 ITranslationProviderService translationProviderService,
                                 ILabelService labelService,
                                 IProjectService projectService,
                                 ProjectMapper projectMapper) : base(organizationService, journalService, languageService, translationProviderService)
        {
            _labelService = labelService;
            _projectService = projectService;
            _projectMapper = projectMapper;
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

            var model = _projectMapper.MapProjectCreateModel(response.Item.Uid);

            return View(model);
        }

        [HttpPost,
         JournalFilter(Message = "journal_project_create")]
        public async Task<IActionResult> Create(ProjectCreateModel model)
        {
            if (model.IsNotValid())
            {
                model.SetInputModelValues();
                return View(model);
            }

            var request = new ProjectCreateRequest(CurrentUser.Id, model.OrganizationUid, model.Name,
                                                   model.Url, model.Description, model.Slug, 
                                                   model.LanguageUid);
            var response = await _projectService.CreateProject(request);
            if (response.Status.IsNotSuccess)
            {
                model.MapMessages(response);
                model.SetInputModelValues();
                return View(model);
            }

            CurrentUser.IsActionSucceed = true;
            return Redirect($"/Project/Detail/{response.Item.Uid}");
        }

        [HttpGet]
        public async Task<IActionResult> Detail(Guid id)
        {
            var projectUid = id;
            if (projectUid.IsEmptyGuid())
            {
                return RedirectToAccessDenied();
            }

            var request = new ProjectReadRequest(CurrentUser.Id, projectUid);
            var response = await _projectService.GetProject(request);
            if (response.Status.IsNotSuccess)
            {
                return RedirectToAccessDenied();
            }

            var model = _projectMapper.MapProjectDetailModel(response.Item);

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var projectUid = id;
            if (projectUid.IsEmptyGuid())
            {
                return RedirectToAccessDenied();
            }

            var request = new ProjectReadRequest(CurrentUser.Id, projectUid);
            var response = await _projectService.GetProject(request);
            if (response.Status.IsNotSuccess)
            {
                return RedirectToAccessDenied();
            }

            var model = _projectMapper.MapProjectEditModel(response.Item);

            return View(model);
        }

        [HttpPost,
         JournalFilter(Message = "journal_project_edit")]
        public async Task<IActionResult> Edit(ProjectEditModel model)
        {
            if (model.IsNotValid())
            {
                model.SetInputModelValues();
                return View(model);
            }

            var request = new ProjectEditRequest(CurrentUser.Id, model.OrganizationUid, model.ProjectUid,
                                                 model.Name, model.Url, model.Description,
                                                 model.Slug, model.LanguageUid);
            var response = await _projectService.EditProject(request);
            if (response.Status.IsNotSuccess)
            {
                model.MapMessages(response);
                return View(model);
            }



            CurrentUser.IsActionSucceed = true;
            return Redirect($"/Project/Detail/{response.Item.Uid}");
        }

        [HttpPost,
         JournalFilter(Message = "journal_project_delete")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var projectUid = id;
            if (projectUid.IsEmptyGuid())
            {
                return Forbid();
            }

            var request = new ProjectDeleteRequest(CurrentUser.Id, projectUid);
            var response = await _projectService.DeleteProject(request);
            if (response.Status.IsNotSuccess)
            {
                return Json(new CommonResult { IsOk = false, Messages = response.ErrorMessages });
            }

            CurrentUser.IsActionSucceed = true;
            return Json(new CommonResult { IsOk = true });
        }

        [HttpGet]
        public async Task<IActionResult> Clone(Guid id)
        {
            var cloningProjectUid = id;
            if (cloningProjectUid.IsEmptyGuid())
            {
                return RedirectToAccessDenied();
            }

            var request = new ProjectReadRequest(CurrentUser.Id, cloningProjectUid);
            var response = await _projectService.GetProject(request);
            if (response.Status.IsNotSuccess)
            {
                return RedirectToAccessDenied();
            }

            var model = _projectMapper.MapProjectCloneModel(response.Item);

            return View(model);
        }

        [HttpPost,
         JournalFilter(Message = "journal_project_clone")]
        public async Task<IActionResult> Clone(ProjectCloneModel model)
        {
            if (model.IsNotValid())
            {
                model.SetInputModelValues();
                return View(model);
            }

            var request = new ProjectCloneRequest(CurrentUser.Id, model.OrganizationUid, model.CloningProjectUid,
                                                  model.Name, model.Url, model.Description,
                                                  model.LabelCount, model.LabelTranslationCount, model.IsSuperProject,
                                                  model.Slug, model.LanguageUid);
            var response = await _projectService.CloneProject(request);
            if (response.Status.IsNotSuccess)
            {
                model.MapMessages(response);
                model.SetInputModelValues();
                return View(model);
            }

            CurrentUser.IsActionSucceed = true;
            return Redirect($"/Project/Detail/{response.Item.Uid }");
        }

        [HttpGet]
        public async Task<IActionResult> SelectData()
        {
            var request = new ProjectReadListRequest(CurrentUser.Id);
            var response = await _projectService.GetProjects(request);
            if (response.Status.IsNotSuccess)
            {
                return null;
            }

            var items = new List<SelectResult>();
            for (var i = 0; i < response.Items.Count; i++)
            {
                var item = response.Items[i];
                items.Add(new SelectResult(item.Uid.ToUidString(), $"{item.Name}"));
            }

            return Json(items);
        }

        [HttpGet]
        public async Task<IActionResult> PendingTranslations(Guid id)
        {
            var projectUid = id;
            if (projectUid.IsEmptyGuid())
            {
                return RedirectToHome();
            }

            var model = new ProjectPendingTranslationReadListModel();
            if (projectUid.IsNotEmptyGuid())
            {
                var request = new ProjectReadRequest(CurrentUser.Id, projectUid);
                var response = await _projectService.GetProject(request);
                if (response.Status.IsNotSuccess)
                {
                    return NotFound();
                }

                model.ProjectUid = projectUid;
                model.ProjectName = response.Item.Name;
                model.SetInputModelValues();
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> PendingTranslationsData(Guid id, int skip, int take)
        {
            var projectUid = id;
            if (projectUid.IsEmptyGuid())
            {
                return Forbid();
            }

            var request = new ProjectPendingTranslationReadListRequest(CurrentUser.Id, projectUid);
            SetPaging(skip, take, request);

            var response = await _projectService.GetPendingTranslations(request);
            if (response.Status.IsNotSuccess)
            {
                return NotFound();
            }

            var result = new DataResult();
            result.AddHeaders("label_key", "label_translation_count", "description", "is_active");

            for (var i = 0; i < response.Items.Count; i++)
            {
                var item = response.Items[i];
                var stringBuilder = new StringBuilder();
                stringBuilder.Append($"{item.Uid}{DataResult.SEPARATOR}");
                stringBuilder.Append($"{result.PrepareLink($"/Label/Detail/{item.Uid}", item.Key)}{DataResult.SEPARATOR}");
                stringBuilder.Append($"{item.LabelTranslationCount}{DataResult.SEPARATOR}");
                stringBuilder.Append($"{item.Description}{DataResult.SEPARATOR}");
                stringBuilder.Append($"{item.IsActive}{DataResult.SEPARATOR}");

                result.Data.Add(stringBuilder.ToString());
            }

            result.PagingInfo = response.PagingInfo;
             result.PagingInfo.PagingType = PagingInfo.PAGE_NUMBERS;

            return Json(result);
        }

        [HttpGet]
        public async Task<IActionResult> LabelListData(Guid id, int skip, int take)
        {
            var projectUid = id;
            if (projectUid.IsEmptyGuid())
            {
                return Forbid();
            }

            var projectReadRequest = new ProjectReadRequest(CurrentUser.Id, projectUid);
            var projectReadResponse = await _projectService.GetProject(projectReadRequest);
            if (projectReadResponse.Status.IsNotSuccess)
            {
                return NotFound();
            }

            var projectSlug = projectReadResponse.Item.Slug;

            var request = new LabelReadListRequest(CurrentUser.Id, projectUid);
            SetPaging(skip, take, request);

            var response = await _labelService.GetLabels(request);
            if (response.Status.IsNotSuccess)
            {
                return NotFound();
            }

            var result = new DataResult();
            result.AddHeaders("label_key", "label_translation_count", "description", "is_active");

            for (var i = 0; i < response.Items.Count; i++)
            {
                var item = response.Items[i];
                var stringBuilder = new StringBuilder();
                stringBuilder.Append($"{item.Uid}{DataResult.SEPARATOR}");
                stringBuilder.Append($"{result.PrepareLink($"/Label/Detail/{item.Uid}", item.Key)}{DataResult.SEPARATOR}");
                stringBuilder.Append($"{item.LabelTranslationCount}{DataResult.SEPARATOR}");
                stringBuilder.Append($"{item.Description}{DataResult.SEPARATOR}");
                stringBuilder.Append($"{item.IsActive}{DataResult.SEPARATOR}");

                result.Data.Add(stringBuilder.ToString());
            }

            result.PagingInfo = response.PagingInfo;
             result.PagingInfo.PagingType = PagingInfo.PAGE_NUMBERS;

            return Json(result);
        }

        [HttpPost,
         JournalFilter(Message = "journal_project_download_labels")]
        public async Task<IActionResult> DownloadLabels(Guid id)
        {
            var projectUid = id;
            if (projectUid.IsEmptyGuid())
            {
                return NoContent();
            }

            var request = new AllLabelReadListRequest(CurrentUser.Id, projectUid);
            request.IsAddLabelsNotTranslated = true;
            var response = await _labelService.GetLabelsWithTranslations(request);
            if (response.Status.IsNotSuccess)
            {
                return NoContent();
            }

            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("label_key,language,translation");
            for (var i = 0; i < response.Labels.Count; i++)
            {
                var label = response.Labels[i];
                if (!label.Translations.Any())
                {
                    stringBuilder.AppendLine($"{label.Key},en,{label.Key}");
                    continue;
                }

                for (var j = 0; j < label.Translations.Count; j++)
                {
                    var translation = label.Translations[j];
                    stringBuilder.AppendLine($"{label.Key},{translation.LanguageIsoCode2},{translation.Translation}");
                }
            }

            CurrentUser.IsActionSucceed = true;
            return File(Encoding.UTF8.GetBytes(stringBuilder.ToString()), "text/csv", "labels.csv");
        }

        [HttpPost,
         JournalFilter(Message = "journal_change_activation")]
        public async Task<IActionResult> ChangeActivation(Guid id, Guid organizationUid)
        {
            var model = new CommonResult { IsOk = false };

            var projectUid = id;
            if (projectUid.IsEmptyGuid()
                || organizationUid.IsEmptyGuid())
            {
                return Json(model);
            }

            var request = new ProjectChangeActivationRequest(CurrentUser.Id, organizationUid, projectUid);
            var response = await _projectService.ChangeActivationForProject(request);
            if (response.Status.IsNotSuccess)
            {
                return Json(model);
            }

            model.IsOk = true;
            CurrentUser.IsActionSucceed = true;
            return Json(model);
        }

        [HttpGet]
        public async Task<IActionResult> Revisions(Guid id)
        {
            var projectUid = id;
            if (projectUid.IsEmptyGuid())
            {
                return RedirectToHome();
            }

            var model = new ProjectRevisionReadListModel();
            if (projectUid.IsNotEmptyGuid())
            {
                var request = new ProjectReadRequest(CurrentUser.Id, projectUid);
                var response = await _projectService.GetProject(request);
                if (response.Status.IsNotSuccess)
                {
                    return NotFound();
                }

                model.ProjectUid = projectUid;
                model.ProjectName = response.Item.Name;
                model.SetInputModelValues();
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> RevisionsData(Guid id)
        {
            var projectUid = id;
            if (projectUid.IsEmptyGuid())
            {
                return NotFound();
            }

            var request = new ProjectRevisionReadListRequest(CurrentUser.Id, projectUid);

            var response = await _projectService.GetProjectRevisions(request);
            if (response.Status.IsNotSuccess)
            {
                return NotFound();
            }

            var result = new DataResult();
            result.AddHeaders("revision", "revisioned_by", "revisioned_at", "project_name", "is_active", "created_at", "");

            for (var i = 0; i < response.Items.Count; i++)
            {
                var revisionItem = response.Items[i];
                var item = revisionItem.Item;
                var stringBuilder = new StringBuilder();
                stringBuilder.Append($"{item.Uid}{DataResult.SEPARATOR}");
                stringBuilder.Append($"{revisionItem.Revision}{DataResult.SEPARATOR}");
                stringBuilder.Append($"{revisionItem.RevisionedByName}{DataResult.SEPARATOR}");
                stringBuilder.Append($"{GetDateTimeAsString(revisionItem.RevisionedAt)}{DataResult.SEPARATOR}");
                stringBuilder.Append($"{result.PrepareLink($"/Project/Detail/{item.Uid}", item.Name)}{DataResult.SEPARATOR}");
                stringBuilder.Append($"{item.IsActive}{DataResult.SEPARATOR}");
                stringBuilder.Append($"{GetDateTimeAsString(item.CreatedAt)}{DataResult.SEPARATOR}");
                stringBuilder.Append($"{result.PrepareRestoreButton("restore", "/Project/Restore/", "/Project/Detail")}{DataResult.SEPARATOR}");

                result.Data.Add(stringBuilder.ToString());
            }

            return Json(result);
        }

        [HttpPost,
         JournalFilter(Message = "journal_project_restore")]
        public async Task<IActionResult> Restore(Guid id, int revision)
        {
            var model = new CommonResult { IsOk = false };

            var projectUid = id;
            if (projectUid.IsEmptyGuid())
            {
                return Json(model);
            }

            if (revision < 1)
            {
                return Json(model);
            }

            var request = new ProjectRestoreRequest(CurrentUser.Id, projectUid, revision);
            var response = await _projectService.RestoreProject(request);
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
