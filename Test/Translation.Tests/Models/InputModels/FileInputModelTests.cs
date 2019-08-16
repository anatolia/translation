using NUnit.Framework;

using Shouldly;

using static Translation.Tests.TestHelpers.FakeModelTestHelper;
using static Translation.Tests.TestHelpers.FakeConstantTestHelper;
namespace Translation.Tests.Models.InputModels
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