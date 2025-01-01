using ecommerce_dash_api.Interfaces;
using ecommerce_dash_api.Models;
using ecommerce_dash_api.QRYS;
using ecommerce_dash_api.Utils;
using Microsoft.EntityFrameworkCore;
using System.Data;
using static Org.BouncyCastle.Asn1.Cmp.Challenge;
using Attribute = ecommerce_dash_api.Models.Attribute;
using Section = ecommerce_dash_api.Models.Section;

namespace ecommerce_dash_api.Repositories
{
    public class SetupRepository : ISetupRepository
    {
        private readonly EcommerceContext _context;
        private readonly IHelpersFunctions _helpersFunctions;
        public SetupRepository(EcommerceContext context, IHelpersFunctions helpersFunctions)
        {
            _context = context;
            _helpersFunctions = helpersFunctions;
        }

        //+------------------------------------------------------------------+
        //| Attribute                                            
        //+------------------------------------------------------------------+
        public async Task<Models.Attribute?> GetAttributeByIdAsync(int id)
        {
            return await _context.Attributes.FirstOrDefaultAsync(i => i.Id == id);
        }
        public async Task<List<AttributeQRY>> GetAllAttributesWithOptionsAsync()
        {
            var result = await _context.Attributes
            .AsNoTracking()
            .Select(i => new AttributeQRY
            {
                Id = i.Id,
                Name = i.Name,
                UpdatedAt = i.UpdatedAt,
                UpdatedBy = i.UpdatedBy,
                Options = i.AttributeOptions.Select(ao => ao.Option).ToList()
            })
            .ToListAsync();

            return result;
        }
        public async Task CreateAttributeAsync(Attribute attribute)
        {
            await _context.Attributes.AddAsync(attribute);
        }
        public async Task CreateAttributeOptionAsync(AttributeOption attributeOption)
        {
            await _context.AttributeOptions.AddAsync(attributeOption);
        }
        public async Task CreateAttributeOptionRangeAsync(IEnumerable<AttributeOption> attributeOptions)
        {
            await _context.AttributeOptions.AddRangeAsync(attributeOptions);
        }
        public Task UpdateAttributeAsync(Attribute attribute)
        {
            _context.Attributes.Update(attribute);
            return Task.CompletedTask;
        }
        public Task DeleteAttributeAsync(Attribute attribute)
        {
            _context.Attributes.Remove(attribute);
            return Task.CompletedTask;
        }
        public Task DeleteAttributeOptionsByAttributeIdAsync(int attributeId)
        {
            var records = _context.AttributeOptions.Where(i => i.AttributeId == attributeId).ToList();
            if (records.Any())
            {
                _context.AttributeOptions.RemoveRange(records);
            }
            return Task.CompletedTask;
        }

        //+------------------------------------------------------------------+
        //| Brand                                            
        //+------------------------------------------------------------------+
        public async Task<Brand?> GetBrandByIdAsync(int id)
        {
            return await _context.Brands.FirstOrDefaultAsync(i => i.Id == id);
        }
        public async Task<Brand?> GetBrandByNameAsync(string name)
        {
            return await _context.Brands.FirstOrDefaultAsync(i => i.Name == name);
        }
        public async Task<List<BrandQRY>> GetAllBrandsAsync()
        {
            var result = await _context.Brands
            .AsNoTracking()
              .Select(i => new BrandQRY
              {
                  Id = i.Id,
                  Name = i.Name,
                  Website = i.Website,
                  LogoFile = _helpersFunctions.GetFileByUrl(i.LogoUrl),
                  UpdatedAt = i.UpdatedAt,
                  UpdatedBy = i.UpdatedBy,
                  Country = i.Country.Name 
              })
            .ToListAsync();

            return result;
        }
        public async Task CreateBrandAsync(Brand brand)
        {
            await _context.Brands.AddAsync(brand);
        }
        public Task UpdateBrandAsync(Brand brand)
        {
            _context.Brands.Update(brand);
            return Task.CompletedTask;
        }

        //+------------------------------------------------------------------+
        //| Category                                            
        //+------------------------------------------------------------------+
        public async Task<Category?> GetCategoryByIdAsync(int id)
        {
            return await _context.Categories.FirstOrDefaultAsync(i => i.Id == id);
        }
        public async Task<List<CategoryQRY>> GetAllCategoriesAsync()
        {
            var result = await _context.Categories
            .AsNoTracking()
            .Select(i => new CategoryQRY
            {
                Id = i.Id,
                Name = i.Name,
                UpdatedAt = i.UpdatedAt,
                UpdatedBy = i.UpdatedBy,
            })
            .ToListAsync();

            return result;
        }
        public async Task CreateCategoryAsync(Category category)
        {
            await _context.Categories.AddAsync(category);
        }
        public Task UpdateCategoryAsync(Category category)
        {
            _context.Categories.Update(category);
            return Task.CompletedTask;
        }
        public Task DeleteCategoryAsync(Category category)
        {
            _context.Categories.Remove(category);
            return Task.CompletedTask;
        }

        //+------------------------------------------------------------------+
        //| Country                                            
        //+------------------------------------------------------------------+
        public async Task<Country?> GetCountryByIdAsync(int id)
        {
            return await _context.Countries.FirstOrDefaultAsync(i => i.Id == id);
        }
        public async Task<Country?> GetCountryByNameAsync(string name)
        {
            return await _context.Countries.FirstOrDefaultAsync(i => i.Name == name);
        }
        public async Task<List<CountryQRY>> GetAllCountriesAsync()
        {
            var result = await _context.Countries
                .AsNoTracking()
              .Select(i => new CountryQRY
              {
                  Id = i.Id,
                  Name = i.Name,
                  Code = i.Code,
                  UpdatedAt = i.UpdatedAt,
                  UpdatedBy = i.UpdatedBy,
              })
              .ToListAsync();

            return result;
        }
        public async Task CreateCountryAsync(Country country)
        {
            await _context.Countries.AddAsync(country);
        }
        public Task UpdateCountryAsync(Country country)
        {
            _context.Countries.Update(country);
            return Task.CompletedTask;
        }

        //+------------------------------------------------------------------+
        //| Currency                                            
        //+------------------------------------------------------------------+
        public async Task<Currency?> GetCurrencyByIdAsync(int id)
        {
            return await _context.Currencies.FirstOrDefaultAsync(i => i.Id == id);
        }
        public async Task<List<CurrencyQRY>> GetAllCurrenciesAsync()
        {
            var result = await _context.Currencies
            .AsNoTracking()
            .Select(i => new CurrencyQRY
            {
                Id = i.Id,
                Name = i.Name,
                Symbol = i.Symbol,
                ExchangeRateUsd = i.ExchangeRateUsd,
                Country = i.Country.Name,
                IsActive = i.IsActive,
                UpdatedAt = i.UpdatedAt,
                UpdatedBy = i.UpdatedBy,
            })
            .ToListAsync();

            return result;
        }
        public async Task CreateCurrencyAsync(Currency currency)
        {
            await _context.Currencies.AddAsync(currency);
        }
        public Task UpdateCurrencyAsync(Currency currency)
        {
            _context.Currencies.Update(currency);
            return Task.CompletedTask;
        }
        public Task DeleteCurrencyAsync(Currency currency)
        {
            _context.Currencies.Remove(currency);
            return Task.CompletedTask;
        }

        //+------------------------------------------------------------------+
        //| Season                                            
        //+------------------------------------------------------------------+
        public async Task<Season?> GetSeasonByIdAsync(int id)
        {
            return await _context.Seasons.FirstOrDefaultAsync(i => i.Id == id);
        }
        public async Task<Season?> GetSeasonByNameAsync(string name)
        {
            return await _context.Seasons.FirstOrDefaultAsync(i => i.Name == name);
        }
        public async Task<List<SeasonQRY>> GetAllSeasonsAsync()
        {
            var result = await _context.Seasons
            .AsNoTracking()
            .Select(i => new SeasonQRY
            {
                Id = i.Id,
                Name = i.Name,
                UpdatedAt = i.UpdatedAt,
                UpdatedBy = i.UpdatedBy,
            })
            .ToListAsync();

            return result;
        }
        public async Task CreateSeasonAsync(Season season)
        {
            await _context.Seasons.AddAsync(season);
        }
        public Task UpdateSeasonAsync(Season season)
        {
            _context.Seasons.Update(season);
            return Task.CompletedTask;
        }

        //+------------------------------------------------------------------+
        //| Section                                            
        //+------------------------------------------------------------------+
        public async Task<Section?> GetSectionByIdAsync(int id)
        {
            return await _context.Sections.FirstOrDefaultAsync(i => i.Id == id);
        }
        public async Task<List<SectionQRY>> GetAllSectionsWithCategoriesAsync()
        {
            var result = await _context.Sections
            .AsNoTracking()
            .Include(i => i.SectionCategories)  
                .ThenInclude(sc => sc.Category)  
            .Select(i => new SectionQRY
            {
                Id = i.Id,
                Name = i.Name,
                UpdatedAt = i.UpdatedAt,
                UpdatedBy = i.UpdatedBy,
                Categories = i.SectionCategories.Select(sc => new CategoryQRY
                {
                    Id = sc.Category.Id,
                    Name = sc.Category.Name
                }).ToList()
            })
            .ToListAsync();

            return result;
        }
        public async Task CreateSectionAsync(Section section)
        {
            await _context.Sections.AddAsync(section);
        }
        public Task UpdateSectionAsync(Section section)
        {
            _context.Sections.Update(section);
            return Task.CompletedTask;
        }
        public Task DeleteSectionAsync(Section section)
        {
            _context.Sections.Remove(section);
            return Task.CompletedTask;
        }
        public Task DeleteSectionCategoriesBySectionIdAsync(int sectionId)
        {
            var records = _context.SectionCategories.Where(i => i.SectionId == sectionId).ToList();
            if (records.Any())
            {
                _context.SectionCategories.RemoveRange(records);
            }
            return Task.CompletedTask;
        }

        //+------------------------------------------------------------------+
        //| Shipping method                                            
        //+------------------------------------------------------------------+
        public async Task<List<ShippingMQRY>> GetAllShippingMAsync()
        {
            var result = await _context.ShippingMethods
            .AsNoTracking()
              .Select(i => new ShippingMQRY
              {
                  Id = i.Id,
                  Name = i.Name,
                  IconFile = _helpersFunctions.GetFileByUrl(i.IconUrl),
                  Overseas = i.Overseas,
                  UpdatedAt = i.UpdatedAt,
                  UpdatedBy = i.UpdatedBy,
                  IsActive = i.IsActive
              })
            .ToListAsync();

            return result;
        }
        public async Task<ShippingMethod?> GetShippingMByIdAsync(int id)
        {
            return await _context.ShippingMethods.FirstOrDefaultAsync(i => i.Id == id);
        }
        public Task CreateShippingMAsync(ShippingMethod shippingM)
        {
            _context.ShippingMethods.Add(shippingM);
            return Task.CompletedTask;
        }
        public Task UpdateShippingMAsync(ShippingMethod shippingM)
        {
            _context.ShippingMethods.Update(shippingM);
            return Task.CompletedTask;
        }

        //+------------------------------------------------------------------+
        //| Supplier                                            
        //+------------------------------------------------------------------+
        public async Task<Supplier?> GetSupplierByIdAsync(int id)
        {
            return await _context.Suppliers.FirstOrDefaultAsync(i => i.Id == id);
        }
        public async Task<Supplier?> GetSupplierByNameAsync(string name)
        {
            return await _context.Suppliers.FirstOrDefaultAsync(i => i.Name == name);
        }
        public async Task<List<SupplierQRY>> GetAllSuppliersAsync()
        {
            var result = await _context.Suppliers
            .AsNoTracking()
            .Select(i => new SupplierQRY
            {
                Id = i.Id,
                Name = i.Name,
                Phone = i.Phone,
                Address = i.Address,
                City = i.City,
                Email = i.Email,
                Website = i.Website,
                Country = i.Country.Name,
                UpdatedAt = i.UpdatedAt,
                UpdatedBy = i.UpdatedBy,
            })
            .ToListAsync();

            return result;
        }
        public async Task CreateSupplierAsync(Supplier supplier)
        {
            await _context.Suppliers.AddAsync(supplier);
        }
        public Task UpdateSupplierAsync(Supplier supplier)
        {
            _context.Suppliers.Update(supplier);
            return Task.CompletedTask;
        }

        //+------------------------------------------------------------------+
        //| Tag                                            
        //+------------------------------------------------------------------+
        public async Task<Tag?> GetTagByIdAsync(int id)
        {
            return await _context.Tags.FirstOrDefaultAsync(i => i.Id == id);
        }
        public async Task<List<TagQRY>> GetAllTagsAsync()
        {
            var result = await _context.Tags
            .AsNoTracking()
             .Select(i => new TagQRY
             {
                 Id = i.Id,
                 Name = i.Name,
                 UpdatedAt = i.UpdatedAt,
                 UpdatedBy = i.UpdatedBy,
             })
             .ToListAsync();

            return result;
        }
        public async Task CreateTagAsync(Tag tag)
        {
            await _context.Tags.AddAsync(tag);
        }
        public Task UpdateTagAsync(Tag tag)
        {
            _context.Tags.Update(tag);
            return Task.CompletedTask;
        }
        public Task DeleteTagAsync(Tag tag)
        {
            _context.Tags.Remove(tag);
            return Task.CompletedTask;
        }


    }
}
