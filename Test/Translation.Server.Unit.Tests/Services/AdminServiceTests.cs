using System.Threading.Tasks;
using Autofac;
using NUnit.Framework;
using Shouldly;

using Translation.Common.Contracts;
using StandardUtils.Enumerations;
using Translation.Common.Models.Responses.Admin;
using Translation.Common.Models.Responses.Integration.Token.RequestLog;
using Translation.Common.Models.Responses.Journal;
using Translation.Common.Models.Responses.Organization;
using Translation.Common.Models.Responses.SendEmailLog;
using Translation.Common.Models.Responses.TranslationProvider;
using Translation.Common.Models.Responses.User;
using Translation.Common.Models.Responses.User.LoginLog;
using Translation.Server.Unit.Tests.RepositorySetupHelpers;

using static Translation.Common.Tests.TestHelpers.FakeRequestTestHelper;
using static Translation.Common.Tests.TestHelpers.FakeConstantTestHelper;
using static Translation.Server.Unit.Tests.TestHelpers.AssertResponseTestHelper;
using static Translation.Server.Unit.Tests.TestHelpers.AssertViewModelTestHelper;

namespace Translation.Server.Unit.Tests.Services
{
    [TestFixture]
    public class AdminServiceTests : ServiceBaseTests
    {
        public IAdminService SystemUnderTest { get; set; }

        [SetUp]
        public void run_before_every_test()
        {
            Refresh();
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
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Success);
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
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Invalid);
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
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Success);
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
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Success);
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
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Invalid);
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
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Success);
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
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Success);
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
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Invalid);
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
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Success);
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
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Invalid);
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
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Invalid, "user_already_invited");
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
            MockUserRepository.Setup_Select_Returns_OrganizationOneUserOneNotExist();
            MockUserRepository.Setup_Insert_Failed();

            // act
            var result = await SystemUnderTest.InviteSuperAdminUser(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Failed);
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
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Success);
            AssertReturnType<AdminInviteValidateResponse>(result);
            MockUserRepository.Verify_Select();
            MockOrganizationRepository.Verify_Any();
        }

        [Test]
        public async Task AdminService_ValidateSuperAdminUserInvitation_Failed_UserNotFound()
        {
            // arrange
            var request = GetAdminInviteValidateRequest();
            MockUserRepository.Setup_Select_Returns_OrganizationOneUserOneNotExist();

            // act
            var result = await SystemUnderTest.ValidateSuperAdminUserInvitation(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Failed, UserNotFound);
            AssertReturnType<AdminInviteValidateResponse>(result);
            MockUserRepository.Verify_Select();
        }

        [Test]
        public async Task AdminService_ValidateSuperAdminUserInvitation_Invalid_OrganizationNotActive()
        {
            // arrange
            var request = GetAdminInviteValidateRequest();
            MockUserRepository.Setup_Select_Returns_OrganizationOneUserOne();
            MockOrganizationRepository.Setup_Any_Returns_True();

            // act
            var result = await SystemUnderTest.ValidateSuperAdminUserInvitation(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Invalid, OrganizationNotActive);
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
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Failed);
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
            MockOrganizationRepository.Setup_Update_Success();

            // act
            var result = await SystemUnderTest.AcceptSuperAdminUserInvite(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Success);
            AssertReturnType<AdminAcceptInviteResponse>(result);
            MockUserRepository.Verify_Select();
            MockOrganizationRepository.Verify_Any();
            MockUserRepository.Verify_Update();
            MockOrganizationRepository.Verify_Update();
        }

        [Test]
        public async Task AdminService_AcceptSuperAdminUserInvite_Failed_UserUpdate()
        {
            // arrange
            var request = GetAdminAcceptInviteRequest();
            MockUserRepository.Setup_Select_Returns_OrganizationOneSuperAdminUserInvitedAtOneDayBefore();
            MockOrganizationRepository.Setup_Any_Returns_False();
            MockUserRepository.Setup_Update_Failed();

            // act
            var result = await SystemUnderTest.AcceptSuperAdminUserInvite(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Failed);
            AssertReturnType<AdminAcceptInviteResponse>(result);
            MockUserRepository.Verify_Select();
            MockOrganizationRepository.Verify_Any();
            MockUserRepository.Verify_Update();
        }

        [Test]
        public async Task AdminService_AcceptSuperAdminUserInvite_Failed_OrganizationUpdate()
        {
            // arrange
            var request = GetAdminAcceptInviteRequest();
            MockUserRepository.Setup_Select_Returns_OrganizationOneSuperAdminUserInvitedAtOneDayBefore();
            MockOrganizationRepository.Setup_Any_Returns_False();
            MockUserRepository.Setup_Update_Success();
            MockOrganizationRepository.Setup_Update_Failed();

            // act
            var result = await SystemUnderTest.AcceptSuperAdminUserInvite(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Failed);
            AssertReturnType<AdminAcceptInviteResponse>(result);
            MockUserRepository.Verify_Select();
            MockOrganizationRepository.Verify_Any();
            MockUserRepository.Verify_Update();
            MockOrganizationRepository.Verify_Update();
        }

        [Test]
        public async Task AdminService_AcceptSuperAdminUserInvite_Failed_UserNotFound()
        {
            // arrange
            var request = GetAdminAcceptInviteRequest();
            MockUserRepository.Setup_Select_Returns_OrganizationOneUserOneNotExist();

            // act
            var result = await SystemUnderTest.AcceptSuperAdminUserInvite(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Failed, UserNotFound);
            AssertReturnType<AdminAcceptInviteResponse>(result);
            MockUserRepository.Verify_Select();
        }

        [Test]
        public async Task AdminService_AcceptSuperAdminUserInvite_Invalid_OrganizationNotActive()
        {
            // arrange
            var request = GetAdminAcceptInviteRequest();
            MockUserRepository.Setup_Select_Returns_OrganizationOneUserOne();
            MockOrganizationRepository.Setup_Any_Returns_True();

            // act
            var result = await SystemUnderTest.AcceptSuperAdminUserInvite(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Invalid, OrganizationNotActive);
            AssertReturnType<AdminAcceptInviteResponse>(result);
            MockUserRepository.Verify_Select();
            MockOrganizationRepository.Verify_Any();
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
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Success);
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
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Invalid);
            AssertReturnType<UserChangeActivationResponse>(result);
            MockUserRepository.Verify_SelectById();
        }

        [Test]
        public async Task AdminService_ChangeActivation_Failed_UserNotFound()
        {
            // arrange
            var request = GetUserChangeActivationRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneSuperAdminUserOne();
            MockUserRepository.Setup_Select_Returns_OrganizationOneUserOneNotExist();

            // act
            var result = await SystemUnderTest.ChangeActivation(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Failed, UserNotFound);
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
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Failed);
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
            MockOrganizationRepository.Setup_Update_Success();

            // act
            var result = await SystemUnderTest.OrganizationChangeActivation(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Success);
            AssertReturnType<OrganizationChangeActivationResponse>(result);
            MockUserRepository.Verify_SelectById();
            MockOrganizationRepository.Verify_Select();
            MockOrganizationRepository.Verify_Update();
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
        public async Task AdminService_OrganizationChangeActivation_Failed_OrganizationNotFound()
        {
            // arrange
            var request = GetOrganizationChangeActivationRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneSuperAdminUserOne();
            MockOrganizationRepository.Setup_Select_Returns_OrganizationOneNotExist();

            // act
            var result = await SystemUnderTest.OrganizationChangeActivation(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Failed, OrganizationNotFound);
            MockUserRepository.Verify_SelectById();
            MockOrganizationRepository.Verify_Select();
        }

        [Test]
        public async Task AdminService_OrganizationChangeActivation_Failed()
        {
            // arrange
            var request = GetOrganizationChangeActivationRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneSuperAdminUserOne();
            MockOrganizationRepository.Setup_Select_Returns_OrganizationOne();
            MockOrganizationRepository.Setup_Update_Failed();

            // act
            var result = await SystemUnderTest.OrganizationChangeActivation(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Failed);
            AssertReturnType<OrganizationChangeActivationResponse>(result);
            MockUserRepository.Verify_SelectById();
            MockOrganizationRepository.Verify_Select();
            MockOrganizationRepository.Verify_Update();
        }

        [Test]
        public async Task AdminService_TranslationProviderChangeActivation_Success()
        {
            // arrange
            var request = GetTranslationProviderChangeActivationRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneSuperAdminUserOne();
            MockTranslationProviderRepository.Setup_SelectAll_Returns_TranslationProviders();
            MockTranslationProviderRepository.Setup_Select_Returns_TranslationProviderOne();
            MockTranslationProviderRepository.Setup_Update_Success();

            // act
            var result = await SystemUnderTest.TranslationProviderChangeActivation(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Success);
            AssertReturnType<TranslationProviderChangeActivationResponse>(result);
            MockUserRepository.Verify_SelectById();
            MockTranslationProviderRepository.Verify_SelectAll();
            MockTranslationProviderRepository.Verify_Select();
            MockTranslationProviderRepository.Verify_Update();
        }

        [Test]
        public async Task AdminService_TranslationProviderChangeActivation_MatchValue_Success()
        {
            // arrange
            var request = GetTranslationProviderChangeActivationRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneSuperAdminUserOne();
            MockTranslationProviderRepository.Setup_SelectAll_Returns_TranslationProviders();
            MockTranslationProviderRepository.Setup_Select_Returns_TranslationProviderTwo();
            MockTranslationProviderRepository.Setup_Update_Success();

            // act
            var result = await SystemUnderTest.TranslationProviderChangeActivation(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Success);
            AssertReturnType<TranslationProviderChangeActivationResponse>(result);
            MockUserRepository.Verify_SelectById();
            MockTranslationProviderRepository.Verify_SelectAll();
            MockTranslationProviderRepository.Verify_Select();
            MockTranslationProviderRepository.Verify_Update();
        }

        [Test]
        public async Task AdminService_TranslationProviderChangeActivation_Invalid_CurrentUserNotSuperAdmin()
        {
            // arrange
            var request = GetTranslationProviderChangeActivationRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneUserOne();

            // act
            var result = await SystemUnderTest.TranslationProviderChangeActivation(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Invalid);
            AssertReturnType<TranslationProviderChangeActivationResponse>(result);
            MockUserRepository.Verify_SelectById();
        }

        [Test]
        public async Task AdminService_TranslationProviderChangeActivation_Failed_TranslationProviderNotFound()
        {
            // arrange
            var request = GetTranslationProviderChangeActivationRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneSuperAdminUserOne();
            MockTranslationProviderRepository.Setup_SelectAll_Returns_TranslationProvidersNull();

            // act
            var result = await SystemUnderTest.TranslationProviderChangeActivation(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Failed, TranslationProviderNotFound);
            AssertReturnType<TranslationProviderChangeActivationResponse>(result);
            MockUserRepository.Verify_SelectById();
            MockTranslationProviderRepository.Verify_SelectAll();
        }

        [Test]
        public async Task AdminService_TranslationProviderChangeActivation_Invalid_NullValue()
        {
            // arrange
            var request = GetTranslationProviderChangeActivationRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneSuperAdminUserOne();
            MockTranslationProviderRepository.Setup_SelectAll_Returns_TranslationProviders();
            MockTranslationProviderRepository.Setup_Select_Returns_GetTranslationProviderNullValue();

            // act
            var result = await SystemUnderTest.TranslationProviderChangeActivation(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Invalid, "please_edit_translation_api_value");
            AssertReturnType<TranslationProviderChangeActivationResponse>(result);
            MockUserRepository.Verify_SelectById();
            MockTranslationProviderRepository.Verify_SelectAll();
            MockTranslationProviderRepository.Verify_Select();
        }

        [Test]
        public async Task AdminService_TranslationProviderChangeActivation_Failed()
        {
            // arrange
            var request = GetTranslationProviderChangeActivationRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneSuperAdminUserOne();
            MockTranslationProviderRepository.Setup_SelectAll_Returns_TranslationProviders();
            MockTranslationProviderRepository.Setup_Select_Returns_TranslationProviderOne();
            MockTranslationProviderRepository.Setup_Update_Failed();

            // act
            var result = await SystemUnderTest.TranslationProviderChangeActivation(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Failed);
            AssertReturnType<TranslationProviderChangeActivationResponse>(result);
            MockUserRepository.Verify_SelectById();
            MockTranslationProviderRepository.Verify_SelectAll();
            MockTranslationProviderRepository.Verify_Select();
            MockTranslationProviderRepository.Verify_Update();
        }

        [Test]
        public async Task AdminService_DemoteToUser_Success()
        {
            // arrange
            var request = GetAdminDemoteRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneSuperAdminUserOne();
            MockUserRepository.Setup_Select_Returns_OrganizationOneAdminUserOne();
            MockUserRepository.Setup_Update_Success();

            // act
            var result = await SystemUnderTest.DemoteToUser(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Success);
            AssertReturnType<AdminDemoteResponse>(result);
            MockUserRepository.Verify_SelectById();
            MockUserRepository.Verify_Select();
            MockUserRepository.Verify_Update();
        }

        [Test]
        public async Task AdminService_DemoteToUser_Invalid_UserNotAdmin()
        {
            // arrange
            var request = GetAdminDemoteRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneSuperAdminUserOne();
            MockUserRepository.Setup_Select_Returns_OrganizationOneUserOne();
            
            // act
            var result = await SystemUnderTest.DemoteToUser(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Invalid, UserNotAdmin);
            AssertReturnType<AdminDemoteResponse>(result);
            MockUserRepository.Verify_SelectById();
            MockUserRepository.Verify_Select();
        }

        [Test]
        public async Task AdminService_DemoteToUser_Invalid_CurrentUserNotSuperAdmin()
        {
            // arrange
            var request = GetAdminDemoteRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneUserOne();

            // act
            var result = await SystemUnderTest.DemoteToUser(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Invalid);
            AssertReturnType<AdminDemoteResponse>(result);
            MockUserRepository.Verify_SelectById();
        }

        [Test]
        public async Task AdminService_DemoteToUser_Failed_UserNotFound()
        {
            // arrange
            var request = GetAdminDemoteRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneSuperAdminUserOne();
            MockUserRepository.Setup_Select_Returns_OrganizationOneUserOneNotExist();

            // act
            var result = await SystemUnderTest.DemoteToUser(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Failed, UserNotFound);
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
            MockUserRepository.Setup_Select_Returns_OrganizationOneAdminUserOne();
            MockUserRepository.Setup_Update_Failed();

            // act
            var result = await SystemUnderTest.DemoteToUser(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Failed);
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
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Success);
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
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Invalid);
            AssertReturnType<AdminUpgradeResponse>(result);
            MockUserRepository.Verify_SelectById();
        }

        [Test]
        public async Task AdminService_UpgradeToAdmin_Failed_UserNotFound()
        {
            // arrange
            var request = GetAdminUpgradeRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneSuperAdminUserOne();
            MockUserRepository.Setup_Select_Returns_OrganizationOneUserOneNotExist();

            // act
            var result = await SystemUnderTest.UpgradeToAdmin(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Failed, UserNotFound);
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
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Failed);
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
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Success);
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
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Success);
            AssertReturnType<JournalReadListResponse>(result);
            AssertPagingInfoForSelectMany(request.PagingInfo, Ten);
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
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Invalid);
            AssertReturnType<JournalReadListResponse>(result);
            MockUserRepository.Verify_SelectById();
        }

        [Test]
        public async Task AdminService_GetTokenRequestLogs_Success_SelectAfter()
        {
            // arrange
            var request = GetAllTokenRequestLogReadListRequestForSelectAfter();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneSuperAdminUserOne();
            MockTokenRequestLogRepository.Setup_SelectAfter_Returns_TokenRequestLogs();
            MockTokenRequestLogRepository.Setup_Count_Returns_Ten();

            // act
            var result = await SystemUnderTest.GetTokenRequestLogs(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Success);
            AssertReturnType<AllTokenRequestLogReadListResponse>(result);
            AssertPagingInfoForSelectAfter(request.PagingInfo, Ten);
            MockUserRepository.Verify_SelectById();
            MockTokenRequestLogRepository.Verify_SelectAfter();
            MockTokenRequestLogRepository.Verify_Count();
        }

        [Test]
        public async Task AdminService_GetTokenRequestLogs_Success_SelectMany()
        {
            // arrange
            var request = GetAllTokenRequestLogReadListRequestForSelectMany();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneSuperAdminUserOne();
            MockTokenRequestLogRepository.Setup_SelectMany_Returns_TokenRequestLogs();
            MockTokenRequestLogRepository.Setup_Count_Returns_Ten();

            // act
            var result = await SystemUnderTest.GetTokenRequestLogs(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Success);
            AssertReturnType<AllTokenRequestLogReadListResponse>(result);
            AssertPagingInfoForSelectMany(request.PagingInfo, Ten);
            MockUserRepository.Verify_SelectById();
            MockTokenRequestLogRepository.Verify_SelectMany();
            MockTokenRequestLogRepository.Verify_Count();
        }

        [Test]
        public async Task AdminService_GetTokenRequestLogs_Invalid_CurrentUserNotSuperAdmin()
        {
            // arrange
            var request = GetAllTokenRequestLogReadListRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneUserOne();

            // act
            var result = await SystemUnderTest.GetTokenRequestLogs(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Invalid);
            AssertReturnType<AllTokenRequestLogReadListResponse>(result);
            MockUserRepository.Verify_SelectById();
        }

        [Test]
        public async Task AdminService_GetSendEmailLogs_Success_SelectAfter()
        {
            // arrange
            var request = GetAllSendEmailLogReadListRequestForSelectAfter();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneSuperAdminUserOne();
            MockSendEmailLogRepository.Setup_SelectAfter_Returns_SendEmailLogs();
            MockSendEmailLogRepository.Setup_Count_Returns_Ten();

            // act
            var result = await SystemUnderTest.GetSendEmailLogs(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Success);
            AssertReturnType<AllSendEmailReadListResponse>(result);
            AssertPagingInfoForSelectAfter(request.PagingInfo, Ten);
            MockUserRepository.Verify_SelectById();
            MockSendEmailLogRepository.Verify_SelectAfter();
            MockSendEmailLogRepository.Verify_Count();
        }

        [Test]
        public async Task AdminService_GetSendEmailLogs_Success_SelectMany()
        {
            // arrange
            var request = GetAllSendEmailLogReadListRequestForSelectMany();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneSuperAdminUserOne();
            MockSendEmailLogRepository.Setup_SelectMany_Returns_SendEmailLogs();
            MockSendEmailLogRepository.Setup_Count_Returns_Ten();

            // act
            var result = await SystemUnderTest.GetSendEmailLogs(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Success);
            AssertReturnType<AllSendEmailReadListResponse>(result);
            AssertPagingInfoForSelectMany(request.PagingInfo, Ten);
            MockUserRepository.Verify_SelectById();
            MockSendEmailLogRepository.Verify_SelectMany();
            MockSendEmailLogRepository.Verify_Count();
        }

        [Test]
        public async Task AdminService_GetSendEmailLogs_Invalid_CurrentUserNotSuperAdmin()
        {
            // arrange
            var request = GetAllSendEmailLogReadListRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneUserOne();

            // act
            var result = await SystemUnderTest.GetSendEmailLogs(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Invalid);
            AssertReturnType<AllSendEmailReadListResponse>(result);
            MockUserRepository.Verify_SelectById();
        }

        [Test]
        public async Task AdminService_AllUserLoginLogs_Success_SelectAfter()
        {
            // arrange
            var request = GetAllLoginLogReadListRequestForSelectAfter();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneSuperAdminUserOne();
            MockUserLoginLogRepository.Setup_SelectAfter_Returns_UserLoginLogs();
            MockUserLoginLogRepository.Setup_Count_Returns_Ten();

            // act
            var result = await SystemUnderTest.GetAllUserLoginLogs(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Success);
            AssertReturnType<AllLoginLogReadListResponse>(result);
            AssertPagingInfoForSelectAfter(request.PagingInfo, Ten);
            MockUserRepository.Verify_SelectById();
            MockUserLoginLogRepository.Verify_SelectAfter();
            MockUserLoginLogRepository.Verify_Count();
        }

        [Test]
        public async Task AdminService_AllUserLoginLogs_Success_SelectMany()
        {
            // arrange
            var request = GetAllLoginLogReadListRequestForSelectMany();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneSuperAdminUserOne();
            MockUserLoginLogRepository.Setup_SelectMany_Returns_UserLoginLogs();
            MockUserLoginLogRepository.Setup_Count_Returns_Ten();

            // act
            var result = await SystemUnderTest.GetAllUserLoginLogs(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Success);
            AssertReturnType<AllLoginLogReadListResponse>(result);
            AssertPagingInfoForSelectMany(request.PagingInfo, Ten);
            MockUserRepository.Verify_SelectById();
            MockUserLoginLogRepository.Verify_SelectMany();
            MockUserLoginLogRepository.Verify_Count();
        }

        [Test]
        public async Task AdminService_AllUserLoginLogs_Invalid_CurrentUserNotSuperAdmin()
        {
            // arrange
            var request = GetAllLoginLogReadListRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneUserOne();

            // act
            var result = await SystemUnderTest.GetAllUserLoginLogs(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Invalid);
            AssertReturnType<AllLoginLogReadListResponse>(result);
            MockUserRepository.Verify_SelectById();
        }
    }
}