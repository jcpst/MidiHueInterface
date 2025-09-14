using MidiHueInterface.App.Interfaces;
using MidiHueInterface.App.Models;
using MidiHueInterface.Infra.Clients;

namespace MidiHueInterface.Infra.Repositories;

public class BridgeRepository(IHueBridgeClient hueBridgeClient) : IBridgeRepository
{
    public Task<IEnumerable<(string Id, string Uri)>> Discover(CancellationToken cancellationToken = default)
    {
        return hueBridgeClient.Discover(cancellationToken);
    }
    
    public Task<string?> RegisterAsync(string ip, string deviceName, CancellationToken cancellationToken = default)
    {
        return hueBridgeClient.RegisterAsync(ip, deviceName, cancellationToken: cancellationToken);
    }
    
    public Task EnableBridgeAsync(Bridge bridge, CancellationToken cancellationToken = default)
    {
        return hueBridgeClient.EnableBridgeAsync(bridge, cancellationToken);
    }
}