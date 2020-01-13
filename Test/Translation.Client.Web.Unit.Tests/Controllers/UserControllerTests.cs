using System;
using System.Threading.Tasks;

using Autofac;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using Shouldly;

using Translation.Client.Web.Controllers;
using Translation.Client.Web.Models.Base;
using Translation.Client.Web.Models.User;
using Translation.Client.Web.Unit.Tests.ServiceSetupHelpers;

using static Translation.Client.Web.Unit.Tests.TestHelpers.ActionMethodNameConstantTestHelper;
using static Translation.Client.Web.Unit.Tests.TestHelpers.AssertViewModelTestHelper;
using static Translation.Common.Tests.TestHelpers.FakeConstantTestHelper;
using static Translation.Client.Web.Unit.Tests.TestHelpers.FakeModelTestHelper;

namespace Translation.Client.Web.Unit.Tests.Controllers
{
    [TestFixture]
    public class UserControllerTests : ControllerBaseTests
    {
        public UserController SystemUnderTest { get; set; }

        [SetUp]
        public void run_before_every_test()
        {
            Refresh();
            SystemUnderTest = Container.Resolve<UserController>();
            SetControllerContext(SystemUnderTest);
        }

        [TestCase(SignUpAction, new Type[] { }, typeof(HttpGetAttribute)),
         TestCase(SignUpAction, new[] { typeof(SignUpModel) }, typeof(HttpPostAttribute)),
         TestCase(ValidateEmailDoneAction, new[] { typeof(string), typeof(Guid) }, typeof(HttpGetAttribute)),
         TestCase(LogOnAction, new Type[] { typeof(string) }, typeof(HttpGetAttribute)),
         TestCase(LogOnAction, new[] { typeof(LogOnModel) }, typeof(HttpPostAttribute)),
         TestCase(DemandPasswordResetAction, new Type[] { }, typeof(HttpGetAttribute)),
         TestCase(DemandPasswordResetDoneAction, new Type[] { }, typeof(HttpGetAttribute)),
         TestCase(ResetPasswordAction, new[] { typeof(string), typeof(Guid) }, typeof(HttpGetAttribute)),
         TestCase(ResetPasswordAction, new[] { typeof(ResetPasswordModel) }, typeof(HttpPostAttribute)),
         TestCase(ResetPasswordDoneAction, new Type[] { }, typeof(HttpGetAttribute)),
         TestCase(DetailAction, new[] { typeof(Guid) }, typeof(HttpGetAttribute)),
         TestCase(ChangePasswordAction, new Type[] { }, typeof(HttpGetAttribute)),
         TestCase(ChangePasswordAction, new[] { typeof(ChangePasswordModel) }, typeof(HttpPostAttribute)),
         TestCase(ChangePasswordDoneAction, new Type[] { }, typeof(HttpGetAttribute)),
         TestCase(EditAction, new[] { typeof(Guid) }, typeof(HttpGetAttribute)),
         TestCase(EditAction, new[] { typeof(UserEditModel) }, typeof(HttpPostAttribute)),
         TestCase(InviteAction, new[] { typeof(Guid) }, typeof(HttpGetAttribute)),
         TestCase(InviteAction, new[] { typeof(InviteModel) }, typeof(HttpPostAttribute)),
         TestCase(InviteDoneAction, new Type[] { }, typeof(HttpGetAttribute)),
         TestCase(AcceptInviteAction, new[] { typeof(string), typeof(Guid) }, typeof(HttpGetAttribute)),
         TestCase(AcceptInviteAction, new[] { typeof(InviteAcceptModel) }, typeof(HttpPostAttribute)),
         TestCase(AcceptInviteDoneAction, new Type[] { }, typeof(HttpGetAttribute)),
         TestCase(ChangeActivationAction, new[] { typeof(Guid) }, typeof(HttpPostAttribute)),
         TestCase(LogOffAction, new Type[] { }, typeof(HttpPostAttribute)),
         TestCase(JournalListAction, new[] { typeof(Guid) }, typeof(HttpGetAttribute)),
         TestCase(JournalListDataAction, new[] { typeof(Guid), typeof(int), typeof(int) }, typeof(HttpGetAttribute)),
         TestCase(RevisionsAction, new[] { typeof(Guid) }, typeof(HttpGetAttribute)),
         TestCase(RevisionsDataAction, new[] { typeof(Guid) }, typeof(HttpGetAttribute)),
         TestCase(RestoreAction, new[] { typeof(Guid), typeof(int) }, typeof(HttpPostAttribute))]
        public void Methods_Has_Http_Verb_Attributes(string actionMethod, Type[] parameters, Type httpVerbAttribute)
        {
            var type = SystemUnderTest.GetType();
            var methodInfo = type.GetMethod(actionMethod, parameters);
            var attributes = methodInfo.GetCustomAttributes(httpVerbAttribute, true);
            Assert.AreEqual(attributes.Length, 1);
        }

        [TestCase(DemandPasswordResetDoneAction)]
        [TestCase(ChangePasswordDoneAction)]
        [TestCase(InviteDoneAction)]
        [TestCase(AcceptInviteDoneAction)]
        public void Methods_Returning_ViewResult_Has_Model_With_Title(string methodName)
        {
            var methodInfo = SystemUnderTest.GetType().GetMethod(methodName);
            var result = (ViewResult)methodInfo.Invoke(SystemUnderTest, null);

            result.ShouldSatisfyAllConditions(() => result.ShouldNotBeNull(),
                () => result.ViewName.ShouldBeNullOrEmpty(),
                () => result.Model.ShouldNotBeNull());
        }

        [Test]
        public void Controller_Derived_From_BaseController()
        {
            var type = SystemUnderTest.GetType();
            type.BaseType.Name.StartsWith("BaseController").ShouldBeTrue();
        }

        [Ignore("User.Identity.IsAuthenticated sounds true")]
        [Test]
        public void SignUp_GET()
        {
            // act
            var result = (ViewResult)SystemUnderTest.SignUp();

            // assert
            AssertView<SignUpModel>(result);
        }

        [Test]
        public async Task SignUp_POST()
        {
            // arrange
            MockOrganizationService.Setup_CreateOrganizationWithAdmin_Returns_SignUpResponse_Success();
            var model = GetOrganizationOneUserOneSignUpModel();

            // act
            var result = await SystemUnderTest.SignUp(model);

            // assert
            AssertViewRedirectToHome(result);
            MockOrganizationService.Verify_CreateOrganizationWithAdmin();
        }

        [Test]
        public async Task SignUp_POST_InvalidModel()
        {
            // arrange
            var model = new SignUpModel();

            // act
            var result = await SystemUnderTest.SignUp(model);

            // assert
            AssertInputErrorMessagesOfView(result, model);
        }

        [Test]
        public async Task SignUp_POST_InvalidResponse()
        {
            // arrange
            MockOrganizationService.Setup_CreateOrganizationWithAdmin_Returns_SignUpResponse_Failed();
            var model = GetOrganizationOneUserOneSignUpModel();

            // act
            var result = await SystemUnderTest.SignUp(model);

            // assert
            AssertErrorMessagesForInvalidOrFailedResponse<SignUpModel>(result);
            MockOrganizationService.Verify_CreateOrganizationWithAdmin();
        }

        [Test]
        public async Task ValidateEmailDone_GET()
        {
            // arrange
            MockOrganizationService.Setup_ValidateEmail_Returns_ValidateEmailResponse_Success();

            // act
            var result = await SystemUnderTest.ValidateEmailDone(EmailOne, UidOne);

            // assert
            AssertViewWithModel<ValidateEmailDoneModel>(result);
            MockOrganizationService.Verify();
        }

        [Test]
        public async Task ValidateEmailDone_GET_InvalidModel()
        {
            // act
            var result = await SystemUnderTest.ValidateEmailDone(StringOne, EmptyUid);

            // assert
            AssertViewAccessDenied(result);
        }

        [Test]
        public async Task ValidateEmailDone_GET_InvalidResponse()
        {
            // arrange
            MockOrganizationService.Setup_ValidateEmail_Returns_ValidateEmailResponse_Failed();

            // act
            var result = await SystemUnderTest.ValidateEmailDone(EmailOne, UidOne);

            // assert
            AssertViewAccessDenied(result);
            MockOrganizationService.Verify();
        }

        [Test]
        public void LogOn_GET()
        {
            // act
            var result = SystemUnderTest.LogOn();

            // assert
            AssertViewWithModel<LogOnModel>(result);
        }

        [Test]
        public void LogOn_GET_RedirectUrl()
        {
            // act
            var result = SystemUnderTest.LogOn(StringOne);

            // assert
            AssertViewWithModel<LogOnModel>(result);
            ((LogOnModel)result.Model).RedirectUrl.ShouldBe(StringOne);
        }

        [Test]
        public async Task LogOn_POST()
        {
            // arrange
            MockOrganizationService.Setup_LogOn_Returns_LogOnResponse_Success();
            var model = GetOrganizationOneUserOneLogOnModel();

            // act
            var result = await SystemUnderTest.LogOn(model);

            // assert
            AssertView<RedirectResult>(result);
            MockOrganizationService.Verify();
        }

        [Test]
        public async Task LogOn_POST_RedirectUrl()
        {
            // arrange
            MockOrganizationService.Setup_LogOn_Returns_LogOnResponse_Success();
            var model = GetOrganizationOneUserOneLogOnModel();
            model.RedirectUrl = StringOne;

            // act
            var result = await SystemUnderTest.LogOn(model);

            // assert
            AssertView<RedirectResult>(result);
            ((RedirectResult)result).Url.ShouldBe(StringOne);
            MockOrganizationService.Verify();
        }

        [Test]
        public async Task LogOn_POST_RedirectToAdminDashboard()
        {
            // arrange
            MockOrganizationService.Setup_LogOn_Returns_LogOnResponse_Success_SuperAdmin();
            var model = GetOrganizationOneUserOneLogOnModel();

            // act
            var result = await SystemUnderTest.LogOn(model);

            // assert
            result.ShouldNotBeNull();
            ((RedirectResult)result).Url.ShouldBe("/Admin/Dashboard");
            MockOrganizationService.Verify();
        }

        [Test]
        public async Task LogOn_POST_InvalidModel()
        {
            // arrange
            var model = new LogOnModel();

            // act
            var result = await SystemUnderTest.LogOn(model);

            // assert
            AssertInputErrorMessagesOfView(result, model);
        }

        [Test]
        public async Task LogOn_POST_InvalidResponse()
        {
            // arrange
            MockOrganizationService.Setup_LogOn_Returns_LogOnResponse_Invalid();
            var model = GetOrganizationOneUserOneLogOnModel();

            // act
            var result = await SystemUnderTest.LogOn(model);

            // assert
            AssertErrorMessagesForInvalidOrFailedResponse<LogOnModel>(result);
            MockOrganizationService.Verify();
        }

        [Test]
        public async Task LogOn_POST_FailedResponse()
        {
            // arrange
            MockOrganizationService.Setup_LogOn_Returns_LogOnResponse_Failed();
            var model = GetOrganizationOneUserOneLogOnModel();

            // act
            var result = await SystemUnderTest.LogOn(model);

            // assert
            AssertErrorMessagesForInvalidOrFailedResponse<LogOnModel>(result);
            MockOrganizationService.Verify();
        }

        [Test]
        public void DemandPasswordReset_GET()
        {
            // act
            var result = SystemUnderTest.DemandPasswordReset();

            // assert
            AssertViewWithModel<DemandPasswordResetModel>(result);
        }

        [Test]
        public async Task DemandPasswordReset_POST()
        {
            // arrange
            MockOrganizationService.Setup_DemandPasswordReset_Returns_DemandPasswordResetResponse_Success();
            var model = GetOrganizationOneUserOneDemandPasswordResetModel();

            // act
            var result = await SystemUnderTest.DemandPasswordReset(model);

            // assert
            result.ShouldNotBeNull();
            ((RedirectResult)result).Url.ShouldBe("/User/DemandPasswordResetDone");
            MockOrganizationService.Verify_DemandPasswordReset();
        }

        [Test]
        public async Task DemandPasswordReset_POST_InvalidModel()
        {
            // arrange
            var model = new DemandPasswordResetModel();

            // act
            var result = await SystemUnderTest.DemandPasswordReset(model);

            // assert
            AssertInputErrorMessagesOfView(result, model);
        }

        [Test]
        public async Task DemandPasswordReset_POST_InvalidResponse()
        {
            // arrange
            MockOrganizationService.Setup_DemandPasswordReset_Returns_DemandPasswordResetResponse_Invalid();
            var model = GetOrganizationOneUserOneDemandPasswordResetModel();

            // act
            var result = await SystemUnderTest.DemandPasswordReset(model);

            // assert
            AssertViewAccessDenied(result);
            MockOrganizationService.Verify_DemandPasswordReset();
        }

        [Test]
        public async Task DemandPasswordReset_POST_FailedResponse()
        {
            // arrange
            MockOrganizationService.Setup_DemandPasswordReset_Returns_DemandPasswordResetResponse_Failed();
            var model = GetOrganizationOneUserOneDemandPasswordResetModel();

            // act
            var result = await SystemUnderTest.DemandPasswordReset(model);

            // assert
            AssertViewAccessDenied(result);
            MockOrganizationService.Verify_DemandPasswordReset();
        }

        [Test]
        public void DemandPasswordResetDone_GET()
        {
            // act
            var result = SystemUnderTest.DemandPasswordResetDone();

            // assert
            AssertViewWithModel<DemandPasswordResetDoneModel>(result);
        }

        [Test]
        public async Task ResetPassword_GET()
        {
            // arrange
            MockOrganizationService.Setup_ValidatePasswordReset_Returns_PasswordResetValidateResponse_Success();

            // act
            var result = await SystemUnderTest.ResetPassword(EmailOne, UidOne);

            // assert
            AssertView<ResetPasswordModel>(result);
            MockOrganizationService.Verify_ValidatePasswordReset();
        }

        [Test]
        public async Task ResetPassword_GET_InvalidParameter()
        {
            // act
            var result = await SystemUnderTest.ResetPassword(StringOne, EmptyUid);

            // assert
            AssertViewAccessDenied(result);
        }

        [Test]
        public async Task ResetPassword_GET_FailedResponse()
        {
            // arrange
            MockOrganizationService.Setup_ValidatePasswordReset_Returns_PasswordResetValidateResponse_Failed();

            // act
            var result = await SystemUnderTest.ResetPassword(EmailOne, UidOne);

            // assert
            AssertViewAccessDenied(result);
            MockOrganizationService.Verify_ValidatePasswordReset();
        }

        [Test]
        public async Task ResetPassword_POST()
        {
            // arrange
            MockOrganizationService.Setup_PasswordReset_Returns_PasswordResetResponse_Success();
            var model = GetOrganizationOneUserOneResetPasswordModel();

            // act
            var result = await SystemUnderTest.ResetPassword(model);

            // assert
            result.ShouldNotBeNull();
            ((RedirectResult)result).Url.ShouldBe("/User/ResetPasswordDone");
            MockOrganizationService.Verify_PasswordReset();
        }

        [Test]
        public async Task ResetPassword_POST_InvalidResponse()
        {
            // arrange
            MockOrganizationService.Setup_PasswordReset_Returns_PasswordResetResponse_Invalid();
            var model = GetOrganizationOneUserOneResetPasswordModel();

            // act
            var result = await SystemUnderTest.ResetPassword(model);

            // assert
            AssertViewAccessDenied(result);
            MockOrganizationService.Verify_PasswordReset();
        }

        [Test]
        public async Task ResetPassword_POST_FailedResponse()
        {
            // arrange
            MockOrganizationService.Setup_PasswordReset_Returns_PasswordResetResponse_Failed();
            var model = GetOrganizationOneUserOneResetPasswordModel();

            // act
            var result = await SystemUnderTest.ResetPassword(model);

            // assert
            AssertViewAccessDenied(result);
            MockOrganizationService.Verify_PasswordReset();
        }

        [Test]
        public async Task ResetPassword_POST_InvalidModel()
        {
            // arrange
            var model = new ResetPasswordModel();

            // act
            var result = await SystemUnderTest.ResetPassword(model);

            // assert
            AssertInputErrorMessagesOfView(result, model);
        }

        [Test]
        public void ResetPasswordDone_GET()
        {
            // act
            var result = SystemUnderTest.ResetPasswordDone();

            // assert
            AssertViewWithModel<ResetPasswordDoneModel>(result);
        }

        [Test]
        public void Detail_GET()
        {
            // arrange
            MockOrganizationService.Setup_GetUser_Returns_UserReadResponse_Success();

            // act
            var result = SystemUnderTest.Detail(UidOne);

            // assert
            AssertView<UserDetailModel>(result);
            MockOrganizationService.Verify_GetUser();
        }

        [Test]
        public void Detail_GET_InvalidParameter()
        {
            // act
            var result = SystemUnderTest.Detail(EmptyUid);

            // assert
            AssertViewAccessDenied(result);
        }

        [Test]
        public void Detail_GET_FailedResponse()
        {
            // arrange
            MockOrganizationService.Setup_GetUser_Returns_UserReadResponse_Invalid();

            // act
            var result = SystemUnderTest.Detail(UidOne);

            // assert
            AssertViewAccessDenied(result);
            MockOrganizationService.Verify_GetUser();
        }

        [Test]
        public void ChangePassword_GET()
        {
            // act
            var result = SystemUnderTest.ChangePassword();

            // assert
            AssertViewWithModel<ChangePasswordModel>(result);
            ((ChangePasswordModel)result.Model).UserUid.ShouldBe(SystemUnderTest.CurrentUser.Uid);
        }

        [Test]
        public async Task ChangePassword_POST()
        {
            // arrange
            MockOrganizationService.Setup_ChangePassword_Returns_PasswordChangeResponse_Success();
            var model = GetOrganizationOneUserOneChangePasswordModel();

            // act
            var result = await SystemUnderTest.ChangePassword(model);

            // assert
            result.ShouldNotBeNull();
            ((RedirectResult)result).Url.ShouldBe("/User/ChangePasswordDone");
            MockOrganizationService.Verify_ChangePassword();
        }

        [Test]
        public async Task ChangePassword_POST_InvalidModel()
        {
            // arrange
            var model = new ChangePasswordModel();

            // act
            var result = await SystemUnderTest.ChangePassword(model);

            // assert
            AssertInputErrorMessagesOfView(result, model);
        }

        [Test]
        public async Task ChangePassword_POST_InvalidResponse()
        {
            // arrange
            MockOrganizationService.Setup_ChangePassword_Returns_PasswordChangeResponse_Invalid();
            var model = GetOrganizationOneUserOneChangePasswordModel();

            // act
            var result = await SystemUnderTest.ChangePassword(model);

            // assert
            AssertView<ChangePasswordModel>(result);
            AssertInputErrorMessagesOfView(result, model);
            MockOrganizationService.Verify_ChangePassword();
        }

        [Test]
        public async Task ChangePassword_POST_FailedResponse()
        {
            // arrange
            MockOrganizationService.Setup_ChangePassword_Returns_PasswordChangeResponse_Invalid();
            var model = GetOrganizationOneUserOneChangePasswordModel();

            // act
            var result = await SystemUnderTest.ChangePassword(model);

            // assert
            AssertView<ChangePasswordModel>(result);
            AssertInputErrorMessagesOfView(result, model);
            MockOrganizationService.Verify_ChangePassword();
        }

        [Test]
        public void ChangePasswordDone_GET()
        {
            // act
            var result = SystemUnderTest.ChangePasswordDone();

            // assert
            AssertView<ChangePasswordDoneModel>(result);
        }

        [Test]
        public void Edit_GET()
        {
            // arrange 
            MockOrganizationService.Setup_GetUser_Returns_UserReadResponse_Success();

            // act
            var result = SystemUnderTest.Edit(OrganizationOneUserOneUid);

            // assert
            AssertViewWithModel<UserEditModel>(result);
            MockOrganizationService.Verify_GetUser();
        }

        [Test]
        public void Edit_GET_IdEmpty()
        {
            // arrange 
            MockOrganizationService.Setup_GetUser_Returns_UserReadResponse_Success();

            // act
            var result = SystemUnderTest.Edit(EmptyUid);

            // assert
            AssertViewWithModel<UserEditModel>(result);
            MockOrganizationService.Verify_GetUser();
        }

        [Test]
        public void Edit_GET_FailedResponse()
        {
            // arrange 
            MockOrganizationService.Setup_GetUser_Returns_UserReadResponse_Failed();

            // act
            var result = SystemUnderTest.Edit(OrganizationOneUserOneUid);

            // assert
            AssertViewAccessDenied(result);
            MockOrganizationService.Verify_GetUser();
        }

        [Test]
        public void Edit_GET_InvalidResponse()
        {
            // arrange 
            MockOrganizationService.Setup_GetUser_Returns_UserReadResponse_Invalid();

            // act
            var result = SystemUnderTest.Edit(OrganizationOneUserOneUid);

            // assert
            AssertViewAccessDenied(result);
            MockOrganizationService.Verify_GetUser();
        }

        [Test]
        public async Task Edit_POST()
        {
            // arrange 
            MockOrganizationService.Setup_EditUser_Returns_UserEditResponse_Success();
            var model = GetOrganizationOneUserOneUserEditModel();

            // act
            var result = await SystemUnderTest.Edit(model);

            // assert
            ((RedirectResult)result).Url.ShouldBe($"/User/Detail/{OrganizationOneUserOneUid}");
            MockOrganizationService.Verify_EditUser();
        }

        [Test]
        public async Task Edit_POST_FailedResponse()
        {
            // arrange 
            MockOrganizationService.Setup_EditUser_Returns_UserEditResponse_Failed();
            var model = GetOrganizationOneUserOneUserEditModel();

            // act
            var result = await SystemUnderTest.Edit(model);

            // assert
            AssertErrorMessagesForInvalidOrFailedResponse<UserEditModel>(result);
            MockOrganizationService.Verify_EditUser();
        }

        [Test]
        public async Task Edit_POST_InvalidResponse()
        {
            // arrange 
            MockOrganizationService.Setup_EditUser_Returns_UserEditResponse_Invalid();
            var model = GetOrganizationOneUserOneUserEditModel();

            // act
            var result = await SystemUnderTest.Edit(model);

            // assert
            AssertErrorMessagesForInvalidOrFailedResponse<UserEditModel>(result);
            MockOrganizationService.Verify_EditUser();
        }

        [Test]
        public async Task Edit_POST_InvalidModel()
        {
            // arrange
            var model = new UserEditModel();

            // act
            var result = await SystemUnderTest.Edit(model);

            // assert
            AssertInputErrorMessagesOfView(result, model);
        }

        [Test]
        public void Invite_GET()
        {
            // act
            var result = (ViewResult)SystemUnderTest.Invite(Guid.Empty);

            // assert
            AssertView<InviteModel>(result);
            ((InviteModel)result.Model).OrganizationUid.ShouldBe(SystemUnderTest.CurrentUser.Organization.Uid);
        }

        [Test]
        public void Invite_GET_WithOrganizationUid()
        {
            // arrange
            var organizationUid = UidOne;

            // act
            var result = (ViewResult)SystemUnderTest.Invite(organizationUid);

            // assert
            AssertView<InviteModel>(result);
            ((InviteModel)result.Model).OrganizationUid.ShouldBe(organizationUid);
        }

        [Test]
        public async Task Invite_POST()
        {
            // arrange
            MockOrganizationService.Setup_InviteUser_Returns_UserInviteResponse_Success();
            var model = GetOrganizationOneUserOneInviteModel();

            // act
            var result = await SystemUnderTest.Invite(model);

            // assert
            result.ShouldNotBeNull();
            ((RedirectResult)result).Url.ShouldBe("/User/InviteDone");
            MockOrganizationService.Verify_InviteUser();
        }

        [Test]
        public async Task Invite_POST_InvalidModel()
        {
            // arrange
            var model = new InviteModel();

            // act
            var result = await SystemUnderTest.Invite(model);

            // assert
            AssertInputErrorMessagesOfView(result, model);
        }

        [Test]
        public async Task Invite_POST_InvalidResponse()
        {
            // arrange
            MockOrganizationService.Setup_InviteUser_Returns_UserInviteResponse_Invalid();
            var model = GetOrganizationOneUserOneInviteModel();

            // act
            var result = await SystemUnderTest.Invite(model);

            // assert
            AssertView<InviteModel>(result);
            MockOrganizationService.Verify_InviteUser();
        }

        [Test]
        public async Task Invite_POST_FailedResponse()
        {
            // arrange
            MockOrganizationService.Setup_InviteUser_Returns_UserInviteResponse_Failed();
            var model = GetOrganizationOneUserOneInviteModel();

            // act
            var result = await SystemUnderTest.Invite(model);

            // assert
            AssertView<InviteModel>(result);
            MockOrganizationService.Verify_InviteUser();
        }

        [Test]
        public void InviteDone_GET()
        {
            // act
            var result = SystemUnderTest.InviteDone();

            // assert
            AssertViewWithModel<InviteDoneModel>(result);
        }

        [Test]
        public async Task AcceptInvite_GET()
        {
            // arrange
            MockOrganizationService.Setup_ValidateUserInvitation_Returns_UserInviteValidateResponse_Success();

            // act
            var result = await SystemUnderTest.AcceptInvite(EmailOne, UidOne);

            // assert
            AssertView<InviteAcceptModel>(result);
            MockOrganizationService.Verify_ValidateUserInvitation();
        }

        [Test]
        public async Task AcceptInvite_GET_InvalidParameter()
        {
            // act
            var result = await SystemUnderTest.AcceptInvite(StringOne, EmptyUid);

            // assert
            AssertViewAccessDenied(result);
        }

        [Test]
        public async Task AcceptInvite_GET_FailedResponse()
        {
            // arrange
            MockOrganizationService.Setup_ValidateUserInvitation_Returns_UserInviteValidateResponse_Failed();

            // act
            var result = await SystemUnderTest.AcceptInvite(EmailOne, UidOne);

            // assert
            AssertViewAccessDenied(result);
            MockOrganizationService.Verify_ValidateUserInvitation();
        }

        [Test]
        public async Task AcceptInvite_POST()
        {
            // arrange
            MockOrganizationService.Setup_AcceptInvitation_Returns_UserAcceptInviteResponse_Success();
            var model = GetOrganizationOneUserOneInviteAcceptModel();

            // act
            var result = await SystemUnderTest.AcceptInvite(model);

            // assert
            result.ShouldNotBeNull();
            ((RedirectResult)result).Url.ShouldBe("/User/AcceptInviteDone");
            MockOrganizationService.Verify_AcceptInvitation();
        }

        [Test]
        public async Task AcceptInvite_POST_InvalidModel()
        {
            // arrange
            var model = new InviteAcceptModel();

            // act
            var result = await SystemUnderTest.AcceptInvite(model);

            // assert
            AssertInputErrorMessagesOfView(result, model);
        }

        [Test]
        public async Task AcceptInvite_POST_FailedResponse()
        {
            // arrange
            MockOrganizationService.Setup_AcceptInvitation_Returns_UserAcceptInviteResponse_Failed();
            var model = GetOrganizationOneUserOneInviteAcceptModel();

            // act
            var result = await SystemUnderTest.AcceptInvite(model);

            // assert
            AssertView<InviteAcceptModel>(result);
            MockOrganizationService.Verify_AcceptInvitation();
        }

        [Test]
        public void AcceptInviteDone_GET()
        {
            // act
            var result = SystemUnderTest.AcceptInviteDone();

            // assert
            AssertView<InviteAcceptDoneModel>(result);
        }

        [Test]
        public async Task ChangeActivation_POST()
        {
            // arrange
            MockOrganizationService
                .Setup_ChangeActivationForUser_Returns_UserChangeActivationResponse_Success();

            // act
            var result = await SystemUnderTest.ChangeActivation(UidOne);

            // assert
            AssertView<JsonResult>(result);
            MockOrganizationService.Verify_ChangeActivationForUser();
        }

        [Test]
        public async Task ChangeActivation_POST_InvalidParameter()
        {
            // act
            var result = (JsonResult)await SystemUnderTest.ChangeActivation(EmptyUid);

            // assert
            ((CommonResult)result.Value).IsOk.ShouldBe(false);
        }

        [Test]
        public async Task ChangeActivation_POST_FailedResponse()
        {
            // arrange
            MockOrganizationService
                .Setup_ChangeActivationForUser_Returns_UserChangeActivationResponse_Failed();

            // act
            var result = (JsonResult)await SystemUnderTest.ChangeActivation(UidOne);

            // assert
            ((CommonResult)result.Value).IsOk.ShouldBe(false);
            MockOrganizationService.Verify_ChangeActivationForUser();
        }

        [Test]
        public async Task ChangeActivation_POST_InvalidResponse()
        {
            // arrange
            MockOrganizationService
                .Setup_ChangeActivationForUser_Returns_UserChangeActivationResponse_Invalid();

            // act
            var result = (JsonResult)await SystemUnderTest.ChangeActivation(UidOne);

            // assert
            ((CommonResult)result.Value).IsOk.ShouldBe(false);
            MockOrganizationService.Verify_ChangeActivationForUser();
        }

        [Test]
        public async Task LogOff_POST()
        {
            // act
            var result = await SystemUnderTest.LogOff();

            // assert
            AssertViewRedirectToHome(result);
        }

        [Test]
        public void JournalList_GET()
        {
            // arrange

            // act
            var result = SystemUnderTest.JournalList(OrganizationOneProjectOneUid);

            // assert
            AssertViewWithModel<UserJournalListModel>(result);
        }

        [Test]
        public void JournalList_GET_IdEmpty()
        {
            // arrange

            // act
            var result = SystemUnderTest.JournalList(EmptyUid);

            // assert
            AssertViewWithModel<UserJournalListModel>(result);
        }

        [Test]
        public void JournalListData_GET()
        {
            // arrange
            MockJournalService.Setup_GetJournalsOfUser_Returns_UserJournalReadListResponse_Success();

            // act
            var result = SystemUnderTest.JournalListData(OrganizationOneProjectOneUid, One, Two);

            // assert
            AssertViewAndHeaders(result, new[] { "message", "created_at" });
            MockJournalService.Verify_GetJournalsOfUser();
        }

        [Test]
        public void JournalListData_GET_IdEmpty()
        {
            // arrange
            MockJournalService.Setup_GetJournalsOfUser_Returns_UserJournalReadListResponse_Success();

            // act
            var result = SystemUnderTest.JournalListData(EmptyUid, One, Two);

            // assert
            AssertViewAndHeaders(result, new[] { "message", "created_at" });
            MockJournalService.Verify_GetJournalsOfUser();
        }

        [Test]
        public void JournalListData_GET_FailedResponse()
        {
            // arrange
            MockJournalService.Setup_GetJournalsOfUser_Returns_UserJournalReadListResponse_Failed();

            // act
            var result = SystemUnderTest.JournalListData(OrganizationOneProjectOneUid, One, Two);

            // assert
            AssertView<NotFoundResult>(result);
            MockJournalService.Verify_GetJournalsOfUser();
        }

        [Test]
        public void JournalListData_GET_InvalidResponse()
        {
            // arrange
            MockJournalService.Setup_GetJournalsOfUser_Returns_UserJournalReadListResponse_Invalid();

            // act
            var result = SystemUnderTest.JournalListData(OrganizationOneProjectOneUid, One, Two);

            // assert
            AssertView<NotFoundResult>(result);
            MockJournalService.Verify_GetJournalsOfUser();
        }

        [Test]
        public void JournalListData_GET_InvalidParameter()
        {
            // arrange

            // act
            var result = SystemUnderTest.JournalListData(OrganizationOneProjectOneUid, One, Two);

            // assert
            AssertView<ForbidResult>(result);
        }

        [TestCase(10, 10)]
        [TestCase(10, 1000)]
        [TestCase(-10, 10)]
        [TestCase(10, -10)]
        [TestCase(1000, 10)]
        public async Task JournalListData_GET_SetPaging(int skip, int take)
        {
            // arrange
            MockJournalService.Setup_GetJournalsOfUser_Returns_UserJournalReadListResponse_Success();

            // act
            var result = (JsonResult)await SystemUnderTest.JournalListData(OrganizationOneProjectOneUid, skip, take);

            // assert
            AssertView<DataResult>(result);
            AssertPagingInfo(result);
        }

        [Test]
        public void Revisions_GET()
        {
            // arrange
            MockOrganizationService.Setup_GetUser_Returns_UserReadResponse_Success();

            // act
            var result = SystemUnderTest.Revisions(OrganizationOneUserOneUid);

            // assert
            AssertViewWithModel<UserRevisionReadListModel>(result);
            MockOrganizationService.Verify_GetUser();
        }

        [Test]
        public void Revisions_GET_FailedResponse()
        {
            // arrange
            MockOrganizationService.Setup_GetUser_Returns_UserReadResponse_Failed();

            // act
            var result = SystemUnderTest.Revisions(OrganizationOneUserOneUid);

            // assert
            AssertView<NotFoundResult>(result);
            MockOrganizationService.Verify_GetUser();
        }

        [Test]
        public void Revisions_GET_InvalidResponse()
        {
            // arrange
            MockOrganizationService.Setup_GetUser_Returns_UserReadResponse_Invalid();

            // act
            var result = SystemUnderTest.Revisions(OrganizationOneUserOneUid);

            // assert
            AssertView<NotFoundResult>(result);
            MockOrganizationService.Verify_GetUser();
        }

        [Test]
        public void Revisions_GET_InvalidParameter()
        {
            // arrange

            // act
            var result = SystemUnderTest.Revisions(EmptyUid);

            // assert
            AssertViewRedirectToHome(result);
        }

        [Test]
        public void RevisionsData_GET()
        {
            // arrange
            MockOrganizationService.Setup_GetUserRevisions_Returns_UserRevisionReadListResponse_Success();

            // act
            var result = SystemUnderTest.RevisionsData(OrganizationOneUserOneUid);

            // assert
            AssertViewAndHeaders(result, new[] { "revision", "revisioned_by", "revisioned_at", "user_name", "email", "is_active", "created_at", "" });
            MockOrganizationService.Verify_GetUserRevisions();
        }

        [Test]
        public void RevisionsData_GET_FailedResponse()
        {
            // arrange
            MockOrganizationService.Setup_GetUserRevisions_Returns_UserRevisionReadListResponse_Failed();

            // act
            var result = SystemUnderTest.RevisionsData(OrganizationOneUserOneUid);

            // assert
            AssertView<NotFoundResult>(result);
            MockOrganizationService.Verify_GetUserRevisions();
        }

        [Test]
        public void RevisionsData_GET_InvalidResponse()
        {
            // arrange
            MockOrganizationService.Setup_GetUserRevisions_Returns_UserRevisionReadListResponse_Invalid();

            // act
            var result = SystemUnderTest.RevisionsData(OrganizationOneUserOneUid);

            // assert
            AssertView<NotFoundResult>(result);
            MockOrganizationService.Verify_GetUserRevisions();
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
            MockOrganizationService.Setup_RestoreUser_Returns_UserRestoreResponse_Success();

            // act
            var result = await SystemUnderTest.Restore(OrganizationOneUserOneUid, One);

            // assert
            AssertView<JsonResult>(result);
            MockOrganizationService.Verify_RestoreUser();
        }

        [Test]
        public async Task Restore_Post_RevisionZero()
        {
            // arrange

            // act
            var result = await SystemUnderTest.Restore(OrganizationOneUserOneUid, Zero);

            // assert
            AssertView<JsonResult>(result);
        }

        [Test]
        public async Task Restore_Post_FailedResponse()
        {
            // arrange
            MockOrganizationService.Setup_RestoreUser_Returns_UserRestoreResponse_Failed();

            // act
            var result = (JsonResult)await SystemUnderTest.Restore(OrganizationOneUserOneUid, One);

            // assert
            ((CommonResult)result.Value).IsOk.ShouldBe(false);
            MockOrganizationService.Verify_RestoreUser();
        }

        [Test]
        public async Task Restore_Post_InvalidResponse()
        {
            // arrange
            MockOrganizationService.Setup_RestoreUser_Returns_UserRestoreResponse_Invalid();

            // act
            var result = (JsonResult)await SystemUnderTest.Restore(OrganizationOneUserOneUid, One);

            // assert
            ((CommonResult)result.Value).IsOk.ShouldBe(false);
            MockOrganizationService.Verify_RestoreUser();
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