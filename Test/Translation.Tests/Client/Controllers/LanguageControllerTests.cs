using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using NUnit.Framework;
using Shouldly;

using Translation.Client.Web.Controllers;
using Translation.Client.Web.Models.Base;
using Translation.Client.Web.Models.Language;
using Translation.Tests.SetupHelpers;
using static Translation.Tests.TestHelpers.ActionMethodNameConstantTestHelper;
using static Translation.Tests.TestHelpers.FakeConstantTestHelper;
using static Translation.Tests.TestHelpers.AssertViewModelTestHelper;
using static Translation.Tests.TestHelpers.AssertModelTestHelper;
using static Translation.Tests.TestHelpers.FakeModelTestHelper;

namespace Translation.Tests.Client.Controllers
{
    [TestFixture]
    public class LanguageControllerTests : ControllerBaseTests
    {
        public LanguageController SystemUnderTest { get; set; }

        [SetUp]
        public void run_before_every_test()
        {
            SystemUnderTest = Container.Resolve<LanguageController>();
            SetControllerContext(SystemUnderTest);
        }

        [TestCase(CreateAction, new Type[] { }, typeof(HttpGetAttribute))]
        [TestCase(CreateAction, new Type[] { typeof(LanguageCreateModel) }, typeof(HttpPostAttribute))]
        [TestCase(DetailAction, new Type[] { typeof(Guid) }, typeof(HttpGetAttribute))]
        [TestCase(EditAction, new Type[] { typeof(Guid) }, typeof(HttpGetAttribute))]
        [TestCase(EditAction, new Type[] { typeof(LanguageEditModel) }, typeof(HttpPostAttribute))]
        [TestCase(ListAction, new Type[] { }, typeof(HttpGetAttribute))]
        [TestCase(ListDataAction, new Type[] { typeof(int), typeof(int) }, typeof(HttpGetAttribute))]
        [TestCase(SelectDataAction, new Type[] { typeof(Guid), typeof(int), typeof(string) }, typeof(HttpGetAttribute))]
        [TestCase(RevisionsAction, new[] { typeof(Guid) }, typeof(HttpGetAttribute))]
        [TestCase(RevisionsDataAction, new[] { typeof(Guid) }, typeof(HttpGetAttribute))]
        [TestCase(RestoreAction, new[] { typeof(Guid), typeof(int) }, typeof(HttpPostAttribute))]
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
        public void Create_GET()
        {
            var result = SystemUnderTest.Create();
            AssertViewWithModel<LanguageCreateModel>(result);
        }

        [Ignore("it needs refactoring of GetLanguageOneCreateModel")]
        [Test]
        public async Task Create_POST()
        {
            // arrange
            MockHostingEnvironment.Setup(x => x.WebRootPath).Returns("");
            MockLanguageService.Setup_CreateLanguage_Returns_LanguageCreateResponse_Success();
            var model = GetLanguageOneCreateModel();
            // todo: model Icon 

            // act
            var result = await SystemUnderTest.Create(model);

            // assert
            ((RedirectResult)result).Url.ShouldBe("/Language/List/");
            MockLanguageService.Verify_CreateLanguage();
        }

        [Ignore("it needs refactoring of GetLanguageOneCreateModel")]
        [Test]
        public async Task Create_POST_FailedResponse()
        {
            // arrange
            MockHostingEnvironment.Setup(x => x.WebRootPath).Returns("");
            MockLanguageService.Setup_CreateLanguage_Returns_LanguageCreateResponse_Failed();
            var model = GetLanguageOneCreateModel();
            // todo: model Icon 

            // act
            var result = await SystemUnderTest.Create(model);

            // assert
            AssertErrorMessagesForInvalidOrFailedResponse<LanguageCreateModel>(result);
            MockLanguageService.Verify_CreateLanguage();
        }

        [Ignore("it needs refactoring of GetLanguageOneCreateModel")]
        [Test]
        public async Task Create_POST_InvalidResponse()
        {
            // arrange
            MockHostingEnvironment.Setup(x => x.WebRootPath).Returns("");
            MockLanguageService.Setup_CreateLanguage_Returns_LanguageCreateResponse_Invalid();
            var model = GetLanguageOneCreateModel();
            // todo: model Icon 

            // act
            var result = await SystemUnderTest.Create(model);

            // assert
            AssertErrorMessagesForInvalidOrFailedResponse<LanguageCreateModel>(result);
            MockLanguageService.Verify_CreateLanguage();
        }

        [Test]
        public async Task Create_POST_InvalidModel()
        {
            // arrange
            var model = new LanguageCreateModel();

            // act
            var result = await SystemUnderTest.Create(model);

            // assert
            AssertInputErrorMessagesOfView(result, model);
        }

        [Test]
        public async Task Edit_GET()
        {
            // arrange
            MockLanguageService.Setup_GetLanguage_Returns_LanguageReadResponse_Success();

            // act
            var result = await SystemUnderTest.Edit(UidOne);

            // assert
            AssertViewWithModel<LanguageEditModel>(result);
            MockLanguageService.Verify_GetLanguage();
        }

        [Test]
        public async Task Edit_GET_FailedResponse()
        {
            // arrange
            MockLanguageService.Setup_GetLanguage_Returns_LanguageReadResponse_Failed();

            // act
            var result = await SystemUnderTest.Edit(UidOne);

            // assert
            AssertViewAccessDenied(result);
            MockLanguageService.Verify_GetLanguage();
        }

        [Test]
        public async Task Edit_GET_InvalidResponse()
        {
            // arrange
            MockLanguageService.Setup_GetLanguage_Returns_LanguageReadResponse_Invalid();

            // act
            var result = await SystemUnderTest.Edit(UidOne);

            // assert
            AssertViewAccessDenied(result);
            MockLanguageService.Verify_GetLanguage();
        }

        [Test]
        public async Task Edit_GET_InvalidParameter()
        {
            // arrange

            // act
            var result = await SystemUnderTest.Edit(UidOne);

            // assert
            AssertViewAccessDenied(result);
        }

        [Ignore("it needs refactoring of GetLanguageOneEditModel")]
        [Test]
        public async Task Edit_POST()
        {
            // arrange
            MockHostingEnvironment.Setup(x => x.WebRootPath).Returns("");
            MockLanguageService.Setup_EditLanguage_Returns_LanguageEditResponse_Success();
            var model = GetLanguageOneEditModel();
            // todo: model Icon 

            // act
            var result = await SystemUnderTest.Edit(model);

            // assert
            ((RedirectResult)result).Url.ShouldBe("/Language/List/");
            MockLanguageService.Verify_EditLanguage();
        }

        [Ignore("it needs refactoring of GetLanguageOneEditModel")]
        [Test]
        public async Task Edit_POST_FailedResponse()
        {
            // arrange
            MockLanguageService.Setup_EditLanguage_Returns_LanguageEditResponse_Failed();
            var model = GetLanguageOneEditModel();
            // todo: model Icon 

            // act
            var result = await SystemUnderTest.Edit(model);

            // assert
            AssertErrorMessagesForInvalidOrFailedResponse<LanguageEditModel>(result);
            MockLanguageService.Verify_EditLanguage();
        }

        [Ignore("it needs refactoring of GetLanguageOneEditModel")]
        [Test]
        public async Task Edit_POST_InvalidResponse()
        {
            // arrange
            MockLanguageService.Setup_EditLanguage_Returns_LanguageEditResponse_Invalid();
            var model = GetLanguageOneEditModel();
            // todo: model Icon 

            // act
            var result = await SystemUnderTest.Edit(model);

            // assert
            AssertErrorMessagesForInvalidOrFailedResponse<LanguageEditModel>(result);
            MockLanguageService.Verify_EditLanguage();
        }

        [Test]
        public async Task Edit_POST_InvalidModel()
        {
            // arrange
            var model = new LanguageEditModel();

            // act
            var result = await SystemUnderTest.Edit(model);

            // assert
            AssertInputErrorMessagesOfView(result, model);
        }

        [Test]
        public void List_GET()
        {
            // arrange

            // act
            var result = SystemUnderTest.List();

            // assert
            AssertViewWithModel<LanguageListModel>(result);
        }

        [Test]
        public void ListData_GET()
        {
            // arrange
            MockLanguageService.Setup_GetLanguages_Returns_LanguageReadListResponse_Success();

            // act
            var result = SystemUnderTest.ListData(One, Two);

            // assert
            AssertViewAndHeaders(result, new[] { "language_name", "2_char_code", "3_char_code", "icon", "" });
            MockLanguageService.Verify_GetLanguages();
        }

        [Test]
        public void ListData_GET_FailedResponse()
        {
            // arrange
            MockLanguageService.Setup_GetLanguages_Returns_LanguageReadListResponse_Failed();

            // act
            var result = SystemUnderTest.ListData(One, Two);

            // assert
            AssertView<NotFoundResult>(result);
            MockLanguageService.Verify_GetLanguages();
        }

        [Test]
        public void ListData_GET_InvalidResponse()
        {
            // arrange
            MockLanguageService.Setup_GetLanguages_Returns_LanguageReadListResponse_Invalid();

            // act
            var result = SystemUnderTest.ListData(One, Two);

            // assert
            AssertView<NotFoundResult>(result);
            MockLanguageService.Verify_GetLanguages();
        }

        [TestCase(10, 10)]
        [TestCase(10, 1000)]
        [TestCase(-10, 10)]
        [TestCase(10, -10)]
        [TestCase(1000, 10)]
        public async Task ListData_GET_SetPaging(int skip, int take)
        {
            // arrange
            MockLanguageService.Setup_GetLanguages_Returns_LanguageReadListResponse_Success();

            // act
            var result = (JsonResult)await SystemUnderTest.ListData(skip, take);

            // assert
            AssertView<DataResult>(result);
            AssertPagingInfo(result);
        }

        [Test]
        public void SelectData_GET()
        {
            // arrange
            MockLanguageService.Setup_GetLanguages_Returns_LanguageReadListResponse_Success();

            // act
            var result = SystemUnderTest.SelectData(UidOne);

            // assert
            AssertView<JsonResult>(result);
            MockLanguageService.Verify_GetLanguages();
        }

        [Test]
        public async Task SelectData_GET_FailedResponse()
        {
            // arrange
            MockLanguageService.Setup_GetLanguages_Returns_LanguageReadListResponse_Failed();

            // act
            var result = await SystemUnderTest.SelectData(UidOne);

            // assert
            MockLanguageService.Verify_GetLanguages();
            AssertView<JsonResult>(result);
        }

        [Test]
        public async Task SelectData_GET_InvalidResponse()
        {
            // arrange
            MockLanguageService.Setup_GetLanguages_Returns_LanguageReadListResponse_Invalid();

            // act
            var result = await SystemUnderTest.SelectData(UidOne);

            // assert
            MockLanguageService.Verify_GetLanguages();
            AssertView<JsonResult>(result);
        }

        [Test]
        public async Task Revisions_GET()
        {
            // arrange
            MockLanguageService.Setup_GetLanguage_Returns_LanguageReadResponse_Success();

            // act
            var result = await SystemUnderTest.Revisions(OrganizationOneProjectOneUid);

            // assert
            AssertViewWithModel<LanguageRevisionReadListModel>(result);
            MockLanguageService.Verify_GetLanguage();
        }

        [Test]
        public async Task Revisions_GET_InvalidParameter()
        {
            // arrange
            MockLanguageService.Setup_GetLanguage_Returns_LanguageReadResponse_Success();

            // act
            var result = await SystemUnderTest.Revisions(EmptyUid);

            // assert
            AssertViewRedirectToHome(result);
            MockLanguageService.Verify_GetLanguage();
        }

        [Test]
        public void RevisionsData_GET()
        {
            // arrange
            MockLanguageService.Setup_GetLanguageRevisions_Returns_LanguageRevisionReadListResponse_Success();

            // act
            var result = SystemUnderTest.RevisionsData(OrganizationOneProjectOneUid);

            // assert
            AssertViewAndHeaders(result, new[] { "revision", "revisioned_by", "revisioned_at", "language_name", "2_char_code", "3_char_code", "icon", "created_at", "" });
            MockLanguageService.Verify_GetLanguageRevisions();
        }

        [Test]
        public void RevisionsData_GET_FailedResponse()
        {
            // arrange
            MockLanguageService.Setup_GetLanguageRevisions_Returns_LanguageRevisionReadListResponse_Failed();

            // act
            var result = SystemUnderTest.RevisionsData(OrganizationOneProjectOneUid);

            // assert
            AssertView<NotFoundResult>(result);
            MockLanguageService.Verify_GetLanguageRevisions();
        }

        [Test]
        public void RevisionsData_GET_InvalidResponse()
        {
            // arrange
            MockLanguageService.Setup_GetLanguageRevisions_Returns_LanguageRevisionReadListResponse_Invalid();

            // act
            var result = SystemUnderTest.RevisionsData(OrganizationOneProjectOneUid);

            // assert
            AssertView<NotFoundResult>(result);
            MockLanguageService.Verify_GetLanguageRevisions();
        }

        [Test]
        public void RevisionsData_GET_InvalidParameter()
        {
            // arrange

            // act
            var result = SystemUnderTest.RevisionsData(EmptyUid);

            // assert
            AssertView<ForbidResult>(result);
        }

        [Test]
        public async Task Restore_Post()
        {
            // arrange
            MockLanguageService.Setup_RestoreLanguage_Returns_LanguageRestoreResponse_Success();

            // act
            var result = await SystemUnderTest.Restore(OrganizationOneProjectOneUid, One);

            // assert
            AssertView<JsonResult>(result);
            MockLanguageService.Verify_RestoreLanguage();
        }

        [Test]
        public async Task Restore_Post_FailedResponse()
        {
            // arrange
            MockLanguageService.Setup_RestoreLanguage_Returns_LanguageRestoreResponse_Failed();

            // act
            var result = (JsonResult)await SystemUnderTest.Restore(OrganizationOneProjectOneUid, One);

            // assert
            ((CommonResult)result.Value).IsOk.ShouldBe(false);
            MockLanguageService.Verify_RestoreLanguage();
        }

        [Test]
        public async Task Restore_Post_InvalidResponse()
        {
            // arrange
            MockLanguageService.Setup_RestoreLanguage_Returns_LanguageRestoreResponse_Invalid();

            // act
            var result = (JsonResult)await SystemUnderTest.Restore(OrganizationOneProjectOneUid, One);

            // assert
            ((CommonResult)result.Value).IsOk.ShouldBe(false);
            MockLanguageService.Verify_RestoreLanguage();
        }

        [Test]
        public async Task Restore_Post_InvalidParameter()
        {
            // arrange

            // act
            var result = (JsonResult)await SystemUnderTest.Restore(EmptyUid, One);

            // assert
            ((CommonResult)result.Value).IsOk.ShouldBe(false);
        }
    }
}