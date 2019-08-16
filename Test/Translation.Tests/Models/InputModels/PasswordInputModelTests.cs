using NUnit.Framework;

using Shouldly;

using static Translation.Tests.TestHelpers.FakeModelTestHelper;
using static Translation.Tests.TestHelpers.FakeConstantTestHelper;
namespace Translation.Tests.Models.InputModels
{
    [TestFixture]
    public class PasswordInputModelTests
    {
        [Test]
        public void PasswordInputModel_Constructor()
        {
            var model = GetPasswordInputModel(StringOne, StringTwo, BooleanFalse, StringThree);
        }
    }
}