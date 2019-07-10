using System;
using Microsoft.AspNetCore.Mvc;

using NUnit.Framework;
using Shouldly;

using Translation.Client.Web.Controllers;
using Translation.Client.Web.Models.Project;
using Translation.Tests.TestHelpers;
using static Translation.Tests.TestHelpers.ActionMethodNameConstantTestHelper;
using static Translation.Tests.TestHelpers.FakeConstantTestHelper;

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
            var result = SystemUnderTest.Create(UidOne);

            // assert
            // todo
            MockOrganizationService.Verify_GetOrganization();
        }
    }
}