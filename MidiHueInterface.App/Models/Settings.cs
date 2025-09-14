namespace MidiHueInterface.App.Models;

public class Settings
{
    public IEnumerable<Bridge> Bridges { get; set; } = [];
    
    public IEnumerable<Preset> Presets { get; set; } = [];
    
    public Metadata Metadata { get; set; } = new ();
}