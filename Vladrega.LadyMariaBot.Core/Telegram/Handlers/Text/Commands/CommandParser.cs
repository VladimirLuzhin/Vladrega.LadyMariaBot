using System.Data;
using System.Reflection;
using Vladrega.LadyMariaBot.Core.Telegram.Handlers.Text.Attributes;

namespace Vladrega.LadyMariaBot.Core.Telegram.Handlers.Text.Commands;

public class CommandParser
{
    private readonly Dictionary<string, ICommandHandler> _commandsCache = new();
    
    public CommandParser(IServiceProvider serviceProvider)
    {
        var applicationTypes = AppDomain.CurrentDomain
            .GetAssemblies()
            .SelectMany(t => t.GetTypes())
            .Where(t => t.GetInterfaces().Any(t => t == typeof(ICommandHandler)));

        foreach (var type in applicationTypes)
        {
            var textCommandAttribute = type.GetCustomAttribute<CommandAttribute>();
            if (textCommandAttribute == null)  
                continue;

            if (_commandsCache.ContainsKey(textCommandAttribute.CommandName))
                throw new InvalidConstraintException($"Повторение названия команды: {textCommandAttribute.CommandName}");
                
            _commandsCache.Add(textCommandAttribute.CommandName, (ICommandHandler) serviceProvider.GetService(type)!);
        }
    }
    
    public bool TryParse(string commandText, out ICommandHandler commandHandler)
    {
        var commandName = commandText.Split(" ")[0];
        return _commandsCache.TryGetValue(commandName, out commandHandler!);
    }
}