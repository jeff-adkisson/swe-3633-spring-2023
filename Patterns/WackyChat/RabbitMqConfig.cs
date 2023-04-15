using Microsoft.Extensions.Configuration;
using System.Diagnostics;

namespace WackyChat;

public class RabbitMqConfig
{
    public static RabbitMqConfig GetConfig()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", false);

        IConfiguration config = builder.Build();

        return config.GetSection("RabbitMq").Get<RabbitMqConfig>();
    }
    
    public string User { get; set; } = "UserNotSet";
    public string Password { get; set; } = "PasswordNotSet";
    public string Host { get; set; } = "HostNotSet";
    public string ConnectionString => $"host={Host};virtualHost={User};username={User};password={Password}";
    public string WhoAmI => $"{Environment.MachineName}.{Environment.UserName}.{Process.GetCurrentProcess().Id}";
}