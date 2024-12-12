namespace ecommerce_dash_api.QRYS
{
    public class CategoryQRY
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public DateTime UpdatedAt { get; set; }
        public string? UpdatedBy { get; set; }
    }
}
