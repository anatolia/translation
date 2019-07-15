using Castle.MicroKernel.Registration;

using Translation.Common.Contracts;
using Translation.Service;
using Translation.Service.Managers;
using Translation.Tests.Common;

namespace Translation.Tests.Server.Services
{
    public class ServiceBaseTests : BaseTests
    {
        public ServiceBaseTests()
        {

            #region Services

            Container.Register(Component.For<IAdminService>().ImplementedBy<AdminService>());
            Container.Register(Component.For<IIntegrationService>().ImplementedBy<IntegrationService>());
            Container.Register(Component.For<IJournalService>().ImplementedBy<JournalService>());
            Container.Register(Component.For<ILabelService>().ImplementedBy<LabelService>());
            Container.Register(Component.For<ILanguageService>().ImplementedBy<LanguageService>());
            Container.Register(Component.For<IOrganizationService>().ImplementedBy<OrganizationService>());
            Container.Register(Component.For<IProjectService>().ImplementedBy<ProjectService>());

            #endregion

            Container.Register(Component.For<CacheManager>());
        }
    }
}