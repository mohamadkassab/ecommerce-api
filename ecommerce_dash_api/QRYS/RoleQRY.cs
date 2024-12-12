namespace ecommerce_dash_api.QRYS
{
    public class RoleQRY
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public List<PermissionQRY>? Permissions { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string? UpdatedBy { get; set; }
    }
}
