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
            services.AddScoped<Areas.Dashboard.Interfaces.IProductService, Areas.Dashboard.Services.ProductService>();
            services.AddScoped<IPageRepository, PageRepository>();
            services.AddScoped<Areas.Shop.Interfaces.IPageService, Areas.Shop.Services.PageService>();
            services.AddSingleton<JwtToken>();
            return services;
        }
    }
}
