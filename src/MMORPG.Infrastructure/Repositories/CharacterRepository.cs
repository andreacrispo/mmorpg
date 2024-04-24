using Microsoft.EntityFrameworkCore;
using MMORPG.Infrastrutture;
using MMORPG.Domain;
using MMORPG.Domain.Entity;
using System.Linq.Expressions;

namespace MMORPG.Repository
{
    public class CharacterRepository : ICharacterRepository
    {

        private readonly MMORPGDbContext _dbContext;

        public CharacterRepository(MMORPGDbContext dbContext)
        {
            _dbContext = dbContext;
        }



        public async Task<CharacterEntity> FindById(int id)
        {
            return await _dbContext.Characters.Include(x => x.User).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<CharacterEntity>> FindByUser(UserEntity user)
        {
            return await GetWhere(x => x.User.Id == user.Id);
        }

        public async Task<List<CharacterEntity>> GetAll()
        {
            return await _dbContext.Characters.Include(x => x.User).ToListAsync();
        }

        public async Task<List<CharacterEntity>> GetWhere(Expression<Func<CharacterEntity, bool>> predicate)
        {
            return await _dbContext.Characters.Include(x => x.User).Where(predicate).ToListAsync();
        }


        public CharacterEntity Create(Character character, CharacterClass characterClass, UserEntity user)
        {
            CharacterEntity entity = new CharacterEntity
            {
                ClassId = characterClass,
                User = user,
                Hp = character.Hp,
                LevelValue = character.Level,
                PositionX = 0,
                PositionY = 0
            };

            _dbContext.Characters.Add(entity);
            _dbContext.SaveChanges();

            return entity;
        }

        public async Task<CharacterEntity> Update(CharacterEntity updatedCharacter)
        {

            CharacterEntity result = _dbContext.Characters
             .Update(updatedCharacter)
             .Entity;

            _dbContext.SaveChanges();

            return result;
        }

    }
}
