using System.Threading.RateLimiting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MidiHueInterface.App.Interfaces;
using MidiHueInterface.App.Services;
using MidiHueInterface.Infra.Clients;
using MidiHueInterface.Infra.Interfaces;
using MidiHueInterface.Infra.Repositories;
using Polly;
using Polly.RateLimiting;
using AppHost = MidiHueInterface.Host;

await Host
    .CreateDefaultBuilder(args)
    .ConfigureServices(configure =>
    {
        configure.AddHostedService<AppHost>();
        configure
            .AddHttpClient<IHueBridgeClient, HueBridgeClient>()
            .AddResilienceHandler("rateLimit", builder =>
            {
                builder.AddRateLimiter(new SlidingWindowRateLimiter(new()
                {
                    Window = TimeSpan.FromSeconds(1),
                    SegmentsPerWindow = 1,
                    PermitLimit = 10,
                    QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
                    QueueLimit = 5
                }));
            });
        
        configure.AddTransient<IHueBridgeClient, HueBridgeClient>();
        configure.AddTransient<IMidiClient, MidiClient>();
        configure.AddTransient<ILightBulbRepository, LightbulbRepository>();
        configure.AddTransient<IMidiEventRepository, MidiEventRepository>();

        configure.AddTransient<IAutomationService, AutomationService>();
        configure.AddTransient<IMessageEventService, MessageEventService>();
    })
    .RunConsoleAsync();