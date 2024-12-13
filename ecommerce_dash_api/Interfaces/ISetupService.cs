using ecommerce_dash_api.DTOS;
using ecommerce_dash_api.Models;
using ecommerce_dash_api.QRYS;

namespace ecommerce_dash_api.Interfaces
{
    public interface ISetupService
    {
        //+------------------------------------------------------------------+
        //| Attribute                                            
        //+------------------------------------------------------------------+
        Task<List<AttributeQRY>> GetAllAttributesWithOptionsAsync();
        Task<bool> CreateAttributeAsync(AttributeCreateDTO attributeDTO, string? username);
        Task<bool> UpdateAttributeAsync(AttributeUpdateDTO attributeDTO, string? username);
        Task<bool> DeleteAttributeAsync(int id);

        //+------------------------------------------------------------------+
        //| Brand                                            
        //+------------------------------------------------------------------+
        Task<List<BrandQRY>> GetAllBrandsAsync();
        Task<bool> CreateBrandAsync(BrandCreateDTO brandDTO, string? username);
        Task<bool> UpdateBrandAsync(BrandUpdateDTO brandDTO, string? username);
        Task<bool> DeleteBrandAsync(int id);

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
        Task<bool> DeleteCountryAsync(int id);

        //+------------------------------------------------------------------+
        //| Currency                                            
        //+------------------------------------------------------------------+
        Task<List<CurrencyQRY>> GetAllCurrenciesAsync();
        Task<bool> CreateCurrencyAsync(CurrencyCreateDTO currencyDTO, string? username);
        Task<bool> UpdateCurrencyAsync(CurrencyUpdateDTO currencyDTO, string? username);
        Task<bool> DeleteCurrencyAsync(int id);

        //+------------------------------------------------------------------+
        //| Payment method                                            
        //+------------------------------------------------------------------+
        Task<List<PaymentMQRY>> GetAllPaymentMAsync();
        Task<bool> UpdatePaymentMAsync(PaymentMUpdateDTO paymentMDTO, string? username);

        //+------------------------------------------------------------------+
        //| Season                                            
        //+------------------------------------------------------------------+
        Task<List<SeasonQRY>> GetAllSeasonsAsync();
        Task<bool> CreateSeasonAsync(SeasonCreateDTO seasonDTO, string? username);
        Task<bool> UpdateSeasonAsync(SeasonUpdateDTO seasonDTO, string? username);
        Task<bool> DeleteSeasonAsync(int id);

        //+------------------------------------------------------------------+
        //| Section                                            
        //+------------------------------------------------------------------+
        Task<List<SectionQRY>> GetAllSectionsWithCategoriesAsync();
        Task<bool> CreateSectionAsync(SectionCreateDTO sectionDTO, string? username);
        Task<bool> UpdateSectionAsync(SectionUpdateDTO sectionDTO, string? username);
        Task<bool> DeleteSectionAsync(int id);

        //+------------------------------------------------------------------+
        //| Shipping method                                            
        //+------------------------------------------------------------------+
        Task<List<ShippingMQRY>> GetAllShippingMAsync();
        Task<bool> UpdateShippingMAsync(ShippingMUpdateDTO shippingMDTO, string? username);

        //+------------------------------------------------------------------+
        //| Supplier                                            
        //+------------------------------------------------------------------+
        Task<List<SupplierQRY>> GetAllSuppliersAsync();
        Task<bool> CreateSupplierAsync(SupplierCreateDTO supplierDTO, string? username);
        Task<bool> UpdateSupplierAsync(SupplierUpdateDTO supplierDTO, string? username);
        Task<bool> DeleteSupplierAsync(int id);

        //+------------------------------------------------------------------+
        //| Tag                                            
        //+------------------------------------------------------------------+
        Task<List<TagQRY>> GetAllTagsAsync();
        Task<bool> CreateTagAsync(TagCreateDTO tagDTO, string? username);
        Task<bool> UpdateTagAsync(TagUpdateDTO tagDTO, string? username);
        Task<bool> DeleteTagAsync(int id);

        //+------------------------------------------------------------------+
        //| Year                                            
        //+------------------------------------------------------------------+
        Task<List<YearQRY>> GetAllYearsAsync();
        Task<bool> CreateYearAsync(YearCreateDTO yearDTO, string? username);
        Task<bool> UpdateYearAsync(YearUpdateDTO yearDTO, string? username);
        Task<bool> DeleteYearAsync(int id);
    }
}
