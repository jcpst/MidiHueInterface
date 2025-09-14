namespace MidiHueInterface.App.Models;

public class Metadata
{
    public DateTime CreatedUtc { get; set; } = DateTime.UtcNow;
    public DateTime LastUpdatedUtc { get; set; } = DateTime.UtcNow;
    public string ModifiedBy { get; set; } = Environment.UserName;

    public void Update()
    {
        this.LastUpdatedUtc = DateTime.UtcNow;
        this.ModifiedBy = Environment.UserName;
    }
}