using ecommerce_dash_api.Interfaces;
using ecommerce_dash_api.Repositories;
using ecommerce_dash_api.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

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
            services.AddScoped<IHelpersFunctions, HelpersFuntions>();
            services.AddSingleton<IPasswordHasher, PasswordHasher>();
            services.AddSingleton<JwtToken>();
            return services;
        }
    }
}
