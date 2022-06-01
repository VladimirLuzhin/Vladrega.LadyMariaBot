using Telegram.Bot;
using Telegram.Bot.Types;
using Vladrega.LadyMariaBot.Core.Telegram.Extensions;
using Vladrega.LadyMariaBot.Core.Telegram.Handlers.Text.Attributes;

namespace Vladrega.LadyMariaBot.Core.Telegram.Handlers.Text.Commands;

[Command("/decide")]
public sealed class DecideCommandHandler : ICommandHandler
{
    private static readonly Random Random = new();
    
    public Task HandleAsync(ITelegramBotClient botClient, Chat chat, User sender, CancellationToken cancellationToken, params string[] arguments)
    {
        if (arguments.Length == 0)
            return botClient.SendTextMessageAsync(chat.Id, $"⚠{sender.GetName()} вы неправильно используете формат охотников по команде /decide", cancellationToken: cancellationToken);

        var choice = arguments[Random.Next(arguments.Length - 1)];
        // TODO поработать над формулировкой, добавить варианты "Опредленно", "Именно" и т.д.
        return botClient.SendTextMessageAsync(chat.Id, $"{sender.GetName()} выбор пал на вариант \"{choice}\"", cancellationToken: cancellationToken);
    }
}