using System.ComponentModel.DataAnnotations;

namespace Shop.ViewModels.HomeShopVM.BrandVM
{
    public class CreateBrandVM
    {
        //[StringLength(maximumLength: 50)]
        public IFormFile Image { get; set; }
    }   
}
