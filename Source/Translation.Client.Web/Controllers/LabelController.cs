using System;
using System.Collections.Generic;
using System.IO;
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
using Translation.Client.Web.Models.Label;
using Translation.Client.Web.Models.LabelTranslation;
using Translation.Common.Contracts;
using Translation.Common.Models.Requests.Label;
using Translation.Common.Models.Requests.Label.LabelTranslation;
using Translation.Common.Models.Requests.Project;

namespace Translation.Client.Web.Controllers
{
    public class LabelController : BaseController
    {
        private readonly ITextTranslateIntegration _textTranslateIntegration;
        private readonly IProjectService _projectService;
        private readonly ILabelService _labelService;
        private readonly LabelMapper _labelMapper;

        public LabelController(IOrganizationService organizationService,
                               IJournalService journalService,
                               ILanguageService languageService,
                               ITranslationProviderService translationProviderService,
                               ITextTranslateIntegration textTranslateIntegration,
                               IProjectService projectService,
                               ILabelService labelService,
                               LabelMapper labelMapper) : base(organizationService, journalService, languageService, translationProviderService)
        {
            _textTranslateIntegration = textTranslateIntegration;
            _projectService = projectService;
            _labelService = labelService;
            _labelMapper = labelMapper;
        }
        #region Label

        [HttpGet]
        public async Task<IActionResult> Create(Guid id)
        {
            var projectUid = id;
            if (projectUid.IsEmptyGuid())
            {
                return RedirectToHome();
            }

            var request = new ProjectReadRequest(CurrentUser.Id, projectUid);
            var response = await _projectService.GetProject(request);
            if (response.Status.IsNotSuccess)
            {
                return RedirectToAccessDenied();
            }

            var model = _labelMapper.MapLabelCreateModel(response.Item, ActiveTranslationProvider);

            return View(model);
        }

        [HttpPost,
         JournalFilter(Message = "journal_label_create")]
        public async Task<IActionResult> Create(LabelCreateModel model)
        {
            if (model.IsNotValid())
            {
                model.SetInputModelValues();
                return View(model);
            }

            var labelTranslationLanguageUidArray = new Guid[] { };
            if (model.LabelTranslationLanguageUid.IsNotEmpty())
            {
                var languageUids = model.LabelTranslationLanguageUid.Split(",", StringSplitOptions.RemoveEmptyEntries);
                labelTranslationLanguageUidArray = new Guid[languageUids.Length];
                for (int i = 0; i < languageUids.Length; i++)
                {
                    labelTranslationLanguageUidArray[i] = Guid.Parse(languageUids[i]);

                }
            }

            var request = new LabelCreateRequest(CurrentUser.Id, model.OrganizationUid, model.ProjectUid,
                                                 model.Key, model.Description, labelTranslationLanguageUidArray,
                                                 model.IsGettingTranslationFromOtherProject);
            var response = await _labelService.CreateLabel(request);
            if (response.Status.IsNotSuccess)
            {
                model.MapMessages(response);
                return View(model);
            }

            CurrentUser.IsActionSucceed = true;
            return Redirect($"/Label/Detail/{response.Item.Uid}");
        }

        [HttpGet]
        public async Task<IActionResult> Detail(Guid id, string project, string label)
        {
            var labelUid = id;
            if (labelUid.IsNotEmptyGuid())
            {
                var labelReadRequest = new LabelReadRequest(CurrentUser.Id, labelUid);
                var labelReadResponse = await _labelService.GetLabel(labelReadRequest);

                if (labelReadResponse.Status.IsNotSuccess)
                {
                    return RedirectToAccessDenied();
                }

                var labelDetailModel = _labelMapper.MapLabelDetailModel(labelReadResponse.Item);

                return View(labelDetailModel);
            }
            else
            {
                if (project.IsEmpty()
                    || label.IsEmpty())
                {
                    return RedirectToHome();
                }

                var projectSlug = project;
                var labelKey = label;

                var projectReadBySlugRequest = new ProjectReadBySlugRequest(CurrentUser.Id, projectSlug);
                var projectReadBySlugResponse = await _projectService.GetProjectBySlug(projectReadBySlugRequest);
                if (projectReadBySlugResponse.Status.IsNotSuccess)
                {
                    return RedirectToAccessDenied();
                }

                var projectName = projectReadBySlugResponse.Item.Name;
                var request = new LabelReadByKeyRequest(CurrentUser.Id, labelKey, projectName);
                var response = await _labelService.GetLabelByKey(request);

                if (response.Status.IsNotSuccess)
                {
                    return RedirectToAccessDenied();
                }

                var model = _labelMapper.MapLabelDetailModel(response.Item);

                return View(model);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var labelUid = id;
            if (labelUid.IsEmptyGuid())
            {
                return RedirectToAccessDenied();
            }

            var request = new LabelReadRequest(CurrentUser.Id, labelUid);
            var response = await _labelService.GetLabel(request);
            if (response.Status.IsNotSuccess)
            {
                return RedirectToAccessDenied();
            }

            var model = _labelMapper.MapLabelEditModel(response.Item);

            return View(model);
        }

        [HttpPost,
         JournalFilter(Message = "journal_label_edit")]
        public async Task<IActionResult> Edit(LabelEditModel model)
        {
            if (model.IsNotValid())
            {
                model.SetInputModelValues();
                return View(model);
            }

            var request = new LabelEditRequest(CurrentUser.Id, model.OrganizationUid, model.ProjectUid, model.LabelUid, model.Key,
                model.Description);
            var response = await _labelService.EditLabel(request);
            if (response.Status.IsNotSuccess)
            {
                model.MapMessages(response);
                model.SetInputModelValues();
                return View(model);
            }

            CurrentUser.IsActionSucceed = true;
            return Redirect($"/Label/Detail/{response.Item.Uid}");
        }

        [HttpPost,
         JournalFilter(Message = "journal_label_delete")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var labelUid = id;
            if (labelUid.IsEmptyGuid())
            {
                return Forbid();
            }

            var request = new LabelDeleteRequest(CurrentUser.Id, labelUid);
            var response = await _labelService.DeleteLabel(request);
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
            var labelUid = id;
            if (labelUid.IsEmptyGuid())
            {
                return RedirectToAccessDenied();
            }

            var request = new LabelReadRequest(CurrentUser.Id, labelUid);
            var response = await _labelService.GetLabel(request);
            if (response.Status.IsNotSuccess)
            {
                return RedirectToAccessDenied();
            }

            var model = _labelMapper.MapLabelCloneModel(response.Item);

            return View(model);
        }

        [HttpPost,
         JournalFilter(Message = "journal_label_clone")]
        public async Task<IActionResult> Clone(LabelCloneModel model)
        {
            if (model.IsNotValid())
            {
                model.SetInputModelValues();
                return View(model);
            }

            var request = new LabelCloneRequest(CurrentUser.Id, model.OrganizationUid, model.CloningLabelUid,
                model.Project, model.Key, model.Description);

            var response = await _labelService.CloneLabel(request);
            if (response.Status.IsNotSuccess)
            {
                model.MapMessages(response);
                model.SetInputModelValues();
                return View(model);
            }

            CurrentUser.IsActionSucceed = true;
            return Redirect($"/Label/Detail/{response.Item.Uid}");
        }

        [HttpGet]
        public ViewResult SearchList(string search)
        {
            var model = new LabelSearchListModel();
            model.SearchTerm = search;

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> SearchData(string search)
        {
            if (search.IsEmpty())
            {
                return Json(null);
            }

            var request = new LabelSearchListRequest(CurrentUser.Id, search);
            request.PagingInfo.Take = 6;

            var response = await _labelService.GetLabels(request);
            if (response.Status.IsNotSuccess)
            {
                return NotFound();
            }

            return Json(response.Items);
        }

        [HttpGet]
        public async Task<IActionResult> SearchListData(string search, int skip, int take)
        {
            if (search.IsEmpty())
            {
                return Json(null);
            }

            var request = new LabelSearchListRequest(CurrentUser.Id, search);
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
                stringBuilder.Append($"{result.PrepareLink($"/Label/Detail/{item.Uid.ToString()}", item.Key)}{DataResult.SEPARATOR}");
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
        public async Task<IActionResult> Revisions(Guid id)
        {
            var labelUid = id;
            if (labelUid.IsEmptyGuid())
            {
                return RedirectToHome();
            }

            var model = new LabelRevisionReadListModel();
            if (labelUid.IsNotEmptyGuid())
            {
                var request = new LabelReadRequest(CurrentUser.Id, labelUid);
                var response = await _labelService.GetLabel(request);
                if (response.Status.IsNotSuccess)
                {
                    return NotFound();
                }

                model.LabelUid = labelUid;
                model.LabelName = response.Item.Name;
                model.SetInputModelValues();
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> RevisionsData(Guid id)
        {
            var labelUid = id;
            if (labelUid.IsEmptyGuid())
            {
                return NotFound();
            }

            var request = new LabelRevisionReadListRequest(CurrentUser.Id, labelUid);

            var response = await _labelService.GetLabelRevisions(request);
            if (response.Status.IsNotSuccess)
            {
                return NotFound();
            }

            var result = DataResultHelper.GetLabelRevisionsData(response.Items);

            return Json(result);
        }

        [HttpPost,
         JournalFilter(Message = "journal_label_restore")]
        public async Task<IActionResult> Restore(Guid id, int revision)
        {
            var model = new CommonResult { IsOk = false };

            var labelUid = id;
            if (labelUid.IsEmptyGuid())
            {
                return Json(model);
            }

            if (revision < 1)
            {
                return Json(model);
            }

            var request = new LabelRestoreRequest(CurrentUser.Id, labelUid, revision);
            var response = await _labelService.RestoreLabel(request);
            if (response.Status.IsNotSuccess)
            {
                model.Messages = response.ErrorMessages;
                return Json(model);
            }

            model.IsOk = true;
            CurrentUser.IsActionSucceed = true;
            return Json(model);
        }

        [HttpPost,
         JournalFilter(Message = "journal_label_change_activation")]
        public async Task<IActionResult> ChangeActivation(Guid id, Guid organizationUid)
        {
            var model = new CommonResult { IsOk = false };

            var labelUid = id;
            if (labelUid.IsEmptyGuid()
                || organizationUid.IsEmptyGuid())
            {
                return Json(model);
            }

            var request = new LabelChangeActivationRequest(CurrentUser.Id, organizationUid, labelUid);
            var response = await _labelService.ChangeActivation(request);
            if (response.Status.IsNotSuccess)
            {
                return Json(model);
            }

            model.IsOk = true;
            CurrentUser.IsActionSucceed = true;
            return Json(model);
        }

        [HttpGet]
        public async Task<IActionResult> UploadLabelFromCSVFile(Guid id)
        {
            var projectUid = id;
            if (projectUid.IsEmptyGuid())
            {
                return RedirectToAccessDenied();
            }

            var request = new ProjectReadRequest(CurrentUser.Id, projectUid);
            var project = await _projectService.GetProject(request);
            if (project.Status.IsNotSuccess)
            {
                return RedirectToAccessDenied();
            }

            var model = _labelMapper.MapLabelUploadFromCSVModel(project.Item);

            return View(model);
        }

        [HttpPost,
         JournalFilter(Message = "journal_label_upload_from_csv")]
        public async Task<IActionResult> UploadLabelFromCSVFile(LabelUploadFromCSVModel model)
        {
            if (model.IsNotValid())
            {
                model.SetInputModelValues();
                return View(model);
            }

            var labelListInfos = new List<LabelListInfo>();

            var lines = new List<string>();
            using (var reader = new StreamReader(model.CSVFile.OpenReadStream()))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    lines.Add(line);
                }
            }

            for (var i = 1; i < lines.Count; i++)
            {
                var values = lines[i].Split(',');
                if (values.Length != 3)
                {
                    model.ErrorMessages.Add("file_has_more_columns_than_expected");
                    model.ErrorMessages.Add("error line : " + i);
                    model.SetInputModelValues();
                    return View(model);
                }

                labelListInfos.Add(new LabelListInfo
                {
                    LabelKey = values[0],
                    LanguageIsoCode2 = values[1],
                    Translation = values[2]
                });
            }

            var request = new LabelCreateListRequest(CurrentUser.Id, model.OrganizationUid, model.ProjectUid,
                                                     model.UpdateExistedTranslations, labelListInfos);
            var response = await _labelService.CreateLabelFromList(request);
            if (response.Status.IsNotSuccess)
            {
                model.MapMessages(response);
                model.SetInputModelValues();
                return View(model);
            }

            var doneModel = new LabelUploadFromCSVDoneModel();
            doneModel.MapMessages(response);
            doneModel.ProjectUid = model.ProjectUid;
            doneModel.ProjectName = model.ProjectName;
            doneModel.AddedLabelCount = response.AddedLabelCount;
            doneModel.CanNotAddedLabelCount = response.CanNotAddedLabelCount;
            doneModel.TotalLabelCount = response.TotalLabelCount;
            doneModel.AddedLabelTranslationCount = response.AddedLabelTranslationCount;
            doneModel.UpdatedLabelTranslationCount = response.UpdatedLabelTranslationCount;
            doneModel.CanNotAddedLabelTranslationCount = response.CanNotAddedLabelTranslationCount;
            doneModel.TotalRowsProcessed = lines.Count - 1;

            CurrentUser.IsActionSucceed = true;
            return View("UploadLabelFromCSVFileDone", doneModel);
        }

        [HttpGet]
        public async Task<IActionResult> CreateBulkLabel(Guid id)
        {
            var projectUid = id;
            if (projectUid.IsEmptyGuid())
            {
                return RedirectToAccessDenied();
            }

            var request = new ProjectReadRequest(CurrentUser.Id, projectUid);
            var project = await _projectService.GetProject(request);
            if (project.Status.IsNotSuccess)
            {
                return RedirectToAccessDenied();
            }

            var model = _labelMapper.MapCreateBulkLabelModel(project.Item);

            return View(model);
        }

        [HttpPost,
         JournalFilter(Message = "journal_create_bulk_label_from_csv")]
        public async Task<IActionResult> CreateBulkLabel(CreateBulkLabelModel model)
        {
            if (model.IsNotValid())
            {
                model.SetInputModelValues();
                return View(model);
            }

            var labelListInfos = new List<LabelListInfo>();

            var lines = model.BulkLabelData.Split('\n');
            for (var i = 1; i < lines.Length; i++)
            {
                var values = lines[i].Split(',');
                if (values.Length != 3)
                {
                    model.ErrorMessages.Add("file_has_more_columns_than_expected");
                    model.ErrorMessages.Add("error line : " + i);
                    model.SetInputModelValues();
                    return View(model);
                }

                labelListInfos.Add(new LabelListInfo
                {
                    LabelKey = values[0],
                    LanguageIsoCode2 = values[1],
                    Translation = values[2]
                });
            }

            var request = new LabelCreateListRequest(CurrentUser.Id, model.OrganizationUid, model.ProjectUid,
                                                     model.UpdateExistedTranslations, labelListInfos);
            var response = await _labelService.CreateLabelFromList(request);
            if (response.Status.IsNotSuccess)
            {
                model.MapMessages(response);
                model.SetInputModelValues();
                return View(model);
            }

            var doneModel = new CreateBulkLabelDoneModel();
            doneModel.MapMessages(response);
            doneModel.ProjectUid = model.ProjectUid;
            doneModel.ProjectName = model.ProjectName;
            doneModel.AddedLabelCount = response.AddedLabelCount;
            doneModel.CanNotAddedLabelCount = response.CanNotAddedLabelCount;
            doneModel.TotalLabelCount = response.TotalLabelCount;
            doneModel.AddedLabelTranslationCount = response.AddedLabelTranslationCount;
            doneModel.CanNotAddedLabelTranslationCount = response.CanNotAddedLabelTranslationCount;
            doneModel.UpdatedLabelTranslationCount = response.UpdatedLabelTranslationCount;
            doneModel.TotalRowsProcessed = lines.Length - 1;

            CurrentUser.IsActionSucceed = true;
            return View("CreateBulkLabelDone", doneModel);
        }

        [HttpGet]
        public async Task<IActionResult> Translate(string text, Guid target, Guid source)
        {
            var textToTranslate = text;
            var targetLanguageUid = target;
            var sourceLanguageUid = source;
            if (textToTranslate.IsEmpty()
                || targetLanguageUid.IsEmptyGuid()
                || sourceLanguageUid.IsEmptyGuid())
            {
                return Json(null);
            }

            var targetLanguageReadResponse = Languages.Find(x => x.Uid == targetLanguageUid);
            if (targetLanguageReadResponse == null)
            {
                return Json(null);
            }
            var targetLanguageIsoCode2 = targetLanguageReadResponse.IsoCode2;


            var sourceLanguageReadResponse = Languages.Find(x => x.Uid == sourceLanguageUid);
            if (sourceLanguageReadResponse == null)
            {
                return Json(null);
            }
            var sourceLanguageIsoCode2 = sourceLanguageReadResponse.IsoCode2;

            var request = new LabelGetTranslatedTextRequest(CurrentUser.Id, textToTranslate, targetLanguageIsoCode2,
                                                            sourceLanguageIsoCode2);

            var response = await _textTranslateIntegration.GetTranslatedText(request);
            if (response.Status.IsNotSuccess)
            {
                return Json(null);
            }

            return Json(response.Item.Name);
        }

        #endregion

        #region LabelTranslation

        [HttpGet]
        public async Task<IActionResult> LabelTranslationCreate(Guid id)
        {
            var labelUid = id;
            if (labelUid.IsEmptyGuid())
            {
                return RedirectToAccessDenied();
            }

            var request = new LabelReadRequest(CurrentUser.Id, labelUid);
            var response = await _labelService.GetLabel(request);
            if (response.Status.IsNotSuccess)
            {
                return RedirectToAccessDenied();
            }

            var projectReadRequest = new ProjectReadRequest(CurrentUser.Id, response.Item.ProjectUid);
            var projectReadResponse = await _projectService.GetProject(projectReadRequest);
            if (projectReadResponse.Status.IsNotSuccess)
            {
                return RedirectToAccessDenied();
            }

            var model = _labelMapper.MapLabelTranslationCreateModel(response.Item, projectReadResponse.Item);

            return View(model);
        }

        [HttpPost,
         JournalFilter(Message = "journal_label_translation_create")]
        public async Task<IActionResult> LabelTranslationCreate(LabelTranslationCreateModel model)
        {
            if (model.IsNotValid())
            {
                model.SetInputModelValues();
                return View(model);
            }

            var request = new LabelTranslationCreateRequest(CurrentUser.Id, model.OrganizationUid, model.LabelUid,
                model.LanguageUid, model.LabelTranslation);

            var response = await _labelService.CreateTranslation(request);
            if (response.Status.IsNotSuccess)
            {
                model.MapMessages(response);
                model.SetInputModelValues();
                return View(model);
            }

            CurrentUser.IsActionSucceed = true;
            return Redirect($"/Label/Detail/{response.Item.LabelUid}");
        }

        [HttpGet]
        public async Task<IActionResult> LabelTranslationDetail(Guid id)
        {
            var labelTranslationUid = id;
            if (labelTranslationUid.IsEmptyGuid())
            {
                return RedirectToHome();
            }

            var request = new LabelTranslationReadRequest(CurrentUser.Id, labelTranslationUid);
            var response = await _labelService.GetTranslation(request);
            if (response.Status.IsNotSuccess)
            {
                return RedirectToAccessDenied();
            }

            var model = _labelMapper.MapLabelTranslationDetailModel(response.Item);

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> LabelTranslationEdit(Guid id)
        {
            var labelTranslationUid = id;
            if (labelTranslationUid.IsEmptyGuid())
            {
                return RedirectToAccessDenied();
            }

            var request = new LabelTranslationReadRequest(CurrentUser.Id, labelTranslationUid);
            var response = await _labelService.GetTranslation(request);
            if (response.Status.IsNotSuccess)
            {
                return RedirectToAccessDenied();
            }

            var model = _labelMapper.MapLabelTranslationEditModel(response.Item);

            return View(model);
        }

        [HttpPost,
         JournalFilter(Message = "journal_label_translation_edit")]
        public async Task<IActionResult> LabelTranslationEdit(LabelTranslationEditModel model)
        {
            if (model.IsNotValid())
            {
                model.SetInputModelValues();
                return View(model);
            }

            var request = new LabelTranslationEditRequest(CurrentUser.Id, model.OrganizationUid,
                model.LabelTranslationUid, model.Translation);
            var response = await _labelService.EditTranslation(request);
            if (response.Status.IsNotSuccess)
            {
                model.MapMessages(response);
                model.SetInputModelValues();
                return View(model);
            }

            CurrentUser.IsActionSucceed = true;
            return Redirect($"/Label/Detail/{model.LabelUid}");
        }

        [HttpPost,
         JournalFilter(Message = "journal_label_translation_delete")]
        public async Task<IActionResult> LabelTranslationDelete(Guid id)
        {
            var labelTranslationUid = id;
            if (labelTranslationUid.IsEmptyGuid())
            {
                return Forbid();
            }

            var request = new LabelTranslationDeleteRequest(CurrentUser.Id, CurrentUser.OrganizationUid, labelTranslationUid);
            var response = await _labelService.DeleteTranslation(request);
            if (response.Status.IsNotSuccess)
            {
                return Json(new CommonResult { IsOk = false, Messages = response.ErrorMessages });
            }

            CurrentUser.IsActionSucceed = true;
            return Json(new CommonResult { IsOk = true });
        }

        [HttpGet]
        public async Task<IActionResult> LabelTranslationListData(Guid id, int skip, int take)
        {
            var labelUid = id;
            if (labelUid.IsEmptyGuid())
            {
                return Forbid();
            }

            var request = new LabelTranslationReadListRequest(CurrentUser.Id, labelUid);
            SetPaging(skip, take, request);

            var response = await _labelService.GetTranslations(request);
            if (response.Status.IsNotSuccess)
            {
                return NotFound();
            }

            var result = DataResultHelper.GetLabelTranslationListData(response.Items,CurrentUser.IsSuperAdmin);
            result.PagingInfo = response.PagingInfo;
            result.PagingInfo.PagingType = PagingInfo.PAGE_NUMBERS;

            return Json(result);
        }

        [HttpGet]
        public async Task<IActionResult> UploadLabelTranslationFromCSVFile(Guid id)
        {
            var labelUid = id;
            if (labelUid.IsEmptyGuid())
            {
                return RedirectToAccessDenied();
            }

            var request = new LabelReadRequest(CurrentUser.Id, labelUid);
            var label = await _labelService.GetLabel(request);
            if (label.Status.IsNotSuccess)
            {
                return RedirectToAccessDenied();
            }

            var model = _labelMapper.MapUploadLabelTranslationFromCSVFileModel(label.Item);

            return View(model);
        }

        [HttpPost,
         JournalFilter(Message = "journal_label_translation_upload_from_csv")]
        public async Task<IActionResult> UploadLabelTranslationFromCSVFile(UploadLabelTranslationFromCSVFileModel model)
        {
            if (model.IsNotValid())
            {
                model.SetInputModelValues();
                return View(model);
            }

            var translationListInfos = new List<TranslationListInfo>();

            var lines = new List<string>();
            using (var reader = new StreamReader(model.CSVFile.OpenReadStream()))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    lines.Add(line);
                }
            }

            for (var i = 1; i < lines.Count; i++)
            {
                var values = lines[i].Split(',');
                if (values.Length != 2)
                {
                    model.ErrorMessages.Add("file_has_more_columns_than_expected");
                    model.ErrorMessages.Add("error line : " + i);
                    model.SetInputModelValues();
                    return View(model);
                }

                translationListInfos.Add(new TranslationListInfo
                {
                    LanguageIsoCode2 = values[0],
                    Translation = values[1]
                });
            }

            var request = new LabelTranslationCreateListRequest(CurrentUser.Id, model.OrganizationUid, model.LabelUid, model.UpdateExistedTranslations,
                translationListInfos);
            var response = await _labelService.CreateTranslationFromList(request);
            if (response.Status.IsNotSuccess)
            {
                model.MapMessages(response);
                model.SetInputModelValues();
                return View(model);
            }

            var doneModel = new TranslationUploadFromCSVDoneModel();
            doneModel.MapMessages(response);
            doneModel.LabelUid = model.LabelUid;
            doneModel.LabelKey = model.LabelKey;
            doneModel.AddedTranslationCount = response.AddedTranslationCount;
            doneModel.UpdatedTranslationCount = response.UpdatedTranslationCount;
            doneModel.CanNotAddedTranslationCount = response.CanNotAddedTranslationCount;
            doneModel.TotalRowsProcessed = lines.Count - 1;

            CurrentUser.IsActionSucceed = true;
            return View("UploadLabelTranslationFromCSVFileDone", doneModel);
        }

        [HttpPost,
         JournalFilter(Message = "journal_label_download_translations")]
        public async Task<IActionResult> DownloadTranslations(Guid id)
        {
            var labelUid = id;
            if (labelUid.IsEmptyGuid())
            {
                return NoContent();
            }

            var labelRequest = new LabelTranslationReadListRequest(CurrentUser.Id, labelUid);
            var translations = await _labelService.GetTranslations(labelRequest);
            if (translations.Status.IsNotSuccess)
            {
                return NoContent();
            }

            var sb = new StringBuilder();

            sb.AppendLine("language,translation");
            for (var i = 0; i < translations.Items.Count; i++)
            {
                var item = translations.Items[i];
                sb.AppendLine(item.LanguageIsoCode2 + "," + item.Translation);
            }

            CurrentUser.IsActionSucceed = true;
            return File(Encoding.UTF8.GetBytes(sb.ToString()), "text/csv", "translations.csv");
        }

        [HttpPost,
         JournalFilter(Message = "journal_label_translation_restore")]
        public async Task<IActionResult> RestoreLabelTranslation(Guid id, int revision)
        {
            var model = new CommonResult { IsOk = false };

            var labelTranslationUid = id;
            if (labelTranslationUid.IsEmptyGuid())
            {
                return Json(model);
            }

            if (revision < 1)
            {
                return Json(model);
            }

            var request = new LabelTranslationRestoreRequest(CurrentUser.Id, labelTranslationUid, revision);
            var response = await _labelService.RestoreLabelTranslation(request);
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
        public async Task<IActionResult> LabelTranslationRevisions(Guid id)
        {
            var labelTranslationUid = id;
            if (labelTranslationUid.IsEmptyGuid())
            {
                return RedirectToHome();
            }

            var model = new LabelTranslationRevisionReadListModel();
            if (labelTranslationUid.IsNotEmptyGuid())
            {
                var request = new LabelTranslationReadRequest(CurrentUser.Id, labelTranslationUid);
                var response = await _labelService.GetTranslation(request);
                if (response.Status.IsNotSuccess)
                {
                    return NotFound();
                }

                model.LabelTranslationUid = labelTranslationUid;
                model.LabelTranslationName = response.Item.Name;
                model.SetInputModelValues();
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> LabelTranslationRevisionsData(Guid id)
        {
            var labelTranslationUid = id;
            if (labelTranslationUid.IsEmptyGuid())
            {
                return NotFound();
            }

            var request = new LabelTranslationRevisionReadListRequest(CurrentUser.Id, labelTranslationUid);

            var response = await _labelService.GetLabelTranslationRevisions(request);
            if (response.Status.IsNotSuccess)
            {
                return NotFound();
            }

            var result = DataResultHelper.GetLabelTranslationRevisionsData(response.Items);
            return Json(result);
        }

        #endregion      
    }
}