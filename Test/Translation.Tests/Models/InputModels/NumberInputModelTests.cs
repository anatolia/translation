using NUnit.Framework;

using Shouldly;

using static Translation.Tests.TestHelpers.FakeModelTestHelper;
using static Translation.Tests.TestHelpers.FakeConstantTestHelper;
namespace Translation.Tests.Models.InputModels
{
    [TestFixture]
    public class NumberInputModelTests
    {
        [Test]
        public void NumberInputModel_Constructor()
        {
            var model = GetNumberInputModel(StringOne, StringTwo, BooleanFalse, Zero);
            model.Value.ShouldBe(Zero);
        }
    }
}
