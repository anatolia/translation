using NUnit.Framework;
using Shouldly;
using static Translation.Tests.TestHelpers.FakeModelTestHelper;
using static Translation.Tests.TestHelpers.FakeConstantTestHelper;

namespace Translation.Tests.Models.InputModels
{
    [TestFixture]
    public class UrlInputModelTests
    {
        [Test]
        public void UrlInputModel_Constructor()
        {
            var model = GetUrlInputModel(StringOne, StringTwo, BooleanFalse, StringThree);
        }
    }
}