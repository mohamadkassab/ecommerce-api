using ecommerce_dash_api.Attributes;
using ecommerce_dash_api.Models;
using System.ComponentModel.DataAnnotations;

namespace ecommerce_dash_api.DTOS
{
    public class CreateRoleDTO
    {
        [Required, StringLength(50, MinimumLength = 1, ErrorMessage = "RoleName must be between 1 and 50 characters.")]
        public string? RoleName { get; set; }

        [Required]
        [AtLeastOneRequired]
        public required List<int>? PermissionIds { get; set; }
    }
}
