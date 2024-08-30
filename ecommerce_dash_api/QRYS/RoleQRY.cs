namespace ecommerce_dash_api.QRYS
{
    public class RoleQRY
    {
        public int Id { get; set; }
        public string RoleName { get; set; }
        public List<PermissionQRY> Permissions { get; set; }
    }
}
