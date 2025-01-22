using ecommerce_dash_api.Areas.Dashboard.DTOS;
using ecommerce_dash_api.Areas.Dashboard.QRYS;

namespace ecommerce_dash_api.Areas.Dashboard.Interfaces
{
    public interface ISetupService
    {
        //+------------------------------------------------------------------+
        //| Attribute                                            
        //+------------------------------------------------------------------+
        Task<List<AttributeQRY>> GetAllAttributesWithOptionsAsync();
        Task<List<string>> GetAllAttributesAsync();
        Task<bool> CreateAttributeAsync(AttributeCreateDTO attributeDTO, string? username);
        Task<bool> UpdateAttributeAsync(AttributeUpdateDTO attributeDTO, string? username);
        Task<bool> DeleteAttributeAsync(int id);

        //+------------------------------------------------------------------+
        //| Brand                                            
        //+------------------------------------------------------------------+
        Task<List<BrandQRY>> GetAllBrandsAsync();
        Task<bool> CreateBrandAsync(BrandCreateDTO brandDTO, string? username);
        Task<bool> UpdateBrandAsync(BrandUpdateDTO brandDTO, string? username);

        //+------------------------------------------------------------------+
        //| Category                                            
        //+------------------------------------------------------------------+
        Task<List<CategoryQRY>> GetAllCategoriesAsync();
        Task<bool> CreateCategoryAsync(CategoryCreateDTO categoryDTO, string? username);
        Task<bool> UpdateCategoryAsync(CategoryUpdateDTO categoryDTO, string? username);
        Task<bool> DeleteCategoryAsync(int id);

        //+------------------------------------------------------------------+
        //| Country                                            
        //+------------------------------------------------------------------+
        Task<List<CountryQRY>> GetAllCountriesAsync();
        Task<bool> CreateCountryAsync(CountryCreateDTO countryDTO, string? username);
        Task<bool> UpdateCountryAsync(CountryUpdateDTO countryDTO, string? username);

        //+------------------------------------------------------------------+
        //| Currency                                            
        //+------------------------------------------------------------------+
        Task<List<CurrencyQRY>> GetAllCurrenciesAsync();
        Task<bool> CreateCurrencyAsync(CurrencyCreateDTO currencyDTO, string? username);
        Task<bool> UpdateCurrencyAsync(CurrencyUpdateDTO currencyDTO, string? username);
        Task<bool> DeleteCurrencyAsync(int id);

        //+------------------------------------------------------------------+
        //| Season                                            
        //+------------------------------------------------------------------+
        Task<List<SeasonQRY>> GetAllSeasonsAsync();
        Task<bool> CreateSeasonAsync(SeasonCreateDTO seasonDTO, string? username);
        Task<bool> UpdateSeasonAsync(SeasonUpdateDTO seasonDTO, string? username);

        //+------------------------------------------------------------------+
        //| Shipping method                                            
        //+------------------------------------------------------------------+
        Task<List<ShippingMQRY>> GetAllShippingMAsync();
        Task<bool> CreateShippingMAsync(ShippingMCreateDTO shippingMDTO, string? username);
        Task<bool> UpdateShippingMAsync(ShippingMUpdateDTO shippingMDTO, string? username);

        //+------------------------------------------------------------------+
        //| Supplier                                            
        //+------------------------------------------------------------------+
        Task<List<SupplierQRY>> GetAllSuppliersAsync();
        Task<bool> CreateSupplierAsync(SupplierCreateDTO supplierDTO, string? username);
        Task<bool> UpdateSupplierAsync(SupplierUpdateDTO supplierDTO, string? username);
    }
}
