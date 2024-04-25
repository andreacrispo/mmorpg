
using Domain.Domain;
using MMORPG.Domain;
using MMORPG.Domain.Entity;
using MMORPG.Repository;

namespace MMORPG.Service
{
    public class CharacterService : ICharacterService
    {
        private readonly ICharacterRepository repository;
        private readonly ICharacterFactory characterFactory;

        public CharacterService(ICharacterRepository repository, ICharacterFactory characterFactory)
        {
            this.repository = repository;
            this.characterFactory = characterFactory;
        }

        public async Task<List<Character>> GetCharacters()
        {
            List<CharacterEntity> charaters = await this.repository.GetAll();

            return charaters.Select(x => this.characterFactory.GetCharacter(x)).ToList();
        }

        public async Task<List<Character>> GetCharactersAlive()
        {

            List<CharacterEntity> charaters = await this.repository.GetWhere(c => (c.Hp > 0));

            return charaters.Select(x => this.characterFactory.GetCharacter(x)).ToList();
        }

        public async Task<List<Character>> GetCharactersConnected()
        {
            List<CharacterEntity> charaters = await this.repository.GetWhere(c => c.IsConnected);

            return charaters.Select(x => this.characterFactory.GetCharacter(x)).ToList();
        }

        public async Task<Character?> GetCharacter(int characterId)
        {

            CharacterEntity? entity = await this.repository.FindById(characterId);
            if (entity == null)

                return null;

            return this.characterFactory.GetCharacter(entity);
        }

        public async Task<Character?> UpdatePosition(int characterId, Position newPosition, MoveDirection moveDirection)
        {

            CharacterEntity? entity = await this.repository.FindById(characterId);
            if (entity == null)
                return null;


            entity.PositionX = newPosition.X;
            entity.PositionY = newPosition.Y;
            entity.PositionZ = newPosition.Z;
            entity.MoveDirection = moveDirection;

            CharacterEntity updated = await this.repository.Update(entity);
            return this.characterFactory.GetCharacter(updated);

        }

        public async Task<Character?> UpdateRotation(int characterId, Rotation rotation)
        {
            CharacterEntity? entity = await this.repository.FindById(characterId);
            if (entity == null)
                return null;

            entity.RotationX = rotation.X;
            entity.RotationY = rotation.Y;
            entity.RotationZ = rotation.Z;

            CharacterEntity updated = await this.repository.Update(entity);
            return this.characterFactory.GetCharacter(updated);
        }

        public async Task<Character?> UpdateCharacter(Character updatedCharacter)
        {
            CharacterEntity? entity = await this.repository.FindById(updatedCharacter.Id);
            if (entity == null)
                return null;


            entity.PositionX = updatedCharacter.Position.X;
            entity.PositionY = updatedCharacter.Position.Y;
            entity.Hp = updatedCharacter.Hp;
            entity.LevelValue = updatedCharacter.Level;
            entity.IsConnected = updatedCharacter.IsConnected;

            CharacterEntity updated = await this.repository.Update(entity);
            return this.characterFactory.GetCharacter(updated);
        }

        public async Task<Character?> Attack(int characterId, int targetId)
        {
            Character? attacker = await this.GetCharacter(characterId);
            Character? target = await this.GetCharacter(targetId);

            if (attacker is null || target is null)
                throw new Exception();

            attacker.Attack(target);
            return await this.UpdateCharacter(target);
        }

        public async Task<List<Character>> GetCharactersByUser(UserEntity user)
        {
            List<CharacterEntity> characters = await this.repository.FindByUser(user);
            return characters.Select(characterFactory.GetCharacter).ToList();
        }

        public Character CreateCharacter(UserEntity user, CharacterClass characterClass)
        {
            if (user == null || user.Username == null)
                throw new Exception();

            Character? newChar = characterFactory.GetCharacterByClass(characterClass);

            CharacterEntity entity = this.repository.Create(newChar, characterClass, user);

            return this.characterFactory.GetCharacter(entity);
        }

        public async Task<bool> Connect(Character character)
        {

            character.IsConnected = true;

            await this.UpdateCharacter(character);

            return character.IsConnected;
        }

        public async Task<bool> Disconnect(Character character)
        {

            character.IsConnected = false;

            await this.UpdateCharacter(character);

            return character.IsConnected;
        }

        public async Task<bool> Respawn(int characterId)
        {
            Character? character = await GetCharacter(characterId);
            if (character == null) return false;

            if (character.IsAlive)
                return false;

            character.Hp = character.InitHp;
            character.Position = Position.At(0, 0, 0);
            character.Rotation = Rotation.At(0, 0, 0);
            await this.UpdateCharacter(character);

            return true;
        }

  
    }
}
