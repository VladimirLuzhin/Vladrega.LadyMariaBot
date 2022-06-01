using Microsoft.Extensions.Caching.Memory;
using Telegram.Bot.Types;

namespace Vladrega.LadyMariaBot.Core.Telegram.DuelClub;

public class DuelClub
{
    private readonly IMemoryCache _memoryCache;

    private readonly Random _randomOfDestiny = new Random();
    
    public DuelClub(IMemoryCache memoryCache)
    {
        _memoryCache = memoryCache;
    }

    public void StartDuel(Guid duelId, User hunter)
    {
        _memoryCache.Set(duelId, hunter, new MemoryCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(10)
        });
    }

    public bool TryAcceptDuel(Guid duelId, User secondHunter, out User winner)
    {
        winner = null!;
        if (!_memoryCache.TryGetValue<User>(duelId, out var firstHunter))
            return false;
        
        winner = GetDuelWinner(firstHunter, secondHunter);
        _memoryCache.Remove(duelId);
        return true;
    }

    private User GetDuelWinner(User firstHunter, User secondHunter)
    {
        var firstHunterRoll = _randomOfDestiny.Next(100);
        var secondHunterRoll = _randomOfDestiny.Next(100);

        return firstHunterRoll > secondHunterRoll
            ? firstHunter
            : secondHunter;
    }
}