using System.Threading.Tasks;

using Translation.Data.Entities.Main;

namespace Translation.Data.UnitOfWorks.Contracts
{
    public interface ISignUpUnitOfWork
    {
        Task<(bool, Organization, User)> DoWork(Organization organization, User user, UserLoginLog userLoginLog);
    }
}