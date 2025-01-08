using ecommerce_dash_api.Areas.Dashboard.Interfaces;
using ecommerce_dash_api.Models;

namespace ecommerce_dash_api.Areas.Dashboard.Repositories
{
    public class ApiRepository : IApiRepository
    {
        private readonly EcommerceContext _context;

        public ApiRepository(EcommerceContext context)
        {
            _context = context;
        }

        //+------------------------------------------------------------------+
        //| Log                                            
        //+------------------------------------------------------------------+
        public async Task CreateLogAsync(ApiLog log)
        {
            try
            {
                await _context.ApiLogs.AddAsync(log);
            }
            catch (Exception ex) {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}

