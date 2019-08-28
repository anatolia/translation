using NUnit.Framework;
using Shouldly;
using StandardRepository.Models.Entities;
using StandardRepository.Models.Entities.Schemas;

using Translation.Data.Entities.Main;
using static Translation.Common.Tests.TestHelpers.AssertPropertyTestHelper;


namespace Translation.Server.Unit.Tests.Data.Entities
{
    [TestFixture]
    public class SendEmailLogEntityTests
    {
        [Test]
        public void SendEmailLog()
        {
            var entity = new SendEmailLog();

            var entityType = entity.GetType();
            var properties = entityType.GetProperties();

            entityType.BaseType.Name.ShouldBe(nameof(BaseEntity));
            entityType.GetInterface(nameof(ISchemaMain)).ShouldNotBeNull();

            AssertLongProperty(properties, "OrganizationId", entity.OrganizationId);
            AssertGuidProperty(properties, "OrganizationUid", entity.OrganizationUid);
            AssertStringProperty(properties, "OrganizationName", entity.OrganizationName);

            AssertGuidProperty(properties, "MailUid", entity.MailUid);
 
            AssertStringProperty(properties, "Subject", entity.Subject);
            AssertStringProperty(properties, "EmailFrom", entity.EmailFrom);
            AssertStringProperty(properties, "EmailTo", entity.EmailTo);

            AssertBooleanProperty(properties, "IsOpened", entity.IsOpened);
        }

    }
}