using NUnit.Framework;
using Shouldly;
using StandardRepository.Models.Entities;
using StandardRepository.Models.Entities.Schemas;

using Translation.Data.Entities.Domain;
using static Translation.Common.Tests.TestHelpers.AssertPropertyTestHelper;

namespace Translation.Server.Unit.Tests.Data.Entities
{
    [TestFixture]
    public class TranslationProviderEntityTests
    {
        [Test]
        public void Label()
        {
            var entity = new TranslationProvider();

            var entityType = entity.GetType();
            var properties = entityType.GetProperties();

            entityType.BaseType.Name.ShouldBe(nameof(BaseEntity));
            entityType.GetInterface(nameof(ISchemaDomain)).ShouldNotBeNull();

            AssertStringProperty(properties, "Value", entity.Value);
            AssertStringProperty(properties, "Description", entity.Description);
            AssertBooleanProperty(properties, "IsActive", entity.IsActive);

        }
    }
}