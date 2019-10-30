using System;
using System.Collections.Generic;
using System.Linq.Expressions;

using Moq;

using StandardRepository.Models;

using Translation.Data.Entities.Main;
using Translation.Data.Repositories.Contracts;
using static Translation.Common.Tests.TestHelpers.FakeConstantTestHelper;
using static Translation.Server.Unit.Tests.TestHelpers.FakeEntityTestHelper;

namespace Translation.Server.Unit.Tests.RepositorySetupHelpers
{
    public static class UserLoginLogRepositorySetupHelper
    {
        public static void Setup_SelectAfter_Returns_UserLoginLogs(this Mock<IUserLoginLogRepository> repository)
        {
            repository.Setup(x => x.SelectAfter(It.IsAny<Expression<Func<UserLoginLog, bool>>>(),
                                                It.IsAny<Guid>(),
                                                It.IsAny<int>(),
                                                It.IsAny<bool>(),
                                                It.IsAny<List<OrderByInfo<UserLoginLog>>>()))
                     .ReturnsAsync(new List<UserLoginLog> { GetUserLoginLog() });
        }

        public static void Setup_SelectMany_Returns_UserLoginLogs(this Mock<IUserLoginLogRepository> repository)
        {
            repository.Setup(x => x.SelectMany(It.IsAny<Expression<Func<UserLoginLog, bool>>>(),
                                               It.IsAny<int>(),
                                               It.IsAny<int>(),
                                               It.IsAny<bool>(),
                                               It.IsAny<List<OrderByInfo<UserLoginLog>>>()))
                     .ReturnsAsync(new List<UserLoginLog> { GetUserLoginLog() });
        }

        public static void Setup_Count_Returns_Ten(this Mock<IUserLoginLogRepository> repository)
        {
            repository.Setup(x => x.Count(It.IsAny<Expression<Func<UserLoginLog, bool>>>(),
                                          It.IsAny<bool>(),
                                          It.IsAny<List<DistinctInfo<UserLoginLog>>>()))
                      .ReturnsAsync(Ten);
        }

        public static void Verify_Count(this Mock<IUserLoginLogRepository> repository)
        {
            repository.Verify(x => x.Count(It.IsAny<Expression<Func<UserLoginLog, bool>>>(),
                                           It.IsAny<bool>(),
                                           It.IsAny<List<DistinctInfo<UserLoginLog>>>()));
        }

        public static void Verify_SelectMany(this Mock<IUserLoginLogRepository> repository)
        {
            repository.Verify(x => x.SelectMany(It.IsAny<Expression<Func<UserLoginLog, bool>>>(),
                                                It.IsAny<int>(),
                                                It.IsAny<int>(),
                                                It.IsAny<bool>(),
                                                It.IsAny<List<OrderByInfo<UserLoginLog>>>()));
        }

        public static void Verify_SelectAfter(this Mock<IUserLoginLogRepository> repository)
        {
            repository.Verify(x => x.SelectAfter(It.IsAny<Expression<Func<UserLoginLog, bool>>>(),
                                                 It.IsAny<Guid>(),
                                                 It.IsAny<int>(),
                                                 It.IsAny<bool>(),
                                                 It.IsAny<List<OrderByInfo<UserLoginLog>>>()));
        }
    }
}