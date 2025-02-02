using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ecommerce_dash_api.Areas.Shop.QRYS
{
    public class SearchProductsQRY
    {
        public long TotalProducts { get; set; }
        public List<ShopProductQRY> Products { get; set; } = new List<ShopProductQRY>();
        public FilterSortQRY FilterSortOptions { get; set; }
        public SelectedFilterSortQRY SelectedFilterSortOptions { get; set; }
    }
}
