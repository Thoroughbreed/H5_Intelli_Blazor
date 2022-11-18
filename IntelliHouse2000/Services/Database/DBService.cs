using IntelliHouse2000.Models;
using Microsoft.VisualBasic;

namespace IntelliHouse2000.Services.Database;

public class DBService : IDBService
{
    private readonly DBContext _context;
    public DBService(DBContext context)
    {
        _context = context;
    }
    public List<LogMessageDTO> GetLogs(int amount, LogType type)
    {
        var debug1 = type.ToString();
        var logs = _context.Messages.Where(l => l.Topic.Contains(type.ToString()))
                                                        .OrderByDescending(l => l.Id)
                                                        .Take(amount).ToList();
        return logs;
    }

    public async Task<bool> WriteLogAsync(LogMessage log)
    {
        LogMessageDTO logDTO = new LogMessageDTO
        {
            Client = log.Client,
            Message = log.Message,
            QoS = 0,
            Retain = 0,
            Timestamp = log.Timestamp,
            Topic = log.Topic
        };
        try
        {
            _context.Messages.Add(logDTO);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}