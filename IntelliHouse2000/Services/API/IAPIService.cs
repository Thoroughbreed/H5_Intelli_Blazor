using IntelliHouse2000.Models;
using IntelliHouse2000.Models.Climate;

namespace IntelliHouse2000.Services.API;


// ReSharper disable once InconsistentNaming
public interface IAPIService
{
    public Task<List<LogMessage>> GetCriticalLogsAsync();
    public Task<List<LogMessage>> GetSystemLogsAsync();
    public Task<List<LogMessage>> GetInfoLogsAsync();
    public Task<List<APIClimate>> GetKitchenListAsync(DateTime? timeStamp);
    public Task<APIClimate> GetKitchenAsync();
    public Task<List<APIClimate>> GetBedroomListAsync(DateTime? timeStamp);
    public Task<APIClimate> GetBedroomAsync();
    public Task<List<APIClimate>> GetlivingroomListAsync(DateTime? timeStamp);
    public Task<APIClimate> GetlivingroomAsync();
    public Task<List<Airquality>> GetAirqualityListAsync();
    public Task<Airquality> GetAirqualityAsync();
}