using System.Text;
using IntelliHouse2000.Helpers;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Client.Connecting;
using MQTTnet.Client.Disconnecting;
using MQTTnet.Client.Options;
using MQTTnet.Client.Receiving;

namespace IntelliHouse2000.Services.MQTT;


[LifeTime(ServiceLifetime.Singleton)]
public class MQTTService : IMQTTService
{
    private IMqttClient _mqttClient;
    private IMqttClientOptions _mqttClientOptions;

    public event EventHandler<MqttClientConnectedEventArgs> Connected;
    public event EventHandler<MqttClientDisconnectedEventArgs> Disconnected;
    public event EventHandler<MqttApplicationMessageReceivedEventArgs> MessageReceived;

    public MQTTService(IConfiguration config)
    {
        Initialize(new MqttClientOptionsBuilder()
            .WithClientId(GenerateUniqueClientId())
            .WithCleanSession(true)
            .WithTcpServer(config["MQTT:BaseUrl"])
            .WithCredentials(new MqttClientCredentials
            {
                Username = config["MQTT:Username"],
                Password = Encoding.UTF8.GetBytes(config["MQTT:Password"])
            })
            .Build());
        
        var _ = Task.Run(async () => await Connect()).Result;
    }
    private string GenerateUniqueClientId()
    {
        string guid = Guid.NewGuid().ToString();
        string randomValue = guid.Substring(0, 12).Replace("-", "");
        string clientId = $"MQTT_APP_{randomValue}";

        return clientId;
    }
    public bool IsConnected()
    {
        return _mqttClient.IsConnected;
    }

    public async Task<bool> Connect()
    {
        try
        {
            if (!_mqttClient.IsConnected) await _mqttClient.ConnectAsync(_mqttClientOptions);
        }
        catch (Exception ex)
        {
            return false;
        }

        return true;
    }

    public async Task<bool> Reconnect()
    {
        try
        {
            await _mqttClient.ReconnectAsync();
        }
        catch (Exception ex)
        {
            return false;
        }

        return true;
    }

    public async Task<bool> Disconnect()
    {
        try
        {
            await _mqttClient.DisconnectAsync();
        }
        catch (Exception ex)
        {
            return false;
        }

        return true;
    }

    public void Initialize(IMqttClientOptions mqttClientOptions)
    {
        try
        {
            _mqttClientOptions = mqttClientOptions;
            var factory = new MqttFactory();
            _mqttClient = factory.CreateMqttClient();
            _mqttClient.ConnectedHandler = new MqttClientConnectedHandlerDelegate(mqttClientConnectedEventArgs =>
            {
                Connected?.Invoke(this, mqttClientConnectedEventArgs);
            });
            _mqttClient.DisconnectedHandler = new MqttClientDisconnectedHandlerDelegate(disconnectEventArgs =>
            {
                Disconnected?.Invoke(this, disconnectEventArgs);
            });
            _mqttClient.ApplicationMessageReceivedHandler =
                new MqttApplicationMessageReceivedHandlerDelegate(messageReceivedArgs =>
                {
                    MessageReceived?.Invoke(this, messageReceivedArgs);
                });
        }
        catch (Exception ex)
        {
            // Blergh
        }
    }

    public async Task<bool> Subscribe(string topic)
    {
        try
        {
            await _mqttClient.SubscribeAsync(topic);
        }
        catch (Exception ex)
        {
            return false;
        }

        return true;
    }

    public async Task<bool> Publish(MqttApplicationMessage message)
    {
        try
        {
            await Connect();
            await _mqttClient.PublishAsync(message);
        }
        catch (Exception ex)
        {
            return false;
        }

        return true;
    }
    public async Task StartAsync()
    {
        await Connect();
    }
}

[IgnoreService]
internal class MqttConnectedHandler : IMqttClientConnectedHandler
{
    Action<MqttClientConnectedEventArgs> _connectedAction;

    public MqttConnectedHandler(Action<MqttClientConnectedEventArgs> connectedAction)
    {
        _connectedAction = connectedAction;
    }

    public async Task HandleConnectedAsync(MqttClientConnectedEventArgs eventArgs)
    {
        _connectedAction?.Invoke(eventArgs);
    }
}

[IgnoreService]
internal class MqttDisconnectedHandler : IMqttClientDisconnectedHandler
{
    Action<MqttClientDisconnectedEventArgs> _disconnectedAction;

    public MqttDisconnectedHandler(Action<MqttClientDisconnectedEventArgs> disconnectAction)
    {
        _disconnectedAction = disconnectAction;
    }

    public async Task HandleDisconnectedAsync(MqttClientDisconnectedEventArgs eventArgs)
    {
        _disconnectedAction?.Invoke(eventArgs);
    }
}

[IgnoreService]
internal class MqttMessageReceivedHandler : IMqttApplicationMessageReceivedHandler
{
    Action<MqttApplicationMessageReceivedEventArgs> _messageReceived;

    public MqttMessageReceivedHandler(Action<MqttApplicationMessageReceivedEventArgs> messageReceived)
    {
        _messageReceived = messageReceived;
    }

    public async Task HandleApplicationMessageReceivedAsync(MqttApplicationMessageReceivedEventArgs eventArgs)
    {
        _messageReceived?.Invoke(eventArgs);
    }
}