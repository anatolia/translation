using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;

using Shouldly;

using Translation.Client.Web.Controllers;
using Translation.Client.Web.Models.Integration;
using Translation.Tests.SetupHelpers;
using static Translation.Tests.TestHelpers.ActionMethodNameConstantTestHelper;
using static Translation.Tests.TestHelpers.FakeConstantTestHelper;
using static Translation.Tests.TestHelpers.AssertViewModelTestHelper;
using static Translation.Tests.TestHelpers.FakeModelTestHelper;

using System.Threading.Tasks;
using Translation.Client.Web.Models.Base;

namespace Translation.Tests.Client.Controllers
{
    [TestFixture]
    public class IntegrationControllerTests : ControllerBaseTests
    {
        public IntegrationController SystemUnderTest { get; set; }

        [SetUp]
        public void run_before_every_test()
        {
            SystemUnderTest = Container.Resolve<IntegrationController>();
            SetControllerContext(SystemUnderTest);
        }

        [TestCase(CreateAction, new[] { typeof(Guid) }, typeof(HttpGetAttribute))]
        [TestCase(CreateAction, new[] { typeof(IntegrationCreateModel) }, typeof(HttpPostAttribute))]
        [TestCase(DetailAction, new[] { typeof(Guid) }, typeof(HttpGetAttribute))]
        [TestCase(EditAction, new[] { typeof(Guid) }, typeof(HttpGetAttribute))]
        [TestCase(EditAction, new[] { typeof(IntegrationEditModel) }, typeof(HttpPostAttribute))]
        [TestCase(DeleteAction, new[] { typeof(Guid) }, typeof(HttpPostAttribute))]
        [TestCase(RevisionsAction, new[] { typeof(Guid) }, typeof(HttpGetAttribute))]
        [TestCase(RevisionsDataAction, new[] { typeof(Guid) }, typeof(HttpGetAttribute))]
        [TestCase(RestoreAction, new[] { typeof(Guid), typeof(int) }, typeof(HttpPostAttribute))]
        [TestCase(ClientListDataAction, new[] { typeof(Guid), typeof(int), typeof(int) }, typeof(HttpGetAttribute))]
        [TestCase(ClientCreateAction, new[] { typeof(Guid) }, typeof(HttpPostAttribute))]
        [TestCase(ClientChangeActivationAction, new[] { typeof(Guid) }, typeof(HttpPostAttribute))]
        [TestCase(ClientRefreshAction, new[] { typeof(Guid) }, typeof(HttpPostAttribute))]
        [TestCase(ClientActiveTokensAction, new[] { typeof(Guid) }, typeof(HttpGetAttribute))]
        [TestCase(ClientActiveTokensDataAction, new[] { typeof(Guid), typeof(int), typeof(int) }, typeof(HttpGetAttribute))]
        [TestCase(ActiveTokensAction, new[] { typeof(Guid) }, typeof(HttpGetAttribute))]
        [TestCase(ActiveTokensDataAction, new[] { typeof(Guid), typeof(int), typeof(int) }, typeof(HttpGetAttribute))]
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
            // arrange
            MockOrganizationService.Setup_GetOrganization_Returns_OrganizationReadResponse_Success();

            // act
            var result = SystemUnderTest.Create(OrganizationOneUid);

            // assert
            AssertViewWithModel<IntegrationCreateModel>(result);
            MockOrganizationService.Verify_GetOrganization();
        }

        [Test]
        public void Create_GET_FailedResponse()
        {
            // arrange
            MockOrganizationService.Setup_GetOrganization_Returns_OrganizationReadResponse_Failed();

            // act
            var result = SystemUnderTest.Create(OrganizationOneUid);

            // assert
            AssertViewAccessDenied(result);
            MockOrganizationService.Verify_GetOrganization();
        }

        [Test]
        public void Create_GET_InvalidResponse()
        {
            // arrange
            MockOrganizationService.Setup_GetOrganization_Returns_OrganizationReadResponse_Invalid();

            // act
            var result = SystemUnderTest.Create(OrganizationOneUid);

            // assert
            AssertViewAccessDenied(result);
            MockOrganizationService.Verify_GetOrganization();
        }

        [Test]
        public void Create_GET_InvalidParameter()
        {
            // arrange
            MockOrganizationService.Setup_GetOrganization_Returns_OrganizationReadResponse_Success();

            // act
            var result = SystemUnderTest.Create(OrganizationOneUid);

            // assert
            AssertViewWithModel<IntegrationCreateModel>(result);
            MockOrganizationService.Verify_GetOrganization();
        }

        [Test]
        public async Task Create_POST()
        {
            // arrange
            MockIntegrationService.Setup_CreateIntegration_Returns_IntegrationCreateResponse_Success();
            var model = GetIntegrationCreateModel();

            // act
            var result = await SystemUnderTest.Create(model);

            // assert
            ((RedirectResult)result).Url.ShouldBe($"/Integration/Detail/{EmptyUid}");
            MockIntegrationService.Verify_CreateIntegration();
        }

        [Test]
        public async Task Create_POST_FailedResponse()
        {
            // arrange
            MockIntegrationService.Setup_CreateIntegration_Returns_IntegrationCreateResponse_Failed();
            var model = GetIntegrationCreateModel();

            // act
            var result = await SystemUnderTest.Create(model);

            // assert
            AssertErrorMessagesForInvalidOrFailedResponse<IntegrationCreateModel>(result);
            MockIntegrationService.Verify_CreateIntegration();
        }

        [Test]
        public async Task Create_POST_InvalidResponse()
        {
            // arrange
            MockIntegrationService.Setup_CreateIntegration_Returns_IntegrationCreateResponse_Invalid();
            var model = GetIntegrationCreateModel();

            // act
            var result = await SystemUnderTest.Create(model);

            // assert
            AssertErrorMessagesForInvalidOrFailedResponse<IntegrationCreateModel>(result);
            MockIntegrationService.Verify_CreateIntegration();
        }

        [Test]
        public async Task Create_POST_InvalidModel()
        {
            // arrange
            var model = new IntegrationCreateModel();

            // act
            var result = await SystemUnderTest.Create(model);

            // assert
            AssertInputErrorMessagesOfView(result, model);
        }

        [Test]
        public async Task Detail_GET()
        {
            // arrange 
            MockIntegrationService.Setup_GetIntegration_Returns_IntegrationReadResponse_Success();

            // act
            var result = await SystemUnderTest.Detail(OrganizationOneIntegrationOneUid);

            // assert
            AssertViewWithModel<IntegrationDetailModel>(result);
            MockIntegrationService.Verify_GetIntegration();
        }

        [Test]
        public async Task Detail_GET_FailedResponse()
        {
            // arrange 
            MockIntegrationService.Setup_GetIntegration_Returns_IntegrationReadResponse_Failed();

            // act
            var result = await SystemUnderTest.Detail(OrganizationOneIntegrationOneUid);

            // assert
            AssertViewAccessDenied(result);
            MockIntegrationService.Verify_GetIntegration();
        }

        [Test]
        public async Task Detail_GET_InvalidResponse()
        {
            // arrange 
            MockIntegrationService.Setup_GetIntegration_Returns_IntegrationReadResponse_Invalid();

            // act
            var result = await SystemUnderTest.Detail(OrganizationOneIntegrationOneUid);

            // assert
            AssertViewAccessDenied(result);
            MockIntegrationService.Verify_GetIntegration();
        }

        [Test]
        public async Task Detail_GET_InvalidParameter()
        {
            // arrange
            MockIntegrationService.Setup_GetIntegration_Returns_IntegrationReadResponse_Invalid();

            // act
            var result = await SystemUnderTest.Detail(OrganizationOneIntegrationOneUid);

            // assert
            AssertViewAccessDenied(result);
        }

        [Test]
        public async Task Edit_GET()
        {
            // arrange 
            MockIntegrationService.Setup_GetIntegration_Returns_IntegrationReadResponse_Success();

            // act
            var result = await SystemUnderTest.Edit(OrganizationOneIntegrationOneUid);

            // assert
            AssertViewWithModel<IntegrationEditModel>(result);
            MockIntegrationService.Verify_GetIntegration();
        }

        [Test]
        public async Task Edit_GET_FailedResponse()
        {
            // arrange 
            MockIntegrationService.Setup_GetIntegration_Returns_IntegrationReadResponse_Failed();

            // act
            var result = await SystemUnderTest.Edit(OrganizationOneIntegrationOneUid);

            // assert
            AssertViewAccessDenied(result);
            MockIntegrationService.Verify_GetIntegration();
        }

        [Test]
        public async Task Edit_GET_InvalidResponse()
        {
            // arrange 
            MockIntegrationService.Setup_GetIntegration_Returns_IntegrationReadResponse_Invalid();

            // act
            var result = await SystemUnderTest.Edit(OrganizationOneIntegrationOneUid);

            // assert
            AssertViewAccessDenied(result);
            MockIntegrationService.Verify_GetIntegration();
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
            MockIntegrationService.Setup_EditIntegration_Returns_IntegrationEditResponse_Success();
            var model = GetIntegrationEditModel();

            // act
            var result = await SystemUnderTest.Edit(model);

            // assert
            ((RedirectResult)result).Url.ShouldBe($"/Integration/Detail/{UidOne}");
            MockIntegrationService.Verify_EditIntegration();
        }

        [Test]
        public async Task Edit_POST_FailedResponse()
        {
            // arrange 
            MockIntegrationService.Setup_EditIntegration_Returns_IntegrationEditResponse_Failed();
            var model = GetIntegrationEditModel();

            // act
            var result = await SystemUnderTest.Edit(model);

            // assert
            AssertErrorMessagesForInvalidOrFailedResponse<IntegrationEditModel>(result);
            MockIntegrationService.Verify_EditIntegration();
        }

        [Test]
        public async Task Edit_POST_InvalidResponse()
        {
            // arrange 
            MockIntegrationService.Setup_EditIntegration_Returns_IntegrationEditResponse_Invalid();
            var model = GetIntegrationEditModel();

            // act
            var result = await SystemUnderTest.Edit(model);

            // assert
            AssertErrorMessagesForInvalidOrFailedResponse<IntegrationEditModel>(result);
            MockIntegrationService.Verify_EditIntegration();
        }

        [Test]
        public async Task Edit_POST_InvalidModel()
        {
            // arrange
            var model = new IntegrationEditModel();

            // act
            var result = await SystemUnderTest.Edit(model);

            // assert
            AssertInputErrorMessagesOfView(result, model);
        }

        [Test]
        public async Task Delete_POST()
        {
            // arrange
            MockIntegrationService.Setup_DeleteIntegration_Returns_IntegrationDeleteResponse_Success();

            // act
            var result = (JsonResult)await SystemUnderTest.Delete(OrganizationOneIntegrationOneUid);

            // assert
            ((CommonResult)result.Value).IsOk.ShouldBe(true);
            MockIntegrationService.Verify_DeleteIntegration();
        }

        [Test]
        public async Task Delete_POST_FailedResponse()
        {
            // arrange
            MockIntegrationService.Setup_DeleteIntegration_Returns_IntegrationDeleteResponse_Failed();

            // act
            var result = await SystemUnderTest.Delete(OrganizationOneIntegrationOneUid);

            // assert
            var commonResult = ((CommonResult)((JsonResult)result).Value);
            commonResult.IsOk.ShouldBeFalse();
            commonResult.Messages.Any(x => x == StringOne);
            MockIntegrationService.Verify_DeleteIntegration();
        }

        [Test]
        public async Task Delete_POST_InvalidResponse()
        {
            // arrange
            MockIntegrationService.Setup_DeleteIntegration_Returns_IntegrationDeleteResponse_Invalid();

            // act
            var result = await SystemUnderTest.Delete(OrganizationOneIntegrationOneUid);

            // assert
            var commonResult = ((CommonResult)((JsonResult)result).Value);
            commonResult.IsOk.ShouldBeFalse();
            commonResult.Messages.Any(x => x == StringOne);
            MockIntegrationService.Verify_DeleteIntegration();
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
        public async Task Revisions_GET()
        {
            // arrange
            MockIntegrationService.Setup_GetIntegration_Returns_IntegrationReadResponse_Success();

            // act
            var result = await SystemUnderTest.Revisions(OrganizationOneIntegrationOneUid);

            // assert
            AssertViewWithModel<IntegrationRevisionReadListModel>(result);
            MockIntegrationService.Verify_GetIntegration();
        }

        [Test]
        public async Task Revisions_GET_InvalidParameter()
        {
            // arrange
            MockIntegrationService.Setup_GetIntegration_Returns_IntegrationReadResponse_Success();

            // act
            var result = await SystemUnderTest.Revisions(EmptyUid);

            // assert
            AssertViewRedirectToHome(result);
            MockIntegrationService.Verify_GetIntegration();
        }

        [Test]
        public void RevisionsData_GET()
        {
            // arrange
            MockIntegrationService.Setup_GetIntegrationRevisions_Returns_IntegrationRevisionReadListResponse_Success();

            // act
            var result = SystemUnderTest.RevisionsData(OrganizationOneIntegrationOneUid);

            // assert
            AssertViewAndHeaders(result, new[] { "revision", "revisioned_by", "revisioned_at", "integration_name", "is_active", "created_at", "" });
            MockIntegrationService.Verify_GetIntegrationRevisions();
        }

        [Test]
        public void RevisionsData_GET_FailedResponse()
        {
            // arrange
            MockIntegrationService.Setup_GetIntegrationRevisions_Returns_IntegrationRevisionReadListResponse_Failed();

            // act
            var result = SystemUnderTest.RevisionsData(OrganizationOneIntegrationOneUid);

            // assert
            AssertView<NotFoundResult>(result);
            MockIntegrationService.Verify_GetIntegrationRevisions();
        }

        [Test]
        public void RevisionsData_GET_InvalidResponse()
        {
            // arrange
            MockIntegrationService.Setup_GetIntegrationRevisions_Returns_IntegrationRevisionReadListResponse_Invalid();

            // act
            var result = SystemUnderTest.RevisionsData(OrganizationOneIntegrationOneUid);

            // assert
            AssertView<NotFoundResult>(result);
            MockIntegrationService.Verify_GetIntegrationRevisions();
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

    }
}