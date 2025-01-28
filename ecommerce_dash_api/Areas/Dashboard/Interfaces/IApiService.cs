using ecommerce_dash_api.Models;

namespace ecommerce_dash_api.Areas.Dashboard.Interfaces
{
    public interface IApiService
    {
        //+------------------------------------------------------------------+
        //| Log                                            
        //+------------------------------------------------------------------+
        Task CreateLogAsync(string? logLevel, string? message, string? stackTrace, string? username, string? ipAddress, string? functionName, object? functionParameters);
        Task CreateLogAsync(ApiLog apiLog);
    }
}
