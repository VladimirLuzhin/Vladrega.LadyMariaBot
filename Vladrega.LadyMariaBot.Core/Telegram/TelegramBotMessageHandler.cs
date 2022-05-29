using Telegram.Bot;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;
using Vladrega.LadyMariaBot.Core.Telegram.Handlers;

namespace Vladrega.LadyMariaBot.Core.Telegram;

public sealed class TelegramBotMessageHandler : IUpdateHandler
{
    private readonly TelegramBotMessageHandlersFactory _telegramBotMessageHandlersFactory;

    public TelegramBotMessageHandler()
    {
        _telegramBotMessageHandlersFactory = new TelegramBotMessageHandlersFactory();
    }

    public Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        return _telegramBotMessageHandlersFactory.GetHandler(update.Type).HandleAsync(botClient, update, cancellationToken);
    }

    public Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}