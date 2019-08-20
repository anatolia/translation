using NUnit.Framework;

using Shouldly;

using Translation.Common.Models.Base;
using Translation.Common.Models.DataTransferObjects;
using static Translation.Tests.TestHelpers.AssertPropertyTestHelper;
namespace Translation.Tests.Common.DataTransferObjects
{
    [TestFixture]
    public class UserLoginLogDtoTests
    {
        [Test]
        public void UserLoginLogDto()
        {
            var dto = new UserLoginLogDto();

            var dtoType = dto.GetType();
            var properties = dtoType.GetProperties();

            dtoType.BaseType.Name.ShouldBe(nameof(BaseDto));

            AssertGuidProperty(properties, "OrganizationUid", dto.OrganizationUid);
            AssertStringProperty(properties, "OrganizationName", dto.OrganizationName);

            AssertGuidProperty(properties, "UserUid", dto.UserUid);
            AssertStringProperty(properties, "UserName", dto.UserName);

            AssertStringProperty(properties, "UserAgent", dto.UserAgent);
            AssertStringProperty(properties, "Ip", dto.Ip);
            AssertStringProperty(properties, "Country", dto.Country);
            AssertStringProperty(properties, "City", dto.City);
            AssertStringProperty(properties, "Browser", dto.Browser);
            AssertStringProperty(properties, "BrowserVersion", dto.BrowserVersion);
            AssertStringProperty(properties, "Platform", dto.Platform);
            AssertStringProperty(properties, "PlatformVersion", dto.PlatformVersion);
        }
    }
}