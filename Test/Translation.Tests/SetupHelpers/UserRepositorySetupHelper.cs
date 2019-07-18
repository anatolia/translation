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
    public static class UserRepositorySetupHelper
    {
        public static void Setup_SelectAfter_Returns_Users(this Mock<IUserRepository> repository)
        {
            repository.Setup(x => x.SelectAfter(It.IsAny<Expression<Func<User, bool>>>(),
                                                It.IsAny<Guid>(),
                                                It.IsAny<int>(),
                                                It.IsAny<Expression<Func<User, object>>>(),
                                                It.IsAny<bool>(), false))
                      .ReturnsAsync(new List<User> { GetUser() });
        }

        public static void Setup_SelectMany_Returns_Users(this Mock<IUserRepository> repository)
        {
            repository.Setup(x => x.SelectAfter(It.IsAny<Expression<Func<User, bool>>>(),
                                                It.IsAny<Guid>(),
                                                It.IsAny<int>(),
                                                It.IsAny<Expression<Func<User, object>>>(),
                                                It.IsAny<bool>(), false))
                      .ReturnsAsync(new List<User> { GetUser() });
        }

        public static void Setup_SelectAfter_Returns_SuperAdmins(this Mock<IUserRepository> repository)
        {
            repository.Setup(x => x.SelectAfter(It.IsAny<Expression<Func<User, bool>>>(),
                                                It.IsAny<Guid>(),
                                                It.IsAny<int>(),
                                                It.IsAny<Expression<Func<User, object>>>(),
                                                It.IsAny<bool>(), false))
                      .ReturnsAsync(new List<User> { GetSuperAdmin() });
        }

        public static void Setup_SelectMany_Returns_SuperAdmins(this Mock<IUserRepository> repository)
        {
            repository.Setup(x => x.SelectAfter(It.IsAny<Expression<Func<User, bool>>>(),
                                                It.IsAny<Guid>(),
                                                It.IsAny<int>(),
                                                It.IsAny<Expression<Func<User, object>>>(),
                                                It.IsAny<bool>(), false))
                      .ReturnsAsync(new List<User> { GetSuperAdmin() });
        }

        public static void Setup_Count_Returns_Ten(this Mock<IUserRepository> repository)
        {
            repository.Setup(x => x.Count(It.IsAny<Expression<Func<User, bool>>>(),
                                          It.IsAny<bool>()))
                      .ReturnsAsync(Ten);
        }

        public static void Setup_Select_Returns_OrganizationOneSuperAdminUserInvitedAtOneDayBefore(this Mock<IUserRepository> repository)
        {
            repository.Setup(x => x.Select(It.IsAny<Expression<Func<User, bool>>>(), false))
                      .ReturnsAsync(GetOrganizationOneSuperAdminUserOneInvitedAtOneDayBefore());
        }


        public static void Setup_Select_Returns_OrganizationOneSuperAdminUserInvitedAtOneWeekBefore(this Mock<IUserRepository> repository)
        {
            repository.Setup(x => x.Select(It.IsAny<Expression<Func<User, bool>>>(), false))
                      .ReturnsAsync(GetOrganizationOneSuperAdminUserOneInvitedAtOneWeekBefore());
        }

        public static void Setup_SelectById_Returns_OrganizationOneUserOne(this Mock<IUserRepository> repository)
        {
            repository.Setup(x => x.SelectById(It.IsAny<long>()))
                      .ReturnsAsync(GetOrganizationOneUserOne());
        }

        public static void Setup_SelectById_Returns_OrganizationTwoUserOne(this Mock<IUserRepository> repository)
        {
            repository.Setup(x => x.SelectById(It.IsAny<long>()))
                      .ReturnsAsync(GetOrganizationTwoUserOne());
        }

        public static void Setup_SelectById_Returns_OrganizationOneAdminUserOne(this Mock<IUserRepository> repository)
        {
            repository.Setup(x => x.SelectById(It.IsAny<long>()))
                      .ReturnsAsync(GetOrganizationOneAdminUserOne());
        }

        public static void Setup_SelectById_Returns_OrganizationOneSuperAdminUserOne(this Mock<IUserRepository> repository)
        {
            repository.Setup(x => x.SelectById(It.IsAny<long>()))
                      .ReturnsAsync(GetOrganizationOneSuperAdminUserOne());
        }

        public static void Setup_Select_Returns_OrganizationOneUserOne(this Mock<IUserRepository> repository)
        {
            repository.Setup(x => x.Select(It.IsAny<Expression<Func<User, bool>>>(), false))
                      .ReturnsAsync(GetOrganizationOneUserOne());
        }

        public static void Setup_Select_Returns_OrganizationOneUserOneNotExist(this Mock<IUserRepository> repository)
        {
            repository.Setup(x => x.Select(It.IsAny<Expression<Func<User, bool>>>(), false))
                      .ReturnsAsync(GetOrganizationOneUserOneNotExist());
        }

        public static void Verify_SelectById(this Mock<IUserRepository> repository)
        {
            repository.Verify(x => x.SelectById(It.IsAny<long>()));
        }

        public static void Verify_SelectMany(this Mock<IUserRepository> repository)
        {
            repository.Verify(x => x.SelectMany(It.IsAny<Expression<Func<User, bool>>>(),
                                                         It.IsAny<int>(),
                                                         It.IsAny<int>(),
                                                         It.IsAny<Expression<Func<User, object>>>(),
                                                         It.IsAny<bool>(), false));
        }

        public static void Verify_SelectAfter(this Mock<IUserRepository> repository)
        {
            repository.Verify(x => x.SelectAfter(It.IsAny<Expression<Func<User, bool>>>(),
                It.IsAny<Guid>(),
                It.IsAny<int>(),
                It.IsAny<Expression<Func<User, object>>>(),
                It.IsAny<bool>(), false));
        }

        public static void Setup_Select_Returns_OrganizationTwoUserOne(this Mock<IUserRepository> repository)
        {
            repository.Setup(x => x.Select(It.IsAny<Expression<Func<User, bool>>>(), false))
                      .ReturnsAsync(GetOrganizationTwoUserOne());
        }

        public static void Verify_Select(this Mock<IUserRepository> repository)
        {
            repository.Verify(x => x.Select(It.IsAny<Expression<Func<User, bool>>>(), false));
        }

        public static void Setup_Count_Returns_POSITIVE_INT_NUMBER_10(this Mock<IUserRepository> repository)
        {
            repository.Setup(x => x.Count(It.IsAny<Expression<Func<User, bool>>>(), false))
                      .ReturnsAsync(Ten);
        }

        public static void Verify_Count(this Mock<IUserRepository> repository)
        {
            repository.Verify(x => x.Count(It.IsAny<Expression<Func<User, bool>>>(), false));
        }

        public static void Setup_Update_Success(this Mock<IUserRepository> repository)
        {
            repository.Setup(x => x.Update(It.IsAny<long>(), 
                                           It.IsAny<User>()))
                      .ReturnsAsync(true);
        }

        public static void Setup_Update_Failed(this Mock<IUserRepository> repository)
        {
            repository.Setup(x => x.Update(It.IsAny<long>(), It.IsAny<User>()))
                      .ReturnsAsync(false);
        }

        public static void Verify_Update(this Mock<IUserRepository> repository)
        {
            repository.Verify(x => x.Update(It.IsAny<long>(), It.IsAny<User>()));
        }

        public static void Setup_Insert_Success(this Mock<IUserRepository> repository)
        {
            repository.Setup(x => x.Insert(It.IsAny<long>(), 
                                           It.IsAny<User>()))
                      .ReturnsAsync(1);
        }

        public static void Setup_Insert_Failed(this Mock<IUserRepository> repository)
        {
            repository.Setup(x => x.Insert(It.IsAny<long>(), It.IsAny<User>()))
                      .ReturnsAsync(0);
        }

        public static void Verify_Insert(this Mock<IUserRepository> repository)
        {
            repository.Verify(x => x.Insert(It.IsAny<long>(), It.IsAny<User>()));
        }

        public static void Setup_Delete_Success(this Mock<IUserRepository> repository)
        {
            repository.Setup(x => x.Delete(It.IsAny<long>(), 
                                           It.IsAny<long>()))
                      .ReturnsAsync(true);
        }

        public static void Setup_Delete_Failed(this Mock<IUserRepository> repository)
        {
            repository.Setup(x => x.Delete(It.IsAny<long>(), It.IsAny<long>()))
                      .ReturnsAsync(false);
        }

        public static void Verify_Delete(this Mock<IUserRepository> repository)
        {
            repository.Verify(x => x.Delete(It.IsAny<long>(), It.IsAny<long>()));
        }

        public static void Setup_Any_Returns_True(this Mock<IUserRepository> repository)
        {
            repository.Setup(x => x.Any(It.IsAny<Expression<Func<User, bool>>>(), false))
                      .ReturnsAsync(true);
        }

        public static void Setup_Any_Returns_False(this Mock<IUserRepository> repository)
        {
            repository.Setup(x => x.Any(It.IsAny<Expression<Func<User, bool>>>(), false))
                      .ReturnsAsync(false);
        }

        public static void Verify_Any(this Mock<IUserRepository> repository)
        {
            repository.Verify(x => x.Any(It.IsAny<Expression<Func<User, bool>>>(), false));
        }
    }
}