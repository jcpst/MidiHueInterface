using System.Text.Json;
using MidiHueInterface.App.Interfaces;
using MidiHueInterface.App.Models;
using static System.Environment;
using static System.Environment.SpecialFolder;
using static System.IO.Path;

namespace MidiHueInterface.Infra.Repositories;

public class SettingsRepository : ISettingsRepository
{
    public string SettingsPath { get; } = GetEnvironmentVariable("MIDIHUE_SETTINGS_PATH") ?? 
                                          Combine(GetFolderPath(ApplicationData), "MidiHue", "presets.json");
    
    public async Task<Settings> GetSettingsAsync(CancellationToken cancellationToken = default)
    {
        if (!File.Exists(this.SettingsPath))
        {
            return new ();
        }
        
        await using var stream = File.OpenRead(this.SettingsPath);
        var settings = await JsonSerializer.DeserializeAsync<Settings>(stream, cancellationToken: cancellationToken);
        
        return settings ?? new ();
    }
    
    public async Task SaveSettingsAsync(Settings settings, CancellationToken cancellationToken = default)
    {
        var settingsDirectory = GetDirectoryName(this.SettingsPath) ?? string.Empty;
        
        if (!Directory.Exists(settingsDirectory))
        {
            Directory.CreateDirectory(settingsDirectory);
        }
        
        settings.Metadata.Update();

        var json = JsonSerializer.Serialize(settings);
        
        await File.WriteAllTextAsync(this.SettingsPath, json, cancellationToken: cancellationToken);
    }
}