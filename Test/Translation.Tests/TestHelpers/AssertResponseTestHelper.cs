using System.Linq;

using Shouldly;

using Translation.Common.Enumerations;
using Translation.Common.Models.Base;

namespace Translation.Tests.TestHelpers
{
    public class AssertResponseTestHelper
    {
        public const string OrganizationNameMustBeUniquie = "organization_name_must_be_unique";
        public const string ProjectNameMustBeUniquie = "project_name_must_be_unique";
        public const string UserNameMustBeUniquie = "user_name_must_be_unique";

        public const string OrganizationNotFound = "organization_not_found";
        public const string ProjectNotFound = "project_not_found";
        public const string UserNotFound = "user_not_found";
        public const string LanguageNotFound = "language_not_found";
        public const string IntegrationNotFound = "integration_not_found";
        public const string IntegrationClientNotFound = "integration_client_not_found";
        public const string TokenNotFound = "token_not_found";
        public const string LabelNotFound = "label_not_found";
        public const string LabelTranslationNotFound = "label_translation_not_found";

        public const string OrganizationNotActive = "organization_not_active";
        public const string ProjectNotActive = "project_not_active";
        public const string UserNotActive = "user_not_active";
        public const string LanguageNotActive = "language_not_active";
        public const string IntegrationNotActive = "integration_not_active";
        public const string IntegrationClientNotActive = "integration_client_not_active";
        public const string TokenNotActive = "token_not_active";
        public const string LabelNotActive = "label_not_active";
        public const string LabelTranslationNotActive = "label_translation_not_active";

        public const string OrganizationRevisionNotFound = "organization_revision_not_found";
        public const string ProjectRevisionNotFound = "project_revision_not_found";
        public const string UserRevisionNotFound = "user_revision_not_found";
        public const string LanguageRevisionNotFound = "language_revision_not_found";
        public const string IntegrationRevisionNotFound = "integration_revision_not_found";
        public const string IntegrationRevisionClientNotFound = "integration_revision_client_not_found";
        public const string TokenRevisionNotFound = "token_revision_not_found";
        public const string LabelRevisionNotFound = "label_revision_not_found";
        public const string LabelTranslationRevisionNotFound = "label_translation_revision_not_found";

        public const string OrganizationHasChildren = "organization_has_children";
        public const string ProjectHasChildren  = "project_has_children";
        public const string UserHasChildren = "user_has_children";
        public const string LanguageHasChildren = "language_has_children";
        public const string IntegrationHasChildren = "integration_has_children";
        public const string IntegrationClientHasChildren = "integration_client_has_children";
        public const string TokenHasChildren = "token_has_children";
        public const string LabelHasChildren = "label_has_children";
        public const string LabelTranslationHasChildren = "label_translation_has_children";

        public static void AssertResponseStatusAndErrorMessages(BaseResponse result, ResponseStatus status, string errorMessage)
        {
            result.ShouldNotBeNull();
            result.Status.ShouldBe(status);
            result.ErrorMessages.ShouldNotBeNull();
            result.ErrorMessages.Any(x => x == errorMessage).ShouldBeTrue();
        }

        public static void AssertResponseStatusAndErrorMessages(BaseResponse result, ResponseStatus status)
        {
            result.ShouldNotBeNull();
            result.Status.ShouldBe(status);
            result.ErrorMessages.ShouldNotBeNull();

            if (status == ResponseStatus.Invalid || status == ResponseStatus.Failed)
            {
                result.ErrorMessages.Count.ShouldNotBe(0);
            }
            else if(status == ResponseStatus.Success)
            {
                result.ErrorMessages.Count.ShouldBe(0);
            }
        }
    }
}