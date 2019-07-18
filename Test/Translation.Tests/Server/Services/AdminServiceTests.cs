using System.Threading.Tasks;

using NUnit.Framework;
using Shouldly;

using Translation.Common.Contracts;
using Translation.Common.Enumerations;
using Translation.Common.Models.Responses.Admin;
using Translation.Common.Models.Responses.Journal;
using Translation.Common.Models.Responses.Organization;
using Translation.Common.Models.Responses.User;
using Translation.Tests.SetupHelpers;
using static Translation.Tests.TestHelpers.FakeRequestTestHelper;
using static Translation.Tests.TestHelpers.FakeConstantTestHelper;
using static Translation.Tests.TestHelpers.AssertViewModelTestHelper;
using static Translation.Tests.TestHelpers.AssertResponseTestHelper;

namespace Translation.Tests.Server.Services
{
    [TestFixture]
    public class AdminServiceTests : ServiceBaseTests
    {
        public IAdminService SystemUnderTest { get; set; }

        [SetUp]
        public void run_before_every_test()
        {
            SystemUnderTest = Container.Resolve<IAdminService>();
        }

        [Test]
        public async Task AdminService_GetOrganizations_Success_SelectAfter()
        {
            // arrange
            var request = GetOrganizationReadListRequestForSelectAfter();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneSuperAdminUserOne();
            MockOrganizationRepository.Setup_SelectAfter_Returns_Organizations();
            MockOrganizationRepository.Setup_Count_Returns_Ten();

            // act
            var result = await SystemUnderTest.GetOrganizations(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Success);
            AssertReturnType<OrganizationReadListResponse>(result);
            AssertPagingInfoForSelectAfter(request.PagingInfo, Ten);
            MockUserRepository.Verify_SelectById();
            MockOrganizationRepository.Verify_SelectAfter();
            MockOrganizationRepository.Verify_Count();
        }

        [Test]
        public async Task AdminService_GetOrganizations_Success_SelectMany()
        {
            // arrange
            var request = GetOrganizationReadListRequestForSelectMany();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneSuperAdminUserOne();
            MockOrganizationRepository.Setup_SelectMany_Returns_Organizations();
            MockOrganizationRepository.Setup_Count_Returns_Ten();

            // act
            var result = await SystemUnderTest.GetOrganizations(request);

            // assert
            result.Status.ShouldBe(ResponseStatus.Success);
            result.ErrorMessages.ShouldNotBeNull();
            result.ErrorMessages.Count.ShouldBe(0);
            AssertReturnType<OrganizationReadListResponse>(result);
            AssertPagingInfoForSelectMany(request.PagingInfo, Ten);
            MockUserRepository.Verify_SelectById();
            MockOrganizationRepository.Verify_SelectMany();
            MockOrganizationRepository.Verify_Count();
        }

        [Test]
        public async Task AdminService_GetOrganizations_Invalid_CurrentUserNotSuperAdmin()
        {
            // arrange
            var request = GetOrganizationReadListRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneUserOne();

            // act
            var result = await SystemUnderTest.GetOrganizations(request);

            // assert
            result.Status.ShouldBe(ResponseStatus.Invalid);
            result.ErrorMessages.ShouldNotBeNull();
            AssertReturnType<OrganizationReadListResponse>(result);
            MockUserRepository.Verify_SelectById();
        }

        [Test]
        public async Task AdminService_GetAllUsers_Success_SelectAfter()
        {
            // arrange
            var request = GetAllUserReadListRequestSelectAfter();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneSuperAdminUserOne();
            MockUserRepository.Setup_SelectAfter_Returns_Users();
            MockUserRepository.Setup_Count_Returns_Ten();

            // act
            var result = await SystemUnderTest.GetAllUsers(request);

            // assert
            result.Status.ShouldBe(ResponseStatus.Success);
            result.ErrorMessages.ShouldNotBeNull();
            result.ErrorMessages.Count.ShouldBe(0);
            AssertReturnType<AllUserReadListResponse>(result);
            AssertPagingInfoForSelectAfter(request.PagingInfo, Ten);
            MockUserRepository.Verify_SelectById();
            MockUserRepository.Verify_SelectAfter();
            MockUserRepository.Verify_Count();
        }

        [Test]
        public async Task AdminService_GetAllUsers_Success_SelectMany()
        {
            // arrange
            var request = GetAllUserReadListRequestSelectMany();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneSuperAdminUserOne();
            MockUserRepository.Setup_SelectMany_Returns_Users();
            MockUserRepository.Setup_Count_Returns_Ten();

            // act
            var result = await SystemUnderTest.GetAllUsers(request);

            // assert
            result.Status.ShouldBe(ResponseStatus.Success);
            result.ErrorMessages.ShouldNotBeNull();
            result.ErrorMessages.Count.ShouldBe(0);
            AssertReturnType<AllUserReadListResponse>(result);
            AssertPagingInfoForSelectMany(request.PagingInfo, Ten);
            MockUserRepository.Verify_SelectById();
            MockUserRepository.Verify_SelectMany();
            MockUserRepository.Verify_Count();
        }

        [Test]
        public async Task AdminService_GetAllUsers_Invalid_CurrentUserNotSuperAdmin()
        {
            // arrange
            var request = GetAllUserReadListRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneUserOne();

            // act
            var result = await SystemUnderTest.GetAllUsers(request);

            // assert
            result.Status.ShouldBe(ResponseStatus.Invalid);
            result.ErrorMessages.ShouldNotBeNull();
            AssertReturnType<AllUserReadListResponse>(result);
            MockUserRepository.Verify_SelectById();
        }

        [Test]
        public async Task AdminService_GetSuperAdmins_Success_SelectAfter()
        {
            // arrange
            var request = GetSuperAdminUserReadListRequestForSelectAfter();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneSuperAdminUserOne();
            MockUserRepository.Setup_SelectAfter_Returns_SuperAdmins();
            MockUserRepository.Setup_Count_Returns_Ten();

            // act
            var result = await SystemUnderTest.GetSuperAdmins(request);

            // assert
            result.Status.ShouldBe(ResponseStatus.Success);
            result.ErrorMessages.ShouldNotBeNull();
            result.ErrorMessages.Count.ShouldBe(0);
            AssertReturnType<SuperAdminUserReadListResponse>(result);
            AssertPagingInfoForSelectAfter(request.PagingInfo, Ten);
            MockUserRepository.Verify_SelectById();
            MockUserRepository.Verify_SelectAfter();
            MockUserRepository.Verify_Count();
        }

        [Test]
        public async Task AdminService_GetSuperAdmins_Success_SelectMany()
        {
            // arrange
            var request = GetSuperAdminUserReadListRequestForSelectMany();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneSuperAdminUserOne();
            MockUserRepository.Setup_SelectMany_Returns_SuperAdmins();
            MockUserRepository.Setup_Count_Returns_Ten();

            // act
            var result = await SystemUnderTest.GetSuperAdmins(request);

            // assert
            result.Status.ShouldBe(ResponseStatus.Success);
            result.ErrorMessages.ShouldNotBeNull();
            result.ErrorMessages.Count.ShouldBe(0);
            AssertReturnType<SuperAdminUserReadListResponse>(result);
            AssertPagingInfoForSelectMany(request.PagingInfo, Ten);
            MockUserRepository.Verify_SelectById();
            MockUserRepository.Verify_SelectMany();
            MockUserRepository.Verify_Count();
        }

        [Test]
        public async Task AdminService_GetSuperAdmins_Invalid_CurrentUserNotSuperAdmin()
        {
            // arrange
            var request = GetSuperAdminUserReadListRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneUserOne();

            // act
            var result = await SystemUnderTest.GetSuperAdmins(request);

            // assert
            result.Status.ShouldBe(ResponseStatus.Invalid);
            result.ErrorMessages.ShouldNotBeNull();
            AssertReturnType<SuperAdminUserReadListResponse>(result);
            MockUserRepository.Verify_SelectById();
        }

        [Test]
        public async Task AdminService_InviteSuperAdminUser_Success()
        {
            // arrange
            var request = GetAdminInviteRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneSuperAdminUserOne();
            MockUserRepository.Setup_Select_Returns_OrganizationOneUserOneNotExist();
            MockUserRepository.Setup_Insert_Success();

            // act
            var result = await SystemUnderTest.InviteSuperAdminUser(request);

            // assert
            result.Status.ShouldBe(ResponseStatus.Success);
            result.ErrorMessages.ShouldNotBeNull();
            result.ErrorMessages.Count.ShouldBe(0);
            AssertReturnType<AdminInviteResponse>(result);
            MockUserRepository.Verify_SelectById();
            MockUserRepository.Verify_Select();
            MockUserRepository.Verify_Insert();
        }

        [Test]
        public async Task AdminService_InviteSuperAdminUser_Invalid_CurrentUserNotSuperAdmin()
        {
            // arrange
            var request = GetAdminInviteRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneUserOne();

            // act
            var result = await SystemUnderTest.InviteSuperAdminUser(request);

            // assert
            result.Status.ShouldBe(ResponseStatus.Invalid);
            result.ErrorMessages.ShouldNotBeNull();
            AssertReturnType<AdminInviteResponse>(result);
            MockUserRepository.Verify_SelectById();
        }

        [Test]
        public async Task AdminService_InviteSuperAdminUser_Invalid_UserAlreadyInvited()
        {
            // arrange
            var request = GetAdminInviteRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneSuperAdminUserOne();
            MockUserRepository.Setup_Select_Returns_OrganizationOneUserOne();

            // act
            var result = await SystemUnderTest.InviteSuperAdminUser(request);

            // assert
            result.Status.ShouldBe(ResponseStatus.Invalid);
            result.ErrorMessages.ShouldNotBeNull();
            AssertReturnType<AdminInviteResponse>(result);
            MockUserRepository.Verify_SelectById();
            MockUserRepository.Verify_Select();
        }

        [Test]
        public async Task AdminService_InviteSuperAdminUser_Failed()
        {
            // arrange
            var request = GetAdminInviteRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneSuperAdminUserOne();
            MockUserRepository.Setup_Select_Returns_OrganizationOneUserOne();
            MockUserRepository.Setup_Insert_Failed();

            // act
            var result = await SystemUnderTest.InviteSuperAdminUser(request);

            // assert
            result.Status.ShouldBe(ResponseStatus.Invalid);
            result.ErrorMessages.ShouldNotBeNull();
            AssertReturnType<AdminInviteResponse>(result);
            MockUserRepository.Verify_SelectById();
            MockUserRepository.Verify_Select();
            MockUserRepository.Verify_Insert();
        }

        [Test]
        public async Task AdminService_ValidateSuperAdminUserInvitation_Success()
        {
            // arrange
            var request = GetAdminInviteValidateRequest();
            MockUserRepository.Setup_Select_Returns_OrganizationOneSuperAdminUserInvitedAtOneDayBefore();
            MockOrganizationRepository.Setup_Any_Returns_False();

            // act
            var result = await SystemUnderTest.ValidateSuperAdminUserInvitation(request);

            // assert
            result.Status.ShouldBe(ResponseStatus.Success);
            result.ErrorMessages.ShouldNotBeNull();
            result.ErrorMessages.Count.ShouldBe(0);
            AssertReturnType<AdminInviteValidateResponse>(result);
            MockUserRepository.Verify_Select();
            MockOrganizationRepository.Verify_Any();
        }

        [Test]
        public async Task AdminService_ValidateSuperAdminUserInvitation_Invalid_UserNotExist()
        {
            // arrange
            var request = GetAdminInviteValidateRequest();
            MockUserRepository.Setup_Select_Returns_OrganizationOneUserOneNotExist();

            // act
            var result = await SystemUnderTest.ValidateSuperAdminUserInvitation(request);

            // assert
            result.Status.ShouldBe(ResponseStatus.Invalid);
            result.ErrorMessages.ShouldNotBeNull();
            AssertReturnType<AdminInviteValidateResponse>(result);
            MockUserRepository.Verify_Select();
        }

        [Test]
        public async Task AdminService_ValidateSuperAdminUserInvitation_Invalid_OrganizationNotExist()
        {
            // arrange
            var request = GetAdminInviteValidateRequest();
            MockUserRepository.Setup_Select_Returns_OrganizationOneUserOne();
            MockOrganizationRepository.Setup_Any_Returns_True();

            // act
            var result = await SystemUnderTest.ValidateSuperAdminUserInvitation(request);

            // assert
            result.Status.ShouldBe(ResponseStatus.Invalid);
            result.ErrorMessages.ShouldNotBeNull();
            AssertReturnType<AdminInviteValidateResponse>(result);
            MockUserRepository.Verify_Select();
            MockOrganizationRepository.Verify_Any();
        }

        [Test]
        public async Task AdminService_ValidateSuperAdminUserInvitation_Failed()
        {
            // arrange
            var request = GetAdminInviteValidateRequest();
            MockUserRepository.Setup_Select_Returns_OrganizationOneUserOne();
            MockOrganizationRepository.Setup_Any_Returns_False();

            // act
            var result = await SystemUnderTest.ValidateSuperAdminUserInvitation(request);

            // assert
            result.Status.ShouldBe(ResponseStatus.Failed);
            result.ErrorMessages.ShouldNotBeNull();
            AssertReturnType<AdminInviteValidateResponse>(result);
            MockUserRepository.Verify_Select();
            MockOrganizationRepository.Verify_Any();
        }

        [Test]
        public async Task AdminService_AcceptSuperAdminUserInvite_Success()
        {
            // arrange
            var request = GetAdminAcceptInviteRequest();
            MockUserRepository.Setup_Select_Returns_OrganizationOneSuperAdminUserInvitedAtOneDayBefore();
            MockOrganizationRepository.Setup_Any_Returns_False();
            MockUserRepository.Setup_Update_Success();

            // act
            var result = await SystemUnderTest.AcceptSuperAdminUserInvite(request);

            // assert
            result.Status.ShouldBe(ResponseStatus.Success);
            result.ErrorMessages.ShouldNotBeNull();
            result.ErrorMessages.Count.ShouldBe(0);
            AssertReturnType<AdminAcceptInviteResponse>(result);
            MockUserRepository.Verify_Select();
            MockOrganizationRepository.Verify_Any();
            MockUserRepository.Verify_Update();
        }

        [Test]
        public async Task AdminService_AcceptSuperAdminUserInvite_Invalid_UserNotFound()
        {
            // arrange
            var request = GetAdminAcceptInviteRequest();
            MockUserRepository.Setup_Select_Returns_OrganizationOneSuperAdminUserInvitedAtOneDayBefore();
            MockOrganizationRepository.Setup_Any_Returns_True();

            // act
            var result = await SystemUnderTest.AcceptSuperAdminUserInvite(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Invalid, UserNotFound);
            AssertReturnType<AdminAcceptInviteResponse>(result);
            MockUserRepository.Verify_Select();
            MockOrganizationRepository.Verify_Any();
        }

        [Test]
        public async Task AdminService_AcceptSuperAdminUserInvite_Invalid_OrganizationNotExist()
        {
            // arrange
            var request = GetAdminAcceptInviteRequest();
            MockUserRepository.Setup_Select_Returns_OrganizationOneUserOneNotExist();

            // act
            var result = await SystemUnderTest.AcceptSuperAdminUserInvite(request);

            // assert
            result.Status.ShouldBe(ResponseStatus.Invalid);
            result.ErrorMessages.ShouldNotBeNull();
            AssertReturnType<AdminAcceptInviteResponse>(result);
            MockUserRepository.Verify_Select();
        }

        [Test]
        public async Task AdminService_AcceptSuperAdminUserInvite_Failed()
        {
            // arrange
            var request = GetAdminAcceptInviteRequest();
            MockUserRepository.Setup_Select_Returns_OrganizationOneSuperAdminUserInvitedAtOneDayBefore();
            MockOrganizationRepository.Setup_Any_Returns_True();
            MockUserRepository.Setup_Update_Failed();

            // act
            var result = await SystemUnderTest.AcceptSuperAdminUserInvite(request);

            // assert
            result.Status.ShouldBe(ResponseStatus.Failed);
            result.ErrorMessages.ShouldNotBeNull();
            AssertReturnType<AdminAcceptInviteResponse>(result);
            MockUserRepository.Verify_Select();
            MockOrganizationRepository.Verify_Any();
            MockUserRepository.Verify_Update();
        }

        [Test]
        public async Task AdminService_ChangeActivation_Success()
        {
            // arrange
            var request = GetUserChangeActivationRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneSuperAdminUserOne();
            MockUserRepository.Setup_Select_Returns_OrganizationOneUserOne();
            MockUserRepository.Setup_Update_Success();

            // act
            var result = await SystemUnderTest.ChangeActivation(request);

            // assert
            result.Status.ShouldBe(ResponseStatus.Success);
            result.ErrorMessages.ShouldNotBeNull();
            result.ErrorMessages.Count.ShouldBe(0);
            AssertReturnType<UserChangeActivationResponse>(result);
            MockUserRepository.Verify_SelectById();
            MockUserRepository.Verify_Select();
            MockUserRepository.Verify_Update();
        }

        [Test]
        public async Task AdminService_ChangeActivation_Invalid_CurrentUserNotSuperAdmin()
        {
            // arrange
            var request = GetUserChangeActivationRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneUserOne();

            // act
            var result = await SystemUnderTest.ChangeActivation(request);

            // assert
            result.Status.ShouldBe(ResponseStatus.Invalid);
            result.ErrorMessages.ShouldNotBeNull();
            AssertReturnType<UserChangeActivationResponse>(result);
            MockUserRepository.Verify_SelectById();
        }

        [Test]
        public async Task AdminService_ChangeActivation_Invalid_UserNotExist()
        {
            // arrange
            var request = GetUserChangeActivationRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneSuperAdminUserOne();
            MockUserRepository.Setup_Select_Returns_OrganizationOneUserOneNotExist();

            // act
            var result = await SystemUnderTest.ChangeActivation(request);

            // assert
            result.Status.ShouldBe(ResponseStatus.Invalid);
            result.ErrorMessages.ShouldNotBeNull();
            AssertReturnType<UserChangeActivationResponse>(result);
            MockUserRepository.Verify_SelectById();
            MockUserRepository.Verify_Select();
        }

        [Test]
        public async Task AdminService_ChangeActivation_Failed()
        {
            // arrange
            var request = GetUserChangeActivationRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneSuperAdminUserOne();
            MockUserRepository.Setup_Select_Returns_OrganizationOneUserOne();
            MockUserRepository.Setup_Update_Failed();

            // act
            var result = await SystemUnderTest.ChangeActivation(request);

            // assert
            result.Status.ShouldBe(ResponseStatus.Failed);
            result.ErrorMessages.ShouldNotBeNull();
            AssertReturnType<UserChangeActivationResponse>(result);
            MockUserRepository.Verify_SelectById();
            MockUserRepository.Verify_Select();
            MockUserRepository.Verify_Update();
        }

        [Test]
        public async Task AdminService_OrganizationChangeActivation_Success()
        {
            // arrange
            var request = GetOrganizationChangeActivationRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneSuperAdminUserOne();
            MockOrganizationRepository.Setup_Select_Returns_OrganizationOne();
            MockUserRepository.Setup_Select_Returns_OrganizationOneUserOne();
            MockUserRepository.Setup_Update_Success();

            // act
            var result = await SystemUnderTest.OrganizationChangeActivation(request);

            // assert
            result.Status.ShouldBe(ResponseStatus.Success);
            result.ErrorMessages.ShouldNotBeNull();
            result.ErrorMessages.Count.ShouldBe(0);
            AssertReturnType<OrganizationChangeActivationResponse>(result);
            MockUserRepository.Verify_SelectById();
            MockOrganizationRepository.Verify_Select();
            MockUserRepository.Verify_Select();
            MockUserRepository.Verify_Update();
        }

        [Test]
        public async Task AdminService_OrganizationChangeActivation_Invalid_CurrentUserNotSuperAdmin()
        {
            // arrange
            var request = GetOrganizationChangeActivationRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneUserOne();

            // act
            var result = await SystemUnderTest.OrganizationChangeActivation(request);

            // assert
            result.Status.ShouldBe(ResponseStatus.Invalid);
            result.ErrorMessages.ShouldNotBeNull();
            AssertReturnType<OrganizationChangeActivationResponse>(result);
            MockUserRepository.Verify_SelectById();
        }

        [Test]
        public async Task AdminService_OrganizationChangeActivation_Invalid_UserNotExist()
        {
            // arrange
            var request = GetOrganizationChangeActivationRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneUserOne();
            MockUserRepository.Setup_Select_Returns_OrganizationOneUserOneNotExist();

            // act
            var result = await SystemUnderTest.OrganizationChangeActivation(request);

            // assert
            result.Status.ShouldBe(ResponseStatus.Invalid);
            result.ErrorMessages.ShouldNotBeNull();
            AssertReturnType<OrganizationChangeActivationResponse>(result);
            MockUserRepository.Verify_SelectById();
            MockUserRepository.Verify_Select();
        }

        [Test]
        public async Task AdminService_OrganizationChangeActivation_Failed()
        {
            // arrange
            var request = GetOrganizationChangeActivationRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneSuperAdminUserOne();
            MockOrganizationRepository.Setup_Select_Returns_OrganizationOne();
            MockUserRepository.Setup_Select_Returns_OrganizationOneUserOne();
            MockUserRepository.Setup_Update_Failed();

            // act
            var result = await SystemUnderTest.OrganizationChangeActivation(request);

            // assert
            result.Status.ShouldBe(ResponseStatus.Failed);
            result.ErrorMessages.ShouldNotBeNull();
            AssertReturnType<OrganizationChangeActivationResponse>(result);
            MockUserRepository.Verify_SelectById();
            MockOrganizationRepository.Verify_Select();
            MockUserRepository.Verify_Select();
            MockUserRepository.Verify_Update();
        }

        [Test]
        public async Task AdminService_DemoteToUser_Success()
        {
            // arrange
            var request = GetAdminDemoteRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneSuperAdminUserOne();
            MockUserRepository.Setup_Select_Returns_OrganizationOneUserOne();
            MockUserRepository.Setup_Update_Success();

            // act
            var result = await SystemUnderTest.DemoteToUser(request);

            // assert
            result.Status.ShouldBe(ResponseStatus.Success);
            result.ErrorMessages.ShouldNotBeNull();
            result.ErrorMessages.Count.ShouldBe(0);
            AssertReturnType<AdminDemoteResponse>(result);
            MockUserRepository.Verify_SelectById();
            MockUserRepository.Verify_Select();
            MockUserRepository.Verify_Update();
        }

        [Test]
        public async Task AdminService_DemoteToUser_Invalid_CurrenctUserNotSuperAdmin()
        {
            // arrange
            var request = GetAdminDemoteRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneUserOne();

            // act
            var result = await SystemUnderTest.DemoteToUser(request);

            // assert
            result.Status.ShouldBe(ResponseStatus.Success);
            result.ErrorMessages.ShouldNotBeNull();
            AssertReturnType<AdminDemoteResponse>(result);
            MockUserRepository.Verify_SelectById();
        }

        [Test]
        public async Task AdminService_DemoteToUser_Invalid_UserNotExist()
        {
            // arrange
            var request = GetAdminDemoteRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneSuperAdminUserOne();
            MockUserRepository.Setup_Select_Returns_OrganizationOneUserOneNotExist();

            // act
            var result = await SystemUnderTest.DemoteToUser(request);

            // assert
            result.Status.ShouldBe(ResponseStatus.Invalid);
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Invalid, UserNotFound);
            AssertReturnType<AdminDemoteResponse>(result);
            MockUserRepository.Verify_SelectById();
            MockUserRepository.Verify_Select();
        }

        [Test]
        public async Task AdminService_DemoteToUser_Failed()
        {
            // arrange
            var request = GetAdminDemoteRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneSuperAdminUserOne();
            MockUserRepository.Setup_Select_Returns_OrganizationOneUserOne();
            MockUserRepository.Setup_Update_Failed();

            // act
            var result = await SystemUnderTest.DemoteToUser(request);

            // assert
            result.Status.ShouldBe(ResponseStatus.Failed);
            result.ErrorMessages.ShouldNotBeNull();
            result.ErrorMessages.Count.ShouldBe(0);
            AssertReturnType<AdminDemoteResponse>(result);
            MockUserRepository.Verify_SelectById();
            MockUserRepository.Verify_Select();
            MockUserRepository.Verify_Update();
        }

        [Test]
        public async Task AdminService_UpgradeToAdmin_Success()
        {
            // arrange
            var request = GetAdminUpgradeRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneSuperAdminUserOne();
            MockUserRepository.Setup_Select_Returns_OrganizationOneUserOne();
            MockUserRepository.Setup_Update_Success();

            // act
            var result = await SystemUnderTest.UpgradeToAdmin(request);

            // assert
            result.Status.ShouldBe(ResponseStatus.Success);
            result.ErrorMessages.ShouldNotBeNull();
            result.ErrorMessages.Count.ShouldBe(0);
            AssertReturnType<AdminUpgradeResponse>(result);
            MockUserRepository.Verify_SelectById();
            MockUserRepository.Verify_Select();
            MockUserRepository.Verify_Update();
        }

        [Test]
        public async Task AdminService_UpgradeToAdmin_CurrentUserNotSuperAdmin()
        {
            // arrange
            var request = GetAdminUpgradeRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneUserOne();

            // act
            var result = await SystemUnderTest.UpgradeToAdmin(request);

            // assert
            result.Status.ShouldBe(ResponseStatus.Invalid);
            result.ErrorMessages.ShouldNotBeNull();
            AssertReturnType<AdminUpgradeResponse>(result);
            MockUserRepository.Verify_SelectById();
        }

        [Test]
        public async Task AdminService_UpgradeToAdmin_Invalid_UserNotExist()
        {
            // arrange
            var request = GetAdminUpgradeRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneSuperAdminUserOne();
            MockUserRepository.Setup_Select_Returns_OrganizationOneUserOneNotExist();

            // act
            var result = await SystemUnderTest.UpgradeToAdmin(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Invalid, UserNotFound);
            AssertReturnType<AdminUpgradeResponse>(result);
            MockUserRepository.Verify_SelectById();
            MockUserRepository.Verify_Select();
        }

        [Test]
        public async Task AdminService_UpgradeToAdmin_Failed()
        {
            // arrange
            var request = GetAdminUpgradeRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneSuperAdminUserOne();
            MockUserRepository.Setup_Select_Returns_OrganizationOneUserOne();
            MockUserRepository.Setup_Update_Failed();

            // act
            var result = await SystemUnderTest.UpgradeToAdmin(request);

            // assert
            result.Status.ShouldBe(ResponseStatus.Failed);
            result.ErrorMessages.ShouldNotBeNull();
            result.ErrorMessages.Count.ShouldBe(0);
            AssertReturnType<AdminUpgradeResponse>(result);
            MockUserRepository.Verify_SelectById();
            MockUserRepository.Verify_Select();
            MockUserRepository.Verify_Update();
        }

        [Test]
        public async Task AdminService_GetJournals_Success_SelectAfter()
        {
            // arrange
            var request = GetAllJournalReadListRequestForSelectAfter();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneSuperAdminUserOne();
            MockJournalRepository.Setup_SelectAfter_Returns_Journals();
            MockJournalRepository.Setup_Count_Returns_Ten();

            // act
            var result = await SystemUnderTest.GetJournals(request);

            // assert
            result.Status.ShouldBe(ResponseStatus.Success);
            result.ErrorMessages.ShouldNotBeNull();
            result.ErrorMessages.Count.ShouldBe(0);
            AssertReturnType<JournalReadListResponse>(result);
            AssertPagingInfoForSelectAfter(request.PagingInfo, Ten);
            MockUserRepository.Verify_SelectById();
            MockJournalRepository.Verify_SelectAfter();
            MockJournalRepository.Verify_Count();
        }

        [Test]
        public async Task AdminService_GetJournals_Success_SelectMany()
        {
            // arrange
            var request = GetAllJournalReadListRequestForSelectMany();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneSuperAdminUserOne();
            MockJournalRepository.Setup_SelectMany_Returns_Journals();
            MockJournalRepository.Setup_Count_Returns_Ten();

            // act
            var result = await SystemUnderTest.GetJournals(request);

            // assert
            result.Status.ShouldBe(ResponseStatus.Success);
            result.ErrorMessages.ShouldNotBeNull();
            result.ErrorMessages.Count.ShouldBe(0);
            AssertReturnType<JournalReadListResponse>(result);
            AssertPagingInfoForSelectAfter(request.PagingInfo, Ten);
            MockUserRepository.Verify_SelectById();
            MockJournalRepository.Verify_SelectMany();
            MockJournalRepository.Verify_Count();
        }

        [Test]
        public async Task AdminService_GetJournals_Invalid_CurrentUserNotSuperAdmin()
        {
            // arrange
            var request = GetAllJournalReadListRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneUserOne();

            // act
            var result = await SystemUnderTest.GetJournals(request);

            // assert
            result.Status.ShouldBe(ResponseStatus.Invalid);
            result.ErrorMessages.ShouldNotBeNull();
            AssertReturnType<JournalReadListResponse>(result);
            MockUserRepository.Verify_SelectById();
        }
    }
}