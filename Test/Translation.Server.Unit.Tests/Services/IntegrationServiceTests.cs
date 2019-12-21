using System.Threading.Tasks;

using Autofac;
using NUnit.Framework;

using Translation.Common.Contracts;
using StandardUtils.Enumerations;
using Translation.Common.Models.Responses.Integration;
using Translation.Common.Models.Responses.Integration.IntegrationClient;
using Translation.Common.Models.Responses.Integration.Token;
using Translation.Common.Models.Responses.Integration.Token.RequestLog;
using Translation.Server.Unit.Tests.RepositorySetupHelpers;

using static Translation.Common.Tests.TestHelpers.FakeRequestTestHelper;
using static Translation.Common.Tests.TestHelpers.FakeConstantTestHelper;
using static Translation.Server.Unit.Tests.TestHelpers.AssertResponseTestHelper;
using static Translation.Server.Unit.Tests.TestHelpers.AssertViewModelTestHelper;

namespace Translation.Server.Unit.Tests.Services
{
    [TestFixture]
    public class IntegrationServiceTests : ServiceBaseTests
    {
        public IIntegrationService SystemUnderTest { get; set; }

        [SetUp]
        public void run_before_every_test()
        {
            Refresh();
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
        public async Task IntegrationService_GetIntegration_Failed_OrganizationNotMatch()
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
        public async Task IntegrationService_GetIntegration_Failed_IntegrationNotFound()
        {
            // arrange
            var request = GetIntegrationReadRequest();
            MockIntegrationRepository.Setup_Select_Returns_OrganizationOneIntegrationOneNotExist();

            // act
            var result = await SystemUnderTest.GetIntegration(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Failed, IntegrationNotFound);
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
        public async Task IntegrationService_GetIntegrationRevisions_Failed_IntegrationNotFound()
        {
            // arrange
            var request = GetIntegrationRevisionReadListRequest();
            MockIntegrationRepository.Setup_Select_Returns_OrganizationOneIntegrationOneNotExist();

            // act
            var result = await SystemUnderTest.GetIntegrationRevisions(request);

            // assert

            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Failed, IntegrationNotFound);
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
        public async Task IntegrationService_CreateIntegration_Invalid_IntegrationNameMustBeUnique()
        {
            //arrange
            var request = GetIntegrationCreateRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneAdminUserOne();
            MockOrganizationRepository.Setup_Any_Returns_False();
            MockIntegrationRepository.Setup_Any_Returns_True();

            //act
            var result = await SystemUnderTest.CreateIntegration(request);

            //assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Invalid, IntegrationNameMustBeUnique);
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
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Invalid, UserNotAdmin);
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
        public async Task IntegrationService_EditIntegration_SameNmeAndDescription_Success()
        {
            // arrange
            var request = GetSameNameAndDescriptionIntegrationEditRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneAdminUserOne();
            MockOrganizationRepository.Setup_Any_Returns_False();
            MockIntegrationRepository.Setup_Select_Returns_OrganizationOneIntegrationOne();

            // act
            var result = await SystemUnderTest.EditIntegration(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Success);
            AssertReturnType<IntegrationEditResponse>(result);
            MockUserRepository.Verify_SelectById();
            MockOrganizationRepository.Verify_Any();
            MockIntegrationRepository.Verify_Select();
        }

        [Test]
        public async Task IntegrationService_EditIntegration_Invalid_NameMustBeUnique()
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
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Invalid, IntegrationNameMustBeUnique);
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
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Invalid, UserNotAdmin);
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
        public async Task IntegrationService_EditIntegration_Failed_IntegrationNotFound()
        {
            // arrange
            var request = GetIntegrationEditRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneAdminUserOne();
            MockOrganizationRepository.Setup_Any_Returns_False();
            MockIntegrationRepository.Setup_Select_Returns_OrganizationOneIntegrationOneNotExist();


            // act
            var result = await SystemUnderTest.EditIntegration(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Failed, IntegrationNotFound);
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
        public async Task IntegrationService_EditIntegration_Failed()
        {
            // arrange
            var request = GetIntegrationEditRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneAdminUserOne();
            MockOrganizationRepository.Setup_Any_Returns_False();
            MockIntegrationRepository.Setup_Select_Returns_OrganizationOneIntegrationOne();
            MockIntegrationRepository.Setup_Any_Returns_False();
            MockIntegrationRepository.Setup_Update_Returns_False();

            // act
            var result = await SystemUnderTest.EditIntegration(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Failed);
            AssertReturnType<IntegrationEditResponse>(result);
            MockUserRepository.Verify_SelectById();
            MockOrganizationRepository.Verify_Any();
            MockIntegrationRepository.Verify_Select();
            MockIntegrationRepository.Verify_Any();
            MockIntegrationRepository.Verify_Update();
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
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Invalid, UserNotAdmin);
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
        public async Task IntegrationService_DeleteIntegration_Failed_IntegrationNotFound()
        {
            // arrange
            var request = GetIntegrationDeleteRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneAdminUserOne();
            MockOrganizationRepository.Setup_Any_Returns_False();
            MockIntegrationRepository.Setup_Select_Returns_OrganizationOneIntegrationOneNotExist();

            // act
            var result = await SystemUnderTest.DeleteIntegration(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Failed, IntegrationNotFound);
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
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Invalid, UserNotAdmin);
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
        public async Task IntegrationService_ChangeActivationForIntegration_Failed_IntegrationNotFound()
        {
            // arrange
            var request = GetIntegrationChangeActivationRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneAdminUserOne();
            MockIntegrationRepository.Setup_Select_Returns_OrganizationOneIntegrationOneNotExist();

            // act
            var result = await SystemUnderTest.ChangeActivationForIntegration(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Failed, IntegrationNotFound);
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
        public async Task IntegrationService_RestoreIntegration_Failed_IntegrationNotFound()
        {
            // arrange
            var request = GetIntegrationRestoreRequest();
            MockOrganizationRepository.Setup_Any_Returns_False();
            MockIntegrationRepository.Setup_Select_Returns_OrganizationOneIntegrationOneNotExist();

            // act
            var result = await SystemUnderTest.RestoreIntegration(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Failed, IntegrationNotFound);
            AssertReturnType<IntegrationRestoreResponse>(result);
            MockOrganizationRepository.Verify_Any();
            MockIntegrationRepository.Verify_Select();
        }

        [Test]
        public async Task IntegrationService_RestoreIntegration_Failed_IntegrationRevisionNotFound()
        {
            // arrange
            var request = GetIntegrationRestoreRequest();
            MockIntegrationRepository.Setup_Select_Returns_OrganizationOneIntegrationOne();
            MockIntegrationRepository.Setup_SelectRevisions_Returns_RevisionTwo();

            // act
            var result = await SystemUnderTest.RestoreIntegration(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Failed, IntegrationRevisionNotFound);
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
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Success);
            AssertReturnType<IntegrationClientCreateResponse>(result);
            MockUserRepository.Verify_SelectById();
            MockOrganizationRepository.Verify_Any();
            MockIntegrationRepository.Verify_Select();

        }

        [Test]
        public async Task IntegrationService_CreateIntegrationClient_Invalid_CurrentUserNotAdmin()
        {
            //arrange
            var request = GetIntegrationClientCreateRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneUserOne();

            //act
            var result = await SystemUnderTest.CreateIntegrationClient(request);

            //assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Invalid, UserNotAdmin);
            AssertReturnType<IntegrationClientCreateResponse>(result);
            MockUserRepository.Verify_SelectById();
        }

        [Test]
        public async Task IntegrationService_CreateIntegrationClient_Invalid_OrganizationNotActive()
        {
            //arrange
            var request = GetIntegrationClientCreateRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneAdminUserOne();
            MockOrganizationRepository.Setup_Any_Returns_True();

            //act
            var result = await SystemUnderTest.CreateIntegrationClient(request);

            //assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Invalid, OrganizationNotActive);
            AssertReturnType<IntegrationClientCreateResponse>(result);
            MockUserRepository.Verify_SelectById();
            MockOrganizationRepository.Verify_Any();
        }

        [Test]
        public async Task IntegrationService_CreateIntegrationClient_Failed_IntegrationNotFound()
        {
            //arrange
            var request = GetIntegrationClientCreateRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneAdminUserOne();
            MockOrganizationRepository.Setup_Any_Returns_False();
            MockIntegrationRepository.Setup_Select_Returns_OrganizationOneIntegrationOneNotExist();
            //act
            var result = await SystemUnderTest.CreateIntegrationClient(request);

            //assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Failed, IntegrationNotFound);
            AssertReturnType<IntegrationClientCreateResponse>(result);
            MockUserRepository.Verify_SelectById();
            MockOrganizationRepository.Verify_Any();
            MockIntegrationRepository.Verify_Select();
        }

        [Test]
        public async Task IntegrationService_CreateIntegrationClient_Invalid_OrganizationNotMatch()
        {
            //arrange
            var request = GetIntegrationClientCreateRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneAdminUserOne();
            MockOrganizationRepository.Setup_Any_Returns_False();
            MockIntegrationRepository.Setup_Select_Returns_OrganizationTwoIntegrationOne();
            //act
            var result = await SystemUnderTest.CreateIntegrationClient(request);

            //assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Invalid);
            AssertReturnType<IntegrationClientCreateResponse>(result);
            MockUserRepository.Verify_SelectById();
            MockOrganizationRepository.Verify_Any();
            MockIntegrationRepository.Verify_Select();
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
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Success);
            AssertReturnType<IntegrationClientReadResponse>(result);
            MockIntegrationClientRepository.Verify_Select();
        }

        [Test]
        public async Task IntegrationService_GetIntegrationClient_Failed_IntegrationClientNotFound()
        {
            // arrange
            var request = GetIntegrationClientReadRequest();
            MockIntegrationClientRepository.Setup_Select_Returns_OrganizationOneIntegrationOneIntegrationClientOneNotExist();
            // act
            var result = await SystemUnderTest.GetIntegrationClient(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Failed, IntegrationClientNotFound);
            AssertReturnType<IntegrationClientReadResponse>(result);
            MockIntegrationClientRepository.Verify_Select();
        }

        [Test]
        public async Task IntegrationService_GetIntegrationClients_Success_SelectAfter()
        {
            // arrange
            var request = GetIntegrationClientReadListRequestForSelectAfter();
            MockIntegrationRepository.Setup_Select_Returns_OrganizationOneIntegrationOne();
            MockIntegrationClientRepository.Setup_SelectAfter_Returns_IntegrationClients();
            MockIntegrationClientRepository.Setup_Count_Returns_Ten();

            // act
            var result = await SystemUnderTest.GetIntegrationClients(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Success);
            AssertReturnType<IntegrationClientReadListResponse>(result);
            AssertPagingInfoForSelectAfter(request.PagingInfo, Ten);
            MockIntegrationRepository.Verify_Select();
            MockIntegrationClientRepository.Verify_SelectAfter();
            MockIntegrationClientRepository.Verify_Count();
        }

        [Test]
        public async Task IntegrationService_GetIntegrationClients_Success_SelectMany()
        {
            // arrange
            var request = GetIntegrationClientReadListRequestForSelectMany();
            MockIntegrationRepository.Setup_Select_Returns_OrganizationOneIntegrationOne();
            MockIntegrationClientRepository.Setup_SelectMany_Returns_IntegrationClients();
            MockIntegrationClientRepository.Setup_Count_Returns_Ten();

            // act
            var result = await SystemUnderTest.GetIntegrationClients(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Success);
            AssertReturnType<IntegrationClientReadListResponse>(result);
            AssertPagingInfoForSelectMany(request.PagingInfo, Ten);
            MockIntegrationRepository.Verify_Select();
            MockIntegrationClientRepository.Verify_SelectMany();
            MockIntegrationClientRepository.Verify_Count();
        }

        [Test]
        public async Task IntegrationService_GetIntegrationClients_Failed_IntegrationNotFound()
        {
            // arrange
            var request = GetIntegrationClientReadListRequest();
            MockIntegrationRepository.Setup_Select_Returns_OrganizationOneIntegrationOneNotExist();

            // act
            var result = await SystemUnderTest.GetIntegrationClients(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Failed, IntegrationNotFound);
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
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Success);
            AssertReturnType<IntegrationClientRefreshResponse>(result);
            MockUserRepository.Verify_SelectById();
            MockOrganizationRepository.Verify_Any();
            MockIntegrationClientRepository.Verify_Select();
            MockIntegrationRepository.Verify_SelectById();
        }

        [Test]
        public async Task IntegrationService_RefreshIntegrationClient_Invalid_CurrentUserNotAdmin()
        {
            // arrange
            var request = GetIntegrationClientRefreshRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneUserOne();

            // act
            var result = await SystemUnderTest.RefreshIntegrationClient(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Invalid, UserNotAdmin);
            AssertReturnType<IntegrationClientRefreshResponse>(result);
            MockUserRepository.Verify_SelectById();
        }

        [Test]
        public async Task IntegrationService_RefreshIntegrationClient_Invalid_OrganizationNotActive()
        {
            // arrange
            var request = GetIntegrationClientRefreshRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneAdminUserOne();
            MockOrganizationRepository.Setup_Any_Returns_True();

            // act
            var result = await SystemUnderTest.RefreshIntegrationClient(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Invalid, OrganizationNotActive);
            AssertReturnType<IntegrationClientRefreshResponse>(result);
            MockUserRepository.Verify_SelectById();
            MockOrganizationRepository.Verify_Any();
        }

        [Test]
        public async Task IntegrationService_RefreshIntegrationClient_Failed_IntegrationClientNotFound()
        {
            // arrange
            var request = GetIntegrationClientRefreshRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneAdminUserOne();
            MockOrganizationRepository.Setup_Any_Returns_False();
            MockIntegrationClientRepository.Setup_Select_Returns_OrganizationOneIntegrationOneIntegrationClientOneNotExist();

            // act
            var result = await SystemUnderTest.RefreshIntegrationClient(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Failed, IntegrationClientNotFound);
            AssertReturnType<IntegrationClientRefreshResponse>(result);
            MockUserRepository.Verify_SelectById();
            MockOrganizationRepository.Verify_Any();
            MockIntegrationClientRepository.Verify_Select();
        }

        [Test]
        public async Task IntegrationService_RefreshIntegrationClient_Invalid_OrganizationNotMatch()
        {
            // arrange
            var request = GetIntegrationClientRefreshRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneAdminUserOne();
            MockOrganizationRepository.Setup_Any_Returns_False();
            MockIntegrationClientRepository.Setup_Select_Returns_OrganizationOneIntegrationOneIntegrationClientOne();
            MockIntegrationClientRepository.Setup_Select_Returns_OrganizationTwoIntegrationOneIntegrationClientOne();

            // act
            var result = await SystemUnderTest.RefreshIntegrationClient(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Invalid);
            AssertReturnType<IntegrationClientRefreshResponse>(result);
            MockUserRepository.Verify_SelectById();
            MockOrganizationRepository.Verify_Any();
            MockIntegrationClientRepository.Verify_Select();
        }

        [Test]
        public async Task IntegrationService_RefreshIntegrationClient_Failed_IntegrationNotFound()
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
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Failed, IntegrationNotFound);
            AssertReturnType<IntegrationClientRefreshResponse>(result);
            MockUserRepository.Verify_SelectById();
            MockOrganizationRepository.Verify_Any();
            MockIntegrationClientRepository.Verify_Select();
            MockIntegrationRepository.Verify_SelectById();
        }

        [Test]
        public async Task IntegrationService_RefreshIntegrationClient_Invalid_IntegrationNotActive()
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
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Invalid, IntegrationNotActive);
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
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Success);
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
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Failed);
            AssertReturnType<IntegrationClientDeleteResponse>(result);
            MockUserRepository.Verify_SelectById();
            MockOrganizationRepository.Verify_Any();
            MockIntegrationClientRepository.Verify_Select();
            MockIntegrationRepository.Verify_SelectById();
            MockIntegrationClientRepository.Verify_Delete();
        }

        [Test]
        public async Task IntegrationService_DeleteIntegrationClient_Invalid_CurrentUserNotAdmin()
        {
            // arrange
            var request = GetIntegrationClientDeleteRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneUserOne();

            // act
            var result = await SystemUnderTest.DeleteIntegrationClient(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Invalid, UserNotAdmin);
            AssertReturnType<IntegrationClientDeleteResponse>(result);
            MockUserRepository.Verify_SelectById();
        }

        [Test]
        public async Task IntegrationService_DeleteIntegrationClient_Invalid_OrganizationNotActive()
        {
            // arrange
            var request = GetIntegrationClientDeleteRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneAdminUserOne();
            MockOrganizationRepository.Setup_Any_Returns_True();

            // act
            var result = await SystemUnderTest.DeleteIntegrationClient(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Invalid, OrganizationNotActive);
            AssertReturnType<IntegrationClientDeleteResponse>(result);
            MockUserRepository.Verify_SelectById();
            MockOrganizationRepository.Verify_Any();
        }

        [Test]
        public async Task IntegrationService_DeleteIntegrationClient_Failed_IntegrationClientNotFound()
        {
            // arrange
            var request = GetIntegrationClientDeleteRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneAdminUserOne();
            MockOrganizationRepository.Setup_Any_Returns_False();
            MockIntegrationClientRepository.Setup_Select_Returns_OrganizationOneIntegrationOneIntegrationClientOneNotExist();

            // act
            var result = await SystemUnderTest.DeleteIntegrationClient(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Failed, IntegrationClientNotFound);
            AssertReturnType<IntegrationClientDeleteResponse>(result);
            MockUserRepository.Verify_SelectById();
            MockOrganizationRepository.Verify_Any();
            MockIntegrationClientRepository.Verify_Select();
        }

        [Test]
        public async Task IntegrationService_DeleteIntegrationClient_Invalid_OrganizationNotMatch()
        {
            // arrange
            var request = GetIntegrationClientDeleteRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneAdminUserOne();
            MockOrganizationRepository.Setup_Any_Returns_False();
            MockIntegrationClientRepository.Setup_Select_Returns_OrganizationOneIntegrationOneIntegrationClientOne();
            MockIntegrationClientRepository.Setup_Select_Returns_OrganizationTwoIntegrationOneIntegrationClientOne();

            // act
            var result = await SystemUnderTest.DeleteIntegrationClient(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Invalid);
            AssertReturnType<IntegrationClientDeleteResponse>(result);
            MockUserRepository.Verify_SelectById();
            MockOrganizationRepository.Verify_Any();
            MockIntegrationClientRepository.Verify_Select();
        }

        [Test]
        public async Task IntegrationService_DeleteIntegrationClient_Failed_IntegrationNotFound()
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
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Failed, IntegrationNotFound);
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
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Success);
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
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Failed);
            AssertReturnType<IntegrationClientChangeActivationResponse>(result);
            MockUserRepository.Verify_SelectById();
            MockOrganizationRepository.Verify_Any();
            MockIntegrationClientRepository.Verify_Select();
            MockIntegrationRepository.Verify_SelectById();
            MockIntegrationClientRepository.Verify_Update();
        }

        [Test]
        public async Task IntegrationService_ChangeActivationForIntegrationClient_Invalid_CurrentUserNotAdmin()
        {
            // arrange
            var request = GetIntegrationClientChangeActivationRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneUserOne();

            // act
            var result = await SystemUnderTest.ChangeActivationForIntegrationClient(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Invalid, UserNotAdmin);
            AssertReturnType<IntegrationClientChangeActivationResponse>(result);
            MockUserRepository.Verify_SelectById();
        }

        [Test]
        public async Task IntegrationService_ChangeActivationForIntegrationClient_Invalid_OrganizationNotActive()
        {
            // arrange
            var request = GetIntegrationClientChangeActivationRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneAdminUserOne();
            MockOrganizationRepository.Setup_Any_Returns_True();

            // act
            var result = await SystemUnderTest.ChangeActivationForIntegrationClient(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Invalid, OrganizationNotActive);
            AssertReturnType<IntegrationClientChangeActivationResponse>(result);
            MockUserRepository.Verify_SelectById();
            MockOrganizationRepository.Verify_Any();
        }

        [Test]
        public async Task IntegrationService_ChangeActivationForIntegrationClient_Failed_IntegrationClientNotFound()
        {
            // arrange
            var request = GetIntegrationClientChangeActivationRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneAdminUserOne();
            MockOrganizationRepository.Setup_Any_Returns_False();
            MockIntegrationClientRepository.Setup_Select_Returns_OrganizationOneIntegrationOneIntegrationClientOneNotExist();

            // act
            var result = await SystemUnderTest.ChangeActivationForIntegrationClient(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Failed, IntegrationClientNotFound);
            AssertReturnType<IntegrationClientChangeActivationResponse>(result);
            MockUserRepository.Verify_SelectById();
            MockOrganizationRepository.Verify_Any();
            MockIntegrationClientRepository.Verify_Select();
        }

        [Test]
        public async Task IntegrationService_ChangeActivationForIntegrationClient_Invalid_OrganizationNotMatch()
        {
            // arrange
            var request = GetIntegrationClientChangeActivationRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneAdminUserOne();
            MockOrganizationRepository.Setup_Any_Returns_False();
            MockIntegrationClientRepository.Setup_Select_Returns_OrganizationTwoIntegrationOneIntegrationClientOne();

            // act
            var result = await SystemUnderTest.ChangeActivationForIntegrationClient(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Invalid);
            AssertReturnType<IntegrationClientChangeActivationResponse>(result);
            MockUserRepository.Verify_SelectById();
            MockOrganizationRepository.Verify_Any();
            MockIntegrationClientRepository.Verify_Select();
        }

        [Test]
        public async Task IntegrationService_ChangeActivationForIntegrationClient_Failed_IntegrationNotFound()
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
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Failed, IntegrationNotFound);
            AssertReturnType<IntegrationClientChangeActivationResponse>(result);
            MockUserRepository.Verify_SelectById();
            MockOrganizationRepository.Verify_Any();
            MockIntegrationClientRepository.Verify_Select();
            MockIntegrationRepository.Verify_SelectById();
        }

        [Test]
        public async Task IntegrationService_ChangeActivationForIntegrationClient_Invalid_IntegrationNotActive()
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
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Invalid, IntegrationNotActive);
            AssertReturnType<IntegrationClientChangeActivationResponse>(result);
            MockUserRepository.Verify_SelectById();
            MockOrganizationRepository.Verify_Any();
            MockIntegrationClientRepository.Verify_Select();
            MockIntegrationRepository.Verify_SelectById();
        }

        [Test]
        public async Task IntegrationService_CreateToken_Success()
        {
            // arrange
            var request = GetTokenCreateRequest();
            MockIntegrationClientRepository.Setup_Select_Returns_OrganizationOneIntegrationOneIntegrationClientOne();
            MockIntegrationRepository.Setup_Any_Returns_False();
            MockTokenRepository.Setup_Insert_Success();

            // act
            var result = await SystemUnderTest.CreateToken(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Success);
            AssertReturnType<TokenCreateResponse>(result);
            MockIntegrationClientRepository.Verify_Select();
            MockIntegrationRepository.Verify_Any();
            MockTokenRepository.Verify_Insert();
        }

        [Test]
        public async Task IntegrationService_CreateToken_Failed()
        {
            // arrange
            var request = GetTokenCreateRequest();
            MockIntegrationClientRepository.Setup_Select_Returns_OrganizationOneIntegrationOneIntegrationClientOne();
            MockIntegrationRepository.Setup_Any_Returns_False();
            MockTokenRepository.Setup_Insert_Failed();

            // act
            var result = await SystemUnderTest.CreateToken(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Failed);
            AssertReturnType<TokenCreateResponse>(result);
            MockIntegrationClientRepository.Verify_Select();
            MockIntegrationRepository.Verify_Any();
            MockTokenRepository.Verify_Insert();
        }

        [Test]
        public async Task IntegrationService_CreateToken_Failed_IntegrationClientNotFound()
        {
            // arrange
            var request = GetTokenCreateRequest();
            MockIntegrationClientRepository.Setup_Select_Returns_OrganizationOneIntegrationOneIntegrationClientOneNotExist();

            // act
            var result = await SystemUnderTest.CreateToken(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Failed, IntegrationClientNotFound);
            AssertReturnType<TokenCreateResponse>(result);
            MockIntegrationClientRepository.Verify_Select();
        }

        [Test]
        public async Task IntegrationService_CreateToken_Invalid_IntegrationNotActive()
        {
            // arrange
            var request = GetTokenCreateRequest();
            MockIntegrationClientRepository.Setup_Select_Returns_OrganizationOneIntegrationOneIntegrationClientOne();
            MockIntegrationRepository.Setup_Any_Returns_True();

            // act
            var result = await SystemUnderTest.CreateToken(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Invalid, IntegrationClientNotActive);
            AssertReturnType<TokenCreateResponse>(result);
            MockIntegrationClientRepository.Verify_Select();
            MockIntegrationRepository.Verify_Any();
        }

        [Test]
        public async Task IntegrationService_CreateTokenWhenUserAuthenticated_Success()
        {
            // arrange
            var request = GetTokenGetRequest();
            MockIntegrationClientRepository.Setup_Select_Returns_OrganizationOneIntegrationOneIntegrationClientOne();
            MockIntegrationRepository.Setup_Any_Returns_False();
            MockTokenRepository.Setup_Insert_Success();

            // act
            var result = await SystemUnderTest.CreateTokenWhenUserAuthenticated(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Success);
            AssertReturnType<TokenCreateResponse>(result);
            MockIntegrationClientRepository.Verify_Select();
            MockTokenRepository.Verify_Insert();
        }

        [Test]
        public async Task IntegrationService_CreateTokenWhenUserAuthenticated_CurrentUserNull()
        {
            // arrange
            var request = GetTokenGetRequest();
           MockUserRepository.Setup_SelectById_Returns_CurrentUserNull();

            // act
            var result = await SystemUnderTest.CreateTokenWhenUserAuthenticated(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Invalid);
            AssertReturnType<TokenCreateResponse>(result);
            MockUserRepository.Verify_SelectById();
        }

        [Test]
        public async Task IntegrationService_CreateTokenWhenUserAuthenticated_Failed()
        {
            // arrange
            var request = GetTokenGetRequest();
            MockIntegrationClientRepository.Setup_Select_Returns_OrganizationOneIntegrationOneIntegrationClientOne();
            MockIntegrationRepository.Setup_Any_Returns_False();
            MockTokenRepository.Setup_Insert_Failed();

            // act
            var result = await SystemUnderTest.CreateTokenWhenUserAuthenticated(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Failed);
            AssertReturnType<TokenCreateResponse>(result);
            MockIntegrationClientRepository.Verify_Select();
            MockIntegrationRepository.Verify_Any();
            MockTokenRepository.Verify_Insert();
        }

        [Test]
        public async Task IntegrationService_CreateTokenWhenUserAuthenticated_Failed_IntegrationClientNotFound()
        {
            // arrange
            var request = GetTokenGetRequest();
            MockIntegrationClientRepository.Setup_Select_Returns_OrganizationOneIntegrationOneIntegrationClientOneNotExist();

            // act
            var result = await SystemUnderTest.CreateTokenWhenUserAuthenticated(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Failed, IntegrationClientNotFound);
            AssertReturnType<TokenCreateResponse>(result);
            MockIntegrationClientRepository.Verify_Select();
        }

        [Test]
        public async Task IntegrationService_CreateTokenWhenUserAuthenticated_Invalid_IntegrationNotActive()
        {
            // arrange
            var request = GetTokenGetRequest();
            MockIntegrationClientRepository.Setup_Select_Returns_OrganizationOneIntegrationOneIntegrationClientOne();
            MockIntegrationRepository.Setup_Any_Returns_True();

            // act
            var result = await SystemUnderTest.CreateTokenWhenUserAuthenticated(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Invalid, IntegrationNotActive);
            AssertReturnType<TokenCreateResponse>(result);
            MockIntegrationClientRepository.Verify_Select();
            MockIntegrationRepository.Verify_Any();
        }

        [Test]
        public async Task IntegrationService_RevokeToken_Success()
        {
            // arrange
            var request = GetTokenRevokeRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneAdminUserOne();
            MockIntegrationClientRepository.Setup_Select_Returns_OrganizationOneIntegrationOneIntegrationClientOne();
            MockTokenRepository.Setup_Select_Returns_OrganizationOneIntegrationOneIntegrationClientOneTokenOne();
            MockTokenRepository.Setup_Delete_Returns_True();

            // act
            var result = await SystemUnderTest.RevokeToken(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Success);
            AssertReturnType<TokenRevokeResponse>(result);
            MockUserRepository.Verify_SelectById();
            MockIntegrationClientRepository.Verify_Select();
            MockTokenRepository.Verify_Select();
            MockTokenRepository.Verify_Delete();
        }

        [Test]
        public async Task IntegrationService_RevokeToken_Failed()
        {
            // arrange
            var request = GetTokenRevokeRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneAdminUserOne();
            MockIntegrationClientRepository.Setup_Select_Returns_OrganizationOneIntegrationOneIntegrationClientOne();
            MockTokenRepository.Setup_Select_Returns_OrganizationOneIntegrationOneIntegrationClientOneTokenOne();
            MockTokenRepository.Setup_Delete_Returns_False();

            // act
            var result = await SystemUnderTest.RevokeToken(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Failed);
            AssertReturnType<TokenRevokeResponse>(result);
            MockUserRepository.Verify_SelectById();
            MockIntegrationClientRepository.Verify_Select();
            MockTokenRepository.Verify_Select();
            MockTokenRepository.Verify_Delete();
        }

        [Test]
        public async Task IntegrationService_RevokeToken_Invalid_CurrentUserNotAdmin()
        {
            // arrange
            var request = GetTokenRevokeRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneUserOne();

            // act
            var result = await SystemUnderTest.RevokeToken(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Invalid, UserNotAdmin);
            AssertReturnType<TokenRevokeResponse>(result);
            MockUserRepository.Verify_SelectById();
        }

        [Test]
        public async Task IntegrationService_RevokeToken_Failed_IntegrationClientNotFound()
        {
            // arrange
            var request = GetTokenRevokeRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneAdminUserOne();
            MockIntegrationClientRepository.Setup_Select_Returns_OrganizationOneIntegrationOneIntegrationClientOneNotExist();

            // act
            var result = await SystemUnderTest.RevokeToken(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Failed, IntegrationClientNotFound);
            AssertReturnType<TokenRevokeResponse>(result);
            MockUserRepository.Verify_SelectById();
            MockIntegrationClientRepository.Verify_Select();
        }

        [Test]
        public async Task IntegrationService_RevokeToken_Invalid_OrganizationNotMatch()
        {
            // arrange
            var request = GetTokenRevokeRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneAdminUserOne();
            MockIntegrationClientRepository.Setup_Select_Returns_OrganizationTwoIntegrationOneIntegrationClientOne();

            // act
            var result = await SystemUnderTest.RevokeToken(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Invalid);
            AssertReturnType<TokenRevokeResponse>(result);
            MockUserRepository.Verify_SelectById();
            MockIntegrationClientRepository.Verify_Select();
        }

        [Test]
        public async Task IntegrationService_RevokeToken_Failed_TokenNotFound()
        {
            // arrange
            var request = GetTokenRevokeRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneAdminUserOne();
            MockIntegrationClientRepository.Setup_Select_Returns_OrganizationOneIntegrationOneIntegrationClientOne();
            MockTokenRepository.Setup_Select_Returns_OrganizationOneIntegrationOneIntegrationClientOneTokenOneNotExist();

            // act
            var result = await SystemUnderTest.RevokeToken(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Failed, TokenNotFound);
            AssertReturnType<TokenRevokeResponse>(result);
            MockUserRepository.Verify_SelectById();
            MockIntegrationClientRepository.Verify_Select();
            MockTokenRepository.Verify_Select();
        }

        [Test]
        public async Task IntegrationService_ValidateToken_Success()
        {
            // arrange
            var request = GetTokenValidateRequest();
            MockProjectRepository.Setup_Select_Returns_OrganizationOneProjectOne();
            MockTokenRepository.Setup_Select_Returns_OrganizationOneIntegrationOneIntegrationClientOneTokenOne();
            MockOrganizationRepository.Setup_Any_Returns_False();

            // act
            var result = await SystemUnderTest.ValidateToken(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Success);
            AssertReturnType<TokenValidateResponse>(result);
            MockProjectRepository.Verify_Select();
            MockTokenRepository.Verify_Select();
            MockOrganizationRepository.Verify_Any();
        }

        [Test]
        public async Task IntegrationService_ValidateToken_Invalid_OrganizationNotActive()
        {
            // arrange
            var request = GetTokenValidateRequest();
            MockProjectRepository.Setup_Select_Returns_OrganizationOneProjectOne();
            MockTokenRepository.Setup_Select_Returns_OrganizationOneIntegrationOneIntegrationClientOneTokenOne();
            MockOrganizationRepository.Setup_Any_Returns_True();

            // act
            var result = await SystemUnderTest.ValidateToken(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Invalid, OrganizationNotActive);
            AssertReturnType<TokenValidateResponse>(result);
            MockProjectRepository.Verify_Select();
            MockTokenRepository.Verify_Select();
            MockOrganizationRepository.Verify_Any();
        }

        [Test]
        public async Task IntegrationService_ValidateToken_Failed_ProjectNotFound()
        {
            // arrange
            var request = GetTokenValidateRequest();
            MockProjectRepository.Setup_Select_Returns_OrganizationOneProjectOneNotExist();

            // act
            var result = await SystemUnderTest.ValidateToken(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Failed, ProjectNotFound);
            AssertReturnType<TokenValidateResponse>(result);
            MockProjectRepository.Verify_Select();
        }

        [Test]
        public async Task IntegrationService_ValidateToken_Failed_TokenNotFound()
        {
            // arrange
            var request = GetTokenValidateRequest();
            MockProjectRepository.Setup_Select_Returns_OrganizationOneProjectOne();
            MockTokenRepository.Setup_Select_Returns_OrganizationOneIntegrationOneIntegrationClientOneTokenOneNotExist();

            // act
            var result = await SystemUnderTest.ValidateToken(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Failed, TokenNotFound);
            AssertReturnType<TokenValidateResponse>(result);
            MockProjectRepository.Verify_Select();
            MockTokenRepository.Verify_Select();
        }

        [Test]
        public async Task IntegrationService_ValidateToken_Invalid_OrganizationNotMatch()
        {
            // arrange
            var request = GetTokenValidateRequest();
            MockProjectRepository.Setup_Select_Returns_OrganizationOneProjectOne();
            MockTokenRepository.Setup_Select_Returns_OrganizationTwoIntegrationOneIntegrationClientOneTokenOne();

            // act
            var result = await SystemUnderTest.ValidateToken(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Invalid);
            AssertReturnType<TokenValidateResponse>(result);
            MockProjectRepository.Verify_Select();
            MockTokenRepository.Verify_Select();
        }

        [Test]
        public async Task IntegrationService_GetActiveTokensOfOrganization_SelectMany_Success()
        {
            // arrange
            var request = GetOrganizationActiveTokenReadListRequest();
            MockTokenRepository.Setup_SelectMany_Returns_Tokens();

            // act
            var result = await SystemUnderTest.GetActiveTokensOfOrganization(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Success);
            AssertReturnType<OrganizationActiveTokenReadListResponse>(result);
            MockTokenRepository.Verify_SelectMany();
        }

        [Test]
        public async Task IntegrationService_GetActiveTokensOfIntegration_SelectMany_Success()
        {
            // arrange
            var request = GetIntegrationActiveTokenReadListRequest();
            MockTokenRepository.Setup_SelectMany_Returns_Tokens();

            // act
            var result = await SystemUnderTest.GetActiveTokensOfIntegration(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Success);
            AssertReturnType<IntegrationActiveTokenReadListResponse>(result);
            MockTokenRepository.Verify_SelectMany();
        }

        [Test]
        public async Task IntegrationService_GetActiveTokensOfIntegrationClient_SelectMany_Success()
        {
            // arrange
            var request = GetIntegrationClientActiveTokenReadListRequest();
            MockTokenRepository.Setup_SelectMany_Returns_Tokens();

            // act
            var result = await SystemUnderTest.GetActiveTokensOfIntegrationClient(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Success);
            AssertReturnType<IntegrationClientActiveTokenReadListResponse>(result);
            MockTokenRepository.Verify_SelectMany();
        }

        [Test]
        public async Task IntegrationService_GetTokenRequestLogsOfOrganization_SelectMany_Success()
        {
            // arrange
            var request = GetOrganizationTokenRequestLogReadListRequest();
            MockTokenRequestLogRepository.Setup_SelectMany_Returns_TokenRequestLogs();

            // act
            var result = await SystemUnderTest.GetTokenRequestLogsOfOrganization(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Success);
            AssertReturnType<OrganizationTokenRequestLogReadListResponse>(result);
            MockTokenRequestLogRepository.Verify_SelectMany();
        }

        [Test]
        public async Task IntegrationService_GetTokenRequestLogsOfIntegration_SelectMany_Success()
        {
            // arrange
            var request = GetIntegrationTokenRequestLogReadListRequest();
            MockIntegrationRepository.Setup_Select_Returns_OrganizationOneIntegrationOne();
            MockTokenRequestLogRepository.Setup_SelectMany_Returns_TokenRequestLogs();

            // act
            var result = await SystemUnderTest.GetTokenRequestLogsOfIntegration(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Success);
            AssertReturnType<IntegrationTokenRequestLogReadListResponse>(result);
            MockIntegrationRepository.Verify_Select();
            MockTokenRequestLogRepository.Verify_SelectMany();
        }

        [Test]
        public async Task IntegrationService_GetTokenRequestLogsOfIntegration_Failed_IntegrationNotFound()
        {
            // arrange
            var request = GetIntegrationTokenRequestLogReadListRequest();
            MockIntegrationRepository.Setup_Select_Returns_OrganizationOneIntegrationOneNotExist();

            // act
            var result = await SystemUnderTest.GetTokenRequestLogsOfIntegration(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Failed, IntegrationNotFound);
            AssertReturnType<IntegrationTokenRequestLogReadListResponse>(result);
            MockIntegrationRepository.Verify_Select();
        }

        [Test]
        public async Task IntegrationService_GetTokenRequestLogsOfIntegrationClient_SelectMany_Success()
        {
            // arrange
            var request = GetIntegrationClientTokenRequestLogReadListRequest();
            MockTokenRequestLogRepository.Setup_SelectMany_Returns_TokenRequestLogs();

            // act
            var result = await SystemUnderTest.GetTokenRequestLogsOfIntegrationClient(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Success);
            AssertReturnType<IntegrationClientTokenRequestLogReadListResponse>(result);
            MockTokenRequestLogRepository.Verify_SelectMany();
        }

        [Test]
        public async Task IntegrationService_GetAllActiveTokens_SelectMany_Success()
        {
            // arrange
            var request = GetAllActiveTokenReadListRequest();
            MockTokenRepository.Setup_SelectMany_Returns_Tokens();

            // act
            var result = await SystemUnderTest.GetAllActiveTokens(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Success);
            AssertReturnType<AllActiveTokenReadListResponse>(result);
            MockTokenRepository.Verify_SelectMany();
        }

        [Test]
        public async Task IntegrationService_GetAllTokenRequestLogs_SelectMany_Success()
        {
            // arrange
            var request = GetAllTokenRequestLogReadListRequest();
            MockTokenRequestLogRepository.Setup_SelectMany_Returns_TokenRequestLogs();

            // act
            var result = await SystemUnderTest.GetAllTokenRequestLogs(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Success);
            AssertReturnType<AllTokenRequestLogReadListResponse>(result);
            MockTokenRequestLogRepository.Verify_SelectMany();
        }
    }
}