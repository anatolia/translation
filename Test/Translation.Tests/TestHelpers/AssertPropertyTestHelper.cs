using System;
using System.Linq;
using System.Reflection;

using Shouldly;

namespace Translation.Tests.TestHelpers
{
    public class AssertPropertyTestHelper
    {
        public static void AssertInstantProperty(PropertyInfo[] properties, string propertyName, DateTime propertyValue)
        {
            propertyValue.ShouldNotBeNull();

            var propertyInfo = properties.First(x => x.Name == propertyName);
            propertyInfo.PropertyType.ShouldBe(typeof(DateTime));
        }

        public static void AssertNullableDateTimeProperty(PropertyInfo[] properties, string propertyName, DateTime? propertyValue)
        {
            propertyValue.ShouldBeNull();

            var propertyInfo = properties.First(x => x.Name == propertyName);
            propertyInfo.PropertyType.ShouldBe(typeof(DateTime?));
        }

        public static void AssertStringProperty(PropertyInfo[] properties, string propertyName, string propertyValue)
        {
            string.IsNullOrEmpty(propertyValue).ShouldBeTrue();

            var propertyInfo = properties.First(x => x.Name == propertyName);
            propertyInfo.PropertyType.Name.ShouldBe(nameof(String));
        }

        public static void AssertBooleanProperty(PropertyInfo[] properties, string propertyName, bool propertyValue)
        {
            propertyValue.ShouldBeFalse();

            var propertyInfo = properties.First(x => x.Name == propertyName);
            propertyInfo.PropertyType.Name.ShouldBe(nameof(Boolean));
        }

        public static void AssertIntegerProperty(PropertyInfo[] properties, string propertyName, int propertyValue)
        {
            propertyValue.ShouldBe(0);

            var propertyInfo = properties.First(x => x.Name == propertyName);
            propertyInfo.PropertyType.Name.ShouldBe(nameof(Int32));
        }

        public static void AssertLongProperty(PropertyInfo[] properties, string propertyName, long propertyValue)
        {
            propertyValue.ShouldBe(0);

            var propFirstName = properties.First(x => x.Name == propertyName);
            propFirstName.PropertyType.Name.ShouldBe(nameof(Int64));
        }
        public static void AssertNullableLongProperty(PropertyInfo[] properties, string propertyName, long? propertyValue)
        {
            propertyValue.ShouldBeNull();

            var propFirstName = properties.First(x => x.Name == propertyName);
            propFirstName.PropertyType.ShouldBe(typeof(long?));
        }
        public static void AssertGuidProperty(PropertyInfo[] properties, string propertyName, Guid propertyValue)
        {
            propertyValue.ToString().ShouldBe("00000000-0000-0000-0000-000000000000");

            var propFirstName = properties.First(x => x.Name == propertyName);
            propFirstName.PropertyType.Name.ShouldBe(nameof(Guid));
        }
        public static void AssertNullableGuidProperty(PropertyInfo[] properties, string propertyName, Guid? propertyValue)
        {
            propertyValue.ShouldBeNull();

            var propFirstName = properties.First(x => x.Name == propertyName);
            propFirstName.PropertyType.ShouldBe(typeof(Guid?));
        }
    }
}