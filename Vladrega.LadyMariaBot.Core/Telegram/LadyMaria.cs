using System.Text;
using Microsoft.Extensions.Logging;
using Telegram.Bot;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types.Enums;
using TwitchLib.Api;
using TwitchLib.Api.Core;
using TwitchLib.Api.Services;
using TwitchLib.Api.Services.Events.LiveStreamMonitor;

namespace Vladrega.LadyMariaBot.Core.Telegram;

public sealed class LadyMaria
{
    private readonly TelegramBotClient _telegramBotClient;

    public LadyMaria(string telegramBotToken, string twitchApiToken, string twitchApiClientId)
    {
        _telegramBotClient = new TelegramBotClient(telegramBotToken);
        _telegramBotClient.StartReceiving<TelegramBotMessageHandler>(new ReceiverOptions
        {
            AllowedUpdates = new []
            {
               UpdateType.Message,
               UpdateType.EditedMessage
            }
        });


        var liveStreamMonitor = new LiveStreamMonitorService(new TwitchAPI(settings: new ApiSettings
        {
            Secret = twitchApiToken,
            ClientId = twitchApiClientId
        }), checkIntervalInSeconds: 5);

        liveStreamMonitor.OnStreamOnline += OnStreamOnline;
        liveStreamMonitor.SetChannelsByName(new List<string> {"vladrega"});
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
        stringBuilder.AppendLine($"<strong>🕹️ Сегодня трансляция по:</strong> {game}");
        stringBuilder.AppendLine($"<strong>💬 Описание</strong>: {title}");
        stringBuilder.AppendLine();
        stringBuilder.AppendLine(streamUrl);
        
        await _telegramBotClient.SendTextMessageAsync(-1001759565168, stringBuilder.ToString(), ParseMode.Html);
    }
}