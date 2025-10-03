using MidiHueInterface.App.Models;

namespace MidiHueInterface.App.Interfaces;

public interface ILightBulbRepository
{
    Task<IEnumerable<string>> GetLightsAsync(CancellationToken cancellationToken = default);

    Task TestAsync(CancellationToken cancellationToken = default);

    Task AllLightsToColor(
        string colorHexCode,
        double brightness = 100,
        double effectSpeed = 1,
        EffectType effectType = EffectType.NoEffect, 
        CancellationToken cancellationToken = default);
}