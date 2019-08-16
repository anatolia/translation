using NUnit.Framework;

using Shouldly;

using static Translation.Tests.TestHelpers.FakeModelTestHelper;
using static Translation.Tests.TestHelpers.FakeConstantTestHelper;
namespace Translation.Tests.Models.InputModels
{
    [TestFixture]
    public class InputModelTests
    {
        [Test]
        public void InputModel_ConstructorValue()
        {
            var model = GetInputModel(StringOne, StringTwo, BooleanFalse, StringThree);
            model.Name.ShouldBe(StringOne);
            model.LabelKey.ShouldBe(StringTwo);
            model.IsRequired.ShouldBe(BooleanFalse);
            model.Value.ShouldBe(StringThree);
        }

        [Test]
        public void InputModel_ConstructorIsRequired()
        {
            var model = GetInputModel(StringOne, StringTwo, BooleanFalse);
            model.Name.ShouldBe(StringOne);
            model.LabelKey.ShouldBe(StringTwo);
            model.IsRequired.ShouldBe(BooleanFalse);
        }

        [Test]
        public void InputModel_ConstructorName()
        {
            var model = GetInputModel(StringOne, StringTwo);

            model.Name.ShouldBe(StringOne);
            model.LabelKey.ShouldBe(StringTwo);
        }

        [Test]
        public void InputModel_Parameter()
        {
            var model = GetInputModel();

            model.InfoText.ShouldBe(StringOne);
        }
    }
}