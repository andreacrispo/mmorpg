using Microsoft.EntityFrameworkCore;
using MMORPG.Domain;
using MMORPG.Domain.Entity;
using System.Linq.Expressions;

namespace MMORPG.Repository
{
    public interface ICharacterRepository
    {
        Task<CharacterEntity> FindById(int id);

        Task<List<CharacterEntity>> FindByUser(UserEntity user);

        Task<List<CharacterEntity>> GetAll();
        Task<List<CharacterEntity>> GetWhere(Expression<Func<CharacterEntity, bool>> predicate);

        CharacterEntity Create(Character character, CharacterClass characterClass, UserEntity user);
        Task<CharacterEntity> Update(CharacterEntity updatedCharacter);
    }
}