
using Domain.Domain.DTO;

namespace Application.Services;

public interface IRealTimeCharacterService
{
    Task HandshakeConnect(string connectionId, string message);
    void Disconnect(string connectionId);
    Task<List<RealTimeCharacterParams>> GetConnectedCharacters();
    Task<bool> HandleMessage(string message);
}
