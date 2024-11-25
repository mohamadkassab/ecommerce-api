using ecommerce_dash_api.DTOS;
using ecommerce_dash_api.Models;

namespace ecommerce_dash_api.Interfaces
{
    public interface IApiRepository
    {
        //+------------------------------------------------------------------+
        //| Log                                            
        //+------------------------------------------------------------------+
        Task CreateLogAsync(ApiLog log);
    }
}
