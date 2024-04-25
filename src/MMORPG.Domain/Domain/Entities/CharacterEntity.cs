

namespace MMORPG.Domain.Entity
{
    public class CharacterEntity
    {
        public int Id { get; set; }
        public int LevelValue { get; set; }
        public bool IsConnected { get; set; }
        public double PositionX { get; set; }
        public double PositionY { get; set; }
        public double PositionZ { get; set; }

        public double RotationX { get; set; }
        public double RotationY { get; set; }
        public double RotationZ { get; set; }

        public MoveDirection MoveDirection { get; set; } = MoveDirection.None;
        public double Hp { get; set; }
        public CharacterClass ClassId { get; set; }
        public virtual UserEntity User { get; set; } = null!;
    }
}
