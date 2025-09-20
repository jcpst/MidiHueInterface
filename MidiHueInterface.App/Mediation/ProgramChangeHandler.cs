using MidiHueInterface.App.Interfaces;
using MidiHueInterface.App.Models;

namespace MidiHueInterface.App.Mediation;

public class ProgramChangeHandler(ILightBulbRepository lightBulbRepository) : IMessageHandler<ProgramChangeMessage>
{
    public async Task HandleAsync(ProgramChangeMessage message, CancellationToken cancellationToken = default)
    {
        await lightBulbRepository.AllLightsToColor(HexColors.Random(), cancellationToken);
    }
}