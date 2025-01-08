using ecommerce_dash_api.Models;
using System.Security.Cryptography;
using System.Text;

namespace ecommerce_dash_api.Utils
{
    public static class Helpers
    {
        public static async Task<byte[]?> GetFileByUrlAsync(string? url)
        {
            if (string.IsNullOrEmpty(url))
            {
                return null;
            }

            try
            {
                return await File.ReadAllBytesAsync(url);
            }
            catch (Exception)
            {
                return null;
            }
        }
        public static string CapitalizeFirstLetter(string value)
        {
            if (string.IsNullOrEmpty(value))
                return value;

            return char.ToUpper(value[0]) + value.Substring(1).ToLower();
        }

        public static Task<string> HashPasswordAsync(string password)
        {
            return Task.Run(() => BCrypt.Net.BCrypt.HashPassword(password));
        }

        public static Task<bool> VerifyPasswordAsync(string hashedPassword, string password)
        {
            return Task.Run(() => BCrypt.Net.BCrypt.Verify(password, hashedPassword));
        }

        public static async Task<string> GenerateProductAttributeKeyAsync(params object[] elements)
        {
            return await Task.Run(() =>
            {
                StringBuilder combinedString = new StringBuilder();

                foreach (var element in elements)
                {
                    if (element is IEnumerable<TransactionAttribute> attributes)  
                    {
                        foreach (var attribute in attributes)
                        {
                            combinedString.Append("-")
                                           .Append(attribute.Attribute)
                                           .Append(":")
                                           .Append(attribute.AttributeOption);
                        }
                    }
                    else if (element is IEnumerable<string> stringList)  
                    {
                        foreach (var str in stringList)
                        {
                            combinedString.Append("-").Append(str);
                        }
                    }
                    else if (element is IEnumerable<int> intList)  
                    {
                        foreach (var number in intList)
                        {
                            combinedString.Append("-").Append(number.ToString());
                        }
                    }
                    else if (element is IEnumerable<bool> boolList)  
                    {
                        foreach (var boolean in boolList)
                        {
                            combinedString.Append("-").Append(boolean.ToString());
                        }
                    }
                    else if (element is string || element is int || element is bool)  
                    {
                        combinedString.Append("-").Append(element.ToString());
                    }
                }

                using (SHA256 sha256 = SHA256.Create())
                {
                    byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(combinedString.ToString()));

                    StringBuilder hashString = new StringBuilder();
                    foreach (byte b in hashBytes)
                    {
                        hashString.Append(b.ToString("x2"));
                    }

                    return hashString.ToString();
                }
            });
        }
    }
}
