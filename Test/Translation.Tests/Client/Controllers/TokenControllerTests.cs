using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using Shouldly;

using Translation.Client.Web.Controllers;
using Translation.Client.Web.Models.Base;
using Translation.Tests.SetupHelpers;
using static Translation.Tests.TestHelpers.ActionMethodNameConstantTestHelper;
using static Translation.Tests.TestHelpers.FakeConstantTestHelper;
using static Translation.Tests.TestHelpers.AssertViewModelTestHelper;

namespace Translation.Tests.Client.Controllers
{
    [TestFixture]
    public class TokenControllerTests : ControllerBaseTests
    {
        public TokenController SystemUnderTest { get; set; }

        [SetUp]
        public void run_before_every_test()
        {
            Refresh();
            SystemUnderTest = Container.Resolve<TokenController>();
            SetControllerContext(SystemUnderTest);
        }

        [TestCase(RevokeAction, new[] { typeof(Guid), typeof(Guid) }, typeof(HttpPostAttribute)),
         TestCase(CreateAction, new Type[] { typeof(Guid), typeof(Guid) }, typeof(HttpPostAttribute)),
         TestCase(GetAction, new Type[] { }, typeof(HttpGetAttribute))]
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
        public async Task Revoke_POST()
        {
            // arrange
            MockIntegrationService.Setup_RevokeToken_Returns_TokenRevokeResponse_Success();

            // act
            var result = await SystemUnderTest.Revoke(UidOne, UidTwo);

            // assert
            AssertView<JsonResult>(result);
            MockIntegrationService.Verify_RevokeToken();
        }

        [Test]
        public async Task Revoke_POST_FailedResponse()
        {
            // arrange
            MockIntegrationService.Setup_RevokeToken_Returns_TokenRevokeResponse_Failed();

            // act
            var result = await SystemUnderTest.Revoke(UidOne, UidTwo);

            // assert
            AssertView<CommonResult>(result);
            MockIntegrationService.Verify_RevokeToken();
        }

        [Test]
        public async Task Revoke_POST_InvalidResponse()
        {
            // arrange
            MockIntegrationService.Setup_RevokeToken_Returns_TokenRevokeResponse_Invalid();

            // act
            var result = await SystemUnderTest.Revoke(UidOne, UidTwo);

            // assert
            AssertView<CommonResult>(result);
            MockIntegrationService.Verify_RevokeToken();
        }

        [Test]
        public async Task Revoke_POST_InvalidParameter()
        {
            // arrange

            // act
            var result = await SystemUnderTest.Revoke(EmptyUid, UidTwo);

            // assert
            AssertViewAccessDenied(result);
        }

        [Test]
        public async Task Create_POST()
        {
            // arrange
            MockIntegrationService.Setup_CreateToken_Returns_TokenCreateResponse_Success();

            // act
            var result = await SystemUnderTest.Create(UidOne, UidTwo);

            // assert
            AssertView<JsonResult>(result);
            MockIntegrationService.Verify_CreateToken();
        }

        [Test]
        public async Task Create_POST_FailedResponse()
        {
            // arrange
            MockIntegrationService.Setup_CreateToken_Returns_TokenCreateResponse_Failed();

            // act
            var result = (ObjectResult)await SystemUnderTest.Create(UidOne, UidTwo);

            // assert
            AssertView<ObjectResult>(result);
            MockIntegrationService.Verify_CreateToken();
        }

        [Test]
        public async Task Create_POST_InvalidResponse()
        {
            // arrange
            MockIntegrationService.Setup_CreateToken_Returns_TokenCreateResponse_Invalid();

            // act
            var result = (ObjectResult)await SystemUnderTest.Create(UidOne, UidTwo);

            // assert
            AssertView<ObjectResult>(result);
            MockIntegrationService.Verify_CreateToken();
        }

        [Test]
        public async Task Create_POST_InvalidParameter()
        {
            // arrange

            // act
            var result = await SystemUnderTest.Create(EmptyUid, UidTwo);

            // assert
            AssertView<JsonResult>(result);
        }

        [Test]
        public async Task Get_GET()
        {
            // arrange
            MockIntegrationService.Setup_CreateTokenWhenUserAuthenticated_Returns_TokenCreateResponse_Success();

            // act
            var result = await SystemUnderTest.Get();

            // assert
            AssertView<JsonResult>(result);
            MockIntegrationService.Verify_CreateTokenWhenUserAuthenticated();
        }

        [Test]
        public async Task Get_GET_FailedResponse()
        {
            // arrange
            MockIntegrationService.Setup_CreateTokenWhenUserAuthenticated_Returns_TokenCreateResponse_Failed();

            // act
            var result = (ObjectResult)await SystemUnderTest.Get();

            // assert
            AssertView<ObjectResult>(result);
            MockIntegrationService.Verify_CreateTokenWhenUserAuthenticated();
        }

        [Test]
        public async Task Get_GET_InvalidResponse()
        {
            // arrange
            MockIntegrationService.Setup_CreateTokenWhenUserAuthenticated_Returns_TokenCreateResponse_Invalid();

            // act
            var result = (ObjectResult)await SystemUnderTest.Get();

            // assert
            AssertView<ObjectResult>(result);
            MockIntegrationService.Verify_CreateTokenWhenUserAuthenticated();
        }

        [Test]
        public async Task Get_GET_CurrentUserNull()
        {
            // arrange
             MockOrganizationService.Reset();

            // act
            var result = (ObjectResult)await SystemUnderTest.Get();

            // assert
            AssertView<ObjectResult>(result);
        }
    }
}
