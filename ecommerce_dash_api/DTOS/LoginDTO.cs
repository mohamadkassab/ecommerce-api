using System.ComponentModel.DataAnnotations;

namespace ecommerce_dash_api.DTOS
{
    public class LoginDTO
    {
        [Required, EmailAddress, StringLength(50, MinimumLength = 4, ErrorMessage = "Username must be between 4 and 50 characters.")]
        public string Username { get; set; } = null!;

        [Required, StringLength(255, MinimumLength = 6, ErrorMessage = "Password must be between 6 and 255 characters.")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;
    }
}
