using System.Configuration;

using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Npgsql;
using StandardRepository.Helpers;
using StandardRepository.Models;
using StandardRepository.PostgreSQL;
using StandardRepository.PostgreSQL.Factories;
using StandardRepository.PostgreSQL.Helpers;
using StandardRepository.PostgreSQL.Helpers.SqlExecutor;

using Translation.Common.Helpers;
using Translation.Common.Models.Shared;
using Translation.Data.Entities.Main;

namespace Translation.Client.Web.Helpers.DependencyInstallers
{
    public class SettingAndHelperInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<CryptoHelper>());

            var connectionSettings = new ConnectionSettings();
            connectionSettings.DbName = ConfigurationManager.AppSettings[ConstantHelper.KEY_DB_NAME];
            connectionSettings.DbHost = ConfigurationManager.AppSettings[ConstantHelper.KEY_DB_HOST];
            connectionSettings.DbUser = ConfigurationManager.AppSettings[ConstantHelper.KEY_DB_USER];
            connectionSettings.DbPassword = ConfigurationManager.AppSettings[ConstantHelper.KEY_DB_PASS];
            connectionSettings.DbPort = ConfigurationManager.AppSettings[ConstantHelper.KEY_DB_PORT];
            container.Register(Component.For<ConnectionSettings>().Instance(connectionSettings));

            var adminSettings = new AdminSettings();
            adminSettings.AdminEmail = ConfigurationManager.AppSettings[ConstantHelper.KEY_SUPER_ADMIN_EMAIL];
            adminSettings.AdminFirstName = ConfigurationManager.AppSettings[ConstantHelper.KEY_SUPER_ADMIN_FIRST_NAME];
            adminSettings.AdminLastName = ConfigurationManager.AppSettings[ConstantHelper.KEY_SUPER_ADMIN_LAST_NAME];
            adminSettings.AdminPassword = ConfigurationManager.AppSettings[ConstantHelper.KEY_SUPER_ADMIN_PASS];
            container.Register(Component.For<AdminSettings>().Instance(adminSettings));

            container.Register(Component.For(typeof(PostgreSQLConstants<>)));

            var expressionUtils = new PostgreSQLExpressionUtils();
            container.Register(Component.For<ExpressionUtils>().Instance(expressionUtils));
            var typeLookup = new PostgreSQLTypeLookup();
            container.Register(Component.For<PostgreSQLTypeLookup>().Instance(typeLookup));

            var entityAssemblies = new[] { typeof(Organization).Assembly };
            var entityUtils = new EntityUtils(typeLookup, entityAssemblies);
            container.Register(Component.For<EntityUtils>().Instance(entityUtils));

            container.Register(Component.For<PostgreSQLConnectionFactory>());
            container.Register(Component.For<NpgsqlConnection>()
                                        .DependsOn(Dependency.OnValue("connectionString", PostgreSQLConnectionFactory.GetConnectionString(connectionSettings)))
                                        .LifestyleTransient());
            
            container.Register(Component.For<PostgreSQLExecutor>().LifestyleTransient());
            container.Register(Component.For<PostgreSQLTransactionalExecutor>().LifestyleTransient());
        }
    }
}