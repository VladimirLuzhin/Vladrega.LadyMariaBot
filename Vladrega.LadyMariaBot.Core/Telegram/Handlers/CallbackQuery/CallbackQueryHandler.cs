using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Vladrega.LadyMariaBot.Core.Telegram.Extensions;
using Vladrega.LadyMariaBot.Core.Telegram.Handlers.Text.Attributes;
using Vladrega.LadyMariaBot.Core.Telegram.Handlers.Text.Commands;

namespace Vladrega.LadyMariaBot.Core.Telegram.Handlers.CallbackQuery;

[TelegramUpdateHandler(UpdateType.CallbackQuery)]
public class CallbackQueryHandler : ITelegramBotUpdateHandler
{
    private readonly DuelClub.DuelClub _duelClub;

    public CallbackQueryHandler(DuelClub.DuelClub duelClub)
    {
        _duelClub = duelClub;
    }
    
    public async Task HandleAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        var callbackQuery = update.CallbackQuery;
        var isDuelAccepted = _duelClub.TryAcceptDuel(Guid.Parse(callbackQuery.Data), callbackQuery.From, out var winner);
        if (!isDuelAccepted)
        { 
            await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, $"{callbackQuery.From.GetName()} время принятия дуэли истекло ⏲", cancellationToken: cancellationToken);
            return;
        }

        await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, $"{callbackQuery.From.GetName()} принял вызов 💪", cancellationToken: cancellationToken);
        await Task.Delay(TimeSpan.FromSeconds(3), cancellationToken);
        await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, $"В дуэли победил 👑 {winner.GetName()}", cancellationToken: cancellationToken);
    }
}