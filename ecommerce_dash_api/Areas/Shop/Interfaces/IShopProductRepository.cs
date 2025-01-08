using ecommerce_dash_api.Areas.Shop.QRYS;

namespace ecommerce_dash_api.Areas.Shop.Interfaces
{
    public interface IShopProductRepository
    {
        Task<List<CategoryProductQRY>> GetProductsByCategoryAndSize(int categoryId = 0, int page = 1, int pageSize = 12);
    }
}
