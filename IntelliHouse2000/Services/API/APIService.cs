namespace IntelliHouse2000.Services.API;

// ReSharper disable once InconsistentNaming
public class APIService : IAPIService
{
    private HttpClient _client;
    
    public APIService(HttpClient client)
    {
        _client = client;
    }

    public async Task<List<LogMessage>> GetCriticalLogsAsync()
    {
        
    }
    
    public async Task<List<LogMessage>> GetCriticalLogsAsync()
    {
        
    }
    
    public async Task<List<LogMessage>> GetCriticalLogsAsync()
    {
        
    }
}


Constants.ApiBaseUrl + "critical"