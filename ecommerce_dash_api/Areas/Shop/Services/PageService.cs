using System.Collections.Generic;
using ecommerce_dash_api.Areas.Shop.Interfaces;
using ecommerce_dash_api.Areas.Shop.QRYS;
using ecommerce_dash_api.Utils;

namespace ecommerce_dash_api.Areas.Shop.Services
{
    public class PageService : IPageService
    {
        private readonly IPageRepository _pageRepository;
        public PageService(IPageRepository pageRepository) 
        {
            _pageRepository = pageRepository;
        }

        //+------------------------------------------------------------------+
        //| Home                                            
        //+------------------------------------------------------------------+
        public async Task<ProductsAndBrandsQRY> GetHomePageProductsAndBrandsAsync(int pageNbr, int pageSize)
        {
            var result = new ProductsAndBrandsQRY();
            var taskGetProductsByCategoryAndSize = _pageRepository.GetAllProductsByCategoryAndSize(pageNbr, pageSize);
            var taskGetAllBrands = _pageRepository.GetAllBrands();
            await Task.WhenAll(taskGetProductsByCategoryAndSize, taskGetAllBrands);
            result.categories = taskGetProductsByCategoryAndSize.Result;
            result.brands = taskGetAllBrands.Result;
            return result;
        }

        //+------------------------------------------------------------------+
        //| Products Search                                            
        //+------------------------------------------------------------------+
        public async Task<List<ShopProductQRY>> GetProductsByCategoryAndPageAsync(int categoryId, int pageNbr, int pageSize)
        {
            var result = new List<ShopProductQRY>();
            result = await _pageRepository.GetProductsByCategoryAndSize(categoryId, pageNbr, pageSize);
            return result;
        }
    }
}
