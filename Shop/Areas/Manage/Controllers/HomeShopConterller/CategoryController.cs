using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.DAL;
using Shop.Models.HomeShop;
using Shop.ViewModels.HomeShopVM.CategoryVM;

namespace Shop.Areas.Manage.Controllers.HomeShopConterller
{
    [Area("Manage")]
    [Authorize(Roles = "Moderator, Admin")]
    public class CategoryController : Controller
    {
        readonly AppDbContext _context;
        readonly IWebHostEnvironment _web;
        public CategoryController(AppDbContext context, IWebHostEnvironment web)
        {
            _context = context;
            _web = web;
        }
        public IActionResult Index()
        {
            return View(_context.Categories.ToList());
        }
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateCategoryVM categoryVM  )
        {
            if (ModelState.IsValid) return View();
            if (categoryVM == null) return NotFound();
            IFormFile file = categoryVM.Image;
            //if(file.ContentType.Contains("image/"))
            //{
            //    ModelState.AddModelError("Image", "yyyYuklediyiniz sekil deyil");
            //    return View();
            //}
            if(file.Length>5*1024*1024)
            {
                ModelState.AddModelError("Image", "Sekilin olcusu 5 mb den cox ola bilmez");
                return View();
            }
            string fileName = Guid.NewGuid()+(file.FileName.Length>64 ? file.FileName.Substring(0,64): file.FileName);
            using (var stream = new FileStream(Path.Combine(_web.WebRootPath, "Admin", "images", "Category", fileName), FileMode.Create))
            {
                file.CopyTo(stream);
            }
            Category category = new Category
            {
                Image = categoryVM.Image,
                ImgUrl = fileName,
                Name = categoryVM.Name,
                Quantity = categoryVM.Quantity,
                CategoryProducts = categoryVM.CategoryProducts
            };
            category.Image=categoryVM.Image;
            category.Name=categoryVM.Name;
            category.Quantity=categoryVM.Quantity;
            category.CategoryProducts=categoryVM.CategoryProducts;
            _context.Categories.Add(category);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
