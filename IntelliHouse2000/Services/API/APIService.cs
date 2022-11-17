using System.Globalization;
using System.Net.Http.Headers;
using IntelliHouse2000.Helpers;
using IntelliHouse2000.Models;
using IntelliHouse2000.Models.Climate;
using Microsoft.AspNetCore.Authentication;

namespace IntelliHouse2000.Services.API;

// ReSharper disable once InconsistentNaming
public class APIService : IAPIService
{
    private readonly HttpClient _client;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly string _apiBaseUrl;

    public APIService(IConfiguration config, HttpClient client, IHttpContextAccessor httpContextAccessor)
    {
        _apiBaseUrl = config["ApiBaseUrl"];
        _client = client;
        _httpContextAccessor = httpContextAccessor;
        HttpClientHandler clientHandler = new();
        clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true;
    }

    private async Task<string> InitializeHttpClient()
    {
        // return "";
        var bearerToken = await _httpContextAccessor.HttpContext!.GetTokenAsync("access_token");
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", bearerToken);
        return bearerToken ?? "404";
    }

    public async Task<List<LogMessage>> GetCriticalLogsAsync()
    {
        await InitializeHttpClient();
        var logs = await _client.GetFromJsonAsync<List<LogMessage>>(new Uri(_apiBaseUrl + "critical"));
        if (logs != null)
        {
            return logs.Take(3).ToList();
        }
        return new List<LogMessage>
            { new LogMessage { Client = "System", Timestamp = DateTime.Now, Message = "No logs found?" } };
    }
    
    public async Task<List<LogMessage>> GetSystemLogsAsync()
    {
        await InitializeHttpClient();
        var logs = await _client.GetFromJsonAsync<List<LogMessage>>(new Uri(_apiBaseUrl + "system"));
        if (logs != null)
        {
            return logs.Take(3).ToList();
        }
        return new List<LogMessage>
            { new LogMessage { Client = "System", Timestamp = DateTime.Now, Message = "No logs found?" } };    }
    
    public async Task<List<LogMessage>> GetInfoLogsAsync()
    {
        await InitializeHttpClient();
        var logs = await _client.GetFromJsonAsync<List<LogMessage>>(new Uri(_apiBaseUrl + "info"));
        if (logs != null)
        {
            return logs.Take(3).ToList();
        }
        return new List<LogMessage>
            { new LogMessage { Client = "System", Timestamp = DateTime.Now, Message = "No logs found?" } };    }
    
    public async Task<List<APIClimate>> GetKitchenListAsync(DateTime? timeStamp)
    {
        await InitializeHttpClient();
        string ts = timeStamp.HasValue ? FixTimestamp(timeStamp.Value) : FixTimestamp(DateTime.Now);
        var logs = await _client.GetFromJsonAsync<List<APIClimate>>(new Uri(_apiBaseUrl + $"kitchen?ts={ts}"));
        return logs ?? new List<APIClimate>();
    }
    
    public async Task<APIClimate> GetKitchenAsync()
    {
        await InitializeHttpClient();
        var logs = await _client.GetFromJsonAsync<APIClimate>(new Uri(_apiBaseUrl + "kitchen/1"));
        return logs ?? new APIClimate();
    }
    
    public async Task<List<APIClimate>> GetBedroomListAsync(DateTime? timeStamp)
    {
        await InitializeHttpClient();
        string ts = timeStamp.HasValue ? FixTimestamp(timeStamp.Value) : FixTimestamp(DateTime.Now);
        var logs = await _client.GetFromJsonAsync<List<APIClimate>>(new Uri(_apiBaseUrl + $"bedroom?ts={ts}"));
        return logs ?? new List<APIClimate>();
    }
    
    public async Task<APIClimate> GetBedroomAsync()
    {
        await InitializeHttpClient();
        var logs = await _client.GetFromJsonAsync<APIClimate>(new Uri(_apiBaseUrl + "bedroom/1"));
        return logs ?? new APIClimate();
    }
    
    public async Task<List<APIClimate>> GetlivingroomListAsync(DateTime? timeStamp)
    {
        await InitializeHttpClient();
        string ts = timeStamp.HasValue ? FixTimestamp(timeStamp.Value) : FixTimestamp(DateTime.Now);
        var logs = await _client.GetFromJsonAsync<List<APIClimate>>(new Uri(_apiBaseUrl + $"livingroom?ts={ts}"));
        return logs ?? new List<APIClimate>();
    }
    
    public async Task<APIClimate> GetlivingroomAsync()
    {
        await InitializeHttpClient();
        var logs = await _client.GetFromJsonAsync<APIClimate>(new Uri(_apiBaseUrl + "livingroom/1"));
        return logs ?? new APIClimate();
    }

    public async Task<List<Airquality>> GetAirqualityListAsync()
    {
        await InitializeHttpClient();
        var result = await _client.GetFromJsonAsync<List<Airquality>>(new Uri(_apiBaseUrl + "airq"));
        return result ?? new List<Airquality>();
    }
    public async Task<Airquality> GetAirqualityAsync()
    {
        await InitializeHttpClient();
        var logs = await _client.GetFromJsonAsync<Airquality>(new Uri(_apiBaseUrl + "airq/1"));
        return logs ?? new Airquality();
    }

    private string FixTimestamp(DateTime ts)
    {
        return ts.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
    }
}