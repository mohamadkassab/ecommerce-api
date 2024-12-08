using ecommerce_dash_api.DTOS;
using System.Reflection;

namespace ecommerce_dash_api.Interfaces
{
    public interface IHelpersFunctions
    {
        byte[]? GetFileByUrl(string? url);
    }
}
