using System.ComponentModel.DataAnnotations;

namespace ecommerce_dash_api.DTOS
{
    public class SupplierUpdateDTO
    {
        public int Id { get; set; }

        private string _name = null!;
        [Required(AllowEmptyStrings = false), StringLength(255, MinimumLength = 1, ErrorMessage = "Name must be between 1 and 255 characters.")]
        public string Name
        {
            get => _name;
            set => _name = value.Trim().ToUpper();
        }

        private string _phone = null!;
        [Phone(ErrorMessage = "Phone number is invalid"), MaxLength(255, ErrorMessage = "Phone maximum length is 255")]
        public string Phone
        {
            get => _phone;
            set => _phone = value.Trim();
        }

        private string _address = null!;
        [MaxLength(255, ErrorMessage = "Address maximum length is 255")]
        public string Address
        {
            get => _address;
            set => _address = value.Trim();
        }

        private string _city = null!;
        [MaxLength(255, ErrorMessage = "City maximum length is 255")]
        public string City
        {
            get => _city;
            set => _city = value.Trim();
        }

        private string _email = null!;
        [MaxLength(255, ErrorMessage = "Email maximum length is 255")]
        public string Email
        {
            get => _email;
            set => _email = value.Trim();
        }

        private string _website = null!;
        [MaxLength(255, ErrorMessage = "Website maximum length is 255")]
        public string Website
        {
            get => _website;
            set => _website = value.Trim();
        }

        [Required]
        public int CountryId { get; set; }
    }
}
