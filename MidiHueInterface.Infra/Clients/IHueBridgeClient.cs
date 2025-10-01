using HueApi.Models;
using Bridge = MidiHueInterface.App.Models.Bridge;

namespace MidiHueInterface.Infra.Clients;

public interface IHueBridgeClient
{
    Task<IEnumerable<(string Id, string Uri)>> Discover(CancellationToken cancellationToken = default);

    Task<string?> RegisterAsync(string ip, string deviceName, string? userName = null, CancellationToken cancellationToken = default);

    Task EnableBridgeAsync(Bridge bridge, CancellationToken cancellationToken = default);

    IEnumerable<string> GetRegisteredBridgeIds();
    
    Task<IEnumerable<Light>> GetLightsAsync(CancellationToken cancellationToken = default);

    Task ChangeLightsAsync(string bridgeId, string hexColor, Effect effect = Effect.no_effect, CancellationToken cancellationToken = default);

    Task BlinkAsync(CancellationToken cancellationToken = default);

    Task<IEnumerable<Scene>> GetScenesAsync(string bridgeId, CancellationToken cancellationToken = default);
}