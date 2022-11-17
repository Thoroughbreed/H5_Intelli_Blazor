using IntelliHouse2000.Models;

namespace IntelliHouse2000.Services.Database;

public interface IDBService
{
    public List<LogMessageDTO> GetLogs(int amount, LogType type);
    public Task<bool> WriteLogAsync(LogMessage log);
}