using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Moq;
using StandardRepository.Models;
using Translation.Data.Entities.Main;
using Translation.Data.Repositories.Contracts;
using static Translation.Common.Tests.TestHelpers.FakeConstantTestHelper;
using static Translation.Common.Tests.TestHelpers.FakeEntityTestHelper;

namespace Translation.Common.Tests.SetupHelpers
{
    public static class SendEmailLogRepositorySetupHelper
    {
        public static void Setup_SelectAfter_Returns_SendEmailLogs(this Mock<ISendEmailLogRepository> repository)
        {
            repository.Setup(x => x.SelectAfter(It.IsAny<Expression<Func<SendEmailLog, bool>>>(),
                                                It.IsAny<Guid>(),
                                                It.IsAny<int>(),
                                                It.IsAny<bool>(),
                                                It.IsAny<List<OrderByInfo<SendEmailLog>>>()))
                      .ReturnsAsync(new List<SendEmailLog> { GetSendEmailLog() });
        }

        public static void Setup_SelectMany_Returns_SendEmailLogs(this Mock<ISendEmailLogRepository> repository)
        {
            repository.Setup(x => x.SelectMany(It.IsAny<Expression<Func<SendEmailLog, bool>>>(),
                                               It.IsAny<int>(),
                                               It.IsAny<int>(),
                                               It.IsAny<bool>(),
                                               It.IsAny<List<OrderByInfo<SendEmailLog>>>()))
                      .ReturnsAsync(new List<SendEmailLog> { GetSendEmailLog() });
        }

        public static void Setup_Count_Returns_Ten(this Mock<ISendEmailLogRepository> repository)
        {
            repository.Setup(x => x.Count(It.IsAny<Expression<Func<SendEmailLog, bool>>>(),
                                          It.IsAny<bool>(),
                                          It.IsAny<List<DistinctInfo<SendEmailLog>>>()))
                      .ReturnsAsync(Ten);
        }

        public static void Verify_SelectMany(this Mock<ISendEmailLogRepository> repository)
        {
            repository.Verify(x => x.SelectMany(It.IsAny<Expression<Func<SendEmailLog, bool>>>(),
                                                It.IsAny<int>(),
                                                It.IsAny<int>(),
                                                It.IsAny<bool>(),
                                                It.IsAny<List<OrderByInfo<SendEmailLog>>>()));
        }

        public static void Verify_SelectAfter(this Mock<ISendEmailLogRepository> repository)
        {
            repository.Verify(x => x.SelectAfter(It.IsAny<Expression<Func<SendEmailLog, bool>>>(),
                                                 It.IsAny<Guid>(),
                                                 It.IsAny<int>(),
                                                 It.IsAny<bool>(),
                                                 It.IsAny<List<OrderByInfo<SendEmailLog>>>()));
        }

        public static void Setup_Count_Returns_POSITIVE_INT_NUMBER_10(this Mock<ISendEmailLogRepository> repository)
        {
            repository.Setup(x => x.Count(It.IsAny<Expression<Func<SendEmailLog, bool>>>(),
                                          It.IsAny<bool>(),
                                          It.IsAny<List<DistinctInfo<SendEmailLog>>>()))
                      .ReturnsAsync(Ten);
        }

        public static void Verify_Count(this Mock<ISendEmailLogRepository> repository)
        {
            repository.Verify(x => x.Count(It.IsAny<Expression<Func<SendEmailLog, bool>>>(),
                                           It.IsAny<bool>(),
                                           It.IsAny<List<DistinctInfo<SendEmailLog>>>()));
        }
    }
}