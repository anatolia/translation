using System.Threading.Tasks;

using StandardRepository.PostgreSQL;

using Translation.Data.Entities.Main;
using Translation.Data.Repositories.Contracts;
using Translation.Data.UnitOfWorks.Contracts;

namespace Translation.Data.UnitOfWorks
{
    public class SignUpUnitOfWork : ISignUpUnitOfWork
    {
        private readonly PostgreSQLTransactionalExecutor _transactionalExecutor;
        private readonly IOrganizationRepository _organizationRepository;
        private readonly IUserRepository _userRepository;
        private readonly IUserLoginLogRepository _userLoginLogRepository;

        public SignUpUnitOfWork(PostgreSQLTransactionalExecutor transactionalExecutor,
                                IOrganizationRepository organizationRepository,
                                IUserRepository userRepository,
                                IUserLoginLogRepository userLoginLogRepository)
        {
            _transactionalExecutor = transactionalExecutor;
            _organizationRepository = organizationRepository;
            _userRepository = userRepository;
            _userLoginLogRepository = userLoginLogRepository;
        }

        public async Task<(bool, Organization, User)> DoWork(Organization organization, User user, UserLoginLog userLoginLog)
        {
            var (organizationResult,
                userResult) = await _transactionalExecutor.ExecuteAsync(async connectionFactory =>
            {
                _userRepository.SetSqlExecutorForTransaction(connectionFactory);
                _organizationRepository.SetSqlExecutorForTransaction(connectionFactory);
                _userLoginLogRepository.SetSqlExecutorForTransaction(connectionFactory);

                var organizationId = await _organizationRepository.Insert(0, organization);
                organization.Id = organizationId;

                user.OrganizationId = organizationId;
                var userId = await _userRepository.Insert(0, user);
                user.Id = userId;

                userLoginLog.OrganizationId = organizationId;
                userLoginLog.UserId = userId;
                await _userLoginLogRepository.Insert(userId, userLoginLog);

                organization.CreatedBy = userId;
                organization.UserCount++;
                user.CreatedBy = userId;
                await _organizationRepository.Update(userId, organization);
                await _userRepository.Update(userId, user);

                return (organization,
                        user);
            });

            return (true,
                    organizationResult,
                    userResult);
        }
    }
}