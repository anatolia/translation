using NUnit.Framework;

using static Translation.Client.Web.Unit.Tests.TestHelpers.FakeModelTestHelper;
using static Translation.Common.Tests.TestHelpers.FakeConstantTestHelper;

namespace Translation.Client.Web.Unit.Tests.Models.InputModels
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