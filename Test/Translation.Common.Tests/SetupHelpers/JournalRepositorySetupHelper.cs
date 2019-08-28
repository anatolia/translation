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
    public static class JournalRepositorySetupHelper
    {
        public static void Setup_SelectAfter_Returns_Journals(this Mock<IJournalRepository> repository)
        {
            repository.Setup(x => x.SelectAfter(It.IsAny<Expression<Func<Journal, bool>>>(),
                                                It.IsAny<Guid>(),
                                                It.IsAny<int>(),
                                                It.IsAny<Expression<Func<Journal, object>>>(),
                                                It.IsAny<bool>(), false))
                      .ReturnsAsync(new List<Journal> { GetJournal() });
        }

        public static void Setup_SelectMany_Returns_Journals(this Mock<IJournalRepository> repository)
        {
            repository.Setup(x => x.SelectMany(It.IsAny<Expression<Func<Journal, bool>>>(),
                                               It.IsAny<int>(),
                                               It.IsAny<int>(),
                                               It.IsAny<Expression<Func<Journal, object>>>(),
                                               It.IsAny<bool>(), false))
                      .ReturnsAsync(new List<Journal> { GetJournal() });
        }

        public static void Setup_Count_Returns_Ten(this Mock<IJournalRepository> repository)
        {
            repository.Setup(x => x.Count(It.IsAny<Expression<Func<Journal, bool>>>(),
                                          It.IsAny<bool>(),
                                          It.IsAny<List<DistinctInfo<Journal>>>()))
                      .ReturnsAsync(Ten);
        }

        public static void Verify_SelectAfter(this Mock<IJournalRepository> repository)
        {
            repository.Verify(x => x.SelectAfter(It.IsAny<Expression<Func<Journal, bool>>>(),
                                                 It.IsAny<Guid>(),
                                                 It.IsAny<int>(),
                                                 It.IsAny<Expression<Func<Journal, object>>>(),
                                                 It.IsAny<bool>(), false));
        }

        public static void Verify_SelectMany(this Mock<IJournalRepository> repository)
        {
            repository.Verify(x => x.SelectMany(It.IsAny<Expression<Func<Journal, bool>>>(),
                                                It.IsAny<int>(),
                                                It.IsAny<int>(),
                                                It.IsAny<Expression<Func<Journal, object>>>(),
                                                It.IsAny<bool>(), false));
        }

        public static void Verify_Count(this Mock<IJournalRepository> repository)
        {
            repository.Verify(x => x.Count(It.IsAny<Expression<Func<Journal, bool>>>(),
                                           It.IsAny<bool>(),
                                           It.IsAny<List<DistinctInfo<Journal>>>()));
        }
    }
}