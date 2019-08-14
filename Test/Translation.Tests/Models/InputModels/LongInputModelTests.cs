using NUnit.Framework;

using Shouldly;

using static Translation.Tests.TestHelpers.FakeModelTestHelper;
using static Translation.Tests.TestHelpers.FakeConstantTestHelper;
namespace Translation.Tests.Models.InputModels
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