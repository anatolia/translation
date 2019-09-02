using NUnit.Framework;

using static Translation.Common.Tests.TestHelpers.FakeModelTestHelper;
using static Translation.Common.Tests.TestHelpers.FakeConstantTestHelper;

namespace Translation.Client.Web.Unit.Tests.Models.InputModels
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