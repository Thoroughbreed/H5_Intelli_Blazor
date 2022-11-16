namespace IntelliHouse2000.Models;

public class LogMessage
{
    public int Id { get; set; }
    public string Client { get; set; }
    public string Message { get; set; }
    public string Topic { get; set; }
    public DateTime Timestamp { get; set; }
}