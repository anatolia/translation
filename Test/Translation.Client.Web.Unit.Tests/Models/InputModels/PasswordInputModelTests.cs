using NUnit.Framework;

using static Translation.Client.Web.Unit.Tests.TestHelpers.FakeModelTestHelper;
using static Translation.Common.Tests.TestHelpers.FakeConstantTestHelper;

namespace Translation.Client.Web.Unit.Tests.Models.InputModels
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