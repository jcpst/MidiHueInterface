using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Multimedia;
using MidiHueInterface.App.Interfaces;
using MidiHueInterface.App.Models;

namespace MidiHueInterface.Infra.Listeners;

public class MidiEventListener(IMediator mediator) : IDisposable
{
    private readonly Dictionary<string, InputDevice> registeredInputDevices = new ();

    public static IEnumerable<InputDevice> AvailableInputDevices => InputDevice.GetAll();

    public void EnableDevice(string name)
    {
        var device = InputDevice.GetByName(name);
        
        device.EventReceived += OnEventReceived;
        device.StartEventsListening();
        
        
        this.registeredInputDevices.Add(name, device);
    }

    private async void OnEventReceived(object? sender, MidiEventReceivedEventArgs e)
    {
        try
        {
            if (e.Event is ProgramChangeEvent pc)
            {
                var message = new ProgramChangeMessage(pc.Channel, pc.ProgramNumber);
            
                await mediator.PublishAsync(message);
            }
            else
            {
                Console.WriteLine(e.Event);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    public void Dispose()
    {
        foreach (var inputDevice in this.registeredInputDevices.Values.ToList())
        {
            inputDevice.Dispose();
        }
    }
}