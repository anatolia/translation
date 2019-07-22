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
using static Translation.Tests.TestHelpers.AssertModelTestHelper;
using static Translation.Tests.TestHelpers.FakeModelTestHelper;
using Translation.Client.Web.Models.Base;
using Translation.Common.Helpers;

namespace Translation.Tests.Client.Controllers
{
    [TestFixture]
    public class LabelControllerTests : ControllerBaseTests
    {
        public LabelController SystemUnderTest { get; set; }

        [SetUp]
        public void run_before_every_test()
        {
            SystemUnderTest = Container.Resolve<LabelController>();
            SetControllerContext(SystemUnderTest);
        }

        [TestCase(CreateAction, new[] { typeof(Guid) }, typeof(HttpGetAttribute))]
        [TestCase(CreateAction, new[] { typeof(LabelCreateModel) }, typeof(HttpPostAttribute))]
        [TestCase(DetailAction, new[] { typeof(string), typeof(string) }, typeof(HttpGetAttribute))]
        [TestCase(EditAction, new[] { typeof(Guid) }, typeof(HttpGetAttribute))]
        [TestCase(EditAction, new[] { typeof(LabelEditModel) }, typeof(HttpPostAttribute))]
        [TestCase(CloneAction, new[] { typeof(Guid) }, typeof(HttpGetAttribute))]
        [TestCase(CloneAction, new[] { typeof(LabelCloneModel) }, typeof(HttpPostAttribute))]
        [TestCase(SearchListAction, new[] { typeof(string) }, typeof(HttpGetAttribute))]
        [TestCase(SearchDataAction, new[] { typeof(string) }, typeof(HttpGetAttribute))]
        [TestCase(SearchListDataAction, new[] { typeof(string), typeof(int), typeof(int) }, typeof(HttpGetAttribute))]
        [TestCase(RevisionsAction, new[] { typeof(Guid) }, typeof(HttpGetAttribute))]
        [TestCase(RevisionsDataAction, new[] { typeof(Guid) }, typeof(HttpGetAttribute))]
        [TestCase(RestoreAction, new[] { typeof(Guid), typeof(int) }, typeof(HttpPostAttribute))]
        [TestCase(ChangeActivationAction, new[] { typeof(Guid), typeof(Guid) }, typeof(HttpPostAttribute))]
        [TestCase(UploadLabelFromCSVFileAction, new[] { typeof(Guid) }, typeof(HttpGetAttribute))]
        [TestCase(UploadLabelFromCSVFileAction, new[] { typeof(LabelUploadFromCSVModel) }, typeof(HttpPostAttribute))]
        [TestCase(CreateBulkLabelAction, new[] { typeof(Guid) }, typeof(HttpGetAttribute))]
        [TestCase(CreateBulkLabelAction, new[] { typeof(CreateBulkLabelModel) }, typeof(HttpPostAttribute))]
        [TestCase(LabelTranslationCreateAction, new[] { typeof(Guid) }, typeof(HttpGetAttribute))]
        [TestCase(LabelTranslationCreateAction, new[] { typeof(LabelTranslationCreateModel) }, typeof(HttpPostAttribute))]
        [TestCase(LabelTranslationDetailAction, new[] { typeof(Guid) }, typeof(HttpGetAttribute))]
        [TestCase(LabelTranslationEditAction, new[] { typeof(Guid) }, typeof(HttpGetAttribute))]
        [TestCase(LabelTranslationEditAction, new[] { typeof(LabelTranslationEditModel) }, typeof(HttpPostAttribute))]
        [TestCase(LabelTranslationListDataAction, new[] { typeof(Guid), typeof(int), typeof(int) }, typeof(HttpGetAttribute))]
        [TestCase(UploadLabelTranslationFromCSVFileAction, new[] { typeof(Guid) }, typeof(HttpGetAttribute))]
        [TestCase(UploadLabelTranslationFromCSVFileAction, new[] { typeof(UploadLabelTranslationFromCSVFileModel) }, typeof(HttpPostAttribute))]
        [TestCase(DownloadTranslationsAction, new[] { typeof(Guid) }, typeof(HttpPostAttribute))]
        [TestCase(RestoreLabelTranslationAction, new[] { typeof(Guid), typeof(int) }, typeof(HttpPostAttribute))]
        [TestCase(LabelTranslationRevisionsAction, new[] { typeof(Guid) }, typeof(HttpGetAttribute))]
        [TestCase(LabelTranslationRevisionsDataAction, new[] { typeof(Guid) }, typeof(HttpGetAttribute))]
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
        public async Task Detail_GET_LabelKey()
        {
            // arrange 
            MockProjectService.Setup_GetProjectBySlug_Returns_ProjectReadBySlugResponse_Success();
            MockLabelService.Setup_GetLabelByKey_Returns_LabelReadByKeyResponse_Success();

            // act
            var result = await SystemUnderTest.Detail(StringOne, StringTwo);

            // assert
            AssertViewWithModel<LabelDetailModel>(result);
            MockProjectService.Verify_GetProjectBySlug();
            MockLabelService.Verify_GetLabelByKey();
        }

        [Test]
        public async Task Detail_GET_LabelUid()
        {
            // arrange
            MockProjectService.Setup_GetProjectBySlug_Returns_ProjectReadBySlugResponse_Success();
            MockLabelService.Setup_GetLabel_Returns_LabelReadResponse_Success();

            // act
            var result = await SystemUnderTest.Detail(StringOne, UidStringOne);

            // assert
            AssertViewWithModel<LabelDetailModel>(result);
            MockProjectService.Verify_GetProjectBySlug();
            MockLabelService.Verify_GetLabel();
        }

        [Test]
        public async Task Detail_GET_FailedProjectReadBySlugResponse()
        {
            // arrange 
            MockProjectService.Setup_GetProjectBySlug_Returns_ProjectReadBySlugResponse_Failed();


            // act
            var result = await SystemUnderTest.Detail(StringOne, StringTwo);

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
            var result = await SystemUnderTest.Detail(StringOne, StringTwo);

            // assert
            AssertViewAccessDenied(result);
            MockProjectService.Verify_GetProjectBySlug();
            MockLabelService.Verify_GetLabelByKey();
        }

        [Test]
        public async Task Detail_GET_FailedLabelReadResponse()
        {
            // arrange 
            MockProjectService.Setup_GetProjectBySlug_Returns_ProjectReadBySlugResponse_Success();
            MockLabelService.Setup_GetLabel_Returns_LabelReadResponse_Failed();

            // act
            var result = await SystemUnderTest.Detail(StringOne, UidStringOne);

            // assert
            AssertViewAccessDenied(result);
            MockProjectService.Verify_GetProjectBySlug();
            MockLabelService.Verify_GetLabel();
        }

        [Test]
        public async Task Detail_GET_InvalidProjectReadBySlugResponse()
        {
            // arrange 
            MockProjectService.Setup_GetProjectBySlug_Returns_ProjectReadBySlugResponse_Invalid();


            // act
            var result = await SystemUnderTest.Detail(StringOne, StringTwo);

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
            var result = await SystemUnderTest.Detail(StringOne, StringTwo);

            // assert
            AssertViewAccessDenied(result);
            MockProjectService.Verify_GetProjectBySlug();
            MockLabelService.Verify_GetLabelByKey();
        }

        [Test]
        public async Task Detail_GET_InvalidLabelReadResponse()
        {
            // arrange 
            MockProjectService.Setup_GetProjectBySlug_Returns_ProjectReadBySlugResponse_Success();
            MockLabelService.Setup_GetLabel_Returns_LabelReadResponse_Invalid();

            // act
            var result = await SystemUnderTest.Detail(StringOne, UidStringOne);

            // assert
            AssertViewAccessDenied(result);
            MockProjectService.Verify_GetProjectBySlug();
            MockLabelService.Verify_GetLabel();
        }

        [Test]
        public async Task Detail_GET_InvalidParameter_RedirectToHome()
        {
            // arrange

            // act
            var result = await SystemUnderTest.Detail(StringEmpty, StringTwo);

            // assert
            AssertViewRedirectToHome(result);
        }

        [Test]
        public async Task Detail_GET_LabelKeyInvalidParameter_RedirectToHome()
        {
            // arrange 
            MockProjectService.Setup_GetProjectBySlug_Returns_ProjectReadBySlugResponse_Success();

            // act
            var result = await SystemUnderTest.Detail(StringOne, StringEmpty);

            // assert
            AssertViewRedirectToHome(result);
            MockProjectService.Verify_GetProjectBySlug();
        }

        [Test]
        public async Task Detail_GET_LabelUidInvalidParameter_RedirectToHome()
        {
            // arrange
            MockProjectService.Setup_GetProjectBySlug_Returns_ProjectReadBySlugResponse_Success();

            // act
            var result = await SystemUnderTest.Detail(StringOne, EmptyUidString);

            // assert
            AssertViewRedirectToHome(result);
            MockProjectService.Verify_GetProjectBySlug();
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
            var result = await SystemUnderTest.SearchData(StringEmpty);

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
            var result = await SystemUnderTest.SearchListData(StringEmpty, One, Two);

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
        public async Task Revisions_GET_InvalidParameter()
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

        [Ignore("it needs refactoring of GetLabelUploadFromCSVModel")]
        [Test]
        public async Task UploadLabelFromCSVFile_POST()
        {
            // arrange
            MockLabelService.Setup_CreateLabelFromList_Returns_LabelCreateListResponse_Success();
            var model = GetLabelUploadFromCSVModel();

            // act
            var result = await SystemUnderTest.UploadLabelFromCSVFile(model);

            // assert
            AssertViewWithModel<LabelUploadFromCSVModel>(result);
            MockLabelService.Verify_CreateLabelFromList();
        }

        [Ignore("it needs refactoring of GetLabelUploadFromCSVModel")]
        [Test]
        public async Task UploadLabelFromCSVFile_POST_FailedResponse()
        {
            // arrange 
            MockLabelService.Setup_CreateLabelFromList_Returns_LabelCreateListResponse_Failed();
            var model = GetLabelUploadFromCSVModel();

            // act
            var result = await SystemUnderTest.UploadLabelFromCSVFile(model);

            // assert
            AssertErrorMessagesForInvalidOrFailedResponse<LabelUploadFromCSVModel>(result);
            MockLabelService.Verify_CreateLabelFromList();
        }

        [Ignore("it needs refactoring of GetLabelUploadFromCSVModel")]
        [Test]
        public async Task UploadLabelFromCSVFile_POST_InvalidResponse()
        {
            // arrange 
            MockLabelService.Setup_CreateLabelFromList_Returns_LabelCreateListResponse_Invalid();
            var model = GetLabelUploadFromCSVModel();

            // act
            var result = await SystemUnderTest.UploadLabelFromCSVFile(model);

            // assert
            AssertErrorMessagesForInvalidOrFailedResponse<LabelUploadFromCSVModel>(result);
            MockLabelService.Verify_CreateLabelFromList();
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

    }
}