using System.ComponentModel.DataAnnotations;

namespace ecommerce_dash_api.QRYS
{
    public class UserWithRolesQRY
    {
        public int Id { get; set; }
        public string Username { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public DateOnly Dob { get; set; }
        public string Phone { get; set; } = null!;
        public string? Address { get; set; }
        public List<RoleWithoutPermissionsQRY>? Roles { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string UpdatedBy { get; set; } = null!;
        public int FailedLoginAttempts { get; set; }
        public bool IsActive { get; set; }
    }
}
