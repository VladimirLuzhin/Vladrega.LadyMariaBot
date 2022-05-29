using Telegram.Bot;
using Telegram.Bot.Types;

namespace Vladrega.LadyMariaBot.Core.Telegram.Handlers.Text;

public class TextMessageHandler : ITelegramBotUpdateHandler
{
    private readonly TextMessageParser _textMessageParser;

    public TextMessageHandler()
    {
        _textMessageParser = new TextMessageParser();
    }

    public Task HandleAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        return _textMessageParser.Parse(update.Message).HandleAsync(botClient, update.Message, cancellationToken);
    }
}