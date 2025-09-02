using System.Threading.RateLimiting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MidiHueInterface.App;
using MidiHueInterface.App.Handlers;
using MidiHueInterface.App.Interfaces;
using MidiHueInterface.App.Models;
using MidiHueInterface.App.Services;
using MidiHueInterface.Infra.Clients;
using MidiHueInterface.Infra.Interfaces;
using MidiHueInterface.Infra.Listeners;
using MidiHueInterface.Infra.Repositories;
using Polly;

namespace MidiHueInterface;

public static class Program
{
    public static async Task Main(string[] args)
    {
        await Microsoft.Extensions.Hosting.Host
            .CreateDefaultBuilder(args)
            .ConfigureHostConfiguration(builder =>
            {
                builder.AddCommandLine(args);
                builder.AddJsonFile("appsettings.json", optional: true);
                builder.AddEnvironmentVariables();
            })
            .ConfigureServices(configure =>
            {
                configure.AddHostedService<Host>();
                ConfigureInfra(configure);
                ConfigureApp(configure);
            })
            .RunConsoleAsync();
    }

    private static void ConfigureInfra(IServiceCollection services)
    {
        services
            .AddHttpClient<IHueBridgeClient, HueBridgeClient>((provider, client) =>
            {
                // TODO: discover device, set base address, generate auth token...
            })
            .AddResilienceHandler("HueBridgeRateLimiter", builder => builder.AddRateLimiter(
                new SlidingWindowRateLimiter(new ()
                {
                    Window = TimeSpan.FromSeconds(1),
                    SegmentsPerWindow = 1,
                    PermitLimit = 10,
                    QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
                    QueueLimit = 20
                })));
        
        services.AddSingleton<MidiEventListener>();
        services.AddTransient<ILightBulbRepository, LightbulbRepository>();
    }

    private static void ConfigureApp(IServiceCollection services)
    {
        services.AddSingleton<IMediator, Mediator>();
        services.AddTransient<IMessageHandler<ProgramChangeMessage>, ProgramChangeHandler>();
        services.AddTransient<IAutomationService, AutomationService>();
    }
}