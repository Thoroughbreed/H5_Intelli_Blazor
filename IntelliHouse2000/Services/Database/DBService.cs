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
    public IQueryable<LogMessage> GetLogs(int amount, LogType type)
    {
        var logs = _context.Messages.Where(l => l.Topic.Contains(type.ToString()))
                                                        .OrderByDescending(l => l.Id)
                                                        .Take(amount);
        return logs;
    }

    public async Task<bool> WriteLogAsync(LogMessage log)
    {
        try
        {
            await _context.Messages.AddAsync(log);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}