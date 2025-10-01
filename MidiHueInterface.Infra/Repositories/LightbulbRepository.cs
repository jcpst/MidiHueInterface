using HueApi.Models;
using MidiHueInterface.App.Interfaces;
using MidiHueInterface.App.Models;
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

    public async Task AllLightsToColor(string colorHexCode, EffectType effectType = EffectType.no_effect, CancellationToken cancellationToken = default)
    {
        var bridges = hueBridgeClient.GetRegisteredBridgeIds();

        var effect = effectType switch
        {
            EffectType.prism => Effect.prism,
            EffectType.opal => Effect.opal,
            EffectType.sparkle => Effect.sparkle,
            EffectType.fire => Effect.fire,
            EffectType.candle => Effect.candle,
            EffectType.underwater => Effect.underwater,
            EffectType.cosmos => Effect.candle,
            EffectType.sunbeam => Effect.sunbeam,
            EffectType.enchant => Effect.enchant,
             _ => Effect.no_effect
        };

        var bridgeTasks = bridges.Select(b => hueBridgeClient.ChangeLightsAsync(b, colorHexCode, effect, cancellationToken));
        
        await Task.WhenAll(bridgeTasks);
    }
}