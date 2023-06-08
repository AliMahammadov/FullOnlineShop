using Microsoft.AspNetCore.Mvc;
using Shop.Abstraction.Services;
using Shop.DAL;
using Shop.ViewModels.Home;
namespace Shop.Controllers
{
    public class HomeController : Controller
    {
        public readonly AppDbContext _context;
        public readonly IEmailService _emailService;
        public HomeController(AppDbContext context, IEmailService emailService)
        {
            _context = context;
            _emailService = emailService;
        }


        public IActionResult Index()
        {


            HomeVM homeVM = new HomeVM
            {
                Brands = _context.Brands.ToList(),
                Products= _context.Products.ToList(),
                Categories= _context.Categories.ToList(),
                

            };

            return View(homeVM);
        }
        public IActionResult SendMail()
        
        {
            _emailService.Send("e.mehemmedov99@gmail.com",
                "ayfon 14 boro maks",
                "indi endirimdedir");
            return View();

        }
    }
}