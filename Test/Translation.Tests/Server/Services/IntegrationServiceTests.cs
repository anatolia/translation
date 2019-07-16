using System.Linq;
using System.Threading.Tasks;

using NUnit.Framework;
using Shouldly;

using Translation.Common.Contracts;
using Translation.Common.Enumerations;
using Translation.Common.Models.Requests.Integration;
using Translation.Common.Models.Responses.Integration;
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
        public async Task IntegrationService_EditIntegration_OrganizationNotExist()
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
        public async Task IntegrationService_DeleteIntegration__InvalidIntegrationEntity()
        {
            // arrange
            var request = GetIntegrationDeleteRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneAdminUserOne();
            MockIntegrationRepository.Setup_Select_Returns_InvalidIntegration();
            MockOrganizationRepository.Setup_Any_Returns_False();
            // act
            var result = await SystemUnderTest.DeleteIntegration(request);

            // assert
            result.ErrorMessages.ShouldNotBeNull();
            result.Status.ShouldBe(ResponseStatus.InvalidBecauseEntityNotFound);
            AssertReturnType<IntegrationDeleteResponse>(result);
            MockUserRepository.Verify_SelectById();
            MockIntegrationRepository.Verify_Select();
            MockOrganizationRepository.Verify_Any();
        }

        [Test]
        public async Task IntegrationService_DeleteIntegration__OrganizationNotExist()
        {
            // arrange
            var request = GetIntegrationDeleteRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneAdminUserOne();
            MockIntegrationRepository.Setup_Select_Returns_OrganizationOneIntegrationOne();
            MockOrganizationRepository.Setup_Any_Returns_True();

            // act
            var result = await SystemUnderTest.DeleteIntegration(request);

            // assert
            result.ErrorMessages.ShouldNotBeNull();
            result.Status.ShouldBe(ResponseStatus.Invalid);
            AssertReturnType<IntegrationDeleteResponse>(result);
            MockUserRepository.Verify_SelectById();
            MockIntegrationRepository.Verify_Select();
            MockOrganizationRepository.Verify_Any();
        }

        [Test]
        public async Task IntegrationService_DeleteIntegration_Success()
        {
            // arrange
            var request = GetIntegrationDeleteRequest();
            MockOrganizationRepository.Setup_Any_Returns_False();
            MockIntegrationRepository.Setup_Delete_Success();

            // act
            var result = await SystemUnderTest.DeleteIntegration(request);

            // assert
            result.Status.ShouldBe(ResponseStatus.Success);
            result.ErrorMessages.ShouldNotBeNull();
            result.ErrorMessages.Count.ShouldBe(0);
            AssertReturnType<IntegrationDeleteResponse>(result);
            MockOrganizationRepository.Verify_Any();
            MockIntegrationRepository.Verify_Delete();
        }

        [Test]
        public async Task IntegrationService_DeleteIntegration_Failed()
        {
            // arrange
            var request = GetIntegrationDeleteRequest();
            MockOrganizationRepository.Setup_Any_Returns_False();
            MockIntegrationRepository.Setup_Delete_Failed();

            // act
            var result = await SystemUnderTest.DeleteIntegration(request);

            // assert
            result.ErrorMessages.ShouldNotBeNull();
            result.Status.ShouldBe(ResponseStatus.Failed);
            AssertReturnType<IntegrationDeleteResponse>(result);
            MockOrganizationRepository.Verify_Any();
            MockIntegrationRepository.Verify_Delete();
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

    }
}