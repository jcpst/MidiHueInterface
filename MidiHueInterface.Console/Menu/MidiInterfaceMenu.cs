using System.ComponentModel.DataAnnotations;

namespace MidiHueInterface.Menu;

public enum MidiInterfaceMenu
{
    [Display(Name = "Back")]   
    Back,
    
    [Display(Name = "List interfaces")]
    List,
    
    [Display(Name = "Enable interface")]   
    Enable,
    
    [Display(Name = "Disable interface")]  
    Disable,
}