using System.ComponentModel.DataAnnotations;

namespace MidiHueInterface.Menu;

public enum DeviceMenu
{
    [Display(Name = "Back")]   
    Back,
    
    [Display(Name = "List devices saved in settings")]
    ListAll,
    
    [Display(Name = "Add new device")]
    Add,
}