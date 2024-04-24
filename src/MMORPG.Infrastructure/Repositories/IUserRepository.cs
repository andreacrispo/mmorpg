using MMORPG.Domain.Entity;

namespace MMORPG.Repository
{
    public interface IUserRepository
    {
        Task<UserEntity?> FindByUsername(string username);
    }
}