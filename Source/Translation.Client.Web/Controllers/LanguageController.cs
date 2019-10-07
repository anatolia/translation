using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

using Translation.Client.Web.Helpers;
using Translation.Client.Web.Helpers.ActionFilters;
using Translation.Client.Web.Helpers.Mappers;
using Translation.Client.Web.Models.Base;
using Translation.Client.Web.Models.Language;
using Translation.Common.Contracts;
using Translation.Common.Helpers;
using Translation.Common.Models.Requests.Language;
using Translation.Common.Models.Shared;

namespace Translation.Client.Web.Controllers
{
    public class LanguageController : BaseController
    {
        private readonly IWebHostEnvironment _environment;
        private readonly ILanguageService _languageService;


        [HttpGet]
        public IActionResult Create()
        {
            var model = new LanguageCreateModel();

            return View(model);
        }

        [HttpPost,
         JournalFilter(Message = "journal_language_create")]
        public async Task<IActionResult> Create(LanguageCreateModel model)
        {
            if (model.IsNotValid())
            {
                model.SetInputModelValues();
                return View(model);
            }

            var iconFileName = Path.Combine("images", "flags", model.IsoCode2.ToLower() + ".png");

            using (var fileStream = new FileStream(Path.Combine(_environment.WebRootPath, iconFileName), FileMode.Create))
            {
                await model.Icon.CopyToAsync(fileStream);
                fileStream.Close();
            }

            var request = new LanguageCreateRequest(CurrentUser.Id, model.Name, model.OriginalName, model.IsoCode2,
                                                    model.IsoCode3, iconFileName, model.Description);
            var response = await _languageService.CreateLanguage(request);
            if (response.Status.IsNotSuccess)
            {
                model.MapMessages(response);
                model.SetInputModelValues();
                return View(model);
            }

            CurrentUser.IsActionSucceed = true;
            return Redirect("/Language/List/");
        }

        [HttpGet]
        public async Task<IActionResult> Detail(Guid id)
        {
            var languageUid = id;
            if (languageUid.IsEmptyGuid())
            {
                return RedirectToAccessDenied();
            }

            var request = new LanguageReadRequest(CurrentUser.Id, languageUid);
            var response = await _languageService.GetLanguage(request);
            if (response.Status.IsNotSuccess)
            {
                return RedirectToAccessDenied();
            }

            var model = LanguageMapper.MapLanguageDetailModel(response.Item);

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var languageUid = id;
            if (languageUid.IsEmptyGuid())
            {
                return RedirectToAccessDenied();
            }

            var request = new LanguageReadRequest(CurrentUser.Id, languageUid);
            var response = await _languageService.GetLanguage(request);
            if (response.Status.IsNotSuccess)
            {
                return RedirectToAccessDenied();
            }

            var model = LanguageMapper.MapLanguageEditModel(response.Item);

            return View(model);
        }

        [HttpPost,
         JournalFilter(Message = "journal_language_edit")]
        public async Task<IActionResult> Edit(LanguageEditModel model)
        {
            if (model.IsNotValid())
            {
                model.SetInputModelValues();
                return View(model);
            }

            var path = Path.Combine("images", "flags", model.IsoCode2 + ".png");
            var icon = model.Icon;
            if (icon != null)
            {
                if (icon.ContentType != "image/png")
                {
                    model.ErrorMessages.Add("please_upload_png_file");
                }

                if ((System.IO.File.Exists(path)))
                {
                    System.IO.File.Delete(path);
                }

                var fileStream = new FileStream(Path.Combine(_environment.WebRootPath, path), FileMode.Create);
                icon.CopyTo(fileStream);
                fileStream.Close();
            }

            var request = new LanguageEditRequest(CurrentUser.Id, model.LanguageUid, model.Name, model.OriginalName,
                                                  model.IsoCode2, model.IsoCode3, path, model.Description);

            var response = await _languageService.EditLanguage(request);
            if (response.Status.IsNotSuccess)
            {
                model.MapMessages(response);
                model.SetInputModelValues();
                return View(model);
            }

            CurrentUser.IsActionSucceed = true;
            return Redirect($"/Language/Detail/{response.Item.Uid}");
        }

        [HttpGet]
        public ViewResult List()
        {
            var model = new LanguageListModel();

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> ListData(int skip, int take)
        {
            var request = new LanguageReadListRequest();
            SetPaging(skip, take, request);

            var response = await _languageService.GetLanguages(request);
            if (response.Status.IsNotSuccess)
            {
                return NotFound();
            }

            var result = new DataResult();
            result.AddHeaders("language_name", "2_char_code", "3_char_code", "icon", "");

            for (var i = 0; i < response.Items.Count; i++)
            {
                var item = response.Items[i];
                var stringBuilder = new StringBuilder();
                stringBuilder.Append($"{item.Uid}{DataResult.SEPARATOR}");
                stringBuilder.Append($"{result.PrepareLink("/Language/Detail/" + item.Uid, item.Name, true)}{DataResult.SEPARATOR}");
                stringBuilder.Append($"{item.IsoCode2}{DataResult.SEPARATOR}");
                stringBuilder.Append($"{item.IsoCode3}{DataResult.SEPARATOR}");
                stringBuilder.Append($"{result.PrepareImage($"{item.IconPath}", item.OriginalName)}{DataResult.SEPARATOR}");
                stringBuilder.Append($"{result.PrepareLink("/Language/Edit/" + item.Uid, "edit", true)}{DataResult.SEPARATOR}");

                result.Data.Add(stringBuilder.ToString());
            }

            result.PagingInfo = response.PagingInfo;
            result.PagingInfo.Type = PagingInfo.PAGE_NUMBERS;

            return Json(result);
        }

        [HttpGet]
        public async Task<IActionResult> SelectData(Guid lastUid, int take = 100, string q = "")
        {
            var request = new LanguageReadListRequest();
            request.PagingInfo.Take = take;

            var searchTerm = q?.ToLowerInvariant();
            if (searchTerm.IsNotEmpty())
            {
                request.SearchTerm = searchTerm;
            }

            if (lastUid.IsNotEmptyGuid())
            {
                request.PagingInfo.LastUid = lastUid;
            }

            var items = new List<SelectResult>();

            var response = await _languageService.GetLanguages(request);
            if (response.Status.IsNotSuccess)
            {
                return Json(items);
            }

            for (var i = 0; i < response.Items.Count; i++)
            {
                var item = response.Items[i];
                items.Add(new SelectResult(item.Uid.ToUidString(), item.Name, $"/images/flags/{item.IsoCode2}.png"));
            }

            return Json(items);
        }

        [HttpPost,
         JournalFilter(Message = "journal_language_restore")]
        public async Task<IActionResult> Restore(Guid id, int revision)
        {
            var model = new CommonResult { IsOk = false };

            var languageUid = id;
            if (languageUid.IsEmptyGuid())
            {
                return Json(model);
            }

            if (revision < 1)
            {
                return Json(model);
            }

            var request = new LanguageRestoreRequest(CurrentUser.Id, languageUid, revision);
            var response = await _languageService.RestoreLanguage(request);
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
        public async Task<IActionResult> Revisions(Guid id)
        {
            var languageUid = id;
            if (languageUid.IsEmptyGuid())
            {
                return RedirectToHome();
            }

            var model = new LanguageRevisionReadListModel();
            if (languageUid.IsNotEmptyGuid())
            {
                var request = new LanguageReadRequest(CurrentUser.Id, languageUid);
                var response = await _languageService.GetLanguage(request);
                if (response.Status.IsNotSuccess)
                {
                    return NotFound();
                }

                model.LanguageUid = languageUid;
                model.LanguageName = response.Item.Name;
                model.SetInputModelValues();
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> RevisionsData(Guid id)
        {
            var languageUid = id;
            if (languageUid.IsEmptyGuid())
            {
                return NotFound();
            }

            var request = new LanguageRevisionReadListRequest(CurrentUser.Id, languageUid);

            var response = await _languageService.GetLanguageRevisions(request);
            if (response.Status.IsNotSuccess)
            {
                return NotFound();
            }

            var result = new DataResult();
            result.AddHeaders("revision", "revisioned_by", "revisioned_at", "language_name", "2_char_code", "3_char_code", "icon", "created_at", "");

            for (var i = 0; i < response.Items.Count; i++)
            {
                var revisionItem = response.Items[i];
                var item = revisionItem.Item;
                var stringBuilder = new StringBuilder();
                stringBuilder.Append($"{item.Uid}{DataResult.SEPARATOR}");
                stringBuilder.Append($"{revisionItem.Revision}{DataResult.SEPARATOR}");
                stringBuilder.Append($"{revisionItem.RevisionedByName}{DataResult.SEPARATOR}");
                stringBuilder.Append($"{GetDateTimeAsString(revisionItem.RevisionedAt)}{DataResult.SEPARATOR}");
                stringBuilder.Append($"{item.Name}{DataResult.SEPARATOR}");
                stringBuilder.Append($"{item.IsoCode2}{DataResult.SEPARATOR}");
                stringBuilder.Append($"{item.IsoCode3}{DataResult.SEPARATOR}");
                stringBuilder.Append($"{result.PrepareImage($"{item.IconPath}", item.OriginalName)}{DataResult.SEPARATOR}");
                stringBuilder.Append($"{GetDateTimeAsString(item.CreatedAt)}{DataResult.SEPARATOR}");
                stringBuilder.Append($"{result.PrepareRestoreButton("restore", "/Language/Restore/", "/Language/Detail")}{DataResult.SEPARATOR}");

                result.Data.Add(stringBuilder.ToString());
            }

            return Json(result);
        }

        public LanguageController(IOrganizationService organizationService, IJournalService journalService, ILanguageService languageService, ITranslationProviderService translationProviderService, IWebHostEnvironment environment) : base(organizationService, journalService, languageService, translationProviderService)
        {
            _languageService = languageService;
            _environment = environment;
        }
    }
}
