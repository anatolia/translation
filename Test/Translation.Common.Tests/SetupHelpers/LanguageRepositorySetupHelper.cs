using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Moq;
using StandardRepository.Models;
using StandardRepository.Models.Entities;
using Translation.Data.Entities.Domain;
using Translation.Data.Entities.Parameter;
using Translation.Data.Repositories.Contracts;
using static Translation.Common.Tests.TestHelpers.FakeConstantTestHelper;
using static Translation.Common.Tests.TestHelpers.FakeEntityTestHelper;

namespace Translation.Common.Tests.SetupHelpers
{
    public static class LanguageRepositorySetupHelper
    {
        public static void Setup_SelectRevisions_Returns_LanguageRevisionsRevisionOneInIt(this Mock<ILanguageRepository> repository)
        {
            repository.Setup(x => x.SelectRevisions(It.IsAny<long>()))
                      .ReturnsAsync(new List<EntityRevision<Language>>() { GetLanguageRevisionOne() });
        }

        public static void Setup_SelectRevisions_Returns_LanguageRevisionsRevisionTwoInIt(this Mock<ILanguageRepository> repository)
        {
            repository.Setup(x => x.SelectRevisions(It.IsAny<long>()))
                      .ReturnsAsync(new List<EntityRevision<Language>>() { GetLanguageRevisionTwo() });
        }

        public static void Setup_RestoreRevision_Returns_True(this Mock<ILanguageRepository> repository)
        {
            repository.Setup(x => x.RestoreRevision(It.IsAny<long>(), It.IsAny<long>(), It.IsAny<int>()))
                      .ReturnsAsync(BooleanTrue);
        }

        public static void Setup_RestoreRevision_Returns_False(this Mock<ILanguageRepository> repository)
        {
            repository.Setup(x => x.RestoreRevision(It.IsAny<long>(), It.IsAny<long>(), It.IsAny<int>()))
                      .ReturnsAsync(BooleanFalse);
        }

        public static void Verify_RestoreRevision(this Mock<ILanguageRepository> repository)
        {
            repository.Verify(x => x.RestoreRevision(It.IsAny<long>(), It.IsAny<long>(), It.IsAny<int>()));
        }

        public static void Setup_Update_Returns_True(this Mock<ILanguageRepository> repository)
        {
            repository.Setup(x => x.Update(It.IsAny<long>(),
                                           It.IsAny<Language>()))
                      .ReturnsAsync(true);
        }

        public static void Setup_Update_Returns_False(this Mock<ILanguageRepository> repository)
        {
            repository.Setup(x => x.Update(It.IsAny<long>(),
                                           It.IsAny<Language>()))
                      .ReturnsAsync(false);

        }

        public static void Setup_Any_Returns_False(this Mock<ILanguageRepository> repository)
        {
            repository.Setup(x => x.Any(It.IsAny<Expression<Func<Language, bool>>>(), false))
                      .ReturnsAsync(false);
        }

        public static void Setup_Any_Returns_True(this Mock<ILanguageRepository> repository)
        {
            repository.Setup(x => x.Any(It.IsAny<Expression<Func<Language, bool>>>(), false))
                      .ReturnsAsync(true);
        }

        public static void Verify_SelectRevisions(this Mock<ILanguageRepository> repository)
        {
            repository.Verify(x => x.SelectRevisions(It.IsAny<long>()));
        }

        public static void Setup_SelectAll_Returns_Languages(this Mock<ILanguageRepository> repository)
        {
            repository.Setup(x => x.SelectAll(It.IsAny<Expression<Func<Language, bool>>>(),
                                              It.IsAny<bool>(),
                                              It.IsAny<List<OrderByInfo<Language>>>()))
                      .ReturnsAsync(new List<Language> { GetLanguageOne() });
        }

        public static void Verify_SelectAll(this Mock<ILanguageRepository> repository)
        {
            repository.Verify(x => x.SelectAll(It.IsAny<Expression<Func<Language, bool>>>(),
                                               It.IsAny<bool>(),
                                               It.IsAny<List<OrderByInfo<Language>>>()));
        }

        public static void Setup_SelectAfter_Returns_Languages(this Mock<ILanguageRepository> repository)
        {
            repository.Setup(x => x.SelectAfter(It.IsAny<Expression<Func<Language, bool>>>(),
                                                It.IsAny<Guid>(),
                                                It.IsAny<int>(),
                                                It.IsAny<Expression<Func<Language, object>>>(),
                                                It.IsAny<bool>(), false))
                      .ReturnsAsync(new List<Language> { GetLanguageOne() });
        }

        public static void Setup_SelectMany_Returns_Languages(this Mock<ILanguageRepository> repository)
        {
            repository.Setup(x => x.SelectMany(It.IsAny<Expression<Func<Language, bool>>>(),
                                               It.IsAny<int>(),
                                               It.IsAny<int>(),
                                               It.IsAny<Expression<Func<Language, object>>>(),
                                               It.IsAny<bool>(), false))
                      .ReturnsAsync(new List<Language> { GetLanguageOne() });
        }

        public static void Setup_Count_Returns_Ten(this Mock<ILanguageRepository> repository)
        {
            repository.Setup(x => x.Count(It.IsAny<Expression<Func<Language, bool>>>(),
                                          It.IsAny<bool>(),
                                          It.IsAny<List<DistinctInfo<Language>>>()))
                      .ReturnsAsync(Ten);
        }

        public static void Setup_Count(this Mock<ILanguageRepository> repository)
        {
            repository.Verify(x => x.Count(It.IsAny<Expression<Func<Language, bool>>>(),
                                           It.IsAny<bool>(),
                                           It.IsAny<List<DistinctInfo<Language>>>()));

        }

        public static void Setup_Language_AlreadyExist(this Mock<ILanguageRepository> repository)
        {
            repository.Setup(x => x.Any(It.IsAny<Expression<Func<Language, bool>>>(), false))
                      .ReturnsAsync(true);
        }

        public static void Setup_Language_NotExist(this Mock<ILanguageRepository> repository)
        {
            repository.Setup(x => x.Any(It.IsAny<Expression<Func<Language, bool>>>(), false))
                      .ReturnsAsync(false);
        }

        public static void Verify_Any(this Mock<ILanguageRepository> repository)
        {
            repository.Verify(x => x.Any(It.IsAny<Expression<Func<Language, bool>>>(), false));
        }

        public static void Setup_Select_Returns_Language_DifferentIsoCode2(this Mock<ILanguageRepository> repository)
        {
            repository.Setup(x => x.Select(It.IsAny<Expression<Func<Language, bool>>>(), false))
                      .ReturnsAsync(GetLanguageTwo());
        }

        public static void Setup_Select_Returns_Language(this Mock<ILanguageRepository> repository)
        {
            repository.Setup(x => x.Select(It.IsAny<Expression<Func<Language, bool>>>(), false))
                      .ReturnsAsync(GetLanguageTwo());
        }

        public static void Setup_Select_Returns_Language_SameIsoCode2(this Mock<ILanguageRepository> repository)
        {
            repository.Setup(x => x.Select(It.IsAny<Expression<Func<Language, bool>>>(), false))
                      .ReturnsAsync(GetLanguageOne());
        }

        public static void Setup_Select_Returns_LanguageNotExist(this Mock<ILanguageRepository> repository)
        {
            repository.Setup(x => x.Select(It.IsAny<Expression<Func<Language, bool>>>(), false))
                      .ReturnsAsync(GetLanguageOneNotExist());
        }

        public static void Verify_Select(this Mock<ILanguageRepository> repository)
        {
            repository.Verify(x => x.Select(It.IsAny<Expression<Func<Language, bool>>>(), false));
        }

        public static void Setup_SelectById_Returns_Language(this Mock<ILanguageRepository> repository)
        {
            repository.Setup(x => x.SelectById(It.IsAny<long>()))
                      .ReturnsAsync(GetLanguageOne());
        }

        public static void Setup_SelectById_Returns_LanguageNotExist(this Mock<ILanguageRepository> repository)
        {
            repository.Setup(x => x.SelectById(It.IsAny<long>()))
                      .ReturnsAsync(GetLanguageOneNotExist());
        }

        public static void Verify_SelectById(this Mock<ILanguageRepository> repository)
        {
            repository.Verify(x => x.SelectById(It.IsAny<long>()));
        }

        public static void Setup_Insert_Success(this Mock<ILanguageRepository> repository)
        {
            repository.Setup(x => x.Insert(It.IsAny<long>(),
                                           It.IsAny<Language>()))
                      .ReturnsAsync(1);
        }

        public static void Setup_Insert_Failed(this Mock<ILanguageRepository> repository)
        {
            repository.Setup(x => x.Insert(It.IsAny<long>(),
                                           It.IsAny<Language>()))
                      .ReturnsAsync(0);
        }

        public static void Verify_Insert(this Mock<ILanguageRepository> repository)
        {
            repository.Verify(x => x.Insert(It.IsAny<long>(),
                                            It.IsAny<Language>()));
        }

        public static void Setup_Update_Success(this Mock<ILanguageRepository> repository)
        {
            repository.Setup(x => x.Update(It.IsAny<long>(),
                                           It.IsAny<Language>()))
                      .ReturnsAsync(true);
        }

        public static void Setup_Update_Failed(this Mock<ILanguageRepository> repository)
        {
            repository.Setup(x => x.Update(It.IsAny<long>(),
                                           It.IsAny<Language>()))
                      .ReturnsAsync(false);
        }

        public static void Verify_Update(this Mock<ILanguageRepository> repository)
        {
            repository.Verify(x => x.Update(It.IsAny<long>(),
                                            It.IsAny<Language>()));
        }

        public static void Setup_Delete_Success(this Mock<ILanguageRepository> repository)
        {
            repository.Setup(x => x.Delete(It.IsAny<long>(),
                                           It.IsAny<long>()))
                      .ReturnsAsync(true);
        }

        public static void Setup_Delete_Failed(this Mock<ILanguageRepository> repository)
        {
            repository.Setup(x => x.Delete(It.IsAny<long>(),
                                           It.IsAny<long>()))
                      .ReturnsAsync(false);
        }

        public static void Verify_Delete(this Mock<ILanguageRepository> repository)
        {
            repository.Verify(x => x.Delete(It.IsAny<long>(),
                                            It.IsAny<long>()));
        }

        public static void Verify_SelectAfter(this Mock<ILanguageRepository> repository)
        {
            repository.Verify(x => x.SelectAfter(It.IsAny<Expression<Func<Language, bool>>>(),
                                                 It.IsAny<Guid>(),
                                                 It.IsAny<int>(),
                                                 It.IsAny<Expression<Func<Language, object>>>(),
                                                 It.IsAny<bool>(), false));
        }

        public static void Verify_SelectMany(this Mock<ILanguageRepository> repository)
        {
            repository.Verify(x => x.SelectMany(It.IsAny<Expression<Func<Language, bool>>>(),
                                                It.IsAny<int>(),
                                                It.IsAny<int>(),
                                                It.IsAny<Expression<Func<Language, object>>>(),
                                                It.IsAny<bool>(), false));
        }

        public static void Verify_Count(this Mock<ILanguageRepository> repository)
        {
            repository.Verify(x => x.Count(It.IsAny<Expression<Func<Language, bool>>>(),
                                           It.IsAny<bool>(),
                                           It.IsAny<List<DistinctInfo<Language>>>()));
        }
    }
}