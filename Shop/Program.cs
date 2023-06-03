using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Shop.Abstraction.Services;
using Shop.DAL;
using Shop.Hubs;
using Shop.Models.User;
using Shop.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<AppDbContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("default"));
});
builder.Services.AddIdentity<AppUser, IdentityRole>(option =>
{
    option.Password.RequireDigit = true;
    option.Password.RequireNonAlphanumeric = false;
    option.Password.RequiredLength = 5;
    option.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstu vwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789.";
    option.Lockout.AllowedForNewUsers = true;

}).AddDefaultTokenProviders().AddEntityFrameworkStores<AppDbContext>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddSignalR();
var app = builder.Build();
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");

}
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication(); // seyfede name gorunsun deye
app.UseAuthorization();
app.MapControllerRoute(
            name: "areas",
            pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}"
          );
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapHub<ChatHub>("/chatHub");
app.Run();