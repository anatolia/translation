using System.Collections.Generic;

using Moq;
using StandardUtils.Enumerations;
using StandardUtils.Models.DataTransferObjects;

using Translation.Common.Contracts;
using Translation.Common.Models.DataTransferObjects;
using Translation.Common.Models.Requests.Language;
using Translation.Common.Models.Responses.Language;
using static Translation.Common.Tests.TestHelpers.FakeDtoTestHelper;
using static Translation.Common.Tests.TestHelpers.FakeConstantTestHelper;

namespace Translation.Client.Web.Unit.Tests.ServiceSetupHelpers
{
    public static class LanguageServiceSetupHelper
    {
        public static void Setup_RestoreLanguage_Returns_LanguageRestoreResponse_Success(this Mock<ILanguageService> service)
        {
            service.Setup(x => x.RestoreLanguage(It.IsAny<LanguageRestoreRequest>()))
                .ReturnsAsync(new LanguageRestoreResponse { Status = ResponseStatus.Success });
        }

        public static void Setup_GetLanguageRevisions_Returns_LanguageRevisionReadListResponse_Success(this Mock<ILanguageService> service)
        {
            var items = new List<RevisionDto<LanguageDto>>();
            items.Add(new RevisionDto<LanguageDto>() { RevisionedByUid = UidOne, Revision = One, Item = new LanguageDto() { Uid = UidOne } });

            service.Setup(x => x.GetLanguageRevisions(It.IsAny<LanguageRevisionReadListRequest>()))
                   .ReturnsAsync(new LanguageRevisionReadListResponse() { Status = ResponseStatus.Success, Items = items });
        }

        public static void Setup_GetLanguage_Returns_LanguageReadResponse_Success(this Mock<ILanguageService> service)
        {
            service.Setup(x => x.GetLanguage(It.IsAny<LanguageReadRequest>()))
                   .ReturnsAsync(new LanguageReadResponse { Status = ResponseStatus.Success, Item = new LanguageDto() { IsoCode2 = IsoCode2One } });
        }

        public static void Setup_GetLanguages_Returns_LanguageReadListResponse_Success(this Mock<ILanguageService> service)
        {
            var items = new List<LanguageDto>() { GetLanguageDtoOne(), GetLanguageDtoTwo() };
            service.Setup(x => x.GetLanguages(It.IsAny<LanguageReadListRequest>()))
                   .ReturnsAsync(new LanguageReadListResponse { Status = ResponseStatus.Success, Items = items });
        }

        public static void Setup_CreateLanguage_Returns_LanguageCreateResponse_Success(this Mock<ILanguageService> service)
        {
            service.Setup(x => x.CreateLanguage(It.IsAny<LanguageCreateRequest>()))
                   .ReturnsAsync(new LanguageCreateResponse { Status = ResponseStatus.Success });
        }

        public static void Setup_EditLanguage_Returns_LanguageEditResponse_Success(this Mock<ILanguageService> service)
        {
            var item = GetLanguageDtoOne();
            service.Setup(x => x.EditLanguage(It.IsAny<LanguageEditRequest>()))
                   .ReturnsAsync(new LanguageEditResponse { Status = ResponseStatus.Success, Item = item });
        }

        public static void Setup_DeleteLanguage_Returns_LanguageDeleteResponse_Success(this Mock<ILanguageService> service)
        {
            service.Setup(x => x.DeleteLanguage(It.IsAny<LanguageDeleteRequest>()))
                   .ReturnsAsync(new LanguageDeleteResponse { Status = ResponseStatus.Success });
        }

        public static void Setup_RestoreLanguage_Returns_LanguageRestoreResponse_Failed(this Mock<ILanguageService> service)
        {
            service.Setup(x => x.RestoreLanguage(It.IsAny<LanguageRestoreRequest>()))
                   .ReturnsAsync(new LanguageRestoreResponse { Status = ResponseStatus.Failed, ErrorMessages = new List<string> { StringOne } });
        }

        public static void Setup_GetLanguageRevisions_Returns_LanguageRevisionReadListResponse_Failed(this Mock<ILanguageService> service)
        {
            var items = new List<RevisionDto<LanguageDto>>();
            items.Add(new RevisionDto<LanguageDto>() { RevisionedByUid = UidOne });

            service.Setup(x => x.GetLanguageRevisions(It.IsAny<LanguageRevisionReadListRequest>()))
                   .ReturnsAsync(new LanguageRevisionReadListResponse() { Status = ResponseStatus.Failed, ErrorMessages = new List<string> { StringOne } });
        }

        public static void Setup_GetLanguage_Returns_LanguageReadResponse_Failed(this Mock<ILanguageService> service)
        {
            service.Setup(x => x.GetLanguage(It.IsAny<LanguageReadRequest>()))
                   .ReturnsAsync(new LanguageReadResponse { Status = ResponseStatus.Failed, ErrorMessages = new List<string> { StringOne } });
        }

        public static void Setup_GetLanguages_Returns_LanguageReadListResponse_Failed(this Mock<ILanguageService> service)
        {
            service.Setup(x => x.GetLanguages(It.IsAny<LanguageReadListRequest>()))
                   .ReturnsAsync(new LanguageReadListResponse { Status = ResponseStatus.Failed, ErrorMessages = new List<string> { StringOne } });
        }

        public static void Setup_GetLanguages_Returns_SourceLanguageReadResponse_Failed(this Mock<ILanguageService> service)
        {
            var items = new List<LanguageDto>() { GetLanguageDtoOne() };
            service.Setup(x => x.GetLanguages(It.IsAny<LanguageReadListRequest>()))
                .ReturnsAsync(new LanguageReadListResponse { Status = ResponseStatus.Failed, ErrorMessages = new List<string> { StringOne }, Items = items });
        }

        public static void Setup_CreateLanguage_Returns_LanguageCreateResponse_Failed(this Mock<ILanguageService> service)
        {
            service.Setup(x => x.CreateLanguage(It.IsAny<LanguageCreateRequest>()))
                   .ReturnsAsync(new LanguageCreateResponse { Status = ResponseStatus.Failed, ErrorMessages = new List<string> { StringOne } });
        }

        public static void Setup_EditLanguage_Returns_LanguageEditResponse_Failed(this Mock<ILanguageService> service)
        {
            service.Setup(x => x.EditLanguage(It.IsAny<LanguageEditRequest>()))
                   .ReturnsAsync(new LanguageEditResponse { Status = ResponseStatus.Failed, ErrorMessages = new List<string> { StringOne } });
        }

        public static void Setup_DeleteLanguage_Returns_LanguageDeleteResponse_Failed(this Mock<ILanguageService> service)
        {
            service.Setup(x => x.DeleteLanguage(It.IsAny<LanguageDeleteRequest>()))
                   .ReturnsAsync(new LanguageDeleteResponse { Status = ResponseStatus.Failed, ErrorMessages = new List<string> { StringOne } });
        }

        public static void Setup_RestoreLanguage_Returns_LanguageRestoreResponse_Invalid(this Mock<ILanguageService> service)
        {
            service.Setup(x => x.RestoreLanguage(It.IsAny<LanguageRestoreRequest>()))
                   .ReturnsAsync(new LanguageRestoreResponse { Status = ResponseStatus.Invalid, ErrorMessages = new List<string> { StringOne } });
        }

        public static void Setup_GetLanguageRevisions_Returns_LanguageRevisionReadListResponse_Invalid(this Mock<ILanguageService> service)
        {
            var items = new List<RevisionDto<LanguageDto>>();
            items.Add(new RevisionDto<LanguageDto>() { RevisionedByUid = UidOne });

            service.Setup(x => x.GetLanguageRevisions(It.IsAny<LanguageRevisionReadListRequest>()))
                   .ReturnsAsync(new LanguageRevisionReadListResponse() { Status = ResponseStatus.Invalid, ErrorMessages = new List<string> { StringOne } });
        }

        public static void Setup_GetLanguage_Returns_LanguageReadResponse_Invalid(this Mock<ILanguageService> service)
        {
            service.Setup(x => x.GetLanguage(It.IsAny<LanguageReadRequest>()))
                   .ReturnsAsync(new LanguageReadResponse { Status = ResponseStatus.Invalid, ErrorMessages = new List<string> { StringOne } });
        }

        public static void Setup_GetLanguages_Returns_LanguageReadListResponse_Invalid(this Mock<ILanguageService> service)
        {
            service.Setup(x => x.GetLanguages(It.IsAny<LanguageReadListRequest>()))
                   .ReturnsAsync(new LanguageReadListResponse { Status = ResponseStatus.Invalid, ErrorMessages = new List<string> { StringOne } });
        }

        public static void Setup_GetLanguages_Returns_SourceLanguageReadResponse_Invalid(this Mock<ILanguageService> service)
        {
            var items = new List<LanguageDto>() { GetLanguageDtoOne() };
            service.Setup(x => x.GetLanguages(It.IsAny<LanguageReadListRequest>()))
                   .ReturnsAsync(new LanguageReadListResponse { Status = ResponseStatus.Invalid, ErrorMessages = new List<string> { StringOne }, Items = items });
        }

        public static void Setup_CreateLanguage_Returns_LanguageCreateResponse_Invalid(this Mock<ILanguageService> service)
        {
            service.Setup(x => x.CreateLanguage(It.IsAny<LanguageCreateRequest>()))
                   .ReturnsAsync(new LanguageCreateResponse { Status = ResponseStatus.Invalid, ErrorMessages = new List<string> { StringOne } });
        }

        public static void Setup_EditLanguage_Returns_LanguageEditResponse_Invalid(this Mock<ILanguageService> service)
        {
            service.Setup(x => x.EditLanguage(It.IsAny<LanguageEditRequest>()))
                   .ReturnsAsync(new LanguageEditResponse { Status = ResponseStatus.Invalid, ErrorMessages = new List<string> { StringOne } });
        }

        public static void Setup_DeleteLanguage_Returns_LanguageDeleteResponse_Invalid(this Mock<ILanguageService> service)
        {
            service.Setup(x => x.DeleteLanguage(It.IsAny<LanguageDeleteRequest>()))
                   .ReturnsAsync(new LanguageDeleteResponse { Status = ResponseStatus.Invalid, ErrorMessages = new List<string> { StringOne } });
        }

        public static void Verify_RestoreLanguage(this Mock<ILanguageService> service)
        {
            service.Verify(x => x.RestoreLanguage(It.IsAny<LanguageRestoreRequest>()));
        }

        public static void Verify_GetLanguageRevisions(this Mock<ILanguageService> service)
        {
            service.Verify(x => x.GetLanguageRevisions(It.IsAny<LanguageRevisionReadListRequest>()));
        }

        public static void Verify_GetLanguage(this Mock<ILanguageService> service)
        {
            service.Verify(x => x.GetLanguage(It.IsAny<LanguageReadRequest>()));
        }

        public static void Verify_GetLanguages(this Mock<ILanguageService> service)
        {
            service.Verify(x => x.GetLanguages(It.IsAny<LanguageReadListRequest>()));
        }

        public static void Verify_CreateLanguage(this Mock<ILanguageService> service)
        {
            service.Verify(x => x.CreateLanguage(It.IsAny<LanguageCreateRequest>()));
        }

        public static void Verify_EditLanguage(this Mock<ILanguageService> service)
        {
            service.Verify(x => x.EditLanguage(It.IsAny<LanguageEditRequest>()));
        }

        public static void Verify_DeleteLanguage(this Mock<ILanguageService> service)
        {
            service.Verify(x => x.DeleteLanguage(It.IsAny<LanguageDeleteRequest>()));
        }
    }
}