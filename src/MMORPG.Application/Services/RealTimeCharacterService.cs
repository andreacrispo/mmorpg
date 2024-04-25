using System.Collections.Concurrent;
using System.Text.Json;
using Domain.Domain;
using Domain.Domain.DTO;
using MMORPG.Domain;
using MMORPG.Service;

namespace Application.Services;


public class RealTimeCharacterService : IRealTimeCharacterService
{

    private readonly ICharacterService _characterService;
    private readonly IUserService _userService;

    private readonly ConcurrentDictionary<string, string> _connectedUsers;

    public RealTimeCharacterService(ICharacterService characterService, IUserService userService)
    {
        _characterService = characterService;
        _connectedUsers = new ConcurrentDictionary<string, string>();
        _userService = userService;
    }


    public Task HandshakeConnect(string connectionId, string message)
    {
        RealTimeHandshakeParams? messageParams = JsonSerializer.Deserialize<RealTimeHandshakeParams>(message);
        if (messageParams == null)
            return Task.FromResult(false);

        bool result = _connectedUsers.TryAdd(connectionId, messageParams.Username);

        return Task.FromResult(result);
    }

    public async void Disconnect(string connectionId)
    {
        _connectedUsers.TryGetValue(connectionId, out string? username);
        if (username != null)
        {
            await this._userService.Logout(username);
        }
    }

    public async Task<List<RealTimeCharacterParams>> GetConnectedCharacters()
    {
        List<Character> charactersConnected = await _characterService.GetCharactersConnected();
        return charactersConnected.Select(x => ToParams(x)).ToList();
    }

    public Task<bool> HandleMessage(string message)
    {

        RealTimeCharacterParams? messageParams = JsonSerializer.Deserialize<RealTimeCharacterParams>(message);

        if (messageParams is null)
            return Task.FromResult(false);

        switch (messageParams.ActionType)
        {
            case ActionType.Movement:
                this.HandleMovement(messageParams);
                break;
            case ActionType.Attack:
                this._characterService.Attack(messageParams.CharacterId, messageParams.TargetId);
                break;
        }

        return Task.FromResult(true);

    }

    private void HandleMovement(RealTimeCharacterParams messageParams)
    {
        this._characterService.UpdatePosition(messageParams.CharacterId, Position.At(messageParams.PositionX, messageParams.PositionY, messageParams.PositionZ), messageParams.MoveDirection);
        this._characterService.UpdateRotation(messageParams.CharacterId, Rotation.At(messageParams.RotationX, messageParams.RotationY, messageParams.RotationZ));
    }

    private RealTimeCharacterParams ToParams(Character character)
    {
        return new RealTimeCharacterParams()
        {
            CharacterId = character.Id,
            PositionX = character.Position.X,
            PositionY = character.Position.Y,
            PositionZ = character.Position.Z,

            MoveDirection = character.MoveDirection,
            Hp = character.Hp,
            TargetId = -1,
            ActionType = ActionType.None,
            InitHp = character.InitHp,
            Level = character.Level,
            IsConnected = character.IsConnected,
            ClassId = character.CharacterClass
        };
    }

}
