using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MidiHueInterface.App.Interfaces;
using MidiHueInterface.App.Mediation;
using MidiHueInterface.App.Services;
using MidiHueInterface.Infra.Clients;
using MidiHueInterface.Infra.Listeners;
using MidiHueInterface.Infra.Repositories;

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
        services.AddSingleton<IHueBridgeClient, HueBridgeClient>();
        services.AddSingleton<MidiEventListener>();
        services.AddTransient<IBridgeRepository, BridgeRepository>();
        services.AddTransient<ISettingsRepository, SettingsRepository>();
        services.AddTransient<ILightBulbRepository, LightbulbRepository>();
    }

    private static void ConfigureApp(IServiceCollection services)
    {
        services.AddSingleton<IMediator, Mediator>();
        services.AddTransient<IMessageHandler<ControlChangeMessage>, ControlChangeHandler>();
        services.AddTransient<IMessageHandler<ProgramChangeMessage>, ProgramChangeHandler>();
        services.AddTransient<ISettingsService, SettingsService>();
        services.AddTransient<IAutomationService, AutomationService>();
    }
}