using System.Text.RegularExpressions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using MidiHueInterface.App.Extensions;
using MidiHueInterface.App.Interfaces;
using MidiHueInterface.App.Models;
using MidiHueInterface.Infra.Listeners;
using MidiHueInterface.Menu;
using static System.Console;
using static MidiHueInterface.Infra.Listeners.MidiEventListener;
using static MidiHueInterface.Menu.MainMenu;
using static Sharprompt.Prompt;

namespace MidiHueInterface;

/// <inheritdoc />
public class Host(IAutomationService automationService, ISettingsService settingsService, MidiEventListener listener)
    : IHostedService
{
    public async Task StartAsync(CancellationToken ct)
    {
        WriteLine("MIDI Hue Synchronization Suite");
        WriteLine("==============================");
        
        while (ct is not { IsCancellationRequested: true }) await RunAsync(ct);
    }

    public Task StopAsync(CancellationToken _) => Task.CompletedTask;

    private Task RunAsync(CancellationToken ct = default) => Selector<MainMenu>() switch
    {
        Devices => Selector<DeviceMenu>() switch
        {
            DeviceMenu.ListAll => ListDevices(ct),
            _ => Task.CompletedTask,
        },
        
        Bridges => Selector<BridgeMenu>() switch
        {
            BridgeMenu.List => ListBridges(ct),
            BridgeMenu.Register => RegisterBridge(ct),
            BridgeMenu.Enable => EnableBridges(ct),
            _ => Task.CompletedTask,
        },
        
        MidiInterfaces => Selector<MidiInterfaceMenu>() switch
        {
            MidiInterfaceMenu.List => ListMidiInterfaces(),
            MidiInterfaceMenu.Enable => EnableMidiInterface(),
            _ => Task.CompletedTask,
        },
        
        AppSettings => SettingsAsync(ct),
        Test => TestApp(ct),
        Exit => ExitApp(),
        _ => Task.CompletedTask,
    };

    private async Task ListDevices(CancellationToken ct = default)
    {
        foreach (var id in await automationService.GetLightBulbIdsAsync(ct))
        {
            WriteLine(id);
        }
    }
    
    private async Task ListBridges(CancellationToken ct = default)
    {
        foreach (var bridge in await settingsService.GetBridgesAsync(ct))
        {
            WriteLine($"bridge: {bridge.Id} - {bridge.Uri} - {bridge.Username}");
        }
    }

    private async Task RegisterBridge(CancellationToken ct = default)
    {
        WriteLine("Please wait while we discover your bridges...");
        WriteLine("Press the button on the bridge before selecting it.");
        
        var bridges = await automationService.GetBridgesAsync(ct);
        
        foreach (var (id, uri) in MultiSelect("Select which bridges you would like to enable", bridges))
        {
            WriteLine($"Please press the button on bridge {id}. Then press any key to continue.");
            ReadKey();
            
            var username = await automationService.RegisterBridge(uri, id, ct);
            var bridgeToSave = new Bridge { Id = id, Uri = uri, Username = username };
            
            await settingsService.SaveBridgeAsync(bridgeToSave, ct);
                        
            WriteLine($"bridge saved: {id} - {uri}");
        }
    }
    
    private async Task EnableBridges(CancellationToken ct = default)
    {
        var bridges = await settingsService.GetBridgesAsync(ct);

        foreach (var bridge in MultiSelect("Select which bridges to enable", bridges))
        {
            await automationService.EnableBridgeAsync(bridge, ct);
        }
    }

    private async Task SettingsAsync(CancellationToken ct = default)
    {
        var settings = await settingsService.GetSettingsAsync(ct);
        
        WriteLine(settingsService.GetSettingsPath());            
        WriteLine(settings.ToJson(pretty: true));
    }

    private Task EnableMidiInterface()
    {
        var midiInputs = AvailableInputDevices.Select(x => x.Name).ToList();
        if (midiInputs.Count != 0)
        {
            var selectedDevice = Select("Select a device", midiInputs);
            
            // TODO: implement functionality in service.
            listener.EnableDevice(selectedDevice);
            
            WriteLine($"Device Enabled: {selectedDevice}.");
        }
        else
        {
            WriteLine("No devices found.");
        }
            
        return Task.CompletedTask;
    }

    private Task ListMidiInterfaces()
    {
        var midiInputs = AvailableInputDevices.Select(x => x.Name).ToList();
        foreach (var midiInput in midiInputs) WriteLine(midiInput);
            
        return Task.CompletedTask;
    }
    
    private async Task TestApp(CancellationToken ct = default)
    {
        await automationService.TestAsync(ct);
    }
    
    private Task ExitApp()
    {
        WriteLine("Exiting...");
        Environment.Exit(0);
        return Task.CompletedTask;
    }

    private static T Selector<T>() where T : Enum => Select<T>(Regex.Replace(typeof(T).Name, "(?<!^)([A-Z])", " $1"));
}