using System.Linq;
using System.Threading.Tasks;

using NUnit.Framework;
using Shouldly;

using Translation.Common.Contracts;
using Translation.Common.Enumerations;
using Translation.Common.Models.Requests.Integration;
using Translation.Common.Models.Responses.Integration;
using Translation.Common.Models.Responses.Integration.IntegrationClient;
using Translation.Tests.SetupHelpers;
using static Translation.Tests.TestHelpers.FakeRequestTestHelper;
using static Translation.Tests.TestHelpers.FakeConstantTestHelper;
using static Translation.Tests.TestHelpers.AssertViewModelTestHelper;
using static Translation.Tests.TestHelpers.AssertResponseTestHelper;

namespace Translation.Tests.Server.Services
{
    [TestFixture]
    public class IntegrationServiceTests : ServiceBaseTests
    {
        public IIntegrationService SystemUnderTest { get; set; }

        [SetUp]
        public void run_before_every_test()
        {
            SystemUnderTest = Container.Resolve<IIntegrationService>();
        }

        [Test]
        public async Task IntegrationService_GetIntegration_Success()
        {
            // arrange
            var request = GetIntegrationReadRequest();
            MockIntegrationRepository.Setup_Select_Returns_OrganizationOneIntegrationOne();

            // act
            var result = await SystemUnderTest.GetIntegration(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Success);
            AssertReturnType<IntegrationReadResponse>(result);
            MockIntegrationRepository.Verify_Select();
        }

        [Test]
        public async Task IntegrationService_GetIntegration_Invalid_OrganizationNotMatch()
        {
            // arrange
            var request = GetIntegrationReadRequest();
            MockIntegrationRepository.Setup_Select_Returns_OrganizationTwoIntegrationOne();

            // act
            var result = await SystemUnderTest.GetIntegration(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Failed);
            AssertReturnType<IntegrationReadResponse>(result);
            MockIntegrationRepository.Verify_Select();
        }

        [Test]
        public async Task IntegrationService_GetIntegration_Invalid_IntegrationNotFound()
        {
            // arrange
            var request = GetIntegrationReadRequest();
            MockIntegrationRepository.Setup_Select_Returns_InvalidIntegration();

            // act
            var result = await SystemUnderTest.GetIntegration(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Invalid, IntegrationNotFound);
            AssertReturnType<IntegrationReadResponse>(result);
            MockIntegrationRepository.Verify_Select();
        }

        [Test]
        public async Task IntegrationService_GetIntegrations_Success_SelectAfter()
        {
            // arrange
            var request = GetIntegrationReadListRequestForSelectAfter();
            MockIntegrationRepository.Setup_SelectAfter_Returns_Integrations();
            MockIntegrationRepository.Setup_Count_Returns_Ten();

            // act
            var result = await SystemUnderTest.GetIntegrations(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Success);
            AssertReturnType<IntegrationReadListResponse>(result);
            AssertPagingInfoForSelectAfter(request.PagingInfo, Ten);
            MockIntegrationRepository.Verify_SelectAfter();
            MockIntegrationRepository.Verify_Count();
        }

        [Test]
        public async Task IntegrationService_GetIntegrations_Success_SelectMany()
        {
            // arrange
            var request = GetIntegrationReadListRequestForSelectMany();
            MockIntegrationRepository.Setup_SelectMany_Returns_Integrations();
            MockIntegrationRepository.Setup_Count_Returns_Ten();

            // act
            var result = await SystemUnderTest.GetIntegrations(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Success);
            AssertReturnType<IntegrationReadListResponse>(result);
            AssertPagingInfoForSelectMany(request.PagingInfo, Ten);
            MockIntegrationRepository.Verify_SelectMany();
            MockIntegrationRepository.Verify_Count();
        }

        [Test]
        public async Task IntegrationService_GetIntegrationRevisions_Success()
        {
            //arrange
            var request = GetIntegrationRevisionReadListRequest();
            MockIntegrationRepository.Setup_Select_Returns_OrganizationOneIntegrationOne();
            MockIntegrationRepository.Setup_SelectRevisions_Returns_OrganizationOneIntegrationOneRevisions();

            //act
            var result = await SystemUnderTest.GetIntegrationRevisions(request);

            //assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Success);
            AssertReturnType<IntegrationRevisionReadListResponse>(result);
            MockIntegrationRepository.Verify_Select();
            MockIntegrationRepository.Verify_SelectRevisions();
        }

        [Test]
        public async Task IntegrationService_GetIntegrationRevisions_Invalid_IntegrationNotFound()
        {
            // arrange
            var request = GetIntegrationRevisionReadListRequest();
            MockIntegrationRepository.Setup_Select_Returns_InvalidIntegration();

            // act
            var result = await SystemUnderTest.GetIntegrationRevisions(request);

            // assert

            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Invalid, IntegrationNotFound);
            AssertReturnType<IntegrationRevisionReadListResponse>(result);
            MockIntegrationRepository.Verify_Select();
        }

        [Test]
        public async Task IntegrationService_CreateIntegration_Success()
        {
            //arrange
            var request = GetIntegrationCreateRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneAdminUserOne();
            MockOrganizationRepository.Setup_Any_Returns_False();
            MockIntegrationRepository.Setup_Any_Returns_False();

            //act
            var result = await SystemUnderTest.CreateIntegration(request);

            //assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Success);
            AssertReturnType<IntegrationCreateResponse>(result);
            MockUserRepository.Verify_SelectById();
            MockOrganizationRepository.Verify_Any();
            MockIntegrationRepository.Verify_Any();
        }

        [Test]
        public async Task IntegrationService_CreateIntegration_Failed_NameMustBeUnique()
        {
            //arrange
            var request = GetIntegrationCreateRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneAdminUserOne();
            MockOrganizationRepository.Setup_Any_Returns_False();
            MockIntegrationRepository.Setup_Any_Returns_True();

            //act
            var result = await SystemUnderTest.CreateIntegration(request);

            //assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Failed, IntegrationNameMustBeUnique);
            AssertReturnType<IntegrationCreateResponse>(result);
            MockUserRepository.Verify_SelectById();
            MockOrganizationRepository.Verify_Any();
            MockIntegrationRepository.Verify_Any();
        }

        [Test]
        public async Task IntegrationService_CreateIntegration_Invalid_CurrentUserNotAdmin()
        {
            //arrange
            var request = GetIntegrationCreateRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneUserOne();

            //act
            var result = await SystemUnderTest.CreateIntegration(request);

            //assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Invalid);
            AssertReturnType<IntegrationCreateResponse>(result);
            MockUserRepository.Verify_SelectById();
        }

        [Test]
        public async Task IntegrationService_CreateIntegration_Invalid_OrganizationNotActive()
        {
            //arrange
            var request = GetIntegrationCreateRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneAdminUserOne();
            MockOrganizationRepository.Setup_Any_Returns_True();

            //act
            var result = await SystemUnderTest.CreateIntegration(request);

            //assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Invalid, OrganizationNotActive);
            AssertReturnType<IntegrationCreateResponse>(result);
            MockUserRepository.Verify_SelectById();
            MockOrganizationRepository.Verify_Any();

        }

        [Test]
        public async Task IntegrationService_EditIntegration_Success()
        {
            // arrange
            var request = GetIntegrationEditRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneAdminUserOne();
            MockOrganizationRepository.Setup_Any_Returns_False();
            MockIntegrationRepository.Setup_Select_Returns_OrganizationOneIntegrationOne();
            MockIntegrationRepository.Setup_Any_Returns_False();
            MockIntegrationRepository.Setup_Update_Returns_True();

            // act
            var result = await SystemUnderTest.EditIntegration(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Success);
            AssertReturnType<IntegrationEditResponse>(result);
            MockUserRepository.Verify_SelectById();
            MockOrganizationRepository.Verify_Any();
            MockIntegrationRepository.Verify_Select();
            MockIntegrationRepository.Verify_Any();
            MockIntegrationRepository.Verify_Update();
        }

        [Test]
        public async Task IntegrationService_EditIntegration_Failed_NameMustBeUnique()
        {
            // arrange
            var request = GetIntegrationEditRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneAdminUserOne();
            MockOrganizationRepository.Setup_Any_Returns_False();
            MockIntegrationRepository.Setup_Select_Returns_OrganizationOneIntegrationOne();
            MockIntegrationRepository.Setup_Any_Returns_True();

            // act
            var result = await SystemUnderTest.EditIntegration(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Failed, IntegrationNameMustBeUnique);
            AssertReturnType<IntegrationEditResponse>(result);
            MockUserRepository.Verify_SelectById();
            MockOrganizationRepository.Verify_Any();
            MockIntegrationRepository.Verify_Select();
            MockIntegrationRepository.Verify_Any();
        }

        [Test]
        public async Task IntegrationService_EditIntegration_Invalid_CurrentUserNotAdmin()
        {
            // arrange
            var request = GetIntegrationEditRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneUserOne();

            // act
            var result = await SystemUnderTest.EditIntegration(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Invalid);
            AssertReturnType<IntegrationEditResponse>(result);
            MockUserRepository.Verify_SelectById();
        }

        [Test]
        public async Task IntegrationService_EditIntegration_Invalid_OrganizationNotActive()
        {
            // arrange
            var request = GetIntegrationEditRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneAdminUserOne();
            MockOrganizationRepository.Setup_Any_Returns_True();

            // act
            var result = await SystemUnderTest.EditIntegration(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Invalid, OrganizationNotActive);
            AssertReturnType<IntegrationEditResponse>(result);
            MockUserRepository.Verify_SelectById();
            MockOrganizationRepository.Verify_Any();
        }

        [Test]
        public async Task IntegrationService_EditIntegration_Invalid_IntegrationNotFound()
        {
            // arrange
            var request = GetIntegrationEditRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneAdminUserOne();
            MockOrganizationRepository.Setup_Any_Returns_False();
            MockIntegrationRepository.Setup_Select_Returns_OrganizationOneIntegrationOneNotExist();


            // act
            var result = await SystemUnderTest.EditIntegration(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Invalid, IntegrationNotFound);
            AssertReturnType<IntegrationEditResponse>(result);
            MockUserRepository.Verify_SelectById();
            MockOrganizationRepository.Verify_Any();
            MockIntegrationRepository.Verify_Select();
        }

        [Test]
        public async Task IntegrationService_EditIntegration_Invalid_OrganizationNotMatch()
        {
            // arrange
            var request = GetIntegrationEditRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneAdminUserOne();
            MockOrganizationRepository.Setup_Any_Returns_False();
            MockIntegrationRepository.Setup_Select_Returns_OrganizationTwoIntegrationOne();

            // act
            var result = await SystemUnderTest.EditIntegration(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Invalid);
            AssertReturnType<IntegrationEditResponse>(result);
            MockUserRepository.Verify_SelectById();
            MockOrganizationRepository.Verify_Any();
            MockIntegrationRepository.Verify_Select();
        }

        [Test]
        public async Task IntegrationService_DeleteIntegration_Success()
        {
            // arrange
            var request = GetIntegrationDeleteRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneAdminUserOne();
            MockOrganizationRepository.Setup_Any_Returns_False();
            MockIntegrationRepository.Setup_Select_Returns_OrganizationOneIntegrationOne();
            MockIntegrationClientRepository.Setup_Any_Returns_False();
            MockIntegrationRepository.Setup_Delete_Success();

            // act
            var result = await SystemUnderTest.DeleteIntegration(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Success);
            AssertReturnType<IntegrationDeleteResponse>(result);
            MockUserRepository.Verify_SelectById();
            MockOrganizationRepository.Verify_Any();
            MockIntegrationRepository.Verify_Select();
            MockIntegrationRepository.Verify_Delete();
        }

        [Test]
        public async Task IntegrationService_DeleteIntegration_Failed()
        {
            // arrange
            var request = GetIntegrationDeleteRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneAdminUserOne();
            MockOrganizationRepository.Setup_Any_Returns_False();
            MockIntegrationRepository.Setup_Select_Returns_OrganizationOneIntegrationOne();
            MockIntegrationRepository.Setup_Delete_Failed();

            // act
            var result = await SystemUnderTest.DeleteIntegration(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Failed);
            AssertReturnType<IntegrationDeleteResponse>(result);
            MockUserRepository.Verify_SelectById();
            MockOrganizationRepository.Verify_Any();
            MockIntegrationRepository.Verify_Select();
            MockIntegrationRepository.Verify_Delete();
        }

        [Test]
        public async Task IntegrationService_DeleteIntegration_Invalid_CurrentUserNotAdmin()
        {
            // arrange
            var request = GetIntegrationDeleteRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneUserOne();

            // act
            var result = await SystemUnderTest.DeleteIntegration(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Invalid);
            AssertReturnType<IntegrationDeleteResponse>(result);
            MockUserRepository.Verify_SelectById();
        }

        [Test]
        public async Task IntegrationService_DeleteIntegration_Invalid_OrganizationNotActive()
        {
            // arrange
            var request = GetIntegrationDeleteRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneAdminUserOne();
            MockOrganizationRepository.Setup_Any_Returns_True();

            // act
            var result = await SystemUnderTest.DeleteIntegration(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Invalid, OrganizationNotActive);
            AssertReturnType<IntegrationDeleteResponse>(result);
            MockUserRepository.Verify_SelectById();
            MockOrganizationRepository.Verify_Any();

        }

        [Test]
        public async Task IntegrationService_DeleteIntegration_Invalid_IntegrationNotFound()
        {
            // arrange
            var request = GetIntegrationDeleteRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneAdminUserOne();
            MockOrganizationRepository.Setup_Any_Returns_False();
            // MockIntegrationRepository.Setup_Select_Returns_InvalidIntegration();
            MockIntegrationRepository.Setup_Select_Returns_OrganizationOneIntegrationOneNotExist();

            // act
            var result = await SystemUnderTest.DeleteIntegration(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Invalid, IntegrationNotFound);
            AssertReturnType<IntegrationDeleteResponse>(result);
            MockUserRepository.Verify_SelectById();
            MockOrganizationRepository.Verify_Any();
            MockIntegrationRepository.Verify_Select();
        }

        [Test]
        public async Task IntegrationService_DeleteIntegration_Invalid_OrganizationNotMatch()
        {
            // arrange
            var request = GetIntegrationDeleteRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneAdminUserOne();
            MockOrganizationRepository.Setup_Any_Returns_False();
            MockIntegrationRepository.Setup_Select_Returns_OrganizationTwoIntegrationOne();

            // act
            var result = await SystemUnderTest.DeleteIntegration(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Invalid);
            AssertReturnType<IntegrationDeleteResponse>(result);
            MockUserRepository.Verify_SelectById();
            MockOrganizationRepository.Verify_Any();
            MockIntegrationRepository.Verify_Select();
        }

        [Test]
        public async Task IntegrationService_DeleteIntegration_Invalid_HasChildren()
        {
            // arrange
            var request = GetIntegrationDeleteRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneAdminUserOne();
            MockOrganizationRepository.Setup_Any_Returns_False();
            MockIntegrationRepository.Setup_Select_Returns_OrganizationOneIntegrationOne();
            MockIntegrationClientRepository.Setup_Any_Returns_True();
            // act
            var result = await SystemUnderTest.DeleteIntegration(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Invalid, IntegrationHasChildren);
            AssertReturnType<IntegrationDeleteResponse>(result);
            MockUserRepository.Verify_SelectById();
            MockOrganizationRepository.Verify_Any();
            MockIntegrationRepository.Verify_Select();
            MockIntegrationClientRepository.Verify_Any();
        }

        [Test]
        public async Task IntegrationService_ChangeActivationForIntegration_Success()
        {
            // arrange
            var request = GetIntegrationChangeActivationRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneAdminUserOne();
            MockOrganizationRepository.Setup_Any_Returns_False();
            MockIntegrationRepository.Setup_Select_Returns_OrganizationOneIntegrationOne();
            MockIntegrationRepository.Setup_Update_Success();

            // act
            var result = await SystemUnderTest.ChangeActivationForIntegration(request);

            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Success);
            AssertReturnType<IntegrationChangeActivationResponse>(result);
            MockUserRepository.Verify_SelectById();
            MockIntegrationRepository.Verify_Select();
            MockIntegrationRepository.Verify_Update();
            MockOrganizationRepository.Verify_Any();
        }

        [Test]
        public async Task IntegrationService_ChangeActivationForIntegration_Failed()
        {
            // arrange
            var request = GetIntegrationChangeActivationRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneAdminUserOne();
            MockOrganizationRepository.Setup_Any_Returns_False();
            MockIntegrationRepository.Setup_Select_Returns_OrganizationOneIntegrationOne();
            MockIntegrationRepository.Setup_Update_Failed();

            // act
            var result = await SystemUnderTest.ChangeActivationForIntegration(request);

            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Failed);
            AssertReturnType<IntegrationChangeActivationResponse>(result);
            MockUserRepository.Verify_SelectById();
            MockOrganizationRepository.Verify_Any();
            MockIntegrationRepository.Verify_Select();
            MockIntegrationRepository.Verify_Update();
        }

        [Test]
        public async Task IntegrationService_ChangeActivationForIntegration_Invalid_CurrentUserNotAdmin()
        {
            // arrange
            var request = GetIntegrationChangeActivationRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneUserOne();

            // act
            var result = await SystemUnderTest.ChangeActivationForIntegration(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Invalid);
            AssertReturnType<IntegrationChangeActivationResponse>(result);
            MockUserRepository.Verify_SelectById();

        }

        [Test]
        public async Task IntegrationService_ChangeActivationForIntegration_Invalid_OrganizationNotActive()
        {
            // arrange
            var request = GetIntegrationChangeActivationRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneAdminUserOne();
            MockOrganizationRepository.Setup_Any_Returns_True();

            // act
            var result = await SystemUnderTest.ChangeActivationForIntegration(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Invalid, OrganizationNotActive);
            AssertReturnType<IntegrationChangeActivationResponse>(result);
            MockUserRepository.Verify_SelectById();
            MockOrganizationRepository.Verify_Any();
        }

        [Test]
        public async Task IntegrationService_ChangeActivationForIntegration_Invalid_IntegrationNotFound()
        {
            // arrange
            var request = GetIntegrationChangeActivationRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneAdminUserOne();
            MockIntegrationRepository.Setup_Select_Returns_InvalidIntegration();

            // act
            var result = await SystemUnderTest.ChangeActivationForIntegration(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Invalid, IntegrationNotFound);
            AssertReturnType<IntegrationChangeActivationResponse>(result);
            MockUserRepository.Verify_SelectById();
            MockIntegrationRepository.Verify_Select();
        }

        [Test]
        public async Task IntegrationService_ChangeActivationForIntegration_Invalid_OrganizationNotMatch()
        {
            // arrange
            var request = GetIntegrationChangeActivationRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneAdminUserOne();
            MockOrganizationRepository.Setup_Any_Returns_False();
            MockIntegrationRepository.Setup_Select_Returns_OrganizationTwoIntegrationOne();

            // act
            var result = await SystemUnderTest.ChangeActivationForIntegration(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Invalid);
            AssertReturnType<IntegrationChangeActivationResponse>(result);
            MockUserRepository.Verify_SelectById();
            MockIntegrationRepository.Verify_Select();
            MockOrganizationRepository.Verify_Any();
        }

        [Test]
        public async Task IntegrationService_RestoreIntegration_Success()
        {
            // arrange
            var request = GetIntegrationRestoreRequest();
            MockOrganizationRepository.Setup_Any_Returns_False();
            MockIntegrationRepository.Setup_Select_Returns_OrganizationOneIntegrationOne();
            MockIntegrationRepository.Setup_SelectRevisions_Returns_OrganizationOneIntegrationOneRevisions();
            MockIntegrationRepository.Setup_RestoreRevision_Returns_True();

            // act
            var result = await SystemUnderTest.RestoreIntegration(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Success);
            AssertReturnType<IntegrationRestoreResponse>(result);
            MockOrganizationRepository.Verify_Any();
            MockIntegrationRepository.Verify_Select();
            MockIntegrationRepository.Verify_SelectRevisions();
            MockIntegrationRepository.Verify_RestoreRevision();
        }

        [Test]
        public async Task IntegrationService_RestoreIntegration_Failed()
        {
            // arrange
            var request = GetIntegrationRestoreRequest();
            MockOrganizationRepository.Setup_Any_Returns_False();
            MockIntegrationRepository.Setup_Select_Returns_OrganizationOneIntegrationOne();
            MockIntegrationRepository.Setup_SelectRevisions_Returns_OrganizationOneIntegrationOneRevisions();
            MockIntegrationRepository.Setup_RestoreRevision_Returns_False();

            // act
            var result = await SystemUnderTest.RestoreIntegration(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Failed);
            AssertReturnType<IntegrationRestoreResponse>(result);
            MockOrganizationRepository.Verify_Any();
            MockIntegrationRepository.Verify_Select();
            MockIntegrationRepository.Verify_SelectRevisions();
            MockIntegrationRepository.Verify_RestoreRevision();
        }

        [Test]
        public async Task IntegrationService_RestoreIntegration_Invalid_OrganizationNotActive()
        {
            // arrange
            var request = GetIntegrationRestoreRequest();
            MockOrganizationRepository.Setup_Any_Returns_True();

            // act
            var result = await SystemUnderTest.RestoreIntegration(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Invalid, OrganizationNotActive);
            AssertReturnType<IntegrationRestoreResponse>(result);
            MockOrganizationRepository.Verify_Any();
        }

        [Test]
        public async Task IntegrationService_RestoreIntegration_Invalid_IntegrationNotFound()
        {
            // arrange
            var request = GetIntegrationRestoreRequest();
            MockOrganizationRepository.Setup_Any_Returns_False();
            MockIntegrationRepository.Setup_Select_Returns_InvalidIntegration();

            // act
            var result = await SystemUnderTest.RestoreIntegration(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Invalid, IntegrationNotFound);
            AssertReturnType<IntegrationRestoreResponse>(result);
            MockOrganizationRepository.Verify_Any();
            MockIntegrationRepository.Verify_Select();
        }

        [Test]
        public async Task IntegrationService_RestoreIntegration_Invalid_IntegrationRevisionNotFound()
        {
            // arrange
            var request = GetIntegrationRestoreRequest();
            MockIntegrationRepository.Setup_Select_Returns_OrganizationOneIntegrationOne();
            MockIntegrationRepository.Setup_SelectRevisions_Returns_InvalidRevision();

            // act
            var result = await SystemUnderTest.RestoreIntegration(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Invalid, IntegrationRevisionNotFound);
            AssertReturnType<IntegrationRestoreResponse>(result);
            MockUserRepository.Verify_SelectById();
            MockIntegrationRepository.Verify_Select();
            MockIntegrationRepository.Verify_SelectRevisions();
        }

        [Test]
        public async Task IntegrationService_CreateIntegrationClient__Success()
        {
            //arrange
            var request = GetIntegrationClientCreateRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneAdminUserOne();
            MockOrganizationRepository.Setup_Any_Returns_False();
            MockIntegrationRepository.Setup_Select_Returns_OrganizationOneIntegrationOne();

            //act
            var result = await SystemUnderTest.CreateIntegrationClient(request);

            //assert
            result.Status.ShouldBe(ResponseStatus.Success);
            result.ErrorMessages.ShouldNotBeNull();
            result.ErrorMessages.Count.ShouldBe(0);
            AssertReturnType<IntegrationClientCreateResponse>(result);
            MockUserRepository.Verify_SelectById();
            MockOrganizationRepository.Verify_Any();
            MockIntegrationRepository.Verify_Select();

        }

        [Test]
        public async Task IntegrationService_CreateIntegrationClient_Invalid_OrganizationNotExist()
        {
            //arrange
            var request = GetIntegrationClientCreateRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneAdminUserOne();
            MockOrganizationRepository.Setup_Any_Returns_True();

            //act
            var result = await SystemUnderTest.CreateIntegrationClient(request);

            //assert
            result.Status.ShouldBe(ResponseStatus.Invalid);
            result.ErrorMessages.ShouldNotBeNull();
            AssertReturnType<IntegrationClientCreateResponse>(result);
            MockUserRepository.Verify_SelectById();
            MockOrganizationRepository.Verify_Any();

        }

        [Test]
        public async Task IntegrationService_CreateIntegrationClient_Invalid_UserNotAdmin()
        {
            //arrange
            var request = GetIntegrationClientCreateRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneUserOne();

            //act
            var result = await SystemUnderTest.CreateIntegrationClient(request);

            //assert
            result.Status.ShouldBe(ResponseStatus.Invalid);
            result.ErrorMessages.ShouldNotBeNull();
            AssertReturnType<IntegrationClientCreateResponse>(result);
            MockUserRepository.Verify_SelectById();
        }

        [Test]
        public async Task IntegrationService_GetIntegrationClient_Success()
        {
            // arrange
            var request = GetIntegrationClientReadRequest();
            MockIntegrationClientRepository.Setup_Select_Returns_OrganizationOneIntegrationOneIntegrationClientOne();

            // act
            var result = await SystemUnderTest.GetIntegrationClient(request);

            // assert
            result.Status.ShouldBe(ResponseStatus.Success);
            result.ErrorMessages.ShouldNotBeNull();
            result.ErrorMessages.Count.ShouldBe(0);
            AssertReturnType<IntegrationClientReadResponse>(result);
            MockIntegrationClientRepository.Verify_Select();
        }

        [Test]
        public async Task IntegrationService_GetIntegrationClient_InvalidIntegrationEntity()
        {
            // arrange
            var request = GetIntegrationClientReadRequest();
            MockIntegrationClientRepository.Setup_Select_Returns_InvalidIntegration();

            // act
            var result = await SystemUnderTest.GetIntegrationClient(request);

            // assert
            result.ErrorMessages.ShouldNotBeNull();
            AssertReturnType<IntegrationClientReadResponse>(result);
            MockIntegrationClientRepository.Verify_Select();
        }

        [Test]
        public async Task IntegrationService_GetIntegrationClients_Success()
        {
            // arrange
            var request = GetIntegrationClientReadListRequest();
            MockIntegrationRepository.Setup_Select_Returns_OrganizationOneIntegrationOne();

            // act
            var result = await SystemUnderTest.GetIntegrationClients(request);

            // assert
            result.Status.ShouldBe(ResponseStatus.Success);
            result.ErrorMessages.ShouldNotBeNull();
            result.ErrorMessages.Count.ShouldBe(0);
            AssertReturnType<IntegrationClientReadListResponse>(result);
            MockIntegrationRepository.Verify_Select();
        }

        [Test]
        public async Task IntegrationService_GetIntegrationClients_InvalidIntegrationEntity()
        {
            // arrange
            var request = GetIntegrationClientReadListRequest();
            MockIntegrationRepository.Setup_Select_Returns_InvalidIntegration();

            // act
            var result = await SystemUnderTest.GetIntegrationClients(request);

            // assert
            result.ErrorMessages.ShouldNotBeNull();
            AssertReturnType<IntegrationClientReadListResponse>(result);
            MockIntegrationRepository.Verify_Select();
        }

        [Test]
        public async Task IntegrationService_RefreshIntegrationClient_Success()
        {
            // arrange
            var request = GetIntegrationClientRefreshRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneAdminUserOne();
            MockOrganizationRepository.Setup_Any_Returns_False();
            MockIntegrationClientRepository.Setup_Select_Returns_OrganizationOneIntegrationOneIntegrationClientOne();
            MockIntegrationRepository.Setup_SelectById_Returns_OrganizationOneIntegrationOne();

            // act
            var result = await SystemUnderTest.RefreshIntegrationClient(request);

            // assert
            result.Status.ShouldBe(ResponseStatus.Success);
            result.ErrorMessages.ShouldNotBeNull();
            result.ErrorMessages.Count.ShouldBe(0);
            AssertReturnType<IntegrationClientRefreshResponse>(result);
            MockUserRepository.Verify_SelectById();
            MockOrganizationRepository.Verify_Any();
            MockIntegrationClientRepository.Verify_Select();
            MockIntegrationRepository.Verify_SelectById();
        }

        [Test]
        public async Task IntegrationService_RefreshIntegrationClient_InvalidIntegrationEntity()
        {
            // arrange
            var request = GetIntegrationClientRefreshRequest();
            MockIntegrationClientRepository.Setup_Select_Returns_InvalidIntegration();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneAdminUserOne();

            // act
            var result = await SystemUnderTest.RefreshIntegrationClient(request);

            // assert
            result.ErrorMessages.ShouldNotBeNull();
            AssertReturnType<IntegrationClientRefreshResponse>(result);
            MockIntegrationClientRepository.Verify_Select();
            MockUserRepository.Verify_SelectById();
        }

        [Test]
        public async Task IntegrationService_RefreshIntegrationClient_UserNotAdmin()
        {
            // arrange
            var request = GetIntegrationClientRefreshRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneUserOne();

            // act
            var result = await SystemUnderTest.RefreshIntegrationClient(request);

            // assert
            result.Status.ShouldBe(ResponseStatus.Invalid);
            result.ErrorMessages.ShouldNotBeNull();
            AssertReturnType<IntegrationClientRefreshResponse>(result);
            MockUserRepository.Verify_SelectById();
        }

        [Test]
        public async Task IntegrationService_RefreshIntegrationClient_OrganizationIsActive()
        {
            // arrange
            var request = GetIntegrationClientRefreshRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneAdminUserOne();
            MockOrganizationRepository.Setup_Any_Returns_True();

            // act
            var result = await SystemUnderTest.RefreshIntegrationClient(request);

            // assert
            result.Status.ShouldBe(ResponseStatus.Invalid);
            result.ErrorMessages.ShouldNotBeNull();
            AssertReturnType<IntegrationClientRefreshResponse>(result);
            MockUserRepository.Verify_SelectById();
            MockOrganizationRepository.Verify_Any();

        }

        [Test]
        public async Task IntegrationService_RefreshIntegrationClient_IntegrationClientNotOrganizationId()
        {
            // arrange
            var request = GetIntegrationClientRefreshRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneAdminUserOne();
            MockOrganizationRepository.Setup_Any_Returns_False();
            MockIntegrationClientRepository.Setup_Select_Returns_OrganizationTwoIntegrationOneIntegrationClientOne();

            // act
            var result = await SystemUnderTest.RefreshIntegrationClient(request);

            // assert
            result.Status.ShouldBe(ResponseStatus.Invalid);
            result.ErrorMessages.ShouldNotBeNull();
            AssertReturnType<IntegrationClientRefreshResponse>(result);
            MockUserRepository.Verify_SelectById();
            MockOrganizationRepository.Verify_Any();
            MockIntegrationClientRepository.Verify_Select();

        }

        [Test]
        public async Task IntegrationService_RefreshIntegrationClient_IntegrationIsExist()
        {
            // arrange
            var request = GetIntegrationClientRefreshRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneAdminUserOne();
            MockOrganizationRepository.Setup_Any_Returns_False();
            MockIntegrationClientRepository.Setup_Select_Returns_OrganizationOneIntegrationOneIntegrationClientOne();
            MockIntegrationRepository.Setup_SelectById_Returns_OrganizationOneIntegrationOneNotExist();

            // act
            var result = await SystemUnderTest.RefreshIntegrationClient(request);

            // assert
            result.ErrorMessages.ShouldNotBeNull();
            AssertReturnType<IntegrationClientRefreshResponse>(result);
            MockUserRepository.Verify_SelectById();
            MockOrganizationRepository.Verify_Any();
            MockIntegrationClientRepository.Verify_Select();
            MockIntegrationRepository.Verify_SelectById();
        }

        [Test]
        public async Task IntegrationService_RefreshIntegrationClient_IntegrationNotIsActive()
        {
            // arrange
            var request = GetIntegrationClientRefreshRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneAdminUserOne();
            MockOrganizationRepository.Setup_Any_Returns_False();
            MockIntegrationClientRepository.Setup_Select_Returns_OrganizationOneIntegrationOneIntegrationClientOne();
            MockIntegrationRepository.Setup_SelectById_Returns_OrganizationOneIntegrationOneNotActive();
            // act
            var result = await SystemUnderTest.RefreshIntegrationClient(request);

            // assert
            result.Status.ShouldBe(ResponseStatus.Invalid);
            result.ErrorMessages.ShouldNotBeNull();
            AssertReturnType<IntegrationClientRefreshResponse>(result);
            MockUserRepository.Verify_SelectById();
            MockOrganizationRepository.Verify_Any();
            MockIntegrationClientRepository.Verify_Select();
            MockIntegrationRepository.Verify_SelectById();
        }

        [Test]
        public async Task IntegrationService_DeleteIntegrationClient_Success()
        {
            // arrange
            var request = GetIntegrationClientDeleteRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneAdminUserOne();
            MockOrganizationRepository.Setup_Any_Returns_False();
            MockIntegrationClientRepository.Setup_Select_Returns_OrganizationOneIntegrationOneIntegrationClientOne();
            MockIntegrationRepository.Setup_SelectById_Returns_OrganizationOneIntegrationOne();
            MockIntegrationClientRepository.Setup_Delete_Returns_True();

            // act
            var result = await SystemUnderTest.DeleteIntegrationClient(request);

            // assert
            result.Status.ShouldBe(ResponseStatus.Success);
            result.ErrorMessages.ShouldNotBeNull();
            result.ErrorMessages.Count.ShouldBe(0);
            AssertReturnType<IntegrationClientDeleteResponse>(result);
            MockUserRepository.Verify_SelectById();
            MockOrganizationRepository.Verify_Any();
            MockIntegrationClientRepository.Verify_Select();
            MockIntegrationRepository.Verify_SelectById();
            MockIntegrationClientRepository.Verify_Delete();
        }

        [Test]
        public async Task IntegrationService_DeleteIntegrationClient_Failed()
        {
            // arrange
            var request = GetIntegrationClientDeleteRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneAdminUserOne();
            MockOrganizationRepository.Setup_Any_Returns_False();
            MockIntegrationClientRepository.Setup_Select_Returns_OrganizationOneIntegrationOneIntegrationClientOne();
            MockIntegrationRepository.Setup_SelectById_Returns_OrganizationOneIntegrationOne();
            MockIntegrationClientRepository.Setup_Delete_Returns_False();

            // act
            var result = await SystemUnderTest.DeleteIntegrationClient(request);

            // assert
            result.Status.ShouldBe(ResponseStatus.Failed);
            result.ErrorMessages.ShouldNotBeNull();
            AssertReturnType<IntegrationClientDeleteResponse>(result);
            MockUserRepository.Verify_SelectById();
            MockOrganizationRepository.Verify_Any();
            MockIntegrationClientRepository.Verify_Select();
            MockIntegrationRepository.Verify_SelectById();
            MockIntegrationClientRepository.Verify_Delete();
        }

        [Test]
        public async Task IntegrationService_DeleteIntegrationClient_InvalidIntegrationEntity()
        {
            // arrange
            var request = GetIntegrationClientDeleteRequest();
            MockIntegrationClientRepository.Setup_Select_Returns_InvalidIntegration();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneAdminUserOne();

            // act
            var result = await SystemUnderTest.DeleteIntegrationClient(request);

            // assert
            result.ErrorMessages.ShouldNotBeNull();
            AssertReturnType<IntegrationClientDeleteResponse>(result);
            MockIntegrationClientRepository.Verify_Select();
            MockUserRepository.Verify_SelectById();
        }

        [Test]
        public async Task IntegrationService_DeleteIntegrationClient_Invalid_UserNotAdmin()
        {
            // arrange
            var request = GetIntegrationClientDeleteRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneUserOne();

            // act
            var result = await SystemUnderTest.DeleteIntegrationClient(request);

            // assert
            result.Status.ShouldBe(ResponseStatus.Invalid);
            result.ErrorMessages.ShouldNotBeNull();
            AssertReturnType<IntegrationClientDeleteResponse>(result);
            MockUserRepository.Verify_SelectById();
        }

        [Test]
        public async Task IntegrationService_DeleteIntegrationClient_OrganizationIsActive()
        {
            // arrange
            var request = GetIntegrationClientDeleteRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneAdminUserOne();
            MockOrganizationRepository.Setup_Any_Returns_True();

            // act
            var result = await SystemUnderTest.DeleteIntegrationClient(request);

            // assert
            result.Status.ShouldBe(ResponseStatus.Invalid);
            result.ErrorMessages.ShouldNotBeNull();
            AssertReturnType<IntegrationClientDeleteResponse>(result);
            MockUserRepository.Verify_SelectById();
            MockOrganizationRepository.Verify_Any();

        }

        [Test]
        public async Task IntegrationService_DeleteIntegrationClient_IntegrationClientNotOrganizationId()
        {
            // arrange
            var request = GetIntegrationClientDeleteRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneAdminUserOne();
            MockOrganizationRepository.Setup_Any_Returns_False();
            MockIntegrationClientRepository.Setup_Select_Returns_OrganizationTwoIntegrationOneIntegrationClientOne();

            // act
            var result = await SystemUnderTest.DeleteIntegrationClient(request);

            // assert
            result.Status.ShouldBe(ResponseStatus.Invalid);
            result.ErrorMessages.ShouldNotBeNull();
            AssertReturnType<IntegrationClientDeleteResponse>(result);
            MockUserRepository.Verify_SelectById();
            MockOrganizationRepository.Verify_Any();
            MockIntegrationClientRepository.Verify_Select();

        }

        [Test]
        public async Task IntegrationService_DeleteIntegrationClient_IntegrationClientIsExist()
        {
            // arrange
            var request = GetIntegrationClientDeleteRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneAdminUserOne();
            MockOrganizationRepository.Setup_Any_Returns_False();
            MockIntegrationClientRepository.Setup_Select_Returns_OrganizationOneIntegrationOneIntegrationClientOneNotExist();

            // act
            var result = await SystemUnderTest.DeleteIntegrationClient(request);

            // assert
            result.ErrorMessages.ShouldNotBeNull();
            AssertReturnType<IntegrationClientDeleteResponse>(result);
            MockUserRepository.Verify_SelectById();
            MockOrganizationRepository.Verify_Any();
            MockIntegrationClientRepository.Verify_Select();
        }

        [Test]
        public async Task IntegrationService_DeleteIntegrationClient_IntegrationIsExit()
        {
            // arrange
            var request = GetIntegrationClientDeleteRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneAdminUserOne();
            MockOrganizationRepository.Setup_Any_Returns_False();
            MockIntegrationClientRepository.Setup_Select_Returns_OrganizationOneIntegrationOneIntegrationClientOne();
            MockIntegrationRepository.Setup_SelectById_Returns_OrganizationOneIntegrationOneNotExist();

            // act
            var result = await SystemUnderTest.DeleteIntegrationClient(request);

            // assert
            result.ErrorMessages.ShouldNotBeNull();
            AssertReturnType<IntegrationClientDeleteResponse>(result);
            MockUserRepository.Verify_SelectById();
            MockOrganizationRepository.Verify_Any();
            MockIntegrationClientRepository.Verify_Select();
            MockIntegrationRepository.Verify_SelectById();
        }

        [Test]
        public async Task IntegrationService_ChangeActivationForIntegrationClient_Success()
        {
            // arrange
            var request = GetIntegrationClientChangeActivationRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneAdminUserOne();
            MockOrganizationRepository.Setup_Any_Returns_False();
            MockIntegrationClientRepository.Setup_Select_Returns_OrganizationOneIntegrationOneIntegrationClientOne();
            MockIntegrationRepository.Setup_SelectById_Returns_OrganizationOneIntegrationOne();
            MockIntegrationClientRepository.Setup_Update_Returns_True();

            // act
            var result = await SystemUnderTest.ChangeActivationForIntegrationClient(request);

            // assert
            result.Status.ShouldBe(ResponseStatus.Success);
            result.ErrorMessages.ShouldNotBeNull();
            result.ErrorMessages.Count.ShouldBe(0);
            AssertReturnType<IntegrationClientChangeActivationResponse>(result);
            MockUserRepository.Verify_SelectById();
            MockOrganizationRepository.Verify_Any();
            MockIntegrationClientRepository.Verify_Select();
            MockIntegrationRepository.Verify_SelectById();
            MockIntegrationClientRepository.Verify_Update();
        }

        [Test]
        public async Task IntegrationService_ChangeActivationForIntegrationClient_Failed()
        {
            // arrange
            var request = GetIntegrationClientChangeActivationRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneAdminUserOne();
            MockOrganizationRepository.Setup_Any_Returns_False();
            MockIntegrationClientRepository.Setup_Select_Returns_OrganizationOneIntegrationOneIntegrationClientOne();
            MockIntegrationRepository.Setup_SelectById_Returns_OrganizationOneIntegrationOne();
            MockIntegrationClientRepository.Setup_Update_Returns_False();

            // act
            var result = await SystemUnderTest.ChangeActivationForIntegrationClient(request);

            // assert
            result.Status.ShouldBe(ResponseStatus.Failed);
            result.ErrorMessages.ShouldNotBeNull();
            AssertReturnType<IntegrationClientChangeActivationResponse>(result);
            MockUserRepository.Verify_SelectById();
            MockOrganizationRepository.Verify_Any();
            MockIntegrationClientRepository.Verify_Select();
            MockIntegrationRepository.Verify_SelectById();
            MockIntegrationClientRepository.Verify_Update();
        }

        [Test]
        public async Task IntegrationService_ChangeActivationForIntegrationClient_InvalidIntegrationEntity()
        {
            // arrange
            var request = GetIntegrationClientChangeActivationRequest();
            MockIntegrationClientRepository.Setup_Select_Returns_InvalidIntegration();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneAdminUserOne();

            // act
            var result = await SystemUnderTest.ChangeActivationForIntegrationClient(request);

            // assert
            result.ErrorMessages.ShouldNotBeNull();
            AssertReturnType<IntegrationClientChangeActivationResponse>(result);
            MockIntegrationClientRepository.Verify_Select();
            MockUserRepository.Verify_SelectById();
        }

        [Test]
        public async Task IntegrationService_ChangeActivationForIntegrationClient_Invalid_UserNotAdmin()
        {
            // arrange
            var request = GetIntegrationClientChangeActivationRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneUserOne();

            // act
            var result = await SystemUnderTest.ChangeActivationForIntegrationClient(request);

            // assert
            result.Status.ShouldBe(ResponseStatus.Invalid);
            result.ErrorMessages.ShouldNotBeNull();
            AssertReturnType<IntegrationClientChangeActivationResponse>(result);
            MockUserRepository.Verify_SelectById();
        }

        [Test]
        public async Task IntegrationService_ChangeActivationForIntegrationClient_OrganizationIsActive()
        {
            // arrange
            var request = GetIntegrationClientChangeActivationRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneAdminUserOne();
            MockOrganizationRepository.Setup_Any_Returns_True();

            // act
            var result = await SystemUnderTest.ChangeActivationForIntegrationClient(request);

            // assert
            result.ErrorMessages.ShouldNotBeNull();
            AssertReturnType<IntegrationClientChangeActivationResponse>(result);
            MockUserRepository.Verify_SelectById();
            MockOrganizationRepository.Verify_Any();

        }

        [Test]
        public async Task IntegrationService_ChangeActivationForIntegrationClient_IntegrationClientIsExist()
        {
            // arrange
            var request = GetIntegrationClientChangeActivationRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneAdminUserOne();
            MockOrganizationRepository.Setup_Any_Returns_False();
            MockIntegrationClientRepository.Setup_Select_Returns_OrganizationOneIntegrationOneIntegrationClientOneNotExist();

            // act
            var result = await SystemUnderTest.ChangeActivationForIntegrationClient(request);

            // assert
            result.Status.ShouldBe(ResponseStatus.Invalid);
            result.ErrorMessages.ShouldNotBeNull();
            AssertReturnType<IntegrationClientChangeActivationResponse>(result);
            MockUserRepository.Verify_SelectById();
            MockOrganizationRepository.Verify_Any();
            MockIntegrationClientRepository.Verify_Select();
        }

        [Test]
        public async Task IntegrationService_ChangeActivationForIntegrationClient_IntegrationClientNotOrganizationId()
        {
            // arrange
            var request = GetIntegrationClientChangeActivationRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneAdminUserOne();
            MockOrganizationRepository.Setup_Any_Returns_False();
            MockIntegrationClientRepository.Setup_Select_Returns_OrganizationTwoIntegrationOneIntegrationClientOne();

            // act
            var result = await SystemUnderTest.ChangeActivationForIntegrationClient(request);

            // assert
            result.Status.ShouldBe(ResponseStatus.Invalid);
            result.ErrorMessages.ShouldNotBeNull();
            AssertReturnType<IntegrationClientChangeActivationResponse>(result);
            MockUserRepository.Verify_SelectById();
            MockOrganizationRepository.Verify_Any();
            MockIntegrationClientRepository.Verify_Select();

        }

        [Test]
        public async Task IntegrationService_ChangeActivationForIntegrationClient_IntegrationIsExit()
        {
            // arrange
            var request = GetIntegrationClientChangeActivationRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneAdminUserOne();
            MockOrganizationRepository.Setup_Any_Returns_False();
            MockIntegrationClientRepository.Setup_Select_Returns_OrganizationOneIntegrationOneIntegrationClientOne();
            MockIntegrationRepository.Setup_SelectById_Returns_OrganizationOneIntegrationOneNotExist();

            // act
            var result = await SystemUnderTest.ChangeActivationForIntegrationClient(request);

            // assert
            result.ErrorMessages.ShouldNotBeNull();
            AssertReturnType<IntegrationClientChangeActivationResponse>(result);
            MockUserRepository.Verify_SelectById();
            MockOrganizationRepository.Verify_Any();
            MockIntegrationClientRepository.Verify_Select();
            MockIntegrationRepository.Verify_SelectById();
        }

        [Test]
        public async Task IntegrationService_ChangeActivationForIntegrationClient_IntegrationNotIsActive()
        {
            // arrange
            var request = GetIntegrationClientChangeActivationRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneAdminUserOne();
            MockOrganizationRepository.Setup_Any_Returns_False();
            MockIntegrationClientRepository.Setup_Select_Returns_OrganizationOneIntegrationOneIntegrationClientOne();
            MockIntegrationRepository.Setup_SelectById_Returns_OrganizationOneIntegrationOneNotActive();

            // act
            var result = await SystemUnderTest.ChangeActivationForIntegrationClient(request);

            // assert
            result.ErrorMessages.ShouldNotBeNull();
            AssertReturnType<IntegrationClientChangeActivationResponse>(result);
            MockUserRepository.Verify_SelectById();
            MockOrganizationRepository.Verify_Any();
            MockIntegrationClientRepository.Verify_Select();
            MockIntegrationRepository.Verify_SelectById();
        }
    }
}