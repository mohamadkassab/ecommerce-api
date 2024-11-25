using System.ComponentModel.DataAnnotations;

namespace ecommerce_dash_api.DTOS
{
    public class SupplierUpdateDTO
    {
        public int Id { get; set; }

        [Required(AllowEmptyStrings = false), StringLength(255, MinimumLength = 1, ErrorMessage = "Name must be between 1 and 255 characters.")]
        public string Name { get; set; }

        [Phone(ErrorMessage = "Phone number is invalid"), MaxLength(255, ErrorMessage = "Phone maximum length is 255")]
        public string Phone { get; set; }

        [MaxLength(255, ErrorMessage = "Address maximum length is 255")]
        public string Address { get; set; }

        [MaxLength(255, ErrorMessage = "City maximum length is 255")]
        public string City { get; set; }

        [MaxLength(255, ErrorMessage = "Email maximum length is 255")]
        public string Email { get; set; }

        [MaxLength(255, ErrorMessage = "Website maximum length is 255")]
        public string Website { get; set; }

        [Required]
        public int CountryId { get; set; }
    }
}
