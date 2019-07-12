using Moq;

using Translation.Data.Entities.Domain;
using Translation.Data.UnitOfWorks.Contracts;
using static Translation.Tests.TestHelpers.FakeConstantTestHelper;

namespace Translation.Tests.SetupHelpers
{
    public static class ProjectUnitOfWorkSetupHelper
    {
        public static void Setup_DoCloneWork_Returns_True(this Mock<IProjectUnitOfWork> unitOfWork)
        {
            unitOfWork.Setup(x => x.DoCloneWork(It.IsAny<long>(), It.IsAny<long>(), It.IsAny<Project>()))
                      .ReturnsAsync(BooleanTrue);
        }

        public static void Setup_DoCreateWork_Returns_True(this Mock<IProjectUnitOfWork> unitOfWork)
        {
            unitOfWork.Setup(x => x.DoCreateWork(It.IsAny<long>(), It.IsAny<Project>()))
                      .ReturnsAsync(BooleanTrue);
        }

        public static void Verify_DoCloneWork(this Mock<IProjectUnitOfWork> unitOfWork)
        {
            unitOfWork.Verify(x => x.DoCloneWork(It.IsAny<long>(), It.IsAny<long>(), It.IsAny<Project>()));
        }

        public static void Verify_DoCreateWork(this Mock<IProjectUnitOfWork> unitOfWork)
        {
            unitOfWork.Verify(x => x.DoCreateWork(It.IsAny<long>(), It.IsAny<Project>()));
        }
    }
}