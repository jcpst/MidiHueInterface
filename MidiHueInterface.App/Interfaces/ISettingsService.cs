using MidiHueInterface.App.Models;

namespace MidiHueInterface.App.Interfaces;

public interface ISettingsService
{
    string GetSettingsPath();
    
    Task<IEnumerable<Bridge>> GetBridgesAsync(CancellationToken cancellationToken = default);
    
    Task SaveBridgeAsync(Bridge bridge, CancellationToken cancellationToken = default);
    
    public Task<Settings> GetSettingsAsync(CancellationToken cancellationToken = default);
}