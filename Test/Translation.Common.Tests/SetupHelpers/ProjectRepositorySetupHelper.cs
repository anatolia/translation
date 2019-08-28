using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Moq;
using StandardRepository.Models;
using StandardRepository.Models.Entities;
using Translation.Data.Entities.Domain;
using Translation.Data.Repositories.Contracts;
using static Translation.Common.Tests.TestHelpers.FakeConstantTestHelper;
using static Translation.Common.Tests.TestHelpers.FakeEntityTestHelper;


namespace Translation.Common.Tests.SetupHelpers
{
    public static class ProjectRepositorySetupHelper
    {
        public static void Setup_SelectAfter_Returns_Projects(this Mock<IProjectRepository> repository)
        {
            repository.Setup(x => x.SelectAfter(It.IsAny<Expression<Func<Project, bool>>>(),
                                                It.IsAny<Guid>(),
                                                It.IsAny<int>(),
                                                It.IsAny<bool>(),
                                                It.IsAny<List<OrderByInfo<Project>>>()))
                      .ReturnsAsync(new List<Project> { GetProject() });
        }

        public static void Setup_SelectMany_Returns_Projects(this Mock<IProjectRepository> repository)
        {
            repository.Setup(x => x.SelectMany(It.IsAny<Expression<Func<Project, bool>>>(),
                                               It.IsAny<int>(),
                                               It.IsAny<int>(),
                                               It.IsAny<bool>(),
                                               It.IsAny<List<OrderByInfo<Project>>>()))
                      .ReturnsAsync(new List<Project> { GetProject() });
        }

        public static void Setup_Count_Returns_Ten(this Mock<IProjectRepository> repository)
        {
            repository.Setup(x => x.Count(It.IsAny<Expression<Func<Project, bool>>>(),
                                          It.IsAny<bool>(),
                                          It.IsAny<List<DistinctInfo<Project>>>()))
                      .ReturnsAsync(Ten);
        }

        public static void Verify_SelectAfter(this Mock<IProjectRepository> repository)
        {
            repository.Verify(x => x.SelectAfter(It.IsAny<Expression<Func<Project, bool>>>(),
                                                 It.IsAny<Guid>(),
                                                 It.IsAny<int>(),
                                                 It.IsAny<bool>(),
                                                 It.IsAny<List<OrderByInfo<Project>>>()));
        }

        public static void Verify_SelectMany(this Mock<IProjectRepository> repository)
        {
            repository.Verify(x => x.SelectMany(It.IsAny<Expression<Func<Project, bool>>>(),
                                                It.IsAny<int>(),
                                                It.IsAny<int>(),
                                                It.IsAny<bool>(),
                                                It.IsAny<List<OrderByInfo<Project>>>()));
        }

        public static void Verify_Count(this Mock<IProjectRepository> repository)
        {
            repository.Setup(x => x.Count(It.IsAny<Expression<Func<Project, bool>>>(),
                                          It.IsAny<bool>(), 
                                          It.IsAny<List<DistinctInfo<Project>>>()))
                      .ReturnsAsync(Ten);
        }

        public static void Setup_RestoreRevision_Returns_True(this Mock<IProjectRepository> repository)
        {
            repository.Setup(x => x.RestoreRevision(It.IsAny<long>(), It.IsAny<long>(), It.IsAny<int>()))
                      .ReturnsAsync(BooleanTrue);
        }

        public static void Setup_RestoreRevision_Returns_False(this Mock<IProjectRepository> repository)
        {
            repository.Setup(x => x.RestoreRevision(It.IsAny<long>(), It.IsAny<long>(), It.IsAny<int>()))
                      .ReturnsAsync(BooleanFalse);
        }

        public static void Setup_SelectRevisions_Returns_InvalidRevision(this Mock<IProjectRepository> repository)
        {
            repository.Setup(x => x.SelectRevisions(It.IsAny<long>()))
                      .ReturnsAsync(new List<EntityRevision<Project>>());
        }

        public static void Setup_SelectRevisions_Returns_OrganizationOneProjectOneRevisions(this Mock<IProjectRepository> repository)
        {
            repository.Setup(x => x.SelectRevisions(It.IsAny<long>()))
                      .ReturnsAsync(GetOrganizationOneProjectOneRevisions());
        }

        public static void Setup_Select_Returns_OrganizationOneProjectOne(this Mock<IProjectRepository> repository)
        {
            repository.Setup(x => x.Select(It.IsAny<Expression<Func<Project, bool>>>(), false))
                      .ReturnsAsync(GetOrganizationOneProjectOne());
        }

        public static void Setup_Select_Returns_OrganizationOneSuperProjectOne(this Mock<IProjectRepository> repository)
        {
            repository.Setup(x => x.Select(It.IsAny<Expression<Func<Project, bool>>>(), false))
                      .ReturnsAsync(GetOrganizationOneSuperProjectOne());
        }

        public static void Setup_Select_Returns_OrganizationTwoProjectOne(this Mock<IProjectRepository> repository)
        {
            repository.Setup(x => x.Select(It.IsAny<Expression<Func<Project, bool>>>(), false))
                      .ReturnsAsync(GetOrganizationTwoProjectOne());
        }

        public static void Setup_Select_Returns_OrganizationOneProjectOneNotExist(this Mock<IProjectRepository> repository)
        {
            repository.Setup(x => x.Select(It.IsAny<Expression<Func<Project, bool>>>(), false))
                      .ReturnsAsync(GetOrganizationOneProjectOneNotExist());
        }

        public static void Setup_Select_Returns_OrganizationOneProjectOneNotActive(this Mock<IProjectRepository> repository)
        {
            repository.Setup(x => x.Select(It.IsAny<Expression<Func<Project, bool>>>(), false))
                      .ReturnsAsync(GetOrganizationOneProjectOneNotActive());
        }

        public static void Setup_Select_Returns_InvalidProject(this Mock<IProjectRepository> repository)
        {
            repository.Setup(x => x.Select(It.IsAny<Expression<Func<Project, bool>>>(), false))
                      .ReturnsAsync(new Project());
        }

        public static void Verify_Select(this Mock<IProjectRepository> repository)
        {
            repository.Verify(x => x.Select(It.IsAny<Expression<Func<Project, bool>>>(), false));

        }

        public static void Setup_Update_Success(this Mock<IProjectRepository> repository)
        {
            repository.Setup(x => x.Update(It.IsAny<long>(),
                                           It.IsAny<Project>()))
                      .ReturnsAsync(true);
        }

        public static void Setup_Update_Failed(this Mock<IProjectRepository> repository)
        {
            repository.Setup(x => x.Update(It.IsAny<long>(),
                                           It.IsAny<Project>()))
                      .ReturnsAsync(false);
        }

        public static void Verify_SelectRevisions(this Mock<IProjectRepository> repository)
        {
            repository.Verify(x => x.SelectRevisions(It.IsAny<long>()));
        }

        public static void Verify_Update(this Mock<IProjectRepository> repository)
        {
            repository.Verify(x => x.Update(It.IsAny<long>(),
                                            It.IsAny<Project>()));
        }

        public static void Setup_Delete_Success(this Mock<IProjectRepository> repository)
        {
            repository.Setup(x => x.Delete(It.IsAny<long>(),
                                           It.IsAny<long>()))
                      .ReturnsAsync(true);
        }

        public static void Setup_Delete_Failed(this Mock<IProjectRepository> repository)
        {
            repository.Setup(x => x.Delete(It.IsAny<long>(),
                                           It.IsAny<long>()))
                      .ReturnsAsync(false);
        }

        public static void Verify_Delete(this Mock<IProjectRepository> repository)
        {
            repository.Verify(x => x.Delete(It.IsAny<long>(),
                                            It.IsAny<long>()));
        }

        public static void Setup_Insert_Success(this Mock<IProjectRepository> repository)
        {
            repository.Setup(x => x.Insert(It.IsAny<long>(),
                                           It.IsAny<Project>()))
                      .ReturnsAsync(1);
        }

        public static void Setup_Insert_Failed(this Mock<IProjectRepository> repository)
        {
            repository.Setup(x => x.Insert(It.IsAny<long>(),
                                           It.IsAny<Project>()))
                      .ReturnsAsync(0);
        }

        public static void Verify_Insert(this Mock<IProjectRepository> repository)
        {
            repository.Verify(x => x.Insert(It.IsAny<long>(),
                                            It.IsAny<Project>()));
        }

        public static void Setup_Any_Returns_False(this Mock<IProjectRepository> repository)
        {
            repository.Setup(x => x.Any(It.IsAny<Expression<Func<Project, bool>>>(), false))
                      .ReturnsAsync(false);
        }

        public static void Setup_Any_Returns_True(this Mock<IProjectRepository> repository)
        {
            repository.Setup(x => x.Any(It.IsAny<Expression<Func<Project, bool>>>(), false))
                      .ReturnsAsync(true);
        }

        public static void Verify_RestoreRevision(this Mock<IProjectRepository> repository)
        {
            repository.Verify(x => x.RestoreRevision(It.IsAny<long>(), It.IsAny<long>(), It.IsAny<int>()));
        }

        public static void Verify_Any(this Mock<IProjectRepository> repository)
        {
            repository.Verify(x => x.Any(It.IsAny<Expression<Func<Project, bool>>>(), false));
        }
    }
}