using Telegram.Bot;
using Telegram.Bot.Types;

namespace Vladrega.LadyMariaBot.Core.Telegram.Handlers.Text;

public class UnknownCommand : ITextCommand
{
    public Task HandleAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        return botClient.SendTextMessageAsync(message.Chat.Id, $"Неизвестная комманда: ```{message.Text}```", cancellationToken: cancellationToken);
    }
}