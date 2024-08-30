using ecommerce_dash_api.Attributes;
using System.ComponentModel.DataAnnotations;

namespace ecommerce_dash_api.DTOS
{
    public class UpdateRoleDTO
    {

        public int RoleId { get; set; }

        [Required]
        [AtLeastOneRequired]
        public required List<int>? PermissionIds { get; set; }
    }
}
