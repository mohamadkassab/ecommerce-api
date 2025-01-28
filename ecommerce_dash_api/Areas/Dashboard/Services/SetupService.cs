using ecommerce_dash_api.Areas.Dashboard.DTOS;
using ecommerce_dash_api.Areas.Dashboard.Interfaces;
using ecommerce_dash_api.Areas.Dashboard.QRYS;
using ecommerce_dash_api.Models;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Xml.Linq;
using Attribute = ecommerce_dash_api.Models.Attribute;

namespace ecommerce_dash_api.Areas.Dashboard.Services
{
    public class SetupService : ISetupService
    {
        private readonly EcommerceContext _context;
        private readonly ISetupRepository _setupRepository;

        public SetupService(ISetupRepository setupRepository, EcommerceContext context)
        {
            _setupRepository = setupRepository;
            _context = context;
        }

        //+------------------------------------------------------------------+
        //| Attribute                                            
        //+------------------------------------------------------------------+
        public async Task<List<AttributeQRY>> GetAllAttributesWithOptionsAsync()
        {
            var result = await _setupRepository.GetAllAttributesWithOptionsAsync();
            return result;
        }
        public async Task<List<string>> GetAllAttributesAsync()
        {
            var result = await _setupRepository.GetAllAttributesAsync();
            return result;
        }
        public async Task<bool> CreateAttributeAsync(AttributeCreateDTO attributeDTO, string? username)
        {
            Attribute attribute = new Attribute
            {
                Name = attributeDTO.Name,
                UpdatedBy = username,
            };
            await _setupRepository.CreateAttributeAsync(attribute);
            List<AttributeOption> attributeoptions = new List<AttributeOption>();
            foreach (var option in attributeDTO.Options)
            {
                attributeoptions.Add (new AttributeOption
                {
                    Attribute = attribute,
                    Option = option
                });
            }
            await _setupRepository.CreateAttributeOptionRangeAsync(attributeoptions);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> UpdateAttributeAsync(AttributeUpdateDTO attributeDTO, string? username)
        {
            var deleteTask = _setupRepository.DeleteAttributeOptionsByAttributeIdAsync(attributeDTO.Id);
            var getTask = _setupRepository.GetAttributeByIdAsync(attributeDTO.Id);
            await Task.WhenAll(deleteTask, getTask);
            Attribute attribute = getTask.Result;
            attribute.Name = attributeDTO.Name;
            attribute.UpdatedBy = username;
            await _setupRepository.UpdateAttributeAsync(attribute);
            List<AttributeOption> attributeoptions = new List<AttributeOption>();
            foreach (var option in attributeDTO.Options)
            {
                attributeoptions.Add( new AttributeOption
                {
                    Attribute = attribute,
                    Option = option
                });
            }
            await _setupRepository.CreateAttributeOptionRangeAsync(attributeoptions);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> DeleteAttributeAsync(int id)
        {
            var attribute = await _context.Attributes
            .Where(i => i.Id == id).FirstOrDefaultAsync();
            await _setupRepository.DeleteAttributeAsync(attribute);
            await _context.SaveChangesAsync();
            return true;
        }

        //+------------------------------------------------------------------+
        //| Brand                                            
        //+------------------------------------------------------------------+
        public async Task<List<BrandQRY>> GetAllBrandsAsync()
        {
            var result = await _setupRepository.GetAllBrandsAsync();
            return result;
        }
        public async Task<bool> CreateBrandAsync(BrandCreateDTO brandDTO, string? username)
        {
            var fileExtension = Path.GetExtension(brandDTO.LogoFile.FileName);
            var uniqueFileName = $"{Guid.NewGuid()}{fileExtension}";
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "brands", uniqueFileName);
            using(Stream stream = new FileStream(filePath, FileMode.Create))
            {
                brandDTO.LogoFile.CopyToAsync(stream);
            }

            var country = await _context.Countries.FirstOrDefaultAsync(c => c.Name == brandDTO.Country);
            Brand brand = new Brand
            {
                Name = brandDTO.Name,
                Website = brandDTO.Website,
                LogoUrl = filePath,
                CountryId = country.Id,
                UpdatedBy = username,
            };

            await _setupRepository.CreateBrandAsync(brand);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> UpdateBrandAsync(BrandUpdateDTO brandDTO, string? username)
        {
            using(var _context2 = new EcommerceContext())
            {
                var getBrandTask = _setupRepository.GetBrandByIdAsync(brandDTO.Id);
                var getCountryTask = _context2.Countries.FirstOrDefaultAsync(c => c.Name == brandDTO.Country);
                Task.WhenAll(getBrandTask, getCountryTask);
                Brand brand = getBrandTask.Result;
                Country country = getCountryTask.Result;
                var filePath = brand.LogoUrl;
                if (brandDTO.LogoFile != null)
                {
                    if (!string.IsNullOrEmpty(brand.LogoUrl) && File.Exists(brand.LogoUrl))
                    {
                        File.Delete(brand.LogoUrl);
                    }
                    var fileExtension = Path.GetExtension(brandDTO.LogoFile.FileName);
                    var uniqueFileName = $"{Guid.NewGuid()}{fileExtension}";
                    filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "brands", uniqueFileName);
                    using (Stream stream = new FileStream(filePath, FileMode.Create))
                    {
                        brandDTO.LogoFile.CopyToAsync(stream);
                    }
                }

                brand.Name = brandDTO.Name;
                brand.Website = brandDTO.Website ?? "";
                brand.LogoUrl = filePath;
                brand.CountryId = country.Id;
                brand.UpdatedBy = username;
                await _setupRepository.UpdateBrandAsync(brand);
                await _context.SaveChangesAsync();
                return true;
            }
        }

        //+------------------------------------------------------------------+
        //| Category                                            
        //+------------------------------------------------------------------+
        public async Task<List<CategoryQRY>> GetAllCategoriesAsync()
        {
            var result = await _setupRepository.GetAllCategoriesAsync();
            return result;
        }
        public async Task<bool> CreateCategoryAsync(CategoryCreateDTO categoryDTO, string? username)
        {
            Category category = new Category
            {
                Name = categoryDTO.Name,
                UpdatedBy = username,
            };

            await _setupRepository.CreateCategoryAsync(category);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> UpdateCategoryAsync(CategoryUpdateDTO categoryDTO, string? username)
        {
            Category category = await _setupRepository.GetCategoryByIdAsync(categoryDTO.Id);
            category.Name = categoryDTO.Name;
            category.UpdatedBy = username;
            await _setupRepository.UpdateCategoryAsync(category);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> DeleteCategoryAsync(int id)
        {
            var category = await _context.Categories
            .Where(i => i.Id == id).FirstOrDefaultAsync();
            await _setupRepository.DeleteCategoryAsync(category);
            await _context.SaveChangesAsync();
            return true;
        }

        //+------------------------------------------------------------------+
        //| Country                                            
        //+------------------------------------------------------------------+
        public async Task<List<CountryQRY>> GetAllCountriesAsync()
        {
            var result = await _setupRepository.GetAllCountriesAsync();
            return result;
        }
        public async Task<bool> CreateCountryAsync(CountryCreateDTO countryDTO, string? username)
        {
            Country country = new Country
            {
                Name = countryDTO.Name,
                Code = countryDTO.Code,
                UpdatedBy = username,
            };

            await _setupRepository.CreateCountryAsync(country);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> UpdateCountryAsync(CountryUpdateDTO countryDTO, string? username)
        {
            Country country = await _setupRepository.GetCountryByIdAsync(countryDTO.Id);
            country.Name = countryDTO.Name;
            country.Code = countryDTO.Code;
            country.UpdatedBy = username;
            await _setupRepository.UpdateCountryAsync(country);
            await _context.SaveChangesAsync();
            return true;
        }

        //+------------------------------------------------------------------+
        //| Currency                                            
        //+------------------------------------------------------------------+
        public async Task<List<CurrencyQRY>> GetAllCurrenciesAsync()
        {
            var result = await _setupRepository.GetAllCurrenciesAsync();
            return result;
        }
        public async Task<bool> CreateCurrencyAsync(CurrencyCreateDTO currencyDTO, string? username)
        {
            Country country = await _context.Countries.FirstOrDefaultAsync(c => c.Name == currencyDTO.Country);
            Currency currency = new Currency
            {
                Name = currencyDTO.Name,
                Symbol = currencyDTO.Symbol,
                ExchangeRateUsd = currencyDTO.ExchangeRateUsd,
                CountryId = country.Id,
                IsActive = currencyDTO.IsActive,
                UpdatedBy = username,
            };

            await _setupRepository.CreateCurrencyAsync(currency);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> UpdateCurrencyAsync(CurrencyUpdateDTO currencyDTO, string? username)
        {
            using (var _context2 = new EcommerceContext()) 
            {
                var getCurrencyTask = _setupRepository.GetCurrencyByIdAsync(currencyDTO.Id);
                var getCountryTask = _context2.Countries.FirstOrDefaultAsync(i => i.Name == currencyDTO.Country);
                Task.WhenAll(getCurrencyTask, getCountryTask);
                Currency currency = getCurrencyTask.Result;
                Country country = getCountryTask.Result;
                currency.Name = currencyDTO.Name;
                currency.Symbol = currencyDTO.Symbol;
                currency.ExchangeRateUsd = currencyDTO.ExchangeRateUsd;
                currency.CountryId = country.Id;
                currency.IsActive = currencyDTO.IsActive;
                currency.UpdatedBy = username;
                await _setupRepository.UpdateCurrencyAsync(currency);
                await _context.SaveChangesAsync();
                return true;
            }

        }
        public async Task<bool> DeleteCurrencyAsync(int id)
        {
            var currency = await _context.Currencies
            .Where(i => i.Id == id).FirstOrDefaultAsync();
            await _setupRepository.DeleteCurrencyAsync(currency);
            await _context.SaveChangesAsync();
            return true;
        }

        //+------------------------------------------------------------------+
        //| Season                                            
        //+------------------------------------------------------------------+
        public async Task<List<SeasonQRY>> GetAllSeasonsAsync()
        {
            var result = await _setupRepository.GetAllSeasonsAsync();
            return result;
        }
        public async Task<bool> CreateSeasonAsync(SeasonCreateDTO seasonDTO, string? username)
        {
            Season season = new Season
            {
                Name = seasonDTO.Name,
                UpdatedBy = username,
            };

            await _setupRepository.CreateSeasonAsync(season);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> UpdateSeasonAsync(SeasonUpdateDTO seasonDTO, string? username)
        {
            Season season = await _setupRepository.GetSeasonByIdAsync(seasonDTO.Id);
            season.Name = seasonDTO.Name;
            season.UpdatedBy = username;
            await _setupRepository.UpdateSeasonAsync(season);
            await _context.SaveChangesAsync();
            return true;
        }

        //+------------------------------------------------------------------+
        //| Shipping method                                            
        //+------------------------------------------------------------------+
        public async Task<List<ShippingMQRY>> GetAllShippingMAsync()
        {
            var result = await _setupRepository.GetAllShippingMAsync();
            return result;
        }
        public async Task<bool> CreateShippingMAsync(ShippingMCreateDTO shippingMDTO, string? username)
        {
            var fileExtension = Path.GetExtension(shippingMDTO.IconFile.FileName);
            var uniqueFileName = $"{Guid.NewGuid()}{fileExtension}";
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "shipping-methods", uniqueFileName);
            using (Stream stream = new FileStream(filePath, FileMode.Create))
            {
                shippingMDTO.IconFile.CopyToAsync(stream);
            }

            ShippingMethod shippingM = new ShippingMethod
            {
                Name = shippingMDTO.Name,
                IconUrl = filePath,
                Overseas = shippingMDTO.Overseas,
                IsActive = shippingMDTO.IsActive,
                UpdatedBy = username,
            };

            await _setupRepository.CreateShippingMAsync(shippingM);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> UpdateShippingMAsync(ShippingMUpdateDTO shippingMDTO, string? username)
        {
            ShippingMethod shippingM = await _setupRepository.GetShippingMByIdAsync(shippingMDTO.Id);
            var filePath = shippingM.IconUrl;
            if (shippingMDTO.IconFile != null)
            {
                if (!string.IsNullOrEmpty(shippingM.IconUrl) && File.Exists(shippingM.IconUrl))
                {
                    File.Delete(shippingM.IconUrl); // Deletes the old file
                }
                var fileExtension = Path.GetExtension(shippingMDTO.IconFile.FileName);
                var uniqueFileName = $"{Guid.NewGuid()}{fileExtension}";
                filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "shipping-methods", uniqueFileName);
                using (Stream stream = new FileStream(filePath, FileMode.Create))
                {
                    shippingMDTO.IconFile.CopyToAsync(stream);
                }
            }

            shippingM.Name = shippingMDTO.Name;
            shippingM.IconUrl = filePath;
            shippingM.Overseas = shippingMDTO.Overseas;
            shippingM.IsActive = shippingMDTO.IsActive;
            shippingM.UpdatedBy = username;
            await _setupRepository.UpdateShippingMAsync(shippingM);
            await _context.SaveChangesAsync();
            return true;
        }

        //+------------------------------------------------------------------+
        //| Supplier                                            
        //+------------------------------------------------------------------+
        public async Task<List<SupplierQRY>> GetAllSuppliersAsync()
        {
            var result = await _setupRepository.GetAllSuppliersAsync();
            return result;
        }
        public async Task<bool> CreateSupplierAsync(SupplierCreateDTO supplierDTO, string? username)
        {
            var country = await _context.Countries.FirstOrDefaultAsync(c => c.Name == supplierDTO.Country);
            Supplier supplier = new Supplier
            {
                Name = supplierDTO.Name,
                Phone = supplierDTO.Phone,
                Address = supplierDTO.Address,
                City = supplierDTO.City,
                Email = supplierDTO.Email,
                Website = supplierDTO.Website,
                CountryId = country.Id,
                UpdatedBy = username,
            };

            await _setupRepository.CreateSupplierAsync(supplier);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> UpdateSupplierAsync(SupplierUpdateDTO supplierDTO, string? username)
        {
            using(var _context2 = new EcommerceContext())
            {
                var getSupplierTask = _setupRepository.GetSupplierByIdAsync(supplierDTO.Id);
                var getCountryTask = _context2.Countries.FirstOrDefaultAsync(c => c.Name == supplierDTO.Country);
                Task.WhenAll(getSupplierTask, getCountryTask);
                Supplier supplier = getSupplierTask.Result;
                Country country = getCountryTask.Result;
                supplier.Name = supplierDTO.Name;
                supplier.Phone = supplierDTO.Phone;
                supplier.Address = supplierDTO.Address;
                supplier.City = supplierDTO.City;
                supplier.Email = supplierDTO.Email;
                supplier.Website = supplierDTO.Website;
                supplier.CountryId = country.Id;
                supplier.UpdatedBy = username;
                await _setupRepository.UpdateSupplierAsync(supplier);
                await _context.SaveChangesAsync();
                return true;
            }
        }


    }
}
