namespace ecommerce_dash_api.Areas.Dashboard.QRYS
{
    public class ChartQRY
    {
        public int Id { get; set; }
        public string Label { get; set; } = null!;
        public string ChartType { get; set; } = null!;
        public string? Query { get; set; }
        public List<ChartPropertyQRY>? ChartProperties { get; set; }
    }
}
