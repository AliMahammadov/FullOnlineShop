using Microsoft.AspNetCore.Identity;
namespace Shop.Models.User
{
    public class AppUser:IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
