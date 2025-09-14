using MidiHueInterface.App.Models;

namespace MidiHueInterface.App.Interfaces;

public interface IAutomationService
{
    Task<IEnumerable<(string Id, string Uri)>> GetBridgesAsync(CancellationToken cancellationToken = default);

    Task<string?> RegisterBridge(string ip, string deviceName, CancellationToken cancellationToken = default);

    Task EnableBridgeAsync(Bridge bridge, CancellationToken cancellationToken = default);

    Task<IEnumerable<string>> GetLightBulbIdsAsync(CancellationToken cancellationToken = default);

    Task TestAsync(CancellationToken cancellationToken = default);
}