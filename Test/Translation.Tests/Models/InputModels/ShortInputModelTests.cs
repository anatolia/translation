using NUnit.Framework;

using Shouldly;

using static Translation.Tests.TestHelpers.FakeModelTestHelper;
using static Translation.Tests.TestHelpers.FakeConstantTestHelper;
namespace Translation.Tests.Models.InputModels
{
    [TestFixture]
    public class ShortInputModelTests
    {
        [Test]
        public void ShortInputModel_Constructor()
        {
            var model = GetShortInputModel(StringOne, StringTwo, BooleanFalse, StringThree);
        }
    }
}