using System.Threading.Tasks;

using NUnit.Framework;
using Shouldly;

using Translation.Common.Contracts;
using Translation.Common.Enumerations;
using Translation.Common.Models.Responses.Journal;
using static Translation.Tests.TestHelpers.AssertViewModelTestHelper;
using static Translation.Tests.TestHelpers.FakeRequestTestHelper;

namespace Translation.Tests.Server.Services
{
    [TestFixture]
    public class JournalServiceTests : ServiceBaseTests
    {
        public IJournalService SystemUnderTest { get; set; }

        [SetUp]
        public void run_before_every_test()
        {
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
            result.Status.ShouldBe(ResponseStatus.Success);
            result.ErrorMessages.ShouldNotBeNull();
            result.ErrorMessages.Count.ShouldBe(0);
            AssertReturnType<JournalCreateResponse>(result);
        }

        [Test]
        public async Task JournalService_GetJournalsOfOrganization_Success()
        {
            // arrange
            var request = GetOrganizationJournalReadListRequest();

            // act
            var result = await SystemUnderTest.GetJournalsOfOrganization(request);

            // assert
            result.Status.ShouldBe(ResponseStatus.Success);
            result.ErrorMessages.ShouldNotBeNull();
            result.ErrorMessages.Count.ShouldBe(0);
            AssertReturnType<JournalReadListResponse>(result);
        }


        [Test]
        public async Task JournalService_GetJournalsOfUser_Success()
        {
            // arrange
            var request = GetUserJournalReadListRequest();

            // act
            var result = await SystemUnderTest.GetJournalsOfUser(request);

            // assert
            result.Status.ShouldBe(ResponseStatus.Success);
            result.ErrorMessages.ShouldNotBeNull();
            result.ErrorMessages.Count.ShouldBe(0);
            AssertReturnType<JournalReadListResponse>(result);
        }
    }
}