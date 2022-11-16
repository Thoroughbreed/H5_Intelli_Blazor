namespace IntelliHouse2000.Services.Alarm;

public interface IAlarmService
{
    Task<bool> SetArmed(ArmedState state);
}