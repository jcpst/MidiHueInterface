namespace MidiHueInterface.Infra.Models.Hue;

public class HueResponse<T>
{
    public IEnumerable<HueError> Errors { get; set; } = [];
    
    public IEnumerable<T> Data { get; set; } = [];
}