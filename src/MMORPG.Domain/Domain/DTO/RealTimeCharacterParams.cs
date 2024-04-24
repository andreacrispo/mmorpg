using MMORPG.Domain;

namespace Domain.Domain.DTO;
public class RealTimeCharacterParams
{
    public string SessionId { get; set; }
    public int CharacterId { get; set; }
    public double PositionX { get; set; }
    public double PositionY { get; set; }


    public MoveDirection MoveDirection { get; set; }

    public double Hp { get; set; }
    public int TargetId { get; set; }


    public ActionType ActionType { get; set; }

    public double InitHp { get; set; }
    public int Level { get; set; }

    public bool IsConnected { get; set; }

    public CharacterClass ClassId { get; set; }



}
