using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ecommerce_dash_api.DTOS
{
    public class UserCreateDTO
    {
        private string _username = null!;
        [Required(AllowEmptyStrings = false), EmailAddress, StringLength(255, MinimumLength = 4, ErrorMessage = "Username must be between 4 and 255 characters.")]
        public string UserName
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

        [Required(AllowEmptyStrings = false), Phone(ErrorMessage ="phone number is invalid"), MaxLength(255, ErrorMessage = "Maximum length is 255")]
        public string Phone { get; set; } = null!;

        [MaxLength(255, ErrorMessage = "maximum length is 255")]
        public string Address { get; set; } = string.Empty;

        [Required(AllowEmptyStrings = false), StringLength(255, MinimumLength = 6, ErrorMessage = "Password must be between 6 and 255 characters.")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;

        public List<int> Roles { get; set; } = new List<int>();

        [Required]
        public Boolean IsActive { get; set; }
    }
}
