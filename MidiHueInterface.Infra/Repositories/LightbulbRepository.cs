using MidiHueInterface.App.Interfaces;
using MidiHueInterface.Infra.Clients;

namespace MidiHueInterface.Infra.Repositories;

public class LightbulbRepository(IHueBridgeClient hueBridgeClient) : ILightBulbRepository
{
    public async Task<IEnumerable<string>> GetLightsAsync(CancellationToken cancellationToken = default)
    {
        var lights = await hueBridgeClient.GetLightsAsync(cancellationToken);
        
        return lights.Select(l => l.Id.ToString());
    }

    public async Task TestAsync(CancellationToken cancellationToken = default)
    {
        await hueBridgeClient.BlinkAsync(cancellationToken);
    }
}