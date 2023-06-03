using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Shop.Areas.Manage.Controllers.HomeShopConterller
{
    [Area("Manage")]
    [Authorize(Roles = "Moderator, Admin")]
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
