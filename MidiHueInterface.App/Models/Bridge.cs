using System.ComponentModel;

namespace MidiHueInterface.App.Models;

public class Bridge
{
    public string Id { get; set; }
    
    [PasswordPropertyText]
    public string? Username { get; set; }
    
    public string Uri { get; set; }
    
    public override string ToString() => this.Id;
}