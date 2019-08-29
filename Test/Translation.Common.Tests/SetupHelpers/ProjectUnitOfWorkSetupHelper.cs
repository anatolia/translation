using Moq;
using Translation.Data.Entities.Domain;
using Translation.Data.UnitOfWorks.Contracts;
using static Translation.Common.Tests.TestHelpers.FakeConstantTestHelper;
namespace Translation.Common.Tests.SetupHelpers
{
    public static class ProjectUnitOfWorkSetupHelper
    {
        public static void Setup_DoDeleteWork_Returns_True(this Mock<IProjectUnitOfWork> unitOfWork)
        {
            unitOfWork.Setup(x => x.DoDeleteWork(It.IsAny<long>(), It.IsAny<Project>()))
                      .ReturnsAsync(BooleanTrue);
        }

        public static void Setup_DoDeleteWork_Returns_False(this Mock<IProjectUnitOfWork> unitOfWork)
        {
            unitOfWork.Setup(x => x.DoDeleteWork(It.IsAny<long>(), It.IsAny<Project>()))
                      .ReturnsAsync(BooleanFalse);
        }

        public static void Verify_DoDeleteWork(this Mock<IProjectUnitOfWork> unitOfWork)
        {
            unitOfWork.Verify(x => x.DoDeleteWork(It.IsAny<long>(), It.IsAny<Project>()));
        }

        public static void Setup_DoCloneWork_Returns_True(this Mock<IProjectUnitOfWork> unitOfWork)
        {
            unitOfWork.Setup(x => x.DoCloneWork(It.IsAny<long>(), It.IsAny<long>(), It.IsAny<Project>()))
                      .ReturnsAsync(BooleanTrue);
        }

        public static void Setup_DoCloneWork_Returns_False(this Mock<IProjectUnitOfWork> unitOfWork)
        {
            unitOfWork.Setup(x => x.DoCloneWork(It.IsAny<long>(), It.IsAny<long>(), It.IsAny<Project>()))
                      .ReturnsAsync(false);
        }

        public static void Setup_DoCreateWork_Returns_True(this Mock<IProjectUnitOfWork> unitOfWork)
        {
            unitOfWork.Setup(x => x.DoCreateWork(It.IsAny<long>(), It.IsAny<Project>()))
                      .ReturnsAsync(BooleanTrue);
        }

        public static void Setup_DoCreateWork_Returns_False(this Mock<IProjectUnitOfWork> unitOfWork)
        {
            unitOfWork.Setup(x => x.DoCreateWork(It.IsAny<long>(), It.IsAny<Project>()))
                      .ReturnsAsync(BooleanFalse);
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