using ecommerce_dash_api.Areas.Shop.QRYS;

namespace ecommerce_dash_api.Areas.Shop.Interfaces
{
    public interface IPageRepository
    {
        Task<List<ShopBrandQRY>> GetAllBrandsAsync();
        Task<List<CategoryProductQRY>> GetProductsBySizeAsync(int pagenNbr, int pageSize);
        Task<int> GetTotalProductsByCategoryAsync(int categoryId);
    }
}
