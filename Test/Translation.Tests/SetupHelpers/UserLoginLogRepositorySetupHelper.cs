using System;
using System.Linq.Expressions;

using Moq;

using Translation.Data.Entities.Main;
using Translation.Data.Repositories.Contracts;
using static Translation.Tests.TestHelpers.FakeEntityTestHelper;
using static Translation.Tests.TestHelpers.FakeConstantTestHelper;

namespace Translation.Tests.SetupHelpers
{
    public static class UserLoginLogRepositorySetupHelper
    {
        public static void Setup_Count_Returns_POSITIVE_INT_NUMBER_10(this Mock<IUserLoginLogRepository> repository)
        {
            repository.Setup(x => x.Count(It.IsAny<Expression<Func<UserLoginLog, bool>>>(), false))
                .ReturnsAsync(Ten);
        }

        public static void Verify_Count(this Mock<IUserLoginLogRepository> repository)
        {
            repository.Verify(x => x.Count(It.IsAny<Expression<Func<UserLoginLog, bool>>>(), false));
        }

        //public static void Setup_SelectMany_Returns_ParkNetUserLoginLogList(this Mock<IUserLoginLogRepository> repository)
        //{
        //    repository.Setup(x => x.SelectMany(It.IsAny<Expression<Func<UserLoginLog, bool>>>(),
        //            It.IsAny<int>(),
        //            It.IsAny<int>(),
        //            It.IsAny<Expression<Func<UserLoginLog, object>>>(),
        //            It.IsAny<bool>(), false))
        //        .ReturnsAsync(EntityDataHelper.GetFakeGetFakeParkNetUserLoginLogList());
        //}

        public static void Verify_SelectMany(this Mock<IUserLoginLogRepository> repository)
        {
            repository.Verify(x => x.SelectMany(It.IsAny<Expression<Func<UserLoginLog, bool>>>(),
                    It.IsAny<int>(),
                    It.IsAny<int>(),
                    It.IsAny<Expression<Func<UserLoginLog, object>>>(),
                    It.IsAny<bool>(), false));
        }

        //public static void Setup_SelectAfter_Returns_ParkNetUserLoginLogList(this Mock<IUserLoginLogRepository> repository)
        //{
        //    repository.Setup(x => x.SelectAfter(It.IsAny<Expression<Func<UserLoginLog, bool>>>(),
        //            It.IsAny<Guid>(),
        //            It.IsAny<int>(),
        //            It.IsAny<Expression<Func<UserLoginLog, object>>>(),
        //            It.IsAny<bool>(), false))
        //        .ReturnsAsync(EntityDataHelper.GetFakeGetFakeParkNetUserLoginLogList());
        //}

        public static void Verify_SelectAfter(this Mock<IUserLoginLogRepository> repository)
        {
            repository.Verify(x => x.SelectAfter(It.IsAny<Expression<Func<UserLoginLog, bool>>>(),
                    It.IsAny<Guid>(),
                    It.IsAny<int>(),
                    It.IsAny<Expression<Func<UserLoginLog, object>>>(),
                    It.IsAny<bool>(), false));
        }

        //public static void Setup_SelectAfter_Returns_UserLoginLogList(this Mock<IUserLoginLogRepository> repository)
        //{
        //    repository.Setup(x => x.SelectAfter(It.IsAny<Expression<Func<UserLoginLog, bool>>>(),
        //            It.IsAny<long>(),
        //            It.IsAny<int>(),
        //            It.IsAny<Expression<Func<UserLoginLog, object>>>(),
        //            It.IsAny<bool>(), false))
        //        .ReturnsAsync(EntityDataHelper.GetFakeParkNetUserLoginLogList());
        //}
    }
}