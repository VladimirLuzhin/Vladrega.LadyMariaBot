using Microsoft.Extensions.Options;
using Telegram.Bot.Extensions.Polling;
using Vladrega.LadyMariaBot;
using Vladrega.LadyMariaBot.Configuration;
using Vladrega.LadyMariaBot.Core;
using Vladrega.LadyMariaBot.Core.Telegram;
using Vladrega.LadyMariaBot.Core.Telegram.DuelClub;
using Vladrega.LadyMariaBot.Core.Telegram.Handlers;
using Vladrega.LadyMariaBot.Core.Telegram.Handlers.Text.Commands;
using Vladrega.LadyMariaBot.Dependencies;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        services.AddMemoryCache();
        services.Configure<Settings>(context.Configuration.GetSection(Settings.ConfigureSection));
        services.AddSingleton<TelegramBotMessageHandlersFactory>();
        services.AddSingleton<IUpdateHandler, TelegramUpdateHandler>();
        services.AddSingleton<CommandParser>();
        services.AddSingleton<DuelClub>();
        services.AddHostedService<Worker>();
        
        services.AddSingleton(serivces =>
        {
            var settings = serivces.GetService<IOptions<Settings>>();
            var updateHandler = serivces.GetService<IUpdateHandler>();
            var loggerFactory = serivces.GetService<ILoggerFactory>();
            
            return new LadyMaria(settings.Value.Streamers, settings.Value.TelegramBotToken, settings.Value.TwitchApiAccessToken, settings.Value.TwitchApiClientId, updateHandler, loggerFactory);
        });

        services.RegisterTelegramDependencies();
    })
    .Build();

await host.RunAsync();