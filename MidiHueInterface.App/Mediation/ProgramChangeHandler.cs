using MidiHueInterface.App.Interfaces;
using MidiHueInterface.App.Models;

namespace MidiHueInterface.App.Mediation;

public class ProgramChangeHandler : IMessageHandler<ProgramChangeMessage>
{
    private readonly ILightBulbRepository lightBulbRepository;
    private readonly IShowRepository showRepository;

    public ProgramChangeHandler(ILightBulbRepository lightBulbRepository, IShowRepository showRepository)
    {
        this.lightBulbRepository = lightBulbRepository;
        this.showRepository = showRepository;
    }

    public async Task HandleAsync(ProgramChangeMessage message, CancellationToken cancellationToken = default)
    {
        Console.WriteLine($"Program Change: {message.ProgramNumber}");
        Console.WriteLine($"Channel: {message.Channel}");
        
        var presetExists = this.showRepository.Presets.ContainsKey(message.ProgramNumber);
        
        if (message.Channel == 0 && presetExists)
        {
            var preset = this.showRepository.Presets.GetValueOrDefault(message.ProgramNumber, new PresetConfig());
            
            await this.lightBulbRepository.AllLightsToColor(
                colorHexCode: HexColors.GetColor(preset.Color), 
                brightness: preset.Brightness, 
                effectSpeed: 1, 
                effectType: Enum.TryParse<EffectType>(preset.Effect, ignoreCase: true, out var effectType) 
                    ? effectType 
                    : EffectType.NoEffect,  
                cancellationToken: cancellationToken);
        }
    }
}