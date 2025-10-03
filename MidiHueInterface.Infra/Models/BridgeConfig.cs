using HueApi;

namespace MidiHueInterface.Infra.Models;

public class BridgeConfig
{
    public LocalHueApi? Api { get; set; }

    public IEnumerable<Guid> LightIds { get; set; } = new List<Guid>();

    public IDictionary<string, Guid> Scenes { get; set; } = new Dictionary<string, Guid>();

    public IDictionary<string, Guid> Zones { get; set; } = new Dictionary<string, Guid>();
}