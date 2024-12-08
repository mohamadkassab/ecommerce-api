using ecommerce_dash_api.Attributes;
using System.ComponentModel.DataAnnotations;

namespace ecommerce_dash_api.DTOS
{
    public class RoleUpdateDTO
    {
        public int Id { get; set; }

        private string _name = null!;
        [Required(AllowEmptyStrings = false), StringLength(255, MinimumLength = 1, ErrorMessage = "RoleName must be between 1 and 255 characters.")]
        public string? Name
        {
            get => _name;
            set => _name = value.Trim().ToLower()!;
        }
        public List<int> Permissions { get; set; } = null!;
    }
}
