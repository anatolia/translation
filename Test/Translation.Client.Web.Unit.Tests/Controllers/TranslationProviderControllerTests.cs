using System;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using Shouldly;

using Translation.Client.Web.Controllers;
using Translation.Client.Web.Models.Base;
using Translation.Client.Web.Models.TranslationProvider;
using Translation.Client.Web.Unit.Tests.ServiceSetupHelpers;
using static Translation.Client.Web.Unit.Tests.TestHelpers.ActionMethodNameConstantTestHelper;
using static Translation.Client.Web.Unit.Tests.TestHelpers.AssertViewModelTestHelper;
using static Translation.Common.Tests.TestHelpers.FakeConstantTestHelper;
using static Translation.Client.Web.Unit.Tests.TestHelpers.FakeModelTestHelper;

namespace Translation.Client.Web.Unit.Tests.Controllers
{
    [TestFixture]
    public class TranslationProviderControllerTests : ControllerBaseTests
    {
        public TranslationProviderController SystemUnderTest { get; set; }

        [SetUp]
        public void run_before_every_test()
        {
            Refresh();
            SystemUnderTest = Container.Resolve<TranslationProviderController>();
            SetControllerContext(SystemUnderTest);
        }

        [TestCase(ListAction, new Type[] { }, typeof(HttpGetAttribute)),
         TestCase(ListDataAction, new[] { typeof(int), typeof(int) }, typeof(HttpGetAttribute)),
         TestCase(ChangeActivationAction, new[] { typeof(Guid) }, typeof(HttpPostAttribute)),
         TestCase(EditAction, new[] { typeof(Guid) }, typeof(HttpGetAttribute)),
         TestCase(EditAction, new[] { typeof(TranslationProviderEditModel) }, typeof(HttpPostAttribute)),
         TestCase(DetailAction, new[] { typeof(Guid) }, typeof(HttpGetAttribute))]
        public void Methods_Has_Http_Verb_Attributes(string actionMethod, Type[] parameters, Type httpVerbAttribute)
        {
            var type = SystemUnderTest.GetType();
            var methodInfo = type.GetMethod(actionMethod, parameters);
            var attributes = methodInfo.GetCustomAttributes(httpVerbAttribute, true);
            Assert.AreEqual(attributes.Length, 1);
        }


        [Test]
        public void Controller_Derived_From_ControllerBaseTests()
        {
            var type = SystemUnderTest.GetType();
            type.BaseType.Name.StartsWith("BaseController").ShouldBeTrue();
        }

        [Test]
        public void List_GET()
        {
            // arrange

            // act
            var result = SystemUnderTest.List();

            // assert
            AssertViewWithModel<TranslationProviderListModel>(result);
        }

        [Test]
        public void ListData_GET()
        {
            // arrange
            MockTranslationProviderService.Setup_GetTranslationProviders_Returns_TranslationProviderReadListResponse_Success();

            // act
            var result = SystemUnderTest.ListData(One, Two);

            // assert
            AssertView<JsonResult>(result);
            MockTranslationProviderService.Verify_GetTranslationProviders();
        }

        [Test]
        public void ListData_GET_FailedResponse()
        {
            // arrange
            MockTranslationProviderService.Setup_GetTranslationProviders_Returns_TranslationProviderReadListResponse_Failed();

            // act
            var result = SystemUnderTest.ListData(One, Two);

            // assert
            AssertView<NotFoundResult>(result);
            MockTranslationProviderService.Verify_GetTranslationProviders();
        }

        [Test]
        public void ListData_GET_InvalidResponse()
        {
            // arrange
            MockTranslationProviderService.Setup_GetActiveTranslationProvider_Returns_TranslationProviderReadListResponse_Invalid();

            // act
            var result = SystemUnderTest.ListData(One, Two);

            // assert
            AssertView<NotFoundResult>(result);
            MockTranslationProviderService.Verify_GetTranslationProviders();
        }

        [TestCase(10, 10)]
        [TestCase(10, 1000)]
        [TestCase(-10, 10)]
        [TestCase(10, -10)]
        [TestCase(1000, 10)]
        public async Task ListData_GET_SetPaging(int skip, int take)
        {
            // arrange
            MockTranslationProviderService.Setup_GetTranslationProviders_Returns_TranslationProviderReadListResponse_Success();

            // act
            var result = (JsonResult)await SystemUnderTest.ListData(skip, take);

            // assert
            AssertView<DataResult>(result);
            AssertPagingInfo(result);
        }

        [Test]
        public async Task ChangeActivation_GET()
        {
            // arrange
            MockAdminService.Setup_TranslationProviderChangeActivation_Returns_TranslationProviderChangeActivationResponse_Success();

            // act
            var result = await SystemUnderTest.ChangeActivation(UidOne);

            // assert
            AssertView<NotFoundResult>(result);
            MockAdminService.Verify_TranslationProviderChangeActivation();
        }

        [Test]
        public async Task ChangeActivation_GET_InvalidResponse()
        {
            // arrange
            MockAdminService.Setup_TranslationProviderChangeActivation_Returns_TranslationProviderChangeActivationResponse_Invalid();

            // act
            var result = await SystemUnderTest.ChangeActivation(UidOne);

            // assert
            AssertView<JsonResult>(result);
            MockAdminService.Verify_TranslationProviderChangeActivation();
        }

        [Test]
        public async Task ChangeActivation_GET_FailedResponse()
        {
            // arrange
            MockAdminService.Setup_TranslationProviderChangeActivation_Returns_TranslationProviderChangeActivationResponse_Failed();

            // act
            var result = await SystemUnderTest.ChangeActivation(UidOne);

            // assert
            AssertView<JsonResult>(result);
            MockAdminService.Verify_TranslationProviderChangeActivation();
        }

        [Test]
        public async Task ChangeActivation_GET_InvalidParameter()
        {
            // arrange

            // act
            var result = await SystemUnderTest.ChangeActivation(EmptyUid);

            // assert
            AssertView<ForbidResult>(result);
        }

        [Test]
        public async Task Edit_GET()
        {
            // arrange
            MockTranslationProviderService.Setup_GetTranslationProvider_Returns_TranslationProviderReadResponse_Success();

            // act
            var result = await SystemUnderTest.Edit(UidOne);

            // assert
            AssertView<TranslationProviderEditModel>(result);
            MockTranslationProviderService.Verify_GetTranslationProvider();
        }

        [Test]
        public async Task Edit_GET_InvalidResponse()
        {
            // arrange
            MockTranslationProviderService.Setup_GetTranslationProvider_Returns_TranslationProviderReadResponse_Invalid();

            // act
            var result = await SystemUnderTest.Edit(UidOne);

            // assert
            AssertViewAccessDenied(result);
            MockTranslationProviderService.Verify_GetTranslationProvider();
        }

        [Test]
        public async Task Edit_GET_FailedResponse()
        {
            // arrange
            MockTranslationProviderService.Setup_GetTranslationProvider_Returns_TranslationProviderReadResponse_Failed();

            // act
            var result = await SystemUnderTest.Edit(UidOne);

            // assert
            AssertViewAccessDenied(result);
            MockTranslationProviderService.Verify_GetTranslationProvider();
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
            var model = GetTranslationProviderEditModel();
            MockTranslationProviderService.Setup_EditTranslationProvider_Returns_TranslationProviderEditResponse_Success();

            // act
            var result = await SystemUnderTest.Edit(model);

            // assert
            ((RedirectResult)result).Url.ShouldBe($"/TranslationProvider/Detail/{UidTwo}");
            MockTranslationProviderService.Verify_EditTranslationProvider();
        }

        [Test]
        public async Task Edit_POST_InvalidResponse()
        {
            // arrange
            var model = GetTranslationProviderEditModel();
            MockTranslationProviderService.Setup_EditTranslationProvider_Returns_TranslationProviderEditResponse_Invalid();

            // act
            var result = await SystemUnderTest.Edit(model);

            // assert
            AssertView<TranslationProviderEditModel>(result);
            MockTranslationProviderService.Verify_EditTranslationProvider();
        }

        [Test]
        public async Task Edit_POST_FailedResponse()
        {
            // arrange
            var model = GetTranslationProviderEditModel();
            MockTranslationProviderService.Setup_EditTranslationProvider_Returns_TranslationProviderEditResponse_Failed();

            // act
            var result = await SystemUnderTest.Edit(model);

            // assert
            AssertView<TranslationProviderEditModel>(result);
            MockTranslationProviderService.Verify_EditTranslationProvider();
        }

        [Test]
        public async Task Edit_POST_InvalidParameter()
        {
            // arrange
            var model = new TranslationProviderEditModel();

            // act
            var result = await SystemUnderTest.Edit(model);

            // assert
            AssertView<TranslationProviderEditModel>(result);
        }

        [Test]
        public async Task Detail_GET()
        {
            // arrange
            MockTranslationProviderService.Setup_GetTranslationProvider_Returns_TranslationProviderReadResponse_Success();

            // act
            var result = await SystemUnderTest.Detail(UidOne);

            // assert
            AssertView<TranslationProviderDetailModel>(result);
            MockTranslationProviderService.Verify_GetTranslationProvider();
        }

        [Test]
        public async Task Detail_GET_InvalidResponse()
        {
            // arrange
            MockTranslationProviderService.Setup_GetTranslationProvider_Returns_TranslationProviderReadResponse_Invalid();

            // act
            var result = await SystemUnderTest.Detail(UidOne);

            // assert
            AssertViewAccessDenied(result);
            MockTranslationProviderService.Verify_GetTranslationProvider();
        }

        [Test]
        public async Task Detail_GET_FailedResponse()
        {
            // arrange
            MockTranslationProviderService.Setup_GetTranslationProvider_Returns_TranslationProviderReadResponse_Failed();

            // act
            var result = await SystemUnderTest.Detail(UidOne);

            // assert
            AssertViewAccessDenied(result);
            MockTranslationProviderService.Verify_GetTranslationProvider();
        }

        [Test]
        public async Task Detail_GET_InvalidParameter()
        {
            // arrange

            // act
            var result = await SystemUnderTest.Detail(EmptyUid);

            // assert
            AssertViewAccessDenied(result);
        }

    }
}