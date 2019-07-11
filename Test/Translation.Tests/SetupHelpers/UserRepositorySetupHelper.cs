using System;
using System.Linq.Expressions;

using Moq;

using Translation.Data.Entities.Main;
using Translation.Data.Repositories.Contracts;
using static Translation.Tests.TestHelpers.FakeEntityTestHelper;

namespace Translation.Tests.SetupHelpers
{
    public static class UserRepositorySetupHelper
    {
        public static void Setup_SelectById_Returns_OrganizationOneUserOne(this Mock<IUserRepository> repository)
        {
            repository.Setup(x => x.SelectById(It.IsAny<long>())).ReturnsAsync(GetOrganizationOneUserOne());
        }

        public static void Setup_Select_Returns_OrganizationOneUserOne(this Mock<IUserRepository> repository)
        {
            repository.Setup(x => x.Select(It.IsAny<Expression<Func<User, bool>>>(), false)).ReturnsAsync(GetOrganizationOneUserOne());
        }
    }
}