using Microsoft.AspNetCore.Mvc;

namespace Shop.Controllers
{
    public class DetailController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
