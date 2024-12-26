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
        Task CreateAttributeOptionRangeAsync(IEnumerable<AttributeOption> attributeOptions);
        Task UpdateAttributeAsync(Attribute attribute);
        Task DeleteAttributeAsync(Attribute attribute);
        Task DeleteAttributeOptionsByAttributeIdAsync(int attributeId);

        //+------------------------------------------------------------------+
        //| Brand                                            
        //+------------------------------------------------------------------+
        Task<Brand?> GetBrandByIdAsync(int id);
        Task<Brand?> GetBrandByNameAsync(string name);
        Task<List<BrandQRY>> GetAllBrandsAsync();
        Task CreateBrandAsync(Brand brand);
        Task UpdateBrandAsync(Brand brand);
        Task DeleteBrandAsync(Brand brand);

        //+------------------------------------------------------------------+
        //| Category                                            
        //+------------------------------------------------------------------+
        Task<Category?> GetCategoryByIdAsync(int id);
        Task<List<CategoryQRY>> GetAllCategoriesAsync();
        Task CreateCategoryAsync(Category category);
        Task UpdateCategoryAsync(Category category);
        Task DeleteCategoryAsync(Category category);

        //+------------------------------------------------------------------+
        //| Country                                            
        //+------------------------------------------------------------------+
        Task<Country?> GetCountryByIdAsync(int id);
        Task<Country?> GetCountryByNameAsync(string name);
        Task<List<CountryQRY>> GetAllCountriesAsync();
        Task CreateCountryAsync(Country country);
        Task UpdateCountryAsync(Country country);

        //+------------------------------------------------------------------+
        //| Currency                                            
        //+------------------------------------------------------------------+
        Task<Currency?> GetCurrencyByIdAsync(int id);
        Task<List<CurrencyQRY>> GetAllCurrenciesAsync();
        Task CreateCurrencyAsync(Currency currency);
        Task UpdateCurrencyAsync(Currency currency);
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
        Task<Season?> GetSeasonByIdAsync(int id);
        Task<Season?> GetSeasonByNameAsync(string name);
        Task<List<SeasonQRY>> GetAllSeasonsAsync();
        Task CreateSeasonAsync(Season season);
        Task UpdateSeasonAsync(Season season);
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
        Task CreateShippingMAsync(ShippingMethod shippingM);
        Task UpdateShippingMAsync(ShippingMethod shippingM);

        //+------------------------------------------------------------------+
        //| Supplier                                            
        //+------------------------------------------------------------------+
        Task<Supplier?> GetSupplierByIdAsync(int id);
        Task<Supplier?> GetSupplierByNameAsync(string name);
        Task<List<SupplierQRY>> GetAllSuppliersAsync();
        Task CreateSupplierAsync(Supplier supplier);
        Task UpdateSupplierAsync(Supplier supplier);
        Task DeleteSupplierAsync(Supplier supplier);

        //+------------------------------------------------------------------+
        //| Tag                                            
        //+------------------------------------------------------------------+
        Task<List<TagQRY>> GetAllTagsAsync();
        Task CreateTagAsync(Tag tag);
        Task UpdateTagAsync(Tag tag);
        Task<Tag?> GetTagByIdAsync(int id);
        Task DeleteTagAsync(Tag tag);
    }
}
