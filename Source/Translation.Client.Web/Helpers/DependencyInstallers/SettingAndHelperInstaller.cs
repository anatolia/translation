using System.Configuration;

using Autofac;
using Npgsql;

using StandardRepository.Helpers;
using StandardRepository.Models;
using StandardRepository.PostgreSQL;
using StandardRepository.PostgreSQL.Factories;
using StandardRepository.PostgreSQL.Helpers;
using StandardRepository.PostgreSQL.Helpers.SqlExecutor;
using StandardUtils.Helpers;

using Translation.Common.Models.Shared;
using Translation.Data.Entities.Domain;

namespace Translation.Client.Web.Helpers.DependencyInstallers
{
    public class SettingAndHelperInstaller : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<CryptoHelper>();

            var connectionSettings = new ConnectionSettings();
            connectionSettings.DbName = ConfigurationManager.AppSettings[ConstantHelper.KEY_DB_NAME];
            connectionSettings.DbHost = ConfigurationManager.AppSettings[ConstantHelper.KEY_DB_HOST];
            connectionSettings.DbUser = ConfigurationManager.AppSettings[ConstantHelper.KEY_DB_USER];
            connectionSettings.DbPassword = ConfigurationManager.AppSettings[ConstantHelper.KEY_DB_PASS];
            connectionSettings.DbPort = ConfigurationManager.AppSettings[ConstantHelper.KEY_DB_PORT];
            builder.RegisterInstance(connectionSettings);

            var adminSettings = new AdminSettings();
            adminSettings.AdminEmail = ConfigurationManager.AppSettings[ConstantHelper.KEY_SUPER_ADMIN_EMAIL];
            adminSettings.AdminFirstName = ConfigurationManager.AppSettings[ConstantHelper.KEY_SUPER_ADMIN_FIRST_NAME];
            adminSettings.AdminLastName = ConfigurationManager.AppSettings[ConstantHelper.KEY_SUPER_ADMIN_LAST_NAME];
            adminSettings.AdminPassword = ConfigurationManager.AppSettings[ConstantHelper.KEY_SUPER_ADMIN_PASS];
            builder.RegisterInstance(adminSettings);

            builder.RegisterGeneric(typeof(PostgreSQLConstants<>));
            builder.RegisterType<PostgreSQLExpressionUtils>().As<ExpressionUtils>();

            var typeLookup = new PostgreSQLTypeLookup();
            builder.RegisterInstance(typeLookup);

            var entityAssemblies = new[] { typeof(Label).Assembly };
            var entityUtils = new EntityUtils(typeLookup, entityAssemblies);
            builder.RegisterInstance(entityUtils);

            builder.RegisterType<PostgreSQLConnectionFactory>();
            builder.Register(x => new NpgsqlConnection(PostgreSQLConnectionFactory.GetConnectionString(connectionSettings))).InstancePerDependency();
            builder.RegisterType<PostgreSQLExecutor>().InstancePerDependency();
            builder.RegisterType<PostgreSQLTransactionalExecutor>().InstancePerDependency();
        }
    }
}