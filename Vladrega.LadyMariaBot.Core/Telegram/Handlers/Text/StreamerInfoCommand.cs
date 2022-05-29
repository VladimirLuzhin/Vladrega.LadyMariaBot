using Telegram.Bot;
using Telegram.Bot.Types;

namespace Vladrega.LadyMariaBot.Core.Telegram.Handlers.Text;

public class StreamerInfoCommand : ITextCommand
{
    public Task HandleAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        return botClient.SendTextMessageAsync(message.Chat.Id, "Всем привет, меня зовут Владимир и я алкоголик (разработчик)", cancellationToken: cancellationToken);
    }
}