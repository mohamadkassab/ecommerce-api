using ecommerce_dash_api.Areas.Shop.QRYS;

namespace ecommerce_dash_api.Areas.Shop.Interfaces
{
    public interface IPageService
    {       
        //+------------------------------------------------------------------+
        //| Home                                            
        //+------------------------------------------------------------------+
        Task<ProductsAndBrandsQRY> GetHomePageProductsAndBrandsAsync(int pageNbr, int pageSize);

        //+------------------------------------------------------------------+
        //| Products Search                                            
        //+------------------------------------------------------------------+
        Task<SearchProductsQRY> GetProductsByCategoryAndPageAsync(int categoryId, int pageNbr, int pageSize);
        Task<SearchProductsQRY> GetProductsByQueryAsync(string query, int pageNbr, int pageSize);
    }
}
