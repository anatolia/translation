using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;

using Translation.Common.Contracts;
using Translation.Common.Enumerations;
using Translation.Common.Models.DataTransferObjects;
using Translation.Common.Models.Requests.Project;
using Translation.Common.Models.Responses.Project;
using static Translation.Tests.TestHelpers.FakeConstantTestHelper;

namespace Translation.Tests.SetupHelpers
{
    public static class ProjectServiceSetupHelper
    {
        public static void Setup_GetProjectBySlug_Returns_ProjectReadBySlugResponse_Success(this Mock<IProjectService> service)
        {
            service.Setup(x => x.GetProjectBySlug(It.IsAny<ProjectReadBySlugRequest>()))
                   .ReturnsAsync(new ProjectReadBySlugResponse() { Status = ResponseStatus.Success,Item = new  ProjectDto(){Name = StringOne}});
        }

        public static void Setup_GetProjectBySlug_Returns_ProjectReadBySlugResponse_Failed(this Mock<IProjectService> service)
        {
            service.Setup(x => x.GetProjectBySlug(It.IsAny<ProjectReadBySlugRequest>()))
                   .ReturnsAsync(new ProjectReadBySlugResponse() { Status = ResponseStatus.Failed });
        }

        public static void Setup_GetProjectBySlug_Returns_ProjectReadBySlugResponse_Invalid(this Mock<IProjectService> service)
        {
            service.Setup(x => x.GetProjectBySlug(It.IsAny<ProjectReadBySlugRequest>()))
                   .ReturnsAsync(new ProjectReadBySlugResponse() { Status = ResponseStatus.Invalid });
        }

        public static void Verify_GetProjectBySlug(this Mock<IProjectService> service)
        {
            service.Verify(x => x.GetProjectBySlug(It.IsAny<ProjectReadBySlugRequest>()));
        }

        public static void Setup_GetProjectRevisions_Returns_ProjectRevisionReadListResponse_Success(this Mock<IProjectService> service)
        {
            var items = new List<RevisionDto<ProjectDto>>();
            items.Add(new RevisionDto<ProjectDto>() { RevisionedByUid = UidOne, Revision = One, Item = new ProjectDto() { Uid = UidOne } });

            service.Setup(x => x.GetProjectRevisions(It.IsAny<ProjectRevisionReadListRequest>()))
                   .Returns(Task.FromResult(new ProjectRevisionReadListResponse() { Status = ResponseStatus.Success, Items = items }));
        }

        public static void Setup_GetPendingTranslations_Returns_ProjectPendingTranslationReadListResponse_Success(this Mock<IProjectService> service)
        {
            var items = new List<LabelDto>();
            items.Add(new LabelDto() { Uid = UidOne });

            service.Setup(x => x.GetPendingTranslations(It.IsAny<ProjectPendingTranslationReadListRequest>()))
                   .Returns(Task.FromResult(new ProjectPendingTranslationReadListResponse { Status = ResponseStatus.Success, Items = items }));
        }

        public static void Setup_GetProjects_Returns_ProjectReadListResponse_Success(this Mock<IProjectService> service)
        {
            service.Setup(x => x.GetProjects(It.IsAny<ProjectReadListRequest>()))
                   .Returns(Task.FromResult(new ProjectReadListResponse { Status = ResponseStatus.Success }));
        }

        public static void Setup_GetProject_Returns_ProjectReadResponse_Success(this Mock<IProjectService> service)
        {
            service.Setup(x => x.GetProject(It.IsAny<ProjectReadRequest>()))
                   .ReturnsAsync(new ProjectReadResponse { Status = ResponseStatus.Success });
        }

        public static void Setup_CreateProject_Returns_ProjectCreateResponse_Success(this Mock<IProjectService> service)
        {
            service.Setup(x => x.CreateProject(It.IsAny<ProjectCreateRequest>()))
                   .Returns(Task.FromResult(new ProjectCreateResponse { Status = ResponseStatus.Success }));
        }

        public static void Setup_EditProject_Returns_ProjectEditResponse_Success(this Mock<IProjectService> service)
        {
            service.Setup(x => x.EditProject(It.IsAny<ProjectEditRequest>()))
                   .Returns(Task.FromResult(new ProjectEditResponse { Status = ResponseStatus.Success }));
        }

        public static void Setup_DeleteProject_Returns_ProjectDeleteResponse_Success(this Mock<IProjectService> service)
        {
            service.Setup(x => x.DeleteProject(It.IsAny<ProjectDeleteRequest>()))
                   .Returns(Task.FromResult(new ProjectDeleteResponse { Status = ResponseStatus.Success }));
        }

        public static void Setup_CloneProject_Returns_ProjectCloneResponse_Success(this Mock<IProjectService> service)
        {
            service.Setup(x => x.CloneProject(It.IsAny<ProjectCloneRequest>()))
                   .Returns(Task.FromResult(new ProjectCloneResponse { Status = ResponseStatus.Success }));
        }

        public static void Setup_RestoreProject_Returns_ProjectRestoreResponse_Success(this Mock<IProjectService> service)
        {
            service.Setup(x => x.RestoreProject(It.IsAny<ProjectRestoreRequest>()))
                   .Returns(Task.FromResult(new ProjectRestoreResponse { Status = ResponseStatus.Success }));
        }

        public static void Setup_ChangeActivationForProject_Returns_ProjectChangeActivationResponse_Success(this Mock<IProjectService> service)
        {
            service.Setup(x => x.ChangeActivationForProject(It.IsAny<ProjectChangeActivationRequest>()))
                   .Returns(Task.FromResult(new ProjectChangeActivationResponse { Status = ResponseStatus.Success }));
        }

        public static void Setup_RestoreProject_Returns_ProjectRestoreResponse_Failed(this Mock<IProjectService> service)
        {
            service.Setup(x => x.RestoreProject(It.IsAny<ProjectRestoreRequest>()))
                   .Returns(Task.FromResult(new ProjectRestoreResponse { Status = ResponseStatus.Failed }));
        }

        public static void Setup_GetProjectRevisions_Returns_ProjectRevisionReadListResponse_Failed(this Mock<IProjectService> service)
        {
            service.Setup(x => x.GetProjectRevisions(It.IsAny<ProjectRevisionReadListRequest>()))
                   .Returns(Task.FromResult(new ProjectRevisionReadListResponse() { Status = ResponseStatus.Failed, ErrorMessages = new List<string> { StringOne } }));
        }

        public static void Setup_GetPendingTranslations_Returns_ProjectPendingTranslationReadListResponse_Failed(this Mock<IProjectService> service)
        {
            service.Setup(x => x.GetPendingTranslations(It.IsAny<ProjectPendingTranslationReadListRequest>()))
                   .Returns(Task.FromResult(new ProjectPendingTranslationReadListResponse { Status = ResponseStatus.Failed, ErrorMessages = new List<string> { StringOne } }));
        }

        public static void Setup_GetProjects_Returns_ProjectReadListResponse_Failed(this Mock<IProjectService> service)
        {
            service.Setup(x => x.GetProjects(It.IsAny<ProjectReadListRequest>()))
                   .Returns(Task.FromResult(new ProjectReadListResponse { Status = ResponseStatus.Failed, ErrorMessages = new List<string> { StringOne } }));
        }

        public static void Setup_GetProject_Returns_ProjectReadResponse_Failed(this Mock<IProjectService> service)
        {
            service.Setup(x => x.GetProject(It.IsAny<ProjectReadRequest>()))
                   .ReturnsAsync(new ProjectReadResponse { Status = ResponseStatus.Failed, ErrorMessages = new List<string> { StringOne } });
        }

        public static void Setup_CreateProject_Returns_ProjectCreateResponse_Failed(this Mock<IProjectService> service)
        {
            service.Setup(x => x.CreateProject(It.IsAny<ProjectCreateRequest>()))
                   .Returns(Task.FromResult(new ProjectCreateResponse { Status = ResponseStatus.Failed, ErrorMessages = new List<string> { StringOne } }));
        }

        public static void Setup_EditProject_Returns_ProjectEditResponse_Failed(this Mock<IProjectService> service)
        {
            service.Setup(x => x.EditProject(It.IsAny<ProjectEditRequest>()))
                   .Returns(Task.FromResult(new ProjectEditResponse { Status = ResponseStatus.Failed, ErrorMessages = new List<string> { StringOne } }));
        }

        public static void Setup_DeleteProject_Returns_ProjectDeleteResponse_Failed(this Mock<IProjectService> service)
        {
            service.Setup(x => x.DeleteProject(It.IsAny<ProjectDeleteRequest>()))
                   .Returns(Task.FromResult(new ProjectDeleteResponse { Status = ResponseStatus.Failed, ErrorMessages = new List<string> { StringOne } }));
        }

        public static void Setup_CloneProject_Returns_ProjectCloneResponse_Failed(this Mock<IProjectService> service)
        {
            service.Setup(x => x.CloneProject(It.IsAny<ProjectCloneRequest>()))
                   .Returns(Task.FromResult(new ProjectCloneResponse { Status = ResponseStatus.Failed, ErrorMessages = new List<string> { StringOne } }));
        }

        public static void Setup_ChangeActivationForProject_Returns_ProjectChangeActivationResponse_Failed(this Mock<IProjectService> service)
        {
            service.Setup(x => x.ChangeActivationForProject(It.IsAny<ProjectChangeActivationRequest>()))
                   .Returns(Task.FromResult(new ProjectChangeActivationResponse { Status = ResponseStatus.Failed, ErrorMessages = new List<string> { StringOne } }));
        }

        public static void Setup_RestoreProject_Returns_ProjectRestoreResponse_Invalid(this Mock<IProjectService> service)
        {
            service.Setup(x => x.RestoreProject(It.IsAny<ProjectRestoreRequest>()))
                   .Returns(Task.FromResult(new ProjectRestoreResponse { Status = ResponseStatus.Invalid }));
        }

        public static void Setup_GetProjectRevisions_Returns_ProjectRevisionReadListResponse_Invalid(this Mock<IProjectService> service)
        {
            service.Setup(x => x.GetProjectRevisions(It.IsAny<ProjectRevisionReadListRequest>()))
                   .Returns(Task.FromResult(new ProjectRevisionReadListResponse() { Status = ResponseStatus.Invalid, ErrorMessages = new List<string> { StringOne } }));
        }

        public static void Setup_GetPendingTranslations_Returns_ProjectPendingTranslationReadListResponse_Invalid(this Mock<IProjectService> service)
        {
            var items = new List<LabelDto>();
            items.Add(new LabelDto() { Uid = UidOne });

            service.Setup(x => x.GetPendingTranslations(It.IsAny<ProjectPendingTranslationReadListRequest>()))
                   .Returns(Task.FromResult(new ProjectPendingTranslationReadListResponse { Status = ResponseStatus.Invalid, ErrorMessages = new List<string> { StringOne } }));
        }

        public static void Setup_GetProjects_Returns_ProjectReadListResponse_Invalid(this Mock<IProjectService> service)
        {
            service.Setup(x => x.GetProjects(It.IsAny<ProjectReadListRequest>()))
                   .Returns(Task.FromResult(new ProjectReadListResponse { Status = ResponseStatus.Invalid, ErrorMessages = new List<string> { StringOne } }));
        }

        public static void Setup_GetProject_Returns_ProjectReadResponse_Invalid(this Mock<IProjectService> service)
        {
            service.Setup(x => x.GetProject(It.IsAny<ProjectReadRequest>()))
                   .ReturnsAsync(new ProjectReadResponse { Status = ResponseStatus.Invalid, ErrorMessages = new List<string> { StringOne } });
        }

        public static void Setup_CreateProject_Returns_ProjectCreateResponse_Invalid(this Mock<IProjectService> service)
        {
            service.Setup(x => x.CreateProject(It.IsAny<ProjectCreateRequest>()))
                   .Returns(Task.FromResult(new ProjectCreateResponse { Status = ResponseStatus.Invalid, ErrorMessages = new List<string> { StringOne } }));
        }

        public static void Setup_EditProject_Returns_ProjectEditResponse_Invalid(this Mock<IProjectService> service)
        {
            service.Setup(x => x.EditProject(It.IsAny<ProjectEditRequest>()))
                   .Returns(Task.FromResult(new ProjectEditResponse { Status = ResponseStatus.Invalid, ErrorMessages = new List<string> { StringOne } }));
        }

        public static void Setup_DeleteProject_Returns_ProjectDeleteResponse_Invalid(this Mock<IProjectService> service)
        {
            service.Setup(x => x.DeleteProject(It.IsAny<ProjectDeleteRequest>()))
                   .Returns(Task.FromResult(new ProjectDeleteResponse { Status = ResponseStatus.Invalid, ErrorMessages = new List<string> { StringOne } }));
        }

        public static void Setup_CloneProject_Returns_ProjectCloneResponse_Invalid(this Mock<IProjectService> service)
        {
            service.Setup(x => x.CloneProject(It.IsAny<ProjectCloneRequest>()))
                   .Returns(Task.FromResult(new ProjectCloneResponse { Status = ResponseStatus.Invalid, ErrorMessages = new List<string> { StringOne } }));
        }

        public static void Setup_ChangeActivationForProject_Returns_ProjectChangeActivationResponse_Invalid(this Mock<IProjectService> service)
        {
            service.Setup(x => x.ChangeActivationForProject(It.IsAny<ProjectChangeActivationRequest>()))
                   .Returns(Task.FromResult(new ProjectChangeActivationResponse { Status = ResponseStatus.Invalid, ErrorMessages = new List<string> { StringOne } }));
        }

        public static void Verify_RestoreProject(this Mock<IProjectService> service)
        {
            service.Verify(x => x.RestoreProject(It.IsAny<ProjectRestoreRequest>()));

        }

        public static void Verify_GetProjectRevisions(this Mock<IProjectService> service)
        {
            service.Verify(x => x.GetProjectRevisions(It.IsAny<ProjectRevisionReadListRequest>()));
        }

        public static void Verify_GetPendingTranslations(this Mock<IProjectService> service)
        {
            var items = new List<LabelDto>();
            items.Add(new LabelDto() { Uid = UidOne });

            service.Verify(x => x.GetPendingTranslations(It.IsAny<ProjectPendingTranslationReadListRequest>()));

        }

        public static void Verify_GetProjects(this Mock<IProjectService> service)
        {
            service.Verify(x => x.GetProjects(It.IsAny<ProjectReadListRequest>()));
        }

        public static void Verify_GetProject(this Mock<IProjectService> service)
        {
            service.Verify(x => x.GetProject(It.IsAny<ProjectReadRequest>()));
        }

        public static void Verify_CreateProject(this Mock<IProjectService> service)
        {
            service.Verify(x => x.CreateProject(It.IsAny<ProjectCreateRequest>()));
        }

        public static void Verify_EditProject(this Mock<IProjectService> service)
        {
            service.Verify(x => x.EditProject(It.IsAny<ProjectEditRequest>()));
        }

        public static void Verify_DeleteProject(this Mock<IProjectService> service)
        {
            service.Verify(x => x.DeleteProject(It.IsAny<ProjectDeleteRequest>()));
        }

        public static void Verify_CloneProject(this Mock<IProjectService> service)
        {
            service.Verify(x => x.CloneProject(It.IsAny<ProjectCloneRequest>()));
        }

        public static void Verify_ChangeActivationForProject(this Mock<IProjectService> service)
        {
            service.Verify(x => x.ChangeActivationForProject(It.IsAny<ProjectChangeActivationRequest>()));
        }
    }
}