namespace MidiHueInterface.App.Interfaces;

public interface ILightBulbRepository
{
    Task<IEnumerable<string>> GetLightsAsync(CancellationToken cancellationToken = default);

    Task TestAsync(CancellationToken cancellationToken = default);
}