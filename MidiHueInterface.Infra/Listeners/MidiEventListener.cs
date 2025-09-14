using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Multimedia;
using MidiHueInterface.App.Interfaces;
using PC = MidiHueInterface.App.Models.ProgramChangeMessage;
using CC = MidiHueInterface.App.Models.ControlChangeMessage;

namespace MidiHueInterface.Infra.Listeners;

public class MidiEventListener(IMediator mediator) : IDisposable
{
    private readonly Dictionary<string, InputDevice> registeredInputDevices = new ();

    public static IEnumerable<InputDevice> AvailableInputDevices => InputDevice.GetAll();

    public void EnableDevice(string name)
    {

        if (this.registeredInputDevices.ContainsKey(name))
        {
            return;
        }
        
        var device = InputDevice.GetByName(name);

        if (!device.IsListeningForEvents)
        {
            device.EventReceived += OnEventReceived;
            device.StartEventsListening();
        
            this.registeredInputDevices.Add(name, device);
        }
    }

    public void DisableDevice(string name)
    {
        var device = InputDevice.GetByName(name);

        if (device.IsListeningForEvents)
        {
            device.EventReceived -= OnEventReceived;
            device.StopEventsListening();
        
            this.registeredInputDevices.Remove(name);
        }
    }

    private async void OnEventReceived(object? sender, MidiEventReceivedEventArgs e)
    {
        var task = e.Event switch
        {
            ProgramChangeEvent pc => mediator.PublishAsync(new PC(pc.Channel, pc.ProgramNumber)),
            ControlChangeEvent cc => mediator.PublishAsync(new CC(cc.Channel, cc.ControlNumber, cc.ControlValue)),
            _ => Task.CompletedTask,
        };
        try
        {
            await task;
        }
        catch
        {
            // Do nothing.
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