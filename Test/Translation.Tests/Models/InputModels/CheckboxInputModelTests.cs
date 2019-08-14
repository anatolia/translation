using NUnit.Framework;
using Shouldly;
using static Translation.Tests.TestHelpers.FakeModelTestHelper;
using static Translation.Tests.TestHelpers.FakeConstantTestHelper;
namespace Translation.Tests.Models.InputModels
{
    [TestFixture]
    public class CheckboxInputModelTests
    {
        [Test]
        public void CheckboxInputModel_Constructor()
        {
            var model = GetCheckboxInputModel(StringOne, StringTwo, BooleanFalse, BooleanFalse, BooleanFalse);

            model.IsReadOnly.ShouldBe(BooleanFalse);
            model.Value.ShouldBe(BooleanFalse);
        }
    }
}