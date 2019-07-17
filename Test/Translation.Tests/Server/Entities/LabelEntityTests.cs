using NUnit.Framework;
using Shouldly;
using StandardRepository.Models.Entities;
using StandardRepository.Models.Entities.Schemas;

using Translation.Data.Entities.Domain;
using static Translation.Tests.TestHelpers.AssertPropertyTestHelper;

namespace Translation.Tests.Server.Entities
{
    [TestFixture]
    public class LabelEntityTests
    {
        [Test]
        public void Label()
        {
            var entity = new Label();

            var entityType = entity.GetType();
            var properties = entityType.GetProperties();

            entityType.BaseType.Name.ShouldBe(nameof(BaseEntity));
            entityType.GetInterface(nameof(ISchemaDomain)).ShouldNotBeNull();

            AssertLongProperty(properties, "OrganizationId", entity.OrganizationId);
            AssertGuidProperty(properties, "OrganizationUid", entity.OrganizationUid);
            AssertStringProperty(properties, "OrganizationName", entity.OrganizationName);

            AssertLongProperty(properties, "ProjectId", entity.ProjectId);
            AssertGuidProperty(properties, "ProjectUid", entity.ProjectUid);
            AssertStringProperty(properties, "ProjectName", entity.ProjectName);

            AssertStringProperty(properties,"Key",entity.Key);
            AssertStringProperty(properties, "Description", entity.Description);
            AssertBooleanProperty(properties, "IsActive", entity.IsActive);

            AssertIntegerProperty(properties, "LabelTranslationCount",entity.LabelTranslationCount);
        }
    }
}