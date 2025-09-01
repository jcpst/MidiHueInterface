using Melanchall.DryWetMidi.Multimedia;

namespace MidiHueInterface.Infra.Clients;

public class MidiClient : IMidiClient
{
    public MidiClient()
    {
    }
    public IEnumerable<InputDevice> GetDevices()
    {
        return InputDevice.GetAll();
    }
}