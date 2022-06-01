namespace Vladrega.LadyMariaBot.Configuration;

public class Settings
{
    public const string ConfigureSection = "Settings";

    public string TelegramBotToken { get; init; }
    
    public string TwitchApiAccessToken { get; init; }
    
    public string TwitchApiClientId { get; init; }

    public Dictionary<string, long[]> Streamers { get; init; }
}