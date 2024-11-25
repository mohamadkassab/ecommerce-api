namespace ecommerce_dash_api.QRYS
{
    public class AttributeQRY
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string? UpdatedBy { get; set; }
        public List<OptionQRY>? Options { get; set; }
    }
}
