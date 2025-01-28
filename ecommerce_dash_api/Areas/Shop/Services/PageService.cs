using System.Collections.Generic;
using ecommerce_dash_api.Areas.Shop.Interfaces;
using ecommerce_dash_api.Areas.Shop.QRYS;
using ecommerce_dash_api.Utils;

namespace ecommerce_dash_api.Areas.Shop.Services
{
    public class PageService : IPageService
    {
        private readonly IPageRepository _pageRepository;
        private readonly IElasticService _elasticSearchService;
        public PageService(IPageRepository pageRepository, IElasticService elasticSearchService) 
        {
            _pageRepository = pageRepository;
            _elasticSearchService = elasticSearchService;
        }

        //+------------------------------------------------------------------+
        //| Home                                            
        //+------------------------------------------------------------------+
        public async Task<ProductsAndBrandsQRY> GetHomePageProductsAndBrandsAsync(int pageNbr, int pageSize)
        {
            var result = new ProductsAndBrandsQRY();
            var taskGetProductsByCategoryAndSize = _pageRepository.GetProductsBySizeAsync(pageNbr, pageSize);
            var taskGetAllBrands = _pageRepository.GetAllBrandsAsync();
            await Task.WhenAll(taskGetProductsByCategoryAndSize, taskGetAllBrands);
            result.categories = taskGetProductsByCategoryAndSize.Result;
            result.brands = taskGetAllBrands.Result;
            return result;
        }

        //+------------------------------------------------------------------+
        //| Products Search                                            
        //+------------------------------------------------------------------+
        public async Task<SearchProductsQRY> GetProductsByCategoryAndPageAsync(int categoryId, int pageNbr, int pageSize)
        {
            var result = new SearchProductsQRY();
            var productsTask = _pageRepository.GetProductsByCategoryAndSizeAsync(categoryId, pageNbr, pageSize);
            var totalProductsTask = _pageRepository.GetTotalProductsByCategoryAsync(categoryId);
            await Task.WhenAll(productsTask, totalProductsTask);
            result.products = productsTask.Result;
            result.totalProducts = totalProductsTask.Result;
            return result;
        }

        public async Task<SearchProductsQRY> GetProductsByQueryAsync(string query, int pageNbr, int pageSize)
        {
            var result = new SearchProductsQRY();
            var totalProductsTask =  _elasticSearchService.GetCountSearchProductsAsync(query);
            var productsTask =  _elasticSearchService.SearchProductsAsync(query, pageNbr, pageSize);
            await Task.WhenAll(productsTask, productsTask);
            var productsModifyFieldsTask = productsTask.Result.Select(async item =>
            {
                item.Media = await Helpers.GetFileByUrlAsync(item.MediaUrl);
                item.MediaUrl = null;
                item.TotalQuantity = null;
            });
            await Task.WhenAll(productsModifyFieldsTask);
            result.totalProducts = totalProductsTask.Result;
            result.products = productsTask.Result;
            return result;
        }
    }
}
