using System;
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
using Translation.Client.Web.Unit.Tests.ServiceSetupHelpers;

using static Translation.Client.Web.Unit.Tests.TestHelpers.ActionMethodNameConstantTestHelper;
using static Translation.Client.Web.Unit.Tests.TestHelpers.AssertViewModelTestHelper;
using static Translation.Common.Tests.TestHelpers.FakeConstantTestHelper;
using static Translation.Client.Web.Unit.Tests.TestHelpers.FakeModelTestHelper;

namespace Translation.Client.Web.Unit.Tests.Controllers
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
            MockOrganizationService.Setup_GetOrganizations_Returns_OrganizationReadListResponse_Success();

            // act
            var result = SystemUnderTest.OrganizationListData(One, Two);

            // assert
            AssertView<JsonResult>(result);
            MockOrganizationService.Verify_GetOrganizations();
        }

        [Test]
        public void OrganizationListData_GET_FailedResponse()
        {
            // arrange
            MockOrganizationService.Setup_GetOrganizations_Returns_OrganizationReadListResponse_Failed();

            // act
            var result = SystemUnderTest.OrganizationListData(One, Two);

            // assert
            AssertView<NotFoundResult>(result);
            MockOrganizationService.Verify_GetOrganizations();
        }

        [Test]
        public void OrganizationListData_GET_InvalidResponse()
        {
            // arrange
            MockOrganizationService.Setup_GetOrganizations_Returns_OrganizationReadListResponse_Invalid();

            // act
            var result = SystemUnderTest.OrganizationListData(One, Two);

            // assert
            AssertView<NotFoundResult>(result);
            MockOrganizationService.Verify_GetOrganizations();
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

        [Test]
        public void UserLoginLogListData_GET()
        {
            // arrange
            MockAdminService.Setup_GetAllUserLoginLogs_Returns_AllLoginLogReadListResponse_Success();

            // act
            var result = SystemUnderTest.UserLoginLogListData(One, Two);

            // assert
            AssertView<JsonResult>(result);
            MockAdminService.Verify_GetAllUserLoginLogs();
        }

        [Test]
        public void UserLoginLogListData_GET_FailedResponse()
        {
            // arrange
            MockAdminService.Setup_GetAllUserLoginLogs_Returns_AllLoginLogReadListResponse_Failed();

            // act
            var result = SystemUnderTest.UserLoginLogListData(One, Two);

            // assert
            AssertView<NotFoundResult>(result);
            MockAdminService.Verify_GetAllUserLoginLogs();
        }

        [Test]
        public void UserLoginLogListData_GET_InvalidResponse()
        {
            // arrange
            MockAdminService.Setup_GetAllUserLoginLogs_Returns_AllLoginLogReadListResponse_Invalid();

            // act
            var result = SystemUnderTest.UserLoginLogListData(One, Two);

            // assert
            AssertView<NotFoundResult>(result);
            MockAdminService.Verify_GetAllUserLoginLogs();
        }

        [Test]
        public void JournalList_GET()
        {
            // arrange

            // act
            var result = SystemUnderTest.JournalList();

            // assert
            AssertViewWithModel<JournalListModel>(result);
        }

        [Test]
        public void JournalListData_GET()
        {
            // arrange
            MockAdminService.Setup_GetJournals_Returns_JournalReadListResponse_Success();

            // act
            var result = SystemUnderTest.JournalListData(One, Two);

            // assert
            AssertView<JsonResult>(result);
            MockAdminService.Verify_GetJournals();
        }

        [Test]
        public void JournalListData_GET_FailedResponse()
        {
            // arrange
            MockAdminService.Setup_GetJournals_Returns_JournalReadListResponse_Failed();

            // act
            var result = SystemUnderTest.JournalListData(One, Two);

            // assert
            AssertView<NotFoundResult>(result);
            MockAdminService.Verify_GetJournals();
        }

        [Test]
        public void JournalListData_GET_InvalidResponse()
        {
            // arrange
            MockAdminService.Setup_GetJournals_Returns_JournalReadListResponse_Invalid();

            // act
            var result = SystemUnderTest.JournalListData(One, Two);

            // assert
            AssertView<NotFoundResult>(result);
            MockAdminService.Verify_GetJournals();
        }

        [Test]
        public void TokenRequestLogList_GET()
        {
            // arrange

            // act
            var result = SystemUnderTest.TokenRequestLogList();

            // assert
            AssertViewWithModel<TokenRequestLogListModel>(result);
        }

        [Test]
        public void TokenRequestLogListData_GET()
        {
            // arrange
            MockAdminService.Setup_GetTokenRequestLogs_Returns_AllJournalReadListResponse_Success();

            // act
            var result = SystemUnderTest.TokenRequestLogListData(One, Two);

            // assert
            AssertView<JsonResult>(result);
            MockAdminService.Verify_GetTokenRequestLogs();
        }

        [Test]
        public void TokenRequestLogListData_GET_FailedResponse()
        {
            // arrange
            MockAdminService.Setup_GetTokenRequestLogs_Returns_AllJournalReadListResponse_Failed();

            // act
            var result = SystemUnderTest.TokenRequestLogListData(One, Two);

            // assert
            AssertView<NotFoundResult>(result);
            MockAdminService.Verify_GetTokenRequestLogs();
        }

        [Test]
        public void TokenRequestLogListData_GET_InvalidResponse()
        {
            // arrange
            MockAdminService.Setup_GetTokenRequestLogs_Returns_AllJournalReadListResponse_Invalid();

            // act
            var result = SystemUnderTest.TokenRequestLogListData(One, Two);

            // assert
            AssertView<NotFoundResult>(result);
            MockAdminService.Verify_GetTokenRequestLogs();
        }

        [Test]
        public void SendEmailLogList_GET()
        {
            // arrange

            // act
            var result = SystemUnderTest.SendEmailLogList();

            // assert
            AssertViewWithModel<SendEmailLogListModel>(result);
        }

        [Test]
        public void SendEmailLogListData_GET()
        {
            // arrange
            MockAdminService.Setup_GetSendEmailLogs_Returns_AllSendEmailReadListResponse_Success();

            // act
            var result = SystemUnderTest.SendEmailLogListData(One, Two);

            // assert
            AssertView<JsonResult>(result);
            MockAdminService.Verify_GetSendEmailLogs();
        }

        [Test]
        public void SendEmailLogListData_GET_FailedResponse()
        {
            // arrange
            MockAdminService.Setup_GetSendEmailLogs_Returns_AllSendEmailReadListResponse_Failed();

            // act
            var result = SystemUnderTest.SendEmailLogListData(One, Two);

            // assert
            AssertView<NotFoundResult>(result);
            MockAdminService.Verify_GetSendEmailLogs();
        }

        [Test]
        public void SendEmailLogListData_GET_InvalidResponse()
        {
            // arrange
            MockAdminService.Setup_GetSendEmailLogs_Returns_AllSendEmailReadListResponse_Invalid();

            // act
            var result = SystemUnderTest.SendEmailLogListData(One, Two);

            // assert
            AssertView<NotFoundResult>(result);
            MockAdminService.Verify_GetSendEmailLogs();
        }

        [Test]
        public void Invite_GET()
        {
            // arrange

            // act
            var result = SystemUnderTest.Invite();

            // assert
            AssertViewWithModel<AdminInviteModel>(result);
        }

        [Test]
        public async Task Invite_POST()
        {
            // arrange
            MockAdminService.Setup_InviteSuperAdminUser_Returns_AdminInviteResponse_Success();
            var model = GetAdminInviteModel();

            // act
            var result = await SystemUnderTest.Invite(model);

            // assert
            ((RedirectResult)result).Url.ShouldBe("/Admin/InviteDone/");
            MockAdminService.Verify_InviteSuperAdminUser();
        }

        [Test]
        public async Task Invite_POST_FailedResponse()
        {
            // arrange
            MockAdminService.Setup_InviteSuperAdminUser_Returns_AdminInviteResponse_Failed();
            var model = GetAdminInviteModel();

            // act
            var result = await SystemUnderTest.Invite(model);

            // assert
            AssertErrorMessagesForInvalidOrFailedResponse<AdminInviteModel>(result);
            MockAdminService.Verify_InviteSuperAdminUser();
        }

        [Test]
        public async Task Invite_POST_InvalidResponse()
        {
            // arrange
            MockAdminService.Setup_InviteSuperAdminUser_Returns_AdminInviteResponse_Invalid();
            var model = GetAdminInviteModel();

            // act
            var result = await SystemUnderTest.Invite(model);

            // assert
            AssertErrorMessagesForInvalidOrFailedResponse<AdminInviteModel>(result);
            MockAdminService.Verify_InviteSuperAdminUser();
        }

        [Test]
        public async Task Invite_POST_InvalidModel()
        {
            // arrange
            var model = new AdminInviteModel();

            // act
            var result = await SystemUnderTest.Invite(model);

            // assert
            AssertInputErrorMessagesOfView(result, model);
        }

        [Test]
        public void InviteDone_GET()
        {
            // arrange

            // act
            var result = SystemUnderTest.InviteDone();

            // assert
            AssertViewWithModel<AdminInviteDoneModel>(result);
        }

        [Test]
        public async Task ChangeActivation_POST()
        {
            // arrange
            MockAdminService.Setup_ChangeActivation_Returns_UserChangeActivationResponse_Success();

            // act
            var result = (JsonResult)await SystemUnderTest.ChangeActivation(UidOne);

            // assert
            ((CommonResult)result.Value).IsOk.ShouldBe(BooleanTrue);
            MockAdminService.Verify_ChangeActivation();
        }

        [Test]
        public async Task ChangeActivation_POST_FailedResponse()
        {
            // arrange
            MockAdminService.Setup_ChangeActivation_Returns_UserChangeActivationResponse_Failed();

            // act
            var result = (JsonResult)await SystemUnderTest.ChangeActivation(UidOne);

            // assert
            ((CommonResult)result.Value).IsOk.ShouldBe(BooleanFalse);
            MockAdminService.Verify_ChangeActivation();
        }

        [Test]
        public async Task ChangeActivation_POST_InvalidResponse()
        {
            // arrange
            MockAdminService.Setup_ChangeActivation_Returns_UserChangeActivationResponse_Invalid();
            // act
            var result = (JsonResult)await SystemUnderTest.ChangeActivation(UidOne);

            // assert
            ((CommonResult)result.Value).IsOk.ShouldBe(BooleanFalse);
            MockAdminService.Verify_ChangeActivation();
        }

        [Test]
        public async Task ChangeActivation_POST_InvalidParameter()
        {
            // arrange

            // act
            var result = await SystemUnderTest.ChangeActivation(EmptyUid);

            // assert
            AssertView<ForbidResult>(result);
        }

        [Test]
        public async Task OrganizationChangeActivation_POST()
        {
            // arrange
            MockAdminService.Setup_OrganizationChangeActivation_Returns_OrganizationChangeActivationResponse_Success();

            // act
            var result = (JsonResult)await SystemUnderTest.OrganizationChangeActivation(UidOne);

            // assert
            ((CommonResult)result.Value).IsOk.ShouldBe(BooleanTrue);
            MockAdminService.Verify_OrganizationChangeActivation();
        }

        [Test]
        public async Task OrganizationChangeActivation_POST_FailedResponse()
        {
            // arrange
            MockAdminService.Setup_OrganizationChangeActivation_Returns_OrganizationChangeActivationResponse_Failed();
            // act
            var result = (JsonResult)await SystemUnderTest.OrganizationChangeActivation(UidOne);

            // assert
            ((CommonResult)result.Value).IsOk.ShouldBe(BooleanFalse);
            MockAdminService.Verify_OrganizationChangeActivation();
        }

        [Test]
        public async Task OrganizationChangeActivation_POST_InvalidResponse()
        {
            // arrange
            MockAdminService.Setup_OrganizationChangeActivation_Returns_OrganizationChangeActivationResponse_Invalid();
            // act
            var result = (JsonResult)await SystemUnderTest.OrganizationChangeActivation(UidOne);

            // assert
            ((CommonResult)result.Value).IsOk.ShouldBe(BooleanFalse);
            MockAdminService.Verify_OrganizationChangeActivation();
        }

        [Test]
        public async Task OrganizationChangeActivation_POST_InvalidParameter()
        {
            // arrange

            // act
            var result = await SystemUnderTest.OrganizationChangeActivation(EmptyUid);

            // assert
            AssertView<ForbidResult>(result);
        }

        [Test]
        public async Task TranslationProviderChangeActivation_POST()
        {
            // arrange
            MockAdminService.Setup_TranslationProviderChangeActivation_Returns_TranslationProviderChangeActivationResponse_Success();

            // act
            var result = (JsonResult)await SystemUnderTest.TranslationProviderChangeActivation(UidOne);

            // assert
            ((CommonResult)result.Value).IsOk.ShouldBe(BooleanTrue);
            MockAdminService.Verify_TranslationProviderChangeActivation();
        }

        [Test]
        public async Task TranslationProviderChangeActivation_POST_FailedResponse()
        {
            // arrange
            MockAdminService.Setup_TranslationProviderChangeActivation_Returns_TranslationProviderChangeActivationResponse_Failed();
            // act
            var result = (JsonResult)await SystemUnderTest.TranslationProviderChangeActivation(UidOne);

            // assert
            ((CommonResult)result.Value).IsOk.ShouldBe(BooleanFalse);
            MockAdminService.Verify_TranslationProviderChangeActivation();
        }

        [Test]
        public async Task TranslationProviderChangeActivation_POST_InvalidResponse()
        {
            // arrange
            MockAdminService.Setup_TranslationProviderChangeActivation_Returns_TranslationProviderChangeActivationResponse_Invalid();
            // act
            var result = (JsonResult)await SystemUnderTest.TranslationProviderChangeActivation(UidOne);

            // assert
            ((CommonResult)result.Value).IsOk.ShouldBe(BooleanFalse);
            MockAdminService.Verify_TranslationProviderChangeActivation();
        }

        [Test]
        public async Task TranslationProviderChangeActivation_POST_InvalidParameter()
        {
            // arrange

            // act
            var result = await SystemUnderTest.TranslationProviderChangeActivation(EmptyUid);

            // assert
            AssertView<ForbidResult>(result);
        }

        [Test]
        public async Task DegradeToUser_POST()
        {
            // arrange
            MockAdminService.Setup_DemoteToUser_Returns_AdminDemoteResponse_Success();

            // act
            var result = (JsonResult)await SystemUnderTest.DegradeToUser(UidOne);

            // assert
            ((CommonResult)result.Value).IsOk.ShouldBe(BooleanTrue);
            MockAdminService.Verify_DemoteToUser();
        }

        [Test]
        public async Task DegradeToUser_POST_FailedResponse()
        {
            // arrange
            MockAdminService.Setup_DemoteToUser_Returns_AdminDemoteResponse_Failed();
            // act
            var result = (JsonResult)await SystemUnderTest.DegradeToUser(UidOne);

            // assert
            ((CommonResult)result.Value).IsOk.ShouldBe(BooleanFalse);
            MockAdminService.Verify_DemoteToUser();
        }

        [Test]
        public async Task DegradeToUser_POST_InvalidResponse()
        {
            // arrange
            MockAdminService.Setup_DemoteToUser_Returns_AdminDemoteResponse_Invalid();
            // act
            var result = (JsonResult)await SystemUnderTest.DegradeToUser(UidOne);

            // assert
            ((CommonResult)result.Value).IsOk.ShouldBe(BooleanFalse);
            MockAdminService.Verify_DemoteToUser();
        }

        [Test]
        public async Task DegradeToUser_POST_InvalidParameter()
        {
            // arrange

            // act
            var result = await SystemUnderTest.DegradeToUser(EmptyUid);

            // assert
            AssertView<JsonResult>(result);
        }

        [Test]
        public async Task UserUpgradeToAdmin_POST()
        {
            // arrange
            MockAdminService.Setup_UpgradeToAdmin_Returns_AdminUpgradeResponse_Success();

            // act
            var result = (JsonResult)await SystemUnderTest.UserUpgradeToAdmin(UidOne);

            // assert
            ((CommonResult)result.Value).IsOk.ShouldBe(BooleanTrue);
            MockAdminService.Verify_UpgradeToAdmin();
        }

        [Test]
        public async Task UserUpgradeToAdmin_POST_FailedResponse()
        {
            // arrange
            MockAdminService.Setup_UpgradeToAdmin_Returns_AdminUpgradeResponse_Failed();
            // act
            var result = (JsonResult)await SystemUnderTest.UserUpgradeToAdmin(UidOne);

            // assert
            ((CommonResult)result.Value).IsOk.ShouldBe(BooleanFalse);
            MockAdminService.Verify_UpgradeToAdmin();
        }

        [Test]
        public async Task UserUpgradeToAdmin_POST_InvalidResponse()
        {
            // arrange
            MockAdminService.Setup_UpgradeToAdmin_Returns_AdminUpgradeResponse_Invalid();
            // act
            var result = (JsonResult)await SystemUnderTest.UserUpgradeToAdmin(UidOne);

            // assert
            ((CommonResult)result.Value).IsOk.ShouldBe(BooleanFalse);
            MockAdminService.Verify_UpgradeToAdmin();
        }

        [Test]
        public async Task UserUpgradeToAdmin_POST_InvalidParameter()
        {
            // arrange

            // act
            var result = await SystemUnderTest.UserUpgradeToAdmin(EmptyUid);

            // assert
            AssertView<ForbidResult>(result);
        }

        [Test]
        public async Task AcceptInvite_GET()
        {
            // arrange
            MockAdminService.Setup_ValidateSuperAdminUserInvitation_Returns_AdminInviteValidateResponse_Success();

            // act
            var result = await SystemUnderTest.AcceptInvite(UidOne, EmailOne);

            // assert
            AssertView<JsonResult>(result);
            MockAdminService.Verify_ValidateSuperAdminUserInvitation();
        }

        [Test]
        public async Task AcceptInvite_GET_FailedResponse()
        {
            // arrange
            MockAdminService.Setup_ValidateSuperAdminUserInvitation_Returns_AdminInviteValidateResponse_Failed();

            // act
            var result = await SystemUnderTest.AcceptInvite(UidOne, EmailOne);

            // assert
            AssertView<RedirectResult>(result);
            MockAdminService.Verify_ValidateSuperAdminUserInvitation();
        }

        [Test]
        public async Task AcceptInvite_GET_InvalidResponse()
        {
            // arrange
            MockAdminService.Setup_ValidateSuperAdminUserInvitation_Returns_AdminInviteValidateResponse_Invalid();

            // act
            var result = await SystemUnderTest.AcceptInvite(UidOne, EmailOne);

            // assert
            AssertViewAccessDenied(result);
            MockAdminService.Verify_ValidateSuperAdminUserInvitation();
        }

        [Test]
        public async Task AcceptInvite_GET_InvalidParameter()
        {
            // arrange

            // act
            var result = await SystemUnderTest.AcceptInvite(EmptyUid, StringOne);

            // assert
            AssertViewAccessDenied(result);
        }

        [Test]
        public async Task AcceptInvite_POST()
        {
            // arrange
            MockAdminService.Setup_AcceptSuperAdminUserInvite_Returns_AdminAcceptInviteResponse_Success();
            var model = GetAdminAcceptInviteModel();

            // act
            var result = await SystemUnderTest.AcceptInvite(model);

            // assert
            ((RedirectResult)result).Url.ShouldBe("/Admin/AcceptInviteDone/");
            MockAdminService.Verify_AcceptSuperAdminUserInvite();
        }

        [Test]
        public async Task AcceptInvite_POST_FailedResponse()
        {
            // arrange
            MockAdminService.Setup_AcceptSuperAdminUserInvite_Returns_AdminAcceptInviteResponse_Failed();
            var model = GetAdminAcceptInviteModel();

            // act
            var result = await SystemUnderTest.AcceptInvite(model);

            // assert
            AssertErrorMessagesForInvalidOrFailedResponse<AdminAcceptInviteModel>(result);
            MockAdminService.Verify_AcceptSuperAdminUserInvite();
        }

        [Test]
        public async Task AcceptInvite_POST_InvalidResponse()
        {
            // arrange
            MockAdminService.Setup_AcceptSuperAdminUserInvite_Returns_AdminAcceptInviteResponse_Invalid();
            var model = GetAdminAcceptInviteModel();

            // act
            var result = await SystemUnderTest.AcceptInvite(model);

            // assert
            AssertErrorMessagesForInvalidOrFailedResponse<AdminAcceptInviteModel>(result);
            MockAdminService.Verify_AcceptSuperAdminUserInvite();
        }

        [Test]
        public async Task AcceptInvite_POST_InvalidModel()
        {
            // arrange
            var model = new AdminAcceptInviteModel();

            // act
            var result = await SystemUnderTest.AcceptInvite(model);

            // assert
            AssertInputErrorMessagesOfView(result, model);
        }

        [Test]
        public void AcceptInviteDone_GET()
        {
            // arrange

            // act
            var result = SystemUnderTest.AcceptInviteDone();

            // assert
            AssertViewWithModel<AdminAcceptInviteDoneModel>(result);
        }

    }
}