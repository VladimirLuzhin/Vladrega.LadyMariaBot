using Telegram.Bot;
using Telegram.Bot.Types;

namespace Vladrega.LadyMariaBot.Core.Telegram.Handlers;

public class StubUpdateHandler : ITelegramBotUpdateHandler
{
    public Task HandleAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}