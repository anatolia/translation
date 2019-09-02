using NUnit.Framework;

using static Translation.Common.Tests.TestHelpers.FakeModelTestHelper;
using static Translation.Common.Tests.TestHelpers.FakeConstantTestHelper;

namespace Translation.Client.Web.Unit.Tests.Models.InputModels
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