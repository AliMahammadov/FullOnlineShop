using Shop.Models.HomeShop;
using System.ComponentModel.DataAnnotations;

namespace Shop.ViewModels.HomeShopVM.BrandVM
{
    public class UpdateBrandVM
    {

        //[StringLength(maximumLength: 50)]
        public IFormFile? Image { get; set; }
        public Brand? Brand { get; set; }

    }
}
