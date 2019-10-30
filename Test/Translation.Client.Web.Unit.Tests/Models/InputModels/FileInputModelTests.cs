using NUnit.Framework;
using Shouldly;

using static Translation.Client.Web.Unit.Tests.TestHelpers.FakeModelTestHelper;
using static Translation.Common.Tests.TestHelpers.FakeConstantTestHelper;

namespace Translation.Client.Web.Unit.Tests.Models.InputModels
{
    [TestFixture]
    public class FileInputModelTests
    {
        [Test]
        public void FileInputModel_Constructor()
        {
            var model = GetFileInputModel(StringOne, StringTwo, BooleanFalse, BooleanFalse);
            model.IsMultiple.ShouldBe(BooleanFalse);
        }
    }
}