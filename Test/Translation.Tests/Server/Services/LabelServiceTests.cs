using System.Threading.Tasks;

using NUnit.Framework;
using Shouldly;

using Translation.Common.Contracts;
using Translation.Common.Enumerations;
using Translation.Common.Models.Responses.Label;
using Translation.Tests.SetupHelpers;
using static Translation.Tests.TestHelpers.FakeRequestTestHelper;
using static Translation.Tests.TestHelpers.FakeConstantTestHelper;
using static Translation.Tests.TestHelpers.AssertViewModelTestHelper;
using static Translation.Tests.TestHelpers.AssertResponseTestHelper;

namespace Translation.Tests.Server.Services
{
    [TestFixture]
    public class LabelServiceTests : ServiceBaseTests
    {
        public ILabelService SystemUnderTest { get; set; }

        [SetUp]
        public void run_before_every_test()
        {
            SystemUnderTest = Container.Resolve<ILabelService>();
        }

        [Test]
        public async Task LabelService_CreateLabel_Success()
        {
            // arrange
            var request = GetLabelCreateRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneAdminUserOne();
            MockProjectRepository.Setup_Select_Returns_OrganizationOneProjectOne();
            MockLabelRepository.Setup_Any_Returns_False();
            MockLabelUnitOfWork.Setup_DoCreateWork_Returns_True();

            // act
            var result = await SystemUnderTest.CreateLabel(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Success);
            AssertReturnType<LabelCreateResponse>(result);
            MockUserRepository.Verify_SelectById();
            MockProjectRepository.Verify_Select();
            MockLabelRepository.Verify_Any();
            MockLabelUnitOfWork.Verify_DoCreateWork();
        }
    }
}