using MidiHueInterface.App.Interfaces;
using MidiHueInterface.App.Models;

namespace MidiHueInterface.App.Handlers;

public class ProgramChangeHandler(ILightBulbRepository lightBulbRepository) : IMessageHandler<ProgramChangeMessage>
{
    public async Task HandleAsync(ProgramChangeMessage message, CancellationToken cancellationToken = default)
    {
        var x = message.ProgramNumber switch
        {
            0 => Task.FromResult(1),
            1 => Task.FromResult(2),
            _ => Task.FromResult(0),
        };

        await lightBulbRepository.GetLightsAsync(cancellationToken);
        
        Console.WriteLine("Message received via mediator.");
        Console.WriteLine($"CH {message.Channel} PC {message.ProgramNumber}");
    }
}