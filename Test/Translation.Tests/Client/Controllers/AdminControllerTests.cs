using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using NUnit.Framework;
using Shouldly;

using Translation.Client.Web.Controllers;
using Translation.Client.Web.Models.Admin;
using Translation.Client.Web.Models.Base;
using Translation.Client.Web.Models.Organization;
using Translation.Client.Web.Models.User;
using Translation.Tests.SetupHelpers;
using static Translation.Tests.TestHelpers.ActionMethodNameConstantTestHelper;
using static Translation.Tests.TestHelpers.FakeConstantTestHelper;
using static Translation.Tests.TestHelpers.AssertViewModelTestHelper;
using static Translation.Tests.TestHelpers.AssertModelTestHelper;
using static Translation.Tests.TestHelpers.FakeModelTestHelper;

namespace Translation.Tests.Client.Controllers
{
    [TestFixture]
    public class AdminControllerTests : ControllerBaseTests
    {
        public AdminController SystemUnderTest { get; set; }

        [SetUp]
        public void run_before_every_test()
        {
            Refresh();
            SystemUnderTest = Container.Resolve<AdminController>();
            SetControllerContext(SystemUnderTest);
        }

        [TestCase(AcceptInviteAction, new[] { typeof(Guid), typeof(string) }),
         TestCase(AcceptInviteAction, new[] { typeof(AdminAcceptInviteModel) }),
         TestCase(AcceptInviteDoneAction, new Type[] { })]
        public void Methods_Has_AllowAnonymousAttribute(string actionMethod, Type[] parameters)
        {
            var type = SystemUnderTest.GetType();
            var methodInfo = type.GetMethod(actionMethod, parameters);
            var attributes = methodInfo.GetCustomAttributes(typeof(AllowAnonymousAttribute), true);
            Assert.AreEqual(attributes.Length, 1);
        }

        [TestCase(DashboardAction, new Type[] { }, typeof(HttpGetAttribute)),
         TestCase(ListAction, new Type[] { }, typeof(HttpGetAttribute)),
         TestCase(ListDataAction, new[] { typeof(int), typeof(int) }, typeof(HttpGetAttribute)),
         TestCase(OrganizationListAction, new Type[] { }, typeof(HttpGetAttribute)),
         TestCase(OrganizationListDataAction, new[] { typeof(int), typeof(int) }, typeof(HttpGetAttribute)),
         TestCase(UserListAction, new Type[] { }, typeof(HttpGetAttribute)),
         TestCase(UserListDataAction, new[] { typeof(int), typeof(int) }, typeof(HttpGetAttribute)),
         TestCase(UserLoginLogListAction, new Type[] { }, typeof(HttpGetAttribute)),
         TestCase(UserLoginLogListDataAction, new[] { typeof(int), typeof(int) }, typeof(HttpGetAttribute)),
         TestCase(JournalListAction, new Type[] { }, typeof(HttpGetAttribute)),
         TestCase(JournalListDataAction, new[] { typeof(int), typeof(int) }, typeof(HttpGetAttribute)),
         TestCase(TokenRequestLogListAction, new Type[] { }, typeof(HttpGetAttribute)),
         TestCase(TokenRequestLogListDataAction, new[] { typeof(int), typeof(int) }, typeof(HttpGetAttribute)),
         TestCase(SendEmailLogListAction, new Type[] { }, typeof(HttpGetAttribute)),
         TestCase(SendEmailLogListDataAction, new[] { typeof(int), typeof(int) }, typeof(HttpGetAttribute)),
         TestCase(InviteAction, new Type[] { }, typeof(HttpGetAttribute)),
         TestCase(InviteAction, new Type[] { typeof(AdminInviteModel) }, typeof(HttpPostAttribute)),
         TestCase(InviteDoneAction, new Type[] { }, typeof(HttpGetAttribute)),
         TestCase(ChangeActivationAction, new[] { typeof(Guid) }, typeof(HttpPostAttribute)),
         TestCase(OrganizationChangeActivationAction, new[] { typeof(Guid) }, typeof(HttpPostAttribute)),
         TestCase(DegradeToUserAction, new[] { typeof(Guid) }, typeof(HttpPostAttribute)),
         TestCase(UserUpgradeToAdminAction, new[] { typeof(Guid) }, typeof(HttpPostAttribute)),
         TestCase(AcceptInviteAction, new[] { typeof(Guid), typeof(string) }, typeof(HttpGetAttribute)),
         TestCase(AcceptInviteAction, new[] { typeof(AdminAcceptInviteModel) }, typeof(HttpPostAttribute)),
         TestCase(AcceptInviteDoneAction, new Type[] { }, typeof(HttpGetAttribute))]
        public void Methods_Has_Http_Verb_Attributes(string actionMethod, Type[] parameters, Type httpVerbAttribute)
        {
            var type = SystemUnderTest.GetType();
            var methodInfo = type.GetMethod(actionMethod, parameters);
            var attributes = methodInfo.GetCustomAttributes(httpVerbAttribute, true);
            Assert.AreEqual(attributes.Length, 1);
        }

        [TestCase(InviteDoneAction)]
        [TestCase(InviteDoneAction)]
        public void Methods_Returning_ViewResult_Has_Model_With_Title(string methodName)
        {
            var methodInfo = SystemUnderTest.GetType().GetMethod(methodName);
            var result = (ViewResult)methodInfo.Invoke(SystemUnderTest, null);

            result.ShouldSatisfyAllConditions(() => result.ShouldNotBeNull(),
                                              () => result.ViewName.ShouldBeNullOrEmpty(),
                                              () => result.Model.ShouldNotBeNull());
        }

        [Test]
        public void Controller_Derived_From_ControllerBaseTests()
        {
            var type = SystemUnderTest.GetType();
            type.BaseType.Name.StartsWith("BaseController").ShouldBeTrue();
        }

        [Test]
        public void Dashboard_GET()
        {
            // arrange

            // act
            var result = SystemUnderTest.Dashboard();

            // assert
            AssertViewWithModel<AdminDashboardBaseModel>(result);
        }

        [Test]
        public void Dashboard_GET_InvalidParameter()
        {
            // arrange

            // act
            var result = SystemUnderTest.Dashboard();

            // assert
            AssertViewWithModel<AdminDashboardBaseModel>(result);
        }

        [Test]
        public void List_GET()
        {
            // arrange

            // act
            var result = SystemUnderTest.List();

            // assert
            AssertViewWithModel<AdminListBaseModel>(result);
        }

        [Test]
        public void ListData_GET()
        {
            // arrange
            MockAdminService.Setup_GetSuperAdmins_Returns_SuperAdminUserReadListResponse_Success();

            // act
            var result = SystemUnderTest.ListData(One, Two);

            // assert
            AssertView<JsonResult>(result);
            MockAdminService.Verify_GetSuperAdmins();
        }

        [Test]
        public void ListData_GET_FailedResponse()
        {
            // arrange
            MockAdminService.Setup_GetSuperAdmins_Returns_SuperAdminUserReadListResponse_Failed();

            // act
            var result = SystemUnderTest.ListData(One, Two);

            // assert
            AssertView<NotFoundResult>(result);
            MockAdminService.Verify_GetSuperAdmins();
        }

        [Test]
        public void ListData_GET_InvalidResponse()
        {
            // arrange
            MockAdminService.Setup_GetSuperAdmins_Returns_SuperAdminUserReadListResponse_Invalid();

            // act
            var result = SystemUnderTest.ListData(One, Two);

            // assert
            AssertView<NotFoundResult>(result);
            MockAdminService.Verify_GetSuperAdmins();
        }

        [TestCase(10, 10)]
        [TestCase(10, 1000)]
        [TestCase(-10, 10)]
        [TestCase(10, -10)]
        [TestCase(1000, 10)]
        public async Task ListData_GET_SetPaging(int skip, int take)
        {
            // arrange
            MockAdminService.Setup_GetSuperAdmins_Returns_SuperAdminUserReadListResponse_Success();

            // act
            var result = (JsonResult)await SystemUnderTest.ListData(skip, take);

            // assert
            AssertView<DataResult>(result);
            AssertPagingInfo(result);
        }

        [Test]
        public void OrganizationList_GET()
        {
            // arrange

            // act
            var result = SystemUnderTest.OrganizationList();

            // assert
            AssertViewWithModel<OrganizationListModel>(result);
        }

        [Test]
        public void OrganizationListData_GET()
        {
            // arrange
            MockAdminService.Setup_GetOrganizations_Returns_OrganizationReadListResponse_Success();

            // act
            var result = SystemUnderTest.OrganizationListData(One, Two);

            // assert
            AssertView<JsonResult>(result);
            MockAdminService.Verify_GetOrganizations();
        }

        [Test]
        public void OrganizationListData_GET_FailedResponse()
        {
            // arrange
            MockAdminService.Setup_GetOrganizations_Returns_OrganizationReadListResponse_Failed();

            // act
            var result = SystemUnderTest.OrganizationListData(One, Two);

            // assert
            AssertView<NotFoundResult>(result);
            MockAdminService.Verify_GetOrganizations();
        }

        [Test]
        public void OrganizationListData_GET_InvalidResponse()
        {
            // arrange
            MockAdminService.Setup_GetOrganizations_Returns_OrganizationReadListResponse_Invalid();

            // act
            var result = SystemUnderTest.OrganizationListData(One, Two);

            // assert
            AssertView<NotFoundResult>(result);
            MockAdminService.Verify_GetOrganizations();
        }

        [TestCase(10, 10)]
        [TestCase(10, 1000)]
        [TestCase(-10, 10)]
        [TestCase(10, -10)]
        [TestCase(1000, 10)]
        public async Task OrganizationListData_GET_SetPaging(int skip, int take)
        {
            // arrange
            MockOrganizationService.Setup_GetOrganizations_Returns_OrganizationReadListResponse_Success();

            // act
            var result = (JsonResult)await SystemUnderTest.OrganizationListData(skip, take);

            // assert
            AssertView<DataResult>(result);
            AssertPagingInfo(result);
            MockOrganizationService.Verify_GetOrganizations();
        }

        [Test]
        public void UserList_GET()
        {
            // arrange

            // act
            var result = SystemUnderTest.UserList();

            // assert
            AssertViewWithModel<AllUserListModel>(result);
        }

        [Test]
        public void UserListData_GET()
        {
            // arrange
            MockAdminService.Setup_GetAllUsers_Returns_AllUserReadListResponse_Success();

            // act
            var result = SystemUnderTest.UserListData(One, Two);

            // assert
            AssertView<JsonResult>(result);
            MockAdminService.Verify_GetAllUsers();
        }

        [Test]
        public void UserListData_GET_FailedResponse()
        {
            // arrange
            MockAdminService.Setup_GetAllUsers_Returns_AllUserReadListResponse_Failed();

            // act
            var result = SystemUnderTest.UserListData(One, Two);

            // assert
            AssertView<NotFoundResult>(result);
            MockAdminService.Verify_GetAllUsers();
        }

        [Test]
        public void UserListData_GET_InvalidResponse()
        {
            // arrange
            MockAdminService.Setup_GetAllUsers_Returns_AllUserReadListResponse_Invalid();

            // act
            var result = SystemUnderTest.UserListData(One, Two);

            // assert
            AssertView<NotFoundResult>(result);
            MockAdminService.Verify_GetAllUsers();
        }

        [TestCase(10, 10)]
        [TestCase(10, 1000)]
        [TestCase(-10, 10)]
        [TestCase(10, -10)]
        [TestCase(1000, 10)]
        public async Task UserListData_GET_SetPaging(int skip, int take)
        {
            // arrange
            MockAdminService.Setup_GetAllUsers_Returns_AllUserReadListResponse_Success();

            // act
            var result = (JsonResult)await SystemUnderTest.UserListData(skip, take);

            // assert
            AssertView<DataResult>(result);
            AssertPagingInfo(result);
        }

        [Test]
        public void UserLoginLogList_GET()
        {
            // arrange

            // act
            var result = SystemUnderTest.UserLoginLogList();

            // assert
            AssertViewWithModel<UserLoginLogListModel>(result);
        }
    }
}