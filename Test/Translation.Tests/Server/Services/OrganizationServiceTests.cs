using System.Threading.Tasks;

using NUnit.Framework;

using Translation.Common.Contracts;
using Translation.Common.Enumerations;
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
    public class OrganizationServiceTests : ServiceBaseTests
    {
        public IOrganizationService SystemUnderTest { get; set; }

        [SetUp]
        public void run_before_every_test()
        {
            SystemUnderTest = Container.Resolve<IOrganizationService>();
        }

        [Test]
        public async Task OrganizationService_CreateOrganizationWithAdmin_Success()
        {
            // arrange
            var request = GetSignUpRequest();
            MockUserRepository.Setup_Select_Returns_OrganizationOneUserOneNotExist();
            MockOrganizationRepository.Setup_Select_Returns_OrganizationOneNotExist();
            MockLanguageRepository.Setup_Select_Returns_Language();
            MockSignUpUnitOfWork.Setup_DoWork_Returns_TrueOrganizationUser();

            // act
            var result = await SystemUnderTest.CreateOrganizationWithAdmin(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Success);
            AssertReturnType<SignUpResponse>(result);
            MockUserRepository.Verify_Select();
            MockOrganizationRepository.Verify_Select();
            MockLanguageRepository.Verify_Select();
            MockSignUpUnitOfWork.Verify_DoWork();
        }

        [Test]
        public async Task OrganizationService_CreateOrganizationWithAdmin_Invalid_EmailAlreadyExist()
        {
            // arrange
            var request = GetSignUpRequest();
            MockUserRepository.Setup_Select_Returns_OrganizationOneUserOne();

            // act
            var result = await SystemUnderTest.CreateOrganizationWithAdmin(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Invalid, "email_already_exist");
            AssertReturnType<SignUpResponse>(result);
            MockUserRepository.Verify_Select();
        }

        [Test]
        public async Task OrganizationService_CreateOrganizationWithAdmin_Failed_OrganizationNameMustBeUnique()
        {
            // arrange
            var request = GetSignUpRequest();
            MockUserRepository.Setup_Select_Returns_OrganizationOneUserOneNotExist();
            MockOrganizationRepository.Setup_Select_Returns_OrganizationOne();

            // act
            var result = await SystemUnderTest.CreateOrganizationWithAdmin(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Failed, OrganizationNameMustBeUnique);
            AssertReturnType<SignUpResponse>(result);
            MockUserRepository.Verify_Select();
            MockOrganizationRepository.Verify_Select();
        }

        [Test]
        public async Task OrganizationService_CreateOrganizationWithAdmin_Failed()
        {
            // arrange
            var request = GetSignUpRequest();
            MockUserRepository.Setup_Select_Returns_OrganizationOneUserOneNotExist();
            MockOrganizationRepository.Setup_Select_Returns_OrganizationOneNotExist();
            MockLanguageRepository.Setup_Select_Returns_Language();
            MockSignUpUnitOfWork.Setup_DoWork_Returns_FalseOrganizationUser();

            // act
            var result = await SystemUnderTest.CreateOrganizationWithAdmin(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Failed);
            AssertReturnType<SignUpResponse>(result);
            MockUserRepository.Verify_Select();
            MockOrganizationRepository.Verify_Select();
            MockLanguageRepository.Verify_Select();
            MockSignUpUnitOfWork.Verify_DoWork();
        }

        [Test]
        public void OrganizationService_GetOrganization_Success()
        {
            // arrange
            var request = GetOrganizationReadRequest();

            // act
            var result = SystemUnderTest.GetOrganization(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Success);
            AssertReturnType<OrganizationReadResponse>(result);
        }

        [Test]
        public async Task OrganizationService_GetOrganizations_Success_SelectAfter()
        {
            // arrange
            var request = GetOrganizationReadListRequestForSelectAfter();
            MockOrganizationRepository.Setup_SelectAfter_Returns_Organizations();
            MockOrganizationRepository.Setup_Count_Returns_Ten();

            // act
            var result = await SystemUnderTest.GetOrganizations(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Success);
            AssertReturnType<OrganizationReadListResponse>(result);
            AssertPagingInfoForSelectAfter(request.PagingInfo, Ten);
            MockOrganizationRepository.Verify_SelectAfter();
            MockOrganizationRepository.Verify_Count();
        }

        [Test]
        public async Task OrganizationService_GetOrganizations_Success_SelectMany()
        {
            // arrange
            var request = GetOrganizationReadListRequestForSelectMany();
            MockOrganizationRepository.Setup_SelectAfter_Returns_Organizations();
            MockOrganizationRepository.Setup_Count_Returns_Ten();

            // act
            var result = await SystemUnderTest.GetOrganizations(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Success);
            AssertReturnType<OrganizationReadListResponse>(result);
            AssertPagingInfoForSelectMany(request.PagingInfo, Ten);
            MockOrganizationRepository.Verify_SelectMany();
            MockOrganizationRepository.Verify_Count();
        }

        [Test]
        public async Task OrganizationService_GetOrganizationRevisions_Success()
        {
            // arrange
            var request = GetOrganizationRevisionReadListRequest();
            MockOrganizationRepository.Setup_Select_Returns_OrganizationOne();
            MockOrganizationRepository.Setup_SelectRevisions_Returns_OrganizationOneRevisions();

            // act
            var result = await SystemUnderTest.GetOrganizationRevisions(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Success);
            AssertReturnType<OrganizationRevisionReadListResponse>(result);
            MockOrganizationRepository.Verify_Select();
            MockOrganizationRepository.Verify_SelectRevisions();
        }

        [Test]
        public async Task OrganizationService_GetOrganizationRevisions_Invalid_OrganizationNotFound()
        {
            // arrange
            var request = GetOrganizationRevisionReadListRequest();
            MockOrganizationRepository.Setup_Select_Returns_OrganizationOneNotExist();

            // act
            var result = await SystemUnderTest.GetOrganizationRevisions(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Invalid, OrganizationNotFound);
            AssertReturnType<OrganizationRevisionReadListResponse>(result);
            MockOrganizationRepository.Verify_Select();
        }

        [Test]
        public async Task OrganizationService_EditOrganization_Success()
        {
            // arrange
            var request = GetOrganizationEditRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneAdminUserOne();
            MockOrganizationRepository.Setup_Any_Returns_False();
            MockOrganizationRepository.Setup_Update_Success();

            // act
            var result = await SystemUnderTest.EditOrganization(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Success);
            AssertReturnType<OrganizationEditResponse>(result);
            MockUserRepository.Verify_SelectById();
            MockOrganizationRepository.Verify_Any();
            MockOrganizationRepository.Verify_Update();
        }

        [Test]
        public async Task OrganizationService_EditOrganization_Invalid_CurrentUserNotAdmin()
        {
            // arrange
            var request = GetOrganizationEditRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneUserOne();

            // act
            var result = await SystemUnderTest.EditOrganization(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Invalid);
            AssertReturnType<OrganizationEditResponse>(result);
            MockUserRepository.Verify_SelectById();
        }

        [Test]
        public async Task OrganizationService_EditOrganization_Invalid_OrganizationNotActive()
        {
            // arrange
            var request = GetOrganizationEditRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneAdminUserOne();
            MockOrganizationRepository.Setup_Any_Returns_True();

            // act
            var result = await SystemUnderTest.EditOrganization(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Invalid, OrganizationNotActive);
            AssertReturnType<OrganizationEditResponse>(result);
            MockUserRepository.Verify_SelectById();
            MockOrganizationRepository.Verify_Any();
        }

        [Test]
        public async Task OrganizationService_EditOrganization_Invalid_OrganizationNotMatch()
        {
            // arrange
            var request = GetOrganizationEditRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneAdminUserOne();
            MockOrganizationRepository.Setup_Select_Returns_OrganizationTwo();

            // act
            var result = await SystemUnderTest.EditOrganization(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Invalid);
            AssertReturnType<OrganizationEditResponse>(result);
            MockUserRepository.Verify_SelectById();
            MockOrganizationRepository.Verify_Select();
        }

        [Ignore("*-> How can we test the 'organization name must be unique' check while there are two Any method call of the OrganizationRepository?")]
        [Test]
        public async Task OrganizationService_EditOrganization_Invalid_OrganizationNameMustBeUnique()
        {
            // arrange
            var request = GetOrganizationEditRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneAdminUserOne();
            MockOrganizationRepository.Setup_Any_Returns_False();
           
           

            // act
            var result = await SystemUnderTest.EditOrganization(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Invalid, OrganizationNameMustBeUnique);
            AssertReturnType<OrganizationEditResponse>(result);
            MockUserRepository.Verify_SelectById();
            MockOrganizationRepository.Verify_Any();
        }

        [Test]
        public async Task OrganizationService_EditOrganization_Failed()
        {
            // arrange
            var request = GetOrganizationEditRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneAdminUserOne();
            MockOrganizationRepository.Setup_Any_Returns_False();
            MockOrganizationRepository.Setup_Select_Returns_OrganizationOne();
            MockOrganizationRepository.Setup_Update_Failed();

            // act
            var result = await SystemUnderTest.EditOrganization(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Failed);
            AssertReturnType<OrganizationEditResponse>(result);
            MockUserRepository.Verify_SelectById();
            MockOrganizationRepository.Verify_Any();
            MockOrganizationRepository.Verify_Select();
            MockOrganizationRepository.Verify_Update();
        }
    }
}