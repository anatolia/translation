using NUnit.Framework;
using Shouldly;

using Translation.Client.Web.Helpers.Mappers;
using Translation.Client.Web.Models.Language;
using static Translation.Common.Tests.TestHelpers.FakeDtoTestHelper;

namespace Translation.Client.Web.Unit.Tests.Helpers.Mappers
{
    [TestFixture]
    public class LanguageMapperTests
    {
        [Test]
        public void LanguageMapper_MapLanguageEditModel()
        {
            // arrange
            var dto = GetLanguageDtoOne();

            // act
            var result = LanguageMapper.MapLanguageEditModel(dto);

            // assert
            result.ShouldBeAssignableTo<LanguageEditModel>();
            result.LanguageUid.ShouldBe(dto.Uid);
            result.Name.ShouldBe(dto.Name);
            result.OriginalName.ShouldBe(dto.OriginalName);
            result.IsoCode2.ShouldBe(dto.IsoCode2);
            result.IsoCode3.ShouldBe(dto.IsoCode3);
            result.Description.ShouldBe(dto.Description);
        }

        [Test]
        public void LanguageMapper_MapLanguageDetailModel()
        {
            // arrange
            var dto = GetLanguageDtoOne();

            // act
            var result = LanguageMapper.MapLanguageDetailModel(dto);

            // assert
            result.ShouldBeAssignableTo<LanguageDetailModel>();
            result.LanguageUid.ShouldBe(dto.Uid);
            result.Name.ShouldBe(dto.Name);
            result.OriginalName.ShouldBe(dto.OriginalName);
            result.IsoCode2.ShouldBe(dto.IsoCode2);
            result.IsoCode3.ShouldBe(dto.IsoCode3);
            result.Description.ShouldBe(dto.Description);
        }
    }
}