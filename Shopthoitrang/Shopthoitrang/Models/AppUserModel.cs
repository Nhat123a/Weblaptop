using Microsoft.AspNetCore.Identity;

namespace Shopthoitrang.Models
{
    public class AppUserModel:IdentityUser
    {
        public string Osname { get; set; }
    }
}
