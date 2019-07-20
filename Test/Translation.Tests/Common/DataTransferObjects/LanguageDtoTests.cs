using NUnit.Framework;

using Shouldly;

using StandardRepository.Models.Entities;
using StandardRepository.Models.Entities.Schemas;
using Translation.Common.Models.Base;
using Translation.Common.Models.DataTransferObjects;
using static Translation.Tests.TestHelpers.AssertPropertyTestHelper;

namespace Translation.Tests.Common.DataTransferObjects
{
    [TestFixture]
    public class LanguageDtoTests
    {
        [Test]
        public void LanguageDto()
        {
            var dto = new LanguageDto();

            var dtoType = dto.GetType();
            var properties = dtoType.GetProperties();

            dtoType.BaseType.Name.ShouldBe(nameof(BaseDto));
        

            AssertStringProperty(properties, "IsoCode2", dto.IsoCode2);
            AssertStringProperty(properties, "IsoCode3", dto.IsoCode3);
            AssertStringProperty(properties, "IconPath", dto.IconPath);
            AssertStringProperty(properties, "OriginalName", dto.OriginalName);
            AssertStringProperty(properties, "Description", dto.Description);
            AssertStringProperty(properties, "IconUrl", dto.IconUrl);
        }
    }
}