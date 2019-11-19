using NUnit.Framework;
using Shouldly;

using Translation.Data.Factories;
using static Translation.Common.Tests.TestHelpers.FakeRequestTestHelper;
using static Translation.Server.Unit.Tests.TestHelpers.FakeEntityTestHelper;

namespace Translation.Server.Unit.Tests.Data.Factories
{
    [TestFixture]
    public class LabelTranslationFactoryTests
    {
        public LabelTranslationFactory LabelTranslationFactory { get; set; }

        [SetUp]
        public void run_before_every_test()
        {
            LabelTranslationFactory = new LabelTranslationFactory();
        }

        [Test]
        public void LabelTranslationFactory_CreateEntity_Project()
        {
            // arrange
            var label = GetLabel();
            var language = GetLanguageOne();
            var translation = "example_string";

            // act
            var result = LabelTranslationFactory.CreateEntity(translation, label, language);

            result.TranslationText.ShouldBe(translation);
            result.IsActive.ShouldBeTrue();

            result.OrganizationId.ShouldBe(label.OrganizationId);
            result.OrganizationUid.ShouldBe(label.OrganizationUid);
            result.OrganizationName.ShouldBe(label.OrganizationName);

            result.ProjectUid.ShouldBe(label.Uid);
            result.ProjectId.ShouldBe(label.Id);
            result.ProjectName.ShouldBe(label.Name);

            result.LabelId.ShouldBe(label.Id);
            result.LabelUid.ShouldBe(label.Uid);
            result.LabelName.ShouldBe(label.Name);

            result.LanguageId.ShouldBe(language.Id);
            result.LanguageUid.ShouldBe(language.Uid);
            result.LanguageName.ShouldBe(language.Name);
        }

        [Test]
        public void LabelTranslationFactory_CreateEntityFromRequest_LabelTranslationEditRequest_LabelTranslation()
        {
            // arrange
            var labelTranslation = GetLabelTranslation();
            var request = GetLabelTranslationEditRequest(labelTranslation);

            // act
            var result = LabelTranslationFactory.CreateEntityFromRequest(request, labelTranslation);

            //assert
            result.TranslationText.ShouldBe(labelTranslation.TranslationText);
        }

        [Test]
        public void LabelTranslationFactory_CreateDtoFromEntity_Language()
        {
            // arrange
            var language = GetLanguageOne();
            var labelTranslation = GetLabelTranslation();

            // act
            var result = LabelTranslationFactory.CreateDtoFromEntity(labelTranslation, language);

            // assert
            result.LanguageIconUrl.ShouldBe(language.IconUrl);
            result.LanguageIsoCode2.ShouldBe(language.IsoCode2Char);
        }

        [Test]
        public void LabelTranslationFactory_CreateDtoFromEntity_LabelTranslation()
        {
            // arrange
            var labelTranslation = GetLabelTranslation();

            // act
            var result = LabelTranslationFactory.CreateDtoFromEntity(labelTranslation);

            // assert
            result.Uid.ShouldBe(labelTranslation.Uid);
            result.Translation.ShouldBe(labelTranslation.TranslationText);
            result.CreatedAt.ShouldBe(labelTranslation.CreatedAt);
            result.UpdatedAt.ShouldBe(labelTranslation.UpdatedAt);
            labelTranslation.IsActive.ShouldBe(labelTranslation.IsActive);

            result.OrganizationUid.ShouldBe(labelTranslation.OrganizationUid);
            result.OrganizationName.ShouldBe(labelTranslation.OrganizationName);

            result.ProjectUid.ShouldBe(labelTranslation.ProjectUid);
            result.ProjectName.ShouldBe(labelTranslation.ProjectName);

            result.LabelUid.ShouldBe(labelTranslation.LabelUid);
            result.LabelKey.ShouldBe(labelTranslation.LabelName);

            result.LanguageUid.ShouldBe(labelTranslation.LanguageUid);
            result.LanguageName.ShouldBe(labelTranslation.LanguageName);
        }

    }
}