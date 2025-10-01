using MidiHueInterface.App.Interfaces;
using MidiHueInterface.App.Models;

namespace MidiHueInterface.App.Mediation;

public class ProgramChangeHandler(
    ILightBulbRepository lightBulbRepository,
    IShowRepository showRepository) : IMessageHandler<ProgramChangeMessage>
{
    private const byte GlobalPreset = 127;
    
    public async Task HandleAsync(ProgramChangeMessage message, CancellationToken cancellationToken = default)
    {
        if (message.Channel == 0)
        {
            var preset = showRepository.Presets.GetValueOrDefault(message.ProgramNumber) 
                         ?? new PresetConfig();
            var color = preset.Color.StartsWith('#') 
                ? preset.Color 
                : HexColors.GetColorByReadableName(preset.Color);
            var effect = Enum.Parse<EffectType>(preset.Effect);
            
            await lightBulbRepository.AllLightsToColor(color, effect, cancellationToken);
        }
        else
        {
            //
        }
    }
}