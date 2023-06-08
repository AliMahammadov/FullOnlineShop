using Shop.Models.HomeShop;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shop.ViewModels.HomeShopVM.CategoryVM
{
    public class UpdateCategoryVM
    {
        public string ImgUrl { get; set; }
        [NotMapped]
        public IFormFile Image { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public ICollection<CategoryProduct>? CategoryProducts { get; set; }
    }
}
