using System.Threading.Tasks;

using NUnit.Framework;

using Translation.Common.Contracts;
using Translation.Common.Enumerations;
using Translation.Common.Models.Responses.Journal;
using Translation.Server.Unit.Tests.RepositorySetupHelpers;
using static Translation.Common.Tests.TestHelpers.FakeRequestTestHelper;
using static Translation.Common.Tests.TestHelpers.AssertViewModelTestHelper;
using static Translation.Server.Unit.Tests.TestHelpers.AssertResponseTestHelper;
using static Translation.Common.Tests.TestHelpers.FakeConstantTestHelper;

namespace Translation.Server.Unit.Tests.Services
{
    [TestFixture]
    public class JournalServiceTests : ServiceBaseTests
    {
        public IJournalService SystemUnderTest { get; set; }

        [SetUp]
        public void run_before_every_test()
        {
            Refresh();
            SystemUnderTest = Container.Resolve<IJournalService>();
        }

        [Test]
        public void JournalService_CreateProject_Success()
        {
            // arrange
            var request = GetJournalCreateRequest();

            // act
            var result = SystemUnderTest.CreateJournal(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Success);
            AssertReturnType<JournalCreateResponse>(result);
        }

        [Test]
        public async Task JournalService_GetJournalsOfOrganization_Success_SelectMany()
        {
            // arrange
            var request = GetOrganizationJournalReadListRequestForSelectMany();
            MockOrganizationRepository.Setup_Select_Returns_OrganizationOne();
            MockJournalRepository.Setup_SelectMany_Returns_Journals();
            MockJournalRepository.Setup_Count_Returns_Ten();

            // act
            var result = await SystemUnderTest.GetJournalsOfOrganization(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Success);
            AssertReturnType<JournalReadListResponse>(result);
            AssertPagingInfoForSelectMany(request.PagingInfo, Ten);
            MockOrganizationRepository.Verify_Select();
            MockJournalRepository.Verify_SelectMany();
            MockJournalRepository.Verify_Count();
        }

        [Test]
        public async Task JournalService_GetJournalsOfOrganization_Failed_OrganizationNotFound()
        {
            // arrange
            var request = GetOrganizationJournalReadListRequest();
            MockOrganizationRepository.Setup_Select_Returns_NotExistOrganization();

            // act
            var result = await SystemUnderTest.GetJournalsOfOrganization(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Failed, OrganizationNotFound);
            AssertReturnType<JournalReadListResponse>(result);
            MockOrganizationRepository.Verify_Select();
        }

        [Test]
        public async Task JournalService_GetJournalsOfUser_Success_SelectMany()
        {
            // arrange
            var request = GetUserJournalReadListRequestForSelectMany();
            MockUserRepository.Setup_Select_Returns_OrganizationOneUserOne();
            MockJournalRepository.Setup_SelectMany_Returns_Journals();
            MockJournalRepository.Setup_Count_Returns_Ten();

            // act
            var result = await SystemUnderTest.GetJournalsOfUser(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Success);
            AssertReturnType<JournalReadListResponse>(result);
            AssertPagingInfoForSelectMany(request.PagingInfo, Ten);
            MockUserRepository.Verify_Select();
            MockJournalRepository.Verify_SelectMany();
            MockJournalRepository.Verify_Count();
        }

        [Test]
        public async Task JournalService_GetJournalsOfUser_Failed_UserNotFound()
        {
            // arrange
            var request = GetUserJournalReadListRequest();
            MockUserRepository.Setup_Select_Returns_OrganizationOneUserOneNotExist();

            // act
            var result = await SystemUnderTest.GetJournalsOfUser(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Failed, UserNotFound);
            AssertReturnType<JournalReadListResponse>(result);
            MockUserRepository.Verify_Select();
        }
    }
}