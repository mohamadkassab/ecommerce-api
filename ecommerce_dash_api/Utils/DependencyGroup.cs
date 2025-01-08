using ecommerce_dash_api.Areas.Dashboard.Interfaces;
using ecommerce_dash_api.Areas.Dashboard.Repositories;
using ecommerce_dash_api.Areas.Dashboard.Services;
using ecommerce_dash_api.Areas.Shop.Interfaces;
using ecommerce_dash_api.Areas.Shop.Repositories;
using ecommerce_dash_api.Areas.Shop.Services;

namespace ecommerce_dash_api.Utils
{
    public static class DependencyGroup
    {
        public static IServiceCollection AddDependencyGroup(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IKpiRepository, KpiRepository>();
            services.AddScoped<IKpiService, KpiService>();
            services.AddScoped<IApiRepository, ApiRepository>();
            services.AddScoped<IApiService, ApiService>();
            services.AddScoped<ISetupRepository, SetupRepository>();
            services.AddScoped<ISetupService, SetupService>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IShopProductRepository, ShopProductRepository>();
            services.AddScoped<IPageService, PageService>();
            services.AddSingleton<JwtToken>();
            return services;
        }
    }
}
