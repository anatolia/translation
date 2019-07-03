using Translation.Common.Models.Shared;
using Translation.Data.Entities.Domain;
using Translation.Data.Entities.Main;
using static Translation.Tests.TestHelpers.GetFakeConstantTestHelper;

namespace Translation.Tests.TestHelpers
{
    public class GetFakeEntityTestHelper
    {
        public static Organization GetParkNet()
        {
            var organization = new Organization();
            organization.Id = IdParkNet;
            organization.Uid = UidParkNet;
            organization.Name = NameParkNet;
            organization.CreatedAt = DateTimeOne;
            organization.Description = StringOne;
            organization.IsActive = true;
            organization.ObfuscationKey = StringSixtyFourOne;

            return organization;
        }

        public static CurrentOrganization GetCurrentParkNet()
        {
            var organization = new CurrentOrganization();
            organization.Id = IdParkNet;
            organization.Uid = UidParkNet;
            organization.Name = NameParkNet;

            return organization;
        }

        public static Project GetParkNetProjectOne()
        {
            var project = new Project();
            project.Id = IdParkNetProjectOne;
            project.Uid = UidBlueSoftProjectOne;
            project.Name = NameParkNetProjectOne;
            project.OrganizationId = IdParkNet;
            project.OrganizationUid = UidParkNet;
            project.OrganizationName = NameParkNet;
            project.IsActive = true;
            project.Url = URL_HTTP;
            project.CreatedAt = DateTimeOne;

            return project;
        }
    }
}