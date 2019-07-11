using System;
using System.Linq.Expressions;

using Moq;

using Translation.Data.Entities.Main;
using Translation.Data.Repositories.Contracts;
using static Translation.Tests.TestHelpers.FakeEntityTestHelper;
using static Translation.Tests.TestHelpers.FakeConstantTestHelper;

namespace Translation.Tests.SetupHelpers
{
    public static class IntegrationRepositorySetupHelper
    {
        //public static void Setup_SelectAfter_Returns_IntegrationList(this Mock<IIntegrationRepository> repository)
        //{
        //    repository.Setup(x => x.SelectAfter(It.IsAny<Expression<Func<Integration, bool>>>(),
        //            It.IsAny<Guid>(), It.IsAny<int>(),
        //            It.IsAny<Expression<Func<Integration, object>>>(),
        //            It.IsAny<bool>(), false))
        //        .ReturnsAsync(EntityDataHelper.GetFakeParkNetIntegrationList());
        //}

        public static void Verify_SelectAfter(this Mock<IIntegrationRepository> repository)
        {
            repository.Verify(x => x.SelectAfter(It.IsAny<Expression<Func<Integration, bool>>>(),
                    It.IsAny<Guid>(), It.IsAny<int>(),
                    It.IsAny<Expression<Func<Integration, object>>>(),
                    It.IsAny<bool>(), false));
        }

        //public static void Setup_SelectMany_Returns_IntegrationList(this Mock<IIntegrationRepository> repository)
        //{
        //    repository.Setup(x => x.SelectMany(It.IsAny<Expression<Func<Integration, bool>>>(),
        //        It.IsAny<int>(), It.IsAny<int>(),
        //        It.IsAny<Expression<Func<Integration, object>>>(),
        //        It.IsAny<bool>(), false))
        //        .ReturnsAsync(EntityDataHelper.GetFakeParkNetIntegrationList());
        //}

        public static void Setup_Update_Returns_True(this Mock<IIntegrationRepository> repository)
        {
            repository.Setup(x => x.Update(It.IsAny<long>(),
                    It.IsAny<Integration>()))
                .ReturnsAsync(true);
        }

        public static void Setup_Update_Returns_False(this Mock<IIntegrationRepository> repository)
        {
            repository.Setup(x => x.Update(It.IsAny<long>(), It.IsAny<Integration>()))
                .ReturnsAsync(false);
        }

        public static void Verify_Update(this Mock<IIntegrationRepository> repository)
        {
            repository.Verify(x => x.Update(It.IsAny<long>(), It.IsAny<Integration>()));
        }

        public static void Setup_Delete_Returns_True(this Mock<IIntegrationRepository> repository)
        {
            repository.Setup(x => x.Delete(It.IsAny<long>(),
                    It.IsAny<long>()))
                .ReturnsAsync(true);
        }

        public static void Setup_Delete_Returns_False(this Mock<IIntegrationRepository> repository)
        {
            repository.Setup(x => x.Delete(It.IsAny<long>(),
                    It.IsAny<long>()))
                .ReturnsAsync(false);
        }

        public static void Verify_Delete(this Mock<IIntegrationRepository> repository)
        {
            repository.Verify(x => x.Delete(It.IsAny<long>(),
                    It.IsAny<long>()));
        }

        public static void Setup_SelectById_Returns_ParkNetIntegration(this Mock<IIntegrationRepository> repository)
        {
            repository.Setup(x => x.SelectById(It.IsAny<long>()))
                .ReturnsAsync(GetOrganizationOneIntegrationOne());
        }

        public static void Setup_SelectById_Returns_BlueSoftIntegration(this Mock<IIntegrationRepository> repository)
        {
            repository.Setup(x => x.SelectById(It.IsAny<long>()))
                .ReturnsAsync(GetOrganizationTwoIntegrationOne());
        }

        public static void Verify_SelectById(this Mock<IIntegrationRepository> repository)
        {
            repository.Verify(x => x.SelectById(It.IsAny<long>()));
        }

        public static void Verify_SelectMany(this Mock<IIntegrationRepository> repository)
        {
            repository.Verify(x => x.SelectMany(It.IsAny<Expression<Func<Integration, bool>>>(),
                It.IsAny<int>(), It.IsAny<int>(),
                It.IsAny<Expression<Func<Integration, object>>>(),
                It.IsAny<bool>(), false));
        }

        public static void Setup_Select_Returns_ParkNetIntegration(this Mock<IIntegrationRepository> repository)
        {
            repository.Setup(x => x.Select(It.IsAny<Expression<Func<Integration, bool>>>(), false))
                .ReturnsAsync(GetOrganizationOneIntegrationOne());
        }

        public static void Setup_Select_Returns_BlueSoftIntegration(this Mock<IIntegrationRepository> repository)
        {
            repository.Setup(x => x.Select(It.IsAny<Expression<Func<Integration, bool>>>(), false))
                .ReturnsAsync(GetOrganizationTwoIntegrationOne);
        }

        //public static void Setup_Select_Returns_NotExistParkNetIntegration(this Mock<IIntegrationRepository> repository)
        //{
        //    repository.Setup(x => x.Select(It.IsAny<Expression<Func<Integration, bool>>>(), false))
        //        .ReturnsAsync(EntityDataHelper.GetFakeParkNetNotExistIntegration());
        //}

        //public static void Setup_Select_Returns_NotExistBlueSoftIntegration(this Mock<IIntegrationRepository> repository)
        //{
        //    repository.Setup(x => x.Select(It.IsAny<Expression<Func<Integration, bool>>>(), false))
        //        .ReturnsAsync(EntityDataHelper.GetFakeBlueSoftNotExistIntegration());
        //}

        public static void Verify_Select(this Mock<IIntegrationRepository> repository)
        {
            repository.Verify(x => x.Select(It.IsAny<Expression<Func<Integration, bool>>>(), false));
        }

        public static void Setup_Any_Success(this Mock<IIntegrationRepository> repository)
        {
            repository.Setup(x => x.Any(It.IsAny<Expression<Func<Integration, bool>>>(), false)).ReturnsAsync(true);
        }

        public static void Setup_Any_Failed(this Mock<IIntegrationRepository> repository)
        {
            repository.Setup(x => x.Any(It.IsAny<Expression<Func<Integration, bool>>>(), false)).ReturnsAsync(false);
        }

        public static void Setup_Count_Returns_POSITIVE_INT_NUMBER_10(this Mock<IIntegrationRepository> repository)
        {
            repository.Setup(x => x.Count(It.IsAny<Expression<Func<Integration, bool>>>(), false))
                                      .ReturnsAsync(Ten);
        }

        public static void Setup_IntegrationAlreadyExist(this Mock<IIntegrationRepository> repository)
        {
            repository.Setup(x => x.Any(It.IsAny<Expression<Func<Integration, bool>>>(), false))
                      .ReturnsAsync(true);
        }

        public static void Setup_IntegrationNotExist(this Mock<IIntegrationRepository> repository)
        {
            repository.Setup(x => x.Any(It.IsAny<Expression<Func<Integration, bool>>>(), false))
                .ReturnsAsync(false);
        }

        public static void Verify_Any(this Mock<IIntegrationRepository> repository)
        {
            repository.Verify(x => x.Any(It.IsAny<Expression<Func<Integration, bool>>>(), false));
        }
    }
}