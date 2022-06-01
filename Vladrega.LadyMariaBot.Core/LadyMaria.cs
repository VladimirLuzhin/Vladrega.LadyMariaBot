using System.Text;
using Microsoft.Extensions.Logging;
using Telegram.Bot;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types.Enums;
using TwitchLib.Api;
using TwitchLib.Api.Core;
using TwitchLib.Api.Services;
using TwitchLib.Api.Services.Events.LiveStreamMonitor;

namespace Vladrega.LadyMariaBot.Core;

public sealed class LadyMaria
{
    private readonly TelegramBotClient _telegramBotClient;
    private readonly Dictionary<string, long[]> _streamers;

    public LadyMaria(Dictionary<string, long[]> streamers, string telegramBotToken, string twitchApiToken, string twitchApiClientId, IUpdateHandler updateHandler, ILoggerFactory loggerFactory)
    {
        _streamers = streamers;
        _telegramBotClient = new TelegramBotClient(telegramBotToken);
        _telegramBotClient.StartReceiving(updateHandler, new ReceiverOptions
        {
            AllowedUpdates = new []
            {
               UpdateType.Message,
               UpdateType.EditedMessage,
               UpdateType.CallbackQuery
            }
        });


        var liveStreamMonitor = new LiveStreamMonitorService(new TwitchAPI(settings: new ApiSettings
        {
            Secret = twitchApiToken,
            ClientId = twitchApiClientId
        }, loggerFactory: loggerFactory), checkIntervalInSeconds: 5);
        
        liveStreamMonitor.OnStreamOnline += OnStreamOnline;
        liveStreamMonitor.SetChannelsByName(streamers.Keys.ToList());
        liveStreamMonitor.Start();
    }

    private async void OnStreamOnline(object? sender, OnStreamOnlineArgs onlineArgs)
    {
        var game = onlineArgs.Stream.GameName;
        var title = onlineArgs.Stream.Title;
        var streamUrl = $"https://twitch.tv/{onlineArgs.Stream.UserName}";
        
        var stringBuilder = new StringBuilder();
        
        stringBuilder.AppendLine("<strong>👋 Всем привет, стрим начался!</strong>");
        stringBuilder.AppendLine();
        stringBuilder.AppendLine($"<strong>🎮️ Сегодня трансляция по:</strong> {game}");
        stringBuilder.AppendLine($"<strong>💻 Описание</strong>: {title}");
        stringBuilder.AppendLine();
        stringBuilder.AppendLine(streamUrl);

        var message = stringBuilder.ToString();

        if (!_streamers.TryGetValue(onlineArgs.Stream.UserName, out var telegramGroupIds))
            return;

        foreach (var telegramGroupId in telegramGroupIds)
        {
            await _telegramBotClient.SendTextMessageAsync(telegramGroupId, message, ParseMode.Html);            
        }
    }
}