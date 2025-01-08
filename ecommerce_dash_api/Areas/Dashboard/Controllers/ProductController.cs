using ecommerce_dash_api.Enum;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ecommerce_dash_api.Areas.Dashboard.Interfaces;
using ecommerce_dash_api.Areas.Dashboard.DTOS;

namespace ecommerce_dash_api.Areas.Dashboard.Controllers
{
    [Area("Dashboard")]
    [Route("Dashboard/[controller]/[action]")]
    [ApiController]
    [Authorize(Roles = nameof(UserTypeEnum.DashboardUser))]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly IConfiguration _configuration;
        public readonly IApiService _apiService;
        public ProductController(IProductService productService, IConfiguration configuration, IApiService apiService)
        {
            _productService = productService;
            _configuration = configuration;
            _apiService = apiService;
        }

        //+------------------------------------------------------------------+
        //| Product                                            
        //+------------------------------------------------------------------+
        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            var userClaims = User.Claims;
            var username = User.FindFirstValue("username") ?? null;
            var ipAddress = HttpContext?.Connection?.RemoteIpAddress?.ToString() ?? null;
            var actionName = this.ControllerContext?.RouteData?.Values["action"]?.ToString() ?? null;
            if (userClaims.Any(c => c.Type == "permission" && c.Value == "user_crud") || (username == "root@e.com"))
            {
                try
                {
                    var result = await _productService.GetAllProductsAsync();
                    await _apiService.CreateLogAsync(LogLevelEnum.INFO.ToString(), LogMessageTemplatesEnum.successful.ToString(), null, username, ipAddress, actionName, null);
                    return Ok(result);

                }
                catch (Exception ex)
                {
                    string errorMessage = $"Error: {ex.Message}";
                    string innerMessage = ex.InnerException != null ? $"Inner Exception: {ex?.InnerException?.Message}" : string.Empty;
                    await _apiService.CreateLogAsync(LogLevelEnum.ERROR.ToString(), $"{errorMessage}\n{innerMessage}", ex?.StackTrace, username, ipAddress, actionName, null);
                    return BadRequest(new { message = innerMessage != string.Empty ? ex?.InnerException?.Message : ex?.Message });
                }
            }
            else
            {
                return Forbid();
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] ProductCreateDTO product)
        {
            var userClaims = User.Claims;
            var username = User.FindFirstValue("username") ?? null;
            var ipAddress = HttpContext?.Connection?.RemoteIpAddress?.ToString() ?? null;
            var actionName = this.ControllerContext?.RouteData?.Values["action"]?.ToString() ?? null;
            if (userClaims.Any(c => c.Type == "permission" && c.Value == "brand") || (username == "root@e.com"))
            {
                try
                {
                    if (!ModelState.IsValid)
                    {
                        await _apiService.CreateLogAsync(LogLevelEnum.WARNING.ToString(), LogMessageTemplatesEnum.invalid_model_state.ToString(), null, username, ipAddress, actionName, product);
                        return BadRequest(ModelState);
                    }

                    var response = await _productService.CreateProductAsync(product, username);
                    if (response)
                    {
                        await _apiService.CreateLogAsync(LogLevelEnum.INFO.ToString(), LogMessageTemplatesEnum.successful.ToString(), null, username, ipAddress, actionName, product);
                        return Ok(new { message = LogMessageTemplatesEnum.successful.ToString() });
                    }
                    await _apiService.CreateLogAsync(LogLevelEnum.WARNING.ToString(), LogMessageTemplatesEnum.failed.ToString(), null, username, ipAddress, actionName, product);
                    return BadRequest(new { message = LogMessageTemplatesEnum.failed.ToString() });
                }
                catch (Exception ex)
                {
                    string errorMessage = $"Error: {ex.Message}";
                    string innerMessage = ex.InnerException != null ? $"Inner Exception: {ex?.InnerException?.Message}" : string.Empty;
                    await _apiService.CreateLogAsync(LogLevelEnum.ERROR.ToString(), $"{errorMessage}\n{innerMessage}", ex?.StackTrace, username, ipAddress, actionName, product);
                    return BadRequest(new { message = innerMessage != string.Empty ? ex?.InnerException?.Message : ex?.Message });
                }
            }
            else
            {
                return Forbid();
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProduct([FromBody] ProductUpdateDTO product)
        {
            var userClaims = User.Claims;
            var username = User.FindFirstValue("username") ?? null;
            var ipAddress = HttpContext?.Connection?.RemoteIpAddress?.ToString() ?? null;
            var actionName = this.ControllerContext?.RouteData?.Values["action"]?.ToString() ?? null;
            if (userClaims.Any(c => c.Type == "permission" && c.Value == "brand") || (username == "root@e.com"))
            {
                try
                {
                    if (!ModelState.IsValid)
                    {
                        await _apiService.CreateLogAsync(LogLevelEnum.WARNING.ToString(), LogMessageTemplatesEnum.invalid_model_state.ToString(), null, username, ipAddress, actionName, product);
                        return BadRequest(ModelState);
                    }

                    var response = await _productService.UpdateProductAsync(product, username);
                    if (response)
                    {
                        await _apiService.CreateLogAsync(LogLevelEnum.INFO.ToString(), LogMessageTemplatesEnum.successful.ToString(), null, username, ipAddress, actionName, product);
                        return Ok(new { message = LogMessageTemplatesEnum.successful.ToString() });
                    }
                    await _apiService.CreateLogAsync(LogLevelEnum.WARNING.ToString(), LogMessageTemplatesEnum.failed.ToString(), null, username, ipAddress, actionName, product);
                    return BadRequest(new { message = LogMessageTemplatesEnum.failed.ToString() });
                }
                catch (Exception ex)
                {
                    string errorMessage = $"Error: {ex.Message}";
                    string innerMessage = ex.InnerException != null ? $"Inner Exception: {ex?.InnerException?.Message}" : string.Empty;
                    await _apiService.CreateLogAsync(LogLevelEnum.ERROR.ToString(), $"{errorMessage}\n{innerMessage}", ex?.StackTrace, username, ipAddress, actionName, product);
                    return BadRequest(new { message = innerMessage != string.Empty ? ex?.InnerException?.Message : ex?.Message });
                }
            }
            else
            {
                return Forbid();
            }
        }

        //+------------------------------------------------------------------+
        //| Product Content                        
        //+------------------------------------------------------------------+
        [HttpGet]
        public async Task<IActionResult> GetAllProductContents()
        {
            var userClaims = User.Claims;
            var username = User.FindFirstValue("username") ?? null;
            var ipAddress = HttpContext?.Connection?.RemoteIpAddress?.ToString() ?? null;
            var actionName = this.ControllerContext?.RouteData?.Values["action"]?.ToString() ?? null;
            if (userClaims.Any(c => c.Type == "permission" && c.Value == "user_crud") || (username == "root@e.com"))
            {
                try
                {
                    var result = await _productService.GetAllProductContentsAsync();
                    await _apiService.CreateLogAsync(LogLevelEnum.INFO.ToString(), LogMessageTemplatesEnum.successful.ToString(), null, username, ipAddress, actionName, null);
                    return Ok(result);

                }
                catch (Exception ex)
                {
                    string errorMessage = $"Error: {ex.Message}";
                    string innerMessage = ex.InnerException != null ? $"Inner Exception: {ex?.InnerException?.Message}" : string.Empty;
                    await _apiService.CreateLogAsync(LogLevelEnum.ERROR.ToString(), $"{errorMessage}\n{innerMessage}", ex?.StackTrace, username, ipAddress, actionName, null);
                    return BadRequest(new { message = innerMessage != string.Empty ? ex?.InnerException?.Message : ex?.Message });
                }
            }
            else
            {
                return Forbid();
            }
        }

        [HttpGet("{productId}")]
        public async Task<IActionResult> GetProductMedia(int productId)
        {
            var userClaims = User.Claims;
            var username = User.FindFirstValue("username") ?? null;
            var ipAddress = HttpContext?.Connection?.RemoteIpAddress?.ToString() ?? null;
            var actionName = this.ControllerContext?.RouteData?.Values["action"]?.ToString() ?? null;
            if (userClaims.Any(c => c.Type == "permission" && c.Value == "user_crud") || (username == "root@e.com"))
            {
                try
                {
                    var result = await _productService.GetProductMediaAsync(productId);
                    await _apiService.CreateLogAsync(LogLevelEnum.INFO.ToString(), LogMessageTemplatesEnum.successful.ToString(), null, username, ipAddress, actionName, null);
                    return Ok(result);

                }
                catch (Exception ex)
                {
                    string errorMessage = $"Error: {ex.Message}";
                    string innerMessage = ex.InnerException != null ? $"Inner Exception: {ex?.InnerException?.Message}" : string.Empty;
                    await _apiService.CreateLogAsync(LogLevelEnum.ERROR.ToString(), $"{errorMessage}\n{innerMessage}", ex?.StackTrace, username, ipAddress, actionName, null);
                    return BadRequest(new { message = innerMessage != string.Empty ? ex?.InnerException?.Message : ex?.Message });
                }
            }
            else
            {
                return Forbid();
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateProductContent([FromForm] ProductContentCreateDTO product)
        {
            var userClaims = User.Claims;
            var username = User.FindFirstValue("username") ?? null;
            var ipAddress = HttpContext?.Connection?.RemoteIpAddress?.ToString() ?? null;
            var actionName = this.ControllerContext?.RouteData?.Values["action"]?.ToString() ?? null;
            if (userClaims.Any(c => c.Type == "permission" && c.Value == "brand") || (username == "root@e.com"))
            {
                try
                {
                    if (!ModelState.IsValid)
                    {
                        await _apiService.CreateLogAsync(LogLevelEnum.WARNING.ToString(), LogMessageTemplatesEnum.invalid_model_state.ToString(), null, username, ipAddress, actionName, product);
                        return BadRequest(ModelState);
                    }

                    var response = await _productService.CreateProductContentAsync(product, username);
                    if (response)
                    {
                        await _apiService.CreateLogAsync(LogLevelEnum.INFO.ToString(), LogMessageTemplatesEnum.successful.ToString(), null, username, ipAddress, actionName, product);
                        return Ok(new { message = LogMessageTemplatesEnum.successful.ToString() });
                    }
                    await _apiService.CreateLogAsync(LogLevelEnum.WARNING.ToString(), LogMessageTemplatesEnum.failed.ToString(), null, username, ipAddress, actionName, product);
                    return BadRequest(new { message = LogMessageTemplatesEnum.failed.ToString() });
                }
                catch (Exception ex)
                {
                    string errorMessage = $"Error: {ex.Message}";
                    string innerMessage = ex.InnerException != null ? $"Inner Exception: {ex?.InnerException?.Message}" : string.Empty;
                    await _apiService.CreateLogAsync(LogLevelEnum.ERROR.ToString(), $"{errorMessage}\n{innerMessage}", ex?.StackTrace, username, ipAddress, actionName, product);
                    return BadRequest(new { message = innerMessage != string.Empty ? ex?.InnerException?.Message : ex?.Message });
                }
            }
            else
            {
                return Forbid();
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProductContent([FromForm] ProductContentUpdateDTO product)
        {
            var userClaims = User.Claims;
            var username = User.FindFirstValue("username") ?? null;
            var ipAddress = HttpContext?.Connection?.RemoteIpAddress?.ToString() ?? null;
            var actionName = this.ControllerContext?.RouteData?.Values["action"]?.ToString() ?? null;
            if (userClaims.Any(c => c.Type == "permission" && c.Value == "brand") || (username == "root@e.com"))
            {
                try
                {
                    if (!ModelState.IsValid)
                    {
                        await _apiService.CreateLogAsync(LogLevelEnum.WARNING.ToString(), LogMessageTemplatesEnum.invalid_model_state.ToString(), null, username, ipAddress, actionName, product);
                        return BadRequest(ModelState);
                    }

                    var response = await _productService.UpdateProductContentAsync(product, username);
                    if (response)
                    {
                        await _apiService.CreateLogAsync(LogLevelEnum.INFO.ToString(), LogMessageTemplatesEnum.successful.ToString(), null, username, ipAddress, actionName, product);
                        return Ok(new { message = LogMessageTemplatesEnum.successful.ToString() });
                    }
                    await _apiService.CreateLogAsync(LogLevelEnum.WARNING.ToString(), LogMessageTemplatesEnum.failed.ToString(), null, username, ipAddress, actionName, product);
                    return BadRequest(new { message = LogMessageTemplatesEnum.failed.ToString() });
                }
                catch (Exception ex)
                {
                    string errorMessage = $"Error: {ex.Message}";
                    string innerMessage = ex.InnerException != null ? $"Inner Exception: {ex?.InnerException?.Message}" : string.Empty;
                    await _apiService.CreateLogAsync(LogLevelEnum.ERROR.ToString(), $"{errorMessage}\n{innerMessage}", ex?.StackTrace, username, ipAddress, actionName, product);
                    return BadRequest(new { message = innerMessage != string.Empty ? ex?.InnerException?.Message : ex?.Message });
                }
            }
            else
            {
                return Forbid();
            }
        }

        //+------------------------------------------------------------------+
        //| Transaction                       
        //+------------------------------------------------------------------+
        [HttpGet]
        public async Task<IActionResult> GetAllTransactions()
        {
            var userClaims = User.Claims;
            var username = User.FindFirstValue("username") ?? null;
            var ipAddress = HttpContext?.Connection?.RemoteIpAddress?.ToString() ?? null;
            var actionName = this.ControllerContext?.RouteData?.Values["action"]?.ToString() ?? null;
            if (userClaims.Any(c => c.Type == "permission" && c.Value == "user_crud") || (username == "root@e.com"))
            {
                try
                {
                    var result = await _productService.GetAllTransactionsAsync();
                    await _apiService.CreateLogAsync(LogLevelEnum.INFO.ToString(), LogMessageTemplatesEnum.successful.ToString(), null, username, ipAddress, actionName, null);
                    return Ok(result);

                }
                catch (Exception ex)
                {
                    string errorMessage = $"Error: {ex.Message}";
                    string innerMessage = ex.InnerException != null ? $"Inner Exception: {ex?.InnerException?.Message}" : string.Empty;
                    await _apiService.CreateLogAsync(LogLevelEnum.ERROR.ToString(), $"{errorMessage}\n{innerMessage}", ex?.StackTrace, username, ipAddress, actionName, null);
                    return BadRequest(new { message = innerMessage != string.Empty ? ex?.InnerException?.Message : ex?.Message });
                }
            }
            else
            {
                return Forbid();
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateTransaction([FromBody] TransactionCreateDTO transaction)
        {
            var userClaims = User.Claims;
            var username = User.FindFirstValue("username") ?? null;
            var ipAddress = HttpContext?.Connection?.RemoteIpAddress?.ToString() ?? null;
            var actionName = this.ControllerContext?.RouteData?.Values["action"]?.ToString() ?? null;
            if (userClaims.Any(c => c.Type == "permission" && c.Value == "brand") || (username == "root@e.com"))
            {
                try
                {
                    if (!ModelState.IsValid)
                    {
                        await _apiService.CreateLogAsync(LogLevelEnum.WARNING.ToString(), LogMessageTemplatesEnum.invalid_model_state.ToString(), null, username, ipAddress, actionName, transaction);
                        return BadRequest(ModelState);
                    }

                    var response = await _productService.CreateTransactionAsync(transaction, username);
                    if (response)
                    {
                        await _apiService.CreateLogAsync(LogLevelEnum.INFO.ToString(), LogMessageTemplatesEnum.successful.ToString(), null, username, ipAddress, actionName, transaction);
                        return Ok(new { message = LogMessageTemplatesEnum.successful.ToString() });
                    }
                    await _apiService.CreateLogAsync(LogLevelEnum.WARNING.ToString(), LogMessageTemplatesEnum.failed.ToString(), null, username, ipAddress, actionName, transaction);
                    return BadRequest(new { message = LogMessageTemplatesEnum.failed.ToString() });
                }
                catch (Exception ex)
                {
                    string errorMessage = $"Error: {ex.Message}";
                    string innerMessage = ex.InnerException != null ? $"Inner Exception: {ex?.InnerException?.Message}" : string.Empty;
                    await _apiService.CreateLogAsync(LogLevelEnum.ERROR.ToString(), $"{errorMessage}\n{innerMessage}", ex?.StackTrace, username, ipAddress, actionName, transaction);
                    return BadRequest(new { message = innerMessage != string.Empty ? ex?.InnerException?.Message : ex?.Message });
                }
            }
            else
            {
                return Forbid();
            }
        }
    }
}
