using System.Threading.Tasks;

using Translation.Data.Entities.Main;

namespace Translation.Data.UnitOfWorks.Contracts
{
    public interface ILogOnUnitOfWork
    {
        Task<bool> DoWork(User user, UserLoginLog userLoginLog);
    }
}