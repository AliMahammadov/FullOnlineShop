using Shop.Models.HomeShop;

namespace Shop.ViewModels.Home
{
    public class HomeVM
    {
        public IEnumerable<Brand> Brands { get; set; }
        public IEnumerable<Product> Products { get; set;}
        public IEnumerable<Category> Categories { get; set; }
    }
}
