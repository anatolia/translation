using NUnit.Framework;
using Shouldly;
using StandardRepository.Models.Entities;
using StandardRepository.Models.Entities.Schemas;


using Translation.Data.Entities.Main;
using static Translation.Common.Tests.TestHelpers.AssertPropertyTestHelper;

namespace Translation.Server.Unit.Tests.Data.Entities
{
    [TestFixture]
    public class UserLoginLogEntityTests
    {
        [Test]
        public void UserLoginLog()
        {
            var entity = new UserLoginLog();

            var entityType = entity.GetType();
            var properties = entityType.GetProperties();

            entityType.BaseType.Name.ShouldBe(nameof(BaseEntity));
            entityType.GetInterface(nameof(ISchemaMain)).ShouldNotBeNull();

            AssertLongProperty(properties, "OrganizationId", entity.OrganizationId);
            AssertGuidProperty(properties, "OrganizationUid", entity.OrganizationUid);
            AssertStringProperty(properties, "OrganizationName", entity.OrganizationName);

            AssertLongProperty(properties, "UserId", entity.UserId);
            AssertGuidProperty(properties, "UserUid", entity.UserUid);
            AssertStringProperty(properties, "UserName", entity.UserName);

            AssertStringProperty(properties, "UserAgent", entity.UserAgent);
            AssertStringProperty(properties, "Ip", entity.Ip);
            AssertStringProperty(properties, "Country", entity.Country);
            AssertStringProperty(properties, "City", entity.City);
            AssertStringProperty(properties, "Browser", entity.Browser);
            AssertStringProperty(properties, "BrowserVersion", entity.BrowserVersion);
            AssertStringProperty(properties, "Platform", entity.Platform);
            AssertStringProperty(properties, "PlatformVersion", entity.PlatformVersion);
        }
    }
}