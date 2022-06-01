using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Vladrega.LadyMariaBot.Core.Telegram.Extensions;
using Vladrega.LadyMariaBot.Core.Telegram.Handlers.Text.Attributes;

namespace Vladrega.LadyMariaBot.Core.Telegram.Handlers.Text.Commands;

[Command("/roll")]
public class RollCommandHandler : ICommandHandler
{
    private enum RollType : byte
    {
        Unknown = 0,
        D4 = 4,
        D6 = 6,
        D8 = 8,
        D10 = 10,
        D12 = 12,
        D20 = 20
    }

    private static readonly Random Random = new(); 
    
    public async Task HandleAsync(ITelegramBotClient botClient, Chat chat, User sender, CancellationToken cancellationToken, params string[] arguments)
    {
        var rollType = GetRollType(arguments);
        if (rollType == RollType.Unknown)
        {
            await botClient.SendTextMessageAsync(chat.Id, $"⚠Передано некорректное значение для параметра команды /roll. Формат команды /roll d20|d12|d10|d8|d6|d4", cancellationToken: cancellationToken);
            return;
        }

        const int minRollValue = 1;
        var rollValue = Random.Next(minRollValue, (int) rollType);
        await botClient.SendTextMessageAsync(chat.Id, $"{sender.GetName()} бросил 🎲<strong>{rollValue}</strong>", cancellationToken: cancellationToken, parseMode: ParseMode.Html);
    }
    
    private static RollType GetRollType(string[] args)
    {
        if (args.Length > 1)
            return RollType.Unknown;
        
        // передана просто команда roll
        if (args.Length == 0)
            return RollType.D20;

        // если передан roll + конкретная вариация
        var rollTypeString = args[0];
        return Enum.TryParse<RollType>(rollTypeString, true, out var rollType) 
            ? rollType
            : RollType.Unknown;
    }
}