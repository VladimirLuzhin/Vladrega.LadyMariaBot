using System.Data;
using System.Reflection;
using Telegram.Bot.Types.Enums;
using Vladrega.LadyMariaBot.Core.Telegram.Handlers.Text.Attributes;

namespace Vladrega.LadyMariaBot.Core.Telegram.Handlers;

public class TelegramBotMessageHandlersFactory
{
    private readonly Dictionary<UpdateType, ITelegramBotUpdateHandler> _handlersCache = new();
    
    private readonly IServiceProvider _serviceProvider;
    
    public TelegramBotMessageHandlersFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
        
        var applicationTypes = AppDomain.CurrentDomain
            .GetAssemblies()
            .SelectMany(t => t.GetTypes())
            .Where(t => t.GetInterfaces().Any(t => t == typeof(ITelegramBotUpdateHandler)));

        foreach (var type in applicationTypes)
        {
            var updateHandlerAttribute = type.GetCustomAttribute<TelegramUpdateHandlerAttribute>();
            if (updateHandlerAttribute == null)  
                continue;

            if (_handlersCache.ContainsKey(updateHandlerAttribute.UpdateType))
                throw new InvalidConstraintException($"Повторение обработчика команды: {updateHandlerAttribute.UpdateType}");
                
            _handlersCache.Add(updateHandlerAttribute.UpdateType, (ITelegramBotUpdateHandler) serviceProvider.GetService(type)!);
        }    
    }

    public ITelegramBotUpdateHandler GetHandler(UpdateType updateType)
    {
        if (_handlersCache.TryGetValue(updateType, out var updateHandler))
            return updateHandler;

        return (ITelegramBotUpdateHandler) _serviceProvider.GetService(typeof(StubUpdateHandler))!;
    }
}