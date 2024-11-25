using System.ComponentModel.DataAnnotations;

namespace ecommerce_dash_api.DTOS
{
    public class SigninDTO
    {
        private string _username = null!;
        [Required(AllowEmptyStrings = false), EmailAddress, StringLength(255, MinimumLength = 4, ErrorMessage = "Username must be between 4 and 255 characters.")]
        public string Username
        {
            get => _username;
            set => _username = value.ToLower();
        }

        [Required(AllowEmptyStrings = false), StringLength(255, MinimumLength = 6, ErrorMessage = "Password must be between 6 and 255 characters.")]
        public string Password { get; set; } = null!;
    }
}
