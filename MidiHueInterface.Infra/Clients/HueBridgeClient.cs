using System.Net.Http.Json;
using MidiHueInterface.Infra.Interfaces;
using MidiHueInterface.Infra.Models.Hue;
using Q42.HueApi;

namespace MidiHueInterface.Infra.Clients;

public class HueBridgeClient : IHueBridgeClient
{
    private readonly HttpClient httpClient;
    private readonly LocalHueClient cl;

    public HueBridgeClient(HttpClient httpClient)
    {
        var config = "";
        this.cl = new LocalHueClient(config, httpClient);
    }

    public async Task<string?> Authenticate()
    {
        var x  = await this.cl.RegisterAsync("MidiHueInterface", "MidiHueInterface", true);

        return x?.StreamingClientKey;
    }
    
    public async Task<IEnumerable<Light>> GetLights() => await this.cl.GetLightsAsync();
}