namespace ecommerce_dash_api.Areas.Dashboard.QRYS
{
    public class AttributeQRY
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public List<string> Options { get; set; } = null!;
        public DateTime UpdatedAt { get; set; }
        public string UpdatedBy { get; set; } = string.Empty;
    }
}
