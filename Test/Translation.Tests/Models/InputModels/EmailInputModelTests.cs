using NUnit.Framework;

using static Translation.Tests.TestHelpers.FakeModelTestHelper;
using static Translation.Tests.TestHelpers.FakeConstantTestHelper;
namespace Translation.Tests.Models.InputModels
{
    [TestFixture]
    public class EmailInputModelTests
    {
        [Test]
        public void EmailInputModel_Constructor()
        {
            var model = GetEmailInputModel(StringOne, StringTwo, BooleanFalse);
        }
    }
}