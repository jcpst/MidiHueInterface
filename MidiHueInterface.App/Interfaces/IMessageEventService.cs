namespace MidiHueInterface.App.Interfaces;

public interface IMessageEventService
{
    IEnumerable<(string DeviceName, int DeviceNumber)> GetDevices();
}