using Microsoft.AspNetCore.Identity;

namespace Entidades
{
    public class ApplicationUser : IdentityUser
    { 
        public bool IsActive { get; set; }
        public string LastName { get; set; }
    }
}
