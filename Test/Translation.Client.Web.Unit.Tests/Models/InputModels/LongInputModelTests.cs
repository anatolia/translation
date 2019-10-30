using NUnit.Framework;

using static Translation.Client.Web.Unit.Tests.TestHelpers.FakeModelTestHelper;
using static Translation.Common.Tests.TestHelpers.FakeConstantTestHelper;

namespace Translation.Client.Web.Unit.Tests.Models.InputModels
{
    [TestFixture]
    public class LongInputModelTests
    {
        [Test]
        public void LongInputModel_Constructor()
        {
            var model = GetLongInputModel(StringOne, StringTwo, BooleanFalse);

        }
    }
}