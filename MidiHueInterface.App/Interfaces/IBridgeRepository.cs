using MidiHueInterface.App.Models;

namespace MidiHueInterface.App.Interfaces;

public interface IBridgeRepository
{
    Task<IEnumerable<(string Id, string Uri)>> Discover(CancellationToken cancellationToken = default);

    Task<string?> RegisterAsync(string ip, string deviceName, CancellationToken cancellationToken = default);

    Task EnableBridgeAsync(Bridge bridge, CancellationToken cancellationToken = default);
}