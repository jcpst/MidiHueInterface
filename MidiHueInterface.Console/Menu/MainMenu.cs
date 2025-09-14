using System.ComponentModel.DataAnnotations;

namespace MidiHueInterface.Menu;

public enum MainMenu
{
    [Display(Name = "Setup")]
    Setup,
    
    [Display(Name = "Settings")]   
    AppSettings,
    
    [Display(Name = "Devices")]
    Devices,
    
    [Display(Name = "Bridges")]
    Bridges,
    
    [Display(Name = "MIDI Interfaces")]
    MidiInterfaces,
    
    [Display(Name = "Test")]
    Test,
    
    [Display(Name = "Exit (ctrl-c)")]
    Exit,
}