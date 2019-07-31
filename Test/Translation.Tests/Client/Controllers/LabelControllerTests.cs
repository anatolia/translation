using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using NUnit.Framework;
using Shouldly;

using Translation.Client.Web.Controllers;
using Translation.Client.Web.Models.Label;
using Translation.Client.Web.Models.LabelTranslation;
using Translation.Tests.SetupHelpers;
using static Translation.Tests.TestHelpers.ActionMethodNameConstantTestHelper;
using static Translation.Tests.TestHelpers.FakeConstantTestHelper;
using static Translation.Tests.TestHelpers.AssertViewModelTestHelper;
using static Translation.Tests.TestHelpers.FakeModelTestHelper;
using Translation.Client.Web.Models.Base;

namespace Translation.Tests.Client.Controllers
{
    [TestFixture]
    public class LabelControllerTests : ControllerBaseTests
    {
        public LabelController SystemUnderTest { get; set; }

        [SetUp]
        public void run_before_every_test()
        {
            Refresh();
            SystemUnderTest = Container.Resolve<LabelController>();
            SetControllerContext(SystemUnderTest);
        }

        [TestCase(CreateAction, new[] { typeof(Guid) }, typeof(HttpGetAttribute)),
         TestCase(CreateAction, new[] { typeof(LabelCreateModel) }, typeof(HttpPostAttribute)),
         TestCase(DetailAction, new[] { typeof(Guid), typeof(string), typeof(string) }, typeof(HttpGetAttribute)),
         TestCase(EditAction, new[] { typeof(Guid) }, typeof(HttpGetAttribute)),
         TestCase(EditAction, new[] { typeof(LabelEditModel) }, typeof(HttpPostAttribute)),
         TestCase(CloneAction, new[] { typeof(Guid) }, typeof(HttpGetAttribute)),
         TestCase(CloneAction, new[] { typeof(LabelCloneModel) }, typeof(HttpPostAttribute)),
         TestCase(SearchListAction, new[] { typeof(string) }, typeof(HttpGetAttribute)),
         TestCase(SearchDataAction, new[] { typeof(string) }, typeof(HttpGetAttribute)),
         TestCase(SearchListDataAction, new[] { typeof(string), typeof(int), typeof(int) }, typeof(HttpGetAttribute)),
         TestCase(RevisionsAction, new[] { typeof(Guid) }, typeof(HttpGetAttribute)),
         TestCase(RevisionsDataAction, new[] { typeof(Guid) }, typeof(HttpGetAttribute)),
         TestCase(RestoreAction, new[] { typeof(Guid), typeof(int) }, typeof(HttpPostAttribute)),
         TestCase(ChangeActivationAction, new[] { typeof(Guid), typeof(Guid) }, typeof(HttpPostAttribute)),
         TestCase(UploadLabelFromCSVFileAction, new[] { typeof(Guid) }, typeof(HttpGetAttribute)),
         TestCase(UploadLabelFromCSVFileAction, new[] { typeof(LabelUploadFromCSVModel) }, typeof(HttpPostAttribute)),
         TestCase(DownloadSampleCSVFileForBulkLabelUploadAction, new Type[] { }, typeof(HttpGetAttribute)),
         TestCase(CreateBulkLabelAction, new[] { typeof(Guid) }, typeof(HttpGetAttribute)),
         TestCase(CreateBulkLabelAction, new[] { typeof(CreateBulkLabelModel) }, typeof(HttpPostAttribute)),
         TestCase(TranslateAction, new[] { typeof(string), typeof(Guid) }, typeof(HttpGetAttribute)),
         TestCase(LabelTranslationCreateAction, new[] { typeof(Guid) }, typeof(HttpGetAttribute)),
         TestCase(LabelTranslationCreateAction, new[] { typeof(LabelTranslationCreateModel) }, typeof(HttpPostAttribute)),
         TestCase(LabelTranslationDetailAction, new[] { typeof(Guid) }, typeof(HttpGetAttribute)),
         TestCase(LabelTranslationEditAction, new[] { typeof(Guid) }, typeof(HttpGetAttribute)),
         TestCase(LabelTranslationEditAction, new[] { typeof(LabelTranslationEditModel) }, typeof(HttpPostAttribute)),
         TestCase(LabelTranslationDeleteAction, new[] { typeof(Guid) }, typeof(HttpPostAttribute)),
         TestCase(LabelTranslationListDataAction, new[] { typeof(Guid), typeof(int), typeof(int) }, typeof(HttpGetAttribute)),
         TestCase(UploadLabelTranslationFromCSVFileAction, new[] { typeof(Guid) }, typeof(HttpGetAttribute)),
         TestCase(UploadLabelTranslationFromCSVFileAction, new[] { typeof(UploadLabelTranslationFromCSVFileModel) }, typeof(HttpPostAttribute)),
         TestCase(DownloadSampleCSVFileForBulkLabelTranslationUploadAction, new Type[] { }, typeof(HttpGetAttribute)),
         TestCase(DownloadTranslationsAction, new[] { typeof(Guid) }, typeof(HttpPostAttribute)),
         TestCase(RestoreLabelTranslationAction, new[] { typeof(Guid), typeof(int) }, typeof(HttpPostAttribute)),
         TestCase(LabelTranslationRevisionsAction, new[] { typeof(Guid) }, typeof(HttpGetAttribute)),
         TestCase(LabelTranslationRevisionsDataAction, new[] { typeof(Guid) }, typeof(HttpGetAttribute))]
        public void Methods_Has_Http_Verb_Attributes(string actionMethod, Type[] parameters, Type httpVerbAttribute)
        {
            var type = SystemUnderTest.GetType();
            var methodInfo = type.GetMethod(actionMethod, parameters);
            var attributes = methodInfo.GetCustomAttributes(httpVerbAttribute, true);
            Assert.AreEqual(attributes.Length, 1);
        }

        [Test]
        public void Controller_Derived_From_BaseController()
        {
            var type = SystemUnderTest.GetType();
            type.BaseType.Name.StartsWith("BaseController").ShouldBeTrue();
        }

        [Test]
        public async Task Create_GET()
        {
            // arrange
            MockProjectService.Setup_GetProject_Returns_ProjectReadResponse_Success();

            // act
            var result = await SystemUnderTest.Create(OrganizationOneProjectOneUid);

            // assert
            AssertViewWithModel<LabelCreateModel>(result);
            MockProjectService.Verify_GetProject();
        }

        [Test]
        public async Task Create_GET_FailedResponse()
        {
            // arrange
            MockProjectService.Setup_GetProject_Returns_ProjectReadResponse_Failed();

            // act
            var result = await SystemUnderTest.Create(OrganizationOneProjectOneUid);

            // assert
            AssertViewAccessDenied(result);
            MockProjectService.Verify_GetProject();
        }

        [Test]
        public async Task Create_GET_InvalidResponse()
        {
            // arrange
            MockProjectService.Setup_GetProject_Returns_ProjectReadResponse_Invalid();

            // act
            var result = await SystemUnderTest.Create(OrganizationOneProjectOneUid);

            // assert
            AssertViewAccessDenied(result);
            MockProjectService.Verify_GetProject();
        }

        [Test]
        public async Task Create_GET_InvalidParameter()
        {
            // arrange

            // act
            var result = await SystemUnderTest.Create(EmptyUid);

            // assert


        }

        [Test]
        public async Task Create_POST()
        {
            // arrange
            MockLabelService.Setup_CreateLabel_Returns_LabelCreateResponse_Success();
            var model = GetLabelCreateModel();

            // act
            var result = await SystemUnderTest.Create(model);

            // assert
            ((RedirectResult)result).Url.ShouldBe($"/Label/Detail/{EmptyUid}");
            MockLabelService.Verify_CreateLabel();
        }

        [Test]
        public async Task Create_POST_FailedResponse()
        {
            // arrange
            MockLabelService.Setup_CreateLabel_Returns_LabelCreateResponse_Failed();
            var model = GetLabelCreateModel();

            // act
            var result = await SystemUnderTest.Create(model);

            // assert
            AssertErrorMessagesForInvalidOrFailedResponse<LabelCreateModel>(result);
            MockLabelService.Verify_CreateLabel();
        }

        [Test]
        public async Task Create_POST_InvalidResponse()
        {
            // arrange
            MockLabelService.Setup_CreateLabel_Returns_LabelCreateResponse_Invalid();
            var model = GetLabelCreateModel();

            // act
            var result = await SystemUnderTest.Create(model);

            // assert
            AssertErrorMessagesForInvalidOrFailedResponse<LabelCreateModel>(result);
            MockLabelService.Verify_CreateLabel();
        }

        [Test]
        public async Task Create_POST_InvalidModel()
        {
            // arrange
            var model = new LabelCreateModel();

            // act
            var result = await SystemUnderTest.Create(model);

            // assert
            AssertInputErrorMessagesOfView(result, model);
        }

        [Test]
        public async Task Detail_GET_ProjectSlugLabelKey()
        {
            // arrange 
            MockProjectService.Setup_GetProjectBySlug_Returns_ProjectReadBySlugResponse_Success();
            MockLabelService.Setup_GetLabelByKey_Returns_LabelReadByKeyResponse_Success();

            // act
            var result = await SystemUnderTest.Detail(EmptyUid, StringOne, StringTwo);

            // assert
            AssertViewWithModel<LabelDetailModel>(result);
            MockProjectService.Verify_GetProjectBySlug();
            MockLabelService.Verify_GetLabelByKey();
        }

        [Test]
        public async Task Detail_GET_LabelUid()
        {
            // arrange
            MockLabelService.Setup_GetLabel_Returns_LabelReadResponse_Success();

            // act
            var result = await SystemUnderTest.Detail(UidOne, EmptySlug, EmptyString);

            // assert
            AssertViewWithModel<LabelDetailModel>(result);
            MockLabelService.Verify_GetLabel();
        }

        [Test]
        public async Task Detail_GET_FailedProjectReadBySlugResponse()
        {
            // arrange 
            MockProjectService.Setup_GetProjectBySlug_Returns_ProjectReadBySlugResponse_Failed();


            // act
            var result = await SystemUnderTest.Detail(EmptyUid, SlugOne, StringOne);

            // assert
            AssertViewAccessDenied(result);
            MockProjectService.Verify_GetProjectBySlug();

        }

        [Test]
        public async Task Detail_GET_FailedLabelReadByKeyResponse()
        {
            // arrange 
            MockProjectService.Setup_GetProjectBySlug_Returns_ProjectReadBySlugResponse_Success();
            MockLabelService.Setup_GetLabelByKey_Returns_LabelReadByKeyResponse_Failed();

            // act
            var result = await SystemUnderTest.Detail(EmptyUid, SlugOne, StringOne);

            // assert
            AssertViewAccessDenied(result);
            MockProjectService.Verify_GetProjectBySlug();
            MockLabelService.Verify_GetLabelByKey();
        }

        [Test]
        public async Task Detail_GET_InvalidProjectReadBySlugResponse()
        {
            // arrange 
            MockProjectService.Setup_GetProjectBySlug_Returns_ProjectReadBySlugResponse_Invalid();

            // act
            var result = await SystemUnderTest.Detail(EmptyUid, SlugOne, StringOne);

            // assert
            AssertViewAccessDenied(result);
            MockProjectService.Verify_GetProjectBySlug();
        }

        [Test]
        public async Task Detail_GET_InvalidLabelReadByKeyResponse()
        {
            // arrange 
            MockProjectService.Setup_GetProjectBySlug_Returns_ProjectReadBySlugResponse_Success();
            MockLabelService.Setup_GetLabelByKey_Returns_LabelReadByKeyResponse_Invalid();

            // act
            var result = await SystemUnderTest.Detail(EmptyUid, SlugOne, StringOne);

            // assert
            AssertViewAccessDenied(result);
            MockProjectService.Verify_GetProjectBySlug();
            MockLabelService.Verify_GetLabelByKey();
        }

        [Test]
        public async Task Detail_GET_InvalidLabelReadResponse()
        {
            // arrange 
            MockLabelService.Setup_GetLabel_Returns_LabelReadResponse_Invalid();

            // act
            var result = await SystemUnderTest.Detail(UidOne, StringOne, StringOne);

            // assert
            AssertViewAccessDenied(result);
            MockLabelService.Verify_GetLabel();
        }

        [Test]
        public async Task Detail_GET_InvalidParameter_RedirectToHome()
        {
            // arrange

            // act
            var result = await SystemUnderTest.Detail(EmptyUid, EmptySlug, EmptyString);

            // assert
            AssertViewRedirectToHome(result);
        }

        [Test]
        public async Task Detail_GET_ProjectSlugOrLabelKeyInvalidParameter_RedirectToHome()
        {
            // arrange

            // act
            var result = await SystemUnderTest.Detail(EmptyUid, EmptySlug, EmptyString);

            // assert
            AssertViewRedirectToHome(result);
        }

        [Test]
        public async Task Edit_GET()
        {
            // arrange 
            MockLabelService.Setup_GetLabel_Returns_LabelReadResponse_Success();

            // act
            var result = await SystemUnderTest.Edit(OrganizationOneProjectOneUid);

            // assert
            AssertViewWithModel<LabelEditModel>(result);
            MockLabelService.Verify_GetLabel();
        }

        [Test]
        public async Task Edit_GET_FailedResponse()
        {
            // arrange 
            MockLabelService.Setup_GetLabel_Returns_LabelReadResponse_Failed();

            // act
            var result = await SystemUnderTest.Edit(OrganizationOneProjectOneUid);

            // assert
            AssertViewAccessDenied(result);
            MockLabelService.Verify_GetLabel();
        }

        [Test]
        public async Task Edit_GET_InvalidResponse()
        {
            // arrange 
            MockLabelService.Setup_GetLabel_Returns_LabelReadResponse_Invalid();

            // act
            var result = await SystemUnderTest.Edit(OrganizationOneProjectOneUid);

            // assert
            AssertViewAccessDenied(result);
            MockLabelService.Verify_GetLabel();
        }

        [Test]
        public async Task Edit_GET_InvalidParameter()
        {
            // arrange

            // act
            var result = await SystemUnderTest.Edit(EmptyUid);

            // assert
            AssertViewAccessDenied(result);
        }

        [Test]
        public async Task Edit_POST()
        {
            // arrange 
            MockLabelService.Setup_EditLabel_Returns_LabelEditResponse_Success();
            var model = GetLabelEditModel();

            // act
            var result = await SystemUnderTest.Edit(model);

            // assert
            ((RedirectResult)result).Url.ShouldBe($"/Label/Detail/{UidOne}");
            MockLabelService.Verify_EditLabel();
        }

        [Test]
        public async Task Edit_POST_FailedResponse()
        {
            // arrange 
            MockLabelService.Setup_EditLabel_Returns_LabelEditResponse_Failed();
            var model = GetLabelEditModel();

            // act
            var result = await SystemUnderTest.Edit(model);

            // assert
            AssertErrorMessagesForInvalidOrFailedResponse<LabelEditModel>(result);
            MockLabelService.Verify_EditLabel();
        }

        [Test]
        public async Task Edit_POST_InvalidResponse()
        {
            // arrange 
            MockLabelService.Setup_EditLabel_Returns_LabelEditResponse_Invalid();
            var model = GetLabelEditModel();

            // act
            var result = await SystemUnderTest.Edit(model);

            // assert
            AssertErrorMessagesForInvalidOrFailedResponse<LabelEditModel>(result);
            MockLabelService.Verify_EditLabel();
        }

        [Test]
        public async Task Edit_POST_InvalidModel()
        {
            // arrange
            var model = new LabelEditModel();

            // act
            var result = await SystemUnderTest.Edit(model);

            // assert
            AssertInputErrorMessagesOfView(result, model);
        }

        [Test]
        public async Task Delete_POST()
        {
            // arrange
            MockLabelService.Setup_DeleteLabel_Returns_LabelDeleteResponse_Success();

            // act
            var result = (JsonResult)await SystemUnderTest.Delete(UidOne);

            // assert
            ((CommonResult)result.Value).IsOk.ShouldBe(true);
            MockLabelService.Verify_DeleteLabel();
        }

        [Test]
        public async Task Delete_POST_FailedResponse()
        {
            // arrange
            MockLabelService.Setup_DeleteLabel_Returns_LabelDeleteResponse_Failed();

            // act
            var result = await SystemUnderTest.Delete(UidOne);

            // assert
            var commonResult = ((CommonResult)((JsonResult)result).Value);
            commonResult.IsOk.ShouldBeFalse();
            commonResult.Messages.Any(x => x == StringOne);
            MockLabelService.Verify_DeleteLabel();
        }

        [Test]
        public async Task Delete_POST_InvalidResponse()
        {
            // arrange
            MockLabelService.Setup_DeleteLabel_Returns_LabelDeleteResponse_Invalid();

            // act
            var result = await SystemUnderTest.Delete(UidOne);

            // assert
            var commonResult = ((CommonResult)((JsonResult)result).Value);
            commonResult.IsOk.ShouldBeFalse();
            commonResult.Messages.Any(x => x == StringOne);
            MockLabelService.Verify_DeleteLabel();
        }

        [Test]
        public async Task Delete_POST_InvalidParameter()
        {
            // arrange

            // act
            var result = await SystemUnderTest.Delete(EmptyUid);

            // assert
            AssertView<ForbidResult>(result);
        }

        [Test]
        public async Task Clone_GET()
        {
            // arrange 
            MockLabelService.Setup_GetLabel_Returns_LabelReadResponse_Success();

            // act
            var result = await SystemUnderTest.Clone(UidOne);

            // assert
            AssertViewWithModel<LabelCloneModel>(result);
            MockLabelService.Verify_GetLabel();
        }

        [Test]
        public async Task Clone_GET_FailedResponse()
        {
            // arrange 
            MockLabelService.Setup_GetLabel_Returns_LabelReadResponse_Failed();

            // act
            var result = await SystemUnderTest.Clone(UidOne);

            // assert
            AssertViewAccessDenied(result);
            MockLabelService.Verify_GetLabel();
        }

        [Test]
        public async Task Clone_GET_InvalidResponse()
        {
            // arrange 
            MockLabelService.Setup_GetLabel_Returns_LabelReadResponse_Invalid();

            // act
            var result = await SystemUnderTest.Clone(UidOne);

            // assert
            AssertViewAccessDenied(result);
            MockLabelService.Verify_GetLabel();
        }

        [Test]
        public async Task Clone_GET_InvalidParameter()
        {
            // arrange

            // act
            var result = await SystemUnderTest.Clone(EmptyUid);

            // assert
            AssertViewAccessDenied(result);
        }

        [Test]
        public async Task Clone_POST()
        {
            // arrange
            MockLabelService.Setup_CloneLabel_Returns_LabelCloneResponse_Success();
            var model = GetLabelCloneModel();

            // act
            var result = await SystemUnderTest.Clone(model);

            // assert
            ((RedirectResult)result).Url.ShouldBe($"/Label/Detail/{EmptyUid}");
            MockLabelService.Verify_CloneLabel();
        }

        [Test]
        public async Task Clone_POST_FailedResponse()
        {
            // arrange
            MockLabelService.Setup_CloneLabel_Returns_LabelCloneResponse_Failed();
            var model = GetLabelCloneModel();

            // act
            var result = await SystemUnderTest.Clone(model);

            // assert
            AssertErrorMessagesForInvalidOrFailedResponse<LabelCloneModel>(result);
            MockLabelService.Verify_CloneLabel();
        }

        [Test]
        public async Task Clone_POST_InvalidResponse()
        {
            // arrange
            MockLabelService.Setup_CloneLabel_Returns_LabelCloneResponse_Invalid();
            var model = GetLabelCloneModel();

            // act
            var result = await SystemUnderTest.Clone(model);

            // assert
            AssertErrorMessagesForInvalidOrFailedResponse<LabelCloneModel>(result);
            MockLabelService.Verify_CloneLabel();
        }

        [Test]
        public async Task Clone_POST_InvalidModel()
        {
            // arrange
            var model = new LabelCloneModel();

            // act
            var result = await SystemUnderTest.Clone(model);

            // assert
            AssertInputErrorMessagesOfView(result, model);
        }

        [Test]
        public void SearchList_GET()
        {
            // arrange

            // act
            var result = SystemUnderTest.SearchList(StringOne);

            // assert
            AssertViewWithModel<LabelSearchListModel>(result);
        }

        [Test]
        public async Task SearchData_GET()
        {
            // arrange
            MockLabelService.Setup_GetLabels_Returns_LabelSearchListResponse_Success();

            // act
            var result = await SystemUnderTest.SearchData(StringOne);

            // assert
            AssertView<JsonResult>(result);
            MockLabelService.Verify_GetLabels_LabelSearchListRequest();
        }

        [Test]
        public async Task SearchData_GET_FailedResponse()
        {
            // arrange
            MockLabelService.Setup_GetLabels_Returns_LabelSearchListResponse_Failed();

            // act
            var result = await SystemUnderTest.SearchData(StringOne);

            // assert
            AssertView<NotFoundResult>(result);
            MockLabelService.Verify_GetLabels_LabelSearchListRequest();
        }

        [Test]
        public async Task SearchData_GET_InvalidResponse()
        {
            // arrange
            MockLabelService.Setup_GetLabels_Returns_LabelSearchListResponse_Invalid();

            // act
            var result = await SystemUnderTest.SearchData(StringOne);

            // assert
            AssertView<NotFoundResult>(result);
            MockLabelService.Verify_GetLabels_LabelSearchListRequest();
        }

        [Test]
        public async Task SearchData_GET_InvalidParameter()
        {
            // arrange

            // act
            var result = await SystemUnderTest.SearchData(EmptyString);

            // assert
            AssertView<JsonResult>(result);
        }

        [Test]
        public async Task SearchListData_GET()
        {
            // arrange
            MockLabelService.Setup_GetLabels_Returns_LabelSearchListResponse_Success();

            // act
            var result = await SystemUnderTest.SearchListData(StringOne, One, Two);

            // assert
            AssertView<JsonResult>(result);
            MockLabelService.Verify_GetLabels_LabelSearchListRequest();
        }

        [Test]
        public async Task SearchListData_GET_FailedResponse()
        {
            // arrange
            MockLabelService.Setup_GetLabels_Returns_LabelSearchListResponse_Failed();

            // act
            var result = await SystemUnderTest.SearchListData(StringOne, One, Two);

            // assert
            AssertView<NotFoundResult>(result);
            MockLabelService.Verify_GetLabels_LabelSearchListRequest();
        }

        [Test]
        public async Task SearchListData_GET_InvalidResponse()
        {
            // arrange
            MockLabelService.Setup_GetLabels_Returns_LabelSearchListResponse_Invalid();

            // act
            var result = await SystemUnderTest.SearchListData(StringOne, One, Two);

            // assert
            AssertView<NotFoundResult>(result);
            MockLabelService.Verify_GetLabels_LabelSearchListRequest();
        }

        [Test]
        public async Task SearchListData_GET_InvalidParameter()
        {
            // arrange

            // act
            var result = await SystemUnderTest.SearchListData(EmptyString, One, Two);

            // assert
            AssertView<JsonResult>(result);
        }

        [Test]
        public async Task Revisions_GET()
        {
            // arrange
            MockLabelService.Setup_GetLabel_Returns_LabelReadResponse_Success();

            // act
            var result = await SystemUnderTest.Revisions(UidOne);

            // assert
            AssertViewWithModel<LabelRevisionReadListModel>(result);
            MockLabelService.Verify_GetLabel();
        }

        [Test]
        public async Task Revisions_GET_FailedResponse()
        {
            // arrange
            MockLabelService.Setup_GetLabel_Returns_LabelReadResponse_Failed();

            // act
            var result = await SystemUnderTest.Revisions(UidOne);

            // assert
            AssertView<NotFoundResult>(result);
            MockLabelService.Verify_GetLabel();

        }

        [Test]
        public async Task Revisions_GET_InvalidResponse()
        {
            // arrange
            MockLabelService.Setup_GetLabel_Returns_LabelReadResponse_Invalid();

            // act
            var result = await SystemUnderTest.Revisions(UidOne);

            // assert
            AssertView<NotFoundResult>(result);
            MockLabelService.Verify_GetLabel();

        }

        [Test]
        public async Task Revisions_GET_InvalidParameter_AssertViewRedirectToHome()
        {
            // arrange

            // act
            var result = await SystemUnderTest.Revisions(EmptyUid);

            // assert
            AssertViewRedirectToHome(result);
        }

        [Test]
        public void RevisionsData_GET()
        {
            // arrange
            MockLabelService.Setup_GetLabelRevisions_Returns_LabelRevisionReadListResponse_Success();

            // act
            var result = SystemUnderTest.RevisionsData(UidOne);

            // assert
            AssertViewAndHeaders(result, new[] { "revision", "revisioned_by", "revisioned_at", "label_name", "is_active", "created_at", "" });
            MockLabelService.Verify_GetLabelRevisions();
        }

        [Test]
        public async Task RevisionsData_GET_FailedResponse()
        {
            // arrange
            MockLabelService.Setup_GetLabelRevisions_Returns_LabelRevisionReadListResponse_Failed();

            // act
            var result = await SystemUnderTest.RevisionsData(UidOne);

            // assert
            AssertView<NotFoundResult>(result);
            MockLabelService.Verify_GetLabelRevisions();
        }

        [Test]
        public async Task RevisionsData_GET_InvalidResponse()
        {
            // arrange
            MockLabelService.Setup_GetLabelRevisions_Returns_LabelRevisionReadListResponse_Invalid();

            // act
            var result = await SystemUnderTest.RevisionsData(UidOne);

            // assert
            AssertView<NotFoundResult>(result);
            MockLabelService.Verify_GetLabelRevisions();
        }

        [Test]
        public async Task RevisionsData_GET_InvalidParameter()
        {
            // arrange

            // act
            var result = await SystemUnderTest.RevisionsData(EmptyUid);

            // assert
            AssertView<NotFoundResult>(result);
        }

        [Test]
        public async Task Restore_POST()
        {
            // arrange
            MockLabelService.Setup_RestoreLabel_Returns_LabelRestoreResponse_Success();

            // act
            var result = (JsonResult)await SystemUnderTest.Restore(UidOne, One);

            // assert
            ((CommonResult)result.Value).IsOk.ShouldBe(true);
            MockLabelService.Verify_RestoreLabel();
        }

        [Test]
        public async Task Restore_POST_FailedResponse()
        {
            // arrange
            MockLabelService.Setup_RestoreLabel_Returns_LabelRestoreResponse_Failed();

            // act
            var result = await SystemUnderTest.Restore(UidOne, One);

            // assert
            var commonResult = ((CommonResult)((JsonResult)result).Value);
            commonResult.IsOk.ShouldBeFalse();
            commonResult.Messages.Any(x => x == StringOne);
            MockLabelService.Verify_RestoreLabel();
        }

        [Test]
        public async Task Restore_POST_InvalidResponse()
        {
            // arrange
            MockLabelService.Setup_RestoreLabel_Returns_LabelRestoreResponse_Invalid();

            // act
            var result = await SystemUnderTest.Restore(UidOne, One);

            // assert
            var commonResult = ((CommonResult)((JsonResult)result).Value);
            commonResult.IsOk.ShouldBeFalse();
            commonResult.Messages.Any(x => x == StringOne);
            MockLabelService.Verify_RestoreLabel();
        }

        [Test]
        public async Task Restore_POST_InvalidParameter()
        {
            // arrange

            // act
            var result = (JsonResult)await SystemUnderTest.Restore(EmptyUid, One);

            // assert
            ((CommonResult)result.Value).IsOk.ShouldBe(false);
        }

        [Test]
        public void ChangeActivation_POST()
        {
            // arrange
            MockLabelService.Setup_ChangeActivationForLabel_Returns_LabelChangeActivationResponse_Success();

            // act
            var result = SystemUnderTest.ChangeActivation(OrganizationOneProjectOneUid, OrganizationOneProjectOneUid);

            // assert
            AssertView<JsonResult>(result);
            MockLabelService.Verify_ChangeActivationForLabel();
        }

        [Test]
        public async Task ChangeActivation_POST_FailedResponse()
        {
            // arrange
            MockLabelService.Setup_ChangeActivationForLabel_Returns_LabelChangeActivationResponse_Failed();

            // act
            var result = (JsonResult)await SystemUnderTest.ChangeActivation(OrganizationOneProjectOneUid, OrganizationOneProjectOneUid);

            // assert
            ((CommonResult)result.Value).IsOk.ShouldBe(false);
            MockLabelService.Verify_ChangeActivationForLabel();
        }

        [Test]
        public async Task ChangeActivation_POST_InvalidResponse()
        {
            // arrange
            MockLabelService.Setup_ChangeActivationForLabel_Returns_LabelChangeActivationResponse_Invalid();

            // act
            var result = (JsonResult)await SystemUnderTest.ChangeActivation(OrganizationOneProjectOneUid, OrganizationOneProjectOneUid);

            // assert
            ((CommonResult)result.Value).IsOk.ShouldBe(false);
            MockLabelService.Verify_ChangeActivationForLabel();
        }

        [Test]
        public async Task ChangeActivation_POST_InvalidParameter()
        {
            // arrange

            // act
            var result = (JsonResult)await SystemUnderTest.ChangeActivation(EmptyUid, EmptyUid);

            // assert
            ((CommonResult)result.Value).IsOk.ShouldBe(false);
        }

        [Test]
        public async Task UploadLabelFromCSVFile_GET()
        {
            // arrange
            MockProjectService.Setup_GetProject_Returns_ProjectReadResponse_Success();

            // act
            var result = await SystemUnderTest.UploadLabelFromCSVFile(UidOne);

            // assert
            AssertViewWithModel<LabelUploadFromCSVModel>(result);
            MockProjectService.Verify_GetProject();
        }

        [Test]
        public async Task UploadLabelFromCSVFile_GET_FailedResponse()
        {
            // arrange
            MockProjectService.Setup_GetProject_Returns_ProjectReadResponse_Failed();

            // act
            var result = await SystemUnderTest.UploadLabelFromCSVFile(UidOne);

            // assert
            AssertViewAccessDenied(result);
            MockProjectService.Verify_GetProject();
        }

        [Test]
        public async Task UploadLabelFromCSVFile_GET_InvalidResponse()
        {
            // arrange
            MockProjectService.Setup_GetProject_Returns_ProjectReadResponse_Invalid();

            // act
            var result = await SystemUnderTest.UploadLabelFromCSVFile(UidOne);

            // assert
            AssertViewAccessDenied(result);
            MockProjectService.Verify_GetProject();
        }

        [Test]
        public async Task UploadLabelFromCSVFile_GET_InvalidParameter_RedirectToAccessDenied()
        {
            // arrange

            // act
            var result = await SystemUnderTest.UploadLabelFromCSVFile(EmptyUid);

            // assert
            AssertViewAccessDenied(result);
        }

        [Test]
        public async Task UploadLabelFromCSVFile_POST()
        {
            // arrange
            MockLabelService.Setup_CreateLabelFromList_Returns_LabelCreateListResponse_Success();
            var model = GetLabelUploadFromCSVModel(3);

            // act
            var result = await SystemUnderTest.UploadLabelFromCSVFile(model);

            // assert

            AssertViewWithModelAndMessage<LabelUploadFromCSVDoneModel>("UploadLabelFromCSVFileDone", result);
            MockLabelService.Verify_CreateLabelFromList();
        }

        [Test]
        public async Task UploadLabelFromCSVFile_POST_FailedResponse()
        {
            // arrange 
            MockLabelService.Setup_CreateLabelFromList_Returns_LabelCreateListResponse_Failed();
            var model = GetLabelUploadFromCSVModel(3);

            // act
            var result = await SystemUnderTest.UploadLabelFromCSVFile(model);

            // assert
            AssertErrorMessagesForInvalidOrFailedResponse<LabelUploadFromCSVModel>(result);
            MockLabelService.Verify_CreateLabelFromList();
        }

        [Test]
        public async Task UploadLabelFromCSVFile_POST_InvalidResponse()
        {
            // arrange 
            MockLabelService.Setup_CreateLabelFromList_Returns_LabelCreateListResponse_Invalid();
            var model = GetLabelUploadFromCSVModel(3);

            // act
            var result = await SystemUnderTest.UploadLabelFromCSVFile(model);

            // assert
            AssertErrorMessagesForInvalidOrFailedResponse<LabelUploadFromCSVModel>(result);
            MockLabelService.Verify_CreateLabelFromList();
        }

        [Test]
        public async Task UploadLabelFromCSVFile_POST_Failed_FileHasMoreColumnsThanExpected()
        {
            // arrange 
            var model = GetLabelUploadFromCSVModel(5);

            // act
            var result = await SystemUnderTest.UploadLabelFromCSVFile(model);

            // assert
            AssertErrorMessagesForInvalidOrFailedResponse<LabelUploadFromCSVModel>("file_has_more_columns_than_expected", result);
        }

        [Test]
        public async Task UploadLabelFromCSVFile_POST_InvalidParameter()
        {
            // arrange
            var model = new LabelUploadFromCSVModel();

            // act
            var result = await SystemUnderTest.UploadLabelFromCSVFile(model);

            // assert
            AssertInputErrorMessagesOfView(result, model);
        }

        [Test]
        public void DownloadSampleCSVFileForBulkLabelUpload_GET()
        {
            // arrange
            MockHostingEnvironment.Setup_WebRootPath_Returns_TestWebRootPath();

            // act
            var result = SystemUnderTest.DownloadSampleCSVFileForBulkLabelUpload();

            // assert
            AssertView<FileResult>(result);
        }

        [Test]
        public async Task CreateBulkLabel_GET()
        {
            // arrange
            MockProjectService.Setup_GetProject_Returns_ProjectReadResponse_Success();

            // act
            var result = await SystemUnderTest.CreateBulkLabel(UidOne);

            // assert
            AssertViewWithModel<CreateBulkLabelModel>(result);
            MockProjectService.Verify_GetProject();
        }

        [Test]
        public async Task CreateBulkLabel_GET_FailedResponse()
        {
            // arrange
            MockProjectService.Setup_GetProject_Returns_ProjectReadResponse_Failed();

            // act
            var result = await SystemUnderTest.CreateBulkLabel(UidOne);

            // assert
            AssertViewAccessDenied(result);
            MockProjectService.Verify_GetProject();
        }

        [Test]
        public async Task CreateBulkLabel_GET_InvalidResponse()
        {
            // arrange
            MockProjectService.Setup_GetProject_Returns_ProjectReadResponse_Invalid();

            // act
            var result = await SystemUnderTest.CreateBulkLabel(UidOne);

            // assert
            AssertViewAccessDenied(result);
            MockProjectService.Verify_GetProject();
        }

        [Test]
        public async Task CreateBulkLabel_GET_InvalidParameter_RedirectToAccessDenied()
        {
            // arrange

            // act
            var result = await SystemUnderTest.CreateBulkLabel(EmptyUid);

            // assert
            AssertViewAccessDenied(result);
        }

        [Test]
        public async Task CreateBulkLabel_POST()
        {
            // arrange
            MockLabelService.Setup_CreateLabelFromList_Returns_LabelCreateListResponse_Success();
            var model = GetCreateBulkLabelModel(3);

            // act
            var result = await SystemUnderTest.CreateBulkLabel(model);

            // assert

            AssertViewWithModelAndMessage<CreateBulkLabelDoneModel>("CreateBulkLabelDone", result);
            MockLabelService.Verify_CreateLabelFromList();
        }

        [Test]
        public async Task CreateBulkLabel_POST_FailedResponse()
        {
            // arrange 
            MockLabelService.Setup_CreateLabelFromList_Returns_LabelCreateListResponse_Failed();
            var model = GetCreateBulkLabelModel(3);

            // act
            var result = await SystemUnderTest.CreateBulkLabel(model);

            // assert
            AssertErrorMessagesForInvalidOrFailedResponse<CreateBulkLabelModel>(result);
            MockLabelService.Verify_CreateLabelFromList();
        }

        [Test]
        public async Task CreateBulkLabel_POST_InvalidResponse()
        {
            // arrange 
            MockLabelService.Setup_CreateLabelFromList_Returns_LabelCreateListResponse_Invalid();
            var model = GetCreateBulkLabelModel(3);

            // act
            var result = await SystemUnderTest.CreateBulkLabel(model);

            // assert
            AssertErrorMessagesForInvalidOrFailedResponse<CreateBulkLabelModel>(result);
            MockLabelService.Verify_CreateLabelFromList();
        }

        [Test]
        public async Task CreateBulkLabel_POST_Failed_FileHasMoreColumnsThanExpected()
        {
            // arrange 
            var model = GetCreateBulkLabelModel(5);

            // act
            var result = await SystemUnderTest.CreateBulkLabel(model);

            // assert
            AssertErrorMessagesForInvalidOrFailedResponse<CreateBulkLabelModel>("file_has_more_columns_than_expected", result);
        }

        [Test]
        public async Task CreateBulkLabel_POST_InvalidParameter()
        {
            // arrange
            var model = new CreateBulkLabelModel();

            // act
            var result = await SystemUnderTest.CreateBulkLabel(model);

            // assert
            AssertInputErrorMessagesOfView(result, model);
        }

        [Test]
        public async Task Translate_GET()
        {
            // arrange
            MockLanguageService.Setup_GetLanguage_Returns_LanguageReadResponse_Success();
            MockUserRepository.Setup_Select_Returns_OrganizationOneUserOne();
            MockCloudTranslationService.Setup_GetTranslatedText_Returns_LabelGetTranslatedTextResponse_Success();

            // act
            var result = (JsonResult)await SystemUnderTest.Translate(StringOne, UidOne);

            // assert
            result.Value.ShouldNotBeNull();
            var name = (string)result.Value;
            name.ShouldBe(StringTwo);
            MockLanguageService.Verify_GetLanguage();
            MockUserRepository.Verify_Select();
            MockCloudTranslationService.Verify_GetTranslatedText();
        }

        [Test]
        public async Task Translate_GET_LanguageReadResponse_Failed()
        {
            // arrange
            MockLanguageService.Setup_GetLanguage_Returns_LanguageReadResponse_Failed();
           

            // act
            var result = await SystemUnderTest.Translate(StringOne, UidOne);

            // assert
            AssertView<JsonResult>(result);
            MockLanguageService.Verify_GetLanguage();
        }

        [Test]
        public async Task Translate_GET_LanguageReadResponse_Invalid()
        {
            // arrange
            MockLanguageService.Setup_GetLanguage_Returns_LanguageReadResponse_Invalid();

            // act
            var result = await SystemUnderTest.Translate(StringOne, UidOne);

            // assert
            AssertView<JsonResult>(result);
            MockLanguageService.Verify_GetLanguage();
        }

        [Test]
        public async Task Translate_GET_LabelGetTranslatedTextResponse_Failed()
        {
            // arrange
            MockLanguageService.Setup_GetLanguage_Returns_LanguageReadResponse_Success();
            MockUserRepository.Setup_Select_Returns_OrganizationOneUserOne();
            MockCloudTranslationService.Setup_GetTranslatedText_Returns_LabelGetTranslatedTextResponse_Failed();

            // act
            var result =await SystemUnderTest.Translate(StringOne, UidOne);

            // assert
            AssertView<JsonResult>(result);
            MockLanguageService.Verify_GetLanguage();
            MockUserRepository.Verify_Select();
            MockCloudTranslationService.Verify_GetTranslatedText();
        }

        [Test]
        public async Task Translate_GET_LabelGetTranslatedTextResponse_Invalid()
        {
            // arrange
            MockLanguageService.Setup_GetLanguage_Returns_LanguageReadResponse_Success();
            MockUserRepository.Setup_Select_Returns_OrganizationOneUserOne();
            MockCloudTranslationService.Setup_GetTranslatedText_Returns_LabelGetTranslatedTextResponse_Invalid();

            // act
            var result = await SystemUnderTest.Translate(StringOne, UidOne);

            // assert
            AssertView<JsonResult>(result);
            MockLanguageService.Verify_GetLanguage();
            MockUserRepository.Verify_Select();
            MockCloudTranslationService.Verify_GetTranslatedText();
        }

        [Test]
        public async Task Translate_GET_InvalidParameter()
        {
            // arrange

            // act
            var result = await SystemUnderTest.Translate(EmptyString, UidOne);

            // assert
            AssertView<JsonResult>(result);
        }

        [Test]
        public async Task LabelTranslationCreate_GET()
        {
            // arrange
            MockLabelService.Setup_GetLabel_Returns_LabelReadResponse_Success();

            // act
            var result = await SystemUnderTest.LabelTranslationCreate(UidOne);

            // assert
            AssertViewWithModel<LabelTranslationCreateModel>(result);
            MockLabelService.Verify_GetLabel();
        }

        [Test]
        public async Task LabelTranslationCreate_GET_FailedResponse()
        {
            // arrange
            MockLabelService.Setup_GetLabel_Returns_LabelReadResponse_Failed();

            // act
            var result = await SystemUnderTest.LabelTranslationCreate(UidOne);

            // assert
            AssertViewAccessDenied(result);
            MockLabelService.Verify_GetLabel();
        }

        [Test]
        public async Task LabelTranslationCreate_GET_InvalidResponse()
        {
            // arrange
            MockLabelService.Setup_GetLabel_Returns_LabelReadResponse_Invalid();

            // act
            var result = await SystemUnderTest.LabelTranslationCreate(UidOne);

            // assert
            AssertViewAccessDenied(result);
            MockLabelService.Verify_GetLabel();
        }

        [Test]
        public async Task LabelTranslationCreate_GET_InvalidParameter_RedirectToAccessDenied()
        {
            // arrange

            // act
            var result = await SystemUnderTest.LabelTranslationCreate(EmptyUid);

            // assert
            AssertViewAccessDenied(result);
        }

        [Test]
        public async Task LabelTranslationCreate_POST()
        {
            // arrange
            MockLabelService.Setup_CreateTranslation_Returns_LabelTranslationCreateResponse_Success();
            var model = GetLabelTranslationCreateModel();

            // act
            var result = await SystemUnderTest.LabelTranslationCreate(model);

            // assert

            ((RedirectResult)result).Url.ShouldBe($"/Label/Detail/{EmptyUid}");
            MockLabelService.Verify_CreateTranslation();
        }

        [Test]
        public async Task LabelTranslationCreate_POST_FailedResponse()
        {
            // arrange 
            MockLabelService.Setup_CreateTranslation_Returns_LabelTranslationCreateResponse_Failed();
            var model = GetLabelTranslationCreateModel();

            // act
            var result = await SystemUnderTest.LabelTranslationCreate(model);

            // assert
            AssertErrorMessagesForInvalidOrFailedResponse<LabelTranslationCreateModel>(result);
            MockLabelService.Verify_CreateTranslation();
        }

        [Test]
        public async Task LabelTranslationCreate_POST_InvalidResponse()
        {
            // arrange 
            MockLabelService.Setup_CreateTranslation_Returns_LabelTranslationCreateResponse_Invalid();
            var model = GetLabelTranslationCreateModel();

            // act
            var result = await SystemUnderTest.LabelTranslationCreate(model);

            // assert
            AssertErrorMessagesForInvalidOrFailedResponse<LabelTranslationCreateModel>(result);
            MockLabelService.Verify_CreateTranslation();
        }

        [Test]
        public async Task LabelTranslationCreate_POST_InvalidParameter()
        {
            // arrange
            var model = new LabelTranslationCreateModel();

            // act
            var result = await SystemUnderTest.LabelTranslationCreate(model);

            // assert
            AssertInputErrorMessagesOfView(result, model);
        }

        [Test]
        public async Task LabelTranslationDetail_GET()
        {
            // arrange
            MockLabelService.Setup_GetTranslation_Returns_LabelTranslationReadResponse_Success();

            // act
            var result = await SystemUnderTest.LabelTranslationDetail(UidOne);

            // assert
            AssertViewWithModel<LabelTranslationDetailModel>(result);
            MockLabelService.Verify_GetTranslation();
        }

        [Test]
        public async Task LabelTranslationDetail_GET_FailedResponse()
        {
            // arrange
            MockLabelService.Setup_GetTranslation_Returns_LabelTranslationReadResponse_Failed();

            // act
            var result = await SystemUnderTest.LabelTranslationDetail(UidOne);

            // assert
            AssertViewAccessDenied(result);
            MockLabelService.Verify_GetTranslation();
        }

        [Test]
        public async Task LabelTranslationDetail_GET_InvalidResponse()
        {
            // arrange
            MockLabelService.Setup_GetTranslation_Returns_LabelTranslationReadResponse_Invalid();

            // act
            var result = await SystemUnderTest.LabelTranslationDetail(UidOne);

            // assert
            AssertViewAccessDenied(result);
            MockLabelService.Verify_GetTranslation();
        }

        [Test]
        public async Task LabelTranslationDetail_GET_InvalidParameter_RedirectToHome()
        {
            // arrange

            // act
            var result = await SystemUnderTest.LabelTranslationDetail(EmptyUid);

            // assert
            AssertViewRedirectToHome(result);
        }

        [Test]
        public async Task LabelTranslationEdit_GET()
        {
            // arrange
            MockLabelService.Setup_GetTranslation_Returns_LabelTranslationReadResponse_Success();

            // act
            var result = await SystemUnderTest.LabelTranslationEdit(UidOne);

            // assert
            AssertViewWithModel<LabelTranslationEditModel>(result);
            MockLabelService.Verify_GetTranslation();
        }

        [Test]
        public async Task LabelTranslationEdit_GET_FailedResponse()
        {
            // arrange
            MockLabelService.Setup_GetTranslation_Returns_LabelTranslationReadResponse_Failed();

            // act
            var result = await SystemUnderTest.LabelTranslationEdit(UidOne);

            // assert
            AssertViewAccessDenied(result);
            MockLabelService.Verify_GetTranslation();
        }

        [Test]
        public async Task LabelTranslationEdit_GET_InvalidResponse()
        {
            // arrange
            MockLabelService.Setup_GetTranslation_Returns_LabelTranslationReadResponse_Invalid();

            // act
            var result = await SystemUnderTest.LabelTranslationEdit(UidOne);

            // assert
            AssertViewAccessDenied(result);
            MockLabelService.Verify_GetTranslation();
        }

        [Test]
        public async Task LabelTranslationEdit_GET_InvalidParameter_RedirectToAccessDenied()
        {
            // arrange

            // act
            var result = await SystemUnderTest.LabelTranslationEdit(EmptyUid);

            // assert
            AssertViewAccessDenied(result);
        }

        [Test]
        public async Task LabelTranslationEdit_POST()
        {
            // arrange
            MockLabelService.Setup_EditTranslation_Returns_LabelTranslationEditResponse_Success();
            var model = GetLabelTranslationEditModel();

            // act
            var result = await SystemUnderTest.LabelTranslationEdit(model);

            // assert

            ((RedirectResult)result).Url.ShouldBe($"/Label/Detail/{EmptyUid}");
            MockLabelService.Verify_EditTranslation();
        }

        [Test]
        public async Task LabelTranslationEdit_POST_FailedResponse()
        {
            // arrange 
            MockLabelService.Setup_EditTranslation_Returns_LabelTranslationEditResponse_Failed();
            var model = GetLabelTranslationEditModel();

            // act
            var result = await SystemUnderTest.LabelTranslationEdit(model);

            // assert
            AssertErrorMessagesForInvalidOrFailedResponse<LabelTranslationEditModel>(result);
            MockLabelService.Verify_EditTranslation();
        }

        [Test]
        public async Task LabelTranslationEdit_POST_InvalidResponse()
        {
            // arrange 
            MockLabelService.Setup_EditTranslation_Returns_LabelTranslationEditResponse_Invalid();
            var model = GetLabelTranslationEditModel();

            // act
            var result = await SystemUnderTest.LabelTranslationEdit(model);

            // assert
            AssertErrorMessagesForInvalidOrFailedResponse<LabelTranslationEditModel>(result);
            MockLabelService.Verify_EditTranslation();
        }

        [Test]
        public async Task LabelTranslationEdit_POST_InvalidParameter()
        {
            // arrange
            var model = new LabelTranslationEditModel();

            // act
            var result = await SystemUnderTest.LabelTranslationEdit(model);

            // assert
            AssertInputErrorMessagesOfView(result, model);
        }


        [Test]
        public async Task LabelTranslationDelete_POST()
        {
            // arrange
            MockLabelService.Setup_DeleteTranslation_Returns_LabelTranslationDeleteResponse_Success();

            // act
            var result = (JsonResult)await SystemUnderTest.LabelTranslationDelete(UidOne);

            // assert
            ((CommonResult)result.Value).IsOk.ShouldBe(true);
            MockLabelService.Verify_DeleteTranslation();
        }

        [Test]
        public async Task LabelTranslationDelete_POST_FailedResponse()
        {
            // arrange
            MockLabelService.Setup_DeleteTranslation_Returns_LabelTranslationDeleteResponse_Failed();

            // act
            var result = await SystemUnderTest.LabelTranslationDelete(UidOne);

            // assert
            var commonResult = ((CommonResult)((JsonResult)result).Value);
            commonResult.IsOk.ShouldBeFalse();
            commonResult.Messages.Any(x => x == StringOne);
            MockLabelService.Verify_DeleteTranslation();
        }

        [Test]
        public async Task LabelTranslationDelete_POST_InvalidResponse()
        {
            // arrange
            MockLabelService.Setup_DeleteTranslation_Returns_LabelTranslationDeleteResponse_Invalid();

            // act
            var result = await SystemUnderTest.LabelTranslationDelete(UidOne);

            // assert
            var commonResult = ((CommonResult)((JsonResult)result).Value);
            commonResult.IsOk.ShouldBeFalse();
            commonResult.Messages.Any(x => x == StringOne);
            MockLabelService.Verify_DeleteTranslation();
        }

        [Test]
        public async Task LabelTranslationDelete_POST_InvalidParameter()
        {
            // arrange

            // act
            var result = await SystemUnderTest.LabelTranslationDelete(EmptyUid);

            // assert
            AssertView<ForbidResult>(result);
        }


        [Test]
        public async Task LabelTranslationListData_GET()
        {
            // arrange
            MockLabelService.Setup_GetTranslations_Returns_LabelTranslationReadListResponse_Success();

            // act
            var result = await SystemUnderTest.LabelTranslationListData(UidOne, One, Two);

            // assert
            AssertView<JsonResult>(result);
            MockLabelService.Verify_GetTranslations();
        }

        [Test]
        public async Task LabelTranslationListData_GET_FailedResponse()
        {
            // arrange
            MockLabelService.Setup_GetTranslations_Returns_LabelTranslationReadListResponse_Failed();

            // act
            var result = await SystemUnderTest.LabelTranslationListData(UidOne, One, Two);

            // assert
            AssertView<NotFoundResult>(result);
            MockLabelService.Verify_GetTranslations();
        }

        [Test]
        public async Task LabelTranslationListData_GET_InvalidResponse()
        {
            // arrange
            MockLabelService.Setup_GetTranslations_Returns_LabelTranslationReadListResponse_Invalid();

            // act
            var result = await SystemUnderTest.LabelTranslationListData(UidOne, One, Two);

            // assert
            AssertView<NotFoundResult>(result);
            MockLabelService.Verify_GetTranslations();
        }

        [Test]
        public async Task LabelTranslationListData_GET_InvalidParameter_Forbid()
        {
            // arrange

            // act
            var result = await SystemUnderTest.LabelTranslationListData(EmptyUid, One, Two);

            // assert
            AssertView<ForbidResult>(result);
        }

        [Test]
        public async Task UploadLabelTranslationFromCSVFile_GET()
        {
            // arrange
            MockLabelService.Setup_GetLabel_Returns_LabelReadResponse_Success();

            // act
            var result = await SystemUnderTest.UploadLabelTranslationFromCSVFile(UidOne);

            // assert
            AssertViewWithModel<UploadLabelTranslationFromCSVFileModel>(result);
            MockLabelService.Verify_GetLabel();
        }

        [Test]
        public async Task UploadLabelTranslationFromCSVFile_GET_FailedResponse()
        {
            // arrange
            MockLabelService.Setup_GetLabel_Returns_LabelReadResponse_Failed();

            // act
            var result = await SystemUnderTest.UploadLabelTranslationFromCSVFile(UidOne);

            // assert
            AssertViewAccessDenied(result);
            MockLabelService.Verify_GetLabel();
        }

        [Test]
        public async Task UploadLabelTranslationFromCSVFile_GET_InvalidResponse()
        {
            // arrange
            MockLabelService.Setup_GetLabel_Returns_LabelReadResponse_Invalid();

            // act
            var result = await SystemUnderTest.UploadLabelTranslationFromCSVFile(UidOne);

            // assert
            AssertViewAccessDenied(result);
            MockLabelService.Verify_GetLabel();
        }

        [Test]
        public async Task UploadLabelTranslationFromCSVFile_GET_InvalidParameter_RedirectToAccessDenied()
        {
            // arrange

            // act
            var result = await SystemUnderTest.UploadLabelTranslationFromCSVFile(EmptyUid);

            // assert
            AssertViewAccessDenied(result);
        }

        [Test]
        public async Task UploadLabelTranslationFromCSVFile_POST()
        {
            // arrange
            MockLabelService.Setup_CreateTranslationFromList_Returns_LabelTranslationCreateListResponse_Success();
            var model = GetUploadLabelTranslationFromCSVFileModel(2);

            // act
            var result = await SystemUnderTest.UploadLabelTranslationFromCSVFile(model);

            // assert

            AssertViewWithModelAndMessage<TranslationUploadFromCSVDoneModel>("UploadLabelTranslationFromCSVFileDone", result);
            MockLabelService.Verify_CreateTranslationFromList();
        }

        [Test]
        public async Task UploadLabelTranslationFromCSVFile_POST_FailedResponse()
        {
            // arrange 
            MockLabelService.Setup_CreateTranslationFromList_Returns_LabelTranslationCreateListResponse_Failed();
            var model = GetUploadLabelTranslationFromCSVFileModel(2);

            // act
            var result = await SystemUnderTest.UploadLabelTranslationFromCSVFile(model);

            // assert
            AssertErrorMessagesForInvalidOrFailedResponse<UploadLabelTranslationFromCSVFileModel>(result);
            MockLabelService.Verify_CreateTranslationFromList();
        }

        [Test]
        public async Task UploadLabelTranslationFromCSVFile_POST_InvalidResponse()
        {
            // arrange 
            MockLabelService.Setup_CreateTranslationFromList_Returns_LabelTranslationCreateListResponse_Invalid();
            var model = GetUploadLabelTranslationFromCSVFileModel(2);

            // act
            var result = await SystemUnderTest.UploadLabelTranslationFromCSVFile(model);

            // assert
            AssertErrorMessagesForInvalidOrFailedResponse<UploadLabelTranslationFromCSVFileModel>(result);
            MockLabelService.Verify_CreateTranslationFromList();
        }

        [Test]
        public async Task UploadLabelTranslationFromCSVFile_POST_Failed_FileHasMoreColumnsThanExpected()
        {
            // arrange 
            var model = GetUploadLabelTranslationFromCSVFileModel(3);

            // act
            var result = await SystemUnderTest.UploadLabelTranslationFromCSVFile(model);

            // assert
            AssertErrorMessagesForInvalidOrFailedResponse<UploadLabelTranslationFromCSVFileModel>("file_has_more_columns_than_expected", result);
        }

        [Test]
        public async Task UploadLabelTranslationFromCSVFile_POST_InvalidModel()
        {
            // arrange
            var model = new UploadLabelTranslationFromCSVFileModel();

            // act
            var result = await SystemUnderTest.UploadLabelTranslationFromCSVFile(model);

            // assert
            AssertInputErrorMessagesOfView(result, model);
        }

        [Test]
        public void DownloadSampleCSVFileForBulkLabelTranslationUpload_GET()
        {
            // arrange
            MockHostingEnvironment.Setup_WebRootPath_Returns_TestWebRootPath();

            // act
            var result = SystemUnderTest.DownloadSampleCSVFileForBulkLabelTranslationUpload();

            // assert
            AssertView<FileResult>(result);
        }

        [Test]
        public async Task DownloadTranslations_POST()
        {
            // arrange
            MockLabelService.Setup_GetTranslations_Returns_LabelTranslationReadListResponse_Success();

            // act
            var result = await SystemUnderTest.DownloadTranslations(UidOne);

            // assert
            AssertView<FileResult>(result);
            MockLabelService.Verify_GetTranslations();
        }

        [Test]
        public async Task DownloadTranslations_POST_FailedResponse()
        {
            // arrange 
            MockLabelService.Setup_GetTranslations_Returns_LabelTranslationReadListResponse_Failed();

            // act
            var result = await SystemUnderTest.DownloadTranslations(UidOne);

            // assert
            AssertView<NoContentResult>(result);
            MockLabelService.Verify_GetTranslations();
        }

        [Test]
        public async Task DownloadTranslations_POST_InvalidResponse()
        {
            // arrange 
            MockLabelService.Setup_GetTranslations_Returns_LabelTranslationReadListResponse_Invalid();

            // act
            var result = await SystemUnderTest.DownloadTranslations(UidOne);

            // assert
            AssertView<NoContentResult>(result);
            MockLabelService.Verify_GetTranslations();
        }

        [Test]
        public async Task DownloadTranslations_POST_InvalidParameter_NoContent()
        {
            // arrange

            // act
            var result = await SystemUnderTest.DownloadTranslations(EmptyUid);

            // assert
            AssertView<NoContentResult>(result);

        }

        [Test]
        public async Task RestoreLabelTranslation_POST()
        {
            // arrange
            MockLabelService.Setup_RestoreLabelTranslation_Returns_LabelTranslationRestoreResponse_Success();

            // act
            var result = (JsonResult)await SystemUnderTest.RestoreLabelTranslation(UidOne, One);

            // assert
            ((CommonResult)result.Value).IsOk.ShouldBe(true);
            MockLabelService.Verify_RestoreLabelTranslation();
        }

        [Test]
        public async Task RestoreLabelTranslation_POST_FailedResponse()
        {
            // arrange 
            MockLabelService.Setup_RestoreLabelTranslation_Returns_LabelTranslationRestoreResponse_Failed();

            // act
            var result = (JsonResult)await SystemUnderTest.RestoreLabelTranslation(UidOne, One);

            // assert
            AssertView<CommonResult>(result);
            MockLabelService.Verify_RestoreLabelTranslation();
        }

        [Test]
        public async Task RestoreLabelTranslation_POST_InvalidResponse()
        {
            // arrange 
            MockLabelService.Setup_RestoreLabelTranslation_Returns_LabelTranslationRestoreResponse_Invalid();

            // act
            var result = (JsonResult)await SystemUnderTest.RestoreLabelTranslation(UidOne, One);

            // assert
            AssertView<CommonResult>(result);
            MockLabelService.Verify_RestoreLabelTranslation();
        }

        [Test]
        public async Task RestoreLabelTranslation_POST_InvalidParameter()
        {
            // arrange

            // act
            var result = await SystemUnderTest.RestoreLabelTranslation(EmptyUid, One);

            // assert
            AssertView<JsonResult>(result);

        }

        [Test]
        public async Task RestoreLabelTranslation_POST_InvalidParameter_RevisionZero()
        {
            // arrange

            // act
            var result = await SystemUnderTest.RestoreLabelTranslation(UidOne, 0);

            // assert
            AssertView<JsonResult>(result);

        }

        [Test]
        public async Task LabelTranslationRevisions_GET()
        {
            // arrange
            MockLabelService.Setup_GetTranslation_Returns_LabelTranslationReadResponse_Success();

            // act
            var result = await SystemUnderTest.LabelTranslationRevisions(UidOne);

            // assert
            AssertViewWithModel<LabelTranslationRevisionReadListModel>(result);
            MockLabelService.Verify_GetTranslation();
        }

        [Test]
        public async Task LabelTranslationRevisions_GET_FailedResponse()
        {
            // arrange
            MockLabelService.Setup_GetTranslation_Returns_LabelTranslationReadResponse_Failed();

            // act
            var result = await SystemUnderTest.LabelTranslationRevisions(UidOne);

            // assert
            AssertView<NotFoundResult>(result);
            MockLabelService.Verify_GetTranslation();

        }

        [Test]
        public async Task LabelTranslationRevisions_GET_InvalidResponse()
        {
            // arrange
            MockLabelService.Setup_GetTranslation_Returns_LabelTranslationReadResponse_Invalid();

            // act
            var result = await SystemUnderTest.LabelTranslationRevisions(UidOne);

            // assert
            AssertView<NotFoundResult>(result);
            MockLabelService.Verify_GetTranslation();

        }

        [Test]
        public async Task LabelTranslationRevisions_GET_InvalidParameter_AssertViewRedirectToHome()
        {
            // arrange

            // act
            var result = await SystemUnderTest.LabelTranslationRevisions(EmptyUid);

            // assert
            AssertViewRedirectToHome(result);
        }

        [Test]
        public void LabelTranslationRevisionsData_GET()
        {
            // arrange
            MockLabelService.Setup_GetLabelTranslationRevisions_Returns_LabelTranslationRevisionReadListResponse_Success();

            // act
            var result = SystemUnderTest.LabelTranslationRevisionsData(UidOne);

            // assert
            AssertViewAndHeaders(result, new[] { "revision", "revisioned_by", "revisioned_at", "label_translation_name", "created_at", "" });
            MockLabelService.Verify_GetLabelTranslationRevisions();
        }

        [Test]
        public async Task LabelTranslationRevisionsData_GET_FailedResponse()
        {
            // arrange
            MockLabelService.Setup_GetLabelTranslationRevisions_Returns_LabelTranslationRevisionReadListResponse_Failed();

            // act
            var result = await SystemUnderTest.LabelTranslationRevisionsData(UidOne);

            // assert
            AssertView<NotFoundResult>(result);
            MockLabelService.Verify_GetLabelTranslationRevisions();
        }

        [Test]
        public async Task LabelTranslationRevisionsData_GET_InvalidResponse()
        {
            // arrange
            MockLabelService.Setup_GetLabelTranslationRevisions_Returns_LabelTranslationRevisionReadListResponse_Invalid();

            // act
            var result = await SystemUnderTest.LabelTranslationRevisionsData(UidOne);

            // assert
            AssertView<NotFoundResult>(result);
            MockLabelService.Verify_GetLabelTranslationRevisions();
        }

        [Test]
        public async Task LabelTranslationRevisionsData_GET_InvalidParameter()
        {
            // arrange

            // act
            var result = await SystemUnderTest.LabelTranslationRevisionsData(EmptyUid);

            // assert
            AssertView<NotFoundResult>(result);
        }
    }
}