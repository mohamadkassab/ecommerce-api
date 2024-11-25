using ecommerce_dash_api.Attributes;
using ecommerce_dash_api.Models;
using System.ComponentModel.DataAnnotations;

namespace ecommerce_dash_api.DTOS
{
    public class RoleCreateDTO
    {
        private string _rolename = null!;
        [Required(AllowEmptyStrings = false), StringLength(255, MinimumLength = 1, ErrorMessage = "RoleName must be between 1 and 255 characters.")]
        public string RoleName
        {
            get => _rolename;
            set => _rolename = value?.ToLower()!;
        }
        public required List<int> Permissions { get; set; }
    }
}
