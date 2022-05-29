using Telegram.Bot;
using Telegram.Bot.Types;

namespace Vladrega.LadyMariaBot.Core.Telegram.Handlers.Text;

public class DefaultMessageCommand : ITextCommand
{
    public Task HandleAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        return botClient.SendTextMessageAsync(message.Chat.Id, "Hello", cancellationToken: cancellationToken);
    }
}