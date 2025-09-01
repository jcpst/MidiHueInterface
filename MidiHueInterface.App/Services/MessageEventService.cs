using MidiHueInterface.App.Interfaces;

namespace MidiHueInterface.App.Services;

public class MessageEventService(IMidiEventRepository midiEventRepository) : IMessageEventService
{
    public IEnumerable<(string DeviceName, int DeviceNumber)> GetDevices() => midiEventRepository.GetDevices();
}