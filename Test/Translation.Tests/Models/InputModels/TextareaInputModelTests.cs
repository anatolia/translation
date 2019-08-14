using NUnit.Framework;

using Shouldly;

using static Translation.Tests.TestHelpers.FakeModelTestHelper;
using static Translation.Tests.TestHelpers.FakeConstantTestHelper;
namespace Translation.Tests.Models.InputModels
{
    [TestFixture]
    public class TextareaInputModelTests
    {
        [Test]
        public void TextareaInputModel_Constructor()
        {
            var model = GetTextareaInputModel(StringOne, StringTwo, BooleanFalse, StringThree);
            model.Cols.ShouldBe(70);
            model.Rows.ShouldBe(5);
        }
    }
}