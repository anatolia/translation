using System.Threading.Tasks;

using Autofac;
using NUnit.Framework;
using Shouldly;

using Translation.Common.Contracts;
using StandardUtils.Enumerations;
using Translation.Common.Models.Responses.TranslationProvider;
using Translation.Common.Models.Shared;
using Translation.Server.Unit.Tests.RepositorySetupHelpers;

using static Translation.Common.Tests.TestHelpers.FakeRequestTestHelper;
using static Translation.Common.Tests.TestHelpers.FakeConstantTestHelper;
using static Translation.Server.Unit.Tests.TestHelpers.AssertResponseTestHelper;
using static Translation.Server.Unit.Tests.TestHelpers.AssertViewModelTestHelper;

namespace Translation.Server.Unit.Tests.Services
{
    [TestFixture]
    public class TranslationProviderServiceTests : ServiceBaseTests
    {
        public ITranslationProviderService SystemUnderTest { get; set; }

        [SetUp]
        public void run_before_every_test()
        {
            Refresh();
            SystemUnderTest = Builder.Build().Resolve<ITranslationProviderService>();
        }

        [Test]
        public async Task TranslationProviderService_GetTranslationProvider_Success()
        {
            // arrange
            var request = GetTranslationProviderReadRequest();
            MockTranslationProviderRepository.Setup_Select_Returns_TranslationProviderOne();

            // act
            var result = await SystemUnderTest.GetTranslationProvider(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Success);
            AssertReturnType<TranslationProviderReadResponse>(result);
            MockTranslationProviderRepository.Verify_Select();
        }

        [Test]
        public async Task TranslationProviderService_GetTranslationProvider_Failed_TranslationProviderNotFound()
        {
            // arrange
            var request = GetTranslationProviderReadRequest();
            MockTranslationProviderRepository.Setup_Select_Returns_TranslationProviderNotExist();

            // act
            var result = await SystemUnderTest.GetTranslationProvider(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Failed, TranslationProviderNotFound);
            AssertReturnType<TranslationProviderReadResponse>(result);
            MockTranslationProviderRepository.Verify_Select();
        }

        [Test]
        public async Task TranslationProviderService_GetTranslationProviders_Success_SelectAfter()
        {
            // arrange
            var request = GetTranslationProviderReadListRequestForSelectAfter();
            MockTranslationProviderRepository.Setup_SelectAfter_Returns_TranslationProviders();
            MockTranslationProviderRepository.Setup_Count_Returns_Ten();

            // act
            var result = await SystemUnderTest.GetTranslationProviders(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Success);
            AssertReturnType<TranslationProviderReadListResponse>(result);
            AssertPagingInfoForSelectAfter(request.PagingInfo, Ten);
            MockTranslationProviderRepository.Verify_SelectAfter();
            MockTranslationProviderRepository.Verify_Count();
        }

        [Test]
        public async Task TranslationProviderService_GetTranslationProviders_Success_SelectMany()
        {
            // arrange
            var request = GetTranslationProviderReadListRequestForSelectMany();
            MockTranslationProviderRepository.Setup_SelectMany_Returns_TranslationProviders();
            MockTranslationProviderRepository.Setup_Count_Returns_Ten();

            // act
            var result = await SystemUnderTest.GetTranslationProviders(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Success);
            AssertReturnType<TranslationProviderReadListResponse>(result);
            AssertPagingInfoForSelectMany(request.PagingInfo, Ten);
            MockTranslationProviderRepository.Verify_SelectMany();
            MockTranslationProviderRepository.Verify_Count();
        }

        [Test]
        public async Task TranslationProviderService_EditTranslationProvider_Success()
        {
            // arrange
            var request = GetTranslationProviderEditRequest();
            MockTranslationProviderRepository.Setup_Select_Returns_TranslationProviderOne();
            MockTranslationProviderRepository.Setup_Any_Returns_False();
            MockTranslationProviderRepository.Setup_Update_Success();

            // act
            var result = await SystemUnderTest.EditTranslationProvider(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Success);
            AssertReturnType<TranslationProviderEditResponse>(result);
            MockTranslationProviderRepository.Verify_Select();
            MockTranslationProviderRepository.Verify_Any();
            MockTranslationProviderRepository.Verify_Update();
        }

        [Test]
        public async Task TranslationProviderService_EditTranslationProvider_SameValue_Success()
        {
            // arrange
            var request = GetSameValueTranslationProviderEditRequest();
            MockTranslationProviderRepository.Setup_Select_Returns_TranslationProviderOne();
            
            // act
            var result = await SystemUnderTest.EditTranslationProvider(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Success);
            AssertReturnType<TranslationProviderEditResponse>(result);
            MockTranslationProviderRepository.Verify_Select();
           
        }

        [Test]
        public async Task TranslationProviderService_EditTranslationProvider_Failed_ProviderAlreadyExist()
        {
            // arrange
            var request = GetTranslationProviderEditRequest();
            MockTranslationProviderRepository.Setup_Select_Returns_TranslationProviderOne();
            MockTranslationProviderRepository.Setup_Any_Returns_True();

            // act
            var result = await SystemUnderTest.EditTranslationProvider(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Failed, "translation_provider_already_exist");
            AssertReturnType<TranslationProviderEditResponse>(result);
            MockTranslationProviderRepository.Verify_Select();
            MockTranslationProviderRepository.Verify_Any();
        }

        [Test]
        public async Task TranslationProviderService_EditTranslationProvider_Failed_TranslationProviderNotFound()
        {
            // arrange
            var request = GetTranslationProviderEditRequest();
            MockTranslationProviderRepository.Setup_Select_Returns_TranslationProviderNotExist();

            // act
            var result = await SystemUnderTest.EditTranslationProvider(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Failed, TranslationProviderNotFound);
            AssertReturnType<TranslationProviderEditResponse>(result);
            MockTranslationProviderRepository.Verify_Select();
        }

        [Test]
        public async Task TranslationProviderService_EditTranslationProvider_Failed()
        {
            // arrange
            var request = GetTranslationProviderEditRequest();
            MockTranslationProviderRepository.Setup_Select_Returns_TranslationProviderOne();
            MockTranslationProviderRepository.Setup_Any_Returns_False();
            MockTranslationProviderRepository.Setup_Update_Failed();

            // act
            var result = await SystemUnderTest.EditTranslationProvider(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Failed);
            AssertReturnType<TranslationProviderEditResponse>(result);
            MockTranslationProviderRepository.Verify_Select();
            MockTranslationProviderRepository.Verify_Any();
            MockTranslationProviderRepository.Verify_Update();
        }

        [Test]
        public void TranslationProviderService_GetActiveTranslationProvider_Success()
        {
            // arrange
            var request = GetActiveTranslationProvider();
            MockTranslationProviderRepository.Setup_Select_Returns_TranslationProviderOne();

            // act
            var result = SystemUnderTest.GetActiveTranslationProvider(request);

            // assert
            AssertReturnType<ActiveTranslationProvider>(result);
            MockTranslationProviderRepository.Verify_Select();
        }

        [Test]
        public void TranslationProviderService_GetActiveTranslationProvider_Null()
        {
            // arrange
            var request = GetActiveTranslationProvider();
            MockTranslationProviderRepository.Setup_Select_Returns_TranslationProviderNotExist();

            // act
            var result = SystemUnderTest.GetActiveTranslationProvider(request);

            // assert
            result.ShouldBe(null);
            MockTranslationProviderRepository.Verify_Select();
        }
    }
}