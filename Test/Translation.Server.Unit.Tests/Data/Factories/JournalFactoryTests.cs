using NUnit.Framework;
using Shouldly;

using Translation.Data.Factories;
using static Translation.Common.Tests.TestHelpers.FakeRequestTestHelper;
using static Translation.Server.Unit.Tests.TestHelpers.FakeEntityTestHelper;

namespace Translation.Server.Unit.Tests.Data.Factories
{
    [TestFixture]
    public class JournalFactoryTests
    {
        public JournalFactory JournalFactory { get; set; }

        [SetUp]
        public void run_before_every_test()
        {
            JournalFactory = new JournalFactory();
        }

        [Test]
        public void JournalFactory_CreateDtoFromEntity_Journal()
        {
            // arrange
            var journal = GetJournal();

            // act
            var result = JournalFactory.CreateDtoFromEntity(journal);

            // assert
            result.OrganizationUid.ShouldBe(journal.OrganizationUid);
            result.OrganizationName.ShouldBe(journal.OrganizationName);

            result.IntegrationUid.ShouldBe(journal.IntegrationUid.Value);
            result.IntegrationName.ShouldBe(journal.IntegrationName);

            result.UserUid.ShouldBe(journal.UserUid.Value);
            result.UserName.ShouldBe(journal.UserName);

            result.Message.ShouldBe(journal.Message);
            result.CreatedAt.ShouldBe(journal.CreatedAt);
        }

        [Test]
        public void JournalFactory_CreateEntityFromRequest_JournalCreateRequest_CurrentUser()
        {
            // arrange
            var currentUser = GetOrganizationOneCurrentUserOne();
            var journal = GetJournal();
            var request = GetJournalCreateRequest(journal, currentUser);

            // act
            var result = JournalFactory.CreateEntityFromRequest(request, currentUser);

            // assert
            result.OrganizationId.ShouldBe(currentUser.OrganizationId);
            result.OrganizationUid.ShouldBe(currentUser.OrganizationUid);
            result.OrganizationName.ShouldBe(currentUser.Organization.Name);

            result.UserId.ShouldBe(currentUser.Id);
            result.UserUid.ShouldBe(currentUser.Uid);
            result.UserName.ShouldBe(currentUser.Name);

        }
    }
}