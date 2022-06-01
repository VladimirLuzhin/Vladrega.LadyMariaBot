namespace Vladrega.LadyMariaBot.Core.Telegram.Handlers.Text.Attributes;

[AttributeUsage(AttributeTargets.Class, Inherited = false)]
public class CommandAttribute : Attribute
{
    public string CommandName { get; }

    public CommandAttribute(string commandName)
    {
        CommandName = commandName;
    }
}