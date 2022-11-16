namespace IntelliHouse2000.Models;

public class LogUser
{
    public int Id { get; set; }
    public string UserName { get; set; }
    public UserEvent Event { get; set; }
    public DateTime TimeStamp { get; set; }
}