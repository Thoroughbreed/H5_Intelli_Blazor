using IntelliHouse2000.Helpers;
using IntelliHouse2000.Models.Alarm;
using IntelliHouse2000.Services.MQTT;
using MQTTnet.Protocol;

namespace IntelliHouse2000.Services.Alarm;

public class AlarmService : IAlarmService
{
    private readonly IMQTTService _mqttService;
    public AlarmService(IMQTTService mqttService)
    {
        _mqttService = mqttService;
    }
    
    public Task<bool> SetArmed(ArmedState state)
    {
        return _mqttService.Publish(Constants.MqttArmedTopic, ((int)state).ToString(), false, MqttQualityOfServiceLevel.AtLeastOnce);
    }
}