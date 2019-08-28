using NUnit.Framework;
using Shouldly;
using StandardRepository.Models.Entities;
using StandardRepository.Models.Entities.Schemas;


using Translation.Data.Entities.Main;
using static Translation.Common.Tests.TestHelpers.AssertPropertyTestHelper;

namespace Translation.Server.Unit.Tests.Data.Entities
{
    [TestFixture]
    public class UserEntityTests
    {
        [Test]
        public void User()
        {
            var entity = new User();

            var entityType = entity.GetType();
            var properties = entityType.GetProperties();

            entityType.BaseType.Name.ShouldBe(nameof(BaseEntity));
            entityType.GetInterface(nameof(ISchemaMain)).ShouldNotBeNull();

            AssertLongProperty(properties, "OrganizationId", entity.OrganizationId);
            AssertGuidProperty(properties, "OrganizationUid", entity.OrganizationUid);
            AssertStringProperty(properties, "OrganizationName", entity.OrganizationName);

            AssertStringProperty(properties, "Email", entity.Email);
            AssertGuidProperty(properties, "EmailValidationToken", entity.EmailValidationToken);
            AssertNullableDateTimeProperty(properties, "EmailValidatedAt",entity.EmailValidatedAt);
            AssertBooleanProperty(properties, "IsEmailValidated",entity.IsEmailValidated);

            AssertStringProperty(properties, "PasswordHash", entity.PasswordHash);
            AssertStringProperty(properties, "ObfuscationSalt", entity.ObfuscationSalt);
            AssertNullableGuidProperty(properties, "PasswordResetToken", entity.PasswordResetToken);
            AssertNullableDateTimeProperty(properties, "PasswordResetRequestedAt", entity.PasswordResetRequestedAt);

            AssertNullableDateTimeProperty(properties, "LastLoginAt", entity.LastLoginAt);
            AssertNullableDateTimeProperty(properties, "LastLoginTryAt", entity.LastLoginTryAt);
            AssertIntegerProperty(properties,"LoginTryCount",entity.LoginTryCount);

            AssertStringProperty(properties, "FirstName", entity.FirstName);
            AssertStringProperty(properties, "LastName", entity.LastName);
            AssertStringProperty(properties, "Description", entity.Description);

            AssertLongProperty(properties, "LanguageId", entity.LanguageId);
            AssertGuidProperty(properties, "LanguageUid", entity.LanguageUid);
            AssertStringProperty(properties, "LanguageName", entity.LanguageName);

            AssertBooleanProperty(properties, "IsActive", entity.IsActive);
            AssertBooleanProperty(properties, "IsAdmin", entity.IsAdmin);
            AssertBooleanProperty(properties, "IsSuperAdmin", entity.IsSuperAdmin);

            AssertNullableDateTimeProperty(properties, "InvitedAt", entity.InvitedAt);
            AssertNullableLongProperty(properties, "InvitedByUserId",entity.InvitedByUserId);
            AssertNullableGuidProperty(properties, "InvitedByUserUid",entity.InvitedByUserUid);
            AssertStringProperty(properties, "InvitedByUserName",entity.InvitedByUserName);
            AssertNullableGuidProperty(properties, "InvitationToken", entity.InvitationToken);

            AssertIntegerProperty(properties, "LabelCount", entity.LabelCount);
            AssertIntegerProperty(properties, "LabelTranslationCount", entity.LabelTranslationCount);

            AssertLongProperty(properties, "LanguageId", entity.LanguageId);
            AssertGuidProperty(properties, "LanguageUid", entity.LanguageUid);
            AssertStringProperty(properties, "LanguageName", entity.LanguageName);
            AssertStringProperty(properties, "LanguageIconUrl", entity.LanguageIconUrl);
        }
    }
}