using MidiHueInterface.App.Interfaces;
using MidiHueInterface.App.Models;

namespace MidiHueInterface.App.Services;

public class AutomationService(ILightBulbRepository lightBulbRepository, IBridgeRepository bridgeRepository)
    : IAutomationService
{
    public async Task<IEnumerable<(string Id, string Uri)>> GetBridgesAsync(CancellationToken cancellationToken = default)
    {
        return await bridgeRepository.Discover(cancellationToken);
    }

    public async Task<string?> RegisterBridge(string ip, string deviceName, CancellationToken cancellationToken = default)
    {
        return await bridgeRepository.RegisterAsync(ip, deviceName, cancellationToken);
    }

    public async Task EnableBridgeAsync(Bridge bridge, CancellationToken cancellationToken = default)
    {
        await bridgeRepository.EnableBridgeAsync(bridge, cancellationToken);
    }

    public async Task<IEnumerable<string>> GetLightBulbIdsAsync(CancellationToken cancellationToken = default)
    {
        return await lightBulbRepository.GetLightsAsync(cancellationToken);   
    }
    
    public async Task TestAsync(CancellationToken cancellationToken = default)
    {
        await lightBulbRepository.TestAsync(cancellationToken);
    }
}