using NUnit.Framework;
using Shouldly;
using Translation.Common.Models.Base;
using Translation.Common.Models.DataTransferObjects;
using static Translation.Common.Tests.TestHelpers.AssertPropertyTestHelper;

namespace Translation.Common.Tests.Models.DataTransferObjects
{
    [TestFixture]
    public class TranslationProviderDtoTests
    {
        [Test]
        public void TranslationProviderDto()
        {
            var dto = new TranslationProviderDto();

           var dtoType = dto.GetType();
            var properties = dtoType.GetProperties();

            dtoType.BaseType.Name.ShouldBe(nameof(BaseDto));

            AssertStringProperty(properties, "Value", dto.Value);
            AssertStringProperty(properties, "Description", dto.Description);
            AssertBooleanProperty(properties, "IsActive", dto.IsActive);

        }
    }
}