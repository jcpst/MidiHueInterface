using System.Text.Json;
using MidiHueInterface.App.Interfaces;
using MidiHueInterface.App.Models;
using static System.Console;

namespace MidiHueInterface.App.Services;

public class SettingsService(
    ISettingsRepository settingsRepository, 
    IShowRepository showRepository) : ISettingsService
{
    public async Task<Settings> GetSettingsAsync(CancellationToken cancellationToken = default)
    {
        return await settingsRepository.GetSettingsAsync(cancellationToken);
    }
    
    public string GetSettingsPath() => settingsRepository.SettingsPath;

    public async Task<IEnumerable<Bridge>> GetBridgesAsync(CancellationToken cancellationToken = default)
    {
        var settings = await this.GetSettingsAsync(cancellationToken);

        return settings.Bridges;
    }
    
    public async Task SaveBridgeAsync(Bridge bridge, CancellationToken cancellationToken = default)
    {
        var settings = await this.GetSettingsAsync(cancellationToken);
        var bridges = settings.Bridges.ToList();
        
        // if bridge id does not exist in settings, add the bridge
        var bridgeExists = bridges.Any(b => b.Id == bridge.Id);

        if (!bridgeExists)
        {
            settings.Bridges = bridges.Append(bridge);
            await settingsRepository.SaveSettingsAsync(settings, cancellationToken);
        }
    }

    public PresetConfig? GetPreset(byte programNumber)
    {
        return showRepository.Presets.GetValueOrDefault(programNumber);
    }
}