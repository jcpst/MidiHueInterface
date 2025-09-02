namespace MidiHueInterface.Infra.Models.Hue;

public class HueAuthResponseError
{
    public IEnumerable<HueError> Errors { get; set; }
}