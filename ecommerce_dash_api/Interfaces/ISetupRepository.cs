using ecommerce_dash_api.Models;
using ecommerce_dash_api.QRYS;
using Attribute = ecommerce_dash_api.Models.Attribute;

namespace ecommerce_dash_api.Interfaces
{
    public interface ISetupRepository
    {
        //+------------------------------------------------------------------+
        //| Attribute                                            
        //+------------------------------------------------------------------+
        Task<Attribute?> GetAttributeByIdAsync(int id);
        Task<List<AttributeQRY>> GetAllAttributesWithOptionsAsync();
        Task CreateAttributeAsync(Attribute attribute);
        Task CreateAttributeOptionAsync(AttributeOption attributeOption);
        Task UpdateAttributeAsync(Attribute attribute);
        Task DeleteAttributeAsync(Attribute attribute);
        Task DeleteAttributeOptionsByAttributeIdAsync(int attributeId);

        //+------------------------------------------------------------------+
        //| Brand                                            
        //+------------------------------------------------------------------+
        Task<List<BrandQRY>> GetAllBrandsAsync();
        Task CreateBrandAsync(Brand brand);
        Task UpdateBrandAsync(Brand brand);
        Task<Brand?> GetBrandByIdAsync(int id);
        Task DeleteBrandAsync(Brand brand);

        //+------------------------------------------------------------------+
        //| Category                                            
        //+------------------------------------------------------------------+
        Task<List<CategoryQRY>> GetAllCategoriesAsync();
        Task CreateCategoryAsync(Category category);
        Task UpdateCategoryAsync(Category category);
        Task<Category?> GetCategoryByIdAsync(int id);
        Task DeleteCategoryAsync(Category category);

        //+------------------------------------------------------------------+
        //| Country                                            
        //+------------------------------------------------------------------+
        Task<List<CountryQRY>> GetAllCountriesAsync();
        Task CreateCountryAsync(Country country);
        Task UpdateCountryAsync(Country country);
        Task<Country?> GetCountryByIdAsync(int id);
        Task DeleteCountryAsync(Country country);

        //+------------------------------------------------------------------+
        //| Currency                                            
        //+------------------------------------------------------------------+
        Task<List<CurrencyQRY>> GetAllCurrenciesAsync();
        Task CreateCurrencyAsync(Currency currency);
        Task UpdateCurrencyAsync(Currency currency);
        Task<Currency?> GetCurrencyByIdAsync(int id);
        Task DeleteCurrencyAsync(Currency currency);

        //+------------------------------------------------------------------+
        //| Payment method                                            
        //+------------------------------------------------------------------+
        Task<List<PaymentMQRY>> GetAllPaymentMAsync();
        Task<PaymentMethod?> GetPaymentMByIdAsync(int id);
        Task UpdatePaymentMAsync(PaymentMethod paymentM);

        //+------------------------------------------------------------------+
        //| Season                                            
        //+------------------------------------------------------------------+
        Task<List<SeasonQRY>> GetAllSeasonsAsync();
        Task CreateSeasonAsync(Season season);
        Task UpdateSeasonAsync(Season season);
        Task<Season?> GetSeasonByIdAsync(int id);
        Task DeleteSeasonAsync(Season season);

        //+------------------------------------------------------------------+
        //| Section                                            
        //+------------------------------------------------------------------+
        Task<Section?> GetSectionByIdAsync(int id);
        Task<List<SectionQRY>> GetAllSectionsWithCategoriesAsync();
        Task CreateSectionAsync(Section section);
        Task UpdateSectionAsync(Section section);
        Task DeleteSectionAsync(Section section);
        Task DeleteSectionCategoriesBySectionIdAsync(int sectionId);

        //+------------------------------------------------------------------+
        //| Shipping method                                            
        //+------------------------------------------------------------------+
        Task<List<ShippingMQRY>> GetAllShippingMAsync();
        Task<ShippingMethod?> GetShippingMByIdAsync(int id);
        Task UpdateShippingMAsync(ShippingMethod shippingM);

        //+------------------------------------------------------------------+
        //| Supplier                                            
        //+------------------------------------------------------------------+
        Task<List<SupplierQRY>> GetAllSuppliersAsync();
        Task CreateSupplierAsync(Supplier supplier);
        Task UpdateSupplierAsync(Supplier supplier);
        Task<Supplier?> GetSupplierByIdAsync(int id);
        Task DeleteSupplierAsync(Supplier supplier);

        //+------------------------------------------------------------------+
        //| Tag                                            
        //+------------------------------------------------------------------+
        Task<List<TagQRY>> GetAllTagsAsync();
        Task CreateTagAsync(Tag tag);
        Task UpdateTagAsync(Tag tag);
        Task<Tag?> GetTagByIdAsync(int id);
        Task DeleteTagAsync(Tag tag);

        //+------------------------------------------------------------------+
        //| Year                                            
        //+------------------------------------------------------------------+
        Task<List<YearQRY>> GetAllYearsAsync();
        Task CreateYearAsync(Year year);
        Task UpdateYearAsync(Year year);
        Task<Year?> GetYearByIdAsync(int id);
        Task DeleteYearAsync(Year year);
    }
}
