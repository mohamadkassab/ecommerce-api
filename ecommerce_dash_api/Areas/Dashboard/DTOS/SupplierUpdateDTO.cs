using System.ComponentModel.DataAnnotations;

namespace ecommerce_dash_api.Areas.Dashboard.DTOS
{
    public class SupplierUpdateDTO
    {
        [Required]
        public int Id { get; set; }

        private string _name = null!;
        [Required(AllowEmptyStrings = false, ErrorMessage = "Name is required and cannot be empty.")]
        [StringLength(255, MinimumLength = 1, ErrorMessage = "Name must be between 1 and 255 characters.")]
        public string Name
        {
            get => _name;
            set => _name = value?.Trim() ?? string.Empty;
        }

        private string _phone = string.Empty;
        [Phone(ErrorMessage = "Phone number is invalid.")]
        [MaxLength(255, ErrorMessage = "Phone maximum length is 255 characters.")]
        public string? Phone
        {
            get => _phone;
            set => _phone = value?.Trim() ?? string.Empty;
        }

        private string _address = string.Empty;
        [MaxLength(255, ErrorMessage = "Address maximum length is 255 characters.")]
        public string? Address
        {
            get => _address;
            set => _address = value?.Trim() ?? string.Empty;
        }

        private string _city = string.Empty;
        [MaxLength(255, ErrorMessage = "City maximum length is 255 characters.")]
        public string? City
        {
            get => _city;
            set => _city = value?.Trim() ?? string.Empty;
        }

        private string _email = string.Empty;
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        [MaxLength(255, ErrorMessage = "Email maximum length is 255 characters.")]
        public string Email
        {
            get => _email;
            set => _email = value?.Trim() ?? string.Empty;
        }

        private string _website = string.Empty;
        [MaxLength(255, ErrorMessage = "Website maximum length is 255 characters.")]
        public string Website
        {
            get => _website;
            set => _website = value?.Trim() ?? string.Empty;
        }

        [Required]
        public string Country { get; set; } = null!;
    }
}
