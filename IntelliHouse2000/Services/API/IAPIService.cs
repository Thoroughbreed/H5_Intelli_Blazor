using IntelliHouse2000.Models;
using IntelliHouse2000.Models.Climate;

namespace IntelliHouse2000.Services.API;


// ReSharper disable once InconsistentNaming
public interface IAPIService
{
    public Task<List<LogMessage>> GetCriticalLogsAsync();
    public Task<List<LogMessage>> GetSystemLogsAsync();
    public Task<List<LogMessage>> GetInfoLogsAsync();
    public Task<List<APIClimate>> GetKitchenListAsync();
    public Task<APIClimate> GetKitchenAsync();
    public Task<List<APIClimate>> GetBedroomListAsync();
    public Task<APIClimate> GetBedroomAsync();
    public Task<List<APIClimate>> GetlivingroomListAsync();
    public Task<APIClimate> GetlivingroomAsync();
    public Task<List<Airquality>> GetAirqualityListAsync();
}