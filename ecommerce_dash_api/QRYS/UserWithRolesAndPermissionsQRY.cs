using System.ComponentModel.DataAnnotations;

namespace ecommerce_dash_api.QRYS
{
    public class UserWithRolesAndPermissionsQRY
    {
        public string? FirstName { get; set; } 

        public string? LastName { get; set; } 

        public int Age { get; set; }

        public string? Phone { get; set; } 

        public string? Address { get; set; }

        public string? Username { get; set; } 

        public List<RoleQRY>? Roles { get; set; }

        public List<PermissionQRY>? Permissions { get; set; }

    }
}
