
using System.Text.Json;
using Api.Settings;
using Application.Services;
using Domain.Domain.DTO;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Options;

namespace Api.Hub;

public class CharacterBackgroundService : BackgroundService
{

    private readonly RealTimeSettings _options;

    private readonly IHubContext<CharacterHub> _context;
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public CharacterBackgroundService(IHubContext<CharacterHub> context, IServiceScopeFactory serviceScopeFactory, IOptions<RealTimeSettings> options)
    {
        _context = context;
        _serviceScopeFactory = serviceScopeFactory;
        _options = options.Value;
    }


    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {

        using IServiceScope scope = _serviceScopeFactory.CreateScope();
        IRealTimeCharacterService realTimeCharacterService = scope.ServiceProvider.GetRequiredService<IRealTimeCharacterService>();

        using var timer = new PeriodicTimer(TimeSpan.FromMilliseconds(_options.UpdateFrequencyInMs));

        while (!stoppingToken.IsCancellationRequested
           && await timer.WaitForNextTickAsync(stoppingToken))
        {
            List<RealTimeCharacterParams> charactersConnected = await realTimeCharacterService.GetConnectedCharacters();
            await _context.Clients.All.SendAsync(JsonSerializer.Serialize(charactersConnected), stoppingToken);
        }


    }
}
