using NUnit.Framework;

using Shouldly;

using static Translation.Tests.TestHelpers.FakeModelTestHelper;
using static Translation.Tests.TestHelpers.FakeConstantTestHelper;
namespace Translation.Tests.Models.InputModels
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