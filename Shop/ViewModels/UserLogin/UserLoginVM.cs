using Microsoft.AspNetCore.Identity;
using Microsoft.Build;
using System.ComponentModel.DataAnnotations;

namespace Shop.ViewModels.UserLogin
{
    public class UserLoginVM : IdentityUser
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Paswword { get; set; }
        public bool IsPersistance { get; set; }
    }
}
