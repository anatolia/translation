using NUnit.Framework;

using Shouldly;

using static Translation.Tests.TestHelpers.FakeModelTestHelper;
using static Translation.Tests.TestHelpers.FakeConstantTestHelper;
namespace Translation.Tests.Models.InputModels
{
    [TestFixture]
    public class SelectInputModelTests
    {
        [Test]
        public void SelectInputModel_Constructor()
        {
            var model = GetSelectInputModel(StringOne, StringTwo, HttpUrl, BooleanTrue, HttpsUrl);

            model.TextFieldName.ShouldBe($"{StringOne}Name");
            model.DataUrl.ShouldBe(HttpUrl);
            model.AddNewUrl.ShouldBe(HttpsUrl);
            model.IsAddNewEnabled.ShouldBe(!string.IsNullOrEmpty(HttpsUrl));
            model.IsSetFirstItem.ShouldBe(BooleanTrue);
        }
    }
}