using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.DAL;
using Shop.Models.HomeShop;
using Shop.ViewModels.HomeShopVM.ProductVM;
using System;

namespace Shop.Areas.Manage.Controllers.HomeShopConterller
{
    [Area("Manage")]
    [Authorize(Roles = "Moderator, Admin")]
    public class ProductController : Controller
    {
        readonly AppDbContext _context;
        readonly IWebHostEnvironment _web;
        public ProductController(AppDbContext context, IWebHostEnvironment web)
        {
            _context = context;
            _web = web; 
        }
        public IActionResult Index()
        {
            return View(_context.Products.ToList());
        }
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateProductVM productVM)
        {
            if (ModelState.IsValid) return View();
            if (productVM == null)  return BadRequest();
            IFormFile file = productVM.Image;
            if (!file.ContentType.Contains("image/"))
            {
                ModelState.AddModelError("Image", "Yuklediyiniz sekil deyil");
                return View();
            }
            if (file.Length > 5 * 1024 * 1024)
            //200*1024 200kb demekdir bir defe de 1024 de vursaq 200 mb eliyir
            {
                ModelState.AddModelError("Image", "Sekil olcusu 5 mb den cox ola bilmez");
                return View();
            }
            string fileName = Guid.NewGuid() + (file.FileName.Length > 64 ? file.FileName.Substring(0, 64) : file.FileName);
            using (var stream = new FileStream(Path.Combine(_web.WebRootPath, "Admin", "images", "Product", fileName), FileMode.Create))
            {
                file.CopyTo(stream);
            }
            Product product = new Product
            {
            Name = productVM.Name,
            Image = productVM.Image,
            ImgUrl = productVM.ImgUrl,
            InitialPrice = productVM.InitialPrice,
            FinalPrice = productVM.FinalPrice,
            CategoryProducts = productVM.CategoryProducts,
            Quantity=productVM.Quantity,
            };
            product.Name = productVM.Name;
            product.Image = productVM.Image;
            product.ImgUrl = productVM.ImgUrl;
            product.InitialPrice = productVM.InitialPrice;
            product.FinalPrice = productVM.FinalPrice;
            product.CategoryProducts = productVM.CategoryProducts;
            product.Quantity = productVM.Quantity;
            _context.Products.Add(product);
            _context.SaveChanges();
            return RedirectToAction("Index","Home");
        }

    }
}
