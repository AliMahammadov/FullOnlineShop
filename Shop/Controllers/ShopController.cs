using Microsoft.AspNetCore.Mvc;

namespace Shop.Controllers
{
    public class ShopController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
