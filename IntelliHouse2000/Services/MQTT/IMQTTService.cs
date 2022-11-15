using IntelliHouse2000.Helpers;
using MQTTnet;
using MQTTnet.Client.Options;
using MQTTnet.Protocol;

namespace IntelliHouse2000.Services.MQTT;

public interface IMQTTService
{
    public Task<bool> Subscribe(string topic);
    public void Initialize(IMqttClientOptions mqttClientOptions);
    public Task<bool> Connect();
    public bool IsConnected();
    public Task<bool> Disconnect();
    public Task<bool> Reconnect();
    public Task StartAsync();
    
    Task<bool> Publish(MqttApplicationMessage message);
    Task<bool> Publish(string topic, byte[] payload, bool retain = false, MqttQualityOfServiceLevel qos = MqttQualityOfServiceLevel.AtMostOnce);
    Task<bool> Publish(string topic, string payload, bool retain = false, MqttQualityOfServiceLevel qos = MqttQualityOfServiceLevel.AtMostOnce);
}