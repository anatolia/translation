using System.Collections.Generic;
using Moq;

using Translation.Data.Entities.Domain;
using Translation.Data.UnitOfWorks.Contracts;
using static Translation.Tests.TestHelpers.FakeConstantTestHelper;

namespace Translation.Tests.SetupHelpers
{
    public static class LabelUnitOfWorkSetupHelper
    {
        public static void Setup_DoCreateWorkBulk_Returns_True(this Mock<ILabelUnitOfWork> unitOfWork)
        {
            unitOfWork.Setup(x => x.DoCreateWorkBulk(It.IsAny<long>(), 
                                                     It.IsAny<List<Label>>(),
                                                     It.IsAny<List<LabelTranslation>>(),
                                                     It.IsAny<List<LabelTranslation>>()))
                      .ReturnsAsync(BooleanTrue);
        }

        public static void Setup_DoCreateWorkBulk_Returns_False(this Mock<ILabelUnitOfWork> unitOfWork)
        {
            unitOfWork.Setup(x => x.DoCreateWorkBulk(It.IsAny<long>(),
                                                     It.IsAny<List<Label>>(),
                                                     It.IsAny<List<LabelTranslation>>(),
                                                     It.IsAny<List<LabelTranslation>>()))
                      .ReturnsAsync(BooleanFalse);
        }

        public static void Verify_DoCreateWorkBulk(this Mock<ILabelUnitOfWork> unitOfWork)
        {
            unitOfWork.Verify(x => x.DoCreateWorkBulk(It.IsAny<long>(),
                                                      It.IsAny<List<Label>>(),
                                                      It.IsAny<List<LabelTranslation>>(),
                                                      It.IsAny<List<LabelTranslation>>()));
        }

        public static void Setup_DoCreateTranslationWork_Returns_True(this Mock<ILabelUnitOfWork> unitOfWork)
        {
            unitOfWork.Setup(x => x.DoCreateTranslationWork(It.IsAny<long>(),
                                                            It.IsAny<LabelTranslation>()))
                      .ReturnsAsync(BooleanTrue);
        }

        public static void Setup_DoCreateTranslationWork_Returns_False(this Mock<ILabelUnitOfWork> unitOfWork)
        {
            unitOfWork.Setup(x => x.DoCreateTranslationWork(It.IsAny<long>(),
                                                            It.IsAny<LabelTranslation>()))
                      .ReturnsAsync(BooleanFalse);
        }

        public static void Verify_DoCreateTranslationWork(this Mock<ILabelUnitOfWork> unitOfWork)
        {
            unitOfWork.Verify(x => x.DoCreateTranslationWork(It.IsAny<long>(),
                                                             It.IsAny<LabelTranslation>()));
        }

        public static void Setup_DoDeleteWork_Returns_True(this Mock<ILabelUnitOfWork> unitOfWork)
        {
            unitOfWork.Setup(x => x.DoDeleteWork(It.IsAny<long>(),
                                                 It.IsAny<Label>()))
                      .ReturnsAsync(BooleanTrue);
        }

        public static void Setup_DoDeleteWork_Returns_False(this Mock<ILabelUnitOfWork> unitOfWork)
        {
            unitOfWork.Setup(x => x.DoDeleteWork(It.IsAny<long>(),
                                                 It.IsAny<Label>()))
                      .ReturnsAsync(BooleanFalse);
        }

        public static void Verify_DoDeleteWork(this Mock<ILabelUnitOfWork> unitOfWork)
        {
            unitOfWork.Verify(x => x.DoDeleteWork(It.IsAny<long>(),
                                                  It.IsAny<Label>()));
        }

        public static void Setup_DoCreateWork_Returns_True(this Mock<ILabelUnitOfWork> unitOfWork)
        {
            unitOfWork.Setup(x => x.DoCreateWork(It.IsAny<long>(), 
                                                 It.IsAny<Label>()))
                      .ReturnsAsync(BooleanTrue);
        }

        public static void Setup_DoCloneWork_Returns_True(this Mock<ILabelUnitOfWork> unitOfWork)
        {
            unitOfWork.Setup(x => x.DoCloneWork(It.IsAny<long>(),
                                                It.IsAny<long>(),
                                                It.IsAny<Label>()))
                      .ReturnsAsync(BooleanTrue);
        }

        public static void Setup_DoCloneWork_Returns_False(this Mock<ILabelUnitOfWork> unitOfWork)
        {
            unitOfWork.Setup(x => x.DoCloneWork(It.IsAny<long>(),
                                                It.IsAny<long>(),
                                                It.IsAny<Label>()))
                      .ReturnsAsync(BooleanFalse);
        }

        public static void Setup_DoCreateWork_Returns_False(this Mock<ILabelUnitOfWork> unitOfWork)
        {
            unitOfWork.Setup(x => x.DoCreateWork(It.IsAny<long>(),
                                                 It.IsAny<Label>()))
                      .ReturnsAsync(BooleanFalse);
        }

        public static void Verify_DoCloneWork(this Mock<ILabelUnitOfWork> unitOfWork)
        {
            unitOfWork.Verify(x => x.DoCloneWork(It.IsAny<long>(),
                                                 It.IsAny<long>(),
                                                 It.IsAny<Label>()));
        }

        public static void Verify_DoCreateWork(this Mock<ILabelUnitOfWork> unitOfWork)
        {
            unitOfWork.Verify(x => x.DoCreateWork(It.IsAny<long>(),
                                                  It.IsAny<Label>()));
        }
    }
}