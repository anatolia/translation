using System.Threading.Tasks;

using NUnit.Framework;

using Translation.Common.Contracts;
using Translation.Common.Enumerations;
using Translation.Common.Models.Responses.Label;
using Translation.Common.Models.Responses.Label.LabelTranslation;
using Translation.Tests.SetupHelpers;
using static Translation.Tests.TestHelpers.FakeRequestTestHelper;
using static Translation.Tests.TestHelpers.FakeConstantTestHelper;
using static Translation.Tests.TestHelpers.AssertViewModelTestHelper;
using static Translation.Tests.TestHelpers.AssertResponseTestHelper;

namespace Translation.Tests.Server.Services
{
    [TestFixture]
    public class LabelServiceTests : ServiceBaseTests
    {
        public ILabelService SystemUnderTest { get; set; }

        [SetUp]
        public void run_before_every_test()
        {
            SystemUnderTest = Container.Resolve<ILabelService>();
        }

        [Test]
        public async Task LabelService_CreateLabel_Success()
        {
            // arrange
            var request = GetLabelCreateRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneAdminUserOne();
            MockProjectRepository.Setup_Select_Returns_OrganizationOneProjectOne();
            MockLabelRepository.Setup_Any_Returns_False();
            MockLabelUnitOfWork.Setup_DoCreateWork_Returns_True();

            // act
            var result = await SystemUnderTest.CreateLabel(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Success);
            AssertReturnType<LabelCreateResponse>(result);
            MockUserRepository.Verify_SelectById();
            MockProjectRepository.Verify_Select();
            MockLabelRepository.Verify_Any();
            MockLabelUnitOfWork.Verify_DoCreateWork();
        }

        [Test]
        public async Task LabelService_CreateLabel_Invalid_CurrentUserNotAdmin()
        {
            // arrange
            var request = GetLabelCreateRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneUserOne();

            // act
            var result = await SystemUnderTest.CreateLabel(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Invalid);
            AssertReturnType<LabelCreateResponse>(result);
            MockUserRepository.Verify_SelectById();
        }

        [Test]
        public async Task LabelService_CreateLabel_Invalid_ProjectNotFound()
        {
            // arrange
            var request = GetLabelCreateRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneAdminUserOne();
            MockProjectRepository.Setup_Select_Returns_OrganizationOneProjectOneNotExist();

            // act
            var result = await SystemUnderTest.CreateLabel(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Invalid, ProjectNotFound);
            AssertReturnType<LabelCreateResponse>(result);
            MockUserRepository.Verify_SelectById();
            MockProjectRepository.Verify_Select();
        }

        [Test]
        public async Task LabelService_CreateLabel_Invalid_OrganizationNotMatch()
        {
            // arrange
            var request = GetLabelCreateRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneAdminUserOne();
            MockProjectRepository.Setup_Select_Returns_OrganizationTwoProjectOne();

            // act
            var result = await SystemUnderTest.CreateLabel(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Invalid, ProjectNotFound);
            AssertReturnType<LabelCreateResponse>(result);
            MockUserRepository.Verify_SelectById();
            MockProjectRepository.Verify_Select();
        }

        [Test]
        public async Task LabelService_CreateLabel_Invalid_OrganizationNotActive()
        {
            // arrange
            var request = GetLabelCreateRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneAdminUserOne();
            MockProjectRepository.Setup_Select_Returns_OrganizationOneProjectOne();
            MockOrganizationRepository.Setup_Any_Returns_True();

            // act
            var result = await SystemUnderTest.CreateLabel(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Invalid, ProjectNotFound);
            AssertReturnType<LabelCreateResponse>(result);
            MockUserRepository.Verify_SelectById();
            MockProjectRepository.Verify_Select();
            MockOrganizationRepository.Verify_Any();
        }

        [Test]
        public async Task LabelService_CreateLabel_Failed()
        {
            // arrange
            var request = GetLabelCreateRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneAdminUserOne();
            MockProjectRepository.Setup_Select_Returns_OrganizationOneProjectOne();
            MockLabelRepository.Setup_Any_Returns_False();
            MockLabelUnitOfWork.Setup_DoCreateWork_Returns_False();

            // act
            var result = await SystemUnderTest.CreateLabel(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Success);
            AssertReturnType<LabelCreateResponse>(result);
            MockUserRepository.Verify_SelectById();
            MockProjectRepository.Verify_Select();
            MockLabelRepository.Verify_Any();
            MockLabelUnitOfWork.Verify_DoCreateWork();
        }

        [Test]
        public async Task LabelService_CreateLabel_LabelCreateWithTokenRequest_Success()
        {
            // arrange
            var request = GetLabelCreateWithTokenRequest();
            MockProjectRepository.Setup_Select_Returns_OrganizationOneProjectOne();
            MockTokenRepository.Setup_Select_Returns_OrganizationOneIntegrationOneIntegrationClientOneTokenOne();
            MockOrganizationRepository.Setup_Any_Returns_False();
            MockLabelRepository.Setup_Any_Returns_False();
            MockLabelUnitOfWork.Setup_DoCreateWork_Returns_True();

            // act
            var result = await SystemUnderTest.CreateLabel(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Success);
            AssertReturnType<LabelCreateResponse>(result);
            MockProjectRepository.Verify_Select();
            MockTokenRepository.Verify_Select();
            MockOrganizationRepository.Verify_Any();
            MockLabelRepository.Verify_Any();
            MockLabelUnitOfWork.Verify_DoCreateWork();
        }

        [Test]
        public async Task LabelService_CreateLabel_LabelCreateWithTokenRequest_Invalid_ProjectNotFound()
        {
            // arrange
            var request = GetLabelCreateWithTokenRequest();
            MockProjectRepository.Setup_Select_Returns_OrganizationOneProjectOneNotExist();

            // act
            var result = await SystemUnderTest.CreateLabel(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Invalid, ProjectNotFound);
            AssertReturnType<LabelCreateResponse>(result);
            MockProjectRepository.Verify_Select();
        }

        [Test]
        public async Task LabelService_CreateLabel_LabelCreateWithTokenRequest_Invalid_TokenNotFound()
        {
            // arrange
            var request = GetLabelCreateWithTokenRequest();
            MockProjectRepository.Setup_Select_Returns_OrganizationOneProjectOne();
            MockTokenRepository.Setup_Select_Returns_OrganizationOneIntegrationOneIntegrationClientOneTokenOneNotExist();

            // act
            var result = await SystemUnderTest.CreateLabel(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Invalid, TokenNotFound);
            AssertReturnType<LabelCreateResponse>(result);
            MockProjectRepository.Verify_Select();
            MockTokenRepository.Verify_Select();
        }

        [Test]
        public async Task LabelService_CreateLabel_LabelCreateWithTokenRequest_Invalid_OrganizationNotMatch()
        {
            // arrange
            var request = GetLabelCreateWithTokenRequest();
            MockProjectRepository.Setup_Select_Returns_OrganizationOneProjectOne();
            MockTokenRepository.Setup_Select_Returns_OrganizationTwoIntegrationOneIntegrationClientOneTokenOne();

            // act
            var result = await SystemUnderTest.CreateLabel(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Invalid);
            AssertReturnType<LabelCreateResponse>(result);
            MockProjectRepository.Verify_Select();
            MockTokenRepository.Verify_Select();
        }

        [Test]
        public async Task LabelService_CreateLabel_LabelCreateWithTokenRequest_Invalid_OrganizationNotFound()
        {
            // arrange
            var request = GetLabelCreateWithTokenRequest();
            MockProjectRepository.Setup_Select_Returns_OrganizationOneProjectOne();
            MockTokenRepository.Setup_Select_Returns_OrganizationOneIntegrationOneIntegrationClientOneTokenOne();
            MockOrganizationRepository.Setup_Any_Returns_True();

            // act
            var result = await SystemUnderTest.CreateLabel(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Success);
            AssertReturnType<LabelCreateResponse>(result);
            MockProjectRepository.Verify_Select();
            MockTokenRepository.Verify_Select();
            MockOrganizationRepository.Verify_Any();
        }

        [Test]
        public async Task LabelService_CreateLabel_LabelCreateWithTokenRequest_Failed_LabelKeyMustBeUnique()
        {
            // arrange
            var request = GetLabelCreateWithTokenRequest();
            MockProjectRepository.Setup_Select_Returns_OrganizationOneProjectOne();
            MockTokenRepository.Setup_Select_Returns_OrganizationOneIntegrationOneIntegrationClientOneTokenOne();
            MockOrganizationRepository.Setup_Any_Returns_False();
            MockLabelRepository.Setup_Any_Returns_True();

            // act
            var result = await SystemUnderTest.CreateLabel(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Failed, "label_key_must_be_unique");
            AssertReturnType<LabelCreateResponse>(result);
            MockProjectRepository.Verify_Select();
            MockTokenRepository.Verify_Select();
            MockOrganizationRepository.Verify_Any();
            MockLabelRepository.Verify_Any();
        }

        [Test]
        public async Task LabelService_CreateLabel_LabelCreateWithTokenRequest_Failed()
        {
            // arrange
            var request = GetLabelCreateWithTokenRequest();
            MockProjectRepository.Setup_Select_Returns_OrganizationOneProjectOne();
            MockTokenRepository.Setup_Select_Returns_OrganizationOneIntegrationOneIntegrationClientOneTokenOne();
            MockOrganizationRepository.Setup_Any_Returns_False();
            MockLabelRepository.Setup_Any_Returns_False();
            MockLabelUnitOfWork.Setup_DoCreateWork_Returns_False();

            // act
            var result = await SystemUnderTest.CreateLabel(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Success);
            AssertReturnType<LabelCreateResponse>(result);
            MockProjectRepository.Verify_Select();
            MockTokenRepository.Verify_Select();
            MockOrganizationRepository.Verify_Any();
            MockLabelRepository.Verify_Any();
            MockLabelUnitOfWork.Verify_DoCreateWork();
        }

        [Test]
        public async Task LabelService_CreateLabelFromList_Success()
        {
            //todo this test will refactor after LanguageServiceTests finished.
        }

        [Test]
        public async Task LabelService_CreateLabelFromList_Invalid_ProjectNotFound()
        {
            //todo this test will refactor after LanguageServiceTests finished.
        }

        [Test]
        public async Task LabelService_CreateLabelFromList_Invalid_ProjectNotActive()
        {
            //todo this test will refactor after LanguageServiceTests finished.
        }

        [Test]
        public async Task LabelService_CreateLabelFromList_Invalid_OrganizationNotMatch()
        {
            //todo this test will refactor after LanguageServiceTests finished.
        }

        [Test]
        public async Task LabelService_CreateLabelFromList_Invalid_OrganizationNotFound()
        {
            //todo this test will refactor after LanguageServiceTests finished.
        }

        [Test]
        public async Task LabelService_CreateLabelFromList_Failed()
        {
            //todo this test will refactor after LanguageServiceTests finished.
        }

        [Test]
        public async Task LabelService_GetLabel_Success()
        {
            // arrange
            var request = GetLabelReadRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneAdminUserOne();
            MockLabelRepository.Setup_Select_Returns_OrganizationOneProjectOneLabelOne();

            // act
            var result = await SystemUnderTest.GetLabel(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Success);
            AssertReturnType<LabelReadResponse>(result);
            MockUserRepository.Verify_SelectById();
            MockLabelRepository.Verify_Select();
        }

        [Test]
        public async Task LabelService_GetLabel_Invalid_LabelNotFound()
        {
            // arrange
            var request = GetLabelReadRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneAdminUserOne();
            MockLabelRepository.Setup_Select_Returns_OrganizationOneProjectOneLabelOne();

            // act
            var result = await SystemUnderTest.GetLabel(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Invalid, LabelNotFound);
            AssertReturnType<LabelReadResponse>(result);
            MockUserRepository.Verify_SelectById();
            MockLabelRepository.Verify_Select();
        }

        [Test]
        public async Task LabelService_GetLabelByKey_Success()
        {
            // arrange
            var request = GetLabelReadByKeyRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneAdminUserOne();
            MockLabelRepository.Setup_Select_Returns_OrganizationOneProjectOneLabelOne();

            // act
            var result = await SystemUnderTest.GetLabelByKey(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Success);
            AssertReturnType<LabelReadByKeyResponse>(result);
            MockUserRepository.Verify_SelectById();
            MockLabelRepository.Verify_Select();
        }

        [Test]
        public async Task LabelService_GetLabelByKey_Invalid_LabelNotFound()
        {
            // arrange
            var request = GetLabelReadByKeyRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneAdminUserOne();
            MockLabelRepository.Setup_Select_Returns_OrganizationOneProjectOneLabelOneNotExist();

            // act
            var result = await SystemUnderTest.GetLabelByKey(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Invalid, LabelNotFound);
            AssertReturnType<LabelReadByKeyResponse>(result);
            MockUserRepository.Verify_SelectById();
            MockLabelRepository.Verify_Select();
        }

        [Test]
        public async Task LabelService_GetLabels_Invalid_OrganizationNotMatch()
        {
            // arrange
            var request = GetLabelReadListRequest();
            MockProjectRepository.Setup_Select_Returns_OrganizationOneProjectOne();
            MockUserRepository.Setup_SelectById_Returns_OrganizationTwoUserOne();

            // act
            var result = await SystemUnderTest.GetLabels(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Invalid);
            AssertReturnType<LabelReadListResponse>(result);
            MockProjectRepository.Verify_Select();
            MockUserRepository.Verify_SelectById();
        }

        [Test]
        public async Task LabelService_GetLabels_Success_SelectAfter()
        {
            // arrange
            var request = GetLabelReadListRequestForSelectAfter();
            MockProjectRepository.Setup_Select_Returns_OrganizationOneProjectOne();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneUserOne();
            MockLabelRepository.Setup_SelectAfter_Returns_Labels();
            MockLabelRepository.Setup_Count_Returns_Ten();

            // act
            var result = await SystemUnderTest.GetLabels(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Success);
            AssertReturnType<LabelReadListResponse>(result);
            AssertPagingInfoForSelectAfter(request.PagingInfo, Ten);
            MockProjectRepository.Verify_Select();
            MockUserRepository.Verify_SelectById();
            MockLabelRepository.Verify_SelectAfter();
            MockLabelRepository.Verify_Count();
        }

        [Test]
        public async Task LabelService_GetLabels_Success_SelectMany()
        {
            // arrange
            var request = GetLabelReadListRequestForSelectMany();
            MockProjectRepository.Setup_Select_Returns_OrganizationOneProjectOne();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneUserOne();
            MockLabelRepository.Setup_SelectMany_Returns_Labels();
            MockLabelRepository.Setup_Count_Returns_Ten();

            // act
            var result = await SystemUnderTest.GetLabels(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Success);
            AssertReturnType<LabelReadListResponse>(result);
            AssertPagingInfoForSelectMany(request.PagingInfo, Ten);
            MockProjectRepository.Verify_Select();
            MockUserRepository.Verify_SelectById();
            MockLabelRepository.Verify_SelectMany();
            MockLabelRepository.Verify_Count();
        }

        [Test]
        public async Task LabelService_GetLabels_LabelSearchListRequest_Success_SelectMany()
        {
            // arrange
            var request = GetLabelSearchListRequestForSelectMany();
            MockProjectRepository.Setup_Select_Returns_OrganizationOneProjectOne();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneUserOne();
            MockLabelRepository.Setup_SelectMany_Returns_Labels();
            MockLabelRepository.Setup_Count_Returns_Ten();

            // act
            var result = await SystemUnderTest.GetLabels(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Success);
            AssertReturnType<LabelSearchListResponse>(result);
            AssertPagingInfoForSelectMany(request.PagingInfo, Ten);
            MockProjectRepository.Verify_Select();
            MockUserRepository.Verify_SelectById();
            MockLabelRepository.Verify_SelectMany();
            MockLabelRepository.Verify_Count();
        }

        [Test]
        public async Task LabelService_GetLabelRevisions_Success()
        {
            // arrange
            var request = GetLabelRevisionReadListRequest();
            MockLabelRepository.Setup_Select_Returns_OrganizationOneProjectOneLabelOne();
            MockLabelRepository.Setup_SelectRevisions_Returns_OrganizationOneProjectOneLabelOneRevisionsRevisionOneInIt();

            // act
            var result = await SystemUnderTest.GetLabelRevisions(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Success);
            AssertReturnType<LabelRevisionReadListResponse>(result);
            MockLabelRepository.Verify_Select();
            MockLabelRepository.Verify_SelectRevisions();
        }

        [Test]
        public async Task LabelService_GetLabelRevisions_Invalid_LabelNotFound()
        {
            // arrange
            var request = GetLabelRevisionReadListRequest();
            MockLabelRepository.Setup_Select_Returns_OrganizationOneProjectOneLabelOneNotExist();

            // act
            var result = await SystemUnderTest.GetLabelRevisions(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Success);
            AssertReturnType<LabelRevisionReadListResponse>(result);
            MockLabelRepository.Verify_Select();
        }

        [Test]
        public async Task LabelService_GetLabelsWithTranslations_Success()
        {
            // arrange
            var request = GetAllLabelReadListRequest();
            MockProjectRepository.Setup_Select_Returns_OrganizationOneSuperProjectOne();

            // todo this test will refactor after finish the others tests of LabelServiceTests

            // act
            var result = await SystemUnderTest.GetLabelsWithTranslations(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Success);
            AssertReturnType<AllLabelReadListResponse>(result);
            MockProjectRepository.Verify_Select();
        }

        [Test]
        public async Task LabelService_EditLabel_Success()
        {
            // arrange
            var request = GetLabelEditRequest();
            MockLabelRepository.Setup_Select_Returns_OrganizationOneProjectOneLabelOne();
            MockOrganizationRepository.Setup_Any_Returns_False();
            MockLabelRepository.Setup_Any_Returns_False();
            MockLabelRepository.Setup_Update_Success();

            // act
            var result = await SystemUnderTest.EditLabel(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Success);
            AssertReturnType<LabelEditResponse>(result);
            MockLabelRepository.Verify_Select();
            MockOrganizationRepository.Verify_Any();
            MockLabelRepository.Verify_Any();
            MockLabelRepository.Verify_Update();
        }

        [Test]
        public async Task LabelService_EditLabel_Invalid_LabelNotFound()
        {
            // arrange
            var request = GetLabelEditRequest();
            MockLabelRepository.Setup_Select_Returns_OrganizationOneProjectOneLabelOneNotExist();

            // act
            var result = await SystemUnderTest.EditLabel(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Invalid, LabelNotFound);
            AssertReturnType<LabelEditResponse>(result);
            MockLabelRepository.Verify_Select();
        }

        [Test]
        public async Task LabelService_EditLabel_Invalid_OrganizationNotActive()
        {
            // arrange
            var request = GetLabelEditRequest();
            MockLabelRepository.Setup_Select_Returns_OrganizationOneProjectOneLabelOne();
            MockOrganizationRepository.Setup_Any_Returns_True();

            // act
            var result = await SystemUnderTest.EditLabel(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Invalid, OrganizationNotActive);
            AssertReturnType<LabelEditResponse>(result);
            MockLabelRepository.Verify_Select();
            MockOrganizationRepository.Verify_Any();
        }

        [Test]
        public async Task LabelService_EditLabel_Failed_LabelKeyAlreadyExist()
        {
            // arrange
            var request = GetLabelEditRequest();
            MockLabelRepository.Setup_Select_Returns_OrganizationOneProjectOneLabelOne();
            MockOrganizationRepository.Setup_Any_Returns_False();
            MockLabelRepository.Setup_Any_Returns_True();

            // act
            var result = await SystemUnderTest.EditLabel(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Failed, "label_key_already_exist_in_this_project");
            AssertReturnType<LabelEditResponse>(result);
            MockLabelRepository.Verify_Select();
            MockOrganizationRepository.Verify_Any();
            MockLabelRepository.Verify_Any();
        }

        [Test]
        public async Task LabelService_EditLabel_Failed()
        {
            // arrange
            var request = GetLabelEditRequest();
            MockLabelRepository.Setup_Select_Returns_OrganizationOneProjectOneLabelOne();
            MockOrganizationRepository.Setup_Any_Returns_False();
            MockLabelRepository.Setup_Any_Returns_False();
            MockLabelRepository.Setup_Update_Failed();

            // act
            var result = await SystemUnderTest.EditLabel(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Failed);
            AssertReturnType<LabelEditResponse>(result);
            MockLabelRepository.Verify_Select();
            MockOrganizationRepository.Verify_Any();
            MockLabelRepository.Verify_Any();
        }

        [Test]
        public async Task LabelService_ChangeActivation_Success()
        {
            // arrange
            var request = GetLabelChangeActivationRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneUserOne();
            MockLabelRepository.Setup_Select_Returns_OrganizationOneProjectOneLabelOne();
            MockOrganizationRepository.Setup_Any_Returns_False();
            MockProjectRepository.Setup_Any_Returns_False();
            MockLabelRepository.Setup_Update_Success();

            // act
            var result = await SystemUnderTest.ChangeActivation(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Success);
            AssertReturnType<LabelChangeActivationResponse>(result);
            MockUserRepository.Verify_SelectById();
            MockLabelRepository.Verify_Select();
            MockOrganizationRepository.Verify_Any();
            MockProjectRepository.Verify_Any();
            MockLabelRepository.Verify_Update();
        }

        [Test]
        public async Task LabelService_ChangeActivation_Invalid_LabelNotFound()
        {
            // arrange
            var request = GetLabelChangeActivationRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneUserOne();
            MockLabelRepository.Setup_Select_Returns_OrganizationOneProjectOneLabelOneNotExist();

            // act
            var result = await SystemUnderTest.ChangeActivation(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Invalid, LabelNotFound);
            AssertReturnType<LabelChangeActivationResponse>(result);
            MockUserRepository.Verify_SelectById();
            MockLabelRepository.Verify_Select();
        }

        [Test]
        public async Task LabelService_ChangeActivation_Invalid_OrganizationNotMatch()
        {
            // arrange
            var request = GetLabelChangeActivationRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneUserOne();
            MockLabelRepository.Setup_Select_Returns_OrganizationTwoProjectOneLabelOne();

            // act
            var result = await SystemUnderTest.ChangeActivation(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Invalid);
            AssertReturnType<LabelChangeActivationResponse>(result);
            MockUserRepository.Verify_SelectById();
            MockLabelRepository.Verify_Select();
        }

        [Test]
        public async Task LabelService_ChangeActivation_Invalid_OrganizationNotFound()
        {
            // arrange
            var request = GetLabelChangeActivationRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneUserOne();
            MockLabelRepository.Setup_Select_Returns_OrganizationOneProjectOneLabelOne();
            MockOrganizationRepository.Setup_Any_Returns_True();

            // act
            var result = await SystemUnderTest.ChangeActivation(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Invalid, OrganizationNotFound);
            AssertReturnType<LabelChangeActivationResponse>(result);
            MockUserRepository.Verify_SelectById();
            MockLabelRepository.Verify_Select();
            MockOrganizationRepository.Verify_Any();
        }

        [Test]
        public async Task LabelService_ChangeActivation_Invalid_ProjectNotFound()
        {
            // arrange
            var request = GetLabelChangeActivationRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneUserOne();
            MockLabelRepository.Setup_Select_Returns_OrganizationOneProjectOneLabelOne();
            MockOrganizationRepository.Setup_Any_Returns_False();
            MockProjectRepository.Setup_Any_Returns_True();

            // act
            var result = await SystemUnderTest.ChangeActivation(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Failed, ProjectNotFound);
            AssertReturnType<LabelChangeActivationResponse>(result);
            MockUserRepository.Verify_SelectById();
            MockLabelRepository.Verify_Select();
            MockOrganizationRepository.Verify_Any();
            MockProjectRepository.Verify_Any();
        }

        [Test]
        public async Task LabelService_ChangeActivation_Failed()
        {
            // arrange
            var request = GetLabelChangeActivationRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneUserOne();
            MockLabelRepository.Setup_Select_Returns_OrganizationOneProjectOneLabelOne();
            MockOrganizationRepository.Setup_Any_Returns_False();
            MockProjectRepository.Setup_Any_Returns_False();
            MockLabelRepository.Setup_Update_Failed();

            // act
            var result = await SystemUnderTest.ChangeActivation(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Failed);
            AssertReturnType<LabelChangeActivationResponse>(result);
            MockUserRepository.Verify_SelectById();
            MockLabelRepository.Verify_Select();
            MockOrganizationRepository.Verify_Any();
            MockProjectRepository.Verify_Any();
            MockLabelRepository.Verify_Update();
        }

        [Test]
        public async Task LabelService_DeleteLabel_Success()
        {
            // arrange
            var request = GetLabelEditRequest();
            MockLabelRepository.Setup_Select_Returns_OrganizationOneProjectOneLabelOne();
            MockOrganizationRepository.Setup_Any_Returns_False();
            MockProjectRepository.Setup_Any_Returns_False();
            MockLabelTranslationRepository.Setup_Any_Return_False();
            MockLabelUnitOfWork.Setup_DoCreateWork_Returns_True();

            // act
            var result = await SystemUnderTest.EditLabel(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Success);
            AssertReturnType<LabelEditResponse>(result);
            MockLabelRepository.Verify_Select();
            MockOrganizationRepository.Verify_Any();
            MockProjectRepository.Verify_Any();
            MockLabelTranslationRepository.Verify_Any();
            MockLabelUnitOfWork.Verify_DoCreateWork();
        }

        [Test]
        public async Task LabelService_DeleteLabel_Invalid_LabelNotFound()
        {
            // arrange
            var request = GetLabelEditRequest();
            MockLabelRepository.Setup_Select_Returns_OrganizationOneProjectOneLabelOneNotExist();

            // act
            var result = await SystemUnderTest.EditLabel(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Invalid, LabelNotFound);
            AssertReturnType<LabelEditResponse>(result);
            MockLabelRepository.Verify_Select();
        }

        [Test]
        public async Task LabelService_DeleteLabel_Invalid_OrganizationNotMatch()
        {
            // arrange
            var request = GetLabelEditRequest();
            MockLabelRepository.Setup_Select_Returns_OrganizationTwoProjectOneLabelOne();
            
            // act
            var result = await SystemUnderTest.EditLabel(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Invalid);
            AssertReturnType<LabelEditResponse>(result);
            MockLabelRepository.Verify_Select();
        }

        [Test]
        public async Task LabelService_DeleteLabel_Invalid_OrganizationNotFound()
        {
            // arrange
            var request = GetLabelEditRequest();
            MockLabelRepository.Setup_Select_Returns_OrganizationOneProjectOneLabelOne();
            MockOrganizationRepository.Setup_Any_Returns_True();

            // act
            var result = await SystemUnderTest.EditLabel(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Invalid, OrganizationNotFound);
            AssertReturnType<LabelEditResponse>(result);
            MockLabelRepository.Verify_Select();
            MockOrganizationRepository.Verify_Any();
        }

        [Test]
        public async Task LabelService_DeleteLabel_Invalid_ProjectNotFound()
        {
            // arrange
            var request = GetLabelEditRequest();
            MockLabelRepository.Setup_Select_Returns_OrganizationOneProjectOneLabelOne();
            MockOrganizationRepository.Setup_Any_Returns_False();
            MockProjectRepository.Setup_Any_Returns_True();
            
            // act
            var result = await SystemUnderTest.EditLabel(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Invalid, ProjectNotFound);
            AssertReturnType<LabelEditResponse>(result);
            MockLabelRepository.Verify_Select();
            MockOrganizationRepository.Verify_Any();
            MockProjectRepository.Verify_Any();
        }

        [Test]
        public async Task LabelService_DeleteLabel_Invalid_LabelHasChildren()
        {
            // arrange
            var request = GetLabelEditRequest();
            MockLabelRepository.Setup_Select_Returns_OrganizationOneProjectOneLabelOne();
            MockOrganizationRepository.Setup_Any_Returns_False();
            MockProjectRepository.Setup_Any_Returns_False();
            MockLabelTranslationRepository.Setup_Any_Return_True();

            // act
            var result = await SystemUnderTest.EditLabel(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Invalid, LabelHasChildren);
            AssertReturnType<LabelEditResponse>(result);
            MockLabelRepository.Verify_Select();
            MockOrganizationRepository.Verify_Any();
            MockProjectRepository.Verify_Any();
            MockLabelTranslationRepository.Verify_Any();
        }

        [Test]
        public async Task LabelService_DeleteLabel_Failed()
        {
            // arrange
            var request = GetLabelEditRequest();
            MockLabelRepository.Setup_Select_Returns_OrganizationOneProjectOneLabelOne();
            MockOrganizationRepository.Setup_Any_Returns_False();
            MockProjectRepository.Setup_Any_Returns_False();
            MockLabelTranslationRepository.Setup_Any_Return_False();
            MockLabelUnitOfWork.Setup_DoCreateWork_Returns_False();

            // act
            var result = await SystemUnderTest.EditLabel(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Failed);
            AssertReturnType<LabelEditResponse>(result);
            MockLabelRepository.Verify_Select();
            MockOrganizationRepository.Verify_Any();
            MockProjectRepository.Verify_Any();
            MockLabelTranslationRepository.Verify_Any();
            MockLabelUnitOfWork.Verify_DoCreateWork();
        }

        [Test]
        public async Task LabelService_CloneLabel_Success()
        {
            // arrange
            var request = GetLabelCloneRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneAdminUserOne();
            MockOrganizationRepository.Setup_Any_Returns_False();
            MockLabelRepository.Setup_Select_Returns_OrganizationOneProjectOneLabelOne();
            MockLabelRepository.Setup_Any_Returns_True();
            MockLabelUnitOfWork.Setup_DoCloneWork_Returns_True();

            // act
            var result = await SystemUnderTest.CloneLabel(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Success);
            AssertReturnType<LabelCloneResponse>(result);
            MockUserRepository.Verify_SelectById();
            MockOrganizationRepository.Verify_Any();
            MockLabelRepository.Verify_Select();
            MockLabelRepository.Verify_Any();
            MockLabelUnitOfWork.Verify_DoCloneWork();
        }

        [Test]
        public async Task LabelService_CloneLabel_Invalid_CurrentUserNotAdmin()
        {
            // arrange
            var request = GetLabelCloneRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneUserOne();

            // act
            var result = await SystemUnderTest.CloneLabel(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Invalid);
            AssertReturnType<LabelCloneResponse>(result);
            MockUserRepository.Verify_SelectById();
        }

        [Test]
        public async Task LabelService_CloneLabel_Invalid_OrganizationNotActive()
        {
            // arrange
            var request = GetLabelCloneRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneAdminUserOne();
            MockOrganizationRepository.Setup_Any_Returns_True();
            
            // act
            var result = await SystemUnderTest.CloneLabel(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Invalid, OrganizationNotActive);
            AssertReturnType<LabelCloneResponse>(result);
            MockUserRepository.Verify_SelectById();
            MockOrganizationRepository.Verify_Any();
        }

        [Test]
        public async Task LabelService_CloneLabel_Invalid_LabelNotFound()
        {
            // arrange
            var request = GetLabelCloneRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneAdminUserOne();
            MockOrganizationRepository.Setup_Any_Returns_False();
            MockLabelRepository.Setup_Select_Returns_OrganizationOneProjectOneLabelOneNotExist();
            
            // act
            var result = await SystemUnderTest.CloneLabel(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Invalid, LabelNotFound);
            AssertReturnType<LabelCloneResponse>(result);
            MockUserRepository.Verify_SelectById();
            MockOrganizationRepository.Verify_Any();
            MockLabelRepository.Verify_Select();
        }

        [Test]
        public async Task LabelService_CloneLabel_Invalid_OrganizationNotMatch()
        {
            // arrange
            var request = GetLabelCloneRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneAdminUserOne();
            MockOrganizationRepository.Setup_Any_Returns_False();
            MockLabelRepository.Setup_Select_Returns_OrganizationTwoProjectOneLabelOne();

            // act
            var result = await SystemUnderTest.CloneLabel(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Invalid);
            AssertReturnType<LabelCloneResponse>(result);
            MockUserRepository.Verify_SelectById();
            MockOrganizationRepository.Verify_Any();
            MockLabelRepository.Verify_Select();
        }

        [Test]
        public async Task LabelService_CloneLabel_Invalid_LabelKeyMustBeUnique()
        {
            // arrange
            var request = GetLabelCloneRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneAdminUserOne();
            MockOrganizationRepository.Setup_Any_Returns_False();
            MockLabelRepository.Setup_Select_Returns_OrganizationOneProjectOneLabelOne();
            MockLabelRepository.Setup_Any_Returns_True();

            // act
            var result = await SystemUnderTest.CloneLabel(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Invalid, "label_key_must_be_unique");
            AssertReturnType<LabelCloneResponse>(result);
            MockUserRepository.Verify_SelectById();
            MockOrganizationRepository.Verify_Any();
            MockLabelRepository.Verify_Select();
            MockLabelRepository.Verify_Any();
        }

        [Test]
        public async Task LabelService_CloneLabel_Failed()
        {
            // arrange
            var request = GetLabelCloneRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneAdminUserOne();
            MockOrganizationRepository.Setup_Any_Returns_False();
            MockLabelRepository.Setup_Select_Returns_OrganizationOneProjectOneLabelOne();
            MockLabelRepository.Setup_Any_Returns_True();
            MockLabelUnitOfWork.Setup_DoCloneWork_Returns_False();

            // act
            var result = await SystemUnderTest.CloneLabel(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Failed);
            AssertReturnType<LabelCloneResponse>(result);
            MockUserRepository.Verify_SelectById();
            MockOrganizationRepository.Verify_Any();
            MockLabelRepository.Verify_Select();
            MockLabelRepository.Verify_Any();
            MockLabelUnitOfWork.Verify_DoCloneWork();
        }

        [Test]
        public async Task LabelService_RestoreLabel_Success()
        {
            // arrange
            var request = GetLabelRestoreRequestRevisionOneInIt();
            MockOrganizationRepository.Setup_Any_Returns_False();
            MockLabelRepository.Setup_Select_Returns_OrganizationOneProjectOneLabelOne();
            MockLabelRepository.Setup_SelectRevisions_Returns_OrganizationOneProjectOneLabelOneRevisionsRevisionOneInIt();
            MockLabelRepository.Setup_RestoreRevision_Returns_True();

            // act
            var result = await SystemUnderTest.RestoreLabel(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Success);
            AssertReturnType<LabelRestoreResponse>(result);
            MockOrganizationRepository.Verify_Any();
            MockLabelRepository.Verify_Select();
            MockLabelRepository.Verify_SelectRevisions();
            MockLabelRepository.Verify_RestoreRevision();
        }

        [Test]
        public async Task LabelService_RestoreLabel_Invalid_OrganizationNotActive()
        {
            // arrange
            var request = GetLabelRestoreRequestRevisionOneInIt();
            MockOrganizationRepository.Setup_Any_Returns_True();

            // act
            var result = await SystemUnderTest.RestoreLabel(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Invalid, OrganizationNotActive);
            AssertReturnType<LabelRestoreResponse>(result);
            MockOrganizationRepository.Verify_Any();
        }

        [Test]
        public async Task LabelService_RestoreLabel_Invalid_LabelNotFound()
        {
            // arrange
            var request = GetLabelRestoreRequestRevisionOneInIt();
            MockOrganizationRepository.Setup_Any_Returns_False();
            MockLabelRepository.Setup_Select_Returns_OrganizationOneProjectOneLabelOneNotExist();

            // act
            var result = await SystemUnderTest.RestoreLabel(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Invalid, LabelNotFound);
            AssertReturnType<LabelRestoreResponse>(result);
            MockOrganizationRepository.Verify_Any();
            MockLabelRepository.Verify_Select();
        }

        [Test]
        public async Task LabelService_RestoreLabel_Invalid_LabelRevisionNotFound()
        {
            // arrange
            var request = GetLabelRestoreRequestRevisionTwoInIt();
            MockOrganizationRepository.Setup_Any_Returns_False();
            MockLabelRepository.Setup_Select_Returns_OrganizationOneProjectOneLabelOne();
            MockLabelRepository.Setup_SelectRevisions_Returns_OrganizationOneProjectOneLabelOneRevisionsRevisionOneInIt();

            // act
            var result = await SystemUnderTest.RestoreLabel(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Success);
            AssertReturnType<LabelRestoreResponse>(result);
            MockOrganizationRepository.Verify_Any();
            MockLabelRepository.Verify_Select();
            MockLabelRepository.Verify_SelectRevisions();
        }

        [Test]
        public async Task LabelService_RestoreLabel_Failed()
        {
            // arrange
            var request = GetLabelRestoreRequestRevisionOneInIt();
            MockOrganizationRepository.Setup_Any_Returns_False();
            MockLabelRepository.Setup_Select_Returns_OrganizationOneProjectOneLabelOne();
            MockLabelRepository.Setup_SelectRevisions_Returns_OrganizationOneProjectOneLabelOneRevisionsRevisionOneInIt();
            MockLabelRepository.Setup_RestoreRevision_Returns_True();

            // act
            var result = await SystemUnderTest.RestoreLabel(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Failed);
            AssertReturnType<LabelRestoreResponse>(result);
            MockOrganizationRepository.Verify_Any();
            MockLabelRepository.Verify_Select();
            MockLabelRepository.Verify_SelectRevisions();
            MockLabelRepository.Verify_RestoreRevision();
        }

        [Test]
        public async Task LabelService_CreateTranslation_Success()
        {
            // arrange
            var request = GetLabelTranslationCreateRequest();
            MockLabelRepository.Setup_Select_Returns_OrganizationOneProjectOneLabelOne();
            MockOrganizationRepository.Setup_Any_Returns_False();
            MockProjectRepository.Setup_Any_Returns_False();
            MockLanguageRepository.Setup_Select_Returns_Language();
            MockLabelTranslationRepository.Setup_Any_Return_False();
            MockLabelUnitOfWork.Setup_DoCreateTranslationWork_Returns_True();

            // act
            var result = await SystemUnderTest.CreateTranslation(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Success);
            AssertReturnType<LabelTranslationCreateResponse>(result);
            MockLabelRepository.Verify_Select();
            MockOrganizationRepository.Verify_Any();
            MockProjectRepository.Verify_Any();
            MockLanguageRepository.Verify_Select();
            MockLabelTranslationRepository.Verify_Any();
            MockLabelUnitOfWork.Verify_DoCreateTranslationWork();
        }

        [Test]
        public async Task LabelService_CreateTranslation_Invalid_LabelNotFound()
        {
            // arrange
            var request = GetLabelTranslationCreateRequest();
            MockLabelRepository.Setup_Select_Returns_OrganizationOneProjectOneLabelOneNotExist();

            // act
            var result = await SystemUnderTest.CreateTranslation(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Invalid, LabelNotFound);
            AssertReturnType<LabelTranslationCreateResponse>(result);
            MockLabelRepository.Verify_SelectById();
            MockLabelUnitOfWork.Verify_DoCreateTranslationWork();
        }

        [Test]
        public async Task LabelService_CreateTranslation_Invalid_LabelNotActive()
        {
            // arrange
            var request = GetLabelTranslationCreateRequest();
            MockLabelRepository.Setup_Select_Returns_OrganizationOneProjectOneLabelOneNotActive();

            // act
            var result = await SystemUnderTest.CreateTranslation(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Invalid, LabelNotActive);
            AssertReturnType<LabelTranslationCreateResponse>(result);
            MockLabelRepository.Verify_SelectById();
            MockLabelUnitOfWork.Verify_DoCreateTranslationWork();
        }

        [Test]
        public async Task LabelService_CreateTranslation_Invalid_OrganizationNotMatch()
        {
            // arrange
            var request = GetLabelTranslationCreateRequest();
            MockLabelRepository.Setup_Select_Returns_OrganizationTwoProjectOneLabelOne();
            
            // act
            var result = await SystemUnderTest.CreateTranslation(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Invalid);
            AssertReturnType<LabelTranslationCreateResponse>(result);
            MockLabelRepository.Verify_Select();
        }


        [Test]
        public async Task LabelService_CreateTranslation_Invalid_OrganizationNotActive()
        {
            // arrange
            var request = GetLabelTranslationCreateRequest();
            MockLabelRepository.Setup_Select_Returns_OrganizationOneProjectOneLabelOne();
            MockOrganizationRepository.Setup_Any_Returns_True();
            
            // act
            var result = await SystemUnderTest.CreateTranslation(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Invalid, OrganizationNotActive);
            AssertReturnType<LabelTranslationCreateResponse>(result);
            MockLabelRepository.Verify_Select();
            MockOrganizationRepository.Verify_Any();
        }

        [Test]
        public async Task LabelService_CreateTranslation_Invalid_ProjectNotActive()
        {
            // arrange
            var request = GetLabelTranslationCreateRequest();
            MockLabelRepository.Setup_Select_Returns_OrganizationOneProjectOneLabelOne();
            MockOrganizationRepository.Setup_Any_Returns_False();
            MockProjectRepository.Setup_Any_Returns_True();

            // act
            var result = await SystemUnderTest.CreateTranslation(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Invalid, ProjectNotActive);
            AssertReturnType<LabelTranslationCreateResponse>(result);
            MockLabelRepository.Verify_Select();
            MockOrganizationRepository.Verify_Any();
            MockProjectRepository.Verify_Any();
        }
    }
}