using Telegram.Bot;
using Vladrega.LadyMariaBot.Core;
using Vladrega.LadyMariaBot.Core.Telegram;

namespace Vladrega.LadyMariaBot;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;

    public Worker(ILogger<Worker> logger, LadyMaria ladyMaria)
    {
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            
            
        }
    }
}