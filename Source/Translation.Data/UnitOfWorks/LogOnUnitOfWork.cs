using System.Threading.Tasks;

using StandardRepository.PostgreSQL;

using Translation.Data.Entities.Main;
using Translation.Data.Repositories.Contracts;
using Translation.Data.UnitOfWorks.Contracts;

namespace Translation.Data.UnitOfWorks
{
    public class LogOnUnitOfWork : ILogOnUnitOfWork
    {
        private readonly PostgreSQLTransactionalExecutor _transactionalExecutor;
        private readonly IUserRepository _userRepository;
        private readonly IUserLoginLogRepository _userLoginLogRepository;

        public LogOnUnitOfWork(PostgreSQLTransactionalExecutor transactionalExecutor,
                               IUserRepository userRepository,
                               IUserLoginLogRepository userLoginLogRepository)
        {
            _transactionalExecutor = transactionalExecutor;
            _userRepository = userRepository;
            _userLoginLogRepository = userLoginLogRepository;
        }

        public async Task<bool> DoWork(User user, UserLoginLog userLoginLog)
        {
            await _transactionalExecutor.ExecuteAsync<bool>(async connectionFactory =>
            {
                _userRepository.SetSqlExecutorForTransaction(connectionFactory);
                _userLoginLogRepository.SetSqlExecutorForTransaction(connectionFactory);

                await _userRepository.Update(user.Id, user);
                await _userLoginLogRepository.Insert(user.Id, userLoginLog);

                return true;
            });

            return true;
        }
    }
}