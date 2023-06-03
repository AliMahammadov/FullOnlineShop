using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Shop.ViewModels;
using Shop.Models.User;
using Shop.ViewModels.UserLogin;

namespace Shop.Controllers;

public class AccountController : Controller
{
    private readonly RoleManager<IdentityRole> roleManager;

    UserManager<AppUser> _userManager { get; }
    SignInManager<AppUser> _signInManager { get; }
    RoleManager<IdentityRole> _roleManager{ get; }


    public AccountController(UserManager<AppUser> userManager,
       SignInManager<AppUser> signInManager,
       RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _roleManager = roleManager;
    }
    public IActionResult Register()
    {
      
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Register(UserRegisterVM registerVM)
    {
        if (!ModelState.IsValid) return View();
        AppUser user = await _userManager.FindByNameAsync(registerVM.Username);
        if (user != null)
        {
            ModelState.AddModelError("Username", "This User already exists");
            return View();
        }
        user = new AppUser
        {
            FirstName=registerVM.Name,
            LastName=registerVM.Surname,
            UserName=registerVM.Username,
            Email=registerVM.Email,
            

        };
        IdentityResult result= await _userManager.CreateAsync(user,registerVM.Password);
        if (!result.Succeeded) 
        {
            foreach (var item in result.Errors)
            {
                ModelState.AddModelError("",item.Description);
            }
            return View();
        }
        await _userManager.AddToRoleAsync(user, "Member");
        await _signInManager.SignInAsync(user, true);
        return RedirectToAction("Index", "Home");
    } 
    public IActionResult Login() 
    {
        return View();

    }
    [HttpPost]
    public async Task<IActionResult> Login( UserLoginVM loginVM)
    {
        if (!ModelState.IsValid) return View();
       AppUser user = await _userManager.FindByNameAsync(loginVM.UserName);
        if (user is null)
        {
            ModelState.AddModelError("", "Login or Password is wrong");
            return View();
        }
        var result = await _signInManager.PasswordSignInAsync(user, loginVM.Paswword, loginVM.IsPersistance, true);
        if (!result.Succeeded) 
        {
            ModelState.AddModelError("", "Login or Password is wrong");
            return View();
        }

        return RedirectToAction("Index", "Home");
    }
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Index","Home");
    }
    //public async Task<IActionResult> AddRoles()
    //{
    //    await _roleManager.CreateAsync(new IdentityRole { Name = "Member" });
    //    await _roleManager.CreateAsync(new IdentityRole { Name = "Moderator" });
    //    await _roleManager.CreateAsync(new IdentityRole { Name = "Admin" });
    //    return View();
    //}
    public async Task<IActionResult> Test()
    {
        var user = await _userManager.FindByNameAsync("nezi");
        await _userManager.AddToRoleAsync(user, "Member");
        //user = await _userManager.FindByNameAsync("Vali");
        //await _userManager.AddToRoleAsync(user, "Member");
        //    //    //ozumuze admin yaratmq ucun
        //AppUser user = new AppUser
        //{
        //    FirstName = "admin",
        //    LastName = "admin",
        //    UserName = "admin",
        //    Email = "tu6hwwz7l@code.edu.az"
        //};
        //await _userManager.CreateAsync(user, "Admin123");
        //await _userManager.AddToRoleAsync(user, "Admin");
        return View();
        }
        public async Task<IActionResult> AccessDenied() //error seyfesin gosterir
    {

        return View();
    }
}
 