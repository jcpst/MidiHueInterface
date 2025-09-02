namespace MidiHueInterface.App.Interfaces;

public interface IMediator
{
    Task PublishAsync<TMessage>(TMessage message, CancellationToken cancellationToken = default)
        where TMessage : IMessage;
}