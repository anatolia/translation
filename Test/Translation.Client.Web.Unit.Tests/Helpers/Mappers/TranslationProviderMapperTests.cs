using NUnit.Framework;
using Shouldly;

using Translation.Client.Web.Helpers.Mappers;
using Translation.Client.Web.Models.TranslationProvider;
using static Translation.Common.Tests.TestHelpers.FakeDtoTestHelper;

namespace Translation.Client.Web.Unit.Tests.Helpers.Mappers
{
    [TestFixture]
    public class TranslationProviderMapperTests
    {
        [Test]
        public void TranslationProviderMapper_MapTranslationProviderEditModel()
        {
            // arrange
            var dto = GetTranslationProviderDto();

            // act
            var result = TranslationProviderMapper.MapTranslationProviderEditModel(dto);

            // assert
            result.ShouldBeAssignableTo<TranslationProviderEditModel>();

            result.TranslationProviderUid.ShouldBe(dto.Uid);
            result.Value.ShouldBe(dto.CredentialValue);
            result.Name.ShouldBe(dto.Name);
            result.Description.ShouldBe(dto.Description);
        }

        [Test]
        public void TranslationProviderMapper_MapTranslationProviderDetailModel()
        {
            // arrange
            var dto = GetTranslationProviderDto();

            // act
            var result = TranslationProviderMapper.MapTranslationProviderDetailModel(dto);

            // assert
            result.ShouldBeAssignableTo<TranslationProviderDetailModel>();

            result.TranslationProviderUid.ShouldBe(dto.Uid);
            result.Value.ShouldBe(dto.CredentialValue);
            result.Name.ShouldBe(dto.Name);
            result.Description.ShouldBe(dto.Description);
        }
    }
}