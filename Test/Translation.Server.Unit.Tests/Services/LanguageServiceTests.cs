using System.Threading.Tasks;

using NUnit.Framework;

using Translation.Common.Contracts;
using StandardUtils.Enumerations;
using Translation.Common.Models.Responses.Language;
using Translation.Server.Unit.Tests.RepositorySetupHelpers;

using static Translation.Common.Tests.TestHelpers.FakeRequestTestHelper;
using static Translation.Common.Tests.TestHelpers.FakeConstantTestHelper;
using static Translation.Server.Unit.Tests.TestHelpers.AssertViewModelTestHelper;
using static Translation.Server.Unit.Tests.TestHelpers.AssertResponseTestHelper;

namespace Translation.Server.Unit.Tests.Services
{
    [TestFixture]
    public class LanguageServiceTests : ServiceBaseTests
    {
        public ILanguageService SystemUnderTest { get; set; }

        [SetUp]
        public void run_before_every_test()
        {
            Refresh();
            SystemUnderTest = Container.Resolve<ILanguageService>();
        }

        [Test]
        public async Task LanguageService_GetLanguage_Success()
        {
            // arrange
            var request = GetLanguageReadRequest();
            MockLanguageRepository.Setup_Select_Returns_Language();


            // act
            var result = await SystemUnderTest.GetLanguage(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Success);
            AssertReturnType<LanguageReadResponse>(result);
            MockLanguageRepository.Verify_Select();
        }

        [Test]
        public async Task LanguageService_GetLanguage_Failed_LanguageNotFound()
        {
            // arrange
            var request = GetLanguageReadRequest();
            MockLanguageRepository.Setup_Select_Returns_LanguageNotExist();

            // act
            var result = await SystemUnderTest.GetLanguage(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Failed, LanguageNotFound);
            AssertReturnType<LanguageReadResponse>(result);
            MockLanguageRepository.Verify_Select();
        }

        [Test]
        public async Task LanguageService_GetLanguages_Success_SelectAfter()
        {
            // arrange
            var request = GetLanguageReadListRequestForSelectAfter();
            MockLanguageRepository.Setup_SelectAfter_Returns_Languages();
            MockLanguageRepository.Setup_Count_Returns_Ten();

            // act
            var result = await SystemUnderTest.GetLanguages(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Success);
            AssertReturnType<LanguageReadListResponse>(result);
            AssertPagingInfoForSelectAfter(request.PagingInfo, Ten);
            MockLanguageRepository.Verify_SelectAfter();
            MockLanguageRepository.Verify_Count();
        }

        [Test]
        public async Task LanguageService_GetLanguages_Success_SelectMany()
        {
            // arrange
            var request = GetLanguageReadListRequestForSelectMany();
            MockLanguageRepository.Setup_SelectMany_Returns_Languages();
            MockLanguageRepository.Setup_Count_Returns_Ten();

            // act
            var result = await SystemUnderTest.GetLanguages(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Success);
            AssertReturnType<LanguageReadListResponse>(result);
            AssertPagingInfoForSelectMany(request.PagingInfo, Ten);
            MockLanguageRepository.Verify_SelectMany();
            MockLanguageRepository.Verify_Count();
        }

        [Test]
        public async Task LanguageService_GetLanguageRevisions_Success()
        {
            //arrange
            var request = GetLanguageRevisionReadListRequest();
            MockLanguageRepository.Setup_Select_Returns_Language();
            MockLanguageRepository.Setup_SelectRevisions_Returns_LanguageRevisionsRevisionOneInIt();
            //act
            var result = await SystemUnderTest.GetLanguageRevisions(request);

            //assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Success);
            AssertReturnType<LanguageRevisionReadListResponse>(result);
            MockLanguageRepository.Verify_Select();
            MockLanguageRepository.Verify_SelectRevisions();
        }

        [Test]
        public async Task LanguageService_GetLanguageRevisions_Failed_LanguageNotFound()
        {
            //arrange
            var request = GetLanguageRevisionReadListRequest();
            MockLanguageRepository.Setup_Select_Returns_LanguageNotExist();

            //act
            var result = await SystemUnderTest.GetLanguageRevisions(request);

            //assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Failed, LanguageNotFound);
            AssertReturnType<LanguageRevisionReadListResponse>(result);
            MockLanguageRepository.Verify_Select();

        }

        [Test]
        public async Task LanguageService_CreateLanguage_Success()
        {
            //arrange
            var request = GetLanguageCreateRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneSuperAdminUserOne();
            MockLanguageRepository.Setup_Any_Returns_False();
            MockLanguageRepository.Setup_Insert_Success();
            //act
            var result = await SystemUnderTest.CreateLanguage(request);

            //assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Success);
            AssertReturnType<LanguageCreateResponse>(result);
            MockUserRepository.Verify_SelectById();
            MockLanguageRepository.Verify_Any();
            MockLanguageRepository.Verify_Insert();
        }

        [Test]
        public async Task LanguageService_CreateLanguage_Failed()
        {
            //arrange
            var request = GetLanguageCreateRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneSuperAdminUserOne();
            MockLanguageRepository.Setup_Any_Returns_False();
            MockLanguageRepository.Setup_Insert_Failed();
            //act
            var result = await SystemUnderTest.CreateLanguage(request);

            //assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Failed);
            AssertReturnType<LanguageCreateResponse>(result);
            MockUserRepository.Verify_SelectById();
            MockLanguageRepository.Verify_Any();
            MockLanguageRepository.Verify_Insert();
        }

        [Test]
        public async Task LanguageService_CreateLanguage_Invalid_NameMustBeUnique()
        {
            //arrange
            var request = GetLanguageCreateRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneSuperAdminUserOne();
            MockLanguageRepository.Setup_Any_Returns_True();

            //act
            var result = await SystemUnderTest.CreateLanguage(request);

            //assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Invalid, LanguageNameMustBeUnique);
            AssertReturnType<LanguageCreateResponse>(result);
            MockUserRepository.Verify_SelectById();
            MockLanguageRepository.Verify_Any();
        }

        [Test]
        public async Task LanguageService_CreateLanguage_Invalid_CurrentUserNotSuperAdmin()
        {
            //arrange
            var request = GetLanguageCreateRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneAdminUserOne();

            //act
            var result = await SystemUnderTest.CreateLanguage(request);

            //assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Invalid, UserNotSuperAdmin);
            AssertReturnType<LanguageCreateResponse>(result);
            MockUserRepository.Verify_SelectById();
        }

        [Test]
        public async Task LanguageService_EditLanguage_Success()
        {
            // arrange
            var request = GetLanguageEditRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneSuperAdminUserOne();
            MockLanguageRepository.Setup_Select_Returns_Language();
            MockLanguageRepository.Setup_Any_Returns_False();
            MockLanguageRepository.Setup_Update_Returns_True();

            // act
            var result = await SystemUnderTest.EditLanguage(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Success);
            AssertReturnType<LanguageEditResponse>(result);
            MockUserRepository.Verify_SelectById();
            MockLanguageRepository.Verify_Select();
            MockLanguageRepository.Verify_Any();
            MockLanguageRepository.Verify_Update();
        }

        [Test]
        public async Task LanguageService_EditLanguage_NotDifferent_Success()
        {
            // arrange
            var request = GetNotDifferentLanguageEditRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneSuperAdminUserOne();
            MockLanguageRepository.Setup_Select_Returns_Language();

            // act
            var result = await SystemUnderTest.EditLanguage(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Success);
            AssertReturnType<LanguageEditResponse>(result);
            MockUserRepository.Verify_SelectById();
            MockLanguageRepository.Verify_Select();
        }

        [Test]
        public async Task LanguageService_EditLanguage_Failed()
        {
            // arrange
            var request = GetLanguageEditRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneSuperAdminUserOne();
            MockLanguageRepository.Setup_Select_Returns_Language();
            MockLanguageRepository.Setup_Any_Returns_False();
            MockLanguageRepository.Setup_Update_Returns_False();

            // act
            var result = await SystemUnderTest.EditLanguage(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Failed);
            AssertReturnType<LanguageEditResponse>(result);
            MockUserRepository.Verify_SelectById();
            MockLanguageRepository.Verify_Select();
            MockLanguageRepository.Verify_Any();
            MockLanguageRepository.Verify_Update();
        }

        [Test]
        public async Task LanguageService_EditLanguage_Invalid_CurrentUserNotSuperAdmin()
        {
            // arrange
            var request = GetLanguageEditRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneUserOne();

            // act
            var result = await SystemUnderTest.EditLanguage(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Invalid, UserNotSuperAdmin);
            AssertReturnType<LanguageEditResponse>(result);
            MockUserRepository.Verify_SelectById();
        }

        [Test]
        public async Task LanguageService_EditLanguage_Failed_LanguageNotFound()
        {
            // arrange
            var request = GetLanguageEditRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneSuperAdminUserOne();
            MockLanguageRepository.Setup_Select_Returns_LanguageNotExist();


            // act
            var result = await SystemUnderTest.EditLanguage(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Failed, LanguageNotFound);
            AssertReturnType<LanguageEditResponse>(result);
            MockUserRepository.Verify_SelectById();
            MockLanguageRepository.Verify_Select();
        }

        [Test]
        public async Task LanguageService_EditLanguage_Invalid_LanguageAlreadyExist()
        {
            // arrange
            var request = GetLanguageEditRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneSuperAdminUserOne();
            MockLanguageRepository.Setup_Select_Returns_Language();
            MockLanguageRepository.Setup_Any_Returns_True();

            // act
            var result = await SystemUnderTest.EditLanguage(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Failed, "language_already_exist");
            AssertReturnType<LanguageEditResponse>(result);
            MockUserRepository.Verify_SelectById();
            MockLanguageRepository.Verify_Select();
            MockLanguageRepository.Verify_Any();
        }

        [Test]
        public async Task LanguageService_DeleteLanguage_Success()
        {
            // arrange
            var request = GetLanguageDeleteRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneSuperAdminUserOne();
            MockLanguageRepository.Setup_Select_Returns_Language();
            MockLanguageRepository.Setup_Delete_Success();

            // act
            var result = await SystemUnderTest.DeleteLanguage(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Success);
            AssertReturnType<LanguageDeleteResponse>(result);
            MockUserRepository.Verify_SelectById();
            MockLanguageRepository.Verify_Select();
            MockLanguageRepository.Verify_Delete();
        }

        [Test]
        public async Task LanguageService_DeleteLanguage_Failed()
        {
            // arrange
            var request = GetLanguageDeleteRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneSuperAdminUserOne();
            MockLanguageRepository.Setup_Select_Returns_Language();
            MockLanguageRepository.Setup_Delete_Failed();

            // act
            var result = await SystemUnderTest.DeleteLanguage(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Failed);
            AssertReturnType<LanguageDeleteResponse>(result);
            MockUserRepository.Verify_SelectById();
            MockLanguageRepository.Verify_Select();
            MockLanguageRepository.Verify_Delete();
        }

        [Test]
        public async Task LanguageService_DeleteLanguage_Invalid_CurrentUserNotAdmin()
        {
            // arrange
            var request = GetLanguageDeleteRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneUserOne();

            // act
            var result = await SystemUnderTest.DeleteLanguage(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Invalid, UserNotSuperAdmin);
            AssertReturnType<LanguageDeleteResponse>(result);
            MockUserRepository.Verify_SelectById();
        }

        [Test]
        public async Task LanguageService_DeleteLanguage_Failed_LanguageNotFound()
        {
            // arrange
            var request = GetLanguageDeleteRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneSuperAdminUserOne();
            MockLanguageRepository.Setup_Select_Returns_LanguageNotExist();

            // act
            var result = await SystemUnderTest.DeleteLanguage(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Failed, LanguageNotFound);
            AssertReturnType<LanguageDeleteResponse>(result);
            MockUserRepository.Verify_SelectById();
            MockLanguageRepository.Verify_Select();
        }

        [Test]
        public async Task LanguageService_RestoreLanguage_Success()
        {
            // arrange
            var request = GetLanguageRestoreRequest();
            MockLanguageRepository.Setup_Select_Returns_Language();
            MockLanguageRepository.Setup_SelectRevisions_Returns_LanguageRevisionsRevisionOneInIt();
            MockLanguageRepository.Setup_RestoreRevision_Returns_True();
            // act
            var result = await SystemUnderTest.RestoreLanguage(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Success);
            AssertReturnType<LanguageRestoreResponse>(result);
            MockLanguageRepository.Verify_Select();
            MockLanguageRepository.Verify_SelectRevisions();
            MockLanguageRepository.Verify_RestoreRevision();

        }

        [Test]
        public async Task LanguageService_RestoreLanguage_Failed()
        {
            // arrange
            var request = GetLanguageRestoreRequest();
            MockLanguageRepository.Setup_Select_Returns_Language();
            MockLanguageRepository.Setup_SelectRevisions_Returns_LanguageRevisionsRevisionOneInIt();
            MockLanguageRepository.Setup_RestoreRevision_Returns_False();

            // act
            var result = await SystemUnderTest.RestoreLanguage(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Failed);
            AssertReturnType<LanguageRestoreResponse>(result);
            MockLanguageRepository.Verify_Select();
            MockLanguageRepository.Verify_SelectRevisions();
            MockLanguageRepository.Verify_RestoreRevision();
        }

        [Test]
        public async Task LanguageService_RestoreLanguage_Failed_LanguageNotFound()
        {
            // arrange
            var request = GetLanguageRestoreRequest();
            MockLanguageRepository.Setup_Select_Returns_LanguageNotExist();

            // act
            var result = await SystemUnderTest.RestoreLanguage(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Failed, LanguageNotFound);
            AssertReturnType<LanguageRestoreResponse>(result);
            MockLanguageRepository.Verify_Select();
        }

        [Test]
        public async Task LanguageService_RestoreLanguage_Failed_LanguageRevisionNotFound()
        {
            // arrange
            var request = GetLanguageRestoreRequest();
            MockLanguageRepository.Setup_Select_Returns_Language();
            MockLanguageRepository.Setup_SelectRevisions_Returns_LanguageRevisionsRevisionTwoInIt();

            // act
            var result = await SystemUnderTest.RestoreLanguage(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Failed, LanguageRevisionNotFound);
            AssertReturnType<LanguageRestoreResponse>(result);
            MockLanguageRepository.Verify_Select();
            MockLanguageRepository.Verify_SelectRevisions();
        }
    }
}