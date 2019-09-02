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
    public static class IntegrationClientRepositorySetupHelper
    {
        public static void Setup_Count_Returns_Ten(this Mock<IIntegrationClientRepository> repository)
        {
            repository.Setup(x => x.Count(It.IsAny<Expression<Func<IntegrationClient, bool>>>(),
                                          It.IsAny<bool>(),
                                          It.IsAny<List<DistinctInfo<IntegrationClient>>>()))
                      .ReturnsAsync(Ten);
        }

        public static void Verify_Count(this Mock<IIntegrationClientRepository> repository)
        {
            repository.Verify(x => x.Count(It.IsAny<Expression<Func<IntegrationClient, bool>>>(),
                                           It.IsAny<bool>(),
                                           It.IsAny<List<DistinctInfo<IntegrationClient>>>()));
        }

        public static void Setup_SelectMany_Returns_IntegrationClients(this Mock<IIntegrationClientRepository> repository)
        {
            repository.Setup(x => x.SelectMany(It.IsAny<Expression<Func<IntegrationClient, bool>>>(),
                                               It.IsAny<int>(),
                                               It.IsAny<int>(),
                                               It.IsAny<bool>(),
                                               It.IsAny<List<OrderByInfo<IntegrationClient>>>()))
                      .ReturnsAsync(new List<IntegrationClient> { GetIntegrationClient() });
        }

        public static void Verify_SelectMany(this Mock<IIntegrationClientRepository> repository)
        {
            repository.Verify(x => x.SelectMany(It.IsAny<Expression<Func<IntegrationClient, bool>>>(),
                                                It.IsAny<int>(),
                                                It.IsAny<int>(),
                                                It.IsAny<bool>(),
                                                It.IsAny<List<OrderByInfo<IntegrationClient>>>()));
        }

        public static void Setup_SelectAfter_Returns_IntegrationClients(this Mock<IIntegrationClientRepository> repository)
        {
            repository.Setup(x => x.SelectAfter(It.IsAny<Expression<Func<IntegrationClient, bool>>>(),
                                                It.IsAny<Guid>(),
                                                It.IsAny<int>(),
                                                It.IsAny<bool>(),
                                                It.IsAny<List<OrderByInfo<IntegrationClient>>>()))
                      .ReturnsAsync(new List<IntegrationClient> { GetIntegrationClient() });
        }

        public static void Verify_SelectAfter(this Mock<IIntegrationClientRepository> repository)
        {
            repository.Verify(x => x.SelectAfter(It.IsAny<Expression<Func<IntegrationClient, bool>>>(),
                                                 It.IsAny<Guid>(),
                                                 It.IsAny<int>(),
                                                 It.IsAny<bool>(),
                                                 It.IsAny<List<OrderByInfo<IntegrationClient>>>()));
        }

        public static void Setup_Any_Returns_False(this Mock<IIntegrationClientRepository> repository)
        {
            repository.Setup(x => x.Any(It.IsAny<Expression<Func<IntegrationClient, bool>>>(),
                                        It.IsAny<bool>()))
                      .ReturnsAsync(false);
        }

        public static void Setup_Any_Returns_True(this Mock<IIntegrationClientRepository> repository)
        {
            repository.Setup(x => x.Any(It.IsAny<Expression<Func<IntegrationClient, bool>>>(),
                                        It.IsAny<bool>()))
                      .ReturnsAsync(true);
        }

        public static void Verify_Any(this Mock<IIntegrationClientRepository> repository)
        {
            repository.Verify(x => x.Any(It.IsAny<Expression<Func<IntegrationClient, bool>>>(),
                                         It.IsAny<bool>()));
        }

        public static void Setup_Select_Returns_OrganizationOneIntegrationOneIntegrationClientOneNotExist(this Mock<IIntegrationClientRepository> repository)
        {
            repository.Setup(x => x.Select(It.IsAny<Expression<Func<IntegrationClient, bool>>>(), false))
                      .ReturnsAsync(GetOrganizationOneIntegrationOneIntegrationClientOneNotExist);
        }

        public static void Setup_Select_Returns_InvalidIntegration(this Mock<IIntegrationClientRepository> repository)
        {
            repository.Setup(x => x.Select(It.IsAny<Expression<Func<IntegrationClient, bool>>>(), false))
                      .ReturnsAsync(new IntegrationClient());
        }

        public static void Setup_SelectById_Returns_OrganizationOneIntegrationOneIntegrationClientOne(this Mock<IIntegrationClientRepository> repository)
        {
            repository.Setup(x => x.SelectById(It.IsAny<long>()))
                      .ReturnsAsync(GetOrganizationOneIntegrationOneIntegrationClientOne());
        }

        public static void Verify_SelectById(this Mock<IIntegrationClientRepository> repository)
        {
            repository.Verify(x => x.SelectById(It.IsAny<long>()));
        }

        public static void Setup_Select_Returns_OrganizationOneIntegrationOneIntegrationClientOne(this Mock<IIntegrationClientRepository> repository)
        {
            repository.Setup(x => x.Select(It.IsAny<Expression<Func<IntegrationClient, bool>>>(), false))
                      .ReturnsAsync(GetOrganizationOneIntegrationOneIntegrationClientOne());
        }

        public static void Setup_Select_Returns_OrganizationTwoIntegrationOneIntegrationClientOne(this Mock<IIntegrationClientRepository> repository)
        {
            repository.Setup(x => x.Select(It.IsAny<Expression<Func<IntegrationClient, bool>>>(), false))
                      .ReturnsAsync(GetOrganizationTwoIntegrationOneIntegrationClientOne());
        }

        public static void Verify_Select(this Mock<IIntegrationClientRepository> repository)
        {
            repository.Verify(x => x.Select(It.IsAny<Expression<Func<IntegrationClient, bool>>>(), false));
        }

        public static void Setup_Update_Returns_True(this Mock<IIntegrationClientRepository> repository)
        {
            repository.Setup(x => x.Update(It.IsAny<long>(),
                                           It.IsAny<IntegrationClient>()))
                      .ReturnsAsync(true);
        }

        public static void Setup_Update_Returns_False(this Mock<IIntegrationClientRepository> repository)
        {
            repository.Setup(x => x.Update(It.IsAny<long>(),
                                           It.IsAny<IntegrationClient>()))
                      .ReturnsAsync(false);
        }

        public static void Verify_Update(this Mock<IIntegrationClientRepository> repository)
        {
            repository.Verify(x => x.Update(It.IsAny<long>(),
                    It.IsAny<IntegrationClient>()));
        }

        public static void Setup_Delete_Returns_True(this Mock<IIntegrationClientRepository> repository)
        {
            repository.Setup(x => x.Delete(It.IsAny<long>(),
                                           It.IsAny<long>()))
                      .ReturnsAsync(true);
        }

        public static void Setup_Delete_Returns_False(this Mock<IIntegrationClientRepository> repository)
        {
            repository.Setup(x => x.Delete(It.IsAny<long>(),
                                           It.IsAny<long>()))
                      .ReturnsAsync(false);
        }

        public static void Verify_Delete(this Mock<IIntegrationClientRepository> repository)
        {
            repository.Verify(x => x.Delete(It.IsAny<long>(),
                    It.IsAny<long>()));
        }
    }
}