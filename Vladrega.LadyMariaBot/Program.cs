using Microsoft.Extensions.Options;
using Vladrega.LadyMariaBot;
using Vladrega.LadyMariaBot.Configuration;
using Vladrega.LadyMariaBot.Core;
using Vladrega.LadyMariaBot.Core.Telegram;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        services.Configure<Settings>(context.Configuration.GetSection(Settings.ConfigureSection));
        services.AddHostedService<Worker>();
        services.AddSingleton(serivces =>
        {
            var settings = serivces.GetService<IOptions<Settings>>();
            var logger = serivces.GetService<ILoggerFactory>();
            return new LadyMaria(settings.Value.TelegramBotToken, settings.Value.TwitchApiAccessToken, settings.Value.TwitchApiClientId);
        });
    })
    .Build();

await host.RunAsync();