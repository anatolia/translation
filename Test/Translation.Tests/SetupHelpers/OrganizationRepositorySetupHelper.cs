using System;
using System.Linq.Expressions;

using Moq;

using Translation.Data.Entities.Main;
using Translation.Data.Repositories.Contracts;
using static Translation.Tests.TestHelpers.FakeEntityTestHelper;

namespace Translation.Tests.SetupHelpers
{
    public static class OrganizationRepositorySetupHelper
    {
        public static void Setup_SelectById_Returns_OrganizationOne(this Mock<IOrganizationRepository> repository)
        {
            repository.Setup(x => x.SelectById(It.IsAny<long>())).ReturnsAsync(GetOrganizationOne());
        }

        public static void Setup_Select_Returns_OrganizationOne(this Mock<IOrganizationRepository> repository)
        {
            repository.Setup(x => x.Select(It.IsAny<Expression<Func<Organization, bool>>>(), false)).ReturnsAsync(GetOrganizationOne());
        }
    }
}