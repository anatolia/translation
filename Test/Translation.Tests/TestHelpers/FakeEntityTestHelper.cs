using Translation.Common.Models.Shared;
using Translation.Data.Entities.Domain;
using Translation.Data.Entities.Main;
using static Translation.Tests.TestHelpers.FakeConstantTestHelper;

namespace Translation.Tests.TestHelpers
{
    public class FakeEntityTestHelper
    {
        public static Organization GetOrganizationOne()
        {
            var organization = new Organization();
            organization.Id = OrganizationOneId;
            organization.Uid = OrganizationOneUid;
            organization.Name = OrganizationOneName;
            organization.CreatedAt = DateTimeOne;
            organization.Description = StringOne;
            organization.IsActive = BooleanTrue;
            organization.ObfuscationKey = StringSixtyFourOne;

            return organization;
        }

        public static CurrentOrganization GetCurrentOrganizationOne()
        {
            var organization = new CurrentOrganization();
            organization.Id = OrganizationOneId;
            organization.Uid = OrganizationOneUid;
            organization.Name = OrganizationOneName;

            return organization;
        }

        public static Project GetOrganizationOneProjectOne()
        {
            var project = new Project();
            project.Id = OrganizationOneProjectOneId;
            project.Uid = OrganizationOneProjectOneUid;
            project.Name = OrganizationOneProjectOneName;
            project.OrganizationId = OrganizationOneId;
            project.OrganizationUid = OrganizationOneUid;
            project.OrganizationName = OrganizationOneName;
            project.IsActive = BooleanTrue;
            project.Url = HttpUrl;
            project.CreatedAt = DateTimeOne;

            return project;
        }

        public static User GetOrganizationOneUserOne()
        {
            var user = new User();
            user.OrganizationId = OrganizationOneId;
            user.OrganizationUid = OrganizationOneUid;
            user.OrganizationName = OrganizationOneName;

            user.Id = OrganizationOneUserOneId;
            user.Uid = OrganizationOneUserOneUid;
            user.Name = OrganizationOneUserOneName;

            user.Email = OrganizationOneUserOneEmail;
            user.IsActive = BooleanTrue;

            return user;
        }

        public static CurrentUser GetOrganizationOneCurrentUserOne()
        {
            var user = new CurrentUser();
            user.Id = OrganizationOneUserOneId;
            user.Uid = OrganizationOneUserOneUid;
            user.Name = OrganizationOneUserOneName;
            user.Email = OrganizationOneUserOneEmail;
            user.IsActive = BooleanTrue;

            return user;
        }
    }
}