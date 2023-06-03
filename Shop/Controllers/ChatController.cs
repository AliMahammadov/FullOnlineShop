using Microsoft.AspNetCore.Mvc;

namespace Shop.Controllers
{
    public class ChatController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
