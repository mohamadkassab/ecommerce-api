using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using ecommerce_dash_api.Areas.Shop.DTOS;
using ecommerce_dash_api.Areas.Shop.Interfaces;
using ecommerce_dash_api.Areas.Shop.QRYS;
using ecommerce_dash_api.Enum;
using ecommerce_dash_api.Utils;
using Google.Protobuf.WellKnownTypes;
using Newtonsoft.Json.Linq;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace ecommerce_dash_api.Areas.Shop.Services
{
    public class PageService : IPageService
    {
        private readonly IPageRepository _pageRepository;
        private readonly IElasticSearchService _elasticSearchService;
        public PageService(IPageRepository pageRepository, IElasticSearchService elasticSearchService) 
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
        public async Task<SearchProductsQRY> GetProductsByQueryAsync(SearchQueryDTO searchQuery)
        {
            var result = new SearchProductsQRY();
            var filterSortingValuesTask = _elasticSearchService.GetUniqueValuesAsync(searchQuery);    
            var totalProductsTask =  _elasticSearchService.GetCountSearchProductsAsync(searchQuery);
            var productsTask =  _elasticSearchService.SearchProductsAsync(searchQuery);
            await Task.WhenAll(productsTask, productsTask, filterSortingValuesTask);
            var productsModifyFieldsTask = productsTask.Result.Select(async item =>
            {
                item.Media = await Helpers.GetFileByUrlAsync(item.MediaUrl);
                item.MediaUrl = null;
                item.TotalQuantity = null;
                item.Note = null;
            });
            await Task.WhenAll(productsModifyFieldsTask);
            result.TotalProducts = totalProductsTask.Result;
            result.Products = productsTask.Result;

            var sortingOptions = await Helpers.GetEnumValuesAndDescriptions<SortingOptionsEnum>();
            var filterSortingOptions = filterSortingValuesTask.Result;
            filterSortingOptions.SortingOptions = sortingOptions;
            result.FilterSortOptions = filterSortingOptions;
            result.SelectedFilterSortOptions = new SelectedFilterSortQRY();
            result.SelectedFilterSortOptions.SortingOption = searchQuery.SortingOption;
            result.SelectedFilterSortOptions.Brands = searchQuery.Brands;
            result.SelectedFilterSortOptions.Categories = searchQuery.Categories;
            return result;
        }
    }
}
