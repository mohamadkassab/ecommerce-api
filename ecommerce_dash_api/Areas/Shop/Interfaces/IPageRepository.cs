using ecommerce_dash_api.Areas.Shop.QRYS;

namespace ecommerce_dash_api.Areas.Shop.Interfaces
{
    public interface IPageRepository
    {
        Task<List<ShopBrandQRY>> GetAllBrands();
        Task<List<CategoryProductQRY>> GetAllProductsByCategoryAndSize(int pagenNbr, int pageSize);
        Task<List<ShopProductQRY>> GetProductsByCategoryAndSize(int categoryId, int pagenNbr, int pageSize);
    }
}
