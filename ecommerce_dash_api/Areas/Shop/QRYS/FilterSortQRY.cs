using ecommerce_dash_api.Enum;
using Elastic.Clients.Elasticsearch;

namespace ecommerce_dash_api.Areas.Shop.QRYS
{
    public class FilterSortQRY
    {
        public List<Dictionary<string, string>> SortingOptions { get; set; } = new List<Dictionary<string, string>>();
        public List<string> Brands { get; set; }
        public List<string> Categories { get; set; }
    }
}
