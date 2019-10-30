using NUnit.Framework;
using Shouldly;

using static Translation.Client.Web.Unit.Tests.TestHelpers.FakeModelTestHelper;
using static Translation.Common.Tests.TestHelpers.FakeConstantTestHelper;

namespace Translation.Client.Web.Unit.Tests.Models.InputModels
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

        [Test]
        public void SelectInputModel_Parameter()
        {
            var model = GetSelectInputModel();

            model.IsAllOptionsVisible.ShouldBe(BooleanTrue);
            model.IsMultiple.ShouldBe(BooleanTrue);
            model.IsHavingDetailInfo.ShouldBe(BooleanTrue);
            model.DetailInfoDataUrl.ShouldBe(HttpUrl);
            model.Parent.ShouldBe(StringOne);
        }
    }
}