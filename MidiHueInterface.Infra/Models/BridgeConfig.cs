using HueApi;

namespace MidiHueInterface.Infra.Models;

public class BridgeConfig
{
    public LocalHueApi Api { get; set; }

    public IEnumerable<Guid> LightIds { get; set; }
}