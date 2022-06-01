using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using Vladrega.LadyMariaBot.Core.Telegram.Extensions;
using Vladrega.LadyMariaBot.Core.Telegram.Handlers.Text.Attributes;

namespace Vladrega.LadyMariaBot.Core.Telegram.Handlers.Text.Commands;

[Command("/duel")]
public class DuelCommandHandler : ICommandHandler
{
    private readonly DuelClub.DuelClub _duelClub;

    public DuelCommandHandler(DuelClub.DuelClub duelClub)
    {
        _duelClub = duelClub;
    }

    public Task HandleAsync(ITelegramBotClient botClient, Chat chat, User sender, CancellationToken cancellationToken, params string[] arguments)
    {
        var duelId = Guid.NewGuid();
        
        _duelClub.StartDuel(duelId, sender);
        
        var inlineKeyboard = new InlineKeyboardMarkup(InlineKeyboardButton.WithCallbackData("⚔ Вступить в бой!", duelId.ToString()));
        return botClient.SendTextMessageAsync(chat.Id, $"⚡ {sender.GetName()} вызывает охотников на дуэль", replyMarkup: inlineKeyboard, cancellationToken: cancellationToken);
    }
}