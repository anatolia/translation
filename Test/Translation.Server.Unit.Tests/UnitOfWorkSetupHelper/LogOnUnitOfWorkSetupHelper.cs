using Moq;

using Translation.Data.Entities.Main;
using Translation.Data.UnitOfWorks.Contracts;
using static Translation.Common.Tests.TestHelpers.FakeConstantTestHelper;

namespace Translation.Server.Unit.Tests.UnitOfWorkSetupHelper
{
    public static class LogOnUnitOfWorkSetupHelper
    {
        public static void Setup_DoWork_Returns_True(this Mock<ILogOnUnitOfWork> unitOfWork)
        {
            unitOfWork.Setup(x => x.DoWork(It.IsAny<User>(),
                                           It.IsAny<UserLoginLog>()))
                      .ReturnsAsync(BooleanTrue);
        }

        public static void Setup_DoWork_Returns_False(this Mock<ILogOnUnitOfWork> unitOfWork)
        {
            unitOfWork.Setup(x => x.DoWork(It.IsAny<User>(),
                                           It.IsAny<UserLoginLog>()))
                      .ReturnsAsync(BooleanFalse);
        }

        public static void Verify_DoWork(this Mock<ILogOnUnitOfWork> unitOfWork)
        {
            unitOfWork.Verify(x => x.DoWork(It.IsAny<User>(),
                                            It.IsAny<UserLoginLog>()));
        }
    }
}