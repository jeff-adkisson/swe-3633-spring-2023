using EasyNetQ;

namespace WackyChatConsole;

public class MessageQueueClient
{
    public delegate void MessageReceived(Message msg);

    private IBus? _bus;
    private MessageQueueConfig? _rabbitMqConfig;

    public event MessageReceived? Receive;

    public async Task<bool> Connect()
    {
        while (true)
            try
            {
                _rabbitMqConfig = MessageQueueConfig.Load();
                _bus = RabbitHutch.CreateBus(_rabbitMqConfig.ConnectionString);

                //subscribe to messages
                await _bus!.PubSub.SubscribeAsync<Message>(
                    Machine.Name,
                    msg => { Receive?.Invoke(msg); },
                    subscriptionConfig => subscriptionConfig.WithExpires(5 * 60 * 1000));

                break;
            }
            catch (Exception e)
            {
                var msg = $"Failed to establish connection to message bus at {_rabbitMqConfig!.Host}. " +
                          $"Verify you are connected to a network and that the appsettings.json configuration is correct. " +
                          $"The error returned by the application was {e.Message}. Click Cancel to end or Retry to try again.";
                await Console.Error.WriteLineAsync($"{msg}. {e.Message}");
                return false;
            }

        return true;
    }

    public void Disconnect()
    {
        if (_bus == null) return;
        _bus!.Dispose(); //clean up when form is closing
        _bus = null;
    }

    private async Task Send(Message msg)
    {
        if (_bus == null) throw new ArgumentNullException(nameof(_bus), "Connect first");
        await _bus!.PubSub.PublishAsync(msg);
    }

    public async Task Send(string text)
    {
        var msg = new Message { Text = text, From = Machine.Name };
        await Send(msg);
    }
}