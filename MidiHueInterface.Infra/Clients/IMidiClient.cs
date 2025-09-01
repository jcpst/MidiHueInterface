using Melanchall.DryWetMidi.Multimedia;

namespace MidiHueInterface.Infra.Clients;

public interface IMidiClient
{
    IEnumerable<InputDevice> GetDevices();
}