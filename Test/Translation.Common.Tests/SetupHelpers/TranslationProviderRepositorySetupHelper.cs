using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Castle.Components.DictionaryAdapter;
using Moq;
using StandardRepository.Models;
using Translation.Common.Contracts;
using Translation.Common.Enumerations;
using Translation.Common.Models.DataTransferObjects;
using Translation.Common.Models.Requests.TranslationProvider;
using Translation.Common.Models.Responses.TranslationProvider;
using Translation.Data.Entities.Domain;
using Translation.Data.Entities.Main;
using Translation.Data.Repositories.Contracts;
using static Translation.Common.Tests.TestHelpers.FakeEntityTestHelper;
using static Translation.Common.Tests.TestHelpers.FakeConstantTestHelper;

namespace Translation.Common.Tests.SetupHelpers
{
    public static class TranslationProviderRepositorySetupHelper
    {

        public static void Setup_Select_Returns_TranslationProvider(this Mock<ITranslationProviderRepository> repository)
        {
            repository.Setup(x => x.Select(It.IsAny<Expression<Func<TranslationProvider, bool>>>(), false))
                .ReturnsAsync(GetTranslationProvider());
        }

        public static void Setup_Select_Returns_TranslationProviderNotExist(this Mock<ITranslationProviderRepository> repository)
        {
            repository.Setup(x => x.Select(It.IsAny<Expression<Func<TranslationProvider, bool>>>(), false))
                .ReturnsAsync(GetTranslationProviderNotExist());
        }

        public static void Verify_Select(this Mock<ITranslationProviderRepository> repository)
        {
            repository.Verify(x => x.Select(It.IsAny<Expression<Func<TranslationProvider, bool>>>(), false));

        }

        public static void Setup_SelectAfter_Returns_TranslationProviders(this Mock<ITranslationProviderRepository> repository)
        {
            repository.Setup(x => x.SelectAfter(It.IsAny<Expression<Func<TranslationProvider, bool>>>(),
                                                It.IsAny<Guid>(),
                                                It.IsAny<int>(),
                                                It.IsAny<bool>(),
                                                It.IsAny<List<OrderByInfo<TranslationProvider>>>()))
                     .ReturnsAsync(new List<TranslationProvider> { GetTranslationProvider() });
        }

        public static void Setup_Count_Returns_Ten(this Mock<ITranslationProviderRepository> repository)
        {
            repository.Setup(x => x.Count(It.IsAny<Expression<Func<TranslationProvider, bool>>>(),
                                          It.IsAny<bool>(),
                                          It.IsAny<List<DistinctInfo<TranslationProvider>>>()))
                     .ReturnsAsync(Ten);
        }

        public static void Setup_SelectMany_Returns_TranslationProviders(this Mock<ITranslationProviderRepository> repository)
        {
            repository.Setup(x => x.SelectMany(It.IsAny<Expression<Func<TranslationProvider, bool>>>(),
                                               It.IsAny<int>(),
                                               It.IsAny<int>(),
                                               It.IsAny<bool>(),
                                               It.IsAny<List<OrderByInfo<TranslationProvider>>>()))
                      .ReturnsAsync(new List<TranslationProvider> { GetTranslationProvider() });
        }

        public static void Verify_SelectMany(this Mock<ITranslationProviderRepository> repository)
        {
            repository.Verify(x => x.SelectMany(It.IsAny<Expression<Func<TranslationProvider, bool>>>(),
                                                It.IsAny<int>(),
                                                It.IsAny<int>(),
                                                It.IsAny<bool>(),
                                                It.IsAny<List<OrderByInfo<TranslationProvider>>>()));
        }

        public static void Verify_SelectAfter(this Mock<ITranslationProviderRepository> repository)
        {
            repository.Verify(x => x.SelectAfter(It.IsAny<Expression<Func<TranslationProvider, bool>>>(),
                                                 It.IsAny<Guid>(),
                                                 It.IsAny<int>(),
                                                 It.IsAny<bool>(),
                                                 It.IsAny<List<OrderByInfo<TranslationProvider>>>()));
        }

        public static void Verify_Count(this Mock<ITranslationProviderRepository> repository)
        {
            repository.Verify(x => x.Count(It.IsAny<Expression<Func<TranslationProvider, bool>>>(),
                                           It.IsAny<bool>(),
                                           It.IsAny<List<DistinctInfo<TranslationProvider>>>()));
            
        }

    }
}