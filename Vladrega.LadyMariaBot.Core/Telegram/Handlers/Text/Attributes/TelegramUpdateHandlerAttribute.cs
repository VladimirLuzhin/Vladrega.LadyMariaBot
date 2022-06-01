using Telegram.Bot.Types.Enums;

namespace Vladrega.LadyMariaBot.Core.Telegram.Handlers.Text.Attributes;

[AttributeUsage(AttributeTargets.Class)]
public class TelegramUpdateHandlerAttribute : Attribute
{
    public UpdateType UpdateType { get; }

    public TelegramUpdateHandlerAttribute(UpdateType updateType)
    {
        UpdateType = updateType;
    }
}