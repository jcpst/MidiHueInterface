using System.Drawing;
using HueApi;
using HueApi.BridgeLocator;
using HueApi.ColorConverters;
using HueApi.ColorConverters.Original.Extensions;
using HueApi.Models;
using HueApi.Models.Requests;
using Bridge = MidiHueInterface.App.Models.Bridge;
using Color = HueApi.Models.Color;

namespace MidiHueInterface.Infra.Clients;

public class HueBridgeClient : IHueBridgeClient
{
    private readonly IDictionary<string, LocalHueApi> bridges = new Dictionary<string, LocalHueApi>();
    
    public async Task<IEnumerable<(string Id, string Uri)>> Discover(CancellationToken cancellationToken = default)
    {
        var locatedBridges = await new LocalNetworkScanBridgeLocator().LocateBridgesAsync(cancellationToken);
        
        return locatedBridges.Select(x => (x.BridgeId, x.IpAddress));
    }

    public async Task<string?> RegisterAsync(string ip, string deviceName, string? userName = null, CancellationToken cancellationToken = default)
    {
        if (userName is null)
        {
            var registration = await LocalHueApi.RegisterAsync(ip, "MidiHue", deviceName, generateClientKey: true);

            if (registration is not null)
            {
                this.bridges.Add(deviceName, new LocalHueApi(ip, registration.Username)); 
            
                return registration.Username!;
            }
        }
        else if (!this.bridges.ContainsKey(deviceName))
        {
            this.bridges.Add(deviceName, new LocalHueApi(ip, userName));
        }
        
        return null;
    }

    public Task EnableBridgeAsync(Bridge bridge, CancellationToken cancellationToken = default)
    {
        if (!bridges.ContainsKey(bridge.Id))
        {
            this.bridges.Add(bridge.Id, new LocalHueApi(bridge.Uri, bridge.Username));
        }
        
        return Task.CompletedTask;
    }
    
    public async Task<IEnumerable<Light>> GetLightsAsync(CancellationToken cancellationToken = default)
    {
        List<Light> allLights = [];
        
        foreach (var bridge in this.bridges)
        {
            var lights = await bridge.Value.GetLightsAsync();
            
            allLights.AddRange(lights.Data);
        }
        
        return allLights;
    }
    
    public async Task ChangeLightColorAsync(string hexColor, CancellationToken cancellationToken = default)
    {
        var color = new RGBColor(hexColor);
        var update = new UpdateLight()
            .TurnOn()
            .SetDuration(TimeSpan.FromMilliseconds(70))
            .SetColor(color);
        
        foreach (var (_, bridge) in this.bridges)
        {
            var lights = await bridge.GetLightsAsync();

            foreach (var light in lights.Data)
            {
                await bridge.UpdateLightAsync(light.Id, update);
            }
        }
    }

    public async Task BlinkAsync(CancellationToken cancellationToken = default)
    {
        var update = new UpdateLight().SetDuration(TimeSpan.FromMilliseconds(100));
        var red = new RGBColor("#ff0000");
        var green = new RGBColor("#00ff00");
        
        foreach (var (_, bridge) in this.bridges)
        {
            var lights = await bridge.GetLightsAsync();

            foreach (var light in lights.Data)
            {
                var originalColor = light.ToRGBColor();
                
                await bridge.UpdateLightAsync(light.Id, update.TurnOn().SetColor(red));
                await Wait();
                await bridge.UpdateLightAsync(light.Id, update.TurnOff());
                await Wait();
                await bridge.UpdateLightAsync(light.Id, update.TurnOn().SetColor(green));
                await Wait();
                await bridge.UpdateLightAsync(light.Id, update.TurnOff());
                await Wait();
                await bridge.UpdateLightAsync(light.Id, update.TurnOn().SetColor(originalColor));
            }
        }
        
        Task Wait(int ms = 300) => Task.Delay(ms, cancellationToken);
    }
}