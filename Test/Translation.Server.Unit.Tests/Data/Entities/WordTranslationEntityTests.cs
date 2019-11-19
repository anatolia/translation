using NUnit.Framework;
using Shouldly;
using StandardRepository.Models.Entities;
using StandardRepository.Models.Entities.Schemas;

using Translation.Data.Entities.Parameter;
using static Translation.Common.Tests.TestHelpers.AssertPropertyTestHelper;

namespace Translation.Server.Unit.Tests.Data.Entities
{
    [TestFixture]
    public class WordTranslationEntityTests
    {
        [Test]
        public void Word()
        {
            var entity = new WordTranslation();

            var entityType = entity.GetType();
            var properties = entityType.GetProperties();

            entityType.BaseType.Name.ShouldBe(nameof(BaseEntity));
            entityType.GetInterface(nameof(ISchemaParameter)).ShouldNotBeNull();

            AssertLongProperty(properties, "WordId", entity.WordId);
            AssertGuidProperty(properties, "WordUid", entity.WordUid);
            AssertStringProperty(properties, "WordName", entity.WordName);

            AssertLongProperty(properties, "LanguageId", entity.LanguageId);
            AssertGuidProperty(properties, "LanguageUid", entity.LanguageUid);
            AssertStringProperty(properties, "LanguageName", entity.LanguageName);

            AssertStringProperty(properties, "TranslationText", entity.TranslationText);
            AssertStringProperty(properties, "Description", entity.Description);
        }
    }
}