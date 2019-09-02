using NUnit.Framework;
using Shouldly;
using StandardRepository.Models.Entities;
using StandardRepository.Models.Entities.Schemas;

using Translation.Data.Entities.Main;
using static Translation.Common.Tests.TestHelpers.AssertPropertyTestHelper;

namespace Translation.Server.Unit.Tests.Data.Entities
{

    [TestFixture]
    public class JournalEntityTests
    {
        [Test]
        public void Journal()
        {
            var entity = new Journal();

            var entityType = entity.GetType();
            var properties = entityType.GetProperties();

            entityType.BaseType.Name.ShouldBe(nameof(BaseEntity));
            entityType.GetInterface(nameof(ISchemaMain)).ShouldNotBeNull();

            AssertLongProperty(properties, "OrganizationId", entity.OrganizationId);
            AssertGuidProperty(properties, "OrganizationUid", entity.OrganizationUid);
            AssertStringProperty(properties, "OrganizationName", entity.OrganizationName);

            AssertNullableLongProperty(properties, "IntegrationId", entity.IntegrationId);
            AssertNullableGuidProperty(properties, "IntegrationUid", entity.IntegrationUid);
            AssertStringProperty(properties, "IntegrationName", entity.IntegrationName);

            AssertNullableLongProperty(properties, "UserId", entity.UserId);
            AssertNullableGuidProperty(properties, "UserUid", entity.UserUid);
            AssertStringProperty(properties, "UserName", entity.UserName);

            AssertStringProperty(properties, "Message", entity.Message);
        }
    }
}