namespace MidiHueInterface.App.Models;

public class PresetConfig
{
    public byte ProgramNumber { get; set; } = 127;
    
    public string PatchName { get; set; } = string.Empty;
    
    public string Color { get; set; } = "#F5F5F5";
    
    public string Effect { get; set; } = "no_effect";
}