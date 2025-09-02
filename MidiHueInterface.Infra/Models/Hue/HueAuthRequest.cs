namespace MidiHueInterface.Infra.Models.Hue;

public class HueAuthRequest
{
    public string Devicetype { get; set; }
    public bool Generateclientkey { get; set; } = true;
}