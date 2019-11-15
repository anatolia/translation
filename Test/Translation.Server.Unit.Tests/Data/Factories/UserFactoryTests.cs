using NUnit.Framework;
using Shouldly;

using Translation.Data.Factories;
using static Translation.Server.Unit.Tests.TestHelpers.FakeEntityTestHelper;
using static Translation.Common.Tests.TestHelpers.FakeRequestTestHelper;
using static Translation.Common.Tests.TestHelpers.FakeConstantTestHelper;

namespace Translation.Server.Unit.Tests.Data.Factories
{
    [TestFixture]
    public class UserFactoryTests
    {
        public UserFactory UserFactory { get; set; }

        [SetUp]
        public void run_before_every_test()
        {
            UserFactory = new UserFactory();
        }

        [Test]
        public void UserFactory_CreateEntityFromRequest_AdminDemoteRequest()
        {
            // arrange
            var request = GetAdminDemoteRequest();
            var user = GetUser();

            // act
            var result = UserFactory.CreateEntityFromRequest(request, user);

            // assert
            result.UpdatedBy.ShouldBe(request.CurrentUserId);
            result.IsAdmin.ShouldBe(BooleanFalse);
        }

        [Test]
        public void UserFactory_CreateEntityFromRequest_AdminUpgradeRequest()
        {
            // arrange
            var request = GetAdminUpgradeRequest();
            var user = GetUser();

            // act
            var result = UserFactory.CreateEntityFromRequest(request, user);

            // assert
            result.UpdatedBy.ShouldBe(request.CurrentUserId);
            result.IsAdmin.ShouldBe(BooleanTrue);
        }

        [Test]
        public void UserFactory_CreateEntityFromRequest_SignUpRequest()
        {
            // arrange
            var request = GetSignUpRequest();
            var organization = GetOrganization();

            // act
            var result = UserFactory.CreateEntityFromRequest(request, organization, StringOne, StringTwo);

            // assert
            result.Email.ShouldBe(request.Email);
            result.FirstName.ShouldBe(request.FirstName);
            result.LastName.ShouldBe(request.LastName);
            result.Name.ShouldBe(UserFactory.MapName(request.FirstName, request.LastName));
            result.IsActive.ShouldBe(BooleanTrue);
            result.IsAdmin.ShouldBe(BooleanTrue);
            result.ObfuscationSalt.ShouldBe(StringOne);
            result.PasswordHash.ShouldBe(StringTwo);

            result.OrganizationUid.ShouldBe(organization.Uid);
            result.OrganizationName.ShouldBe(request.OrganizationName);
            result.EmailValidationToken.GetType().ShouldBe(UidOne.GetType());
        }

        [Test]
        public void UserFactory_CreateEntityFromRequest_UserEditRequest()
        {
            // arrange
            var request = GetUserEditRequest();
            var user = GetUser();
            var language = GetLanguage();

            // act
            var result = UserFactory.CreateEntityFromRequest(request, user, language);

            // assert
            result.UpdatedBy.ShouldBe(request.CurrentUserId);

            result.FirstName.ShouldBe(request.FirstName);
            result.LastName.ShouldBe(request.LastName);
            result.Name.ShouldBe(UserFactory.MapName(request.FirstName, request.LastName));

            result.LanguageId.ShouldBe(language.Id);
            result.LanguageUid.ShouldBe(language.Uid);
            result.LanguageName.ShouldBe(language.Name);
        }

        [Test]
        public void UserFactory_CreateEntityFromRequest_UserInviteRequest_Organization()
        {
            // arrange
            var request = GetUserInviteRequest();
            var organization = GetOrganization();

            // act
            var result = UserFactory.CreateEntityFromRequest(request, organization, StringOne);

            // assert
            result.Email.ShouldBe(request.Email);
            result.FirstName.ShouldBe(request.FirstName);
            result.LastName.ShouldBe(request.LastName);
            result.Name.ShouldBe(UserFactory.MapName(request.FirstName, request.LastName));
            result.ObfuscationSalt.ShouldBe(StringOne);
            result.IsActive.ShouldBe(BooleanTrue);

            result.OrganizationId.ShouldBe(organization.Id);
            result.OrganizationUid.ShouldBe(organization.Uid);
            result.OrganizationName.ShouldBe(organization.Name);
        }

        [Test]
        public void UserFactory_CreateEntityFromRequest_UserInviteRequest_CurrentOrganization()
        {
            // arrange
            var request = GetUserInviteRequest();
            var currentOrganization = GetCurrentOrganizationOne();

            // act
            var result = UserFactory.CreateEntityFromRequest(request, currentOrganization, StringOne);

            // assert
            result.Email.ShouldBe(request.Email);
            result.FirstName.ShouldBe(request.FirstName);
            result.LastName.ShouldBe(request.LastName);
            result.Name.ShouldBe(UserFactory.MapName(request.FirstName, request.LastName));
            result.ObfuscationSalt.ShouldBe(StringOne);
            result.IsActive.ShouldBe(BooleanTrue);

            result.OrganizationId.ShouldBe(currentOrganization.Id);
            result.OrganizationUid.ShouldBe(currentOrganization.Uid);
            result.OrganizationName.ShouldBe(currentOrganization.Name);
        }

        [Test]
        public void UserFactory_CreateEntityFromRequest_AdminInviteRequest()
        {
            // arrange
            var request = GetAdminInviteRequest();
            var currentUser = GetOrganizationOneCurrentUserOne();

            // act
            var result = UserFactory.CreateEntityFromRequest(request, currentUser, StringOne);

            // assert
           
            result.IsAdmin.ShouldBe( true);
            result.Email.ShouldBe( request.Email);
            result.FirstName.ShouldBe( request.FirstName);
            result.LastName.ShouldBe( request.LastName);
            result.Name.ShouldBe(UserFactory.MapName(request.FirstName, request.LastName));
            result.ObfuscationSalt.ShouldBe(StringOne);
            result.IsActive.ShouldBe( BooleanTrue);

            result.OrganizationId.ShouldBe( currentUser.Organization.Id);
            result.OrganizationUid.ShouldBe( currentUser.Organization.Uid);
            result.OrganizationName.ShouldBe( currentUser.Organization.Name);

            result.InvitedByUserId.ShouldBe( currentUser.Id);
            result.InvitedByUserUid.ShouldBe( currentUser.Uid);
            result.InvitedByUserName.ShouldBe( currentUser.Name);
        }

        [Test]
        public void UserFactory_CreateDtoFromEntity()
        {
            // arrange
            var user = GetUser();

            // act
            var result = UserFactory.CreateDtoFromEntity(user);

            // assert
            result.Uid.ShouldBe(user.Uid);
            result.Name.ShouldBe(user.Name);
            result.FirstName.ShouldBe(user.FirstName);
            result.LastName.ShouldBe(user.LastName);
            result.Description.ShouldBe(user.Description);
            result.Email.ShouldBe(user.Email);
            result.IsActive.ShouldBe(user.IsActive);
            result.IsAdmin.ShouldBe(user.IsAdmin);
            result.CreatedAt.ShouldBe(user.CreatedAt);
            result.LastLoggedInAt.ShouldBe(user.LastLoginAt);

            result.InvitedAt.ShouldBe(user.InvitedAt);
            result.InvitedByUserUid.ShouldBe(user.InvitedByUserUid);
            result.InvitedByUserName.ShouldBe(user.InvitedByUserName);

            result.OrganizationUid.ShouldBe(user.OrganizationUid);
            result.OrganizationName.ShouldBe(user.OrganizationName);

            result.LanguageUid.ShouldBe(user.LanguageUid);
            result.LanguageName.ShouldBe(user.LanguageName);
            result.LanguageIconUrl.ShouldBe(user.LanguageIconUrl);
        }

        [Test]
        public void UserFactory_MapName()
        {
            // arrange

            // act
            var result = UserFactory.MapName(StringOne, StringTwo);

            // assert
            result.ShouldBe(StringOne + " " + StringTwo);
        }

        [Test]
        public void UserFactory_MapCurrentUser()
        {
            // arrange
            var user = GetUser();

            // act
            var result = UserFactory.MapCurrentUser(user, IsoCode2Two);

            // assert
            result.Id.ShouldBe(user.Id);
            result.Uid.ShouldBe(user.Uid);
            result.Name.ShouldBe(user.Name);
            result.Email.ShouldBe(user.Email);
            result.IsAdmin.ShouldBe(user.IsAdmin);
            result.IsSuperAdmin.ShouldBe(user.IsSuperAdmin);
            result.IsActive.ShouldBe(user.IsActive);

            result.Organization.Id.ShouldBe(user.OrganizationId);
            result.Organization.Uid.ShouldBe(user.OrganizationUid);
            result.Organization.Name.ShouldBe(user.OrganizationName);

            result.LanguageCode.ShouldBe(IsoCode2Two);
        }
    }
}