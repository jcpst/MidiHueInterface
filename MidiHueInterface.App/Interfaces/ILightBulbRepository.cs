using MidiHueInterface.App.Models;

namespace MidiHueInterface.App.Interfaces;

public interface ILightBulbRepository
{
    Task<IEnumerable<string>> GetLightsAsync(CancellationToken cancellationToken = default);

    Task TestAsync(CancellationToken cancellationToken = default);

    Task AllLightsToColor(string colorHexCode, EffectType effectType = EffectType.no_effect, CancellationToken cancellationToken = default);
}