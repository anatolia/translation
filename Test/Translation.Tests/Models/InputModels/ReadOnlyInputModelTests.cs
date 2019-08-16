using NUnit.Framework;
using Shouldly;
using static Translation.Tests.TestHelpers.FakeModelTestHelper;
using static Translation.Tests.TestHelpers.FakeConstantTestHelper;

namespace Translation.Tests.Models.InputModels
{
    [TestFixture]
    public class ReadOnlyInputModelTests
    {
        [Test]
        public void ReadOnlyInputModel_Constructor()
        {
            var model = GetReadOnlyInputModel(StringOne, StringTwo);
        }
    }
}