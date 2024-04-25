using Domain.Domain;
using MMORPG.Domain;
using MMORPG.Domain.Entity;

namespace MMORPG.Service
{
    public interface ICharacterService
    {
        Task<Character?> Attack(int characterId, int targetId);
        Task<bool> Connect(Character character);
        Character CreateCharacter(UserEntity user, CharacterClass characterClass);
        Task<bool> Disconnect(Character character);
        Task<Character?> GetCharacter(int characterId);
        Task<List<Character>> GetCharacters();
        Task<List<Character>> GetCharactersAlive();
        Task<List<Character>> GetCharactersByUser(UserEntity user);
        Task<List<Character>> GetCharactersConnected();
        Task<bool> Respawn(int characterId);
        Task<Character?> UpdateCharacter(Character updatedCharacter);
        Task<Character?> UpdatePosition(int characterId, Position newPosition, MoveDirection moveDirection);
        Task<Character?> UpdateRotation(int characterId, Rotation rotation);
    }
}
