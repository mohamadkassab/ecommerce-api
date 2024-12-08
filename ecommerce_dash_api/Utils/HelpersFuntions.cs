using ecommerce_dash_api.Interfaces;
using System.Reflection;

namespace ecommerce_dash_api.Utils
{
    public class HelpersFuntions: IHelpersFunctions
    {
        public byte[]? GetFileByUrl(string? url)
        {
            if (string.IsNullOrEmpty(url))
            {
                return null;
            }

            try
            {
                return File.ReadAllBytes(url);
            }
            catch (Exception)
            {
                return null;
            }
        }     
    }
}
