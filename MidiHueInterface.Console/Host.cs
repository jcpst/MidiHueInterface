using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using MidiHueInterface.App.Models;
using MidiHueInterface.Infra.Listeners;
using static MidiHueInterface.Infra.Listeners.MidiEventListener;
using static Sharprompt.Prompt;

namespace MidiHueInterface;

public class Host : IHostedService
{
    private readonly IServiceProvider serviceProvider;
    private readonly IOptions<AppConfiguration> config;

    public Host(IOptions<AppConfiguration> config, IServiceProvider serviceProvider)
    {
        this.config = config;
        this.serviceProvider = serviceProvider;
    }
    
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        var listener = this.serviceProvider.GetRequiredService<MidiEventListener>();
        
        var deviceNames = AvailableInputDevices.Select(x => x.Name);
        var selectedDevice = Select("Select a device", deviceNames);
        
        listener.EnableDevice(selectedDevice);
        
        Console.WriteLine($"Registered: {selectedDevice}.");
        await Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}