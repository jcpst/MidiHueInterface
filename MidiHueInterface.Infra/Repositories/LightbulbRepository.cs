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

    public async Task AllLightsToColor(
        string colorHexCode,
        double brightness = 100,
        double effectSpeed = 0,
        EffectType effectType = EffectType.NoEffect, 
        CancellationToken cancellationToken = default)
    {
        var bridges = hueBridgeClient.GetRegisteredBridgeIds();
        var effect = Map(effectType);

        var bridgeTasks = bridges.Select(bridgeId => hueBridgeClient.ChangeLightsAsync(
            bridgeId, 
            colorHexCode, 
            brightness, 
            effectSpeed, 
            effect, 
            cancellationToken));
        
        await Task.WhenAll(bridgeTasks);
    }

    private static Effect Map(EffectType effectType) => effectType switch
    {
        EffectType.Prism => Effect.prism,
        EffectType.Opal => Effect.opal,
        EffectType.Glisten => Effect.glisten,
        EffectType.Sparkle => Effect.sparkle,
        EffectType.Fire => Effect.fire,
        EffectType.Candle => Effect.candle,
        EffectType.Underwater => Effect.underwater,
        EffectType.Cosmos => Effect.candle,
        EffectType.Sunbeam => Effect.sunbeam,
        EffectType.Enchant => Effect.enchant,
        _ => Effect.no_effect
    };
}