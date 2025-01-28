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
        public async Task CreateLogAsync(string? logLevel, string? message, string? stackTrace, string? username, string? ipAddress, string? functionName, object? functionParameters)
        {
            try
            {
                using (var _context2 = new EcommerceContext()) 
                {
                    ApiLog apiLog = new ApiLog
                    {
                        LogLevel = logLevel,
                        Message = message,
                        StackTrace = stackTrace,
                        Username = username,
                        IpAddress = ipAddress,
                        FunctionName = functionName,
                        FunctionParameters = JsonConvert.SerializeObject(functionParameters)
                    };

                    _context2.ApiLogs.Add(apiLog);
                    await _context2.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }        
        }
        public async Task CreateLogAsync(ApiLog apiLog)
        {
            try
            {
                apiLog.FunctionParameters = JsonConvert.SerializeObject(apiLog.FunctionParameters);
                using (var _context2 = new EcommerceContext())
                {
                    _context2.ApiLogs.Add(apiLog);
                    await _context2.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
