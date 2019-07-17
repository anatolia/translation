using System;
using Microsoft.AspNetCore.Mvc;

using NUnit.Framework;
using Shouldly;

using Translation.Client.Web.Controllers;
using Translation.Client.Web.Models.Label;
using Translation.Client.Web.Models.LabelTranslation;
using static Translation.Tests.TestHelpers.ActionMethodNameConstantTestHelper;
using static Translation.Tests.TestHelpers.FakeConstantTestHelper;
using static Translation.Tests.TestHelpers.AssertViewModelTestHelper;
using static Translation.Tests.TestHelpers.AssertModelTestHelper;
using static Translation.Tests.TestHelpers.FakeModelTestHelper;

namespace Translation.Tests.Client.Controllers
{
    [TestFixture]
    public class LabelControllerTests : ControllerBaseTests
    {
        public LabelController SystemUnderTest { get; set; }

        [SetUp]
        public void run_before_every_test()
        {
            SystemUnderTest = Container.Resolve<LabelController>();
            SetControllerContext(SystemUnderTest);
        }

        [TestCase(CreateAction, new[] { typeof(Guid) }, typeof(HttpGetAttribute))]
        [TestCase(CreateAction, new[] { typeof(LabelCreateModel) }, typeof(HttpPostAttribute))]
        [TestCase(DetailAction, new[] { typeof(Guid) }, typeof(HttpGetAttribute))]
        [TestCase(EditAction, new[] { typeof(Guid) }, typeof(HttpGetAttribute))]
        [TestCase(EditAction, new[] { typeof(LabelEditModel) }, typeof(HttpPostAttribute))]
        [TestCase(CloneAction, new[] { typeof(Guid) }, typeof(HttpGetAttribute))]
        [TestCase(CloneAction, new[] { typeof(LabelCloneModel) }, typeof(HttpPostAttribute))]
        [TestCase(SearchListAction, new[] { typeof(string) }, typeof(HttpGetAttribute))]
        [TestCase(SearchDataAction, new[] { typeof(string)}, typeof(HttpGetAttribute))]
        [TestCase(SearchListDataAction, new[] { typeof(string), typeof(int), typeof(int) }, typeof(HttpGetAttribute))]
        [TestCase(RevisionsAction, new[] { typeof(Guid) }, typeof(HttpGetAttribute))]
        [TestCase(RevisionsDataAction, new[] { typeof(Guid) }, typeof(HttpGetAttribute))]
        [TestCase(RestoreAction, new[] { typeof(Guid), typeof(int) }, typeof(HttpPostAttribute))]
        [TestCase(ChangeActivationAction, new[] { typeof(Guid), typeof(Guid) }, typeof(HttpPostAttribute))]
        [TestCase(UploadLabelFromCSVFileAction, new[] { typeof(Guid) }, typeof(HttpGetAttribute))]
        [TestCase(UploadLabelFromCSVFileAction, new[] { typeof(LabelUploadFromCSVModel) }, typeof(HttpPostAttribute))]
        [TestCase(CreateBulkLabelAction, new[] { typeof(Guid) }, typeof(HttpGetAttribute))]
        [TestCase(CreateBulkLabelAction, new[] { typeof(CreateBulkLabelModel) }, typeof(HttpPostAttribute))]
        [TestCase(LabelTranslationCreateAction, new[] { typeof(Guid) }, typeof(HttpGetAttribute))]
        [TestCase(LabelTranslationCreateAction, new[] { typeof(LabelTranslationCreateModel) }, typeof(HttpPostAttribute))]
        [TestCase(LabelTranslationDetailAction, new[] { typeof(Guid) }, typeof(HttpGetAttribute))]
        [TestCase(LabelTranslationDetailAction, new[] { typeof(LabelTranslationDetailModel) }, typeof(HttpPostAttribute))]
        [TestCase(LabelTranslationEditAction, new[] { typeof(Guid) }, typeof(HttpGetAttribute))]
        [TestCase(LabelTranslationEditAction, new[] { typeof(LabelTranslationEditModel) }, typeof(HttpPostAttribute))]
        [TestCase(LabelTranslationListDataAction, new[] { typeof(Guid), typeof(int), typeof(int) }, typeof(HttpGetAttribute))]
        [TestCase(UploadLabelTranslationFromCSVFileAction, new[] { typeof(Guid) }, typeof(HttpGetAttribute))]
        [TestCase(UploadLabelTranslationFromCSVFileAction, new[] { typeof(UploadLabelTranslationFromCSVFileModel) }, typeof(HttpPostAttribute))]
        [TestCase(DownloadTranslationsAction, new[] { typeof(Guid) }, typeof(HttpPostAttribute))]
        [TestCase(RestoreLabelTranslationAction, new[] { typeof(Guid), typeof(int) }, typeof(HttpPostAttribute))]
        [TestCase(LabelTranslationRevisionsAction, new[] { typeof(Guid) }, typeof(HttpGetAttribute))]
        [TestCase(LabelTranslationRevisionsDataAction, new[] { typeof(Guid) }, typeof(HttpGetAttribute))]
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
    }
}