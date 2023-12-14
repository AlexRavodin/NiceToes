using Microsoft.AspNetCore.Identity;

namespace NiceToes.AuthAPI.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
    }
}
