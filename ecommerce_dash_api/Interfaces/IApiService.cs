using ecommerce_dash_api.Models;

namespace ecommerce_dash_api.Interfaces
{
    public interface IApiService
    {
        Task<bool> CreateLogAsync(string? logLevel, string? message, string? stackTrace, string? username, string? ipAddress, string? functionName, object? functionParameters);
        Task<bool> CreateLogAsync(ApiLog apiLog);
    }
}
