using ecommerce_dash_api.Areas.Dashboard.Interfaces;
using ecommerce_dash_api.Models;
using Newtonsoft.Json;
using System.Net;

namespace ecommerce_dash_api.Areas.Dashboard.Services 
{
    public class ApiService : IApiService
    {
        private readonly EcommerceContext _context;
        private readonly IApiRepository _apiRepository;
        public ApiService(EcommerceContext context, IApiRepository apiRepository)
        {
            _context = context;
            _apiRepository = apiRepository;
        }

        //+------------------------------------------------------------------+
        //| Log                                            
        //+------------------------------------------------------------------+
        public async Task<bool> CreateLogAsync(string? logLevel, string? message, string? stackTrace, string? username, string? ipAddress, string? functionName, object? functionParameters)
        {
            ApiLog apiLog = new ApiLog();
            apiLog.LogLevel = logLevel;
            apiLog.Message = message;
            apiLog.StackTrace = stackTrace;
            apiLog.Username = username;
            apiLog.IpAddress = ipAddress;
            apiLog.FunctionName = functionName;
            apiLog.FunctionParameters = JsonConvert.SerializeObject(functionParameters);

            await _apiRepository.CreateLogAsync(apiLog);
            await _context.SaveChangesAsync();

            return true;
        }
        public async Task<bool> CreateLogAsync(ApiLog apiLog)
        {
            apiLog.FunctionParameters = JsonConvert.SerializeObject(apiLog.FunctionParameters);
            await _apiRepository.CreateLogAsync(apiLog);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
