namespace Api.Hub;

using Application.Services;
using Microsoft.AspNetCore.SignalR;

public record Notification(string Text, DateTime Date);


public class CharacterHub : Hub
{

    private readonly IRealTimeCharacterService _realTimeCharacterService;


    public CharacterHub(IRealTimeCharacterService realTimeCharacterService)
    {
        _realTimeCharacterService = realTimeCharacterService;
    }

    public override async Task OnConnectedAsync()
    {
        await base.OnConnectedAsync();
        string msg = _realTimeCharacterService.GetHandShake(Context.ConnectionId);
        await Clients.Client(Context.ConnectionId).SendAsync(msg);
    }

    public override Task OnDisconnectedAsync(Exception? exception)
    {
        _realTimeCharacterService.Disconnect(Context.ConnectionId);
        return base.OnDisconnectedAsync(exception);
    }

    public async Task HandshakeConnect(string message)
    {
        await _realTimeCharacterService.HandshakeConnect(Context.ConnectionId, message);
    }

    public async Task ReceiveMessage(string message)
    {
        await _realTimeCharacterService.HandleMessage(message);
    }



}


