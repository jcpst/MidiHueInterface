using System.Threading.RateLimiting;
using MidiHueInterface.Infra.Interfaces;

namespace MidiHueInterface.Infra.Clients;

public class HueBridgeClient : IHueBridgeClient
{
    private readonly HttpClient httpClient;

    public HueBridgeClient(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }

    public async Task Get()
    {
        var x = await this.httpClient.GetStringAsync("http://localhost:8080/");
    }
}