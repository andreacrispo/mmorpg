using Microsoft.EntityFrameworkCore;
using MMORPG.Infrastrutture;
using MMORPG.Domain.Entity;

namespace MMORPG.Repository
{
    public class UserRepository : IUserRepository
    {

        private readonly MMORPGDbContext _dbContext;

        public UserRepository(MMORPGDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task<UserEntity?> FindByUsername(string username)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(x => x.Username == username);
        }
    }
}
