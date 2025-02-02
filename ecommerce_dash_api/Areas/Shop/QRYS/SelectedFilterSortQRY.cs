namespace ecommerce_dash_api.Areas.Shop.QRYS
{
    public class SelectedFilterSortQRY
    {
        public string SortingOption { get; set; } 
        public List<string> Brands { get; set; }
        public List<string> Categories { get; set; }
    }
}
