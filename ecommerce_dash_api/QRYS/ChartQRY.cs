namespace ecommerce_dash_api.QRYS
{
    public class ChartQRY
    {
        public int Id { get; set; }
        public string? Label { get; set; }
        public string? ChartType { get; set; }
        public string? Query { get; set; }
        public List<ChartPropertyQRY>? ChartProperties { get; set; }
    }
}
