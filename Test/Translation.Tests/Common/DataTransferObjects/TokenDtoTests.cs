using NUnit.Framework;

using Shouldly;

using Translation.Common.Models.Base;
using Translation.Common.Models.DataTransferObjects;
using static Translation.Tests.TestHelpers.AssertPropertyTestHelper;

namespace Translation.Tests.Common.DataTransferObjects
{
    [TestFixture]
    public class TokenDtoTests
    {
        [Test]
        public void TokenDto()
        {
            var dto = new TokenDto();

           var dtoType = dto.GetType();
            var properties = dtoType.GetProperties();

            dtoType.BaseType.Name.ShouldBe(nameof(BaseDto));

            AssertGuidProperty(properties, "IntegrationClientUid", dto.IntegrationClientUid);
            AssertGuidProperty(properties, "AccessToken", dto.AccessToken);
            AssertDateTimeProperty(properties, "ExpiresAt",dto.ExpiresAt);
            AssertStringProperty(properties, "IP", dto.IP);

        }
    }
}