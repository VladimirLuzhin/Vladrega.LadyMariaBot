using Telegram.Bot;
using Telegram.Bot.Types;

namespace Vladrega.LadyMariaBot.Core.Telegram.Handlers.Text;

public interface ITextCommand
{
    Task HandleAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken);
}