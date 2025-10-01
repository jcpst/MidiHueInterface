using System.Drawing;
using HueApi;
using HueApi.BridgeLocator;
using HueApi.ColorConverters;
using HueApi.ColorConverters.Original.Extensions;
using HueApi.Models;
using HueApi.Models.Requests;
using MidiHueInterface.Infra.Models;
using Bridge = MidiHueInterface.App.Models.Bridge;
using Color = HueApi.Models.Color;

namespace MidiHueInterface.Infra.Clients;

public class HueBridgeClient : IHueBridgeClient
{
    private readonly IDictionary<string, BridgeConfig> bridges = new Dictionary<string, BridgeConfig>();
    private readonly IDictionary<string, string> bridgeLightIds = new Dictionary<string, string>();
    
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
                var bridge = new LocalHueApi(ip, registration.Username);
                var lights = await bridge.GetLightsAsync();
                
                this.bridges.Add(deviceName, new BridgeConfig
                {
                    Api = bridge,
                    LightIds = lights.Data.Select(l => l.Id)
                });
            
                return registration.Username!;
            }
        }
        
        return null;
    }

    public async Task EnableBridgeAsync(Bridge bridge, CancellationToken cancellationToken = default)
    {
        if (!bridges.ContainsKey(bridge.Id))
        {
            var b = new LocalHueApi(bridge.Uri, bridge.Username);
            
            var lights = await b.GetLightsAsync();
            
            this.bridges.Add(bridge.Id, new BridgeConfig
            {
                Api = b,
                LightIds = lights.Data.Select(l => l.Id)
            });
        }
    }

    public IEnumerable<string> GetRegisteredBridgeIds()
    {
        return this.bridges.AsEnumerable().Select(b => b.Key);
    }
    
    public async Task<IEnumerable<Light>> GetLightsAsync(CancellationToken cancellationToken = default)
    {
        List<Light> allLights = [];
        
        foreach (var bridge in this.bridges)
        {
            var lights = await bridge.Value.Api.GetLightsAsync();
            
            allLights.AddRange(lights.Data);
        }
        
        return allLights;
    }
    
    public async Task ChangeLightsAsync(
        string bridgeId, 
        string hexColor, 
        Effect effect = Effect.no_effect, 
        CancellationToken cancellationToken = default)
    {
        var color = new RGBColor(hexColor);
        var effects = new EffectsV2
        {
            Action = new EffectAction
            {
                Effect = effect
            }
        };
        
        var update = new UpdateLight { EffectsV2 = effects }
            .SetColor(color)
            .TurnOn()
            .SetDuration(TimeSpan.FromMilliseconds(20));
        
        var bridge = this.bridges[bridgeId];
        var lights = bridge.LightIds;
        var lightTasks = lights.Select(l => bridge.Api.UpdateLightAsync(l, update));
        
        await Task.WhenAll(lightTasks);
    }

    public async Task BlinkAsync(CancellationToken cancellationToken = default)
    {
        var update = new UpdateLight().SetDuration(TimeSpan.FromMilliseconds(50));
        var red = new RGBColor("#ff0000");
        var green = new RGBColor("#00ff00");
        
        foreach (var (_, bridge) in this.bridges)
        {
            var lights = await bridge.Api.GetLightsAsync();

            foreach (var light in lights.Data)
            {
                var originalColor = light.ToRGBColor();
                
                await bridge.Api.UpdateLightAsync(light.Id, update.TurnOn().SetColor(red));
                await Wait();
                await bridge.Api.UpdateLightAsync(light.Id, update.TurnOff());
                await Wait();
                await bridge.Api.UpdateLightAsync(light.Id, update.TurnOn().SetColor(green));
                await Wait();
                await bridge.Api.UpdateLightAsync(light.Id, update.TurnOff());
                await Wait();
                await bridge.Api.UpdateLightAsync(light.Id, update.TurnOn().SetColor(originalColor));
            }
        }
        
        Task Wait(int ms = 300) => Task.Delay(ms, cancellationToken);
    }
    
    public async Task<IEnumerable<Scene>> GetScenesAsync(string bridgeId, CancellationToken cancellationToken = default)
    {
        List<Scene> scenes = [];

        if (bridges.TryGetValue(bridgeId, out var bridge))
        {
            var scenesResponse = await bridge.Api.GetScenesAsync();
            
            scenes.AddRange(scenesResponse.Data);
        }
        
        return scenes;
    }
}