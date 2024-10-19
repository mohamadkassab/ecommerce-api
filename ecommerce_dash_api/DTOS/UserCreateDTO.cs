using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ecommerce_dash_api.DTOS
{
    public class UserCreateDTO
    {
        [Required, EmailAddress, StringLength(50, MinimumLength = 4, ErrorMessage = "Username must be between 4 and 50 characters.")]
        public string Username { get; set; } = null!;

        [Required, StringLength(50, MinimumLength = 2, ErrorMessage = "First name must be between 2 and 50 characters.")]
        public string FirstName { get; set; } = null!;

        [Required, StringLength(50, MinimumLength = 2, ErrorMessage = "Last name must be between 2 and 50 characters.")]
        public string LastName { get; set; } = null!;

        public DateOnly? Dob { get; set; }

        [Required, Phone(ErrorMessage ="phone number is invalid"), MaxLength(50, ErrorMessage = "Maximum Phone length is 50")]
        public string Phone { get; set; } = null!;

        [MaxLength(255, ErrorMessage = "maximum address length is 50")]
        public string? Address { get; set; }

        [Required, StringLength(255, MinimumLength = 6, ErrorMessage = "Password must be between 6 and 255 characters.")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;

        public List<int> Roles { get; set; } = new List<int>();
    }
}
