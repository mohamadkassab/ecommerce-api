using ecommerce_dash_api.Areas.Shop.DTOS;
using ecommerce_dash_api.Areas.Shop.QRYS;
using ecommerce_dash_api.Models;

namespace ecommerce_dash_api.Areas.Shop.Interfaces
{
    public interface IElasticSearchService
    {
        Task IndexProductAsync(ShopProductQRY product);
        Task UpdateIndexProductAsync(int productId, int transactionValue);
        Task UpdateIndexProductAsync(Product product);
        Task UpdateIndexProductAsync(ProductInfo productInfo, List<ProductCategory> productCategories);
        Task<List<ShopProductQRY>> SearchProductsAsync(SearchQueryDTO searchQuery);
        Task<long> GetCountSearchProductsAsync(SearchQueryDTO searchQuery);
        Task<FilterSortQRY> GetUniqueValuesAsync(SearchQueryDTO searchQuery);
        Task UpdateElasticDatabaseAsync();
    }
}
