using System.Collections.Generic;
using ecommerce_dash_api.Areas.Shop.Interfaces;
using ecommerce_dash_api.Areas.Shop.QRYS;
using ecommerce_dash_api.Utils;

namespace ecommerce_dash_api.Areas.Shop.Services
{
    public class PageService : IPageService
    {
        private readonly IShopProductRepository _shopProductRepository;
        public PageService(IShopProductRepository shopProductRepository) 
        {
            _shopProductRepository = shopProductRepository;
        }

        public async Task<HomePageQRY> GetHomePageAsync()
        {
            var result = new HomePageQRY();
            result.SectionImage = await Helpers.GetFileByUrlAsync("C:\\Users\\mhmdk\\Desktop\\Projects\\ecommerce-api\\ecommerce_dash_api\\wwwroot\\images\\section-main-images\\home\\home.jpg");
            List<CategoryProductQRY> products = await _shopProductRepository.GetProductsByCategoryAndSize();
            result.categoryProducts = products;
            return result;
        }
    }
}
