using Moq;

using Translation.Data.Entities.Domain;
using Translation.Data.UnitOfWorks.Contracts;
using static Translation.Tests.TestHelpers.FakeConstantTestHelper;

namespace Translation.Tests.SetupHelpers
{
    public static class LabelUnitOfWorkSetupHelper
    {
        public static void Setup_DoCreateWork_Returns_True(this Mock<ILabelUnitOfWork> unitOfWork)
        {
            unitOfWork.Setup(x => x.DoCreateWork(It.IsAny<long>(), 
                                                 It.IsAny<Label>()))
                      .ReturnsAsync(BooleanTrue);
        }

        public static void Setup_DoCreateWork_Returns_False(this Mock<ILabelUnitOfWork> unitOfWork)
        {
            unitOfWork.Setup(x => x.DoCreateWork(It.IsAny<long>(),
                                                 It.IsAny<Label>()))
                      .ReturnsAsync(BooleanFalse);
        }

        public static void Verify_DoCreateWork(this Mock<ILabelUnitOfWork> unitOfWork)
        {
            unitOfWork.Verify(x => x.DoCreateWork(It.IsAny<long>(),
                                                  It.IsAny<Label>()));
        }
    }
}