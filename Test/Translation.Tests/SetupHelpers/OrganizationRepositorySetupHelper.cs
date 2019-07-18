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
    public static class OrganizationRepositorySetupHelper
    {
        public static void Setup_SelectAfter_Returns_Organizations(this Mock<IOrganizationRepository> repository)
        {
            repository.Setup(x => x.SelectAfter(It.IsAny<Expression<Func<Organization, bool>>>(), 
                                                It.IsAny<Guid>(), 
                                                It.IsAny<int>(),
                                                It.IsAny<Expression<Func<Organization, object>>>(),
                                                It.IsAny<bool>(), false))
                      .ReturnsAsync(new List<Organization>{GetOrganization()});
        }

        public static void Setup_SelectMany_Returns_Organizations(this Mock<IOrganizationRepository> repository)
        {
            repository.Setup(x => x.SelectMany(It.IsAny<Expression<Func<Organization, bool>>>(),
                                               It.IsAny<int>(),
                                               It.IsAny<int>(),
                                               It.IsAny<Expression<Func<Organization, object>>>(),
                                               It.IsAny<bool>(), false))
                      .ReturnsAsync(new List<Organization> { GetOrganization() });
        }

        public static void Setup_SelectById_Returns_OrganizationOne(this Mock<IOrganizationRepository> repository)
        {
            repository.Setup(x => x.SelectById(It.IsAny<long>()))
                      .ReturnsAsync(GetOrganizationOne());
        }

        public static void Setup_Select_Returns_OrganizationOne(this Mock<IOrganizationRepository> repository)
        {
            repository.Setup(x => x.Select(It.IsAny<Expression<Func<Organization, bool>>>(), false))
                      .ReturnsAsync(GetOrganizationOne());
        }

        public static void Setup_Any_Returns_False(this Mock<IOrganizationRepository> repository)
        {
            repository.Setup(x => x.Any(It.IsAny<Expression<Func<Organization, bool>>>(), It.IsAny<bool>()))
                      .ReturnsAsync(false);
        }

        public static void Setup_Any_Returns_True(this Mock<IOrganizationRepository> repository)
        {
            repository.Setup(x => x.Any(It.IsAny<Expression<Func<Organization, bool>>>(),             
                                        It.IsAny<bool>()))
                      .ReturnsAsync(true);
        }

        public static void Verify_Any(this Mock<IOrganizationRepository> repository)
        {
            repository.Verify(x => x.Any(It.IsAny<Expression<Func<Organization, bool>>>(), It.IsAny<bool>()));
        }

        public static void Setup_Count_Returns_Ten(this Mock<IOrganizationRepository> repository)
        {
            repository.Setup(x => x.Count(It.IsAny<Expression<Func<Organization, bool>>>(),                    
                                          It.IsAny<bool>()))
                      .ReturnsAsync(Ten);
        }

        public static void Verify_Count(this Mock<IOrganizationRepository> repository)
        {
            repository.Verify(x => x.Count(It.IsAny<Expression<Func<Organization, bool>>>(), It.IsAny<bool>()));
        }

        public static void Verify_SelectMany(this Mock<IOrganizationRepository> repository)
        {
            repository.Verify(x => x.SelectMany(It.IsAny<Expression<Func<Organization, bool>>>(),
                    It.IsAny<int>(),
                    It.IsAny<int>(),
                    It.IsAny<Expression<Func<Organization, object>>>(),
                    It.IsAny<bool>(), It.IsAny<bool>()));
        }

        public static void Verify_SelectAfter(this Mock<IOrganizationRepository> repository)
        {
            repository.Verify(x => x.SelectAfter(It.IsAny<Expression<Func<Organization, bool>>>(),
                    It.IsAny<Guid>(),
                    It.IsAny<int>(),
                    It.IsAny<Expression<Func<Organization, object>>>(),
                    It.IsAny<bool>(), It.IsAny<bool>()));
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