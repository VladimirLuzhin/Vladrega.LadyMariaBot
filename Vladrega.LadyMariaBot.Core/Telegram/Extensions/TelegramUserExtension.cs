using Telegram.Bot.Types;

namespace Vladrega.LadyMariaBot.Core.Telegram.Extensions;

public static class TelegramUserExtension
{
    public static string GetName(this User user)
    {
        if (string.IsNullOrWhiteSpace(user.Username))
            return $"{user.FirstName} {user.LastName}";

        return $"@{user.Username}";
    }
}