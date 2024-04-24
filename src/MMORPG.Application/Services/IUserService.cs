using MMORPG.Domain;

namespace MMORPG.Service
{
    public interface IUserService
    {
        Task<bool> Login(string username, CharacterClass characterClass);
        Task<bool> Logout(string username);
    }
}