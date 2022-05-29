using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Vladrega.LadyMariaBot.Core.Telegram.Handlers.Text;

namespace Vladrega.LadyMariaBot.Core.Telegram.Handlers;

public class TelegramBotMessageHandlersFactory
{
    public ITelegramBotUpdateHandler GetHandler(UpdateType updateType)
    {
        switch (updateType)
        {
            case UpdateType.Message:
                return new TextMessageHandler();
            case UpdateType.EditedMessage:
                return new EditMessageHandler();
            default:
                return new StubUpdateHandler();
        }
    }
}

public class EditMessageHandler : ITelegramBotUpdateHandler
{
    public Task HandleAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        return botClient.SendTextMessageAsync(update.EditedMessage.Chat.Id, $"Сообщение: \"{update.EditedMessage.Text}\" было отредактировано в {update.EditedMessage.EditDate}");
    }
}