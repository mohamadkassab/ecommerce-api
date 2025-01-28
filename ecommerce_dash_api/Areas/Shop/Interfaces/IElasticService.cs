using ecommerce_dash_api.Areas.Shop.QRYS;
using ecommerce_dash_api.Models;

namespace ecommerce_dash_api.Areas.Shop.Interfaces
{
    public interface IElasticService
    {
        Task IndexProductAsync(ShopProductQRY product);
        Task<List<ShopProductQRY>> SearchProductsAsync(string query, int pageNbr, int pageSize);
        Task<long> GetCountSearchProductsAsync(string query);
        Task UpdateElasticDatabaseAsync();
    }
}
