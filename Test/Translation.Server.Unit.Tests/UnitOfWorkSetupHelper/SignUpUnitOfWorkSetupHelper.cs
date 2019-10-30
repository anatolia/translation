using Moq;

using Translation.Data.Entities.Domain;
using Translation.Data.Entities.Main;
using Translation.Data.UnitOfWorks.Contracts;
using static Translation.Server.Unit.Tests.TestHelpers.FakeEntityTestHelper;

namespace Translation.Server.Unit.Tests.UnitOfWorkSetupHelper
{
    public static class SignUpUnitOfWorkSetupHelper
    {
        public static void Setup_DoWork_Returns_TrueOrganizationUser(this Mock<ISignUpUnitOfWork> unitOfWork)
        {
            unitOfWork.Setup(x => x.DoWork(It.IsAny<Organization>(),
                                           It.IsAny<User>(),
                                           It.IsAny<UserLoginLog>(),
                                           It.IsAny<Integration>(),
                                           It.IsAny<IntegrationClient>(),
                                           It.IsAny<Project>()))
                      .ReturnsAsync(GetTrueOrganizationUser());
        }

        public static void Setup_DoWork_Returns_FalseOrganizationUser(this Mock<ISignUpUnitOfWork> unitOfWork)
        {
            unitOfWork.Setup(x => x.DoWork(It.IsAny<Organization>(),
                                           It.IsAny<User>(),
                                           It.IsAny<UserLoginLog>(),
                                           It.IsAny<Integration>(),
                                           It.IsAny<IntegrationClient>(),
                                           It.IsAny<Project>()))
                      .ReturnsAsync(GetFalseOrganizationUser());
        }

        public static void Verify_DoWork(this Mock<ISignUpUnitOfWork> unitOfWork)
        {
            unitOfWork.Verify(x => x.DoWork(It.IsAny<Organization>(),
                                            It.IsAny<User>(),
                                            It.IsAny<UserLoginLog>(),
                                            It.IsAny<Integration>(),
                                            It.IsAny<IntegrationClient>(),
                                            It.IsAny<Project>()));
        }
    }
}