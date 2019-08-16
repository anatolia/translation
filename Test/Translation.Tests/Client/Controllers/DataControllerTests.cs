using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using Shouldly;

using Translation.Client.Web.Controllers;
using Translation.Client.Web.Models.Data;
using Translation.Client.Web.Models.Label;

using Translation.Tests.SetupHelpers;
using static Translation.Tests.TestHelpers.ActionMethodNameConstantTestHelper;
using static Translation.Tests.TestHelpers.FakeConstantTestHelper;
using static Translation.Tests.TestHelpers.AssertViewModelTestHelper;
using static Translation.Tests.TestHelpers.FakeModelTestHelper;

namespace Translation.Tests.Client.Controllers
{
    [TestFixture]
    public class DataControllerTests : ControllerBaseTests
    {
        public DataController SystemUnderTest { get; set; }

        [SetUp]
        public void run_before_every_test()
        {
            Refresh();
            SystemUnderTest = Container.Resolve<DataController>();
            SetControllerContext(SystemUnderTest);
        }

        [TestCase(GetLabelsAction, new[] { typeof(Guid), typeof(Guid) }, typeof(HttpGetAttribute)),
         TestCase(GetMainLabelsAction, new Type[] { }, typeof(HttpGetAttribute)),
         TestCase(GetCurrentUserAction, new Type[] { }, typeof(HttpGetAttribute)),
         TestCase(AddLabelAction, new[] { typeof(DataAddLabelModel) }, typeof(HttpPostAttribute))]
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
        public async Task GetLabels_GET()
        {
            // arrange
            MockIntegrationService.Setup_ValidateToken_Returns_TokenValidateResponse_Success();
            MockLabelService.Setup_GetLabelsWithTranslations_Returns_AllLabelReadListResponse_Success();

            // act
            var result = await SystemUnderTest.GetLabels(UidOne, UidTwo);

            // assert
            AssertView<JsonResult>(result);
            MockIntegrationService.Verify_ValidateToken();
            MockLabelService.Verify_GetLabelsWithTranslations();
        }

        [Test]
        public async Task GetLabels_GET_Failed_AllLabelReadListResponse()
        {
            // arrange
            MockIntegrationService.Setup_ValidateToken_Returns_TokenValidateResponse_Success();
            MockLabelService.Setup_GetLabelsWithTranslations_Returns_AllLabelReadListResponse_Failed();

            // act
            var result = await SystemUnderTest.GetLabels(UidOne, UidTwo);

            // assert
            AssertView<JsonResult>(result);
            MockIntegrationService.Verify_ValidateToken();
            MockLabelService.Verify_GetLabelsWithTranslations();
        }

        [Test]
        public async Task GetLabels_GET_Invalid_AllLabelReadListResponse()
        {
            // arrange
            MockIntegrationService.Setup_ValidateToken_Returns_TokenValidateResponse_Success();
            MockLabelService.Setup_GetLabelsWithTranslations_Returns_AllLabelReadListResponse_Invalid();

            // act
            var result = await SystemUnderTest.GetLabels(UidOne, UidTwo);

            // assert
            MockIntegrationService.Verify_ValidateToken();
            MockLabelService.Verify_GetLabelsWithTranslations();
        }

        [Test]
        public async Task GetLabels_GET_Failed_TokenValidateResponse()
        {
            // arrange
            MockIntegrationService.Setup_ValidateToken_Returns_TokenValidateResponse_Failed();

            // act
            var result = await SystemUnderTest.GetLabels(UidOne, UidTwo);

            // assert
            AssertView<JsonResult>(result);
            MockIntegrationService.Verify_ValidateToken();
        }

        [Test]
        public async Task GetLabels_GET_Invalid_TokenValidateResponse()
        {
            // arrange
            MockIntegrationService.Setup_ValidateToken_Returns_TokenValidateResponse_Invalid();

            // act
            var result = await SystemUnderTest.GetLabels(UidOne, UidTwo);

            // assert
            MockIntegrationService.Verify_ValidateToken();
        }

        [Test]
        public async Task GetLabels_GET_InvalidParameter()
        {
            // arrange

            // act
            var result = await SystemUnderTest.GetLabels(EmptyUid, UidTwo);

            // assert

        }

        [Test]
        public async Task GetMainLabels_GET()
        {
            // arrange
            MockLabelService.Setup_GetLabelsWithTranslations_Returns_AllLabelReadListResponse_Success();

            // act
            var result = await SystemUnderTest.GetMainLabels();

            // assert
            AssertView<JsonResult>(result);
            MockLabelService.Verify_GetLabelsWithTranslations();
        }

        [Test]
        public async Task GetMainLabels_FailedResponse()
        {
            // arrange
            MockLabelService.Setup_GetLabelsWithTranslations_Returns_AllLabelReadListResponse_Failed();

            // act
            var result = await SystemUnderTest.GetMainLabels();

            // assert
            AssertView<JsonResult>(result);
            MockLabelService.Verify_GetLabelsWithTranslations();
        }

        [Test]
        public async Task GetMainLabels_InvalidResponse()
        {
            // arrange
            MockLabelService.Setup_GetLabelsWithTranslations_Returns_AllLabelReadListResponse_Invalid();

            // act
            var result = await SystemUnderTest.GetMainLabels();

            // assert
            AssertView<JsonResult>(result);
            MockLabelService.Verify_GetLabelsWithTranslations();
        }

        [Test]
        public void GetCurrentUser_GET()
        {
            // arrange

            // act
            var result = SystemUnderTest.GetCurrentUser();

            // assert
            AssertView<JsonResult>(result);
        }

        [Test]
        public void GetCurrentUser_GET_Null()
        {
            // arrange
            MockOrganizationService.Reset();

            // act
            var result = (JsonResult)SystemUnderTest.GetCurrentUser();

            // assert
            result.Value.ShouldBe(null);
        }

        [Test]
        public async Task AddLabel_GET()
        {
            // arrange
            MockIntegrationService.Setup_ValidateToken_Returns_TokenValidateResponse_Success();
            MockLabelService.Setup_LabelCreateWithToken_Returns_LabelCreateResponse_Success();
            var model = GetMDataAddLabelModel();

            // act
            var result = await SystemUnderTest.AddLabel(model);

            // assert
            AssertView<JsonResult>(result);
            MockIntegrationService.Verify_ValidateToken();
            MockLabelService.Verify_LabelCreateWithToken();
        }

        [Test]
        public async Task AddLabel_POST_Failed_LabelCreateResponse()
        {
            // arrange
            MockIntegrationService.Setup_ValidateToken_Returns_TokenValidateResponse_Success();
            MockLabelService.Setup_LabelCreateWithToken_Returns_LabelCreateResponse_Failed();
            var model = GetMDataAddLabelModel();

            // act
            var result = await SystemUnderTest.AddLabel(model);

            // assert
            AssertView<JsonResult>(result);
            MockIntegrationService.Verify_ValidateToken();
            MockLabelService.Verify_LabelCreateWithToken();
        }

        [Test]
        public async Task AddLabel_POST_Invalid_AllLabelReadListResponse()
        {
            // arrange
            MockIntegrationService.Setup_ValidateToken_Returns_TokenValidateResponse_Success();
            MockLabelService.Setup_LabelCreateWithToken_Returns_LabelCreateResponse_Invalid();
            var model = GetMDataAddLabelModel();

            // act
            var result = await SystemUnderTest.AddLabel(model);

            // assert
            MockIntegrationService.Verify_ValidateToken();
            MockLabelService.Verify_LabelCreateWithToken();
        }

        [Test]
        public async Task AddLabel_POST_Failed_TokenValidateResponse()
        {
            // arrange
            MockIntegrationService.Setup_ValidateToken_Returns_TokenValidateResponse_Failed();
            var model = GetMDataAddLabelModel();

            // act
            var result = await SystemUnderTest.AddLabel(model);

            // assert
            AssertView<JsonResult>(result);
            MockIntegrationService.Verify_ValidateToken();
        }

        [Test]
        public async Task AddLabel_POST_Invalid_TokenValidateResponse()
        {
            // arrange
            MockIntegrationService.Setup_ValidateToken_Returns_TokenValidateResponse_Invalid();
            var model = GetMDataAddLabelModel();

            // act
            var result = await SystemUnderTest.AddLabel(model);

            // assert
            MockIntegrationService.Verify_ValidateToken();
        }

        [Test]
        public async Task AddLabel_POST_InvalidModel()
        {
            // arrange
          
            var model = new DataAddLabelModel();

            // act
            var result = await SystemUnderTest.AddLabel(model);

            // assert
           AssertView<JsonResult>(result);
        }


    }
}