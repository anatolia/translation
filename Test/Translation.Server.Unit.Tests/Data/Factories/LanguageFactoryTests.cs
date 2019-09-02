using NUnit.Framework;
using Shouldly;

using Translation.Data.Factories;
using static Translation.Common.Tests.TestHelpers.FakeRequestTestHelper;
using static Translation.Common.Tests.TestHelpers.FakeEntityTestHelper;


namespace Translation.Server.Unit.Tests.Data.Factories
{
    [TestFixture]
    public class LanguageFactoryTests
    {
        public LanguageFactory LanguageFactory { get; set; }

        [SetUp]
        public void run_before_every_test()
        {
            LanguageFactory = new LanguageFactory();
        }

        [Test]
        public void LanguageFactory_CreateEntity_Project()
        {
            // arrange
            var project = GetProject();
            var isoCode2Char = "example_key";
            var isoCode3Char = "example_key";
            var name = "example_key";
            var originalName = "example_key";

            // act
            var result = LanguageFactory.CreateEntity(isoCode2Char,isoCode3Char,name,originalName);

            // assert
            result.IsoCode2Char.ShouldBe(isoCode2Char);
            result.IsoCode3Char.ShouldBe(isoCode3Char);
            result.IconUrl.ShouldBe($"/images/flags/{isoCode2Char}.png");
            result.Name.ShouldBe(name);
            result.OriginalName.ShouldBe(originalName);
        }

        [Test]
        public void LanguageFactory_CreateEntityFromRequest_LanguageEditRequest()
        {
            //arrange
            var language = GetLanguageOne();
            var request = GetLanguageEditRequest(language);

            //act
            var result = LanguageFactory.CreateEntityFromRequest(request, language);

            //assert
            result.Name.ShouldBe(request.Name);
            result.OriginalName.ShouldBe(result.OriginalName);
            result.IsoCode2Char.ShouldBe(request.IsoCode2);
            result.IsoCode3Char.ShouldBe(request.IsoCode3);
            result.IconUrl.ShouldBe(request.Icon);
            result.Description.ShouldBe(request.Description);
        }

        [Test]
        public void LanguageFactory_CreateEntityFromRequest_LanguageCreateRequest()
        {
            //arrange
            var request = GetLanguageCreateRequest();

            //act
            var result = LanguageFactory.CreateEntityFromRequest(request);

            //assert
            result.Name.ShouldBe(request.Name);
            result.IsoCode2Char.ShouldBe(request.IsoCode2);
            result.IsoCode3Char.ShouldBe(request.IsoCode3);
            result.IconUrl.ShouldBe(request.Icon);
            result.Description.ShouldBe(request.Description);
        }

        [Test]
        public void LanguageFactory_CreateDtoFromEntity_Language()
        {
            // arrange
            var entity = GetLanguageOne();
            
            // act
            var result = LanguageFactory.CreateDtoFromEntity(entity);

            // assert
            result.Uid.ShouldBe(entity.Uid);
            result.CreatedAt.ShouldBe(entity.CreatedAt);
            result.UpdatedAt.ShouldBe(entity.UpdatedAt);
            result.OriginalName.ShouldBe(entity.OriginalName);
            result.Name.ShouldBe(entity.Name);
            result.IsoCode2.ShouldBe(entity.IsoCode2Char);
            result.IsoCode3.ShouldBe(entity.IsoCode3Char);
            result.IconPath.ShouldBe(entity.IconUrl);
            result.Description.ShouldBe(entity.Description);
        }
    }
}