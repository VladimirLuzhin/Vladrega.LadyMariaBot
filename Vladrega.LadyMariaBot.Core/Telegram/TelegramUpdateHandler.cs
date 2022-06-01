using Microsoft.Extensions.Logging;
using Telegram.Bot;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;
using Vladrega.LadyMariaBot.Core.Telegram.Handlers;

namespace Vladrega.LadyMariaBot.Core.Telegram;

public sealed class TelegramUpdateHandler : IUpdateHandler
{
    private readonly TelegramBotMessageHandlersFactory _telegramBotMessageHandlersFactory;
    private readonly ILogger _logger;

    public TelegramUpdateHandler(TelegramBotMessageHandlersFactory telegramBotMessageHandlerFactory, ILoggerFactory loggerFactory)
    {
        _telegramBotMessageHandlersFactory = telegramBotMessageHandlerFactory;
        _logger = loggerFactory.CreateLogger<TelegramUpdateHandler>();
    }

    public async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        try
        {
            await _telegramBotMessageHandlersFactory.GetHandler(update.Type).HandleAsync(botClient, update, cancellationToken);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "При обработке обновления телеграмма произошла ошибка");
        }
    }

    public Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}