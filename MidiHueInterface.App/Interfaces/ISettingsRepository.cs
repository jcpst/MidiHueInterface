using MidiHueInterface.App.Models;

namespace MidiHueInterface.App.Interfaces;

public interface ISettingsRepository
{
    string SettingsPath { get; }

    Task<Settings> GetSettingsAsync(CancellationToken cancellationToken = default);

    Task SaveSettingsAsync(Settings settings, CancellationToken cancellationToken = default);
}