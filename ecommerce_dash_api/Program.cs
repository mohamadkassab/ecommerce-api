using ecommerce_dash_api.Areas.Shop.Interfaces;
using ecommerce_dash_api.Areas.Shop.QRYS;
using ecommerce_dash_api.Models;
using ecommerce_dash_api.Utils;
using Elastic.Clients.Elasticsearch;
using Elastic.Transport;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// START JWT Configuration
var jwtSettings = builder.Configuration.GetSection("JwtSettings");
var secretKey = Encoding.UTF8.GetBytes(jwtSettings["SecretKey"]!);
var dbConnectionString = builder.Configuration.GetSection("ConnectionStrings:DefaultConnection").Value;
var elasticSearchUrl = builder.Configuration.GetSection("ElasticSettings:Url").Value;
var elasticSearchDefaultIndex = builder.Configuration.GetSection("ElasticSettings:DefaultIndex").Value;
var elasticSearchUsername = builder.Configuration.GetSection("ElasticSettings:Username").Value;
var elasticSearchPassword = builder.Configuration.GetSection("ElasticSettings:Password").Value;
var elasticSearchCertificateFingerprint = builder.Configuration.GetSection("ElasticSettings:CertificateFingerprint").Value;

builder.Services.AddDependencyGroup();
builder.Services.AddEndpointsApiExplorer(); 
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(secretKey)
    };
});
// END JWT Configuration

// Add services to the container.
builder.Services.AddDbContext<EcommerceContext>(options =>
       options.UseMySql(dbConnectionString,
                        new MySqlServerVersion(new Version(8, 0, 23))));


// Add services to the container.
builder.Services.AddDbContext<EcommerceContext>(options =>
{
    options.UseMySql(dbConnectionString, new MySqlServerVersion(new Version(8, 0, 23)))
           .LogTo(Console.WriteLine, LogLevel.Information); 
});

// Add elastic search service.
builder.Services.AddSingleton(provider =>
{
    var settings = new ElasticsearchClientSettings(new Uri(elasticSearchUrl))
       .CertificateFingerprint(elasticSearchCertificateFingerprint)
       .Authentication(new BasicAuthentication(elasticSearchUsername, elasticSearchPassword));

    return new ElasticsearchClient(settings);
});


var app = builder.Build();


using (var scope = app.Services.CreateScope())
{
    var elasticService = scope.ServiceProvider.GetRequiredService<IElasticService>();
    try
    {
        await elasticService.UpdateElasticDatabaseAsync();
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error updating Elasticsearch: {ex.Message}");
    }
};

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage(); 
    app.UseSwagger(); 
    app.UseSwaggerUI();
}

app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.Run();

