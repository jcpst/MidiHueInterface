using MidiHueInterface.App.Interfaces;

namespace MidiHueInterface.App.Mediation;

public class ControlChangeHandler : IMessageHandler<ControlChangeMessage>
{
    public Task HandleAsync(ControlChangeMessage message, CancellationToken cancellationToken = default)
    {
        return Task.CompletedTask;
    }
}