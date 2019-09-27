using System;
using System.Collections.Generic;
using System.Linq.Expressions;

using Moq;

using StandardRepository.Models;

using Translation.Data.Entities.Main;
using Translation.Data.Repositories.Contracts;
using static Translation.Common.Tests.TestHelpers.FakeConstantTestHelper;
using static Translation.Common.Tests.TestHelpers.FakeEntityTestHelper;

namespace Translation.Server.Unit.Tests.RepositorySetupHelpers
{
    public static class OrganizationRepositorySetupHelper
    {
        public static void Setup_SelectAll_Returns_Organizations(this Mock<IOrganizationRepository> repository)
        {
            repository.Setup(x => x.SelectAll(It.IsAny<Expression<Func<Organization, bool>>>(),
                                              It.IsAny<bool>(),
                                              It.IsAny<List<OrderByInfo<Organization>>>()))
                      .ReturnsAsync(new List<Organization> { GetOrganization() });
        }

        public static void Verify_SelectAll(this Mock<IOrganizationRepository> repository)
        {
            repository.Verify(x => x.SelectAll(It.IsAny<Expression<Func<Organization, bool>>>(),
                                               It.IsAny<bool>(),
                                               It.IsAny<List<OrderByInfo<Organization>>>()));
        }

        public static void Setup_RestoreRevision_Returns_True(this Mock<IOrganizationRepository> repository)
        {
            repository.Setup(x => x.RestoreRevision(It.IsAny<long>(), It.IsAny<long>(), It.IsAny<int>()))
                      .ReturnsAsync(BooleanTrue);
        }

        public static void Setup_RestoreRevision_Returns_False(this Mock<IOrganizationRepository> repository)
        {
            repository.Setup(x => x.RestoreRevision(It.IsAny<long>(), It.IsAny<long>(), It.IsAny<int>()))
                      .ReturnsAsync(BooleanFalse);
        }

        public static void Verify_RestoreRevision(this Mock<IOrganizationRepository> repository)
        {
            repository.Verify(x => x.RestoreRevision(It.IsAny<long>(), It.IsAny<long>(), It.IsAny<int>()));
        }

        public static void Setup_SelectRevisions_Returns_OrganizationOneRevisionsRevisionOneInIt(this Mock<IOrganizationRepository> repository)
        {
            repository.Setup(x => x.SelectRevisions(It.IsAny<long>()))
                      .ReturnsAsync(GetOrganizationOneRevisionsRevisionOneInIt());
        }

        public static void Setup_SelectRevisions_Returns_OrganizationOneRevisionsRevisionTwoInIt(this Mock<IOrganizationRepository> repository)
        {
            repository.Setup(x => x.SelectRevisions(It.IsAny<long>()))
                      .ReturnsAsync(GetOrganizationOneRevisionsRevisionTwoInIt());
        }

        public static void Verify_SelectRevisions(this Mock<IOrganizationRepository> repository)
        {
            repository.Verify(x => x.SelectRevisions(It.IsAny<long>()));
        }

        public static void Setup_SelectAfter_Returns_Organizations(this Mock<IOrganizationRepository> repository)
        {
            repository.Setup(x => x.SelectAfter(It.IsAny<Expression<Func<Organization, bool>>>(),
                                                It.IsAny<Guid>(),
                                                It.IsAny<int>(),
                                                It.IsAny<bool>(),
                                                It.IsAny<List<OrderByInfo<Organization>>>()))
                      .ReturnsAsync(new List<Organization> { GetOrganization() });
        }

        public static void Setup_SelectMany_Returns_Organizations(this Mock<IOrganizationRepository> repository)
        {
            repository.Setup(x => x.SelectMany(It.IsAny<Expression<Func<Organization, bool>>>(),
                                               It.IsAny<int>(),
                                               It.IsAny<int>(),
                                               It.IsAny<bool>(),
                                               It.IsAny<List<OrderByInfo<Organization>>>()))
                      .ReturnsAsync(new List<Organization> { GetOrganization() });
        }

        public static void Setup_Count_Returns_Ten(this Mock<IOrganizationRepository> repository)
        {
            repository.Setup(x => x.Count(It.IsAny<Expression<Func<Organization, bool>>>(),
                                          It.IsAny<bool>(),
                                          It.IsAny<List<DistinctInfo<Organization>>>()))
                      .ReturnsAsync(Ten);
        }

        public static void Setup_SelectById_Returns_OrganizationOne(this Mock<IOrganizationRepository> repository)
        {
            repository.Setup(x => x.SelectById(It.IsAny<long>()))
                      .ReturnsAsync(GetOrganizationOne());
        }

        public static void Setup_SelectById_Returns_OrganizationTwo(this Mock<IOrganizationRepository> repository)
        {
            repository.Setup(x => x.SelectById(It.IsAny<long>()))
                      .ReturnsAsync(GetOrganizationTwo());
        }

        public static void Setup_Select_Returns_OrganizationOne(this Mock<IOrganizationRepository> repository)
        {
            repository.Setup(x => x.Select(It.IsAny<Expression<Func<Organization, bool>>>(), false))
                      .ReturnsAsync(GetOrganizationOne());
        }

        public static void Setup_Select_Returns_OrganizationTwo(this Mock<IOrganizationRepository> repository)
        {
            repository.Setup(x => x.Select(It.IsAny<Expression<Func<Organization, bool>>>(), false))
                      .ReturnsAsync(GetOrganizationTwo());
        }

        public static void Setup_Select_Returns_OrganizationOneNotExist(this Mock<IOrganizationRepository> repository)
        {
            repository.Setup(x => x.Select(It.IsAny<Expression<Func<Organization, bool>>>(), false))
                      .ReturnsAsync(GetOrganizationOneNotExist());
        }

        public static void Setup_Any_Returns_False(this Mock<IOrganizationRepository> repository)
        {
            repository.Setup(x => x.Any(It.IsAny<Expression<Func<Organization, bool>>>(),
                                        It.IsAny<bool>()))
                      .ReturnsAsync(BooleanFalse);
        }

        public static void Setup_Any_Returns_True(this Mock<IOrganizationRepository> repository)
        {
            repository.Setup(x => x.Any(It.IsAny<Expression<Func<Organization, bool>>>(),
                                        It.IsAny<bool>()))
                      .ReturnsAsync(BooleanTrue);
        }

        public static void Setup_IsOrganizationActive_Returns_True(this Mock<IOrganizationRepository> repository)
        {
            repository.Setup(x => x.IsOrganizationActive(It.IsAny<long>()))
                      .ReturnsAsync(true);
        }

        public static void Setup_IsOrganizationActive_Returns_False(this Mock<IOrganizationRepository> repository)
        {
            repository.Setup(x => x.IsOrganizationActive(It.IsAny<long>()))
                      .ReturnsAsync(false);
        }

        public static void Verify_IsOrganizationActive(this Mock<IOrganizationRepository> repository)
        {
            repository.Verify(x => x.IsOrganizationActive(It.IsAny<long>()));
        }

        public static void Verify_Any(this Mock<IOrganizationRepository> repository)
        {
            repository.Verify(x => x.Any(It.IsAny<Expression<Func<Organization, bool>>>(),
                                         It.IsAny<bool>()));
        }

        public static void Verify_Count(this Mock<IOrganizationRepository> repository)
        {
            repository.Verify(x => x.Count(It.IsAny<Expression<Func<Organization, bool>>>(),
                                           It.IsAny<bool>(),
                                           It.IsAny<List<DistinctInfo<Organization>>>()));
        }

        public static void Verify_SelectMany(this Mock<IOrganizationRepository> repository)
        {
            repository.Verify(x => x.SelectMany(It.IsAny<Expression<Func<Organization, bool>>>(),
                                                It.IsAny<int>(),
                                                It.IsAny<int>(),
                                                It.IsAny<bool>(),
                                                It.IsAny<List<OrderByInfo<Organization>>>()));
        }

        public static void Verify_SelectAfter(this Mock<IOrganizationRepository> repository)
        {
            repository.Verify(x => x.SelectAfter(It.IsAny<Expression<Func<Organization, bool>>>(),
                                                 It.IsAny<Guid>(),
                                                 It.IsAny<int>(),
                                                 It.IsAny<bool>(),
                                                 It.IsAny<List<OrderByInfo<Organization>>>()));
        }

        public static void Setup_Update_Success(this Mock<IOrganizationRepository> repository)
        {
            repository.Setup(x => x.Update(It.IsAny<long>(),
                                           It.IsAny<Organization>()))
                      .ReturnsAsync(true);
        }

        public static void Setup_Update_Failed(this Mock<IOrganizationRepository> repository)
        {
            repository.Setup(x => x.Update(It.IsAny<long>(),
                                           It.IsAny<Organization>()))
                      .ReturnsAsync(false);
        }

        public static void Verify_Update(this Mock<IOrganizationRepository> repository)
        {
            repository.Verify(x => x.Update(It.IsAny<long>(), It.IsAny<Organization>()));
        }

        public static void Setup_SelectById_Returns_ParkNetOrganization(this Mock<IOrganizationRepository> repository)
        {
            repository.Setup(x => x.SelectById(It.IsAny<long>()))
                      .ReturnsAsync(GetOrganizationOne());
        }

        public static void Verify_SelectById(this Mock<IOrganizationRepository> repository)
        {
            repository.Verify(x => x.SelectById(It.IsAny<long>()));
        }

        public static void Setup_Select_Returns_NotExistOrganization(this Mock<IOrganizationRepository> repository)
        {
            repository.Setup(x => x.Select(It.IsAny<Expression<Func<Organization, bool>>>(), It.IsAny<bool>()))
                      .ReturnsAsync(new Organization());
        }

        public static void Verify_Select(this Mock<IOrganizationRepository> repository)
        {
            repository.Verify(x => x.Select(It.IsAny<Expression<Func<Organization, bool>>>(), It.IsAny<bool>()));
        }
    }
}