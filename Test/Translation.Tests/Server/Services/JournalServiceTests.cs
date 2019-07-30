using System.Threading.Tasks;

using NUnit.Framework;

using Translation.Common.Contracts;
using Translation.Common.Enumerations;
using Translation.Common.Models.Responses.Journal;
using Translation.Tests.SetupHelpers;
using static Translation.Tests.TestHelpers.AssertViewModelTestHelper;
using static Translation.Tests.TestHelpers.FakeRequestTestHelper;
using static Translation.Tests.TestHelpers.FakeConstantTestHelper;
using static Translation.Tests.TestHelpers.AssertResponseTestHelper;

namespace Translation.Tests.Server.Services
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
            MockJournalRepository.Setup_SelectAfter_Returns_Journals();
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
        public async Task JournalService_GetJournalsOfOrganization_Invalid_OrganizationNotFound()
        {
            // arrange
            var request = GetOrganizationJournalReadListRequest();
            MockOrganizationRepository.Setup_Select_Returns_NotExistOrganization();

            // act
            var result = await SystemUnderTest.GetJournalsOfOrganization(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Invalid, OrganizationNotFound);
            AssertReturnType<JournalReadListResponse>(result);
            MockOrganizationRepository.Verify_Select();
        }

        [Test]
        public async Task JournalService_GetJournalsOfUser_Success_SelectMany()
        {
            // arrange
            var request = GetUserJournalReadListRequestForSelectMany();
            MockOrganizationRepository.Setup_Select_Returns_OrganizationOne();
            MockJournalRepository.Setup_SelectAfter_Returns_Journals();
            MockJournalRepository.Setup_Count_Returns_Ten();

            // act
            var result = await SystemUnderTest.GetJournalsOfUser(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Success);
            AssertReturnType<JournalReadListResponse>(result);
            AssertPagingInfoForSelectMany(request.PagingInfo, Ten);
            MockOrganizationRepository.Verify_Select();
            MockJournalRepository.Verify_SelectMany();
            MockJournalRepository.Verify_Count();
        }

        [Test]
        public async Task JournalService_GetJournalsOfUser_Invalid_UserNotFound()
        {
            // arrange
            var request = GetUserJournalReadListRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneUserOne();

            // act
            var result = await SystemUnderTest.GetJournalsOfUser(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Success);
            AssertReturnType<JournalReadListResponse>(result);
            MockUserRepository.Verify_SelectById();
        }
    }
}