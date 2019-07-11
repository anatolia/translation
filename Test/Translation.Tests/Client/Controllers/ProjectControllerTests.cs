﻿using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using NUnit.Framework;
using Shouldly;

using Translation.Client.Web.Controllers;
using Translation.Client.Web.Models.Base;
using Translation.Client.Web.Models.Project;
using Translation.Tests.SetupHelpers;
using static Translation.Tests.TestHelpers.ActionMethodNameConstantTestHelper;
using static Translation.Tests.TestHelpers.FakeConstantTestHelper;
using static Translation.Tests.TestHelpers.AssertViewModelTestHelper;
using static Translation.Tests.TestHelpers.AssertModelTestHelper;
using static Translation.Tests.TestHelpers.FakeModelTestHelper;

namespace Translation.Tests.Client.Controllers
{
    [TestFixture]
    public class ProjectControllerTests : ControllerBaseTests
    {
        public ProjectController SystemUnderTest { get; set; }

        [SetUp]
        public void run_before_every_test()
        {
            SystemUnderTest = Container.Resolve<ProjectController>();
            SetControllerContext(SystemUnderTest);
        }

        [TestCase(Create, new[] { typeof(Guid) }, typeof(HttpGetAttribute))]
        [TestCase(Create, new[] { typeof(ProjectCreateModel) }, typeof(HttpPostAttribute))]
        [TestCase(Detail, new[] { typeof(Guid) }, typeof(HttpGetAttribute))]
        [TestCase(Edit, new[] { typeof(Guid) }, typeof(HttpGetAttribute))]
        [TestCase(Edit, new[] { typeof(ProjectEditModel) }, typeof(HttpPostAttribute))]
        [TestCase(Delete, new[] { typeof(Guid) }, typeof(HttpPostAttribute))]
        [TestCase(Clone, new[] { typeof(Guid) }, typeof(HttpGetAttribute))]
        [TestCase(Clone, new[] { typeof(ProjectCloneModel) }, typeof(HttpPostAttribute))]
        [TestCase(SelectData, new Type[] { }, typeof(HttpGetAttribute))]
        [TestCase(PendingTranslations, new[] { typeof(Guid) }, typeof(HttpGetAttribute))]
        [TestCase(PendingTranslationsData, new[] { typeof(Guid), typeof(int), typeof(int) }, typeof(HttpGetAttribute))]
        [TestCase(LabelListData, new[] { typeof(Guid), typeof(int), typeof(int) }, typeof(HttpGetAttribute))]
        [TestCase(DownloadLabels, new[] { typeof(Guid) }, typeof(HttpPostAttribute))]
        [TestCase(ChangeActivation, new[] { typeof(Guid), typeof(Guid) }, typeof(HttpPostAttribute))]
        [TestCase(Revisions, new[] { typeof(Guid)}, typeof(HttpGetAttribute))]
        [TestCase(RevisionsData, new[] { typeof(Guid)}, typeof(HttpGetAttribute))]
        [TestCase(Restore, new[] { typeof(Guid), typeof(int)}, typeof(HttpPostAttribute))]
        public void Methods_Has_Http_Verb_Attributes(string actionMethod, Type[] parameters, Type httpVerbAttribute)
        {
            var type = SystemUnderTest.GetType();
            var methodInfo = type.GetMethod(actionMethod, parameters);
            var attributes = methodInfo.GetCustomAttributes(httpVerbAttribute, true);
            Assert.AreEqual(attributes.Length, 1);
        }

        [Test]
        public void Controller_Derived_From_BaseController()
        {
            var type = SystemUnderTest.GetType();
            type.BaseType.Name.StartsWith("BaseController").ShouldBeTrue();
        }

        [Test]
        public void Create_GET()
        {
            // arrange
            MockOrganizationService.Setup_GetOrganization_Returns_OrganizationReadResponse_Success();

            // act
            var result = SystemUnderTest.Create(OrganizationOneUid);

            // assert
            AssertViewWithModel<ProjectCreateModel>(result);
            MockOrganizationService.Verify_GetOrganization();
        }

        [Test]
        public void Create_GET_FailedResponse()
        {
            // arrange
            MockOrganizationService.Setup_GetOrganization_Returns_OrganizationReadResponse_Failed();

            // act
            var result = SystemUnderTest.Create(OrganizationOneUid);

            // assert
            AssertViewAccessDenied(result);
            MockOrganizationService.Verify_GetOrganization();
        }

        [Test]
        public void Create_GET_InvalidResponse()
        {
            // arrange
            MockOrganizationService.Setup_GetOrganization_Returns_OrganizationReadResponse_Invalid();

            // act
            var result = SystemUnderTest.Create(OrganizationOneUid);

            // assert
            AssertViewAccessDenied(result);
            MockOrganizationService.Verify_GetOrganization();
        }

        [Test]
        public void Create_GET_InvalidParameter()
        {
            // arrange
            MockOrganizationService.Setup_GetOrganization_Returns_OrganizationReadResponse_Success();

            // act
            var result = SystemUnderTest.Create(OrganizationOneUid);

            // assert
            AssertViewWithModel<ProjectCreateModel>(result);
            MockOrganizationService.Verify_GetOrganization();
        }

        [Test]
        public async Task Create_POST()
        {
            // arrange
            MockProjectService.Setup_CreateProject_Returns_ProjectCreateResponse_Success();
            var model = GetOrganizationOneProjectOneCreateModel();

            // act
            var result = await SystemUnderTest.Create(model);

            // assert
            ((RedirectResult)result).Url.ShouldBe($"/Project/Detail/{EmptyUid}");
            MockProjectService.Verify_CreateProject();
        }


        [Test]
        public async Task Create_POST_FailedResponse()
        {
            // arrange
            MockProjectService.Setup_CreateProject_Returns_ProjectCreateResponse_Failed();
            var model = GetOrganizationOneProjectOneCreateModel();

            // act
            var result = await SystemUnderTest.Create(model);

            // assert
            AssertErrorMessagesForInvalidOrFailedResponse<ProjectCreateModel>(result);
            MockProjectService.Verify_CreateProject();
        }


        [Test]
        public async Task Create_POST_InvalidResponse()
        {
            // arrange
            MockProjectService.Setup_CreateProject_Returns_ProjectCreateResponse_Invalid();
            var model = GetOrganizationOneProjectOneCreateModel();

            // act
            var result = await SystemUnderTest.Create(model);

            // assert
            AssertErrorMessagesForInvalidOrFailedResponse<ProjectCreateModel>(result);
            MockProjectService.Verify_CreateProject();
        }

        [Test]
        public async Task Create_POST_InvalidModel()
        {
            // arrange
            var model = new ProjectCreateModel();

            // act
            var result = await SystemUnderTest.Create(model);

            // assert
            AssertInputErrorMessagesOfView(result, model);
        }

        [Test]
        public async Task Detail_GET()
        {
            // arrange 
            MockProjectService.Setup_GetProject_Returns_ProjectReadResponse_Success();

            // act
            var result = await SystemUnderTest.Detail(OrganizationOneProjectOneUid);

            // assert
            AssertViewWithModel<ProjectDetailModel>(result);
            MockProjectService.Verify_GetProject();
        }

        [Test]
        public async Task Detail_GET_FailedResponse()
        {
            // arrange 
            MockProjectService.Setup_GetProject_Returns_ProjectReadResponse_Failed();

            // act
            var result = await SystemUnderTest.Detail(OrganizationOneProjectOneUid);

            // assert
            AssertViewAccessDenied(result);
            MockProjectService.Verify_GetProject();
        }

        [Test]
        public async Task Detail_GET_InvalidResponse()
        {
            // arrange 
            MockProjectService.Setup_GetProject_Returns_ProjectReadResponse_Invalid();

            // act
            var result = await SystemUnderTest.Detail(OrganizationOneProjectOneUid);

            // assert
            AssertViewAccessDenied(result);
            MockProjectService.Verify_GetProject();
        }

        [Test]
        public async Task Detail_GET_InvalidParameter()
        {
            // arrange

            // act
            var result = await SystemUnderTest.Detail(OrganizationOneProjectOneUid);

            // assert
            AssertViewAccessDenied(result);
        }

        [Test]
        public async Task Edit_GET()
        {
            // arrange 
            MockProjectService.Setup_GetProject_Returns_ProjectReadResponse_Success();

            // act
            var result = await SystemUnderTest.Edit(OrganizationOneProjectOneUid);

            // assert
            AssertViewWithModel<ProjectEditModel>(result);
            MockProjectService.Verify_GetProject();
        }

        [Test]
        public async Task Edit_GET_FailedResponse()
        {
            // arrange 
            MockProjectService.Setup_GetProject_Returns_ProjectReadResponse_Failed();

            // act
            var result = await SystemUnderTest.Edit(OrganizationOneProjectOneUid);

            // assert
            AssertViewAccessDenied(result);
            MockProjectService.Verify_GetProject();
        }

        [Test]
        public async Task Edit_GET_InvalidResponse()
        {
            // arrange 
            MockProjectService.Setup_GetProject_Returns_ProjectReadResponse_Invalid();

            // act
            var result = await SystemUnderTest.Edit(OrganizationOneProjectOneUid);

            // assert
            AssertViewAccessDenied(result);
            MockProjectService.Verify_GetProject();
        }

        [Test]
        public async Task Edit_GET_InvalidParameter()
        {
            // arrange

            // act
            var result = await SystemUnderTest.Edit(EmptyUid);

            // assert
            AssertViewAccessDenied(result);
        }

        [Test]
        public async Task Edit_POST()
        {
            // arrange 
            MockProjectService.Setup_EditProject_Returns_ProjectEditResponse_Success();
            var model = GetOrganizationOneProjectOneEditModel();

            // act
            var result = await SystemUnderTest.Edit(model);

            // assert
            ((RedirectResult)result).Url.ShouldBe($"/Project/Detail/{OrganizationOneProjectOneUid}");
            MockProjectService.Verify_EditProject();
        }

        [Test]
        public async Task Edit_POST_FailedResponse()
        {
            // arrange 
            MockProjectService.Setup_EditProject_Returns_ProjectEditResponse_Failed();
            var model = GetOrganizationOneProjectOneEditModel();

            // act
            var result = await SystemUnderTest.Edit(model);

            // assert
            AssertErrorMessagesForInvalidOrFailedResponse<ProjectEditModel>(result);
            MockProjectService.Verify_EditProject();
        }

        [Test]
        public async Task Edit_POST_InvalidResponse()
        {
            // arrange 
            MockProjectService.Setup_EditProject_Returns_ProjectEditResponse_Invalid();
            var model = GetOrganizationOneProjectOneEditModel();
            
            // act
            var result = await SystemUnderTest.Edit(model);

            // assert
            AssertErrorMessagesForInvalidOrFailedResponse<ProjectEditModel>(result);
            MockProjectService.Verify_EditProject();
        }

        [Test]
        public async Task Edit_POST_InvalidModel()
        {
            // arrange
            var model = new ProjectEditModel();

            // act
            var result = await SystemUnderTest.Edit(model);

            // assert
            AssertInputErrorMessagesOfView(result, model);
        }

        [Test]
        public async Task Delete_POST()
        {
            // arrange
            MockProjectService.Setup_DeleteProject_Returns_ProjectDeleteResponse_Success();

            // act
            var result = (JsonResult)await SystemUnderTest.Delete(OrganizationOneProjectOneUid);

            // assert
            ((CommonResult)result.Value).IsOk.ShouldBe(true);
            MockProjectService.Verify_DeleteProject();
        }

        [Test]
        public async Task Delete_POST_FailedResponse()
        {
            // arrange
            MockProjectService.Setup_DeleteProject_Returns_ProjectDeleteResponse_Failed();

            // act
            var result = await SystemUnderTest.Delete(OrganizationOneProjectOneUid);

            // assert
            var commonResult = ((CommonResult)((JsonResult)result).Value);
            commonResult.IsOk.ShouldBeFalse();
            commonResult.Messages.Any(x => x == StringOne);
            MockProjectService.Verify_DeleteProject();
        }

        [Test]
        public async Task Delete_POST_InvalidResponse()
        {
            // arrange
            MockProjectService.Setup_DeleteProject_Returns_ProjectDeleteResponse_Invalid();

            // act
            var result = await SystemUnderTest.Delete(OrganizationOneProjectOneUid);

            // assert
            var commonResult = ((CommonResult)((JsonResult)result).Value);
            commonResult.IsOk.ShouldBeFalse();
            commonResult.Messages.Any(x => x == StringOne);
            MockProjectService.Verify_DeleteProject();
        }

        [Test]
        public async Task Delete_POST_InvalidParameter()
        {
            // arrange

            // act
            var result = await SystemUnderTest.Delete(EmptyUid);

            // assert
            AssertView<ForbidResult>(result);
        }


        [Test]
        public async Task Clone_GET()
        {
            // arrange 
            MockProjectService.Setup_GetProject_Returns_ProjectReadResponse_Success();

            // act
            var result = await SystemUnderTest.Clone(OrganizationOneProjectOneUid);

            // assert
            AssertViewWithModel<ProjectCloneModel>(result);
            MockProjectService.Verify_GetProject();
        }

        [Test]
        public async Task Clone_GET_FailedResponse()
        {
            // arrange 
            MockProjectService.Setup_GetProject_Returns_ProjectReadResponse_Failed();

            // act
            var result = await SystemUnderTest.Clone(OrganizationOneProjectOneUid);

            // assert
            AssertViewAccessDenied(result);
            MockProjectService.Verify_GetProject();
        }

        [Test]
        public async Task Clone_GET_InvalidResponse()
        {
            // arrange 
            MockProjectService.Setup_GetProject_Returns_ProjectReadResponse_Invalid();

            // act
            var result = await SystemUnderTest.Clone(OrganizationOneProjectOneUid);

            // assert
            AssertViewAccessDenied(result);
            MockProjectService.Verify_GetProject();
        }

        [Test]
        public async Task Clone_GET_InvalidParameter()
        {
            // arrange

            // act
            var result = await SystemUnderTest.Clone(EmptyUid);

            // assert
            AssertViewAccessDenied(result);
        }

        [Test]
        public async Task Clone_POST()
        {
            // arrange
            MockProjectService.Setup_CloneProject_Returns_ProjectCloneResponse_Success();
            var model = GetOrganizationOneProjectOneCloneModel();

            // act
            var result = await SystemUnderTest.Clone(model);

            // assert
            ((RedirectResult)result).Url.ShouldBe($"/Project/Detail/{EmptyUid}");
            MockProjectService.Verify_CloneProject();
        }

        [Test]
        public async Task Clone_POST_FailedResponse()
        {
            // arrange
            MockProjectService.Setup_CloneProject_Returns_ProjectCloneResponse_Failed();
            var model = GetOrganizationOneProjectOneCloneModel();

            // act
            var result = await SystemUnderTest.Clone(model);

            // assert
            AssertErrorMessagesForInvalidOrFailedResponse<ProjectCloneModel>(result);
            MockProjectService.Verify_CloneProject();
        }

        [Test]
        public async Task Clone_POST_InvalidResponse()
        {
            // arrange
            MockProjectService.Setup_CloneProject_Returns_ProjectCloneResponse_Invalid();
            var model = GetOrganizationOneProjectOneCloneModel();

            // act
            var result = await SystemUnderTest.Clone(model);

            // assert
            AssertErrorMessagesForInvalidOrFailedResponse<ProjectCloneModel>(result);
            MockProjectService.Verify_CloneProject();
        }

        [Test]
        public async Task Clone_POST_InvalidModel()
        {
            // arrange
            var model = new ProjectCloneModel();

            // act
            var result = await SystemUnderTest.Clone(model);

            // assert
            AssertInputErrorMessagesOfView(result, model);
        }

        [Test]
        public void SelectData_GET()
        {
            // arrange
            MockProjectService.Setup_GetProjects_Returns_ProjectReadListResponse_Success();

            // act
            var result = SystemUnderTest.SelectData();

            // assert
            AssertView<JsonResult>(result);
            MockProjectService.Verify_GetProjects();
        }

        [Test]
        public async Task SelectData_GET_FailedResponse()
        {
            // arrange
            MockProjectService.Setup_GetProjects_Returns_ProjectReadListResponse_Failed();

            // act
            var result = await SystemUnderTest.SelectData();

            // assert
            MockProjectService.Verify_GetProjects();
            result.ShouldBe(null);
        }

        [Test]
        public async Task SelectData_GET_InvalidResponse()
        {
            // arrange
            MockProjectService.Setup_GetProjects_Returns_ProjectReadListResponse_Invalid();

            // act
            var result = await SystemUnderTest.SelectData();

            // assert
            MockProjectService.Verify_GetProjects();
            result.ShouldBe(null);
        }

        [Test]
        public void LabelListData_GET()
        {
            // arrange
            MockLabelService.Setup_GetLabels_Returns_LabelReadListResponse_Success();

            // act
            var result = SystemUnderTest.LabelListData(OrganizationOneProjectOneUid, One, Two);

            // assert
            AssertView<JsonResult>(result);
            MockLabelService.Verify_GetLabels();
        }

        [Test]
        public void LabelListData_GET_FailedResponse()
        {
            // arrange
            MockLabelService.Setup_GetLabels_Returns_LabelReadListResponse_Failed();

            // act
            var result = SystemUnderTest.LabelListData(OrganizationOneProjectOneUid, One, Two);

            // assert
            AssertView<NotFoundResult>(result);
            MockLabelService.Verify_GetLabels();
        }

        [Test]
        public void LabelListData_GET_InvalidResponse()
        {
            // arrange
            MockLabelService.Setup_GetLabels_Returns_LabelReadListResponse_Invalid();

            // act
            var result = SystemUnderTest.LabelListData(OrganizationOneProjectOneUid, One, Two);

            // assert
            AssertView<NotFoundResult>(result);
            MockLabelService.Verify_GetLabels();
        }

        [Test]
        public void LabelListData_GET_InvalidParameter()
        {
            // arrange

            // act
            var result = SystemUnderTest.LabelListData(OrganizationOneProjectOneUid, One, Two);

            // assert
            AssertView<ForbidResult>(result);
        }

        [TestCase(10, 10)]
        [TestCase(10, 1000)]
        [TestCase(-10, 10)]
        [TestCase(10, -10)]
        [TestCase(1000, 10)]
        public async Task LabelListData_GET_SetPaging(int skip, int take)
        {
            // arrange
            MockLabelService.Setup_GetLabels_Returns_LabelReadListResponse_Success();

            // act
            var result = (JsonResult)await SystemUnderTest.LabelListData(OrganizationOneProjectOneUid, skip, take);

            // assert
            AssertView<DataResult>(result);
            AssertPagingInfo(result);
        }

        [Test]
        public void DownloadLabels_POST()
        {
            // arrange
            MockLabelService.Setup_GetLabelsWithTranslations_Returns_AllLabelReadListResponse_Success();

            // act
            var result = SystemUnderTest.DownloadLabels(OrganizationOneProjectOneUid);

            // assert
            AssertView<FileResult>(result);
            MockLabelService.Verify_GetLabelsWithTranslations();
        }

        [Test]
        public void DownloadLabels_POST_FailedResponse()
        {
            // arrange
            MockLabelService.Setup_GetLabelsWithTranslations_Returns_AllLabelReadListResponse_Failed();

            // act
            var result = SystemUnderTest.DownloadLabels(OrganizationOneProjectOneUid);

            // assert
            AssertView<FileResult>(result);
            MockLabelService.Verify_GetLabelsWithTranslations();
        }

        [Test]
        public void DownloadLabels_POST_InvalidResponse()
        {
            // arrange
            MockLabelService.Setup_GetLabelsWithTranslations_Returns_AllLabelReadListResponse_Invalid();

            // act
            var result = SystemUnderTest.DownloadLabels(OrganizationOneProjectOneUid);

            // assert
            AssertView<FileResult>(result);
            MockLabelService.Verify_GetLabelsWithTranslations();
        }

        [Test]
        public async Task DownloadLabels_Post_InvalidParameter()
        {
            // arrange

            // act
            var result = await SystemUnderTest.DownloadLabels(EmptyUid);

            // assert
            AssertView<NoContentResult>(result);
        }

        [Test]
        public void ChangeActivation_Post()
        {
            // arrange
            MockProjectService.Setup_ChangeActivationForProject_Returns_ProjectChangeActivationResponse_Success();

            // act
            var result = SystemUnderTest.ChangeActivation(OrganizationOneProjectOneUid, OrganizationOneProjectOneUid);

            // assert
            AssertView<JsonResult>(result);
            MockProjectService.Verify_ChangeActivationForProject();
        }

        [Test]
        public async Task ChangeActivation_Post_FailedResponse()
        {
            // arrange
            MockProjectService.Setup_ChangeActivationForProject_Returns_ProjectChangeActivationResponse_Failed();

            // act
            var result = (JsonResult)await SystemUnderTest.ChangeActivation(OrganizationOneProjectOneUid, OrganizationOneProjectOneUid);

            // assert
            ((CommonResult)result.Value).IsOk.ShouldBe(false);
            MockProjectService.Verify_ChangeActivationForProject();
        }

        [Test]
        public async Task ChangeActivation_Post_InvalidResponse()
        {
            // arrange
            MockProjectService.Setup_ChangeActivationForProject_Returns_ProjectChangeActivationResponse_Invalid();

            // act
            var result = (JsonResult)await SystemUnderTest.ChangeActivation(OrganizationOneProjectOneUid, OrganizationOneProjectOneUid);

            // assert
            ((CommonResult)result.Value).IsOk.ShouldBe(false);
            MockProjectService.Verify_ChangeActivationForProject();
        }

        [Test]
        public async Task ChangeActivation_Post_InvalidParameter()
        {
            // arrange

            // act
            var result = (JsonResult)await SystemUnderTest.ChangeActivation(EmptyUid, OrganizationOneProjectOneUid);

            // assert
            ((CommonResult)result.Value).IsOk.ShouldBe(false);
        }
    }
}