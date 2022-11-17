namespace IntelliHouse2000.Models;

public class LogMessageDTO : LogMessage
{
    public int QoS { get; set; }
    public int Retain { get; set; }
}