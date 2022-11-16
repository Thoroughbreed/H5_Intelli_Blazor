using IntelliHouse2000.Helpers;
using MQTTnet;
using MQTTnet.Client.Connecting;
using MQTTnet.Client.Disconnecting;
using MQTTnet.Client.Options;
using MQTTnet.Protocol;

namespace IntelliHouse2000.Services.MQTT;

public interface IMQTTService
{
    public Task<bool> Subscribe(string topic);
    public Task<bool> Unsubscribe(string topic);
    
    public void Initialize(IMqttClientOptions mqttClientOptions);
    public Task<bool> Connect();
    public bool IsConnected();
    public Task<bool> Disconnect();
    public Task<bool> Reconnect();
    public Task StartAsync();
    
    public Task<bool> Publish(MqttApplicationMessage message);
    public Task<bool> Publish(string topic, byte[] payload, bool retain = false, MqttQualityOfServiceLevel qos = MqttQualityOfServiceLevel.AtMostOnce);
    public Task<bool> Publish(string topic, string payload, bool retain = false, MqttQualityOfServiceLevel qos = MqttQualityOfServiceLevel.AtMostOnce);
    
    public event EventHandler<MqttClientConnectedEventArgs> Connected;
    public event EventHandler<MqttClientDisconnectedEventArgs> Disconnected;
    public event EventHandler<MqttApplicationMessageReceivedEventArgs> MessageReceived;
}