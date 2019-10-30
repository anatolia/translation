using NUnit.Framework;
using Shouldly;

using static Translation.Client.Web.Unit.Tests.TestHelpers.FakeModelTestHelper;
using static Translation.Common.Tests.TestHelpers.FakeConstantTestHelper;

namespace Translation.Client.Web.Unit.Tests.Models.InputModels
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