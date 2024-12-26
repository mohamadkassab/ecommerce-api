using System.ComponentModel.DataAnnotations;

namespace ecommerce_dash_api.DTOS
{
    public class UserUpdateDTO
    {
        public int Id { get; set; }

        private string _username = null!;
        [Required(AllowEmptyStrings = false), EmailAddress, StringLength(255, MinimumLength = 4, ErrorMessage = "Username must be between 4 and 255 characters.")]
        public string Username
        {
            get => _username;
            set => _username = value.Trim().ToLower();
        }

        private string _firstname = null!;
        [Required(AllowEmptyStrings = false), StringLength(255, MinimumLength = 2, ErrorMessage = "First name must be between 2 and 255 characters.")]
        public string FirstName
        {
            get => _firstname;
            set => _firstname = value.Trim().ToLower();
        }

        private string _lastname = null!;
        [Required(AllowEmptyStrings = false), StringLength(255, MinimumLength = 2, ErrorMessage = "Last name must be between 2 and 255 characters.")]
        public string LastName
        {
            get => _lastname;
            set => _lastname = value.Trim().ToLower();
        }

        public DateOnly Dob { get; set; }

        [Required(AllowEmptyStrings = false), Phone(ErrorMessage = "phone number is invalid"), MaxLength(50, ErrorMessage = "Phone maximum Phone length is 255")]
        public string Phone { get; set; } = null!;

        [MaxLength(255, ErrorMessage = "Address maximum address length is 255")]
        public string? Address { get; set; }

        [StringLength(255)]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        public List<int> Roles { get; set; } = new List<int>();

        [Required]
        public Boolean IsActive { get; set; }
    }
}
