using System.ComponentModel.DataAnnotations;

namespace MidiHueInterface.Menu;

public enum BridgeMenu
{
    [Display(Name = "Back")]   
    Back,
    
    [Display(Name = "List saved bridges")]
    List,
    
    [Display(Name = "Register new bridges")]
    Register,
    
    [Display(Name = "Enable bridges")]
    Enable,
    
    [Display(Name = "Remove a bridge")]
    Unregister,
}