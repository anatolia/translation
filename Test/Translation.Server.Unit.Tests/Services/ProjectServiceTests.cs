using System.Threading.Tasks;
using Autofac;
using NUnit.Framework;

using Translation.Common.Contracts;
using StandardUtils.Enumerations;
using Translation.Common.Models.Responses.Project;
using Translation.Server.Unit.Tests.RepositorySetupHelpers;
using Translation.Server.Unit.Tests.UnitOfWorkSetupHelper;

using static Translation.Common.Tests.TestHelpers.FakeRequestTestHelper;
using static Translation.Common.Tests.TestHelpers.FakeConstantTestHelper;
using static Translation.Server.Unit.Tests.TestHelpers.AssertResponseTestHelper;
using static Translation.Server.Unit.Tests.TestHelpers.AssertViewModelTestHelper;

namespace Translation.Server.Unit.Tests.Services
{
    [TestFixture]
    public class ProjectServiceTests : ServiceBaseTests
    {
        public IProjectService SystemUnderTest { get; set; }

        [SetUp]
        public void run_before_every_test()
        {
            Refresh();
            SystemUnderTest = Container.Resolve<IProjectService>();
        }

        [Ignore("Get current organizationUid")]
        [Test]
        public async Task ProjectService_GetProjects_Invalid_OrganizationNotMatch()
        {
            // arrange
            var request = GetProjectReadListRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationTwoUserOne();

            // act
            var result = await SystemUnderTest.GetProjects(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Invalid);
            AssertReturnType<ProjectReadListResponse>(result);
            MockUserRepository.Verify_SelectById();
        }

        [Test]
        public async Task ProjectService_GetProjects_Success_SelectAfter()
        {
            // arrange
            var request = GetProjectReadListRequestForSelectAfter();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneUserOne();
            MockProjectRepository.Setup_SelectAfter_Returns_Projects();
            MockProjectRepository.Setup_Count_Returns_Ten();

            // act
            var result = await SystemUnderTest.GetProjects(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Success);
            AssertReturnType<ProjectReadListResponse>(result);
            AssertPagingInfoForSelectAfter(request.PagingInfo, Ten);
            MockUserRepository.Verify_SelectById();
            MockProjectRepository.Verify_SelectAfter();
            MockProjectRepository.Verify_Count();
        }

        [Test]
        public async Task ProjectService_GetProjects_Success_SelectMany()
        {
            // arrange
            var request = GetProjectReadListRequestForSelectMany();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneUserOne();
            MockProjectRepository.Setup_SelectMany_Returns_Projects();
            MockProjectRepository.Setup_Count_Returns_Ten();

            // act
            var result = await SystemUnderTest.GetProjects(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Success);
            AssertReturnType<ProjectReadListResponse>(result);
            AssertPagingInfoForSelectMany(request.PagingInfo, Ten);
            MockUserRepository.Verify_SelectById();
            MockProjectRepository.Verify_SelectMany();
            MockProjectRepository.Verify_Count();
        }

        [Test]
        public async Task ProjectService_GetProjectRevisions_Failed_ProjectNotFound()
        {
            // arrange
            var request = GetProjectRevisionReadListRequest();
            MockProjectRepository.Setup_Select_Returns_OrganizationOneProjectOneNotExist();

            // act
            var result = await SystemUnderTest.GetProjectRevisions(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Failed, ProjectNotFound);
            AssertReturnType<ProjectRevisionReadListResponse>(result);
            MockProjectRepository.Verify_Select();
        }

        [Test]
        public async Task ProjectService_GetProjectRevisions_Success()
        {
            // arrange
            var request = GetProjectRevisionReadListRequest();
            MockProjectRepository.Setup_Select_Returns_OrganizationOneProjectOne();
            MockProjectRepository.Setup_SelectRevisions_Returns_OrganizationOneProjectOneRevisions();

            // act
            var result = await SystemUnderTest.GetProjectRevisions(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Success);
            AssertReturnType<ProjectRevisionReadListResponse>(result);
            MockProjectRepository.Verify_Select();
            MockProjectRepository.Verify_SelectRevisions();
        }

        [Test]
        public async Task ProjectService_GetProject_Success()
        {
            // arrange
            var request = GetProjectReadRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneAdminUserOne();
            MockProjectRepository.Setup_Select_Returns_OrganizationOneProjectOne();

            // act
            var result = await SystemUnderTest.GetProject(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Success);
            AssertReturnType<ProjectReadResponse>(result);
            MockUserRepository.Verify_SelectById();
            MockProjectRepository.Verify_Select();
        }

        [Test]
        public async Task ProjectService_GetProject_Failed_ProjectNotFound()
        {
            // arrange
            var request = GetProjectReadRequest();
            MockProjectRepository.Setup_Select_Returns_OrganizationOneProjectOneNotExist();

            // act
            var result = await SystemUnderTest.GetProject(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Failed, ProjectNotFound);
            AssertReturnType<ProjectReadResponse>(result);
            MockProjectRepository.Verify_Select();
        }

        [Test]
        public async Task ProjectService_GetProject_Invalid_OrganizationNotMatch()
        {
            // arrange
            var request = GetProjectReadRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneAdminUserOne();
            MockProjectRepository.Setup_Select_Returns_OrganizationTwoProjectOne();

            // act
            var result = await SystemUnderTest.GetProject(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Invalid);
            AssertReturnType<ProjectReadResponse>(result);
            MockUserRepository.Verify_SelectById();
            MockProjectRepository.Verify_Select();
        }

        [Test]
        public async Task ProjectService_GetProjectBySlug_Success()
        {
            // arrange
            var request = GetProjectReadBySlugRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneAdminUserOne();
            MockProjectRepository.Setup_Select_Returns_OrganizationOneProjectOne();

            // act
            var result = await SystemUnderTest.GetProjectBySlug(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Success);
            AssertReturnType<ProjectReadBySlugResponse>(result);
            MockUserRepository.Verify_SelectById();
            MockProjectRepository.Verify_Select();
        }

        [Test]
        public async Task ProjectService_GetProjectBySlug_Failed_ProjectNotFound()
        {
            // arrange
            var request = GetProjectReadBySlugRequest();
            MockProjectRepository.Setup_Select_Returns_InvalidProject();

            // act
            var result = await SystemUnderTest.GetProjectBySlug(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Failed, ProjectNotFound);
            AssertReturnType<ProjectReadBySlugResponse>(result);
            MockProjectRepository.Verify_Select();
        }

        [Test]
        public async Task ProjectService_GetProjectBySlug_Invalid_OrganizationNotMatch()
        {
            // arrange
            var request = GetProjectReadBySlugRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneAdminUserOne();
            MockProjectRepository.Setup_Select_Returns_OrganizationTwoProjectOne();

            // act
            var result = await SystemUnderTest.GetProjectBySlug(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Invalid);
            AssertReturnType<ProjectReadBySlugResponse>(result);
            MockUserRepository.Verify_SelectById();
            MockProjectRepository.Verify_Select();
        }

        [Test]
        public async Task ProjectService_CreateProject_Failed_ProjectNameAndSlugMustBeUnique()
        {
            // arrange
            var request = GetProjectCreateRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneAdminUserOne();
            MockOrganizationRepository.Setup_Any_Returns_False();
            MockProjectRepository.Setup_Any_Returns_True();

            // act
            var result = await SystemUnderTest.CreateProject(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Failed, "project_name_and_slug_must_be_unique");
            AssertReturnType<ProjectCreateResponse>(result);
            MockUserRepository.Verify_SelectById();
            MockOrganizationRepository.Verify_Any();
            MockProjectRepository.Verify_Any();
        }

        [Test]
        public async Task ProjectService_CreateProject_Invalid_OrganizationNotActive()
        {
            // arrange
            var request = GetProjectCreateRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneAdminUserOne();
            MockOrganizationRepository.Setup_Any_Returns_True();

            // act
            var result = await SystemUnderTest.CreateProject(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Invalid, OrganizationNotActive);
            AssertReturnType<ProjectCreateResponse>(result);
            MockUserRepository.Verify_SelectById();
            MockOrganizationRepository.Verify_Any();
        }

        [Test]
        public async Task ProjectService_CreateProject_Failed()
        {
            // arrange
            var request = GetProjectCreateRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneAdminUserOne();
            MockOrganizationRepository.Setup_Any_Returns_False();
            MockProjectRepository.Setup_Any_Returns_False();
            MockLanguageRepository.Setup_Select_Returns_Language();
            MockProjectUnitOfWork.Setup_DoCreateWork_Returns_False();

            // act
            var result = await SystemUnderTest.CreateProject(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Failed);
            AssertReturnType<ProjectCreateResponse>(result);
            MockUserRepository.Verify_SelectById();
            MockOrganizationRepository.Verify_Any();
            MockProjectRepository.Verify_Any();
            MockLanguageRepository.Verify_Select();
            MockProjectUnitOfWork.Verify_DoCreateWork();
        }

        [Test]
        public async Task ProjectService_CreateProject_Success()
        {
            // arrange
            var request = GetProjectCreateRequest();
            MockOrganizationRepository.Setup_Any_Returns_False();
            MockProjectRepository.Setup_Any_Returns_False();
            MockLanguageRepository.Setup_Select_Returns_Language();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneAdminUserOne();
            MockProjectUnitOfWork.Setup_DoCreateWork_Returns_True();

            // act
            var result = await SystemUnderTest.CreateProject(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Success);
            AssertReturnType<ProjectCreateResponse>(result);
            MockOrganizationRepository.Verify_Any();
            MockProjectRepository.Verify_Any();
            MockLanguageRepository.Verify_Select();
            MockUserRepository.Verify_SelectById();
            MockProjectUnitOfWork.Verify_DoCreateWork();
        }

        [Test]
        public async Task ProjectService_CreateProjectFailed_LanguageNotFound()
        {
            // arrange
            var request = GetProjectCreateRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneAdminUserOne();
            MockOrganizationRepository.Setup_Any_Returns_False();
            MockProjectRepository.Setup_Any_Returns_False();
            MockLanguageRepository.Setup_Select_Returns_LanguageNotExist();

            // act
            var result = await SystemUnderTest.CreateProject(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Failed, LanguageNotFound);
            MockUserRepository.Verify_SelectById();
            MockOrganizationRepository.Verify_Any();
            MockProjectRepository.Verify_Any();
            MockLanguageRepository.Verify_Select();
        }

        [Test]
        public async Task ProjectService_CreateProject_Invalid_NotAdmin()
        {
            // arrange
            var request = GetProjectCreateRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneUserOne();

            // act
            var result = await SystemUnderTest.CreateProject(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Invalid);
            AssertReturnType<ProjectCreateResponse>(result);
            MockUserRepository.Verify_SelectById();

        }

        [Test]
        public async Task ProjectService_EditProject_Failed_ProjectNotFound()
        {
            // arrange
            var request = GetProjectEditRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneAdminUserOne();
            MockProjectRepository.Setup_Select_Returns_InvalidProject();
            MockOrganizationRepository.Setup_Any_Returns_False();

            // act
            var result = await SystemUnderTest.EditProject(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Failed, ProjectNotFound);
            AssertReturnType<ProjectEditResponse>(result);
            MockUserRepository.Verify_SelectById();
            MockProjectRepository.Verify_Select();
            MockOrganizationRepository.Verify_Any();
        }

        [Test]
        public async Task ProjectService_EditProject_Invalid_OrganizationNotActive()
        {
            // arrange
            var request = GetProjectEditRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneAdminUserOne();
            MockOrganizationRepository.Setup_Any_Returns_True();

            // act
            var result = await SystemUnderTest.EditProject(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Invalid, OrganizationNotActive);
            AssertReturnType<ProjectEditResponse>(result);
            MockUserRepository.Verify_SelectById();
            MockOrganizationRepository.Verify_Any();
        }

        [Test]
        public async Task ProjectService_EditProject_Invalid_ProjectNameMustBeUnique()
        {
            // arrange
            var request = GetProjectEditRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneAdminUserOne();
            MockOrganizationRepository.Setup_Any_Returns_False();
            MockProjectRepository.Setup_Select_Returns_OrganizationOneProjectOne();
            MockProjectRepository.Setup_Any_Returns_True();

            // act
            var result = await SystemUnderTest.EditProject(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Invalid, ProjectNameMustBeUnique);
            AssertReturnType<ProjectEditResponse>(result);
            MockUserRepository.Verify_SelectById();
            MockOrganizationRepository.Verify_Any();
            MockProjectRepository.Verify_Select();
            MockProjectRepository.Verify_Any();
        }

        [Test]
        public async Task ProjectService_EditProject_Success()
        {
            // arrange
            var request = GetProjectEditRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneAdminUserOne();
            MockOrganizationRepository.Setup_Any_Returns_False();
            MockProjectRepository.Setup_Update_Success();
            MockProjectRepository.Setup_Any_Returns_False();
            MockLanguageRepository.Setup_Select_Returns_Language();
            MockProjectRepository.Setup_Select_Returns_OrganizationOneProjectOne();

            // act
            var result = await SystemUnderTest.EditProject(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Success);
            AssertReturnType<ProjectEditResponse>(result);
            MockUserRepository.Verify_SelectById();
            MockOrganizationRepository.Verify_Any();
            MockProjectRepository.Verify_Update();
            MockProjectRepository.Verify_Any();
            MockLanguageRepository.Verify_Select();
            MockProjectRepository.Verify_Select();
        }

        [Test]
        public async Task ProjectService_EditProject_NotDifferent_Success()
        {
            // arrange
            var request = GetNotDifferentProjectEditRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneAdminUserOne();
            MockOrganizationRepository.Setup_Any_Returns_False();
            MockProjectRepository.Setup_Select_Returns_OrganizationOneProjectOne();

            // act
            var result = await SystemUnderTest.EditProject(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Success);
            AssertReturnType<ProjectEditResponse>(result);
            MockUserRepository.Verify_SelectById();
            MockOrganizationRepository.Verify_Any();
            MockProjectRepository.Verify_Select();
        }

        [Test]
        public async Task ProjectService_EditProject_Invalid_OrganizationNotMatch()
        {
            // arrange
            var request = GetProjectEditRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneAdminUserOne();
            MockOrganizationRepository.Setup_Any_Returns_False();
            MockProjectRepository.Setup_Select_Returns_OrganizationTwoProjectOne();

            // act
            var result = await SystemUnderTest.EditProject(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Invalid);
            AssertReturnType<ProjectEditResponse>(result);
            MockUserRepository.Verify_SelectById();
            MockOrganizationRepository.Verify_Any();
            MockProjectRepository.Verify_Select();
        }

        [Test]
        public async Task ProjectService_EditProjectFailed_LanguageNotFound()
        {
            // arrange
            var request = GetProjectEditRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneAdminUserOne();
            MockOrganizationRepository.Setup_Any_Returns_False();
            MockProjectRepository.Setup_Select_Returns_OrganizationOneProjectOne();
            MockProjectRepository.Setup_Any_Returns_False();
            MockLanguageRepository.Setup_Select_Returns_LanguageNotExist();

            // act
            var result = await SystemUnderTest.EditProject(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Failed, LanguageNotFound);
            AssertReturnType<ProjectEditResponse>(result);
            MockUserRepository.Verify_SelectById();
            MockOrganizationRepository.Verify_Any();
            MockProjectRepository.Verify_Select();
            MockProjectRepository.Verify_Any();
            MockLanguageRepository.Verify_Select();
        }

        [Test]
        public async Task ProjectService_EditProject_CurrentUserNotAdmin()
        {
            // arrange
            var request = GetProjectEditRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneUserOne();

            // act
            var result = await SystemUnderTest.EditProject(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Invalid);
            AssertReturnType<ProjectEditResponse>(result);
            MockUserRepository.Verify_SelectById();
        }

        [Test]
        public async Task ProjectService_EditProject_Failed()
        {
            // arrange
            var request = GetProjectEditRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneAdminUserOne();
            MockOrganizationRepository.Setup_Any_Returns_False();
            MockProjectRepository.Setup_Select_Returns_OrganizationOneProjectOne();
            MockProjectRepository.Setup_Any_Returns_False();
            MockLanguageRepository.Setup_Select_Returns_Language();
            MockProjectRepository.Setup_Update_Failed();


            // act
            var result = await SystemUnderTest.EditProject(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Failed);
            AssertReturnType<ProjectEditResponse>(result);
            MockUserRepository.Verify_SelectById();
            MockOrganizationRepository.Verify_Any();
            MockProjectRepository.Verify_Select();
            MockProjectRepository.Verify_Any();
            MockLanguageRepository.Verify_Select();
            MockProjectRepository.Verify_Update();
        }

        [Test]
        public async Task ProjectService_CloneProject_Success()
        {
            // arrange
            var request = GetProjectCloneRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneAdminUserOne();
            MockProjectRepository.Setup_Select_Returns_OrganizationOneProjectOne();
            MockOrganizationRepository.Setup_Any_Returns_False();
            MockProjectRepository.Setup_Any_Returns_False();
            MockProjectRepository.Setup_IsProjectNameMustBeUnique_Returns_False();
            MockLanguageRepository.Setup_Select_Returns_Language();
            MockProjectUnitOfWork.Setup_DoCloneWork_Returns_True();

            // act
            var result = await SystemUnderTest.CloneProject(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Success);
            AssertReturnType<ProjectCloneResponse>(result);
            MockUserRepository.Verify_SelectById();
            MockProjectRepository.Verify_Select();
            MockOrganizationRepository.Verify_Any();
            MockProjectRepository.Verify_IsProjectNameMustBeUnique();
            MockProjectRepository.Verify_Any();
            MockLanguageRepository.Verify_Select();
            MockProjectUnitOfWork.Verify_DoCloneWork();
        }

        [Test]
        public async Task ProjectService_CloneProject_Failed()
        {
            // arrange
            var request = GetProjectCloneRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneAdminUserOne();
            MockProjectRepository.Setup_Select_Returns_OrganizationOneProjectOne();
            MockOrganizationRepository.Setup_Any_Returns_False();
            MockProjectRepository.Setup_Any_Returns_False();
            MockProjectRepository.Setup_IsProjectNameMustBeUnique_Returns_False();
            MockLanguageRepository.Setup_Select_Returns_Language();
            MockProjectUnitOfWork.Setup_DoCloneWork_Returns_False();

            // act
            var result = await SystemUnderTest.CloneProject(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Failed);
            AssertReturnType<ProjectCloneResponse>(result);
            MockUserRepository.Verify_SelectById();
            MockProjectRepository.Verify_Select();
            MockOrganizationRepository.Verify_Any();
            MockProjectRepository.Verify_IsProjectNameMustBeUnique();
            MockProjectRepository.Verify_Any();
            MockLanguageRepository.Verify_Select();
            MockProjectUnitOfWork.Verify_DoCloneWork();
        }

        [Test]
        public async Task ProjectService_CloneProject_Invalid_NotAdmin()
        {
            // arrange
            var request = GetProjectCloneRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneUserOne();

            // act
            var result = await SystemUnderTest.CloneProject(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Invalid);
            AssertReturnType<ProjectCloneResponse>(result);
            MockUserRepository.Verify_SelectById();
        }

        [Test]
        public async Task ProjectService_CloneProject_Failed_ProjectNotFound()
        {
            // arrange
            var request = GetProjectCloneRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneAdminUserOne();
            MockProjectRepository.Setup_Select_Returns_OrganizationOneProjectOneNotExist();

            // act
            var result = await SystemUnderTest.CloneProject(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Failed, ProjectNotFound);
            AssertReturnType<ProjectCloneResponse>(result);
            MockUserRepository.Verify_SelectById();
            MockProjectRepository.Verify_Select();
        }

        [Test]
        public async Task ProjectService_CloneProject_Invalid_OrganizationNotMatch()
        {
            // arrange
            var request = GetProjectCloneRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneAdminUserOne();
            MockProjectRepository.Setup_Select_Returns_OrganizationTwoProjectOne();

            // act
            var result = await SystemUnderTest.CloneProject(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Invalid);
            AssertReturnType<ProjectCloneResponse>(result);
            MockUserRepository.Verify_SelectById();
            MockProjectRepository.Verify_Select();
        }

        [Test]
        public async Task ProjectService_CloneProject_Invalid_OrganizationNotActive()
        {
            // arrange
            var request = GetProjectCloneRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneAdminUserOne();
            MockProjectRepository.Setup_Select_Returns_OrganizationOneProjectOne();
            MockOrganizationRepository.Setup_Any_Returns_True();

            // act
            var result = await SystemUnderTest.CloneProject(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Invalid, OrganizationNotActive);
            AssertReturnType<ProjectCloneResponse>(result);
            MockUserRepository.Verify_SelectById();
            MockProjectRepository.Verify_Select();
            MockOrganizationRepository.Verify_Any();
        }

        [Test]
        public async Task ProjectService_CloneProject_Invalid_ProjectNameMustBeUnique()
        {
            // arrange
            var request = GetProjectCloneRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneAdminUserOne();
            MockProjectRepository.Setup_Select_Returns_OrganizationOneProjectOne();
            MockOrganizationRepository.Setup_Any_Returns_False();
            MockProjectRepository.Setup_IsProjectNameMustBeUnique_Returns_True();

            // act
            var result = await SystemUnderTest.CloneProject(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Invalid, ProjectNameMustBeUnique);
            AssertReturnType<ProjectCloneResponse>(result);
            MockUserRepository.Verify_SelectById();
            MockProjectRepository.Verify_Select();
            MockOrganizationRepository.Verify_Any();
            MockProjectRepository.Verify_IsProjectNameMustBeUnique();
        }

        [Test]
        public async Task ProjectService_CloneProject_Invalid_ProjectSlugMustBeUnique()
        {
            // arrange
            var request = GetProjectCloneRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneAdminUserOne();
            MockProjectRepository.Setup_Select_Returns_OrganizationOneProjectOne();
            MockOrganizationRepository.Setup_Any_Returns_False();
            MockProjectRepository.Setup_IsProjectNameMustBeUnique_Returns_False();
            MockProjectRepository.Setup_Any_Returns_True();

            // act
            var result = await SystemUnderTest.CloneProject(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Invalid, ProjectSlugMustBeUnique);
            AssertReturnType<ProjectCloneResponse>(result);
            MockUserRepository.Verify_SelectById();
            MockProjectRepository.Verify_Select();
            MockOrganizationRepository.Verify_Any();
            MockProjectRepository.Verify_IsProjectNameMustBeUnique();
            MockProjectRepository.Verify_Any();
        }

        [Test]
        public async Task ProjectService_CloneProjectFailed_LanguageNotFound()
        {
            // arrange
            var request = GetProjectCloneRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneAdminUserOne();
            MockProjectRepository.Setup_Select_Returns_OrganizationOneProjectOne();
            MockOrganizationRepository.Setup_Any_Returns_False();
            MockProjectRepository.Setup_IsProjectNameMustBeUnique_Returns_False();
            MockProjectRepository.Setup_Any_Returns_False();
            MockLanguageRepository.Setup_Select_Returns_LanguageNotExist();

            // act
            var result = await SystemUnderTest.CloneProject(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Failed, LanguageNotFound);
            AssertReturnType<ProjectCloneResponse>(result);
            MockUserRepository.Verify_SelectById();
            MockOrganizationRepository.Verify_Any();
            MockProjectRepository.Verify_Select();
            MockProjectRepository.Verify_IsProjectNameMustBeUnique();
            MockProjectRepository.Verify_Any();
            MockLanguageRepository.Verify_Select();
        }

        [Test]
        public async Task ProjectService_DeleteProject_Success()
        {
            // arrange
            var request = GetProjectDeleteRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneAdminUserOne();
            MockProjectRepository.Setup_Select_Returns_OrganizationOneProjectOne();
            MockOrganizationRepository.Setup_Any_Returns_False();
            MockProjectUnitOfWork.Setup_DoDeleteWork_Returns_True();

            // act
            var result = await SystemUnderTest.DeleteProject(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Success);
            AssertReturnType<ProjectDeleteResponse>(result);
            MockUserRepository.Verify_SelectById();
            MockProjectRepository.Verify_Select();
            MockOrganizationRepository.Verify_Any();
            MockProjectUnitOfWork.Verify_DoDeleteWork();
        }

        [Test]
        public async Task ProjectService_DeleteProject_Invalid_NotAdmin()
        {
            // arrange
            var request = GetProjectDeleteRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneUserOne();

            // act
            var result = await SystemUnderTest.DeleteProject(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Invalid);
            AssertReturnType<ProjectDeleteResponse>(result);
            MockUserRepository.Verify_SelectById();
        }

        [Test]
        public async Task ProjectService_DeleteProject__Failed_ProjectNotFound()
        {
            // arrange
            var request = GetProjectDeleteRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneAdminUserOne();
            MockProjectRepository.Setup_Select_Returns_OrganizationOneProjectOneNotExist();

            // act
            var result = await SystemUnderTest.DeleteProject(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Failed, ProjectNotFound);
            AssertReturnType<ProjectDeleteResponse>(result);
            MockUserRepository.Verify_SelectById();
            MockProjectRepository.Verify_Select();
        }

        [Test]
        public async Task ProjectService_DeleteProject_Invalid_OrganizationNotMatch()
        {
            // arrange
            var request = GetProjectDeleteRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneAdminUserOne();
            MockProjectRepository.Setup_Select_Returns_OrganizationTwoProjectOne();

            // act
            var result = await SystemUnderTest.DeleteProject(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Invalid);
            AssertReturnType<ProjectDeleteResponse>(result);
            MockUserRepository.Verify_SelectById();
            MockProjectRepository.Verify_Select();
        }

        [Test]
        public async Task ProjectService_DeleteProject_Invalid_OrganizationNotActive()
        {
            // arrange
            var request = GetProjectDeleteRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneAdminUserOne();
            MockProjectRepository.Setup_Select_Returns_OrganizationOneProjectOne();
            MockOrganizationRepository.Setup_Any_Returns_True();

            // act
            var result = await SystemUnderTest.DeleteProject(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Invalid, OrganizationNotActive);
            AssertReturnType<ProjectDeleteResponse>(result);
            MockUserRepository.Verify_SelectById();
            MockProjectRepository.Verify_Select();
            MockOrganizationRepository.Verify_Any();
        }

        [Test]
        public async Task ProjectService_DeleteProject_Invalid_ProjectHasChildren()
        {
            // arrange
            var request = GetProjectDeleteRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneAdminUserOne();
            MockProjectRepository.Setup_Select_Returns_OrganizationOneProjectOne();
            MockOrganizationRepository.Setup_Any_Returns_False();
            MockLabelRepository.Setup_Any_Returns_True();

            // act
            var result = await SystemUnderTest.DeleteProject(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Invalid, ProjectHasChildren);
            AssertReturnType<ProjectDeleteResponse>(result);
            MockUserRepository.Verify_SelectById();
            MockProjectRepository.Verify_Select();
            MockOrganizationRepository.Verify_Any();
            MockLabelRepository.Verify_Any();
        }

        [Test]
        public async Task ProjectService_DeleteProject_Failed()
        {
            // arrange
            var request = GetProjectDeleteRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneAdminUserOne();
            MockProjectRepository.Setup_Select_Returns_OrganizationOneProjectOne();
            MockOrganizationRepository.Setup_Any_Returns_False();
            MockProjectUnitOfWork.Setup_DoDeleteWork_Returns_False();

            // act
            var result = await SystemUnderTest.DeleteProject(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Failed);
            AssertReturnType<ProjectDeleteResponse>(result);
            MockUserRepository.Verify_SelectById();
            MockProjectRepository.Verify_Select();
            MockOrganizationRepository.Verify_Any();
            MockProjectUnitOfWork.Verify_DoDeleteWork();
        }

        [Test]
        public async Task ProjectService_ChangeActivationForProject_Failed_ProjectNotFound()
        {
            // arrange
            var request = GetProjectChangeActivationRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneAdminUserOne();
            MockProjectRepository.Setup_Select_Returns_InvalidProject();

            // act
            var result = await SystemUnderTest.ChangeActivationForProject(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Failed, ProjectNotFound);
            AssertReturnType<ProjectChangeActivationResponse>(result);
            MockUserRepository.Verify_SelectById();
            MockProjectRepository.Verify_Select();
        }

        [Test]
        public async Task ProjectService_ChangeActivationForProject_OrganizationAlreadyExist()
        {
            // arrange
            var request = GetProjectChangeActivationRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneAdminUserOne();
            MockProjectRepository.Setup_Select_Returns_OrganizationOneProjectOne();
            MockOrganizationRepository.Setup_Any_Returns_True();

            // act
            var result = await SystemUnderTest.ChangeActivationForProject(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Invalid);
            AssertReturnType<ProjectChangeActivationResponse>(result);
            MockUserRepository.Verify_SelectById();
            MockProjectRepository.Verify_Select();
            MockOrganizationRepository.Verify_Any();
        }

        [Test]
        public async Task ProjectService_ChangeActivationForProject_Success()
        {
            // arrange
            var request = GetProjectChangeActivationRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneAdminUserOne();
            MockProjectRepository.Setup_Select_Returns_OrganizationOneProjectOne();
            MockProjectRepository.Setup_Update_Success();
            MockOrganizationRepository.Setup_Any_Returns_False();

            // act
            var result = await SystemUnderTest.ChangeActivationForProject(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Success);
            AssertReturnType<ProjectChangeActivationResponse>(result);
            MockUserRepository.Verify_SelectById();
            MockProjectRepository.Verify_Select();
            MockProjectRepository.Verify_Update();
            MockOrganizationRepository.Verify_Any();
        }

        [Test]
        public async Task ProjectService_ChangeActivationForProject_Failed()
        {
            // arrange
            var request = GetProjectChangeActivationRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneAdminUserOne();
            MockProjectRepository.Setup_Select_Returns_OrganizationOneProjectOne();
            MockProjectRepository.Setup_Update_Failed();
            MockOrganizationRepository.Setup_Any_Returns_False();

            // act
            var result = await SystemUnderTest.ChangeActivationForProject(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Failed);
            AssertReturnType<ProjectChangeActivationResponse>(result);
            MockUserRepository.Verify_SelectById();
            MockProjectRepository.Verify_Select();
            MockProjectRepository.Verify_Update();
            MockOrganizationRepository.Verify_Any();
        }

        [Test]
        public async Task ProjectService_ChangeActivationForProject_Failed_OrganizationNotMatch()
        {
            // arrange
            var request = GetProjectChangeActivationRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneAdminUserOne();
            MockProjectRepository.Setup_Select_Returns_OrganizationTwoProjectOne();


            // act
            var result = await SystemUnderTest.ChangeActivationForProject(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Invalid);
            AssertReturnType<ProjectChangeActivationResponse>(result);
            MockUserRepository.Verify_SelectById();
            MockProjectRepository.Verify_Select();
        }

        [Test]
        public async Task ProjectService_ChangeActivationForProject_Invalid_NotAdmin()
        {
            // arrange
            var request = GetProjectChangeActivationRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneUserOne();

            // act
            var result = await SystemUnderTest.ChangeActivationForProject(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Invalid);
            AssertReturnType<ProjectChangeActivationResponse>(result);
            MockUserRepository.Verify_SelectById();
        }

        [Test]
        public async Task ProjectService_RestoreProject_Success()
        {
            // arrange
            var request = GetProjectRestoreRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneUserOne();
            MockProjectRepository.Setup_Select_Returns_OrganizationOneProjectOne();
            MockProjectRepository.Setup_SelectRevisions_Returns_OrganizationOneProjectOneRevisions();
            MockProjectRepository.Setup_RestoreRevision_Returns_True();

            // act
            var result = await SystemUnderTest.RestoreProject(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Success);
            AssertReturnType<ProjectRestoreResponse>(result);
            MockUserRepository.Verify_SelectById();
            MockProjectRepository.Verify_Select();
            MockProjectRepository.Verify_SelectRevisions();
            MockProjectRepository.Verify_RestoreRevision();
        }

        [Test]
        public async Task ProjectService_RestoreProject_Invalid_OrganizationNotActive()
        {
            // arrange
            var request = GetProjectRestoreRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneUserOne();
            MockOrganizationRepository.Setup_Any_Returns_True();

            // act
            var result = await SystemUnderTest.RestoreProject(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Invalid);
            AssertReturnType<ProjectRestoreResponse>(result);
            MockUserRepository.Verify_SelectById();
            MockOrganizationRepository.Verify_Any();

        }

        [Test]
        public async Task ProjectService_RestoreProject_Failed_ProjectNotFound()
        {
            // arrange
            var request = GetProjectRestoreRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneUserOne();
            MockProjectRepository.Setup_Select_Returns_InvalidProject();

            // act
            var result = await SystemUnderTest.RestoreProject(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Failed, ProjectNotFound);
            AssertReturnType<ProjectRestoreResponse>(result);
            MockUserRepository.Verify_SelectById();
            MockProjectRepository.Verify_Select();
        }

        [Test]
        public async Task ProjectService_RestoreProject_Failed_RevisionNotFound()
        {
            // arrange
            var request = GetProjectRestoreRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneUserOne();
            MockProjectRepository.Setup_Select_Returns_OrganizationOneProjectOne();
            MockProjectRepository.Setup_SelectRevisions_Returns_InvalidRevision();

            // act
            var result = await SystemUnderTest.RestoreProject(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Failed, ProjectRevisionNotFound);
            AssertReturnType<ProjectRestoreResponse>(result);
            MockUserRepository.Verify_SelectById();
            MockProjectRepository.Verify_Select();
            MockProjectRepository.Verify_SelectRevisions();
        }

        [Test]
        public async Task ProjectService_RestoreProject_Failed()
        {
            // arrange
            var request = GetProjectRestoreRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneUserOne();
            MockProjectRepository.Setup_Select_Returns_OrganizationOneProjectOne();
            MockProjectRepository.Setup_SelectRevisions_Returns_OrganizationOneProjectOneRevisions();
            MockProjectRepository.Setup_RestoreRevision_Returns_False();

            // act
            var result = await SystemUnderTest.RestoreProject(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Failed);
            AssertReturnType<ProjectRestoreResponse>(result);
            MockUserRepository.Verify_SelectById();
            MockProjectRepository.Verify_Select();
            MockProjectRepository.Verify_SelectRevisions();
            MockProjectRepository.Verify_RestoreRevision();
        }

        [Test]
        public async Task ProjectService_GetPendingTranslations_SelectAfter_Success()
        {
            // arrange
            var request = GetProjectPendingTranslationReadListRequestForSelectAfter();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneSuperAdminUserOne();
            MockProjectRepository.Setup_Select_Returns_OrganizationOneProjectOne();
            MockLabelRepository.Setup_SelectAfter_Returns_Labels();
            MockLabelRepository.Setup_Count_Returns_Ten();

            // act
            var result = await SystemUnderTest.GetPendingTranslations(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Success);
            AssertReturnType<ProjectPendingTranslationReadListResponse>(result);
            AssertPagingInfoForSelectAfter(request.PagingInfo, Ten);
            MockProjectRepository.Verify_Select();
            MockUserRepository.Verify_SelectById();
            MockLabelRepository.Verify_SelectAfter();
            MockLabelRepository.Verify_Count();
        }

        [Test]
        public async Task ProjectService_GetPendingTranslations_SelectMany_Success()
        {
            // arrange
            var request = GetProjectPendingTranslationReadListRequestForSelectMany();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneSuperAdminUserOne();
            MockProjectRepository.Setup_Select_Returns_OrganizationOneProjectOne();
            MockLabelRepository.Setup_SelectMany_Returns_Labels();
            MockLabelRepository.Setup_Count_Returns_Ten();

            // act
            var result = await SystemUnderTest.GetPendingTranslations(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Success);
            AssertReturnType<ProjectPendingTranslationReadListResponse>(result);
            AssertPagingInfoForSelectMany(request.PagingInfo, Ten);
            MockProjectRepository.Verify_Select();
            MockUserRepository.Verify_SelectById();
            MockLabelRepository.Verify_SelectMany();
            MockLabelRepository.Verify_Count();
        }

        [Test]
        public async Task ProjectService_GetPendingTranslations_Failed_ProjectNotFound()
        {
            // arrange
            var request = GetProjectPendingTranslationReadListRequest();
            MockProjectRepository.Setup_Select_Returns_InvalidProject();

            // act
            var result = await SystemUnderTest.GetPendingTranslations(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Failed, ProjectNotFound);
            AssertReturnType<ProjectPendingTranslationReadListResponse>(result);
            MockProjectRepository.Verify_Select();
        }

        [Test]
        public async Task ProjectService_GetPendingTranslations_Invalid_OrganizationNotMatch()
        {
            // arrange
            var request = GetProjectPendingTranslationReadListRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneUserOne();
            MockProjectRepository.Setup_Select_Returns_OrganizationTwoProjectOne();

            // act
            var result = await SystemUnderTest.GetPendingTranslations(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Invalid);
            AssertReturnType<ProjectPendingTranslationReadListResponse>(result);
            MockUserRepository.Verify_SelectById();
            MockProjectRepository.Verify_Select();
        }

    }
}