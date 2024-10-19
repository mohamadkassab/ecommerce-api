using System.ComponentModel.DataAnnotations;

namespace ecommerce_dash_api.QRYS
{
    public class UserWithRolesAndPermissionsQRY
    {
        public int Id { get; set; }
        public string? FirstName { get; set; } 

        public string? LastName { get; set; } 

        public DateOnly? Dob { get; set; }

        public string? Phone { get; set; } 

        public string? Address { get; set; }

        public string? Username { get; set; } 

        public List<RoleWithoutPermissionsQRY>? Roles { get; set; }

        public List<PermissionQRY>? Permissions { get; set; }

        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }

    }
}
