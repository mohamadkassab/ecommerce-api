using ecommerce_dash_api.Areas.Shop.QRYS;

namespace ecommerce_dash_api.Areas.Shop.Interfaces
{
    public interface IPageService
    {
        //+------------------------------------------------------------------+
        //| Home                                            
        //+------------------------------------------------------------------+
        Task<HomePageQRY> GetHomePageAsync();
    }
}
