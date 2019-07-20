using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

using Moq;

using Translation.Common.Contracts;
using Translation.Common.Enumerations;
using Translation.Common.Models.DataTransferObjects;
using Translation.Common.Models.Requests.Label;
using Translation.Common.Models.Requests.Label.LabelTranslation;
using Translation.Common.Models.Responses.Label;
using Translation.Common.Models.Responses.Label.LabelTranslation;
using Translation.Data.Entities.Domain;
using Translation.Data.Repositories.Contracts;
using static Translation.Tests.TestHelpers.FakeConstantTestHelper;
using static Translation.Tests.TestHelpers.FakeEntityTestHelper;

namespace Translation.Tests.SetupHelpers
{
    public static class LabelServiceSetupHelper
    {
        public static void Setup_RestoreRevision_Returns_True(this Mock<ILabelRepository> repository)
        {
            repository.Setup(x => x.RestoreRevision(It.IsAny<long>(), It.IsAny<long>(), It.IsAny<int>()))
                      .ReturnsAsync(BooleanTrue);
        }

        public static void Setup_RestoreRevision_Returns_False(this Mock<ILabelRepository> repository)
        {
            repository.Setup(x => x.RestoreRevision(It.IsAny<long>(), It.IsAny<long>(), It.IsAny<int>()))
                      .ReturnsAsync(BooleanFalse);
        }

        public static void Verify_RestoreRevision(this Mock<ILabelRepository> repository)
        {
            repository.Verify(x => x.RestoreRevision(It.IsAny<long>(), It.IsAny<long>(), It.IsAny<int>()));
        }

        public static void Setup_SelectRevisions_Returns_OrganizationOneProjectOneLabelOneRevisionsRevisionOneInIt(this Mock<ILabelRepository> repository)
        {
            repository.Setup(x => x.SelectRevisions(It.IsAny<long>()))
                      .ReturnsAsync(GetOrganizationOneProjectOneLabelOneRevisionsRevisionOneInIt());
        }

        public static void Verify_SelectRevisions(this Mock<ILabelRepository> repository)
        {
            repository.Verify(x => x.SelectRevisions(It.IsAny<long>()));
        }

        public static void Setup_GetLabels_Returns_LabelReadListResponse_Success(this Mock<ILabelService> service)
        {
            var items = new List<LabelDto>();
            items.Add(new LabelDto() { Uid = UidOne });

            service.Setup(x => x.GetLabels(It.IsAny<LabelReadListRequest>()))
                   .Returns(Task.FromResult(new LabelReadListResponse { Status = ResponseStatus.Success, Items = items }));
        }

        public static void Setup_GetLabel_Returns_LabelReadResponse_Success(this Mock<ILabelService> service)
        {
            service.Setup(x => x.GetLabel(It.IsAny<LabelReadRequest>()))
                   .ReturnsAsync(new LabelReadResponse { Status = ResponseStatus.Success });
        }

        public static void Setup_CreateLabel_Returns_LabelCreateResponse_Success(this Mock<ILabelService> service)
        {
            service.Setup(x => x.CreateLabel(It.IsAny<LabelCreateRequest>()))
                   .Returns(Task.FromResult(new LabelCreateResponse { Status = ResponseStatus.Success }));
        }

        public static void Setup_CreateLabelFromList_Returns_LabelCreateListResponse_Success(this Mock<ILabelService> service)
        {
            service.Setup(x => x.CreateLabelFromList(It.IsAny<LabelCreateListRequest>()))
                   .Returns(Task.FromResult(new LabelCreateListResponse { Status = ResponseStatus.Success }));
        }

        public static void Setup_EditLabel_Returns_LabelEditResponse_Success(this Mock<ILabelService> service)
        {
            service.Setup(x => x.EditLabel(It.IsAny<LabelEditRequest>()))
                   .Returns(Task.FromResult(new LabelEditResponse { Status = ResponseStatus.Success }));
        }

        public static void Setup_DeleteLabel_Returns_LabelDeleteResponse_Success(this Mock<ILabelService> service)
        {
            service.Setup(x => x.DeleteLabel(It.IsAny<LabelDeleteRequest>()))
                   .Returns(Task.FromResult(new LabelDeleteResponse { Status = ResponseStatus.Success }));
        }

        public static void Setup_CloneLabel_Returns_LabelCloneResponse_Success(this Mock<ILabelService> service)
        {
            service.Setup(x => x.CloneLabel(It.IsAny<LabelCloneRequest>()))
                   .Returns(Task.FromResult(new LabelCloneResponse { Status = ResponseStatus.Success }));
        }

        public static void Setup_GetTranslations_Returns_LabelTranslationReadListResponse_Success(this Mock<ILabelService> service)
        {
            service.Setup(x => x.GetTranslations(It.IsAny<LabelTranslationReadListRequest>()))
                   .Returns(Task.FromResult(new LabelTranslationReadListResponse { Status = ResponseStatus.Success }));
        }

        public static void Setup_GetLabelsWithTranslations_Returns_AllLabelReadListResponse_Success(this Mock<ILabelService> service)
        {
            service.Setup(x => x.GetLabelsWithTranslations(It.IsAny<AllLabelReadListRequest>()))
                   .Returns(Task.FromResult(new AllLabelReadListResponse { Status = ResponseStatus.Success }));
        }

        public static void Setup_CreateTranslation_Returns_LabelTranslationCreateResponse_Success(this Mock<ILabelService> service)
        {
            service.Setup(x => x.CreateTranslation(It.IsAny<LabelTranslationCreateRequest>()))
                   .Returns(Task.FromResult(new LabelTranslationCreateResponse { Status = ResponseStatus.Success }));
        }

        public static void Setup_CreateTranslationFromList_Returns_LabelTranslationCreateListResponse_Success(this Mock<ILabelService> service)
        {
            service.Setup(x => x.CreateTranslationFromList(It.IsAny<LabelTranslationCreateListRequest>()))
                   .Returns(Task.FromResult(new LabelTranslationCreateListResponse { Status = ResponseStatus.Success }));
        }

        public static void Setup_EditTranslation_Returns_LabelTranslationEditResponse_Success(this Mock<ILabelService> service)
        {
            service.Setup(x => x.EditTranslation(It.IsAny<LabelTranslationEditRequest>()))
                   .Returns(Task.FromResult(new LabelTranslationEditResponse { Status = ResponseStatus.Success }));
        }

        public static void Setup_DeleteTranslation_Returns_LabelTranslationDeleteResponse_Success(this Mock<ILabelService> service)
        {
            service.Setup(x => x.DeleteTranslation(It.IsAny<LabelTranslationDeleteRequest>()))
                   .Returns(Task.FromResult(new LabelTranslationDeleteResponse { Status = ResponseStatus.Success }));
        }

        public static void Setup_ChangeActivation_Returns_LabelChangeActivationResponse_Success(this Mock<ILabelService> service)
        {
            service.Setup(x => x.ChangeActivation(It.IsAny<LabelChangeActivationRequest>()))
                   .Returns(Task.FromResult(new LabelChangeActivationResponse { Status = ResponseStatus.Success }));
        }

        public static void Setup_GetTranslation_Returns_LabelTranslationReadListResponse_Success(this Mock<ILabelService> service)
        {
            service.Setup(x => x.GetTranslation(It.IsAny<LabelTranslationReadRequest>()))
                   .Returns(Task.FromResult(new LabelTranslationReadListResponse { Status = ResponseStatus.Success }));
        }

        public static void Setup_GetLabelsWithTranslations_Returns_AllLabelReadListResponse_Failed(this Mock<ILabelService> service)
        {
            service.Setup(x => x.GetLabelsWithTranslations(It.IsAny<AllLabelReadListRequest>()))
                   .Returns(Task.FromResult(new AllLabelReadListResponse { Status = ResponseStatus.Failed, ErrorMessages = new List<string> { StringOne } }));
        }

        public static void Setup_GetLabels_Returns_LabelReadListResponse_Failed(this Mock<ILabelService> service)
        {
            service.Setup(x => x.GetLabels(It.IsAny<LabelReadListRequest>()))
                   .Returns(Task.FromResult(new LabelReadListResponse { Status = ResponseStatus.Failed, ErrorMessages = new List<string> { StringOne } }));
        }

        public static void Setup_GetLabel_Returns_LabelReadResponse_Failed(this Mock<ILabelService> service)
        {
            service.Setup(x => x.GetLabel(It.IsAny<LabelReadRequest>()))
                   .ReturnsAsync(new LabelReadResponse { Status = ResponseStatus.Failed, ErrorMessages = new List<string> { StringOne } });
        }

        public static void Setup_CreateLabel_Returns_LabelCreateResponse_Failed(this Mock<ILabelService> service)
        {
            service.Setup(x => x.CreateLabel(It.IsAny<LabelCreateRequest>()))
                   .Returns(Task.FromResult(new LabelCreateResponse { Status = ResponseStatus.Failed, ErrorMessages = new List<string> { StringOne } }));
        }

        public static void Setup_CreateLabelFromList_Returns_LabelCreateListResponse_Failed(this Mock<ILabelService> service)
        {
            service.Setup(x => x.CreateLabelFromList(It.IsAny<LabelCreateListRequest>()))
                   .Returns(Task.FromResult(new LabelCreateListResponse { Status = ResponseStatus.Failed, ErrorMessages = new List<string> { StringOne } }));
        }

        public static void Setup_EditLabel_Returns_LabelEditResponse_Failed(this Mock<ILabelService> service)
        {
            service.Setup(x => x.EditLabel(It.IsAny<LabelEditRequest>()))
                   .Returns(Task.FromResult(new LabelEditResponse { Status = ResponseStatus.Failed, ErrorMessages = new List<string> { StringOne } }));
        }

        public static void Setup_DeleteLabel_Returns_LabelDeleteResponse_Failed(this Mock<ILabelService> service)
        {
            service.Setup(x => x.DeleteLabel(It.IsAny<LabelDeleteRequest>()))
                   .Returns(Task.FromResult(new LabelDeleteResponse { Status = ResponseStatus.Failed, ErrorMessages = new List<string> { StringOne } }));
        }

        public static void Setup_CloneLabel_Returns_LabelCloneResponse_Failed(this Mock<ILabelService> service)
        {
            service.Setup(x => x.CloneLabel(It.IsAny<LabelCloneRequest>()))
                   .Returns(Task.FromResult(new LabelCloneResponse { Status = ResponseStatus.Failed, ErrorMessages = new List<string> { StringOne } }));
        }

        public static void Setup_GetTranslations_Returns_LabelTranslationReadListResponse_Failed(this Mock<ILabelService> service)
        {
            service.Setup(x => x.GetTranslations(It.IsAny<LabelTranslationReadListRequest>()))
                   .Returns(Task.FromResult(new LabelTranslationReadListResponse { Status = ResponseStatus.Failed, ErrorMessages = new List<string> { StringOne } }));
        }

        public static void Setup_CreateTranslation_Returns_LabelTranslationCreateResponse_Failed(this Mock<ILabelService> service)
        {
            service.Setup(x => x.CreateTranslation(It.IsAny<LabelTranslationCreateRequest>()))
                   .Returns(Task.FromResult(new LabelTranslationCreateResponse { Status = ResponseStatus.Failed, ErrorMessages = new List<string> { StringOne } }));
        }

        public static void Setup_CreateTranslationFromList_Returns_LabelTranslationCreateListResponse_Failed(this Mock<ILabelService> service)
        {
            service.Setup(x => x.CreateTranslationFromList(It.IsAny<LabelTranslationCreateListRequest>()))
                   .Returns(Task.FromResult(new LabelTranslationCreateListResponse { Status = ResponseStatus.Failed, ErrorMessages = new List<string> { StringOne } }));
        }

        public static void Setup_EditTranslation_Returns_LabelTranslationEditResponse_Failed(this Mock<ILabelService> service)
        {
            service.Setup(x => x.EditTranslation(It.IsAny<LabelTranslationEditRequest>()))
                   .Returns(Task.FromResult(new LabelTranslationEditResponse { Status = ResponseStatus.Failed, ErrorMessages = new List<string> { StringOne } }));
        }

        public static void Setup_DeleteTranslation_Returns_LabelTranslationDeleteResponse_Failed(this Mock<ILabelService> service)
        {
            service.Setup(x => x.DeleteTranslation(It.IsAny<LabelTranslationDeleteRequest>()))
                   .Returns(Task.FromResult(new LabelTranslationDeleteResponse { Status = ResponseStatus.Failed, ErrorMessages = new List<string> { StringOne } }));
        }

        public static void Setup_ChangeActivation_Returns_LabelChangeActivationResponse_Failed(this Mock<ILabelService> service)
        {
            service.Setup(x => x.ChangeActivation(It.IsAny<LabelChangeActivationRequest>()))
                   .Returns(Task.FromResult(new LabelChangeActivationResponse { Status = ResponseStatus.Failed, ErrorMessages = new List<string> { StringOne } }));
        }

        public static void Setup_GetTranslation_Returns_LabelTranslationReadListResponse_Failed(this Mock<ILabelService> service)
        {
            service.Setup(x => x.GetTranslation(It.IsAny<LabelTranslationReadRequest>()))
                   .Returns(Task.FromResult(new LabelTranslationReadListResponse { Status = ResponseStatus.Failed, ErrorMessages = new List<string> { StringOne } }));
        }

        public static void Setup_GetLabelsWithTranslations_Returns_AllLabelReadListResponse_Invalid(this Mock<ILabelService> service)
        {
            service.Setup(x => x.GetLabelsWithTranslations(It.IsAny<AllLabelReadListRequest>()))
                   .Returns(Task.FromResult(new AllLabelReadListResponse { Status = ResponseStatus.Invalid, ErrorMessages = new List<string> { StringOne } }));
        }

        public static void Setup_GetLabels_Returns_LabelReadListResponse_Invalid(this Mock<ILabelService> service)
        {
            service.Setup(x => x.GetLabels(It.IsAny<LabelReadListRequest>()))
                   .Returns(Task.FromResult(new LabelReadListResponse { Status = ResponseStatus.Invalid, ErrorMessages = new List<string> { StringOne } }));
        }

        public static void Setup_GetLabel_Returns_LabelReadResponse_Invalid(this Mock<ILabelService> service)
        {
            service.Setup(x => x.GetLabel(It.IsAny<LabelReadRequest>()))
                   .ReturnsAsync(new LabelReadResponse { Status = ResponseStatus.Invalid, ErrorMessages = new List<string> { StringOne } });
        }

        public static void Setup_CreateLabel_Returns_LabelCreateResponse_Invalid(this Mock<ILabelService> service)
        {
            service.Setup(x => x.CreateLabel(It.IsAny<LabelCreateRequest>()))
                   .Returns(Task.FromResult(new LabelCreateResponse { Status = ResponseStatus.Invalid, ErrorMessages = new List<string> { StringOne } }));
        }

        public static void Setup_CreateLabelFromList_Returns_LabelCreateListResponse_Invalid(this Mock<ILabelService> service)
        {
            service.Setup(x => x.CreateLabelFromList(It.IsAny<LabelCreateListRequest>()))
                   .Returns(Task.FromResult(new LabelCreateListResponse { Status = ResponseStatus.Invalid, ErrorMessages = new List<string> { StringOne } }));
        }

        public static void Setup_EditLabel_Returns_LabelEditResponse_Invalid(this Mock<ILabelService> service)
        {
            service.Setup(x => x.EditLabel(It.IsAny<LabelEditRequest>()))
                   .Returns(Task.FromResult(new LabelEditResponse { Status = ResponseStatus.Invalid, ErrorMessages = new List<string> { StringOne } }));
        }

        public static void Setup_DeleteLabel_Returns_LabelDeleteResponse_Invalid(this Mock<ILabelService> service)
        {
            service.Setup(x => x.DeleteLabel(It.IsAny<LabelDeleteRequest>()))
                   .Returns(Task.FromResult(new LabelDeleteResponse { Status = ResponseStatus.Invalid, ErrorMessages = new List<string> { StringOne } }));
        }

        public static void Setup_CloneLabel_Returns_LabelCloneResponse_Invalid(this Mock<ILabelService> service)
        {
            service.Setup(x => x.CloneLabel(It.IsAny<LabelCloneRequest>()))
                   .Returns(Task.FromResult(new LabelCloneResponse { Status = ResponseStatus.Invalid, ErrorMessages = new List<string> { StringOne } }));
        }

        public static void Setup_GetTranslations_Returns_LabelTranslationReadListResponse_Invalid(this Mock<ILabelService> service)
        {
            service.Setup(x => x.GetTranslations(It.IsAny<LabelTranslationReadListRequest>()))
                   .Returns(Task.FromResult(new LabelTranslationReadListResponse { Status = ResponseStatus.Invalid, ErrorMessages = new List<string> { StringOne } }));
        }

        public static void Setup_CreateTranslation_Returns_LabelTranslationCreateResponse_Invalid(this Mock<ILabelService> service)
        {
            service.Setup(x => x.CreateTranslation(It.IsAny<LabelTranslationCreateRequest>()))
                   .Returns(Task.FromResult(new LabelTranslationCreateResponse { Status = ResponseStatus.Invalid, ErrorMessages = new List<string> { StringOne } }));
        }

        public static void Setup_CreateTranslationFromList_Returns_LabelTranslationCreateListResponse_Invalid(this Mock<ILabelService> service)
        {
            service.Setup(x => x.CreateTranslationFromList(It.IsAny<LabelTranslationCreateListRequest>()))
                   .Returns(Task.FromResult(new LabelTranslationCreateListResponse { Status = ResponseStatus.Invalid, ErrorMessages = new List<string> { StringOne } }));
        }

        public static void Setup_EditTranslation_Returns_LabelTranslationEditResponse_Invalid(this Mock<ILabelService> service)
        {
            service.Setup(x => x.EditTranslation(It.IsAny<LabelTranslationEditRequest>()))
                   .Returns(Task.FromResult(new LabelTranslationEditResponse { Status = ResponseStatus.Invalid, ErrorMessages = new List<string> { StringOne } }));
        }

        public static void Setup_DeleteTranslation_Returns_LabelTranslationDeleteResponse_Invalid(this Mock<ILabelService> service)
        {
            service.Setup(x => x.DeleteTranslation(It.IsAny<LabelTranslationDeleteRequest>()))
                   .Returns(Task.FromResult(new LabelTranslationDeleteResponse { Status = ResponseStatus.Invalid, ErrorMessages = new List<string> { StringOne } }));
        }

        public static void Setup_ChangeActivation_Returns_LabelChangeActivationResponse_Invalid(this Mock<ILabelService> service)
        {
            service.Setup(x => x.ChangeActivation(It.IsAny<LabelChangeActivationRequest>()))
                   .Returns(Task.FromResult(new LabelChangeActivationResponse { Status = ResponseStatus.Invalid, ErrorMessages = new List<string> { StringOne } }));
        }

        public static void Setup_GetTranslation_Returns_LabelTranslationReadListResponse_Invalid(this Mock<ILabelService> service)
        {
            service.Setup(x => x.GetTranslation(It.IsAny<LabelTranslationReadRequest>()))
                   .Returns(Task.FromResult(new LabelTranslationReadListResponse { Status = ResponseStatus.Invalid, ErrorMessages = new List<string> { StringOne } }));
        }

        public static void Verify_GetLabelsWithTranslations(this Mock<ILabelService> service)
        {
            service.Verify(x => x.GetLabelsWithTranslations(It.IsAny<AllLabelReadListRequest>()));
        }

        public static void Verify_GetLabels(this Mock<ILabelService> service)
        {
            service.Verify(x => x.GetLabels(It.IsAny<LabelReadListRequest>()));
        }

        public static void Verify_GetLabel(this Mock<ILabelService> service)
        {
            service.Verify(x => x.GetLabel(It.IsAny<LabelReadRequest>()));
        }

        public static void Verify_CreateLabel(this Mock<ILabelService> service)
        {
            service.Verify(x => x.CreateLabel(It.IsAny<LabelCreateRequest>()));
        }

        public static void Verify_CreateLabelFromList(this Mock<ILabelService> service)
        {
            service.Verify(x => x.CreateLabelFromList(It.IsAny<LabelCreateListRequest>()));
        }

        public static void Verify_EditLabel(this Mock<ILabelService> service)
        {
            service.Verify(x => x.EditLabel(It.IsAny<LabelEditRequest>()));
        }

        public static void Verify_DeleteLabel(this Mock<ILabelService> service)
        {
            service.Verify(x => x.DeleteLabel(It.IsAny<LabelDeleteRequest>()));
        }

        public static void Verify_CloneLabel(this Mock<ILabelService> service)
        {
            service.Verify(x => x.CloneLabel(It.IsAny<LabelCloneRequest>()));
        }

        public static void Verify_GetTranslations(this Mock<ILabelService> service)
        {
            service.Verify(x => x.GetTranslations(It.IsAny<LabelTranslationReadListRequest>()));
        }

        public static void Verify_CreateTranslation(this Mock<ILabelService> service)
        {
            service.Verify(x => x.CreateTranslation(It.IsAny<LabelTranslationCreateRequest>()));
        }

        public static void Verify_CreateTranslationFromList(this Mock<ILabelService> service)
        {
            service.Verify(x => x.CreateTranslationFromList(It.IsAny<LabelTranslationCreateListRequest>()));
        }

        public static void Verify_EditTranslation(this Mock<ILabelService> service)
        {
            service.Verify(x => x.EditTranslation(It.IsAny<LabelTranslationEditRequest>()));
        }

        public static void Verify_DeleteTranslation(this Mock<ILabelService> service)
        {
            service.Verify(x => x.DeleteTranslation(It.IsAny<LabelTranslationDeleteRequest>()));
        }

        public static void Verify_ChangeActivation(this Mock<ILabelService> service)
        {
            service.Verify(x => x.ChangeActivation(It.IsAny<LabelChangeActivationRequest>()));
        }

        public static void Verify_GetTranslation(this Mock<ILabelService> service)
        {
            service.Verify(x => x.GetTranslation(It.IsAny<LabelTranslationReadRequest>()));
        }
    }
}