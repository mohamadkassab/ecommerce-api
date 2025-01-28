namespace ecommerce_dash_api.Areas.Shop.QRYS
{
    public class SearchProductsQRY
    {
        public long totalProducts { get; set; }
        public List<ShopProductQRY> products { get; set; } = new List<ShopProductQRY>();
    }
}
