namespace MidiHueInterface.App.Interfaces;

public interface IMidiEventRepository
{
    IEnumerable<(string DeviceName, int DeviceNumber)> GetDevices();
}