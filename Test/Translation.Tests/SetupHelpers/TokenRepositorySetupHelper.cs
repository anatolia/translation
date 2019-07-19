using System;
using System.Collections.Generic;
using System.Linq.Expressions;

using Moq;

using Translation.Data.Entities.Main;
using Translation.Data.Repositories.Contracts;
using static Translation.Tests.TestHelpers.FakeEntityTestHelper;
using static Translation.Tests.TestHelpers.FakeConstantTestHelper;

namespace Translation.Tests.SetupHelpers
{
    public static class TokenRepositorySetupHelper
    {
        public static void Setup_SelectMany_Returns_Tokens(this Mock<ITokenRepository> repository)
        {
            repository.Setup(x => x.SelectMany(It.IsAny<Expression<Func<Token, bool>>>(),
                    It.IsAny<int>(),
                    It.IsAny<int>(),
                    It.IsAny<Expression<Func<Token, object>>>(),
                    It.IsAny<bool>(), false))
                .ReturnsAsync(new List<Token> {GetToken() });
        }

        public static void Setup_Select_Returns_OrganizationTwoIntegrationOneIntegrationClientOneTokenOne(this Mock<ITokenRepository> repository)
        {
            repository.Setup(x => x.Select(It.IsAny<Expression<Func<Token, bool>>>(), false))
                .ReturnsAsync(GetOrganizationTwoIntegrationOneIntegrationClientOneTokenOne());
        }

        public static void Setup_Select_Returns_OrganizationOneIntegrationOneIntegrationClientOneTokenOne(this Mock<ITokenRepository> repository)
        {
            repository.Setup(x => x.Select(It.IsAny<Expression<Func<Token, bool>>>(), false))
                .ReturnsAsync(GetOrganizationOneIntegrationOneIntegrationClientOneTokenOne());
        }

        public static void Setup_Select_Returns_OrganizationOneIntegrationOneIntegrationClientOneTokenOneNotExist(this Mock<ITokenRepository> repository)
        {
            repository.Setup(x => x.Select(It.IsAny<Expression<Func<Token, bool>>>(), false))
                .ReturnsAsync(GetOrganizationOneIntegrationOneIntegrationClientOneTokenOneNotExist());
        }

        public static void Verify_Select(this Mock<ITokenRepository> repository)
        {
            repository.Verify(x => x.Select(It.IsAny<Expression<Func<Token, bool>>>(), false));
        }
        public static void Setup_Any_Returns_False(this Mock<ITokenRepository> repository)
        {
            repository.Setup(x => x.Any(It.IsAny<Expression<Func<Token, bool>>>(), false))
                .ReturnsAsync(false);
        }

        public static void Setup_Any_Returns_True(this Mock<ITokenRepository> repository)
        {
            repository.Setup(x => x.Any(It.IsAny<Expression<Func<Token, bool>>>(), false))
                .ReturnsAsync(true);
        }

        public static void Verify_Any(this Mock<ITokenRepository> repository)
        {
            repository.Verify(x => x.Any(It.IsAny<Expression<Func<Token, bool>>>(), false));
        }

        public static void Verify_SelectMany(this Mock<ITokenRepository> repository)
        {
            repository.Verify(x => x.SelectMany(It.IsAny<Expression<Func<Token, bool>>>(),
                    It.IsAny<int>(), It.IsAny<int>(),
                    It.IsAny<Expression<Func<Token, object>>>(),
                    It.IsAny<bool>(), false));
        }

        public static void Setup_Delete_Returns_True(this Mock<ITokenRepository> repository)
        {
            repository.Setup(x => x.Delete(It.IsAny<long>(),
                                           It.IsAny<long>()))
                      .ReturnsAsync(true);
        }

        public static void Setup_Delete_Returns_False(this Mock<ITokenRepository> repository)
        {
            repository.Setup(x => x.Delete(It.IsAny<long>(),
                                           It.IsAny<long>()))
                      .ReturnsAsync(false);
        }

        public static void Verify_Delete(this Mock<ITokenRepository> repository)
        {
            repository.Verify(x => x.Delete(It.IsAny<long>(),
                    It.IsAny<long>()));
        }

        public static void Setup_Insert_Failed(this Mock<ITokenRepository> repository)
        {
            repository.Setup(x => x.Insert(It.IsAny<long>(),
                                           It.IsAny<Token>()))
                      .ReturnsAsync(0);
        }

        public static void Setup_Insert_Success(this Mock<ITokenRepository> repository)
        {
            repository.Setup(x => x.Insert(It.IsAny<long>(),
                                           It.IsAny<Token>()))
                      .ReturnsAsync(1);
        }

        public static void Verify_Insert(this Mock<ITokenRepository> repository)
        {
            repository.Verify(x => x.Insert(It.IsAny<long>(),
                    It.IsAny<Token>()));
        }
    }
}