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
using static Translation.Tests.TestHelpers.AssertViewModelTestHelper;

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
        public async Task IntegrationService_GetIntegrations_Success()
        {
            // arrange
            var request = GetIntegrationReadListRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneAdminUserOne();

            // act
            var result = await SystemUnderTest.GetIntegrations(request);

            // assert
            result.Status.ShouldBe(ResponseStatus.Success);
            result.ErrorMessages.ShouldNotBeNull();
            result.ErrorMessages.Count.ShouldBe(0);
            AssertReturnType<IntegrationReadListResponse>(result);
            MockUserRepository.Verify_SelectById();
        }

        [Test]
        public async Task IntegrationService_GetIntegrationRevisions_InvalidIntegrationEntity()
        {
            // arrange
            var request = GetIntegrationRevisionReadListRequest();
            MockIntegrationRepository.Setup_Select_Returns_InvalidIntegration();

            // act
            var result = await SystemUnderTest.GetIntegrationRevisions(request);

            // assert
            result.ErrorMessages.ShouldNotBeNull();
            result.Status.ShouldBe(ResponseStatus.InvalidBecauseEntityNotFound);
            AssertReturnType<IntegrationRevisionReadListResponse>(result);
            MockIntegrationRepository.Verify_Select();
        }

        [Test]
        public async Task IntegrationService_GetIntegrationRevisions__Success()
        {
            //arrange
            var request = GetIntegrationRevisionReadListRequest();
            MockIntegrationRepository.Setup_Select_Returns_OrganizationOneIntegrationOne();
            MockIntegrationRepository.Setup_SelectRevisions_Returns_OrganizationOneIntegrationOneRevisions();

            //act
            var result = await SystemUnderTest.GetIntegrationRevisions(request);

            //assert
            result.Status.ShouldBe(ResponseStatus.Success);
            result.ErrorMessages.ShouldNotBeNull();
            result.ErrorMessages.Count.ShouldBe(0);
            AssertReturnType<IntegrationRevisionReadListResponse>(result);
            MockIntegrationRepository.Verify_Select();
            MockIntegrationRepository.Verify_SelectRevisions();
        }

        [Test]
        public async Task IntegrationService_GetIntegration_InvalidIntegrationEntity()
        {
            // arrange
            var request = GetIntegrationReadRequest();
            MockIntegrationRepository.Setup_Select_Returns_InvalidIntegration();

            // act
            var result = await SystemUnderTest.GetIntegration(request);

            // assert
            result.ErrorMessages.ShouldNotBeNull();
            result.Status.ShouldBe(ResponseStatus.InvalidBecauseEntityNotFound);
            AssertReturnType<IntegrationReadResponse>(result);
            MockIntegrationRepository.Verify_Select();
        }

        [Test]
        public async Task IntegrationService_CreateIntegration__Success()
        {
            //arrange
            var request = GetIntegrationCreateRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneAdminUserOne();
            MockOrganizationRepository.Setup_Any_Returns_False();
            MockIntegrationRepository.Setup_Any_False();

            //act
            var result = await SystemUnderTest.CreateIntegration(request);

            //assert
            result.Status.ShouldBe(ResponseStatus.Success);
            result.ErrorMessages.ShouldNotBeNull();
            result.ErrorMessages.Count.ShouldBe(0);
            AssertReturnType<IntegrationCreateResponse>(result);
            MockUserRepository.Verify_SelectById();
            MockOrganizationRepository.Verify_Any();
            MockIntegrationRepository.Verify_Any();
        }
        [Test]
        public async Task IntegrationService_CreateIntegration__Failed()
        {
            //arrange
            var request = GetIntegrationCreateRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneAdminUserOne();
            MockOrganizationRepository.Setup_Any_Returns_False();
            MockIntegrationRepository.Setup_Any_True();

            //act
            var result = await SystemUnderTest.CreateIntegration(request);

            //assert
            result.Status.ShouldBe(ResponseStatus.Failed);
            result.ErrorMessages.ShouldNotBeNull();
            AssertReturnType<IntegrationCreateResponse>(result);
            MockUserRepository.Verify_SelectById();
            MockOrganizationRepository.Verify_Any();
            MockIntegrationRepository.Verify_Any();
        }

        [Test]
        public async Task IntegrationService_CreateIntegration_Invalid_OrganizationNotExist()
        {
            //arrange
            var request = GetIntegrationCreateRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneAdminUserOne();
            MockOrganizationRepository.Setup_Any_Returns_True();

            //act
            var result = await SystemUnderTest.CreateIntegration(request);

            //assert
            result.Status.ShouldBe(ResponseStatus.Invalid);
            result.ErrorMessages.ShouldNotBeNull();
            AssertReturnType<IntegrationCreateResponse>(result);
            MockUserRepository.Verify_SelectById();
            MockOrganizationRepository.Verify_Any();

        }

        [Test]
        public async Task IntegrationService_CreateIntegration_Invalid_UserNotAdmin()
        {
            //arrange
            var request = GetIntegrationCreateRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneUserOne();

            //act
            var result = await SystemUnderTest.CreateIntegration(request);

            //assert
            result.Status.ShouldBe(ResponseStatus.Invalid);
            result.ErrorMessages.ShouldNotBeNull();
            AssertReturnType<IntegrationCreateResponse>(result);
            MockUserRepository.Verify_SelectById();
        }

        [Test]
        public async Task IntegrationService_EditIntegration_InvalidIntegrationEntity()
        {
            // arrange
            var request = GetIntegrationEditRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneAdminUserOne();
            MockIntegrationRepository.Setup_Select_Returns_InvalidIntegration();
            MockOrganizationRepository.Setup_Any_Returns_False();

            // act
            var result = await SystemUnderTest.EditIntegration(request);

            // assert
            result.ErrorMessages.ShouldNotBeNull();
            result.Status.ShouldBe(ResponseStatus.InvalidBecauseEntityNotFound);
            AssertReturnType<IntegrationEditResponse>(result);
            MockUserRepository.Verify_SelectById();
            MockIntegrationRepository.Verify_Select();
            MockOrganizationRepository.Verify_Any();
        }

        [Test]
        public async Task IntegrationService_EditIntegration_Invalid_OrganizationNotExist()
        {
            // arrange
            var request = GetIntegrationEditRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneAdminUserOne();
            MockOrganizationRepository.Setup_Any_Returns_True();

            // act
            var result = await SystemUnderTest.EditIntegration(request);

            // assert
            result.ErrorMessages.ShouldNotBeNull();
            result.Status.ShouldBe(ResponseStatus.Invalid);
            AssertReturnType<IntegrationEditResponse>(result);
            MockUserRepository.Verify_SelectById();
            MockOrganizationRepository.Verify_Any();
        }

        [Test]
        public async Task IntegrationService_EditIntegration_Failed_IntegrationAlreadyExist()
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
            result.ErrorMessages.ShouldNotBeNull();
            result.Status.ShouldBe(ResponseStatus.Failed);
            result.ErrorMessages.Any(x => x == "integration_name_must_be_unique").ShouldBeTrue();
            AssertReturnType<IntegrationEditResponse>(result);
            MockUserRepository.Verify_SelectById();
            MockOrganizationRepository.Verify_Any();
            MockIntegrationRepository.Verify_Select();
            MockIntegrationRepository.Verify_Any();
        }

        [Test]
        public async Task IntegrationService_EditIntegration_Success()
        {
            // arrange
            var request = GetIntegrationEditRequest();
            MockOrganizationRepository.Setup_Any_Returns_False();
            MockIntegrationRepository.Setup_Update_Success();
            MockIntegrationRepository.Setup_Any_Returns_False();
            MockIntegrationRepository.Setup_Select_Returns_OrganizationOneIntegrationOne();

            // act
            var result = await SystemUnderTest.EditIntegration(request);

            // assert
            result.Status.ShouldBe(ResponseStatus.Success);
            result.ErrorMessages.ShouldNotBeNull();
            result.ErrorMessages.Count.ShouldBe(0);
            AssertReturnType<IntegrationEditResponse>(result);
            MockOrganizationRepository.Verify_Any();
            MockIntegrationRepository.Verify_Update();
            MockIntegrationRepository.Verify_Any();
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
            MockIntegrationRepository.Setup_Delete_Success();

            // act
            var result = await SystemUnderTest.DeleteIntegration(request);

            // assert
            result.Status.ShouldBe(ResponseStatus.Success);
            result.ErrorMessages.ShouldNotBeNull();
            result.ErrorMessages.Count.ShouldBe(0);
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
            result.Status.ShouldBe(ResponseStatus.Failed);
            result.ErrorMessages.ShouldNotBeNull();
            AssertReturnType<IntegrationDeleteResponse>(result);
            MockUserRepository.Verify_SelectById();
            MockOrganizationRepository.Verify_Any();
            MockIntegrationRepository.Verify_Select();
            MockIntegrationRepository.Verify_Delete();
        }

        [Test]
        public async Task IntegrationService_DeleteIntegration_Invalid_UserNotAdmin()
        {
            // arrange
            var request = GetIntegrationDeleteRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneUserOne();

            // act
            var result = await SystemUnderTest.DeleteIntegration(request);

            // assert
            result.Status.ShouldBe(ResponseStatus.Invalid);
            result.ErrorMessages.ShouldNotBeNull();
            AssertReturnType<IntegrationDeleteResponse>(result);
            MockUserRepository.Verify_SelectById();
        }

        [Test]
        public async Task IntegrationService_DeleteIntegration_OrganizationIsActive()
        {
            // arrange
            var request = GetIntegrationDeleteRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneAdminUserOne();
            MockOrganizationRepository.Setup_Any_Returns_True();

            // act
            var result = await SystemUnderTest.DeleteIntegration(request);

            // assert
            result.Status.ShouldBe(ResponseStatus.Invalid);
            result.ErrorMessages.ShouldNotBeNull();
            AssertReturnType<IntegrationDeleteResponse>(result);
            MockUserRepository.Verify_SelectById();
            MockOrganizationRepository.Verify_Any();

        }

        [Test]
        public async Task IntegrationService_DeleteIntegration_InvalidIntegrationEntity()
        {
            // arrange
            var request = GetIntegrationDeleteRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneAdminUserOne();
            MockOrganizationRepository.Setup_Any_Returns_False();
            MockIntegrationRepository.Setup_Select_Returns_InvalidIntegration();


            // act
            var result = await SystemUnderTest.DeleteIntegration(request);

            // assert
            result.ErrorMessages.ShouldNotBeNull();
            result.Status.ShouldBe(ResponseStatus.InvalidBecauseEntityNotFound);
            AssertReturnType<IntegrationDeleteResponse>(result);
            MockIntegrationRepository.Verify_Select();
            MockUserRepository.Verify_SelectById();
            MockOrganizationRepository.Verify_Any();
        }

        [Test]
        public async Task IntegrationService_DeleteIntegration_NotOrganizationId()
        {
            // arrange
            var request = GetIntegrationDeleteRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneAdminUserOne();
            MockOrganizationRepository.Setup_Any_Returns_False();
            MockIntegrationRepository.Setup_Select_Returns_OrganizationTwoIntegrationOne();

            // act
            var result = await SystemUnderTest.DeleteIntegration(request);

            // assert
            result.Status.ShouldBe(ResponseStatus.Invalid);
            result.ErrorMessages.ShouldNotBeNull();
            AssertReturnType<IntegrationDeleteResponse>(result);
            MockUserRepository.Verify_SelectById();
            MockOrganizationRepository.Verify_Any();
            MockIntegrationRepository.Verify_Select();

        }

        [Test]
        public async Task IntegrationService_DeleteIntegration_IntegrationIsExit()
        {
            // arrange
            var request = GetIntegrationDeleteRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneAdminUserOne();
            MockOrganizationRepository.Setup_Any_Returns_False();
            MockIntegrationClientRepository.Setup_Select_Returns_OrganizationOneIntegrationOneIntegrationClientOne();
            MockIntegrationRepository.Setup_SelectById_Returns_OrganizationOneIntegrationOneNotExist();

            // act
            var result = await SystemUnderTest.DeleteIntegration(request);

            // assert
            result.Status.ShouldBe(ResponseStatus.InvalidBecauseEntityNotFound);
            result.ErrorMessages.ShouldNotBeNull();
            AssertReturnType<IntegrationDeleteResponse>(result);
            MockUserRepository.Verify_SelectById();
            MockOrganizationRepository.Verify_Any();
            MockIntegrationClientRepository.Verify_Select();
            MockIntegrationRepository.Verify_SelectById();
        }

        [Test]
        public async Task IntegrationService_ChangeActivationForIntegration_InvalidIntegrationEntity()
        {
            // arrange
            var request = GetIntegrationChangeActivationRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneAdminUserOne();
            MockIntegrationRepository.Setup_Select_Returns_InvalidIntegration();

            // act
            var result = await SystemUnderTest.ChangeActivationForIntegration(request);

            // assert
            result.ErrorMessages.ShouldNotBeNull();
            result.Status.ShouldBe(ResponseStatus.InvalidBecauseEntityNotFound);
            AssertReturnType<IntegrationChangeActivationResponse>(result);
            MockUserRepository.Verify_SelectById();
            MockIntegrationRepository.Verify_Select();
        }

        [Test]
        public async Task IntegrationService_ChangeActivationForIntegration_OrganizationAlreadyExist()
        {
            // arrange
            var request = GetIntegrationChangeActivationRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneAdminUserOne();
            MockIntegrationRepository.Setup_Select_Returns_OrganizationOneIntegrationOne();
            MockOrganizationRepository.Setup_Any_Returns_True();

            // act
            var result = await SystemUnderTest.ChangeActivationForIntegration(request);

            // assert
            result.ErrorMessages.ShouldNotBeNull();
            result.Status.ShouldBe(ResponseStatus.Invalid);
            AssertReturnType<IntegrationChangeActivationResponse>(result);
            MockUserRepository.Verify_SelectById();
            MockIntegrationRepository.Verify_Select();
            MockOrganizationRepository.Verify_Any();
        }

        [Test]
        public async Task IntegrationService_ChangeActivationForIntegration_Success()
        {
            // arrange
            var request = GetIntegrationChangeActivationRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneAdminUserOne();
            MockIntegrationRepository.Setup_Select_Returns_OrganizationOneIntegrationOne();
            MockIntegrationRepository.Setup_Update_Success();
            MockOrganizationRepository.Setup_Any_Returns_False();

            // act
            var result = await SystemUnderTest.ChangeActivationForIntegration(request);

            // assert
            result.Status.ShouldBe(ResponseStatus.Success);
            result.ErrorMessages.ShouldNotBeNull();
            result.ErrorMessages.Count.ShouldBe(0);
            AssertReturnType<IntegrationChangeActivationResponse>(result);
            MockUserRepository.Verify_SelectById();
            MockIntegrationRepository.Verify_Select();
            MockIntegrationRepository.Verify_Update();
            MockOrganizationRepository.Verify_Any();
        }

        [Test]
        public async Task IntegrationService_RestoreIntegration_InvalidIntegrationEntity()
        {
            // arrange
            var request = GetIntegrationRestoreRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneUserOne();
            MockIntegrationRepository.Setup_Select_Returns_InvalidIntegration();

            // act
            var result = await SystemUnderTest.RestoreIntegration(request);

            // assert
            result.ErrorMessages.ShouldNotBeNull();
            result.Status.ShouldBe(ResponseStatus.InvalidBecauseEntityNotFound);
            result.InfoMessages.Any(x => x == "integration_not_found").ShouldBeTrue();
            AssertReturnType<IntegrationRestoreResponse>(result);
            MockUserRepository.Verify_SelectById();
            MockIntegrationRepository.Verify_Select();
        }

        [Test]
        public async Task IntegrationService_RestoreIntegration_InvalidRevisionEntity()
        {
            // arrange
            var request = GetIntegrationRestoreRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneUserOne();
            MockIntegrationRepository.Setup_Select_Returns_OrganizationOneIntegrationOne();
            MockIntegrationRepository.Setup_SelectRevisions_Returns_InvalidRevision();

            // act
            var result = await SystemUnderTest.RestoreIntegration(request);

            // assert
            result.ErrorMessages.ShouldNotBeNull();
            result.Status.ShouldBe(ResponseStatus.InvalidBecauseEntityNotFound);
            result.InfoMessages.Any(x => x == "revision_not_found").ShouldBeTrue();
            AssertReturnType<IntegrationRestoreResponse>(result);
            MockUserRepository.Verify_SelectById();
            MockIntegrationRepository.Verify_Select();
            MockIntegrationRepository.Verify_SelectRevisions();
        }

        [Test]
        public async Task IntegrationService_RestoreIntegration_Success()
        {
            // arrange
            var request = GetIntegrationRestoreRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneUserOne();
            MockIntegrationRepository.Setup_Select_Returns_OrganizationOneIntegrationOne();
            MockIntegrationRepository.Setup_SelectRevisions_Returns_OrganizationOneIntegrationOneRevisions();
            MockIntegrationRepository.Setup_RestoreRevision_Returns_True();

            // act
            var result = await SystemUnderTest.RestoreIntegration(request);

            // assert
            result.Status.ShouldBe(ResponseStatus.Success);
            result.ErrorMessages.ShouldNotBeNull();
            result.ErrorMessages.Count.ShouldBe(0);
            AssertReturnType<IntegrationRestoreResponse>(result);
            MockUserRepository.Verify_SelectById();
            MockIntegrationRepository.Verify_Select();
            MockIntegrationRepository.Verify_SelectRevisions();
            MockIntegrationRepository.Verify_RestoreRevision();
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
            result.Status.ShouldBe(ResponseStatus.InvalidBecauseEntityNotFound);
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
            result.Status.ShouldBe(ResponseStatus.InvalidBecauseEntityNotFound);
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
            result.Status.ShouldBe(ResponseStatus.InvalidBecauseEntityNotFound);
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
            result.Status.ShouldBe(ResponseStatus.InvalidBecauseEntityNotFound);
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
            result.Status.ShouldBe(ResponseStatus.InvalidBecauseEntityNotFound);
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
            result.Status.ShouldBe(ResponseStatus.InvalidBecauseEntityNotFound);
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
            result.Status.ShouldBe(ResponseStatus.InvalidBecauseEntityNotFound);
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
            result.Status.ShouldBe(ResponseStatus.InvalidBecauseEntityNotFound);
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
            result.Status.ShouldBe(ResponseStatus.InvalidBecauseParentNotActive);
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
            result.Status.ShouldBe(ResponseStatus.InvalidBecauseEntityNotFound);
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
            result.Status.ShouldBe(ResponseStatus.InvalidBecauseParentNotActive);
            result.ErrorMessages.ShouldNotBeNull();
            AssertReturnType<IntegrationClientChangeActivationResponse>(result);
            MockUserRepository.Verify_SelectById();
            MockOrganizationRepository.Verify_Any();
            MockIntegrationClientRepository.Verify_Select();
            MockIntegrationRepository.Verify_SelectById();
        }
    }
}