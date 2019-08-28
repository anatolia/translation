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
    public static class TokenRequestLogRepositorySetupHelper
    {
        public static void Setup_SelectAfter_Returns_TokenRequestLogs(this Mock<ITokenRequestLogRepository> repository)
        {
            repository.Setup(x => x.SelectAfter(It.IsAny<Expression<Func<TokenRequestLog, bool>>>(),
                                                It.IsAny<Guid>(),
                                                It.IsAny<int>(),
                                                It.IsAny<Expression<Func<TokenRequestLog, object>>>(),
                                                It.IsAny<bool>(), false))
                      .ReturnsAsync(new List<TokenRequestLog> { GetTokenRequestLog() });
        }

        public static void Setup_SelectMany_Returns_TokenRequestLogs(this Mock<ITokenRequestLogRepository> repository)
        {
            repository.Setup(x => x.SelectMany(It.IsAny<Expression<Func<TokenRequestLog, bool>>>(),
                                               It.IsAny<int>(),
                                               It.IsAny<int>(),
                                               It.IsAny<Expression<Func<TokenRequestLog, object>>>(),
                                               It.IsAny<bool>(), false))
                      .ReturnsAsync(new List<TokenRequestLog> { GetTokenRequestLog() });
        }

        public static void Setup_Count_Returns_Ten(this Mock<ITokenRequestLogRepository> repository)
        {
            repository.Setup(x => x.Count(It.IsAny<Expression<Func<TokenRequestLog, bool>>>(),
                                          It.IsAny<bool>(),
                                          It.IsAny<List<DistinctInfo<TokenRequestLog>>>()))
                      .ReturnsAsync(Ten);
        }

        public static void Verify_Count(this Mock<ITokenRequestLogRepository> repository)
        {
            repository.Verify(x => x.Count(It.IsAny<Expression<Func<TokenRequestLog, bool>>>(),
                                           It.IsAny<bool>(),
                                           It.IsAny<List<DistinctInfo<TokenRequestLog>>>()));
        }

        public static void Verify_SelectMany(this Mock<ITokenRequestLogRepository> repository)
        {
            repository.Verify(x => x.SelectMany(It.IsAny<Expression<Func<TokenRequestLog, bool>>>(),
                                                It.IsAny<int>(),
                                                It.IsAny<int>(),
                                                It.IsAny<Expression<Func<TokenRequestLog, object>>>(),
                                                It.IsAny<bool>(), false));
        }

        public static void Verify_SelectAfter(this Mock<ITokenRequestLogRepository> repository)
        {
            repository.Verify(x => x.SelectAfter(It.IsAny<Expression<Func<TokenRequestLog, bool>>>(),
                                                 It.IsAny<Guid>(),
                                                 It.IsAny<int>(),
                                                 It.IsAny<Expression<Func<TokenRequestLog, object>>>(),
                                                 It.IsAny<bool>(), false));
        }
    }
}