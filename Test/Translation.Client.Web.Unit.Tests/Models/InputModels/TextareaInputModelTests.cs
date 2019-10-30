using NUnit.Framework;
using Shouldly;

using static Translation.Client.Web.Unit.Tests.TestHelpers.FakeModelTestHelper;
using static Translation.Common.Tests.TestHelpers.FakeConstantTestHelper;

namespace Translation.Client.Web.Unit.Tests.Models.InputModels
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