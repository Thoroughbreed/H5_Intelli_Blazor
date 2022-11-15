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
    private readonly HttpContextAccessor _httpContextAccessor;
    
    public APIService() // HttpClient client, HttpContextAccessor httpContextAccessor
    {
        // _client = client;
        // _httpContextAccessor = httpContextAccessor;
        _client = new HttpClient();
        _httpContextAccessor = new HttpContextAccessor();
        HttpClientHandler clientHandler = new();
        clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true;
    }

    private async Task<string> InitializeHttpClient()
    {
        var bearerToken = await _httpContextAccessor.HttpContext!.GetTokenAsync("access_token");
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", bearerToken);
        return bearerToken ?? "404";
    }

    public async Task<List<LogMessage>> GetCriticalLogsAsync()
    {
        await InitializeHttpClient();
        var logs = await _client.GetFromJsonAsync<List<LogMessage>>(new Uri(Constants.ApiBaseUrl + "critical"));
        return logs ?? new List<LogMessage>{ new LogMessage { Client = "System", Timestamp = DateTime.Now, Message = "No logs found?"}};
    }
    
    public async Task<List<LogMessage>> GetSystemLogsAsync()
    {
        await InitializeHttpClient();
        var logs = await _client.GetFromJsonAsync<List<LogMessage>>(new Uri(Constants.ApiBaseUrl + "system"));
        return logs ?? new List<LogMessage>{ new LogMessage { Client = "System", Timestamp = DateTime.Now, Message = "No logs found?"}};
    }
    
    public async Task<List<LogMessage>> GetInfoLogsAsync()
    {
        await InitializeHttpClient();
        var logs = await _client.GetFromJsonAsync<List<LogMessage>>(new Uri(Constants.ApiBaseUrl + "info"));
        return logs ?? new List<LogMessage>{ new LogMessage { Client = "System", Timestamp = DateTime.Now, Message = "No logs found?"}};
    }
    
    public async Task<List<APIClimate>> GetKitchenListAsync()
    {
        await InitializeHttpClient();
        var logs = await _client.GetFromJsonAsync<List<APIClimate>>(new Uri(Constants.ApiBaseUrl + "kitchen"));
        return logs ?? new List<APIClimate>();
    }
    
    public async Task<APIClimate> GetKitchenAsync()
    {
        await InitializeHttpClient();
        var logs = await _client.GetFromJsonAsync<APIClimate>(new Uri(Constants.ApiBaseUrl + "kitchen/1"));
        return logs ?? new APIClimate();
    }
    
    public async Task<List<APIClimate>> GetBedroomListAsync()
    {
        await InitializeHttpClient();
        var logs = await _client.GetFromJsonAsync<List<APIClimate>>(new Uri(Constants.ApiBaseUrl + "bedroom"));
        return logs ?? new List<APIClimate>();
    }
    
    public async Task<APIClimate> GetBedroomAsync()
    {
        await InitializeHttpClient();
        var logs = await _client.GetFromJsonAsync<APIClimate>(new Uri(Constants.ApiBaseUrl + "bedroom/1"));
        return logs ?? new APIClimate();
    }
    
    public async Task<List<APIClimate>> GetlivingroomListAsync()
    {
        await InitializeHttpClient();
        var logs = await _client.GetFromJsonAsync<List<APIClimate>>(new Uri(Constants.ApiBaseUrl + "livingroom"));
        return logs ?? new List<APIClimate>();
    }
    
    public async Task<APIClimate> GetlivingroomAsync()
    {
        await InitializeHttpClient();
        var logs = await _client.GetFromJsonAsync<APIClimate>(new Uri(Constants.ApiBaseUrl + "livingroom/1"));
        return logs ?? new APIClimate();
    }

    public async Task<List<Airquality>> GetAirqualityListAsync()
    {
        await InitializeHttpClient();
        var result = await _client.GetFromJsonAsync<List<Airquality>>(new Uri(Constants.ApiBaseUrl + "airq"));
        return result ?? new List<Airquality>();
    }
}