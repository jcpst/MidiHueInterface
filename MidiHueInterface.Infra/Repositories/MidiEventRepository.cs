using MidiHueInterface.App.Interfaces;
using MidiHueInterface.Infra.Clients;

namespace MidiHueInterface.Infra.Repositories;

public class MidiEventRepository(IMidiClient midiClient) : IMidiEventRepository
{
    public IEnumerable<(string DeviceName, int DeviceNumber)> GetDevices() => midiClient
        .GetDevices()
        .Select((device, index) => (device.Name, index));
}