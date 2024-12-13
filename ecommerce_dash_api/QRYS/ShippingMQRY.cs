namespace ecommerce_dash_api.QRYS
{
    public class ShippingMQRY
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public byte[]? IconFile { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string? UpdatedBy { get; set; }
        public Boolean IsActive { get; set; }
    }
}
