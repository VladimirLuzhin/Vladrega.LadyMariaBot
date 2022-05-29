using Telegram.Bot.Types;

namespace Vladrega.LadyMariaBot.Core.Telegram.Handlers.Text;

public class TextMessageParser
{
    public ITextCommand Parse(Message message)
    {
        if (!message.Text.StartsWith("/"))
            return new DefaultMessageCommand();

        if (message.Text.StartsWith("/mycommand"))
            return new MyCommand();

        if (message.Text.StartsWith("/streamerinfo"))
            return new StreamerInfoCommand();

        return new UnknownCommand();
    }
}