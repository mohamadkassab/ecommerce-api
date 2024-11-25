using ecommerce_dash_api.Enum;
using ecommerce_dash_api.Interfaces;
using ecommerce_dash_api.Models;
using Org.BouncyCastle.Utilities.Net;
using System.Net;

namespace ecommerce_dash_api.Repositories
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

