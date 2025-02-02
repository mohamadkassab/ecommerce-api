using ecommerce_dash_api.Areas.Shop.DTOS;
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
        Task<SearchProductsQRY> GetProductsByQueryAsync(SearchQueryDTO searchQuery);
    }
}
