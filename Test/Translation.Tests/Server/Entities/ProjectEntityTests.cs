using NUnit.Framework;
using Shouldly;

using StandardRepository.Models.Entities;
using StandardRepository.Models.Entities.Schemas;
using Translation.Data.Entities.Domain;
using static Translation.Tests.TestHelpers.AssertPropertyTestHelper;

namespace Translation.Tests.Server.Entities
{
    [TestFixture]
    public class ProjectEntityTests
    {
        [Test]
        public void Project()
        {
            var entity =new Project();

            var entityType = entity.GetType();
            var properties = entityType.GetProperties();

            entityType.BaseType.Name.ShouldBe(nameof(BaseEntity));
            entityType.GetInterface(nameof(ISchemaDomain)).ShouldNotBeNull();

            AssertLongProperty(properties, "OrganizationId", entity.OrganizationId);
            AssertGuidProperty(properties, "OrganizationUid", entity.OrganizationUid);
            AssertStringProperty(properties, "OrganizationName", entity.OrganizationName);

            AssertStringProperty(properties, "Description", entity.Description);
            AssertStringProperty(properties, "Url", entity.Url);
            AssertBooleanProperty(properties, "IsActive", entity.IsActive);
            AssertIntegerProperty(properties,"LabelCount",entity.LabelCount);
            AssertIntegerProperty(properties, "LabelTranslationCount", entity.LabelTranslationCount);
            AssertBooleanProperty(properties, "IsSuperProject", entity.IsSuperProject);

        }
    }
}