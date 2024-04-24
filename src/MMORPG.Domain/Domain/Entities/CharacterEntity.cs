

namespace MMORPG.Domain.Entity
{
    public class CharacterEntity
    {
        public int Id { get; set; }
        public int LevelValue { get; set; }
        public bool IsConnected { get; set; }
        public double PositionX { get; set; }
        public double PositionY { get; set; }
        public MoveDirection MoveDirection { get; set; } = MoveDirection.None;
        public double Hp { get; set; }
        public CharacterClass ClassId { get; set; }
        public virtual UserEntity User { get; set; } = null!;
    }
}