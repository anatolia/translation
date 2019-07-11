using System;
using System.Linq.Expressions;

using Moq;

using Translation.Data.Entities.Main;
using Translation.Data.Entities.Parameter;
using Translation.Data.Repositories.Contracts;
using static Translation.Tests.TestHelpers.FakeEntityTestHelper;
using static Translation.Tests.TestHelpers.FakeConstantTestHelper;

namespace Translation.Tests.SetupHelpers
{
    public static class LanguageRepositorySetupHelper
    {
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

        public static void Setup_Select_Returns_Language(this Mock<ILanguageRepository> repository)
        {
            repository.Setup(x => x.Select(It.IsAny<Expression<Func<Language, bool>>>(), false))
                      .ReturnsAsync(GetLanguageOne());
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

        //public static void Setup_SelectMany_Returns_LanguageList(this Mock<ILanguageRepository> repository)
        //{
        //    repository.Setup(x => x.SelectMany(It.IsAny<Expression<Func<Language, bool>>>(),
        //                                       It.IsAny<int>(),
        //                                       It.IsAny<int>(),
        //                                       It.IsAny<Expression<Func<Language, object>>>(),
        //                                       It.IsAny<bool>(), false))
        //              .ReturnsAsync(TranslationEntityDataHelper.GetFakeLanguageList());
        //}

        //public static void Setup_SelectAfter_Returns_LanguageList(this Mock<ILanguageRepository> repository)
        //{
        //    repository.Setup(x => x.SelectAfter(It.IsAny<Expression<Func<Language, bool>>>(),
        //                                        It.IsAny<Guid>(),
        //                                        It.IsAny<int>(),
        //                                        It.IsAny<Expression<Func<Language, object>>>(),
        //                                        It.IsAny<bool>(), false))
        //              .ReturnsAsync(TranslationEntityDataHelper.GetFakeLanguageList());
        //}

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

        public static void Setup_Count_Returns_POSITIVE_INT_NUMBER_10(this Mock<ILanguageRepository> repository)
        {
            repository.Setup(x => x.Count(It.IsAny<Expression<Func<Language, bool>>>(), false))
                .ReturnsAsync(Ten);
        }

        public static void Verify_Count(this Mock<ILanguageRepository> repository)
        {
            repository.Verify(x => x.Count(It.IsAny<Expression<Func<Language, bool>>>(), false));
        }
    }
}