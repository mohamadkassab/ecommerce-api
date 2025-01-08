using ecommerce_dash_api.Models;

namespace ecommerce_dash_api.Areas.Dashboard.Interfaces
{
    public interface IApiRepository
    {
        //+------------------------------------------------------------------+
        //| Log                                            
        //+------------------------------------------------------------------+
        Task CreateLogAsync(ApiLog log);
    }
}
