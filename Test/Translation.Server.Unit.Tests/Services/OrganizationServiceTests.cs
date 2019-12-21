using System;
using System.Threading.Tasks;
using Autofac;
using NUnit.Framework;
using Shouldly;

using Translation.Common.Contracts;
using StandardUtils.Enumerations;
using Translation.Common.Models.Responses.Organization;
using Translation.Common.Models.Responses.User;
using Translation.Common.Models.Responses.User.LoginLog;
using Translation.Common.Models.Shared;
using Translation.Server.Unit.Tests.RepositorySetupHelpers;
using Translation.Server.Unit.Tests.UnitOfWorkSetupHelper;

using static Translation.Common.Tests.TestHelpers.FakeRequestTestHelper;
using static Translation.Common.Tests.TestHelpers.FakeConstantTestHelper;
using static Translation.Server.Unit.Tests.TestHelpers.AssertViewModelTestHelper;
using static Translation.Server.Unit.Tests.TestHelpers.AssertResponseTestHelper;

namespace Translation.Server.Unit.Tests.Services
{
    [TestFixture]
    public class OrganizationServiceTests : ServiceBaseTests
    {
        public IOrganizationService SystemUnderTest { get; set; }

        [SetUp]
        public void run_before_every_test()
        {
            Refresh();
            SystemUnderTest = Container.Resolve<IOrganizationService>();
        }

        [Test]
        public async Task OrganizationService_CreateOrganizationWithAdmin_Success()
        {
            // arrange
            var request = GetSignUpRequest();
            MockUserRepository.Setup_Select_Returns_OrganizationOneUserOneNotExist();
            MockOrganizationRepository.Setup_Select_Returns_OrganizationOneNotExist();
            MockLanguageRepository.Setup_Select_Returns_Language();
            MockSignUpUnitOfWork.Setup_DoWork_Returns_TrueOrganizationUser();

            // act
            var result = await SystemUnderTest.CreateOrganizationWithAdmin(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Success);
            AssertReturnType<SignUpResponse>(result);
            MockUserRepository.Verify_Select();
            MockOrganizationRepository.Verify_Select();
            MockLanguageRepository.Verify_Select();
            MockSignUpUnitOfWork.Verify_DoWork();
        }

        [Test]
        public async Task OrganizationService_CreateOrganizationWithAdmin_Invalid_EmailAlreadyExist()
        {
            // arrange
            var request = GetSignUpRequest();
            MockUserRepository.Setup_Select_Returns_OrganizationOneUserOne();

            // act
            var result = await SystemUnderTest.CreateOrganizationWithAdmin(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Invalid, "email_already_exist");
            AssertReturnType<SignUpResponse>(result);
            MockUserRepository.Verify_Select();
        }

        [Test]
        public async Task OrganizationService_CreateOrganizationWithAdmin_Invalid_OrganizationNameMustBeUnique()
        {
            // arrange
            var request = GetSignUpRequest();
            MockUserRepository.Setup_Select_Returns_OrganizationOneUserOneNotExist();
            MockOrganizationRepository.Setup_Select_Returns_OrganizationOne();

            // act
            var result = await SystemUnderTest.CreateOrganizationWithAdmin(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Invalid, OrganizationNameMustBeUnique);
            AssertReturnType<SignUpResponse>(result);
            MockUserRepository.Verify_Select();
            MockOrganizationRepository.Verify_Select();
        }

        [Test]
        public async Task OrganizationService_CreateOrganizationWithAdmin_Failed()
        {
            // arrange
            var request = GetSignUpRequest();
            MockUserRepository.Setup_Select_Returns_OrganizationOneUserOneNotExist();
            MockOrganizationRepository.Setup_Select_Returns_OrganizationOneNotExist();
            MockLanguageRepository.Setup_Select_Returns_Language();
            MockSignUpUnitOfWork.Setup_DoWork_Returns_FalseOrganizationUser();

            // act
            var result = await SystemUnderTest.CreateOrganizationWithAdmin(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Failed);
            AssertReturnType<SignUpResponse>(result);
            MockUserRepository.Verify_Select();
            MockOrganizationRepository.Verify_Select();
            MockLanguageRepository.Verify_Select();
            MockSignUpUnitOfWork.Verify_DoWork();
        }

        [Test]
        public void OrganizationService_GetOrganization_Success()
        {
            // arrange
            var request = GetOrganizationReadRequest();

            // act
            var result = SystemUnderTest.GetOrganization(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Success);
            AssertReturnType<OrganizationReadResponse>(result);
        }

        [Test]
        public async Task OrganizationService_GetOrganizations_Success_SelectAfter()
        {
            // arrange
            var request = GetOrganizationReadListRequestForSelectAfter();
            MockOrganizationRepository.Setup_SelectAfter_Returns_Organizations();
            MockOrganizationRepository.Setup_Count_Returns_Ten();

            // act
            var result = await SystemUnderTest.GetOrganizations(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Success);
            AssertReturnType<OrganizationReadListResponse>(result);
            AssertPagingInfoForSelectAfter(request.PagingInfo, Ten);
            MockOrganizationRepository.Verify_SelectAfter();
            MockOrganizationRepository.Verify_Count();
        }

        [Test]
        public async Task OrganizationService_GetOrganizations_Success_SelectMany()
        {
            // arrange
            var request = GetOrganizationReadListRequestForSelectMany();
            MockOrganizationRepository.Setup_SelectMany_Returns_Organizations();
            MockOrganizationRepository.Setup_Count_Returns_Ten();

            // act
            var result = await SystemUnderTest.GetOrganizations(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Success);
            AssertReturnType<OrganizationReadListResponse>(result);
            AssertPagingInfoForSelectMany(request.PagingInfo, Ten);
            MockOrganizationRepository.Verify_SelectMany();
            MockOrganizationRepository.Verify_Count();
        }

        [Test]
        public async Task OrganizationService_GetOrganizationRevisions_Success()
        {
            // arrange
            var request = GetOrganizationRevisionReadListRequest();
            MockOrganizationRepository.Setup_Select_Returns_OrganizationOne();
            MockOrganizationRepository.Setup_SelectRevisions_Returns_OrganizationOneRevisionsRevisionOneInIt();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneUserOne();

            // act
            var result = await SystemUnderTest.GetOrganizationRevisions(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Success);
            AssertReturnType<OrganizationRevisionReadListResponse>(result);
            MockOrganizationRepository.Verify_Select();
            MockOrganizationRepository.Verify_SelectRevisions();
            MockUserRepository.Verify_SelectById();
        }

        [Test]
        public async Task OrganizationService_GetOrganizationRevisions_Failed_OrganizationNotFound()
        {
            // arrange
            var request = GetOrganizationRevisionReadListRequest();
            MockOrganizationRepository.Setup_Select_Returns_OrganizationOneNotExist();

            // act
            var result = await SystemUnderTest.GetOrganizationRevisions(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Failed, OrganizationNotFound);
            AssertReturnType<OrganizationRevisionReadListResponse>(result);
            MockOrganizationRepository.Verify_Select();
        }

        [Test]
        public async Task OrganizationService_EditOrganization_Success()
        {
            // arrange
            var request = GetOrganizationEditRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneAdminUserOne();
            MockOrganizationRepository.Setup_Any_Returns_False();
            MockOrganizationRepository.Setup_Update_Success();

            // act
            var result = await SystemUnderTest.EditOrganization(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Success);
            AssertReturnType<OrganizationEditResponse>(result);
            MockUserRepository.Verify_SelectById();
            MockOrganizationRepository.Verify_Any();
            MockOrganizationRepository.Verify_Update();
        }

        [Test]
        public async Task OrganizationService_EditOrganization_NotDifferent_Success()
        {
            // arrange
            var request = GetNotDifferentOrganizationEditRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneAdminUserOne();
            MockOrganizationRepository.Setup_IsOrganizationActive_Returns_False();
            MockOrganizationRepository.Setup_Select_Returns_OrganizationOne();

            // act
            var result = await SystemUnderTest.EditOrganization(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Success);
            AssertReturnType<OrganizationEditResponse>(result);
            MockUserRepository.Verify_SelectById();
            MockOrganizationRepository.Verify_IsOrganizationActive();
            MockOrganizationRepository.Verify_Select();

        }

        [Test]
        public async Task OrganizationService_EditOrganization_Invalid_CurrentUserNotAdmin()
        {
            // arrange
            var request = GetOrganizationEditRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneUserOne();

            // act
            var result = await SystemUnderTest.EditOrganization(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Invalid, UserNotAdmin);
            AssertReturnType<OrganizationEditResponse>(result);
            MockUserRepository.Verify_SelectById();
        }

        [Test]
        public async Task OrganizationService_EditOrganization_Invalid_OrganizationNotActive()
        {
            // arrange
            var request = GetOrganizationEditRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneAdminUserOne();
            MockOrganizationRepository.Setup_IsOrganizationActive_Returns_True();

            // act
            var result = await SystemUnderTest.EditOrganization(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Invalid, OrganizationNotActive);
            AssertReturnType<OrganizationEditResponse>(result);
            MockUserRepository.Verify_SelectById();
            MockOrganizationRepository.Verify_IsOrganizationActive();

        }

        [Test]
        public async Task OrganizationService_EditOrganization_Invalid_OrganizationNameMustBeUnique()
        {
            // arrange
            var request = GetOrganizationEditRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneAdminUserOne();
            MockOrganizationRepository.Setup_IsOrganizationActive_Returns_False();
            MockOrganizationRepository.Setup_Any_Returns_True();

            // act
            var result = await SystemUnderTest.EditOrganization(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Invalid, OrganizationNameMustBeUnique);
            AssertReturnType<OrganizationEditResponse>(result);
            MockUserRepository.Verify_SelectById();
            MockOrganizationRepository.Verify_IsOrganizationActive();
            MockOrganizationRepository.Verify_Any();
        }

        [Test]
        public async Task OrganizationService_EditOrganization_Invalid_OrganizationNotMatch()
        {
            // arrange
            var request = GetOrganizationEditRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneAdminUserOne();
            MockOrganizationRepository.Setup_Select_Returns_OrganizationTwo();

            // act
            var result = await SystemUnderTest.EditOrganization(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Invalid);
            AssertReturnType<OrganizationEditResponse>(result);
            MockUserRepository.Verify_SelectById();
            MockOrganizationRepository.Verify_Select();
        }

        [Test]
        public async Task OrganizationService_EditOrganization_Failed()
        {
            // arrange
            var request = GetOrganizationEditRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneAdminUserOne();
            MockOrganizationRepository.Setup_Any_Returns_False();
            MockOrganizationRepository.Setup_Select_Returns_OrganizationOne();
            MockOrganizationRepository.Setup_Update_Failed();

            // act
            var result = await SystemUnderTest.EditOrganization(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Failed);
            AssertReturnType<OrganizationEditResponse>(result);
            MockUserRepository.Verify_SelectById();
            MockOrganizationRepository.Verify_Any();
            MockOrganizationRepository.Verify_Select();
            MockOrganizationRepository.Verify_Update();
        }

        [Test]
        public async Task OrganizationService_RestoreOrganization_Success()
        {
            // arrange
            var request = GetOrganizationRestoreRequest();
            MockOrganizationRepository.Setup_Any_Returns_False();
            MockOrganizationRepository.Setup_Select_Returns_OrganizationOne();
            MockOrganizationRepository.Setup_SelectRevisions_Returns_OrganizationOneRevisionsRevisionOneInIt();
            MockOrganizationRepository.Setup_RestoreRevision_Returns_True();

            // act
            var result = await SystemUnderTest.RestoreOrganization(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Success);
            AssertReturnType<OrganizationRestoreResponse>(result);
            MockOrganizationRepository.Verify_Any();
            MockOrganizationRepository.Verify_Select();
            MockOrganizationRepository.Verify_SelectRevisions();
            MockOrganizationRepository.Verify_RestoreRevision();
        }

        [Test]
        public async Task OrganizationService_RestoreOrganization_Invalid_OrganizationNotActive()
        {
            // arrange
            var request = GetOrganizationRestoreRequest();
            MockOrganizationRepository.Setup_Any_Returns_True();

            // act
            var result = await SystemUnderTest.RestoreOrganization(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Invalid, OrganizationNotActive);
            AssertReturnType<OrganizationRestoreResponse>(result);
            MockOrganizationRepository.Verify_Any();
        }

        [Test]
        public async Task OrganizationService_RestoreOrganization_Failed_OrganizationNotFound()
        {
            // arrange
            var request = GetOrganizationRestoreRequest();
            MockOrganizationRepository.Setup_Any_Returns_False();
            MockOrganizationRepository.Setup_Select_Returns_OrganizationOneNotExist();

            // act
            var result = await SystemUnderTest.RestoreOrganization(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Failed, OrganizationNotFound);
            AssertReturnType<OrganizationRestoreResponse>(result);
            MockOrganizationRepository.Verify_Any();
            MockOrganizationRepository.Verify_Select();
        }

        [Test]
        public async Task OrganizationService_RestoreOrganization_Failed_OrganizationRevisionNotFound()
        {
            // arrange
            var request = GetOrganizationRestoreRequest();
            MockOrganizationRepository.Setup_Any_Returns_False();
            MockOrganizationRepository.Setup_Select_Returns_OrganizationOne();
            MockOrganizationRepository.Setup_SelectRevisions_Returns_OrganizationOneRevisionsRevisionTwoInIt();

            // act
            var result = await SystemUnderTest.RestoreOrganization(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Failed, OrganizationRevisionNotFound);
            AssertReturnType<OrganizationRestoreResponse>(result);
            MockOrganizationRepository.Verify_Any();
            MockOrganizationRepository.Verify_Select();
            MockOrganizationRepository.Verify_SelectRevisions();
        }

        [Test]
        public async Task OrganizationService_RestoreOrganization_Failed()
        {
            // arrange
            var request = GetOrganizationRestoreRequest();
            MockOrganizationRepository.Setup_Any_Returns_False();
            MockOrganizationRepository.Setup_Select_Returns_OrganizationOne();
            MockOrganizationRepository.Setup_SelectRevisions_Returns_OrganizationOneRevisionsRevisionOneInIt();
            MockOrganizationRepository.Setup_RestoreRevision_Returns_False();

            // act
            var result = await SystemUnderTest.RestoreOrganization(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Failed);
            AssertReturnType<OrganizationRestoreResponse>(result);
            MockOrganizationRepository.Verify_Any();
            MockOrganizationRepository.Verify_Select();
            MockOrganizationRepository.Verify_SelectRevisions();
            MockOrganizationRepository.Verify_RestoreRevision();
        }

        [Test]
        public async Task OrganizationService_GetPendingTranslations_Success_SelectAfter()
        {
            // arrange
            var request = GetOrganizationPendingTranslationReadListRequestForSelectAfter(OrganizationOneUid);
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneUserOne();
            MockOrganizationRepository.Setup_Select_Returns_OrganizationOne();
            MockLabelRepository.Setup_SelectAfter_Returns_Labels();
            // act
            var result = await SystemUnderTest.GetPendingTranslations(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Success);
            AssertReturnType<OrganizationPendingTranslationReadListResponse>(result);
            AssertPagingInfoForSelectAfter(result.PagingInfo, Ten);
            MockUserRepository.Verify_SelectById();
            MockOrganizationRepository.Verify_Select();
            MockLabelRepository.Verify_SelectAfter();
        }

        [Test]
        public async Task OrganizationService_GetPendingTranslations_Invalid_OrganizationNotFound()
        {
            // arrange
            var request = GetOrganizationPendingTranslationReadListRequestForSelectAfter(OrganizationOneUid);
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneUserOne();
            MockOrganizationRepository.Setup_Select_Returns_OrganizationOneNotExist();

            // act
            var result = await SystemUnderTest.GetPendingTranslations(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Failed, OrganizationNotFound);
            AssertReturnType<OrganizationPendingTranslationReadListResponse>(result);
            MockUserRepository.Verify_SelectById();
            MockOrganizationRepository.Verify_Select();
        }

        [Test]
        public async Task OrganizationService_GetPendingTranslations_Success_SelectMany()
        {
            // arrange
            var request = GetOrganizationPendingTranslationReadListRequestForSelectMany(OrganizationOneUid);
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneUserOne();
            MockOrganizationRepository.Setup_Select_Returns_OrganizationOne();
            MockLabelRepository.Setup_SelectMany_Returns_Labels();

            // act
            var result = await SystemUnderTest.GetPendingTranslations(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Success);
            AssertReturnType<OrganizationPendingTranslationReadListResponse>(result);
            AssertPagingInfoForSelectMany(result.PagingInfo, Ten);
            MockUserRepository.Verify_SelectById();
            MockOrganizationRepository.Verify_Select();
            MockLabelRepository.Verify_SelectMany();
        }

        [Ignore("Get organization Uid")]
        [Test]
        public async Task OrganizationService_GetPendingTranslations_Invalid_OrganizationNotMatch()
        {
            // arrange
            var request = GetOrganizationPendingTranslationReadListRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationTwoUserOne();

            // act
            var result = await SystemUnderTest.GetPendingTranslations(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Invalid);
            AssertReturnType<OrganizationPendingTranslationReadListResponse>(result);
            MockUserRepository.Verify_SelectById();
        }

        [Test]
        public async Task OrganizationService_ValidateEmail_Success()
        {
            // arrange
            var request = GetValidateEmailRequest();
            MockUserRepository.Setup_Select_Returns_OrganizationOneUserOne();
            MockUserRepository.Setup_Update_Success();

            // act
            var result = await SystemUnderTest.ValidateEmail(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Success);
            AssertReturnType<ValidateEmailResponse>(result);
            MockUserRepository.Verify_Select();
            MockUserRepository.Verify_Update();
        }

        [Test]
        public async Task OrganizationService_ValidateEmail_Failed()
        {
            // arrange
            var request = GetValidateEmailRequest();
            MockUserRepository.Setup_Select_Returns_OrganizationOneUserOne();
            MockUserRepository.Setup_Update_Failed();

            // act
            var result = await SystemUnderTest.ValidateEmail(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Failed);
            AssertReturnType<ValidateEmailResponse>(result);
            MockUserRepository.Verify_Select();
            MockUserRepository.Verify_Update();
        }

        [Test]
        public async Task OrganizationService_LogOn_Success()
        {
            // arrange
            var request = GetLogOnRequest();
            MockUserRepository.Setup_Select_Returns_OrganizationOneUserOneForSuccessLogOn();
            MockLogOnUnitOfWork.Setup_DoWork_Returns_True();

            // act
            var result = await SystemUnderTest.LogOn(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Success);
            AssertReturnType<LogOnResponse>(result);
            MockUserRepository.Verify_Select();
            MockLogOnUnitOfWork.Verify_DoWork();
        }

        [Test]
        public async Task OrganizationService_LogOn_Failed_UserNotFound()
        {
            // arrange
            var request = GetLogOnRequest();
            MockUserRepository.Setup_Select_Returns_OrganizationOneUserOneNotExist();

            // act
            var result = await SystemUnderTest.LogOn(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Failed, UserNotFound);
            AssertReturnType<LogOnResponse>(result);
            MockUserRepository.Verify_Select();
        }

        [Test]
        public async Task OrganizationService_LogOn_Invalid_UserNotActive()
        {
            // arrange
            var request = GetLogOnRequest();
            MockUserRepository.Setup_Select_Returns_OrganizationOneUserOneNotActive();

            // act
            var result = await SystemUnderTest.LogOn(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Invalid, UserNotActive);
            AssertReturnType<LogOnResponse>(result);
            MockUserRepository.Verify_Select();
        }

        [Test]
        public async Task OrganizationService_LogOn_Failed()
        {
            // arrange
            var request = GetLogOnRequest();
            MockUserRepository.Setup_Select_Returns_OrganizationOneUserOneForSuccessLogOn();
            MockLogOnUnitOfWork.Setup_DoWork_Returns_False();

            // act
            var result = await SystemUnderTest.LogOn(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Failed);
            AssertReturnType<LogOnResponse>(result);
            MockUserRepository.Verify_Select();
            MockLogOnUnitOfWork.Verify_DoWork();
        }

        [Test]
        public async Task OrganizationService_DemandPasswordReset_Success()
        {
            // arrange
            var request = GetDemandPasswordResetRequest();
            MockUserRepository.Setup_Select_Returns_OrganizationOneUserOnePasswordResetRequestedAtFiveMinutesBefore();
            MockUserRepository.Setup_Update_Success();

            // act
            var result = await SystemUnderTest.DemandPasswordReset(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Success);
            AssertReturnType<DemandPasswordResetResponse>(result);
            MockUserRepository.Verify_Select();
            MockUserRepository.Verify_Update();
        }

        [Test]
        public async Task OrganizationService_DemandPasswordReset_Failed_UserNotFound()
        {
            // arrange
            var request = GetDemandPasswordResetRequest();
            MockUserRepository.Setup_Select_Returns_OrganizationOneUserOneNotExist();

            // act
            var result = await SystemUnderTest.DemandPasswordReset(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Failed, UserNotFound);
            AssertReturnType<DemandPasswordResetResponse>(result);
            MockUserRepository.Verify_Select();
        }

        [Test]
        public async Task OrganizationService_DemandPasswordReset_Invalid_UserNotActive()
        {
            // arrange
            var request = GetDemandPasswordResetRequest();
            MockUserRepository.Setup_Select_Returns_OrganizationOneUserOneNotActive();

            // act
            var result = await SystemUnderTest.DemandPasswordReset(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Invalid, UserNotActive);
            AssertReturnType<DemandPasswordResetResponse>(result);
            MockUserRepository.Verify_Select();
        }

        [Test]
        public async Task OrganizationService_DemandPasswordReset_InvalidAlreadyRequested()
        {
            // arrange
            var request = GetDemandPasswordResetRequest();
            MockUserRepository.Setup_Select_Returns_OrganizationOneUserOnePasswordResetRequestedAtOneMinuteBefore();

            // act
            var result = await SystemUnderTest.DemandPasswordReset(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Invalid, "already_requested_password_reset_in_last_two_minutes");
            AssertReturnType<DemandPasswordResetResponse>(result);
            MockUserRepository.Verify_Select();
        }

        [Test]
        public async Task OrganizationService_DemandPasswordReset_Failed()
        {
            // arrange
            var request = GetDemandPasswordResetRequest();
            MockUserRepository.Setup_Select_Returns_OrganizationOneUserOnePasswordResetRequestedAtFiveMinutesBefore();
            MockUserRepository.Setup_Update_Failed();

            // act
            var result = await SystemUnderTest.DemandPasswordReset(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Failed);
            AssertReturnType<DemandPasswordResetResponse>(result);
            MockUserRepository.Verify_Select();
            MockUserRepository.Verify_Update();
        }

        [Test]
        public async Task OrganizationService_ValidatePasswordReset_Success()
        {
            // arrange
            var request = GetPasswordResetValidateRequest();
            MockUserRepository.Setup_Select_Returns_OrganizationOneUserOnePasswordResetRequestedAtFiveMinutesBefore();

            // act
            var result = await SystemUnderTest.ValidatePasswordReset(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Success);
            AssertReturnType<PasswordResetValidateResponse>(result);
            MockUserRepository.Verify_Select();
        }

        [Test]
        public async Task OrganizationService_ValidatePasswordReset_Failed_UserNotExist()
        {
            // arrange
            var request = GetPasswordResetValidateRequest();
            MockUserRepository.Setup_Select_Returns_OrganizationOneUserOneNotExist();

            // act
            var result = await SystemUnderTest.ValidatePasswordReset(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Failed);
            AssertReturnType<PasswordResetValidateResponse>(result);
            MockUserRepository.Verify_Select();
        }

        [Test]
        public async Task OrganizationService_ValidatePasswordReset_Failed_PasswordResetRequestedAtTwoDaysBefore()
        {
            // arrange
            var request = GetPasswordResetValidateRequest();
            MockUserRepository.Setup_Select_Returns_OrganizationOneUserOnePasswordResetRequestedAtTwoDaysBefore();

            // act
            var result = await SystemUnderTest.ValidatePasswordReset(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Failed);
            AssertReturnType<PasswordResetValidateResponse>(result);
            MockUserRepository.Verify_Select();
        }

        [Test]
        public async Task OrganizationService_PasswordReset_Success()
        {
            // arrange
            var request = GetPasswordResetRequest();
            MockUserRepository.Setup_Select_Returns_OrganizationOneUserOnePasswordResetRequestedAtFiveMinutesBefore();
            MockUserRepository.Setup_Update_Success();

            // act
            var result = await SystemUnderTest.PasswordReset(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Success);
            AssertReturnType<PasswordResetResponse>(result);
            MockUserRepository.Verify_Select();
            MockUserRepository.Verify_Update();
        }

        [Test]
        public async Task OrganizationService_PasswordReset_Failed()
        {
            // arrange
            var request = GetPasswordResetRequest();
            MockUserRepository.Setup_Select_Returns_OrganizationOneUserOnePasswordResetRequestedAtFiveMinutesBefore();
            MockUserRepository.Setup_Update_Failed();

            // act
            var result = await SystemUnderTest.PasswordReset(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Failed);
            AssertReturnType<PasswordResetResponse>(result);
            MockUserRepository.Verify_Select();
            MockUserRepository.Verify_Update();
        }

        [Test]
        public async Task OrganizationService_PasswordReset_Failed_PasswordResetRequestedAtTwoDaysBefore()
        {
            // arrange
            var request = GetPasswordResetRequest();
            MockUserRepository.Setup_Select_Returns_OrganizationOneUserOnePasswordResetRequestedAtTwoDaysBefore();

            // act
            var result = await SystemUnderTest.PasswordReset(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Failed);
            AssertReturnType<PasswordResetResponse>(result);
            MockUserRepository.Verify_Select();
        }

        [Test]
        public async Task OrganizationService_ChangePassword_Success()
        {
            // arrange
            var request = GetPasswordChangeRequest();
            MockUserRepository.Setup_Select_Returns_OrganizationOneUserOne();
            MockUserRepository.Setup_Update_Success();
            MockUserRepository.Setup_SelectRevisions_Returns_OrganizationOneUserOneRevisions();

            // act
            var result = await SystemUnderTest.ChangePassword(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Success);
            AssertReturnType<PasswordChangeResponse>(result);
            MockUserRepository.Verify_Select();
            MockUserRepository.Verify_Update();
            MockUserRepository.Verify_SelectRevisions();
        }

        [Test]
        public async Task OrganizationService_ChangePassword_Failed()
        {
            // arrange
            var request = GetPasswordChangeRequest();
            MockUserRepository.Setup_Select_Returns_OrganizationOneUserOne();
            MockUserRepository.Setup_SelectRevisions_Returns_OrganizationOneUserOneRevisions();
            MockUserRepository.Setup_Update_Failed();

            // act
            var result = await SystemUnderTest.ChangePassword(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Failed);
            AssertReturnType<PasswordChangeResponse>(result);
            MockUserRepository.Verify_Select();
            MockUserRepository.Verify_Update();
            MockUserRepository.Verify_SelectRevisions();
        }

        [Test]
        public async Task OrganizationService_ChangePassword_Failed_UserNotFound()
        {
            // arrange
            var request = GetPasswordChangeRequest();
            MockUserRepository.Setup_Select_Returns_OrganizationOneUserOneNotExist();

            // act
            var result = await SystemUnderTest.ChangePassword(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Failed, UserNotFound);
            AssertReturnType<PasswordChangeResponse>(result);
            MockUserRepository.Verify_Select();
        }

        [Test]
        public async Task OrganizationService_ChangePassword_Invalid_UserNotActive()
        {
            // arrange
            var request = GetPasswordChangeRequest();
            MockUserRepository.Setup_Select_Returns_OrganizationOneUserOneNotActive();

            // act
            var result = await SystemUnderTest.ChangePassword(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Invalid, UserNotActive);
            AssertReturnType<PasswordChangeResponse>(result);
            MockUserRepository.Verify_Select();
        }

        [Test]
        public async Task OrganizationService_ChangePassword_Failed_OldPasswordIsNotRight()
        {
            // arrange
            var request = GetPasswordChangeRequest(PasswordTwo, PasswordOne);
            MockUserRepository.Setup_Select_Returns_OrganizationOneUserOne();

            // act
            var result = await SystemUnderTest.ChangePassword(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Failed, "old_password_is_not_right");
            AssertReturnType<PasswordChangeResponse>(result);
            MockUserRepository.Verify_Select();
        }

        [Test]
        public async Task OrganizationService_ChangePassword_Failed_ChooseOtherPasswordDifferentThanLastTwo()
        {
            // arrange
            var request = GetPasswordChangeRequest(PasswordOne, PasswordOne);
            MockUserRepository.Setup_Select_Returns_OrganizationOneUserOne();
            MockUserRepository.Setup_SelectRevisions_Returns_OrganizationOneUserOneRevisions();

            // act
            var result = await SystemUnderTest.ChangePassword(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Failed, "choose_other_password_different_then_last_2");
            AssertReturnType<PasswordChangeResponse>(result);
            MockUserRepository.Verify_Select();
            MockUserRepository.Verify_SelectRevisions();
        }

        [Test]
        public async Task OrganizationService_ChangeActivationForUser_Success()
        {
            // arrange
            var request = GetUserChangeActivationRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneAdminUserOne();
            MockOrganizationRepository.Setup_Any_Returns_False();
            MockUserRepository.Setup_Update_Success();

            // act
            var result = await SystemUnderTest.ChangeActivationForUser(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Success);
            AssertReturnType<UserChangeActivationResponse>(result);
            MockUserRepository.Verify_SelectById();
            MockOrganizationRepository.Verify_Any();
            MockUserRepository.Verify_Update();
        }

        [Test]
        public async Task OrganizationService_ChangeActivationForUser_Invalid_CurrentUserNotAdmin()
        {
            // arrange
            var request = GetUserChangeActivationRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneUserOne();

            // act
            var result = await SystemUnderTest.ChangeActivationForUser(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Invalid, UserNotAdmin);
            AssertReturnType<UserChangeActivationResponse>(result);
            MockUserRepository.Verify_SelectById();
        }

        [Test]
        public async Task OrganizationService_ChangeActivationForUser_Invalid_OrganizationNotActive()
        {
            // arrange
            var request = GetUserChangeActivationRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneAdminUserOne();
            MockOrganizationRepository.Setup_Any_Returns_True();

            // act
            var result = await SystemUnderTest.ChangeActivationForUser(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Invalid, OrganizationNotActive);
            AssertReturnType<UserChangeActivationResponse>(result);
            MockUserRepository.Verify_SelectById();
            MockOrganizationRepository.Verify_Any();
        }

        [Test]
        public async Task OrganizationService_ChangeActivationForUser_Invalid_OrganizationNotMatch()
        {
            // arrange
            var request = GetUserChangeActivationRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneAdminUserOne();
            MockOrganizationRepository.Setup_Any_Returns_False();
            MockUserRepository.Setup_Select_Returns_OrganizationTwoUserOne();

            // act
            var result = await SystemUnderTest.ChangeActivationForUser(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Invalid);
            AssertReturnType<UserChangeActivationResponse>(result);
            MockUserRepository.Verify_SelectById();
            MockOrganizationRepository.Verify_Any();
            MockUserRepository.Verify_Select();
        }

        [Test]
        public async Task OrganizationService_ChangeActivationForUser_Failed()
        {
            // arrange
            var request = GetUserChangeActivationRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneAdminUserOne();
            MockOrganizationRepository.Setup_Any_Returns_False();
            MockUserRepository.Setup_Update_Failed();

            // act
            var result = await SystemUnderTest.ChangeActivationForUser(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Failed);
            AssertReturnType<UserChangeActivationResponse>(result);
            MockUserRepository.Verify_SelectById();
            MockOrganizationRepository.Verify_Any();
            MockUserRepository.Verify_Update();
        }

        [Test]
        public async Task OrganizationService_EditUser_Success()
        {
            // arrange
            var request = GetUserEditRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneAdminUserOne();
            MockOrganizationRepository.Setup_Any_Returns_False();
            MockLanguageRepository.Setup_Select_Returns_Language();
            MockUserRepository.Setup_Update_Success();
            MockLanguageRepository.Setup_SelectById_Returns_Language();

            // act
            var result = await SystemUnderTest.EditUser(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Success);
            AssertReturnType<UserEditResponse>(result);
            MockUserRepository.Verify_SelectById();
            MockOrganizationRepository.Verify_Any();
            MockLanguageRepository.Verify_Select();
            MockUserRepository.Verify_Update();
            MockLanguageRepository.Verify_Select();
        }

        [Test]
        public async Task OrganizationService_EditUser_NotDifferent_Success()
        {
            // arrange
            var request = GetNotDifferentUserEditRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneAdminUserOne();
            MockOrganizationRepository.Setup_Any_Returns_False();

            // act
            var result = await SystemUnderTest.EditUser(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Success);
            AssertReturnType<UserEditResponse>(result);
            MockUserRepository.Verify_SelectById();
            MockOrganizationRepository.Verify_Any();
        }

        [Test]
        public async Task OrganizationService_EditUser_Failed()
        {
            // arrange
            var request = GetUserEditRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneAdminUserOne();
            MockOrganizationRepository.Setup_Any_Returns_False();
            MockLanguageRepository.Setup_Select_Returns_Language();
            MockUserRepository.Setup_Update_Failed();
            // act
            var result = await SystemUnderTest.EditUser(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Failed);
            AssertReturnType<UserEditResponse>(result);
            MockUserRepository.Verify_SelectById();
            MockOrganizationRepository.Verify_Any();
            MockLanguageRepository.Verify_Select();
            MockUserRepository.Verify_Update();
        }

        [Test]
        public async Task OrganizationService_EditUser_Invalid_CurrentUserNotAdmin()
        {
            // arrange
            var request = GetUserEditRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneUserOne();

            // act
            var result = await SystemUnderTest.EditUser(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Invalid, UserNotAdmin);
            AssertReturnType<UserEditResponse>(result);
            MockUserRepository.Verify_SelectById();
        }

        [Test]
        public async Task OrganizationService_EditUser_Invalid_OrganizationNotActive()
        {
            // arrange
            var request = GetUserEditRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneAdminUserOne();
            MockOrganizationRepository.Setup_Any_Returns_True();

            // act
            var result = await SystemUnderTest.EditUser(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Invalid, OrganizationNotActive);
            AssertReturnType<UserEditResponse>(result);
            MockUserRepository.Verify_SelectById();
            MockOrganizationRepository.Verify_Any();
        }

        [Test]
        public async Task OrganizationService_EditUser_Invalid_OrganizationNotMatch()
        {
            // arrange
            var request = GetUserEditRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationTwoAdminUserOne();
            MockOrganizationRepository.Setup_Any_Returns_False();
            MockUserRepository.Setup_Select_Returns_OrganizationOneUserOne();

            // act
            var result = await SystemUnderTest.EditUser(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Invalid);
            AssertReturnType<UserEditResponse>(result);
            MockUserRepository.Verify_SelectById();
            MockOrganizationRepository.Verify_Any();
            MockUserRepository.Verify_Select();
        }

        [Test]
        public async Task IntegrationService_EditUser_Failed_IntegrationNotFound()
        {
            // arrange
            var request = GetUserEditRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneAdminUserOne();
            MockOrganizationRepository.Setup_Any_Returns_False();
            MockLanguageRepository.Setup_Select_Returns_LanguageNotExist();

            // act
            var result = await SystemUnderTest.EditUser(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Failed, LanguageNotFound);
            AssertReturnType<UserEditResponse>(result);
            MockUserRepository.Verify_SelectById();
            MockOrganizationRepository.Verify_Any();
            MockLanguageRepository.Verify_Select();
        }

        [Test]
        public async Task OrganizationService_DeleteUser_Success()
        {
            // arrange
            var request = GetUserDeleteRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneAdminUserOne();
            MockOrganizationRepository.Setup_Any_Returns_False();
            MockUserRepository.Setup_Delete_Success();

            // act
            var result = await SystemUnderTest.DeleteUser(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Success);
            AssertReturnType<UserDeleteResponse>(result);
            MockUserRepository.Verify_SelectById();
            MockOrganizationRepository.Verify_Any();
            MockUserRepository.Verify_Delete();
        }

        [Test]
        public async Task OrganizationService_DeleteUser_Failed()
        {
            // arrange
            var request = GetUserDeleteRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneAdminUserOne();
            MockOrganizationRepository.Setup_Any_Returns_False();
            MockUserRepository.Setup_Delete_Failed();

            // act
            var result = await SystemUnderTest.DeleteUser(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Failed);
            AssertReturnType<UserDeleteResponse>(result);
            MockUserRepository.Verify_SelectById();
            MockOrganizationRepository.Verify_Any();
            MockUserRepository.Verify_Delete();
        }

        [Test]
        public async Task OrganizationService_DeleteUser_Invalid_CurrentUserNotAdmin()
        {
            // arrange
            var request = GetUserDeleteRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneUserOne();

            // act
            var result = await SystemUnderTest.DeleteUser(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Invalid, UserNotAdmin);
            AssertReturnType<UserDeleteResponse>(result);
            MockUserRepository.Verify_SelectById();
        }

        [Test]
        public async Task OrganizationService_DeleteUser_Invalid_OrganizationNotActive()
        {
            // arrange
            var request = GetUserDeleteRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneAdminUserOne();
            MockOrganizationRepository.Setup_Any_Returns_True();

            // act
            var result = await SystemUnderTest.DeleteUser(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Invalid, OrganizationNotActive);
            AssertReturnType<UserDeleteResponse>(result);
            MockUserRepository.Verify_SelectById();
            MockOrganizationRepository.Verify_Any();
        }

        [Test]
        public async Task OrganizationService_DeleteUser_Invalid_OrganizationNotMatch()
        {
            // arrange
            var request = GetUserDeleteRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationTwoAdminUserOne();
            MockOrganizationRepository.Setup_Any_Returns_False();
            MockUserRepository.Setup_Select_Returns_OrganizationOneUserOne();

            // act
            var result = await SystemUnderTest.DeleteUser(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Invalid);
            AssertReturnType<UserDeleteResponse>(result);
            MockUserRepository.Verify_SelectById();
            MockOrganizationRepository.Verify_Any();
            MockUserRepository.Verify_Select();
        }

        [Test]
        public async Task OrganizationService_InviteUser_Success()
        {
            // arrange
            var request = GetUserInviteRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneAdminUserOne();
            MockOrganizationRepository.Setup_Any_Returns_False();
            MockUserRepository.Setup_Select_Returns_OrganizationOneUserOneNotExist();
            MockUserRepository.Setup_Insert_Success();

            // act
            var result = await SystemUnderTest.InviteUser(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Success);
            AssertReturnType<UserInviteResponse>(result);
            MockUserRepository.Verify_SelectById();
            MockOrganizationRepository.Verify_Any();
            MockUserRepository.Verify_Select();
            MockUserRepository.Verify_Insert();
        }

        [Test]
        public async Task OrganizationService_InviteUser_Invalid_CurrentUserNotAdmin()
        {
            // arrange
            var request = GetUserInviteRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneUserOne();

            // act
            var result = await SystemUnderTest.InviteUser(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Invalid, UserNotAdmin);
            AssertReturnType<UserInviteResponse>(result);
            MockUserRepository.Verify_SelectById();
        }

        [Test]
        public async Task OrganizationService_InviteUser_Invalid_OrganizationNotActive()
        {
            // arrange
            var request = GetUserInviteRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneAdminUserOne();
            MockOrganizationRepository.Setup_Any_Returns_True();

            // act
            var result = await SystemUnderTest.InviteUser(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Invalid, OrganizationNotActive);
            AssertReturnType<UserInviteResponse>(result);
            MockUserRepository.Verify_SelectById();
            MockOrganizationRepository.Verify_Any();
        }

        [Test]
        public async Task OrganizationService_InviteUser_Invalid_EmailAlreadyInvited()
        {
            // arrange
            var request = GetUserInviteRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneAdminUserOne();
            MockOrganizationRepository.Setup_Any_Returns_False();
            MockUserRepository.Setup_Select_Returns_OrganizationOneUserOne();

            // act
            var result = await SystemUnderTest.InviteUser(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Invalid, "email_already_invited");
            AssertReturnType<UserInviteResponse>(result);
            MockUserRepository.Verify_SelectById();
            MockOrganizationRepository.Verify_Any();
            MockUserRepository.Verify_Select();
        }

        [Test]
        public async Task OrganizationService_InviteUser_Failed()
        {
            // arrange
            var request = GetUserInviteRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneAdminUserOne();
            MockOrganizationRepository.Setup_Any_Returns_False();
            MockUserRepository.Setup_Select_Returns_OrganizationOneUserOneNotExist();
            MockUserRepository.Setup_Insert_Failed();

            // act
            var result = await SystemUnderTest.InviteUser(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Failed);
            AssertReturnType<UserInviteResponse>(result);
            MockUserRepository.Verify_SelectById();
            MockOrganizationRepository.Verify_Any();
            MockUserRepository.Verify_Select();
            MockUserRepository.Verify_Insert();
        }

        [Test]
        public async Task OrganizationService_ValidateUserInvitation_Success()
        {
            // arrange
            var request = GetUserInviteValidateRequest();
            MockUserRepository.Setup_Select_Returns_OrganizationOneUserOneInvitedAtOneDayBefore();

            // act
            var result = await SystemUnderTest.ValidateUserInvitation(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Success);
            AssertReturnType<UserInviteValidateResponse>(result);
            MockUserRepository.Verify_Select();
        }

        [Test]
        public async Task OrganizationService_ValidateUserInvitation_Failed_UserNotExist()
        {
            // arrange
            var request = GetUserInviteValidateRequest();
            MockUserRepository.Setup_Select_Returns_OrganizationOneUserOneNotExist();

            // act
            var result = await SystemUnderTest.ValidateUserInvitation(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Failed);
            AssertReturnType<UserInviteValidateResponse>(result);
            MockUserRepository.Verify_Select();
        }

        [Test]
        public async Task OrganizationService_ValidateUserInvitation_Failed_UserInvitedAtOneWeekBefore()
        {
            // arrange
            var request = GetUserInviteValidateRequest();
            MockUserRepository.Setup_Select_Returns_OrganizationOneUserOneInvitedAtOneWeekBefore();

            // act
            var result = await SystemUnderTest.ValidateUserInvitation(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Failed);
            AssertReturnType<UserInviteValidateResponse>(result);
            MockUserRepository.Verify_Select();
        }

        [Test]
        public async Task OrganizationService_AcceptInvitation_Success()
        {
            // arrange
            var request = GetUserAcceptInviteRequest();
            MockUserRepository.Setup_Select_Returns_OrganizationOneUserOneInvitedAtOneDayBefore();
            MockUserRepository.Setup_Update_Success();
            MockLanguageRepository.Setup_Select_Returns_Language();
            MockOrganizationRepository.Setup_Update_Success();

            // act
            var result = await SystemUnderTest.AcceptInvitation(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Success);
            AssertReturnType<UserAcceptInviteResponse>(result);
            MockUserRepository.Verify_Select();
            MockUserRepository.Verify_Update();
            MockLanguageRepository.Verify_Select();
            MockOrganizationRepository.Verify_Update();
        }

        [Test]
        public async Task OrganizationService_AcceptInvitation_Failed_UserNotExist()
        {
            // arrange
            var request = GetUserAcceptInviteRequest();
            MockUserRepository.Setup_Select_Returns_OrganizationOneUserOneNotExist();

            // act
            var result = await SystemUnderTest.AcceptInvitation(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Failed);
            AssertReturnType<UserAcceptInviteResponse>(result);
            MockUserRepository.Verify_Select();
        }

        [Test]
        public async Task OrganizationService_AcceptInvitation_Failed_UserInvitedAtOneWeekBefore()
        {
            // arrange
            var request = GetUserAcceptInviteRequest();
            MockUserRepository.Setup_Select_Returns_OrganizationOneUserOneInvitedAtOneWeekBefore();

            // act
            var result = await SystemUnderTest.AcceptInvitation(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Failed);
            AssertReturnType<UserAcceptInviteResponse>(result);
            MockUserRepository.Verify_Select();
        }

        [Test]
        public async Task OrganizationService_AcceptInvitation_Failed()
        {
            // arrange
            var request = GetUserAcceptInviteRequest();
            MockUserRepository.Setup_Select_Returns_OrganizationOneUserOneInvitedAtOneDayBefore();
            MockLanguageRepository.Setup_Select_Returns_Language();
            MockUserRepository.Setup_Update_Success();
            MockOrganizationRepository.Setup_Update_Failed();

            // act
            var result = await SystemUnderTest.AcceptInvitation(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Failed);
            AssertReturnType<UserAcceptInviteResponse>(result);
            MockUserRepository.Verify_Select();
            MockLanguageRepository.Verify_Select();
            MockUserRepository.Verify_Update();
            MockOrganizationRepository.Verify_Update();
        }

        [Test]
        public void OrganizationService_GetUser_Success()
        {
            // arrange
            var request = GetUserReadRequest();
            MockUserRepository.Setup_Select_Returns_OrganizationOneUserOne();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneUserOne();
            // act
            var result = SystemUnderTest.GetUser(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Success);
            AssertReturnType<UserReadResponse>(result);
            MockUserRepository.Verify_Select();
            MockUserRepository.Verify_SelectById();

        }

        [Test]
        public void OrganizationService_GetUser_Invalid_OrganizationNotMatch()
        {
            // arrange
            var request = GetUserReadRequest();

            MockUserRepository.Setup_SelectById_Returns_OrganizationTwoUserOne();

            // act
            var result = SystemUnderTest.GetUser(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Invalid);
            AssertReturnType<UserReadResponse>(result);
            MockUserRepository.Verify_SelectById();
        }

        [Test]
        public async Task OrganizationService_GetUsers_Success_SelectAfter()
        {
            // arrange
            var request = GetUserReadListRequestForSelectAfter();
            MockUserRepository.Setup_SelectAfter_Returns_Users();

            MockUserRepository.Setup_Count_Returns_Ten();

            // act
            var result = await SystemUnderTest.GetUsers(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Success);
            AssertReturnType<UserReadListResponse>(result);
            AssertPagingInfoForSelectAfter(request.PagingInfo, Ten);
            MockUserRepository.Verify_SelectAfter();
            MockUserRepository.Verify_Count();

        }

        [Test]
        public async Task OrganizationService_GetUsers_Success_SelectMany()
        {
            // arrange
            var request = GetUserReadListRequestForSelectMany();
            MockUserRepository.Setup_SelectMany_Returns_Users();
            MockUserRepository.Setup_Count_Returns_Ten();

            // act
            var result = await SystemUnderTest.GetUsers(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Success);
            AssertReturnType<UserReadListResponse>(result);
            AssertPagingInfoForSelectMany(request.PagingInfo, Ten);
            MockUserRepository.Verify_SelectMany();
            MockUserRepository.Verify_Count();
        }

        [Test]
        public async Task OrganizationService_GetUserRevisions_Success()
        {
            // arrange
            var request = GetUserRevisionReadListRequest();
            MockUserRepository.Setup_Select_Returns_OrganizationOneUserOne();
            MockUserRepository.Setup_SelectRevisions_Returns_OrganizationOneUserOneRevisions();

            // act
            var result = await SystemUnderTest.GetUserRevisions(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Success);
            AssertReturnType<UserRevisionReadListResponse>(result);
            MockUserRepository.Verify_Select();
            MockUserRepository.Verify_SelectRevisions();
        }

        [Test]
        public async Task OrganizationService_GetUserRevisions_Failed_UserNotFound()
        {
            // arrange
            var request = GetUserRevisionReadListRequest();
            MockUserRepository.Setup_Select_Returns_OrganizationOneUserOneNotExist();

            // act
            var result = await SystemUnderTest.GetUserRevisions(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Failed, UserNotFound);
            AssertReturnType<UserRevisionReadListResponse>(result);
            MockUserRepository.Verify_Select();
        }

        [Test]
        public async Task OrganizationService_GetUserLoginLogs_Success_SelectAfter()
        {
            // arrange
            var request = GetUserLoginLogReadListRequestForSelectAfter();
            MockUserRepository.Setup_Select_Returns_OrganizationOneAdminUserOne();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneAdminUserOne();
            MockUserLoginLogRepository.Setup_SelectAfter_Returns_UserLoginLogs();
            MockUserLoginLogRepository.Setup_Count_Returns_Ten();

            // act
            var result = await SystemUnderTest.GetUserLoginLogs(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Success);
            AssertReturnType<UserLoginLogReadListResponse>(result);
            AssertPagingInfoForSelectAfter(request.PagingInfo, Ten);
            MockUserRepository.Verify_Select();
            MockUserRepository.Verify_SelectById();
            MockUserLoginLogRepository.Verify_SelectAfter();
            MockUserLoginLogRepository.Verify_Count();
        }

        [Test]
        public async Task OrganizationService_GetUserLoginLogs_Success_SelectMany()
        {
            // arrange
            var request = GetUserLoginLogReadListRequestForSelectMany();
            MockUserRepository.Setup_Select_Returns_OrganizationOneAdminUserOne();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneAdminUserOne();
            MockUserLoginLogRepository.Setup_SelectMany_Returns_UserLoginLogs();
            MockUserRepository.Setup_Count_Returns_Ten();

            // act
            var result = await SystemUnderTest.GetUserLoginLogs(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Success);
            AssertReturnType<UserLoginLogReadListResponse>(result);
            AssertPagingInfoForSelectMany(request.PagingInfo, Ten);
            MockUserRepository.Verify_Select();
            MockUserRepository.Verify_SelectById();
            MockUserLoginLogRepository.Verify_SelectMany();
            MockUserLoginLogRepository.Verify_Count();
        }

        [Test]
        public async Task OrganizationService_GetUserLoginLogs_Invalid_OrganizationNotMatch()
        {
            // arrange
            var request = GetUserLoginLogReadListRequest();
            MockUserRepository.Setup_Select_Returns_OrganizationOneUserOne();
            MockUserRepository.Setup_SelectById_Returns_OrganizationTwoUserOne();

            // act
            var result = await SystemUnderTest.GetUserLoginLogs(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Invalid);
            AssertReturnType<UserLoginLogReadListResponse>(result);
            MockUserRepository.Verify_Select();
            MockUserRepository.Verify_SelectById();
        }

        [Test]
        public async Task OrganizationService_GetUserLoginLogsOfOrganization_Success_SelectAfter()
        {
            // arrange
            var request = GetOrganizationLoginLogReadListRequestForSelectAfter();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneUserOne();
            MockOrganizationRepository.Setup_Select_Returns_OrganizationOne();
            MockUserLoginLogRepository.Setup_SelectAfter_Returns_UserLoginLogs();
            MockUserRepository.Setup_Count_Returns_Ten();

            // act
            var result = await SystemUnderTest.GetUserLoginLogsOfOrganization(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Success);
            AssertReturnType<OrganizationLoginLogReadListResponse>(result);
            AssertPagingInfoForSelectAfter(request.PagingInfo, Ten);
            MockUserRepository.Verify_SelectById();
            MockOrganizationRepository.Verify_Select();
            MockUserLoginLogRepository.Verify_SelectAfter();
            MockUserLoginLogRepository.Verify_Count();

        }

        [Test]
        public async Task OrganizationService_GetUserLoginLogsOfOrganization_Success_SelectMany()
        {
            // arrange
            var request = GetOrganizationLoginLogReadListRequestForSelectMany();
            MockUserLoginLogRepository.Setup_SelectMany_Returns_UserLoginLogs();
            MockUserRepository.Setup_Count_Returns_Ten();

            // act
            var result = await SystemUnderTest.GetUserLoginLogsOfOrganization(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Success);
            AssertReturnType<OrganizationLoginLogReadListResponse>(result);
            AssertPagingInfoForSelectMany(request.PagingInfo, Ten);
            MockUserLoginLogRepository.Verify_SelectMany();
            MockUserLoginLogRepository.Verify_Count();

        }

        [Test]
        public async Task OrganizationService_GetUserLoginLogsOfOrganization_Invalid_OrganizationNotMatch()
        {
            // arrange
            var request = GetOrganizationLoginLogReadListRequest();
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneUserOne();
            MockOrganizationRepository.Setup_Select_Returns_OrganizationTwo();

            // act
            var result = await SystemUnderTest.GetUserLoginLogsOfOrganization(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Invalid);
            AssertReturnType<OrganizationLoginLogReadListResponse>(result);
            MockUserRepository.Verify_SelectById();
            MockOrganizationRepository.Verify_Select();
        }

        [Test]
        public async Task OrganizationService_RestoreUser_Success()
        {
            // arrange
            var request = GetUserRestoreRequest(OrganizationOneUserOneUid, One);
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneUserOne();
            MockOrganizationRepository.Setup_Any_Returns_False();
            MockUserRepository.Setup_Select_Returns_OrganizationOneUserOne();
            MockUserRepository.Setup_SelectRevisions_Returns_OrganizationOneUserOneRevisions();
            MockUserRepository.Setup_RestoreRevision_Returns_True();

            // act
            var result = await SystemUnderTest.RestoreUser(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Success);
            AssertReturnType<UserRestoreResponse>(result);
            MockUserRepository.Verify_SelectById();
            MockOrganizationRepository.Verify_Any();
            MockUserRepository.Verify_Select();
            MockUserRepository.Verify_SelectRevisions();
            MockUserRepository.Verify_RestoreRevision();
        }

        [Test]
        public async Task OrganizationService_RestoreUser_Invalid_OrganizationNotActive()
        {
            // arrange
            var request = GetUserRestoreRequest(OrganizationOneUserOneUid, One);
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneUserOne();
            MockOrganizationRepository.Setup_Any_Returns_True();

            // act
            var result = await SystemUnderTest.RestoreUser(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Invalid, OrganizationNotActive);
            AssertReturnType<UserRestoreResponse>(result);
            MockUserRepository.Verify_SelectById();
            MockOrganizationRepository.Verify_Any();
        }

        [Test]
        public async Task OrganizationService_RestoreUser_Failed_UserNotFound()
        {
            // arrange
            var request = GetUserRestoreRequest(OrganizationOneUserOneUid, One);
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneUserOne();
            MockOrganizationRepository.Setup_Any_Returns_False();
            MockUserRepository.Setup_Select_Returns_OrganizationOneUserOneNotExist();

            // act
            var result = await SystemUnderTest.RestoreUser(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Failed, UserNotFound);
            AssertReturnType<UserRestoreResponse>(result);
            MockUserRepository.Verify_SelectById();
            MockOrganizationRepository.Verify_Any();
            MockUserRepository.Verify_Select();
        }

        [Test]
        public async Task OrganizationService_RestoreUser_Failed_UserRevisionNotFound()
        {
            // arrange
            var request = GetUserRestoreRequest(OrganizationOneUserOneUid, One);
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneUserOne();
            MockOrganizationRepository.Setup_Any_Returns_False();
            MockUserRepository.Setup_Select_Returns_OrganizationOneUserOne();
            MockUserRepository.Setup_SelectRevisions_Returns_OrganizationOneUserOneRevisionsRevisionTwoInIt();

            // act
            var result = await SystemUnderTest.RestoreUser(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Failed, UserRevisionNotFound);
            AssertReturnType<UserRestoreResponse>(result);
            MockUserRepository.Verify_SelectById();
            MockOrganizationRepository.Verify_Any();
            MockUserRepository.Verify_Select();
            MockUserRepository.Verify_SelectRevisions();
        }

        [Test]
        public async Task OrganizationService_RestoreUser_Failed()
        {
            // arrange
            var request = GetUserRestoreRequest(OrganizationOneUserOneUid, One);
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneUserOne();
            MockOrganizationRepository.Setup_Any_Returns_False();
            MockUserRepository.Setup_Select_Returns_OrganizationOneUserOne();
            MockUserRepository.Setup_SelectRevisions_Returns_OrganizationOneUserOneRevisions();
            MockUserRepository.Setup_RestoreRevision_Returns_False();

            // act
            var result = await SystemUnderTest.RestoreUser(request);

            // assert
            AssertResponseStatusAndErrorMessages(result, ResponseStatus.Failed);
            AssertReturnType<UserRestoreResponse>(result);
            MockUserRepository.Verify_SelectById();
            MockOrganizationRepository.Verify_Any();
            MockUserRepository.Verify_Select();
            MockUserRepository.Verify_SelectRevisions();
            MockUserRepository.Verify_RestoreRevision();
        }

        [Test]
        public void OrganizationService_GetCurrentUser_Success()
        {
            // arrange
            var request = GetCurrentUserRequest();
            MockUserRepository.Setup_Select_Returns_OrganizationOneUserOne();

            // act
            var result = SystemUnderTest.GetCurrentUser(request);

            // assert
            result.ShouldNotBeNull();
            AssertReturnType<CurrentUser>(result);
            MockUserRepository.Verify_Select();
        }

        [Test]
        public void OrganizationService_GetCurrentUser_Failed_CurrentUserNotMapped()
        {
            // arrange
            var request = GetCurrentUserRequest();
            MockUserRepository.Setup_Select_Returns_OrganizationOneUserOneNotExist();

            // act
            void Result() => SystemUnderTest.GetCurrentUser(request);

            // assert
            Assert.Throws<ApplicationException>(Result).Message.ShouldBe("Current user not mapped!");
            MockUserRepository.Verify_Select();
        }

        [Test]
        public async Task OrganizationService_LoadOrganizationsToCache()
        {
            // arrange
            MockOrganizationRepository.Setup_SelectAll_Returns_Organizations();

            // act
            var result = await SystemUnderTest.LoadOrganizationsToCache();

            // assert
            result.ShouldNotBeNull();
            result.ShouldBeTrue();
            MockOrganizationRepository.Verify_SelectAll();
        }

        [Test]
        public async Task OrganizationService_LoadUsersToCache()
        {
            // arrange
            MockUserRepository.Setup_SelectAll_Returns_Users();

            // act
            var result = await SystemUnderTest.LoadUsersToCache();

            // assert
            result.ShouldNotBeNull();
            result.ShouldBeTrue();
            MockUserRepository.Verify_SelectAll();
        }
    }
}