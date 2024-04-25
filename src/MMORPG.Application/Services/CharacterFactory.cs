using Domain.Domain;
using MMORPG.Domain;
using MMORPG.Domain.Entity;

namespace MMORPG.Service
{
    public class CharacterFactory : ICharacterFactory
    {
        public Character? GetCharacter(CharacterEntity entity)
        {
            Character? instance = GetCharacterByClass(entity.ClassId);
            if (instance == null)
                return null;

            instance.Id = entity.Id;
            instance.Level = entity.LevelValue;


            instance.IsConnected = entity.IsConnected;

            instance.Position = Position.At(entity.PositionX, entity.PositionY, entity.PositionZ);
            instance.Rotation = Rotation.At(entity.RotationX, entity.RotationY, entity.RotationZ);
            instance.Hp = entity.Hp;
            MoveDirection moveDirection = entity.MoveDirection;

            instance.MoveDirection = moveDirection;


            if (entity.User != null)
                instance.Username = entity.User.Username;

            return instance;
        }

        public Character? GetCharacterByClass(CharacterClass characterClass)
        {
            Character? instance = characterClass switch
            {
                CharacterClass.Paladin => new Paladin(),
                CharacterClass.Wizard => new Wizard(),
                CharacterClass.Rogue => new Rogue(),
                _ => null,
            };

            return instance;
        }
    }
}
