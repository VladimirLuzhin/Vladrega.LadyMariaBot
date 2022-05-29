using Telegram.Bot;
using Telegram.Bot.Types;

namespace Vladrega.LadyMariaBot.Core.Telegram.Handlers.Text;

public class MyCommand : ITextCommand
{
    public Task HandleAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        return botClient.SendTextMessageAsync(message.Chat.Id, $"Текущее время: {DateTime.Now}", cancellationToken: cancellationToken);
    }
}