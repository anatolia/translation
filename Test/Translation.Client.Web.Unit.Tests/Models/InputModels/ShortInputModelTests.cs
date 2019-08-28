using NUnit.Framework;

using static Translation.Common.Tests.TestHelpers.FakeModelTestHelper;
using static Translation.Common.Tests.TestHelpers.FakeConstantTestHelper;

namespace Translation.Client.Web.Unit.Tests.Models.InputModels
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