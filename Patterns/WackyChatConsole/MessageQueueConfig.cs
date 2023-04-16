using Microsoft.Extensions.Configuration;

namespace WackyChatConsole;

public class MessageQueueConfig
{
    public string User { get; set; } = "UserNotSet";

    public string Password { get; set; } = "PasswordNotSet";

    public string Host { get; set; } = "HostNotSet";

    public string ConnectionString => $"host={Host};virtualHost={User};username={User};password={Password}";

    public static MessageQueueConfig Load()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", false);

        IConfiguration config = builder.Build();

        return config.GetSection("RabbitMq").Get<MessageQueueConfig>()!;
    }
}