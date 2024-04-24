using MMORPG.Domain;
using MMORPG.Domain.Entity;

namespace MMORPG.Service
{
    public interface ICharacterFactory
    {
        Character? GetCharacter(CharacterEntity entity);
        Character? GetCharacterByClass(CharacterClass characterClass);
    }
}