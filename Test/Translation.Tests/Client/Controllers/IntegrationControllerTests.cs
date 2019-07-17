using System;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;

using Translation.Client.Web.Controllers;
using Translation.Client.Web.Models.Integration;
using static Translation.Tests.TestHelpers.ActionMethodNameConstantTestHelper;
using static Translation.Tests.TestHelpers.FakeConstantTestHelper;
using static Translation.Tests.TestHelpers.AssertViewModelTestHelper;
using static Translation.Tests.TestHelpers.AssertModelTestHelper;
using static Translation.Tests.TestHelpers.FakeModelTestHelper;

namespace Translation.Tests.Client.Controllers
{
    [TestFixture]
    public class IntegrationControllerTests : ControllerBaseTests
    {
        public IntegrationController SystemUnderTest { get; set; }

        [SetUp]
        public void run_before_every_test()
        {
            SystemUnderTest = Container.Resolve<IntegrationController>();
            SetControllerContext(SystemUnderTest);
        }

        [TestCase(CreateAction, new[] { typeof(Guid) }, typeof(HttpGetAttribute))]
        [TestCase(CreateAction, new[] { typeof(IntegrationCreateModel) }, typeof(HttpPostAttribute))]
        [TestCase(DetailAction, new[] { typeof(Guid) }, typeof(HttpGetAttribute))]
        [TestCase(EditAction, new[] { typeof(Guid) }, typeof(HttpGetAttribute))]
        [TestCase(EditAction, new[] { typeof(IntegrationEditModel) }, typeof(HttpPostAttribute))]
        [TestCase(DeleteAction, new[] { typeof(Guid) }, typeof(HttpPostAttribute))]
        [TestCase(RevisionsAction, new[] { typeof(Guid) }, typeof(HttpGetAttribute))]
        [TestCase(RevisionsDataAction, new[] { typeof(Guid) }, typeof(HttpGetAttribute))]
        [TestCase(RestoreAction, new[] { typeof(Guid), typeof(int) }, typeof(HttpPostAttribute))]
        [TestCase(ClientListDataAction, new[] { typeof(Guid), typeof(int), typeof(int) }, typeof(HttpGetAttribute))]
        [TestCase(ClientCreateAction, new[] { typeof(Guid) }, typeof(HttpPostAttribute))]
        [TestCase(ClientChangeActivationAction, new[] { typeof(Guid) }, typeof(HttpPostAttribute))]
        [TestCase(ClientRefreshAction, new[] { typeof(Guid) }, typeof(HttpPostAttribute))]
        [TestCase(ClientActiveTokensAction, new[] { typeof(Guid) }, typeof(HttpGetAttribute))]
        [TestCase(ClientActiveTokensDataAction, new[] { typeof(Guid), typeof(int), typeof(int) }, typeof(HttpGetAttribute))]
        [TestCase(ActiveTokensAction, new[] { typeof(Guid) }, typeof(HttpGetAttribute))]
        [TestCase(ActiveTokensDataAction, new[] { typeof(Guid), typeof(int), typeof(int) }, typeof(HttpGetAttribute))]
        public void Methods_Has_Http_Verb_Attributes(string actionMethod, Type[] parameters, Type httpVerbAttribute)
        {
            var type = SystemUnderTest.GetType();
            var methodInfo = type.GetMethod(actionMethod, parameters);
            var attributes = methodInfo.GetCustomAttributes(httpVerbAttribute, true);
            Assert.AreEqual(attributes.Length, 1);
        }
    }
}