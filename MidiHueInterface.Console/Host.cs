using Microsoft.Extensions.Hosting;
using MidiHueInterface.App.Interfaces;
using static Sharprompt.Prompt;

namespace MidiHueInterface;

public class Host(IMessageEventService messageEventService) : IHostedService
{
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        var devices = messageEventService.GetDevices().Select(x => x.DeviceName);
        var val = Select("Select a number", devices);
        
        Console.WriteLine(val);
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}