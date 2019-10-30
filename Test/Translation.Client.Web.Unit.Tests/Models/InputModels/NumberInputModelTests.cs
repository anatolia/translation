using NUnit.Framework;
using Shouldly;

using static Translation.Client.Web.Unit.Tests.TestHelpers.FakeModelTestHelper;
using static Translation.Common.Tests.TestHelpers.FakeConstantTestHelper;

namespace Translation.Client.Web.Unit.Tests.Models.InputModels
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
