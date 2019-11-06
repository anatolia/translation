using NUnit.Framework;
using Shouldly;

using Translation.Data.Factories;
using static Translation.Server.Unit.Tests.TestHelpers.FakeEntityTestHelper;

namespace Translation.Server.Unit.Tests.Data.Factories
{
    [TestFixture]
    public class SendEmailLogFactoryTests
    {
        public SendEmailLogFactory SendEmailLogFactory { get; set; }

        [SetUp]
        public void run_before_every_test()
        {
            SendEmailLogFactory = new SendEmailLogFactory();
        }

        [Test]
        public void SendEmailLogFactory_CreateDtoFromEntity_SendEmailLog()
        {
            // arrange
            var entity = GetSendEmailLog();

            // act
            var result = SendEmailLogFactory.CreateDtoFromEntity(entity);

            // assert
          result.OrganizationUid.ShouldBe(entity.OrganizationUid);
          result.OrganizationName.ShouldBe(entity.OrganizationName);

          result.MailUid.ShouldBe(entity.MailUid);

          result.Subject.ShouldBe(entity.Subject);
          result.EmailFrom.ShouldBe(entity.EmailFrom);
          result.EmailTo.ShouldBe(entity.EmailTo);

          result.IsOpened.ShouldBe(entity.IsOpened);
        }
    }
}