using System;
using System.Collections.Generic;
using System.Linq.Expressions;

using Moq;

using Translation.Data.Entities.Domain;
using Translation.Data.Repositories.Contracts;
using static Translation.Tests.TestHelpers.FakeEntityTestHelper;
using static Translation.Tests.TestHelpers.FakeConstantTestHelper;

namespace Translation.Tests.SetupHelpers
{
    public static class LabelTranslationRepositorySetupHelper
    {
        public static void Setup_RestoreRevision_Returns_True(this Mock<ILabelTranslationRepository> repository)
        {
            repository.Setup(x => x.RestoreRevision(It.IsAny<long>(), It.IsAny<long>(), It.IsAny<int>()))
                      .ReturnsAsync(BooleanTrue);
        }

        public static void Setup_RestoreRevision_Returns_False(this Mock<ILabelTranslationRepository> repository)
        {
            repository.Setup(x => x.RestoreRevision(It.IsAny<long>(), It.IsAny<long>(), It.IsAny<int>()))
                      .ReturnsAsync(BooleanFalse);
        }

        public static void Verify_RestoreRevision(this Mock<ILabelTranslationRepository> repository)
        {
            repository.Verify(x => x.RestoreRevision(It.IsAny<long>(), It.IsAny<long>(), It.IsAny<int>()));
        }

        public static void Setup_Count_Returns_Ten(this Mock<ILabelTranslationRepository> repository)
        {
            repository.Setup(x => x.Count(It.IsAny<Expression<Func<LabelTranslation, bool>>>(),
                                          It.IsAny<bool>()))
                      .ReturnsAsync(Ten);
        }

        public static void Verify_Count(this Mock<ILabelTranslationRepository> repository)
        {
            repository.Verify(x => x.Count(It.IsAny<Expression<Func<LabelTranslation, bool>>>(),
                                           It.IsAny<bool>()));
        }

        public static void Setup_SelectAll_Returns_LabelTranslations(this Mock<ILabelTranslationRepository> repository)
        {
            repository.Setup(x => x.SelectAll(It.IsAny<Expression<Func<LabelTranslation, bool>>>(),
                                              It.IsAny<Expression<Func<LabelTranslation, object>>>(),
                                              It.IsAny<bool>(),
                                              It.IsAny<bool>()))
                      .ReturnsAsync(new List<LabelTranslation> { GetLabelTranslation() });
        }

        public static void Verify_SelectAll(this Mock<ILabelTranslationRepository> repository)
        {
            repository.Verify(x => x.SelectAll(It.IsAny<Expression<Func<LabelTranslation, bool>>>(),
                                               It.IsAny<Expression<Func<LabelTranslation, object>>>(),
                                               It.IsAny<bool>(),
                                               It.IsAny<bool>()));
        }

        public static void Setup_SelectById_Returns_ParkNetLabelTranslation(this Mock<ILabelTranslationRepository> repository)
        {
            repository.Setup(x => x.SelectById(It.IsAny<long>()))
                      .ReturnsAsync(GetOrganizationOneProjectOneLabelOneLabelTranslationOne());

        }

        public static void Verify_SelectById(this Mock<ILabelTranslationRepository> repository)
        {
            repository.Verify(x => x.SelectById(It.IsAny<long>()));
        }

        public static void Setup_SelectRevisions_Returns_GetOrganizationOneProjectOneLabelOneLabelTranslationOneRevisionsRevisionOneInIt(this Mock<ILabelTranslationRepository> repository)
        {
            repository.Setup(x => x.SelectRevisions(It.IsAny<long>()))
                      .ReturnsAsync(GetOrganizationOneProjectOneLabelOneLabelTranslationOneRevisionsRevisionOneInIt());
        }

        public static void Setup_SelectRevisions_Returns_GetOrganizationOneProjectOneLabelOneLabelTranslationOneRevisionsRevisionTwoInIt(this Mock<ILabelTranslationRepository> repository)
        {
            repository.Setup(x => x.SelectRevisions(It.IsAny<long>()))
                .ReturnsAsync(GetOrganizationOneProjectOneLabelOneLabelTranslationOneRevisionsRevisionTwoInIt());
        }

        public static void Verify_SelectRevisions(this Mock<ILabelTranslationRepository> repository)
        {
            repository.Verify(x => x.SelectRevisions(It.IsAny<long>()));
        }

        public static void Setup_Select_Returns_OrganizationOneProjectOneLabelOneLabelTranslationOne(this Mock<ILabelTranslationRepository> repository)
        {
            repository.Setup(x => x.Select(It.IsAny<Expression<Func<LabelTranslation, bool>>>(), false))
                      .ReturnsAsync(GetOrganizationOneProjectOneLabelOneLabelTranslationOne());
        }

        public static void Setup_Select_Returns_OrganizationOneProjectOneLabelOneLabelTranslationOneNotActive(this Mock<ILabelTranslationRepository> repository)
        {
            repository.Setup(x => x.Select(It.IsAny<Expression<Func<LabelTranslation, bool>>>(), false))
                      .ReturnsAsync(GetOrganizationOneProjectOneLabelOneLabelTranslationOneNotActive());
        }

        public static void Setup_Select_Returns_OrganizationOneProjectOneLabelOneLabelTranslationOneNotExist(this Mock<ILabelTranslationRepository> repository)
        {
            repository.Setup(x => x.Select(It.IsAny<Expression<Func<LabelTranslation, bool>>>(), false))
                .ReturnsAsync(GetOrganizationOneProjectOneLabelOneLabelTranslationOneNotExist());
        }

        public static void Setup_Select_Returns_OrganizationTwoProjectOneLabelOneLabelTranslationOne(this Mock<ILabelTranslationRepository> repository)
        {
            repository.Setup(x => x.Select(It.IsAny<Expression<Func<LabelTranslation, bool>>>(), false))
                .ReturnsAsync(GetOrganizationTwoProjectOneLabelOneLabelTranslationOne());
        }

        public static void Setup_Select_Returns_OrganizationOneLabelTranslationOneNotExist(this Mock<ILabelTranslationRepository> repository)
        {
            repository.Setup(x => x.Select(It.IsAny<Expression<Func<LabelTranslation, bool>>>(), false))
                      .ReturnsAsync(GetOrganizationOneProjectOneLabelOneLabelTranslationOneNotExist());
        }

        public static void Verify_Select(this Mock<ILabelTranslationRepository> repository)
        {
            repository.Verify(x => x.Select(It.IsAny<Expression<Func<LabelTranslation, bool>>>(), false));
        }

        public static void Verify_SelectMany(this Mock<ILabelTranslationRepository> repository)
        {
            repository.Verify(x => x.SelectMany(It.IsAny<Expression<Func<LabelTranslation, bool>>>(),
                                               It.IsAny<int>(),
                                               It.IsAny<int>(),
                                               It.IsAny<Expression<Func<LabelTranslation, object>>>(),
                                               It.IsAny<bool>(), false));
        }

        public static void Setup_Insert_Success(this Mock<ILabelTranslationRepository> repository)
        {
            repository.Setup(x => x.Insert(It.IsAny<long>(),
                                           It.IsAny<LabelTranslation>()))
                      .ReturnsAsync(1);
        }

        public static void Setup_Insert_Failed(this Mock<ILabelTranslationRepository> repository)
        {
            repository.Setup(x => x.Insert(It.IsAny<long>(),
                                           It.IsAny<LabelTranslation>()))
                      .ReturnsAsync(0);
        }

        public static void Verify_Insert(this Mock<ILabelTranslationRepository> repository)
        {
            repository.Verify(x => x.Insert(It.IsAny<long>(),
                It.IsAny<LabelTranslation>()));
        }

        public static void Setup_Delete_Success(this Mock<ILabelTranslationRepository> repository)
        {
            repository.Setup(x => x.Delete(It.IsAny<long>(),
                                           It.IsAny<long>()))
                      .ReturnsAsync(true);
        }

        public static void Setup_Delete_Failed(this Mock<ILabelTranslationRepository> repository)
        {
            repository.Setup(x => x.Delete(It.IsAny<long>(),
                                           It.IsAny<long>()))
                      .ReturnsAsync(false);
        }

        public static void Verify_Delete(this Mock<ILabelTranslationRepository> repository)
        {
            repository.Verify(x => x.Delete(It.IsAny<long>(),
                                            It.IsAny<long>()));
        }

        public static void Setup_Update_Success(this Mock<ILabelTranslationRepository> repository)
        {
            repository.Setup(x => x.Update(It.IsAny<long>(),
                                           It.IsAny<LabelTranslation>()))
                      .ReturnsAsync(true);
        }

        public static void Setup_Update_Failed(this Mock<ILabelTranslationRepository> repository)
        {
            repository.Setup(x => x.Update(It.IsAny<long>(),
                                           It.IsAny<LabelTranslation>()))
                      .ReturnsAsync(false);
        }

        public static void Verify_Update(this Mock<ILabelTranslationRepository> repository)
        {
            repository.Verify(x => x.Update(It.IsAny<long>(),
                                            It.IsAny<LabelTranslation>()));
        }

        public static void Setup_LabelAlreadyExist(this Mock<ILabelTranslationRepository> repository)
        {
            repository.Setup(x => x.Any(It.IsAny<Expression<Func<LabelTranslation, bool>>>(), false))
                      .ReturnsAsync(true);
        }

        public static void Setup_LabelNotExist(this Mock<ILabelTranslationRepository> repository)
        {
            repository.Setup(x => x.Any(It.IsAny<Expression<Func<LabelTranslation, bool>>>(), false))
                      .ReturnsAsync(false);
        }

        public static void Setup_Any_Return_False(this Mock<ILabelTranslationRepository> repository)
        {
            repository.Setup(x => x.Any(It.IsAny<Expression<Func<LabelTranslation, bool>>>(), It.IsAny<bool>()))
                      .ReturnsAsync(false);
        }

        public static void Setup_Any_Return_True(this Mock<ILabelTranslationRepository> repository)
        {
            repository.Setup(x => x.Any(It.IsAny<Expression<Func<LabelTranslation, bool>>>(), It.IsAny<bool>()))
                      .ReturnsAsync(true);
        }

        public static void Verify_Any(this Mock<ILabelTranslationRepository> repository)
        {
            repository.Verify(x => x.Any(It.IsAny<Expression<Func<LabelTranslation, bool>>>(), false));
        }
    }
}