using NUnit.Framework;
using Shouldly;

using static Translation.Client.Web.Unit.Tests.TestHelpers.FakeModelTestHelper;
using static Translation.Common.Tests.TestHelpers.FakeConstantTestHelper;

namespace Translation.Client.Web.Unit.Tests.Models.InputModels
{
    [TestFixture]
    public class HiddenInputModelTests
    {
        [Test]
        public void HiddenInputModel_Constructor()
        {
            var model = GetHiddenInputModel(StringOne, StringTwo);

            model.Value.ShouldBe(StringTwo);
        }
    }
}