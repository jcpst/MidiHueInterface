using MidiHueInterface.App.Interfaces;

namespace MidiHueInterface.App.Mediation;

public class Mediator(IServiceProvider serviceProvider) : IMediator
{
    public async Task PublishAsync<TMessage>(TMessage message, CancellationToken cancellationToken = default)
        where TMessage : IMessage
    {
        var registeredServices = serviceProvider.GetService(typeof(IEnumerable<IMessageHandler<TMessage>>));
        var handlers = registeredServices as IEnumerable<IMessageHandler<TMessage>>;

        foreach (var handler in handlers ?? [])
        {
            await handler.HandleAsync(message, cancellationToken);
        }
    }
}