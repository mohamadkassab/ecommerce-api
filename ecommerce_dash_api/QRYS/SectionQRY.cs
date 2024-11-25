namespace ecommerce_dash_api.QRYS
{
    public class SectionQRY
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string? UpdatedBy { get; set; }
        public List<CategoryQRY>? Categories { get; set; }
    }
}
