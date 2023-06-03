using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shop.DAL;
using Shop.Models.HomeShop;
using Shop.ViewModels.HomeShopVM.BrandVM;

namespace Shop.Areas.Manage.Controllers.HomeShopConterller
{
    [Area("Manage")]
    [Authorize(Roles = "Moderator, Admin")]

    public class BrandController : Controller
    {
        readonly AppDbContext _context;
        readonly IWebHostEnvironment _web;
        public BrandController(AppDbContext context, IWebHostEnvironment web)
        {
            _context = context;
            _web = web;
        }

        public IActionResult Index()
        {
            return View(_context.Brands.ToList());
        }
      
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(CreateBrandVM brandVM)
        {
            if (!ModelState.IsValid) return View();
            if (brandVM is null) return BadRequest();
            IFormFile file = brandVM.Image;
            if (!file.ContentType.Contains("image/"))
            {
                ModelState.AddModelError("Image", "Yuklediyiniz sekil deyil");
                return View();
            }
            if (file.Length > 5 * 1024*1024) 
                //200*1024 200kb demekdir bir defe de 1024 de vursaq 200 mb eliyir
            {
                ModelState.AddModelError("Image", "Sekil olcusu 5 mb den cox ola bilmez");
                return View();
            }
            string fileName = Guid.NewGuid() + (file.FileName.Length > 64 ? file.FileName.Substring(0, 64) : file.FileName);
            using (var stream = new FileStream(Path.Combine(_web.WebRootPath,"Admin","images","Brand",fileName), FileMode.Create))
            {
                file.CopyTo(stream);
            }

        Brand brand = new Brand { Image=brandVM.Image,ImgUrl=fileName};
            brand.Image = brandVM.Image;
            _context.Brands.Add(brand);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Delete(int id)
        {
            if (id == null) return NotFound();
            Brand brand = _context.Brands.Find(id);
            if (brand == null) return BadRequest();
            _context.Brands.Remove(brand);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> Update(int? id)
        {
            if (id == null) return BadRequest();
            var brand = _context.Brands.FirstOrDefault(x => x.Id == id);
            if (brand == null) return NotFound();

            UpdateBrandVM brandVM = new UpdateBrandVM { Brand = brand};

            return View(brandVM);
        }
        [HttpPost]
        public async Task<IActionResult> Update(int? id, UpdateBrandVM brand)
        {

            Brand existBrand = await _context.Brands.FirstOrDefaultAsync(x => x.Id == id);

            if (existBrand == null) return NotFound();

            if (!ModelState.IsValid) return View(brand);


            if (!brand.Image.ContentType.Contains("image/png"))
            {
                ModelState.AddModelError("Image", "Yuklediyiniz sekil deyil");
                return View();
            }
            if (brand.Image.Length > 5 * 1024 * 1024)
            //200*1024 200kb demekdir bir defe de 1024 de vursaq 200 mb eliyir
            {
                ModelState.AddModelError("Image", "Sekil olcusu 5 mb den cox ola bilmez");
                return View();
            }
            string fileName = Guid.NewGuid() + (brand.Image.FileName.Length > 64 ? brand.Image.FileName.Substring(brand.Image.FileName.Length-64, 64) : brand.Image.FileName);
            using (var stream = new FileStream(Path.Combine(_web.WebRootPath, "Admin", "images", "Brand", fileName), FileMode.Create))
            {
                brand.Image.CopyTo(stream);
            }
            UpdateBrandVM brandVM = new UpdateBrandVM
            {
                Image = brand.Image,
            };

            existBrand.ImgUrl = fileName;


            await _context.SaveChangesAsync();

            return RedirectToAction("index");

        }
    }
}
