using NUnit.Framework;
using Shouldly;
using static Translation.Tests.TestHelpers.FakeModelTestHelper;
using static Translation.Tests.TestHelpers.FakeConstantTestHelper;
namespace Translation.Tests.Models.InputModels
{
    [TestFixture]
    public class DateInputModelTests
    {
        [Test]
        public void DateInputModel_Constructor()
        {
            var model = GetDateInputModel(StringOne, StringTwo, BooleanFalse);
            model.Value.ShouldBe(DateTimeOne);
        }
    }
}