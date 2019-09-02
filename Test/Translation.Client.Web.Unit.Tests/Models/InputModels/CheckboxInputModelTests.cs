using NUnit.Framework;
using Shouldly;

using static Translation.Common.Tests.TestHelpers.FakeModelTestHelper;
using static Translation.Common.Tests.TestHelpers.FakeConstantTestHelper;

namespace Translation.Client.Web.Unit.Tests.Models.InputModels
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